using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;
using Robotics.ClientGateway.Services;
using Robotics.ClientGateway.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection;

string packageRoot = ResolvePackageRoot(args);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = Path.Combine(packageRoot, "app", "wwwroot")
});
string robotsFilePath = Path.Combine(packageRoot, "robots.json");
try
{
    builder.Configuration.AddJsonFile(robotsFilePath, optional: true, reloadOnChange: false);
}
catch (Exception exception)
{
    Console.Error.WriteLine("Configuration error");
    Console.Error.WriteLine($"Could not parse {robotsFilePath}: {exception.Message}");
    Console.Error.WriteLine("Expected example: { \"robots\": [{ \"id\": \"my-robot\", \"displayName\": \"My Robot\", \"endpointUrl\": \"opc.tcp://host:4840/RoboticsServer\", \"enabled\": true }] }");
    Environment.ExitCode = 1;
    return;
}
string[] configuredUrls = builder.Configuration.GetSection("Gateway:Urls").Get<string[]>() ?? ["http://localhost:5080"];
string? commandLineUrls = builder.Configuration["urls"];
string? environmentUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (string.IsNullOrWhiteSpace(commandLineUrls) && string.IsNullOrWhiteSpace(environmentUrls) && configuredUrls.Length > 0)
    builder.WebHost.UseUrls(configuredUrls);

builder.Services.Configure<OpcUaOptions>(builder.Configuration.GetSection("OpcUa"));
builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection("Gateway"));
builder.Services.Configure<WorkbenchOptions>(builder.Configuration.GetSection("Workbench"));
builder.Services.AddSingleton<RobotConnectionRegistry>();
builder.Services.AddScoped<GatewayOpcUaClient>();
builder.Services.AddScoped<LiveStreamService>();

bool packagedMode = builder.Environment.IsProduction();
IReadOnlyList<string> configurationErrors = RobotConfigurationValidator.Validate(builder.Configuration, robotsFilePath, packagedMode);
if (configurationErrors.Count > 0)
{
    Console.Error.WriteLine("Configuration error");
    Console.Error.WriteLine();
    foreach (string error in configurationErrors) Console.Error.WriteLine($"- {error}");
    Console.Error.WriteLine();
    Console.Error.WriteLine($"Edit: {robotsFilePath}");
    Console.Error.WriteLine("Expected example: { \"robots\": [{ \"id\": \"my-robot\", \"displayName\": \"My Robot\", \"endpointUrl\": \"opc.tcp://host:4840/RoboticsServer\", \"enabled\": true }] }");
    Environment.ExitCode = 1;
    return;
}

string[] corsAllowedOrigins = builder.Configuration
    .GetSection("Gateway:CorsAllowedOrigins")
    .GetChildren()
    .Select(section => section.Value)
    .Where(value => !string.IsNullOrWhiteSpace(value))
    .Select(value => value!)
    .ToArray();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (corsAllowedOrigins.Length > 0)
        {
            policy.WithOrigins(corsAllowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

Directory.CreateDirectory(Path.Combine(packageRoot, builder.Environment.IsProduction() ? "logs" : "Logs"));
builder.Logging.AddProvider(new FileLoggerProvider(Path.Combine(packageRoot, builder.Environment.IsProduction() ? "logs" : "Logs", "RoboticsClientGateway.log")));
var app = builder.Build();
if (app.Environment.IsProduction())
{
    foreach (string directory in new[] { "application", "trusted", "rejected", "issuers" })
        Directory.CreateDirectory(Path.Combine(packageRoot, "certificates", directory));
}

// CORS is a development/Vite concern only. The packaged Workbench is same-origin,
// so applying the Vite policy in Production creates false browser CORS failures.
if (app.Environment.IsDevelopment())
{
    app.UseCors();
}
app.UseWebSockets();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "Robotics.ClientGateway"
}));

app.MapGet("/api/metadata", (IConfiguration configuration) => Results.Ok(new
{
    product = "OPC UA Robotics Workbench",
    version = typeof(Program).Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
        .OfType<System.Reflection.AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion,
    informationalVersion = typeof(Program).Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
        .OfType<System.Reflection.AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion,
    sourceRevisionId = typeof(Program).Assembly
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .FirstOrDefault(attribute =>
            string.Equals(attribute.Key, "SourceRevisionId", StringComparison.OrdinalIgnoreCase))?.Value
        ?? configuration["SourceRevisionId"] ?? configuration["SOURCE_REVISION_ID"] ?? "unavailable",
    runtimeIdentifier = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier
}));

