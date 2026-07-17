namespace Robotics.Shared;

public sealed record RobotTelemetrySnapshot
{
    public required string Manufacturer { get; init; }

    public required string Model { get; init; }

    public required string SerialNumber { get; init; }

    public required string ProductCode { get; init; }

    public required bool IsMoving { get; init; }

    public required double Speed { get; init; }

    public required double Acceleration { get; init; }

    public required CartesianPose CartesianPose { get; init; }

    public required IReadOnlyList<RobotAxisState> AxisStates { get; init; }

    public required DateTimeOffset TimestampUtc { get; init; }
}
