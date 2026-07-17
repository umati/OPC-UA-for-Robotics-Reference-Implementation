namespace Robotics.Client.Core.Client;

public sealed record MethodInvocationResult(
    MethodInvocationOutcome Outcome,
    string Target,
    string? ObjectId,
    string? MethodId,
    IReadOnlyList<MethodInvocationArgumentValue> InputArguments,
    string? CallStatusCode,
    bool Success,
    IReadOnlyList<MethodInvocationArgumentValue> OutputArguments,
    IReadOnlyList<string> InputArgumentResults,
    IReadOnlyList<string> DiagnosticInfos,
    IReadOnlyList<string> Warnings,
    string? Error,
    string? Evidence)
{
    public static MethodInvocationResult Failure(
        MethodInvocationOutcome outcome,
        string target,
        string error,
        string? evidence = null,
        IReadOnlyList<string>? warnings = null)
    {
        return new MethodInvocationResult(
            outcome,
            target,
            ObjectId: null,
            MethodId: null,
            InputArguments: [],
            CallStatusCode: null,
            Success: false,
            OutputArguments: [],
            InputArgumentResults: [],
            DiagnosticInfos: [],
            Warnings: warnings ?? [],
            Error: error,
            Evidence: evidence);
    }
}

public sealed record MethodInvocationArgumentValue(
    string Name,
    string DataType,
    int ValueRank,
    string ValueText);

public enum MethodInvocationOutcome
{
    Called,
    Missing,
    Ambiguous,
    InvalidNodeId,
    InvalidInput,
    UnsupportedMetadata,
    UnexpectedResult
}
