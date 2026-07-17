namespace Robotics.Shared;

public sealed record RobotProgram
{
    public string ProgramName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public IReadOnlyList<RobotProgramStep> Steps { get; init; } = Array.Empty<RobotProgramStep>();
}
