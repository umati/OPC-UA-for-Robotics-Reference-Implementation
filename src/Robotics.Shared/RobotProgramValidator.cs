namespace Robotics.Shared;

public static class RobotProgramValidator
{
    public static RobotProgramValidationResult Validate(RobotProgram? program)
    {
        List<string> errors = [];

        if (program is null)
        {
            errors.Add("Program is required.");
            return CreateResult(errors);
        }

        if (string.IsNullOrWhiteSpace(program.ProgramName))
        {
            errors.Add("ProgramName is required.");
        }

        if (program.Steps is null || program.Steps.Count == 0)
        {
            errors.Add("Program must contain at least one step.");
            return CreateResult(errors);
        }

        for (int index = 0; index < program.Steps.Count; index++)
        {
            RobotProgramStep? step = program.Steps[index];
            ValidateStep(step, index, errors);
        }

        return CreateResult(errors);
    }

    private static void ValidateStep(RobotProgramStep? step, int index, List<string> errors)
    {
        if (step is null)
        {
            errors.Add($"Step {index} is required.");
            return;
        }

        if (step.Type is null || !Enum.IsDefined(typeof(RobotProgramStepType), step.Type.Value))
        {
            errors.Add($"Step {index} has an unsupported or missing Type.");
            return;
        }

        switch (step.Type.Value)
        {
            case RobotProgramStepType.MoveJoint:
                ValidateMoveJointStep(step, index, errors);
                break;

            case RobotProgramStepType.Wait:
                ValidateWaitStep(step, index, errors);
                break;

            default:
                errors.Add($"Step {index} has an unsupported Type.");
                break;
        }
    }

    private static void ValidateMoveJointStep(RobotProgramStep step, int index, List<string> errors)
    {
        if (step.JointTarget is null)
        {
            errors.Add($"Step {index} MoveJoint requires JointTarget.");
        }

        if (!double.IsFinite(step.SpeedPercent) || step.SpeedPercent <= 0 || step.SpeedPercent > 100)
        {
            errors.Add($"Step {index} MoveJoint SpeedPercent must be greater than 0 and less than or equal to 100.");
        }

        if (step.JointTarget is not null && !IsFinite(step.JointTarget))
        {
            errors.Add($"Step {index} MoveJoint JointTarget values must be finite numbers.");
        }
    }

    private static void ValidateWaitStep(RobotProgramStep step, int index, List<string> errors)
    {
        if (step.DurationMs <= 0)
        {
            errors.Add($"Step {index} Wait DurationMs must be greater than 0.");
        }
    }

    private static bool IsFinite(JointMoveTarget target)
    {
        return double.IsFinite(target.S)
            && double.IsFinite(target.L)
            && double.IsFinite(target.U)
            && double.IsFinite(target.R)
            && double.IsFinite(target.B)
            && double.IsFinite(target.T);
    }

    private static RobotProgramValidationResult CreateResult(IReadOnlyList<string> errors)
    {
        return new RobotProgramValidationResult
        {
            IsValid = errors.Count == 0,
            ErrorMessages = errors
        };
    }
}
