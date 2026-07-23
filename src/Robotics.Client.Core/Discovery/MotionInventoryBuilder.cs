using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Discovery;

public static class MotionInventoryBuilder
{
    public static MotionInventoryReport Build(DiscoveryReport discovery, SnapshotReport? snapshot = null)
    {
        var diagnostics = new List<string>();
        var values = snapshot?.Sections.SelectMany(section => section.Values).ToArray() ?? [];
        var seenAxisKeys = new HashSet<string>(StringComparer.Ordinal);
        var systems = discovery.MotionDeviceSystems.Select(system => new MotionDeviceSystemInventoryReport(
            system.Node,
            system.MotionDevices.Select(device =>
            {
                var axes = device.Axes.Select(axis =>
                {
                    string axisKey = axis.StableKey ?? $"nodeid={axis.NodeId}";
                    string deviceKey = device.Node.StableKey ?? $"nodeid={device.Node.NodeId}";
                    if (!seenAxisKeys.Add(axisKey)) diagnostics.Add($"Duplicate logical Axis identity detected: {axisKey}.");
                    SnapshotValueReport? position = values.FirstOrDefault(value =>
                        string.Equals(value.AxisKey, axisKey, StringComparison.Ordinal) &&
                        value.BrowseName.EndsWith("ActualPosition", StringComparison.Ordinal));
                    var axisDiagnostics = new List<string>();
                    if (position is null) axisDiagnostics.Add("ActualPosition was not discovered for this Axis.");
                    else if (!position.StatusCode.StartsWith("Good", StringComparison.OrdinalIgnoreCase)) axisDiagnostics.Add($"ActualPosition has non-Good StatusCode {position.StatusCode}.");
                    if (position is not null && position.EngineeringUnits is null && position.EngineeringUnit is null) axisDiagnostics.Add("Engineering-unit metadata is missing or undecodable.");
                    return new AxisInventoryReport(axis, deviceKey, axisKey, position, axisDiagnostics);
                }).ToArray();
                if (device.Node.StableKey is null) diagnostics.Add($"MotionDevice {device.Node.DisplayName} has no stable identity.");
                return new MotionDeviceInventoryReport(device.Node, axes, []);
            }).ToArray())).ToArray();
        return new MotionInventoryReport(discovery.EndpointUrl, systems, diagnostics);
    }
}
