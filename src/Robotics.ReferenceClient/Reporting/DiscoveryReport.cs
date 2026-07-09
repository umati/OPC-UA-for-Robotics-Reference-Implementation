namespace Robotics.ReferenceClient.Reporting;

internal sealed record DiscoveryReport(
    string EndpointUrl,
    bool Connected,
    int? RoboticsNamespaceIndex,
    IReadOnlyList<MotionDeviceSystemReport> MotionDeviceSystems,
    IReadOnlyList<string> Warnings);

internal sealed record MotionDeviceSystemReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? ControllersFolder,
    NodeDiscoveryInfo? MotionDevicesFolder,
    IReadOnlyList<ControllerReport> Controllers,
    IReadOnlyList<MotionDeviceReport> MotionDevices);

internal sealed record ControllerReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? TaskControlsFolder,
    IReadOnlyList<TaskControlReport> TaskControls,
    OperationReport? SystemOperation);

internal sealed record MotionDeviceReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? AxesFolder,
    NodeDiscoveryInfo? PowerTrainsFolder,
    IReadOnlyList<NodeDiscoveryInfo> Axes,
    IReadOnlyList<NodeDiscoveryInfo> PowerTrains);

internal sealed record TaskControlReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? TaskControlOperation,
    IReadOnlyList<MethodReport> Methods);

internal sealed record OperationReport(
    NodeDiscoveryInfo Node,
    NodeDiscoveryInfo? StateMachine,
    bool IsExpectedType,
    IReadOnlyList<MethodReport> Methods);

internal sealed record MethodReport(
    string Name,
    bool Found,
    string? NodeId,
    string Evidence);

internal sealed record NodeDiscoveryInfo(
    string BrowseName,
    string DisplayName,
    string NodeId,
    string TypeDefinition,
    string Evidence);
