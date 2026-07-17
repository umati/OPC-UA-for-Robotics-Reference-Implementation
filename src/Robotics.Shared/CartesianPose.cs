namespace Robotics.Shared;

public sealed record CartesianPose
{
    public required double X { get; init; }

    public required double Y { get; init; }

    public required double Z { get; init; }

    public required double Rx { get; init; }

    public required double Ry { get; init; }

    public required double Rz { get; init; }
}
