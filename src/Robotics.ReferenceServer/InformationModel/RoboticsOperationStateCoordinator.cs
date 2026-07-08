using Robotics.ReferenceServer.ControlBridge;
using Robotics.ReferenceServer.Simulation;

namespace Robotics.ReferenceServer.InformationModel;

internal sealed class RoboticsOperationStateCoordinator
{
    private static readonly TimeSpan ObservableTransitionDelay = TimeSpan.FromMilliseconds(1200);

    private readonly RobotProgramExecutor _programExecutor;
    private RoboticsTaskControlBinding? _taskControlBinding;
    private RoboticsSystemOperationBinding? _systemOperationBinding;
    private RobotProgramExecutionState? _lastExecutionState;
    private RoboticsTransitionReason _nextProgramTransitionReason = RoboticsTransitionReason.System;

    public RoboticsOperationStateCoordinator(RobotProgramExecutor programExecutor)
    {
        _programExecutor = programExecutor ?? throw new ArgumentNullException(nameof(programExecutor));
    }

    public void Bind(
        RoboticsTaskControlBinding taskControlBinding,
        RoboticsSystemOperationBinding systemOperationBinding)
    {
        _taskControlBinding = taskControlBinding;
        _systemOperationBinding = systemOperationBinding;
    }

    public void Initialize(RobotProgramExecutionSnapshot snapshot, Opc.Ua.ISystemContext context)
    {
        _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Idle, context, RoboticsTransitionReason.System);
        _systemOperationBinding?.SetIdleSubstate(RoboticsIdleSubstate.StandBy, context, RoboticsTransitionReason.System);
        _systemOperationBinding?.SetExecutingSubstate(RoboticsExecutingSubstate.Running, context, RoboticsTransitionReason.System);
        RefreshFromProgramSnapshot(snapshot, context);
    }

    public void MarkExternalProgramCommand()
    {
        _nextProgramTransitionReason = RoboticsTransitionReason.External;
    }

    public void RefreshFromProgramSnapshot(RobotProgramExecutionSnapshot snapshot, Opc.Ua.ISystemContext context)
    {
        RoboticsTransitionReason transitionReason = _nextProgramTransitionReason;
        _nextProgramTransitionReason = RoboticsTransitionReason.System;

        bool naturalCompletion = _lastExecutionState == RobotProgramExecutionState.Running
            && snapshot.State == RobotProgramExecutionState.Completed;

        _taskControlBinding?.UpdateFromProgramSnapshot(
            snapshot,
            context,
            naturalCompletion ? RoboticsTransitionReason.System : transitionReason);

        if (naturalCompletion)
        {
            Console.WriteLine("Natural complete: TaskControl ReadySubstate AtProgramStart");
            if (_systemOperationBinding?.CurrentProgramState == RoboticsProgramState.Executing)
            {
                _systemOperationBinding.SetExplicitDemoState(
                    RoboticsProgramState.Ready,
                    context,
                    RoboticsTransitionReason.System);
            }
        }
        else
        {
            _systemOperationBinding?.UpdateFromProgramSnapshot(snapshot, context, transitionReason);
        }

        _lastExecutionState = snapshot.State;
    }

    public RobotControlCommandResult GetReady(Opc.Ua.ISystemContext context)
    {
        RoboticsProgramState currentState = _systemOperationBinding?.CurrentProgramState ?? RoboticsProgramState.Idle;
        if (currentState == RoboticsProgramState.Ready)
        {
            return RobotControlCommandResult.Accept("SystemOperation GetReady accepted; already Ready.");
        }

        if (currentState == RoboticsProgramState.Executing)
        {
            return RobotControlCommandResult.Reject(
                "SystemOperation GetReady is invalid while Executing.",
                RobotControlCommandFailureKind.InvalidState);
        }

        _systemOperationBinding?.SetIdleSubstate(RoboticsIdleSubstate.StandBy, context);
        _systemOperationBinding?.SetIdleSubstate(RoboticsIdleSubstate.GettingReady, context);
        Console.WriteLine("GetReady: IdleSubstate StandBy -> GettingReady -> System Ready");
        Thread.Sleep(ObservableTransitionDelay);
        _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Ready, context);
        return RobotControlCommandResult.Accept("SystemOperation GetReady accepted.");
    }

    public RobotControlCommandResult Start(
        Opc.Ua.ISystemContext context,
        Func<RobotControlCommandResult> startProgram)
    {
        RoboticsProgramState currentState = _systemOperationBinding?.CurrentProgramState ?? RoboticsProgramState.Idle;
        if (currentState == RoboticsProgramState.Idle)
        {
            return RobotControlCommandResult.Reject(
                "SystemOperation Start is invalid while Idle. Call GetReady first.",
                RobotControlCommandFailureKind.InvalidState);
        }

        if (currentState == RoboticsProgramState.Executing)
        {
            return RobotControlCommandResult.Accept("SystemOperation Start accepted; already Executing.");
        }

        _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Executing, context);
        _systemOperationBinding?.SetExecutingSubstate(RoboticsExecutingSubstate.Running, context);
        Console.WriteLine("Start: System Ready -> Executing, ExecutingSubstate Running");

        if (string.IsNullOrWhiteSpace(_programExecutor.CurrentProgramName))
        {
            return RobotControlCommandResult.Accept("SystemOperation Start accepted without a loaded task program.");
        }

        MarkExternalProgramCommand();
        RobotControlCommandResult result = startProgram();
        if (!result.Accepted)
        {
            _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Ready, context);
        }

        return result;
    }

    public RobotControlCommandResult Stop(
        Opc.Ua.ISystemContext context,
        Func<RobotControlCommandResult> stopProgram)
    {
        RoboticsProgramState currentState = _systemOperationBinding?.CurrentProgramState ?? RoboticsProgramState.Idle;
        if (currentState == RoboticsProgramState.Idle)
        {
            return RobotControlCommandResult.Reject(
                "SystemOperation Stop is invalid while Idle.",
                RobotControlCommandFailureKind.InvalidState);
        }

        if (currentState == RoboticsProgramState.Ready)
        {
            return RobotControlCommandResult.Accept("SystemOperation Stop accepted; already Ready.");
        }

        _systemOperationBinding?.SetExecutingSubstate(RoboticsExecutingSubstate.Running, context);
        _systemOperationBinding?.SetExecutingSubstate(RoboticsExecutingSubstate.Stopping, context);
        Console.WriteLine("Stop: ExecutingSubstate Running -> Stopping -> System Ready");

        RobotControlCommandResult result = RobotControlCommandResult.Accept("No task program was running.");
        if (_programExecutor.State is RobotProgramExecutionState.Running or RobotProgramExecutionState.Paused)
        {
            MarkExternalProgramCommand();
            result = stopProgram();
        }

        Thread.Sleep(ObservableTransitionDelay);
        _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Ready, context);
        _systemOperationBinding?.SetExecutingSubstate(RoboticsExecutingSubstate.Running, context);
        return result.Accepted
            ? RobotControlCommandResult.Accept("SystemOperation Stop accepted.")
            : result;
    }

    public RobotControlCommandResult StandDown(Opc.Ua.ISystemContext context)
    {
        RoboticsProgramState currentState = _systemOperationBinding?.CurrentProgramState ?? RoboticsProgramState.Idle;
        if (currentState == RoboticsProgramState.Executing)
        {
            return RobotControlCommandResult.Reject(
                "SystemOperation StandDown is invalid while Executing. Stop first.",
                RobotControlCommandFailureKind.InvalidState);
        }

        _systemOperationBinding?.SetExplicitDemoState(RoboticsProgramState.Idle, context);
        _systemOperationBinding?.SetIdleSubstate(RoboticsIdleSubstate.StandBy, context);
        return RobotControlCommandResult.Accept("SystemOperation StandDown accepted.");
    }
}
