namespace Robotics.ClientGateway.Dtos;

public sealed record OpcUaStatusDto(
    string EndpointUrl,
    bool Connected,
    bool RoboticsNamespaceFound,
    int? RoboticsNamespaceIndex,
    string? Error);