app.MapGet("/api/opcua/status", async (GatewayOpcUaClient client, CancellationToken cancellationToken) =>
{
    return Results.Ok(await client.GetStatusAsync(cancellationToken));
});

app.MapGet("/api/robots", (RobotConnectionRegistry registry) =>
    Results.Ok(registry.GetEnabledRobots()));

app.MapGet("/api/robots/{robotId}/status", async Task<IResult> (
    string robotId,
    RobotConnectionRegistry registry,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    RobotConnectionOptions? robot = registry.FindEnabled(robotId);
    if (robot is null)
    {
        return Results.NotFound(new ErrorDto("Robot not found", $"Robot '{robotId}' is unknown or disabled.", robotId));
    }

    OpcUaStatusDto status = await client.GetStatusAsync(robot.EndpointUrl, cancellationToken, robot.Id, robot.DisplayName);
    var dto = new RobotStatusDto(robot.Id, robot.DisplayName, status.EndpointUrl, status.Connected,
        status.RoboticsNamespaceFound, status.RoboticsNamespaceIndex, status.Error);
    return status.Connected ? Results.Ok(dto) : Results.Json(dto, statusCode: StatusCodes.Status502BadGateway);
});

app.MapGet("/api/robotics/discovery", async (GatewayOpcUaClient client, CancellationToken cancellationToken) =>
{
    return Results.Ok(await client.DiscoverAsync(cancellationToken));
});

app.MapGet("/api/robots/{robotId}/discovery", async Task<IResult> (
    string robotId,
    RobotConnectionRegistry registry,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    RobotConnectionOptions? robot = registry.FindEnabled(robotId);
    if (robot is null)
        return Results.NotFound(new ErrorDto("Robot not found", $"Robot '{robotId}' is unknown or disabled.", robotId));

    DiscoveryDto discovery = await client.DiscoverAsync(robot.EndpointUrl, cancellationToken, robot.Id, robot.DisplayName);
    if (!discovery.Connected)
        return Results.Json(new ErrorDto("OPC UA connection failed", string.Join("; ", discovery.Warnings), robot.EndpointUrl), statusCode: StatusCodes.Status502BadGateway);
    if (discovery.RoboticsNamespaceIndex is null)
        return Results.Json(new ErrorDto("Robotics discovery failed", string.Join("; ", discovery.Warnings), robot.EndpointUrl), statusCode: StatusCodes.Status424FailedDependency);
    return Results.Ok(discovery);
});

app.MapGet("/api/robotics/snapshot", async Task<IResult> (
    string? selection,
    GatewayOpcUaClient client,
    Microsoft.Extensions.Options.IOptions<OpcUaOptions> opcUaOptions,
    CancellationToken cancellationToken) =>
{
    if (!TryParseSnapshotSelection(selection, out SnapshotSelection parsedSelection))
    {
        return Results.BadRequest(new ErrorDto(
            "Invalid snapshot selection",
            "selection must be one of: states, equipment, all.",
            opcUaOptions.Value.EndpointUrl));
    }

    SnapshotResult result = await client.GetSnapshotAsync(parsedSelection, cancellationToken);
    return result.Snapshot is not null
        ? Results.Json(result.Snapshot, statusCode: result.StatusCode)
        : Results.Json(result.Error, statusCode: result.StatusCode);
});

app.MapGet("/api/robots/{robotId}/snapshot", async Task<IResult> (
    string robotId, string? selection, RobotConnectionRegistry registry, GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    RobotConnectionOptions? robot = registry.FindEnabled(robotId);
    if (robot is null)
        return Results.NotFound(new ErrorDto("Robot not found", $"Robot '{robotId}' is unknown or disabled.", robotId));
    if (!TryParseSnapshotSelection(selection, out SnapshotSelection parsedSelection))
        return Results.BadRequest(new ErrorDto("Invalid snapshot selection", "selection must be one of: states, equipment, all.", robot.EndpointUrl));
    SnapshotResult result = await client.GetSnapshotAsync(robot.EndpointUrl, parsedSelection, cancellationToken, robot.Id, robot.DisplayName);
    return result.Snapshot is not null ? Results.Json(result.Snapshot, statusCode: result.StatusCode) : Results.Json(result.Error, statusCode: result.StatusCode);
});

