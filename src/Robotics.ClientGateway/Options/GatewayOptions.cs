namespace Robotics.ClientGateway.Options;

public sealed class GatewayOptions
{
    public string[] CorsAllowedOrigins { get; set; } = [];
    public string[] Urls { get; set; } = ["http://localhost:5080"];
}

public sealed class WorkbenchOptions
{
    public bool OpenBrowserOnStartup { get; set; }
    public string Url { get; set; } = "http://localhost:5080";
}
