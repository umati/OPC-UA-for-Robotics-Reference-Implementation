using System.Globalization;

namespace Robotics.ReferenceClient.Client;

internal sealed record DemoCommand(
    string DemoName,
    string ProgramName,
    bool ProgramWasDefaulted,
    bool ContinueOnFailure,
    int DelayMilliseconds,
    int StopMode,
    bool StopModeWasSupplied,
    SnapshotOptions SnapshotOptions)
{
    public const string DefaultProgramName = "PickAndPlace";
    public const int DefaultDelayMilliseconds = 500;
    public const int DefaultStopMode = 0;

    public static bool TryParse(IReadOnlyList<string> args, out DemoCommand? command, out string? error)
    {
        command = null;
        error = null;

        int demoIndex = Array.FindIndex(args.ToArray(), argument => string.Equals(argument, "demo", StringComparison.OrdinalIgnoreCase));
        if (demoIndex < 0)
        {
            return false;
        }

        if (demoIndex + 1 >= args.Count)
        {
            error = "Usage: demo <system-ready-cycle|task-program-cycle|full-reference-cycle> [--program <name>] [--stop-mode <integer>] [--delay-ms <milliseconds>] [--continue-on-failure]";
            return false;
        }

        string demoName = args[demoIndex + 1];
        if (!IsKnownDemo(demoName))
        {
            error = $"Unknown demo '{demoName}'. Expected system-ready-cycle, task-program-cycle, or full-reference-cycle.";
            return false;
        }

        string programName = DefaultProgramName;
        bool programWasDefaulted = true;
        bool continueOnFailure = false;
        int delayMilliseconds = DefaultDelayMilliseconds;
        int stopMode = DefaultStopMode;
        bool stopModeWasSupplied = false;
        SnapshotOptions snapshotOptions = SnapshotOptions.None;

        int index = demoIndex + 2;
        while (index < args.Count)
        {
            string option = args[index];
            if (string.Equals(option, "--continue-on-failure", StringComparison.OrdinalIgnoreCase))
            {
                continueOnFailure = true;
                index++;
            }
            else if (SnapshotOptions.TryConsume(option, snapshotOptions, out SnapshotOptions updated))
            {
                snapshotOptions = updated;
                index++;
            }
            else if (string.Equals(option, "--program", StringComparison.OrdinalIgnoreCase))
            {
                if (index + 1 >= args.Count)
                {
                    error = "--program requires a program name.";
                    return false;
                }

                programName = args[index + 1];
                programWasDefaulted = false;
                index += 2;
            }
            else if (string.Equals(option, "--delay-ms", StringComparison.OrdinalIgnoreCase))
            {
                if (!TryReadNonNegativeInteger(args, index, "--delay-ms", out delayMilliseconds, out error))
                {
                    return false;
                }

                index += 2;
            }
            else if (string.Equals(option, "--stop-mode", StringComparison.OrdinalIgnoreCase))
            {
                if (!TryReadInteger(args, index, "--stop-mode", out stopMode, out error))
                {
                    return false;
                }

                stopModeWasSupplied = true;
                index += 2;
            }
            else
            {
                error = $"Unknown demo option '{option}'.";
                return false;
            }
        }

        if (string.IsNullOrWhiteSpace(programName))
        {
            error = "--program requires a non-empty program name.";
            return false;
        }

        command = new DemoCommand(
            demoName,
            programName,
            programWasDefaulted,
            continueOnFailure,
            delayMilliseconds,
            stopMode,
            stopModeWasSupplied,
            snapshotOptions);
        return true;
    }

    private static bool IsKnownDemo(string demoName)
    {
        return string.Equals(demoName, "system-ready-cycle", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(demoName, "task-program-cycle", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(demoName, "full-reference-cycle", StringComparison.OrdinalIgnoreCase);
    }

    private static bool TryReadNonNegativeInteger(
        IReadOnlyList<string> args,
        int optionIndex,
        string optionName,
        out int value,
        out string? error)
    {
        if (!TryReadInteger(args, optionIndex, optionName, out value, out error))
        {
            return false;
        }

        if (value < 0)
        {
            error = $"{optionName} must be greater than or equal to 0.";
            return false;
        }

        return true;
    }

    private static bool TryReadInteger(
        IReadOnlyList<string> args,
        int optionIndex,
        string optionName,
        out int value,
        out string? error)
    {
        value = 0;
        error = null;

        if (optionIndex + 1 >= args.Count)
        {
            error = $"{optionName} requires an integer value.";
            return false;
        }

        if (!int.TryParse(args[optionIndex + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
        {
            error = $"{optionName} value '{args[optionIndex + 1]}' is not a valid integer.";
            return false;
        }

        return true;
    }
}
