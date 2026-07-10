namespace Robotics.ClientGateway.Dtos;

public sealed record ErrorDto(
    string Error,
    string Details,
    string EndpointUrl);
