namespace Robotics.ReferenceServer.ControlBridge;

internal enum RobotControlCommandFailureKind
{
    None,
    InvalidArgument,
    InvalidState,
    NotFound,
    NotSupported,
    Unexpected
}

internal sealed record RobotControlCommandResult(
    bool Accepted,
    string Message,
    RobotControlCommandFailureKind FailureKind = RobotControlCommandFailureKind.None)
{
    public static RobotControlCommandResult Accept(string message)
    {
        return new RobotControlCommandResult(true, message);
    }

    public static RobotControlCommandResult Reject(string message, RobotControlCommandFailureKind failureKind)
    {
        return new RobotControlCommandResult(false, message, failureKind);
    }
}
