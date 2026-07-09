namespace Robotics.ReferenceClient.Client;

internal sealed record MethodCallCommand(
    MethodCallTarget Target,
    string MethodName,
    IReadOnlyList<string> InputValues)
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

        command = new MethodCallCommand(
            target,
            methodName,
            args.Skip(callIndex + 3).ToArray());
        return true;
    }
}

internal enum MethodCallTarget
{
    TaskControlOperation,
    SystemOperation
}
