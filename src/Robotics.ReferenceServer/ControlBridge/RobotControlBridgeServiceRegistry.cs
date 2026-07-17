namespace Robotics.ReferenceServer.ControlBridge;

internal sealed class RobotControlBridgeServiceRegistry
{
    private RobotControlCommandService? _commandService;

    public RobotControlCommandService? CommandService => Volatile.Read(ref _commandService);

    public void SetCommandService(RobotControlCommandService commandService)
    {
        Volatile.Write(ref _commandService, commandService);
    }
}
