namespace Robotics.ClientGateway.Dtos;

public sealed record RobotConnectionDto(
    string Id,
    string DisplayName,
    string EndpointUrl,
    bool Enabled);

public sealed record RobotStatusDto(
    string RobotId,
    string DisplayName,
    string EndpointUrl,
    bool Connected,
    bool RoboticsNamespaceFound,
    int? RoboticsNamespaceIndex,
    string? Error);
