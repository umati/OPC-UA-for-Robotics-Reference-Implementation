using Robotics.ClientGateway.Options;
using Robotics.ClientGateway.Services;

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

app.Run();
