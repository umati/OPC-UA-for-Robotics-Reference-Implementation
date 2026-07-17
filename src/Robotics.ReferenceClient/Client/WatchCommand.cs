using System.Globalization;

namespace Robotics.ReferenceClient.Client;

internal sealed record WatchCommand(
    int DurationSeconds,
    int PublishingIntervalMilliseconds,
    int SamplingIntervalMilliseconds,
    uint QueueSize,
    bool DiscardOldest,
    WatchSelection Selection,
    bool SelectionWasDefaulted,
    bool VerboseWatch)
{
    public const int DefaultDurationSeconds = 30;
    public const int DefaultPublishingIntervalMilliseconds = 500;
    public const int DefaultSamplingIntervalMilliseconds = 250;
    public const uint DefaultQueueSize = 10;
    public const bool DefaultDiscardOldest = true;

    public string SelectionLabel => Selection switch
    {
        WatchSelection.States => "states",
        WatchSelection.Equipment => "equipment",
        WatchSelection.All => "all",
        _ => "states"
    };

    public static bool TryParse(IReadOnlyList<string> args, out WatchCommand? command, out string? error)
    {
        command = null;
        error = null;

        int watchIndex = Array.FindIndex(args.ToArray(), argument => string.Equals(argument, "watch", StringComparison.OrdinalIgnoreCase));
        if (watchIndex < 0)
        {
            return false;
        }

        int durationSeconds = DefaultDurationSeconds;
        int publishingIntervalMilliseconds = DefaultPublishingIntervalMilliseconds;
        int samplingIntervalMilliseconds = DefaultSamplingIntervalMilliseconds;
        uint queueSize = DefaultQueueSize;
        bool discardOldest = DefaultDiscardOldest;
        bool states = false;
        bool equipment = false;
        bool all = false;
        bool verboseWatch = false;

        int index = watchIndex + 1;
        while (index < args.Count)
        {
            string option = args[index];
            if (option.StartsWith("opc.tcp://", StringComparison.OrdinalIgnoreCase))
            {
                index++;
                continue;
            }

            if (string.Equals(option, "--states", StringComparison.OrdinalIgnoreCase))
            {
                states = true;
                index++;
            }
            else if (string.Equals(option, "--equipment", StringComparison.OrdinalIgnoreCase))
            {
                equipment = true;
                index++;
            }
            else if (string.Equals(option, "--all", StringComparison.OrdinalIgnoreCase))
            {
                all = true;
                index++;
            }
            else if (string.Equals(option, "--verbose-watch", StringComparison.OrdinalIgnoreCase))
            {
                verboseWatch = true;
                index++;
            }
            else if (TryConsumePositiveInt(args, ref index, "--duration-seconds", out int parsedDuration, out error))
            {
                durationSeconds = parsedDuration;
            }
            else if (error is not null)
            {
                return true;
            }
            else if (TryConsumePositiveInt(args, ref index, "--publishing-interval-ms", out int parsedPublishingInterval, out error))
            {
                publishingIntervalMilliseconds = parsedPublishingInterval;
            }
            else if (error is not null)
            {
                return true;
            }
            else if (TryConsumePositiveInt(args, ref index, "--sampling-interval-ms", out int parsedSamplingInterval, out error))
            {
                samplingIntervalMilliseconds = parsedSamplingInterval;
            }
            else if (error is not null)
            {
                return true;
            }
            else if (TryConsumePositiveUInt(args, ref index, "--queue-size", out uint parsedQueueSize, out error))
            {
                queueSize = parsedQueueSize;
            }
            else if (error is not null)
            {
                return true;
            }
            else if (TryConsumeBool(args, ref index, "--discard-oldest", out bool parsedDiscardOldest, out error))
            {
                discardOldest = parsedDiscardOldest;
            }
            else if (error is not null)
            {
                return true;
            }
            else
            {
                error = $"Unknown watch option '{option}'.";
                return true;
            }
        }

        bool selectionWasDefaulted = !states && !equipment && !all;
        WatchSelection selection = all || (states && equipment)
            ? WatchSelection.All
            : equipment
                ? WatchSelection.Equipment
                : WatchSelection.States;

        command = new WatchCommand(
            durationSeconds,
            publishingIntervalMilliseconds,
            samplingIntervalMilliseconds,
            queueSize,
            discardOldest,
            selection,
            selectionWasDefaulted,
            verboseWatch);
        return true;
    }

    private static bool TryConsumePositiveInt(
        IReadOnlyList<string> args,
        ref int index,
        string optionName,
        out int value,
        out string? error)
    {
        value = 0;
        error = null;
        if (!string.Equals(args[index], optionName, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (index + 1 >= args.Count)
        {
            error = $"Option {optionName} requires a value.";
            return false;
        }

        if (!int.TryParse(args[index + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out value) || value <= 0)
        {
            error = $"Option {optionName} requires a positive integer.";
            return false;
        }

        index += 2;
        return true;
    }

    private static bool TryConsumePositiveUInt(
        IReadOnlyList<string> args,
        ref int index,
        string optionName,
        out uint value,
        out string? error)
    {
        value = 0;
        error = null;
        if (!string.Equals(args[index], optionName, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (index + 1 >= args.Count)
        {
            error = $"Option {optionName} requires a value.";
            return false;
        }

        if (!uint.TryParse(args[index + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out value) || value == 0)
        {
            error = $"Option {optionName} requires a positive integer.";
            return false;
        }

        index += 2;
        return true;
    }

    private static bool TryConsumeBool(
        IReadOnlyList<string> args,
        ref int index,
        string optionName,
        out bool value,
        out string? error)
    {
        value = false;
        error = null;
        if (!string.Equals(args[index], optionName, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (index + 1 >= args.Count)
        {
            error = $"Option {optionName} requires true or false.";
            return false;
        }

        if (!bool.TryParse(args[index + 1], out value))
        {
            error = $"Option {optionName} requires true or false.";
            return false;
        }

        index += 2;
        return true;
    }
}

internal enum WatchSelection
{
    States,
    Equipment,
    All
}