app.MapGet("/api/robots/{robotId}/diagnostics", async Task<IResult> (string robotId, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken cancellationToken) =>
{
    RobotConnectionOptions? robot=registry.FindEnabled(robotId);
    if(robot is null)return Results.NotFound(new ErrorDto("Robot not found",$"Robot '{robotId}' is unknown or disabled.",robotId));
    var result=await client.GetDiagnosticsAsync(robot.Id,robot.DisplayName,robot.EndpointUrl,cancellationToken);
    return result.Diagnostics is not null?Results.Ok(result.Diagnostics):Results.Json(result.Error,statusCode:result.StatusCode);
});

app.MapGet("/ws/robotics/live", async (
    HttpContext context,
    LiveStreamService liveStreamService,
    CancellationToken cancellationToken) =>
{
    await liveStreamService.HandleAsync(context, cancellationToken);
});

app.MapGet("/ws/robots/{robotId}/live", async (
    string robotId,
    HttpContext context,
    RobotConnectionRegistry registry,
    LiveStreamService liveStreamService,
    CancellationToken cancellationToken) =>
{
    RobotConnectionOptions? robot = registry.FindEnabled(robotId);
    if (robot is null)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsJsonAsync(
            new ErrorDto("Robot not found", $"Robot '{robotId}' is unknown or disabled.", robotId), cancellationToken);
        return;
    }

    await liveStreamService.HandleAsync(context, robot, cancellationToken);
});

app.MapPost("/api/robotics/system/get-ready", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallSystemAsync(Opc.Ua.Robotics.BrowseNames.GetReady, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/system/start", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallSystemAsync(Opc.Ua.Robotics.BrowseNames.Start, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/system/stop", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallSystemAsync(Opc.Ua.Robotics.BrowseNames.Stop, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/system/stand-down", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallSystemAsync(Opc.Ua.Robotics.BrowseNames.StandDown, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/task/load-by-name", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallTaskAsync(Opc.Ua.Robotics.BrowseNames.LoadByName, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/task/start", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallTaskAsync(Opc.Ua.Robotics.BrowseNames.Start, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/task/stop", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallTaskAsync(Opc.Ua.Robotics.BrowseNames.Stop, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/task/reset-to-program-start", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallTaskAsync(Opc.Ua.Robotics.BrowseNames.ResetToProgramStart, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

app.MapPost("/api/robotics/task/unload-program", async Task<IResult> (
    [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request,
    GatewayOpcUaClient client,
    CancellationToken cancellationToken) =>
{
    MethodCallResultDto result = await client.CallTaskAsync(Opc.Ua.Robotics.BrowseNames.UnloadProgram, request, cancellationToken);
    return ToMethodCallHttpResult(result);
});

// C15: robot-scoped routing. Invocation remains the shared runtime-derived C9 path.
app.MapPost("/api/robots/{robotId}/system/get-ready", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, false, Opc.Ua.Robotics.BrowseNames.GetReady));
app.MapPost("/api/robots/{robotId}/system/start", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, false, Opc.Ua.Robotics.BrowseNames.Start));
app.MapPost("/api/robots/{robotId}/system/stop", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, false, Opc.Ua.Robotics.BrowseNames.Stop));
app.MapPost("/api/robots/{robotId}/system/stand-down", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, false, Opc.Ua.Robotics.BrowseNames.StandDown));
app.MapPost("/api/robots/{robotId}/task/load-by-name", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, true, Opc.Ua.Robotics.BrowseNames.LoadByName));
app.MapPost("/api/robots/{robotId}/task/start", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, true, Opc.Ua.Robotics.BrowseNames.Start));
app.MapPost("/api/robots/{robotId}/task/stop", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, true, Opc.Ua.Robotics.BrowseNames.Stop));
app.MapPost("/api/robots/{robotId}/task/reset-to-program-start", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, true, Opc.Ua.Robotics.BrowseNames.ResetToProgramStart));
app.MapPost("/api/robots/{robotId}/task/unload-program", (string robotId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] MethodCallRequestDto? request, RobotConnectionRegistry registry, GatewayOpcUaClient client, CancellationToken ct) => RobotCall(robotId, registry, client, request, ct, true, Opc.Ua.Robotics.BrowseNames.UnloadProgram));

app.MapFallback(async context =>
{
    if (context.Request.Path.StartsWithSegments("/api") || context.Request.Path.StartsWithSegments("/ws"))
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }

    string indexPath = Path.Combine(packageRoot, "app", "wwwroot", "index.html");
    if (File.Exists(indexPath)) await context.Response.SendFileAsync(indexPath);
    else context.Response.StatusCode = StatusCodes.Status404NotFound;
});

