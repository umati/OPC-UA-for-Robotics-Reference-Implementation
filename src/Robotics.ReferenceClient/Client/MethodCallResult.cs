namespace Robotics.ReferenceClient.Client;

internal sealed record MethodCallResult(
    string StepName,
    bool Succeeded,
    string? StatusCode,
    string? Error)
{
    public static MethodCallResult Success(string stepName, string statusCode)
    {
        return new MethodCallResult(stepName, true, statusCode, null);
    }

    public static MethodCallResult Failed(string stepName, string error)
    {
        return new MethodCallResult(stepName, false, null, error);
    }
}
