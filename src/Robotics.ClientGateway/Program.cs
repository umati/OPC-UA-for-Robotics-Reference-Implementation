using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;
using Robotics.ClientGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpcUaOptions>(builder.Configuration.GetSection("OpcUa"));
builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection("Gateway"));
builder.Services.AddScoped<GatewayOpcUaClient>();

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

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "Robotics.ClientGateway"
}));

app.MapGet("/api/opcua/status", async (GatewayOpcUaClient client, CancellationToken cancellationToken) =>
{
    return Results.Ok(await client.GetStatusAsync(cancellationToken));
});

app.MapGet("/api/robotics/discovery", async (GatewayOpcUaClient client, CancellationToken cancellationToken) =>
{
    return Results.Ok(await client.DiscoverAsync(cancellationToken));
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

app.Run();

static IResult ToMethodCallHttpResult(MethodCallResultDto result)
{
    return result.Response is not null
        ? Results.Json(result.Response, statusCode: result.StatusCode)
        : Results.Json(result.Error, statusCode: result.StatusCode);
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
