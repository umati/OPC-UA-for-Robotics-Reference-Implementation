namespace Robotics.ClientGateway.Options;

public sealed class RobotConnectionOptions
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string EndpointUrl { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
}
