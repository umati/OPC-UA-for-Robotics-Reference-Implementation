namespace Robotics.Shared;

public sealed record RobotJointLimits
{
    public required RobotAxisName AxisName { get; init; }

    public required double MinPositionDegrees { get; init; }

    public required double MaxPositionDegrees { get; init; }

    public required double MaxVelocityDegreesPerSecond { get; init; }

    public required double MaxAccelerationDegreesPerSecondSquared { get; init; }
}
