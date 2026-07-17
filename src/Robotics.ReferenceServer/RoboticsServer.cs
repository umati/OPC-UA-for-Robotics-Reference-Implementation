using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.ControlBridge;
using Robotics.ReferenceServer.InformationModel;
using Robotics.ReferenceServer.Telemetry;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsServer : StandardServer
{
    private readonly RobotAddressSpaceMode _addressSpaceMode;
    private readonly IRobotTelemetryPublisher? _telemetryPublisher;
    private readonly RobotControlBridgeServiceRegistry? _controlBridgeServiceRegistry;

    public RoboticsServer(
        RobotAddressSpaceMode addressSpaceMode = RobotAddressSpaceMode.Both,
        IRobotTelemetryPublisher? telemetryPublisher = null,
        RobotControlBridgeServiceRegistry? controlBridgeServiceRegistry = null)
    {
        _addressSpaceMode = addressSpaceMode;
        _telemetryPublisher = telemetryPublisher;
        _controlBridgeServiceRegistry = controlBridgeServiceRegistry;
    }

    protected override MasterNodeManager CreateMasterNodeManager(
        IServerInternal server,
        ApplicationConfiguration configuration)
    {
        var nodeManagers = new INodeManager[]
        {
            new RoboticsNodeManager(server, configuration, _addressSpaceMode, _telemetryPublisher, _controlBridgeServiceRegistry)
        };

        return new MasterNodeManager(server, configuration, null, nodeManagers);
    }
}