app.Lifetime.ApplicationStarted.Register(() =>
{
    var workbench = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<WorkbenchOptions>>().Value;
    string informationalVersion = typeof(Program).Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
        .OfType<System.Reflection.AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion ?? "unavailable";
    string version = typeof(Program).Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
        .OfType<System.Reflection.AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion ?? "unavailable";
    string sourceRevisionId = typeof(Program).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
        .FirstOrDefault(attribute => string.Equals(attribute.Key, "SourceRevisionId", StringComparison.OrdinalIgnoreCase))?.Value
        ?? "unavailable";
    app.Logger.LogInformation("OPC UA Robotics Workbench Version: {Version}; Revision: {Revision}; InformationalVersion: {InformationalVersion}. Configuration: {ConfigurationPath}",
        version, sourceRevisionId, informationalVersion, robotsFilePath);
    Console.WriteLine($"Version: {version}");
    Console.WriteLine($"Revision: {sourceRevisionId}");
    app.Logger.LogInformation("Enabled robots: {Robots}", string.Join(", ", app.Services.GetRequiredService<RobotConnectionRegistry>().GetEnabledRobots().Select(robot => $"{robot.Id} ({robot.EndpointUrl})")));
    app.Logger.LogInformation("Workbench: {WorkbenchUrl}; Logs: {LogDirectory}; Certificates: {CertificateDirectory}", workbench.Url,
        Path.Combine(packageRoot, "logs"), Path.Combine(packageRoot, "certificates"));
    if (configuredUrls.Any(url => url.Contains("0.0.0.0", StringComparison.OrdinalIgnoreCase)))
        app.Logger.LogWarning("LAN binding is enabled. Use this package only on a trusted private demo network; it is not hardened for public internet exposure.");
    Console.WriteLine("Press Ctrl+C to stop.");
    if (packagedMode && workbench.OpenBrowserOnStartup)
    {
        try
        {
            Process.Start(new ProcessStartInfo { FileName = workbench.Url, UseShellExecute = true });
        }
        catch (Exception ex) { app.Logger.LogWarning(ex, "Could not open the browser automatically. Open {WorkbenchUrl} manually.", workbench.Url); }
    }
});

app.Run();

static string ResolvePackageRoot(string[] args)
{
    for (int index = 0; index < args.Length; index++)
    {
        const string prefix = "--PackageRoot=";
        if (args[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            return Path.GetFullPath(args[index][prefix.Length..]);
        if (string.Equals(args[index], "--PackageRoot", StringComparison.OrdinalIgnoreCase) && index + 1 < args.Length)
            return Path.GetFullPath(args[index + 1]);
    }

    string? environment = Environment.GetEnvironmentVariable("OPCUA_ROBOTICS_WORKBENCH_ROOT");
    if (!string.IsNullOrWhiteSpace(environment))
        return Path.GetFullPath(environment);

    return AppContext.BaseDirectory;
}

static IResult ToMethodCallHttpResult(MethodCallResultDto result)
{
    return result.Response is not null
        ? Results.Json(result.Response, statusCode: result.StatusCode)
        : Results.Json(result.Error, statusCode: result.StatusCode);
}

static async Task<IResult> RobotCall(string robotId, RobotConnectionRegistry registry, GatewayOpcUaClient client,
    MethodCallRequestDto? request, CancellationToken cancellationToken, bool task, string methodName)
{
    RobotConnectionOptions? robot = registry.FindEnabled(robotId);
    if (robot is null)
        return Results.NotFound(new ErrorDto("Robot not found", $"Robot '{robotId}' is unknown or disabled.", robotId));

    MethodCallResultDto result = task
        ? await client.CallTaskAsync(robot.EndpointUrl, methodName, request, cancellationToken)
        : await client.CallSystemAsync(robot.EndpointUrl, methodName, request, cancellationToken);
    return ToMethodCallHttpResult(result);
}

static bool TryParseSnapshotSelection(string? selection, out SnapshotSelection parsed)
{
    if (string.IsNullOrWhiteSpace(selection) ||
        string.Equals(selection, "all", StringComparison.OrdinalIgnoreCase))
    {
        parsed = SnapshotSelection.All;
        return true;
    }

    if (string.Equals(selection, "states", StringComparison.OrdinalIgnoreCase))
    {
        parsed = SnapshotSelection.States;
        return true;
    }

    if (string.Equals(selection, "equipment", StringComparison.OrdinalIgnoreCase))
    {
        parsed = SnapshotSelection.Equipment;
        return true;
    }

    parsed = SnapshotSelection.All;
    return false;
}
