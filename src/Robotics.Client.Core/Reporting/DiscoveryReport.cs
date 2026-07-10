namespace Robotics.Client.Core.Reporting;

public sealed record DiscoveryReport(
    string EndpointUrl,
    bool Connected,
    int? RoboticsNamespaceIndex,
    IReadOnlyList<MotionDeviceSystemReport> MotionDeviceSystems,
    IReadOnlyList<string> Warnings);

public sealed record MotionDeviceSystemReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? ControllersFolder,
    NodeDiscoveryInfo? MotionDevicesFolder,
    IReadOnlyList<ControllerReport> Controllers,
    IReadOnlyList<MotionDeviceReport> MotionDevices);

public sealed record ControllerReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? TaskControlsFolder,
    IReadOnlyList<TaskControlReport> TaskControls,
    OperationReport? SystemOperation);

public sealed record MotionDeviceReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? AxesFolder,
    NodeDiscoveryInfo? PowerTrainsFolder,
    IReadOnlyList<NodeDiscoveryInfo> Axes,
    IReadOnlyList<NodeDiscoveryInfo> PowerTrains);

public sealed record TaskControlReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? TaskControlOperation,
    IReadOnlyList<MethodReport> Methods);

public sealed record OperationReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? StateMachine,
    bool IsExpectedType,
    IReadOnlyList<MethodReport> Methods);

public sealed record MethodReport(
    string Name,
    bool Found,
    string? BrowseName,
    string? DisplayName,
    string? NodeId,
    string? ParentNodeId,
    MethodArgumentsReport InputArguments,
    MethodArgumentsReport OutputArguments,
    string Evidence);

public sealed record MethodArgumentsReport(
    string Status,
    string? PropertyNodeId,
    IReadOnlyList<MethodArgumentReport> Arguments,
    string? Diagnostic)
{
    public static MethodArgumentsReport Missing { get; } = new("missing", null, [], null);
}

public sealed record MethodArgumentReport(
    string Name,
    string DataType,
    int ValueRank,
    string ArrayDimensions,
    string Description);

public sealed record NodeDiscoveryInfo(
    string BrowseName,
    string DisplayName,
    string NodeId,
    string TypeDefinition,
    string Evidence);
