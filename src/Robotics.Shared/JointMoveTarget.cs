namespace Robotics.Shared;

public sealed record JointMoveTarget
{
    public double S { get; init; }

    public double L { get; init; }

    public double U { get; init; }

    public double R { get; init; }

    public double B { get; init; }

    public double T { get; init; }
}
