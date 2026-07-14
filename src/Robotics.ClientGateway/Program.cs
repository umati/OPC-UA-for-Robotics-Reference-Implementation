using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;
using Robotics.ClientGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpcUaOptions>(builder.Configuration.GetSection("OpcUa"));
builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection("Gateway"));
builder.Services.AddSingleton<RobotConnectionRegistry>();
builder.Services.AddScoped<GatewayOpcUaClient>();
builder.Services.AddScoped<LiveStreamService>();

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

var app = builder.Build();

app.UseCors();
app.UseWebSockets();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "Robotics.ClientGateway"
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

    OpcUaStatusDto status = await client.GetStatusAsync(robot.EndpointUrl, cancellationToken);
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

    DiscoveryDto discovery = await client.DiscoverAsync(robot.EndpointUrl, cancellationToken);
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
    SnapshotResult result = await client.GetSnapshotAsync(robot.EndpointUrl, parsedSelection, cancellationToken);
    return result.Snapshot is not null ? Results.Json(result.Snapshot, statusCode: result.StatusCode) : Results.Json(result.Error, statusCode: result.StatusCode);
});

app.MapGet("/ws/robotics/live", async (
    HttpContext context,
    LiveStreamService liveStreamService,
    CancellationToken cancellationToken) =>
{
    await liveStreamService.HandleAsync(context, cancellationToken);
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

app.Run();

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
