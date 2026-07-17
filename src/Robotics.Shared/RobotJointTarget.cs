namespace Robotics.Shared;

public sealed record RobotJointTarget
{
    public required RobotAxisName AxisName { get; init; }

    public required double TargetPositionDegrees { get; init; }
}
