namespace Robotics.ClientGateway.Options;

public sealed class OpcUaOptions
{
    public string EndpointUrl { get; set; } = "opc.tcp://localhost:4840/RoboticsReferenceServer";
    public bool AutoAcceptUntrustedCertificates { get; set; } = true;
}
