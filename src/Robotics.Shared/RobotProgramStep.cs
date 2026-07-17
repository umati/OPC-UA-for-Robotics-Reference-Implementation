namespace Robotics.Shared;

public sealed record RobotProgramStep
{
    public RobotProgramStepType? Type { get; init; }

    public JointMoveTarget? JointTarget { get; init; }

    public double SpeedPercent { get; init; }

    public int DurationMs { get; init; }
}
