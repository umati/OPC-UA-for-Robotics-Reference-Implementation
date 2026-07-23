namespace Robotics.ClientGateway.Dtos;

public sealed record DiscoveryDto(
    string EndpointUrl,
    bool Connected,
    int? RoboticsNamespaceIndex,
    IReadOnlyList<string> Warnings,
    IReadOnlyList<MotionDeviceSystemDto> MotionDeviceSystems);

public sealed record MotionDeviceSystemDto(
    NodeDiscoveryDto Node,
    NodeDiscoveryDto? ControllersFolder,
    NodeDiscoveryDto? MotionDevicesFolder,
    IReadOnlyList<ControllerDto> Controllers,
    IReadOnlyList<MotionDeviceDto> MotionDevices);

public sealed record ControllerDto(
    NodeDiscoveryDto Node,
    NodeDiscoveryDto? TaskControlsFolder,
    IReadOnlyList<TaskControlDto> TaskControls,
    OperationDto? SystemOperation);

public sealed record MotionDeviceDto(
    NodeDiscoveryDto Node,
    NodeDiscoveryDto? AxesFolder,
    NodeDiscoveryDto? PowerTrainsFolder,
    IReadOnlyList<NodeDiscoveryDto> Axes,
    IReadOnlyList<PowerTrainDto> PowerTrains);

public sealed record PowerTrainDto(
    NodeDiscoveryDto Node,
    IReadOnlyList<MotorDto> Motors);

public sealed record MotorDto(NodeDiscoveryDto Node);

public sealed record TaskControlDto(
    NodeDiscoveryDto Node,
    NodeDiscoveryDto? TaskControlOperation,
    IReadOnlyList<MethodDto> Methods);

public sealed record OperationDto(
    NodeDiscoveryDto Node,
    NodeDiscoveryDto? StateMachine,
    bool IsExpectedType,
    IReadOnlyList<MethodDto> Methods);

public sealed record MethodDto(
    string Name,
    bool Found,
    string? BrowseName,
    string? DisplayName,
    string? NodeId,
    string? ParentNodeId,
    MethodArgumentsDto InputArguments,
    MethodArgumentsDto OutputArguments,
    string Evidence);

public sealed record MethodArgumentsDto(
    string Status,
    string? PropertyNodeId,
    IReadOnlyList<MethodArgumentDto> Arguments,
    string? Diagnostic);

public sealed record MethodArgumentDto(
    string Name,
    string DataType,
    int ValueRank,
    string ArrayDimensions,
    string Description);

public sealed record NodeDiscoveryDto(
    string BrowseName,
    string DisplayName,
    string NodeId,
    string TypeDefinition,
    string Evidence,
    string? StableKey = null,
    string? NamespaceUri = null);
