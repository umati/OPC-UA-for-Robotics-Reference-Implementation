using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.InformationModel;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsServer : StandardServer
{
    private readonly RobotAddressSpaceMode _addressSpaceMode;

    public RoboticsServer(RobotAddressSpaceMode addressSpaceMode = RobotAddressSpaceMode.Both)
    {
        _addressSpaceMode = addressSpaceMode;
    }

    protected override MasterNodeManager CreateMasterNodeManager(
        IServerInternal server,
        ApplicationConfiguration configuration)
    {
        var nodeManagers = new INodeManager[]
        {
            new RoboticsNodeManager(server, configuration, _addressSpaceMode)
        };

        return new MasterNodeManager(server, configuration, null, nodeManagers);
    }
}
