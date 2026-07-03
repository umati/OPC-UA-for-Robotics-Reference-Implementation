using Robotics.Shared;

namespace Robotics.ReferenceServer.Simulation;

public sealed class RobotSimulationService
{
    private readonly SixAxisRobot _robot = new();

    public RobotTelemetrySnapshot GetSnapshot()
    {
        return _robot.GetSnapshot();
    }

    public void Update(TimeSpan elapsed)
    {
        _robot.Update(elapsed);
    }

    public void SetJointTarget(RobotJointTarget target)
    {
        _robot.SetJointTarget(target);
    }

    public void SetJointTargets(IEnumerable<RobotJointTarget> targets)
    {
        _robot.SetJointTargets(targets);
    }
}
