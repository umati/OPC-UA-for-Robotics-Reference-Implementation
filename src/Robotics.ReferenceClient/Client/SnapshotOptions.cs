namespace Robotics.ReferenceClient.Client;

internal sealed record SnapshotOptions(bool Before, bool After)
{
    public static SnapshotOptions None { get; } = new(Before: false, After: false);

    public bool Enabled => Before || After;

    public static bool TryConsume(
        string argument,
        SnapshotOptions current,
        out SnapshotOptions updated)
    {
        updated = current;

        if (string.Equals(argument, "--snapshot", StringComparison.OrdinalIgnoreCase))
        {
            updated = new SnapshotOptions(Before: true, After: true);
            return true;
        }

        if (string.Equals(argument, "--snapshot-before", StringComparison.OrdinalIgnoreCase))
        {
            updated = current with { Before = true };
            return true;
        }

        if (string.Equals(argument, "--snapshot-after", StringComparison.OrdinalIgnoreCase))
        {
            updated = current with { After = true };
            return true;
        }

        return false;
    }
}
