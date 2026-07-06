namespace Robotics.ReferenceServer.InformationModel;

public sealed class RobotNodeBinder
{
    // This class is intentionally not wired into the running server yet.
    // It will later locate nodes loaded from SixAxisRobot.Instance.NodeSet2.xml,
    // bind them into RobotNodeHandles, and provide the handles used to update
    // selected OPC UA Robotics nodes from simulation values.
    public RobotNodeHandles Bind()
    {
        return new RobotNodeHandles();
    }
}
