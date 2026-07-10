namespace Robotics.Client.Core.Client;

public sealed record MethodInvocationRequest(
    MethodInvocationTarget Target,
    string MethodName,
    IReadOnlyList<string> PositionalInputValues,
    IReadOnlyDictionary<string, string> NamedInputValues,
    IReadOnlyDictionary<string, string> InputAliases)
{
    public string TargetLabel => Target == MethodInvocationTarget.TaskControlOperation
        ? "TaskControlOperation"
        : "SystemOperation";

    public string QualifiedTarget => $"{TargetLabel}.{MethodName}";

    public static MethodInvocationRequest Positional(
        MethodInvocationTarget target,
        string methodName,
        IReadOnlyList<string> inputValues)
    {
        return new MethodInvocationRequest(target, methodName, inputValues, new Dictionary<string, string>(), new Dictionary<string, string>());
    }
}

public enum MethodInvocationTarget
{
    TaskControlOperation,
    SystemOperation
}
