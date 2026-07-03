namespace Robotics.Shared;

public sealed record RobotAxisState
{
    public required RobotAxisName AxisName { get; init; }

    public required double PositionDegrees { get; init; }

    public required double VelocityDegreesPerSecond { get; init; }

    public required double TargetPositionDegrees { get; init; }

    public required double MotorLoadPercent { get; init; }

    public required double TemperatureCelsius { get; init; }

    public required double MinPositionDegrees { get; init; }

    public required double MaxPositionDegrees { get; init; }

    public required double MaxVelocityDegreesPerSecond { get; init; }

    public required double MaxAccelerationDegreesPerSecondSquared { get; init; }
}
