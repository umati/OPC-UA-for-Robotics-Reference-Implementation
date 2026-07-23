namespace Robotics.Client.Core.Reporting;

public sealed record MotionInventoryReport(
    string RobotIdentity,
    IReadOnlyList<MotionDeviceSystemInventoryReport> MotionDeviceSystems,
    IReadOnlyList<string> Diagnostics);

public sealed record MotionDeviceSystemInventoryReport(
    NodeDiscoveryInfo System,
    IReadOnlyList<MotionDeviceInventoryReport> MotionDevices);

public sealed record MotionDeviceInventoryReport(
    NodeDiscoveryInfo MotionDevice,
    IReadOnlyList<AxisInventoryReport> Axes,
    IReadOnlyList<string> Diagnostics);

public sealed record AxisInventoryReport(
    NodeDiscoveryInfo Axis,
    string MotionDeviceKey,
    string StableKey,
    SnapshotValueReport? ActualPosition,
    IReadOnlyList<string> Diagnostics);
