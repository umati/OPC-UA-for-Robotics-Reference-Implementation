namespace Robotics.ReferenceClient.Client;

internal sealed record MethodCallCommand(
    MethodCallTarget Target,
    string MethodName,
    IReadOnlyList<string> InputValues,
    SnapshotOptions SnapshotOptions)
{
    public string TargetLabel => Target == MethodCallTarget.TaskControlOperation
        ? "TaskControlOperation"
        : "SystemOperation";

    public string QualifiedTarget => $"{TargetLabel}.{MethodName}";

    public static bool TryParse(IReadOnlyList<string> args, out MethodCallCommand? command, out string? error)
    {
        command = null;
        error = null;

        int callIndex = Array.FindIndex(args.ToArray(), argument => string.Equals(argument, "call", StringComparison.OrdinalIgnoreCase));
        if (callIndex < 0)
        {
            return false;
        }

        if (callIndex + 2 >= args.Count)
        {
            error = "Usage: call <task|system> <MethodName> [arguments...]";
            return false;
        }

        MethodCallTarget target;
        string targetText = args[callIndex + 1];
        if (string.Equals(targetText, "task", StringComparison.OrdinalIgnoreCase))
        {
            target = MethodCallTarget.TaskControlOperation;
        }
        else if (string.Equals(targetText, "system", StringComparison.OrdinalIgnoreCase))
        {
            target = MethodCallTarget.SystemOperation;
        }
        else
        {
            error = $"Unknown call target '{targetText}'. Expected 'task' or 'system'.";
            return false;
        }

        string methodName = args[callIndex + 2];
        if (string.IsNullOrWhiteSpace(methodName))
        {
            error = "Method name is required.";
            return false;
        }

        var inputValues = new List<string>();
        SnapshotOptions snapshotOptions = SnapshotOptions.None;
        for (int index = callIndex + 3; index < args.Count; index++)
        {
            if (SnapshotOptions.TryConsume(args[index], snapshotOptions, out SnapshotOptions updated))
            {
                snapshotOptions = updated;
                continue;
            }

            inputValues.Add(args[index]);
        }

        command = new MethodCallCommand(
            target,
            methodName,
            inputValues,
            snapshotOptions);
        return true;
    }
}

internal enum MethodCallTarget
{
    TaskControlOperation,
    SystemOperation
}
