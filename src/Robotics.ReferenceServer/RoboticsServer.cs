using Opc.Ua;
using Opc.Ua.Server;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsServer : StandardServer
{
    protected override MasterNodeManager CreateMasterNodeManager(
        IServerInternal server,
        ApplicationConfiguration configuration)
    {
        var nodeManagers = new INodeManager[]
        {
            new RoboticsNodeManager(server, configuration)
        };

        return new MasterNodeManager(server, configuration, null, nodeManagers);
    }
}
