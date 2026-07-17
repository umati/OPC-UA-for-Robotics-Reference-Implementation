namespace Robotics.Shared;

public sealed record RobotProgramValidationResult
{
    public required bool IsValid { get; init; }

    public required IReadOnlyList<string> ErrorMessages { get; init; }
}
