using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;

namespace Robotics.Shared.Tests;

public sealed class MotionInventoryTests
{
    [Fact]
    public void InventoryRetainsArbitraryAxisCountAndMotionDeviceOwnership()
    {
        MotionDeviceReport first = Device("DeviceA", ["SAxis", "LAxis", "Axis7"], "device-a");
        MotionDeviceReport second = Device("DeviceB", ["SAxis"], "device-b");
        DiscoveryReport discovery = Report(first, second);

        MotionInventoryReport inventory = MotionInventoryBuilder.Build(discovery);

        Assert.Equal(2, inventory.MotionDeviceSystems[0].MotionDevices.Count);
        Assert.Equal(3, inventory.MotionDeviceSystems[0].MotionDevices[0].Axes.Count);
        Assert.Contains(inventory.MotionDeviceSystems[0].MotionDevices[0].Axes, axis => axis.Axis.DisplayName == "Axis7");
        Assert.NotEqual(
            inventory.MotionDeviceSystems[0].MotionDevices[0].Axes[0].StableKey,
            inventory.MotionDeviceSystems[0].MotionDevices[1].Axes[0].StableKey);
        Assert.All(inventory.MotionDeviceSystems[0].MotionDevices.SelectMany(device => device.Axes), axis =>
            Assert.Equal(axis.MotionDeviceKey, inventory.MotionDeviceSystems[0].MotionDevices.Single(device => device.Axes.Contains(axis)).MotionDevice.StableKey));
    }

    [Fact]
    public void InventoryPreservesExactNonGoodStatusAndConvergesSnapshotIdentity()
    {
        MotionDeviceReport device = Device("DeviceA", ["Axis7"], "device-a");
        string axisKey = device.Axes[0].StableKey!;
        SnapshotValueReport snapshot = new(
            "DeviceA.Axes.Axis7.ParameterSet.ActualPosition", "ActualPosition", "ns=5;s=axis7/position", "Double", -1,
            "BadWaitingForInitialData", null, null, "42", false,
            StableKey: "position-key", MotionDeviceKey: device.Node.StableKey, AxisKey: axisKey);

        MotionInventoryReport inventory = MotionInventoryBuilder.Build(
            Report(device),
            new SnapshotReport([new SnapshotSectionReport("Axes", [snapshot])]));

        AxisInventoryReport axis = Assert.Single(inventory.MotionDeviceSystems[0].MotionDevices[0].Axes);
        Assert.Same(snapshot, axis.ActualPosition);
        Assert.Equal("BadWaitingForInitialData", axis.ActualPosition!.StatusCode);
        Assert.Equal(axisKey, axis.ActualPosition.AxisKey);
        Assert.Contains(axis.Diagnostics, diagnostic => diagnostic.Contains("non-Good", StringComparison.Ordinal));
    }

    [Fact]
    public void RuntimeIdentityUsesNamespaceUriAndNotNamespaceIndexAlone()
    {
        var namespaces = new Opc.Ua.NamespaceTable();
        namespaces.Append("urn:test:vendor");
        var identity = RuntimeIdentity.From(new Opc.Ua.NodeId("Axis7", 1), namespaces);

        Assert.Contains("urn%3Atest%3Avendor", identity.StableKey, StringComparison.Ordinal);
        Assert.DoesNotContain("BrowseName", identity.StableKey, StringComparison.Ordinal);
        Assert.Equal("ns=1;s=Axis7", identity.NodeId);
    }

    private static DiscoveryReport Report(params MotionDeviceReport[] devices) => new(
        "opc.tcp://fixture", true, 5,
        [new MotionDeviceSystemReport(Node("System", "system"), null, null, [], devices)], []);

    private static MotionDeviceReport Device(string name, string[] axisNames, string key) => new(
        Node(name, key), null, null,
        axisNames.Select(axis => Node(axis, $"{key}/{axis}")).ToArray(), []);

    private static NodeDiscoveryInfo Node(string name, string key) =>
        new(name, name, $"ns=5;s={key}", "AxisType", "synthetic fixture", key, "urn:test:vendor");
}
