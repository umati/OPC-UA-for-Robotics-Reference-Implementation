using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.Simulation;
using RoboticsObjectIds = Opc.Ua.Robotics.Objects;

namespace Robotics.ReferenceServer.InformationModel;

internal sealed class RoboticsSystemOperationBinding
{
    private const string SystemOperationPath = "SixAxisRobot.MinimalRealistic/Controller/MainController/SystemOperation";
    private const string SystemOperationStateMachinePath = $"{SystemOperationPath}/SystemOperationStateMachine";
    private const string IdleSubstateMachinePath = $"{SystemOperationStateMachinePath}/IdleSubstateMachine";
    private const string ExecutingSubstateMachinePath = $"{SystemOperationStateMachinePath}/ExecutingSubstateMachine";

    private static readonly RoboticsTaskControlBinding.MethodBindingDescriptor[] RequiredMethods =
    [
        new("GetReady", [], RoboticsTaskControlBinding.StatusOutputArguments),
        new("Start", [], RoboticsTaskControlBinding.StatusOutputArguments),
        new("Stop", [RoboticsTaskControlBinding.CreateArgument("StopMode", DataTypeIds.Int64)], RoboticsTaskControlBinding.StatusOutputArguments),
        new("StandDown", [], RoboticsTaskControlBinding.StatusOutputArguments)
    ];

    private readonly IReadOnlyDictionary<RoboticsProgramState, StateMachineNode?> _states;
    private readonly IReadOnlyDictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?> _transitions;
    private RoboticsProgramState? _lastProgramState;
    private RoboticsProgramState? _explicitDemoState;
    private RoboticsIdleSubstate? _lastIdleSubstate;
    private RoboticsExecutingSubstate? _lastExecutingSubstate;
    private bool? _idleSubstateActive;
    private bool? _executingSubstateActive;

    private RoboticsSystemOperationBinding(
        BaseVariableState? currentState,
        BaseVariableState? currentStateId,
        BaseVariableState? lastTransition,
        BaseVariableState? lastTransitionId,
        BaseVariableState? lastTransitionReason,
        BaseVariableState? lastTransitionReasonValueAsText,
        SubstateMachineNodeSet<RoboticsIdleSubstate> idleSubstate,
        SubstateMachineNodeSet<RoboticsExecutingSubstate> executingSubstate,
        IReadOnlyDictionary<RoboticsProgramState, StateMachineNode?> states,
        IReadOnlyDictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?> transitions,
        IReadOnlyList<string> missingStateNodes,
        IReadOnlyList<string> missingMethodNodes,
        int boundMethodCount)
    {
        CurrentState = currentState;
        CurrentStateId = currentStateId;
        LastTransition = lastTransition;
        LastTransitionId = lastTransitionId;
        LastTransitionReason = lastTransitionReason;
        LastTransitionReasonValueAsText = lastTransitionReasonValueAsText;
        IdleSubstate = idleSubstate;
        ExecutingSubstate = executingSubstate;
        _states = states;
        _transitions = transitions;
        MissingStateNodes = missingStateNodes;
        MissingMethodNodes = missingMethodNodes;
        BoundMethodCount = boundMethodCount;
    }

    public BaseVariableState? CurrentState { get; }

    public BaseVariableState? CurrentStateId { get; }

    public BaseVariableState? LastTransition { get; }

    public BaseVariableState? LastTransitionId { get; }

    public BaseVariableState? LastTransitionReason { get; }

    public BaseVariableState? LastTransitionReasonValueAsText { get; }

    public SubstateMachineNodeSet<RoboticsIdleSubstate> IdleSubstate { get; }

    public SubstateMachineNodeSet<RoboticsExecutingSubstate> ExecutingSubstate { get; }

    public IReadOnlyList<string> MissingStateNodes { get; }

    public IReadOnlyList<string> MissingMethodNodes { get; }

    public int BoundMethodCount { get; }

    public int BoundStateVariableCount =>
        (CurrentState is null ? 0 : 1)
        + (CurrentStateId is null ? 0 : 1)
        + (LastTransition is null ? 0 : 1)
        + (LastTransitionId is null ? 0 : 1)
        + (LastTransitionReason is null ? 0 : 1)
        + (LastTransitionReasonValueAsText is null ? 0 : 1)
        + IdleSubstate.BoundStateVariableCount
        + ExecutingSubstate.BoundStateVariableCount;

    public int BoundMainStateVariableCount =>
        (CurrentState is null ? 0 : 1)
        + (CurrentStateId is null ? 0 : 1)
        + (LastTransition is null ? 0 : 1)
        + (LastTransitionId is null ? 0 : 1)
        + (LastTransitionReason is null ? 0 : 1)
        + (LastTransitionReasonValueAsText is null ? 0 : 1);

    public int BoundIdleSubstateVariableCount => IdleSubstate.BoundStateVariableCount;

    public int BoundExecutingSubstateVariableCount => ExecutingSubstate.BoundStateVariableCount;

    public bool IsMonitoringAvailable => CurrentState is not null && CurrentStateId is not null;

    public bool IsMethodBindingAvailable => BoundMethodCount == RequiredMethods.Length && MissingMethodNodes.Count == 0;

    public bool IsPartial => IsMonitoringAvailable && !IsMethodBindingAvailable;

    public static RoboticsSystemOperationBinding Bind(
        NodeStateCollection importedNodes,
        ISystemContext context,
        IReadOnlyDictionary<string, GenericMethodCalledEventHandler> methodHandlers)
    {
        ArgumentNullException.ThrowIfNull(importedNodes);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(methodHandlers);

        ushort instanceNamespaceIndex = RoboticsTaskControlBinding.GetInstanceNamespaceIndex(context);
        ushort roboticsNamespaceIndex = RoboticsTaskControlBinding.GetRoboticsNamespaceIndex(context);
        var missingStateNodes = new List<string>();
        var missingMethodNodes = new List<string>();

        int boundMethodCount = 0;
        foreach (RoboticsTaskControlBinding.MethodBindingDescriptor descriptor in RequiredMethods)
        {
            string methodPath = $"{SystemOperationStateMachinePath}/{descriptor.RelativePath}";
            if (RoboticsTaskControlBinding.BindOfficialMethod(
                importedNodes,
                instanceNamespaceIndex,
                methodPath,
                descriptor,
                methodHandlers.TryGetValue(descriptor.RelativePath, out GenericMethodCalledEventHandler? handler) ? handler : null,
                missingMethodNodes,
                "SystemOperation",
                descriptor.RelativePath))
            {
                boundMethodCount++;
            }
        }

        var states = new Dictionary<RoboticsProgramState, StateMachineNode?>
        {
            [RoboticsProgramState.Idle] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_Idle, "Idle"),
            [RoboticsProgramState.Ready] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_Ready, "Ready"),
            [RoboticsProgramState.Executing] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_Executing, "Executing")
        };

        var transitions = new Dictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?>
        {
            [(RoboticsProgramState.Idle, RoboticsProgramState.Idle)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_IdleToIdle, "IdleToIdle"),
            [(RoboticsProgramState.Idle, RoboticsProgramState.Ready)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_IdleToReady, "IdleToReady"),
            [(RoboticsProgramState.Ready, RoboticsProgramState.Idle)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_ReadyToIdle, "ReadyToIdle"),
            [(RoboticsProgramState.Ready, RoboticsProgramState.Executing)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_ReadyToExecuting, "ReadyToExecuting"),
            [(RoboticsProgramState.Executing, RoboticsProgramState.Ready)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_ExecutingToReady, "ExecutingToReady"),
            [(RoboticsProgramState.Executing, RoboticsProgramState.Idle)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.SystemOperationStateMachineType_ExecutingToIdle, "ExecutingToIdle")
        };

        var idleStates = new Dictionary<RoboticsIdleSubstate, StateMachineNode?>
        {
            [RoboticsIdleSubstate.StandBy] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.IdleSubstateMachineType_StandBy, "StandBy"),
            [RoboticsIdleSubstate.GettingReady] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.IdleSubstateMachineType_GettingReady, "GettingReady")
        };

        var idleTransitions = new Dictionary<(RoboticsIdleSubstate From, RoboticsIdleSubstate To), StateMachineNode?>
        {
            [(RoboticsIdleSubstate.StandBy, RoboticsIdleSubstate.GettingReady)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.IdleSubstateMachineType_StandByToGettingReady, "StandByToGettingReady"),
            [(RoboticsIdleSubstate.GettingReady, RoboticsIdleSubstate.StandBy)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.IdleSubstateMachineType_GettingReadyToStandBy, "GettingReadyToStandBy")
        };

        var executingStates = new Dictionary<RoboticsExecutingSubstate, StateMachineNode?>
        {
            [RoboticsExecutingSubstate.Running] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ExecutingSubstateMachineType_Running, "Running"),
            [RoboticsExecutingSubstate.Stopping] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ExecutingSubstateMachineType_Stopping, "Stopping")
        };

        var executingTransitions = new Dictionary<(RoboticsExecutingSubstate From, RoboticsExecutingSubstate To), StateMachineNode?>
        {
            [(RoboticsExecutingSubstate.Running, RoboticsExecutingSubstate.Stopping)] = RoboticsTaskControlBinding.CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ExecutingSubstateMachineType_RunningToStopping, "RunningToStopping")
        };

        return new RoboticsSystemOperationBinding(
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/CurrentState", missingStateNodes, "SystemOperation CurrentState variable"),
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/CurrentState/Id", missingStateNodes, "SystemOperation CurrentState Id variable"),
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/LastTransition", missingStateNodes, "SystemOperation LastTransition variable"),
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/LastTransition/Id", missingStateNodes, "SystemOperation LastTransition Id variable"),
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/LastTransitionReason", missingStateNodes, "SystemOperation LastTransitionReason variable"),
            RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{SystemOperationStateMachinePath}/LastTransitionReason/ValueAsText", missingStateNodes, "SystemOperation LastTransitionReason ValueAsText variable"),
            SubstateMachineNodeSet<RoboticsIdleSubstate>.Bind(
                importedNodes,
                instanceNamespaceIndex,
                IdleSubstateMachinePath,
                missingStateNodes,
                "IdleSubstateMachine",
                idleStates,
                idleTransitions),
            SubstateMachineNodeSet<RoboticsExecutingSubstate>.Bind(
                importedNodes,
                instanceNamespaceIndex,
                ExecutingSubstateMachinePath,
                missingStateNodes,
                "ExecutingSubstateMachine",
                executingStates,
                executingTransitions),
            states,
            transitions,
            missingStateNodes,
            missingMethodNodes,
            boundMethodCount);
    }

    public RoboticsProgramState? CurrentProgramState => _lastProgramState;

    public void SetExplicitDemoState(
        RoboticsProgramState state,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        ArgumentNullException.ThrowIfNull(context);

        _explicitDemoState = state;
        SetOperationState(state, DateTime.UtcNow, context, transitionReason);
    }

    public void ClearExplicitDemoState()
    {
        _explicitDemoState = null;
    }

    public void UpdateFromProgramSnapshot(
        RobotProgramExecutionSnapshot snapshot,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.System)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        ArgumentNullException.ThrowIfNull(context);

        DateTime timestamp = DateTime.UtcNow;
        RoboticsProgramState currentState = MapProgramState(snapshot, _explicitDemoState);
        SetOperationState(currentState, timestamp, context, transitionReason);
    }

    public void SetIdleSubstate(
        RoboticsIdleSubstate currentState,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        DateTime timestamp = DateTime.UtcNow;
        RoboticsTaskControlBinding.SetState(
            IdleSubstate.CurrentState,
            IdleSubstate.CurrentStateId,
            IdleSubstate.GetState(currentState),
            timestamp,
            context);

        if (_lastIdleSubstate is not null && _lastIdleSubstate != currentState)
        {
            StateMachineNode? transition = IdleSubstate.GetTransition(_lastIdleSubstate.Value, currentState);
            if (transition is not null)
            {
                RoboticsTaskControlBinding.SetState(IdleSubstate.LastTransition, IdleSubstate.LastTransitionId, transition, timestamp, context);
                RoboticsTaskControlBinding.SetTransitionReason(
                    IdleSubstate.LastTransitionReason,
                    IdleSubstate.LastTransitionReasonValueAsText,
                    transitionReason,
                    timestamp,
                    context);
            }
        }

        _lastIdleSubstate = currentState;
        SetSubstateReadStatus(IdleSubstate, IsIdleSubstateActive, timestamp, context);
    }

    public void SetExecutingSubstate(
        RoboticsExecutingSubstate currentState,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        DateTime timestamp = DateTime.UtcNow;
        RoboticsTaskControlBinding.SetState(
            ExecutingSubstate.CurrentState,
            ExecutingSubstate.CurrentStateId,
            ExecutingSubstate.GetState(currentState),
            timestamp,
            context);

        if (_lastExecutingSubstate is not null && _lastExecutingSubstate != currentState)
        {
            StateMachineNode? transition = ExecutingSubstate.GetTransition(_lastExecutingSubstate.Value, currentState);
            if (transition is not null)
            {
                RoboticsTaskControlBinding.SetState(ExecutingSubstate.LastTransition, ExecutingSubstate.LastTransitionId, transition, timestamp, context);
                RoboticsTaskControlBinding.SetTransitionReason(
                    ExecutingSubstate.LastTransitionReason,
                    ExecutingSubstate.LastTransitionReasonValueAsText,
                    transitionReason,
                    timestamp,
                    context);
            }
        }

        _lastExecutingSubstate = currentState;
        SetSubstateReadStatus(ExecutingSubstate, IsExecutingSubstateActive, timestamp, context);
    }

    private void SetOperationState(
        RoboticsProgramState currentState,
        DateTime timestamp,
        ISystemContext context,
        RoboticsTransitionReason transitionReason)
    {
        SetState(CurrentState, CurrentStateId, GetState(currentState), timestamp, context);

        if (_lastProgramState is not null && _lastProgramState != currentState)
        {
            StateMachineNode? transition = GetTransition(_lastProgramState.Value, currentState);
            if (transition is not null)
            {
                SetState(LastTransition, LastTransitionId, transition, timestamp, context);
                RoboticsTaskControlBinding.SetTransitionReason(
                    LastTransitionReason,
                    LastTransitionReasonValueAsText,
                    transitionReason,
                    timestamp,
                    context);
            }
        }

        _lastProgramState = currentState;
        SetIdleSubstateActive(currentState == RoboticsProgramState.Idle, currentState, timestamp, context);
        SetExecutingSubstateActive(currentState == RoboticsProgramState.Executing, currentState, timestamp, context);
    }

    private bool IsIdleSubstateActive => _lastProgramState == RoboticsProgramState.Idle;

    private bool IsExecutingSubstateActive => _lastProgramState == RoboticsProgramState.Executing;

    private void SetIdleSubstateActive(
        bool active,
        RoboticsProgramState parentState,
        DateTime timestamp,
        ISystemContext context)
    {
        if (_idleSubstateActive == active)
        {
            return;
        }

        bool hadPreviousState = _idleSubstateActive.HasValue;
        _idleSubstateActive = active;
        SetSubstateReadStatus(IdleSubstate, active, timestamp, context);

        if (!hadPreviousState)
        {
            return;
        }

        Console.WriteLine(active
            ? "IdleSubstateMachine active: SystemOperation re-entered Idle."
            : $"IdleSubstateMachine inactive: SystemOperation left Idle and entered {parentState}.");
    }

    private void SetExecutingSubstateActive(
        bool active,
        RoboticsProgramState parentState,
        DateTime timestamp,
        ISystemContext context)
    {
        if (_executingSubstateActive == active)
        {
            return;
        }

        bool hadPreviousState = _executingSubstateActive.HasValue;
        _executingSubstateActive = active;
        SetSubstateReadStatus(ExecutingSubstate, active, timestamp, context);

        if (!hadPreviousState)
        {
            return;
        }

        Console.WriteLine(active
            ? "ExecutingSubstateMachine active: SystemOperation re-entered Executing."
            : $"ExecutingSubstateMachine inactive: SystemOperation left Executing and entered {parentState}.");
    }

    private StateMachineNode? GetState(RoboticsProgramState state)
    {
        return _states.TryGetValue(state, out StateMachineNode? node) ? node : null;
    }

    private StateMachineNode? GetTransition(RoboticsProgramState from, RoboticsProgramState to)
    {
        return _transitions.TryGetValue((from, to), out StateMachineNode? node) ? node : null;
    }

    private static RoboticsProgramState MapProgramState(
        RobotProgramExecutionSnapshot snapshot,
        RoboticsProgramState? explicitDemoState)
    {
        if (snapshot.State == RobotProgramExecutionState.Running)
        {
            return RoboticsProgramState.Executing;
        }

        if (explicitDemoState is not null)
        {
            return explicitDemoState.Value;
        }

        if (string.IsNullOrWhiteSpace(snapshot.CurrentProgramName))
        {
            return RoboticsProgramState.Idle;
        }

        return RoboticsProgramState.Ready;
    }

    private static void SetState(
        BaseVariableState? currentState,
        BaseVariableState? currentStateId,
        StateMachineNode? state,
        DateTime timestamp,
        ISystemContext context)
    {
        if (state is null)
        {
            return;
        }

        RoboticsTaskControlBinding.SetValue(currentState, new LocalizedText(state.DisplayName), timestamp, context);
        RoboticsTaskControlBinding.SetValue(currentStateId, state.NodeId, timestamp, context);
    }

    internal sealed class SubstateMachineNodeSet<TState>
        where TState : struct, Enum
    {
        private readonly IReadOnlyDictionary<TState, StateMachineNode?> _states;
        private readonly IReadOnlyDictionary<(TState From, TState To), StateMachineNode?> _transitions;

        private SubstateMachineNodeSet(
            BaseVariableState? currentState,
            BaseVariableState? currentStateId,
            BaseVariableState? lastTransition,
            BaseVariableState? lastTransitionId,
            BaseVariableState? lastTransitionReason,
            BaseVariableState? lastTransitionReasonValueAsText,
            IReadOnlyDictionary<TState, StateMachineNode?> states,
            IReadOnlyDictionary<(TState From, TState To), StateMachineNode?> transitions)
        {
            CurrentState = currentState;
            CurrentStateId = currentStateId;
            LastTransition = lastTransition;
            LastTransitionId = lastTransitionId;
            LastTransitionReason = lastTransitionReason;
            LastTransitionReasonValueAsText = lastTransitionReasonValueAsText;
            _states = states;
            _transitions = transitions;
        }

        public BaseVariableState? CurrentState { get; }

        public BaseVariableState? CurrentStateId { get; }

        public BaseVariableState? LastTransition { get; }

        public BaseVariableState? LastTransitionId { get; }

        public BaseVariableState? LastTransitionReason { get; }

        public BaseVariableState? LastTransitionReasonValueAsText { get; }

        public int BoundStateVariableCount =>
            (CurrentState is null ? 0 : 1)
            + (CurrentStateId is null ? 0 : 1)
            + (LastTransition is null ? 0 : 1)
            + (LastTransitionId is null ? 0 : 1)
            + (LastTransitionReason is null ? 0 : 1)
            + (LastTransitionReasonValueAsText is null ? 0 : 1);

        public StateMachineNode? GetState(TState state)
        {
            return _states.TryGetValue(state, out StateMachineNode? node) ? node : null;
        }

        public StateMachineNode? GetTransition(TState from, TState to)
        {
            return _transitions.TryGetValue((from, to), out StateMachineNode? node) ? node : null;
        }

        public static SubstateMachineNodeSet<TState> Bind(
            NodeStateCollection importedNodes,
            ushort instanceNamespaceIndex,
            string substateMachinePath,
            List<string> missingNodes,
            string diagnosticName,
            IReadOnlyDictionary<TState, StateMachineNode?> states,
            IReadOnlyDictionary<(TState From, TState To), StateMachineNode?> transitions)
        {
            return new SubstateMachineNodeSet<TState>(
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/CurrentState", missingNodes, $"{diagnosticName} CurrentState variable"),
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/CurrentState/Id", missingNodes, $"{diagnosticName} CurrentState Id variable"),
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/LastTransition", missingNodes, $"{diagnosticName} LastTransition variable"),
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/LastTransition/Id", missingNodes, $"{diagnosticName} LastTransition Id variable"),
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/LastTransitionReason", missingNodes, $"{diagnosticName} LastTransitionReason variable"),
                RoboticsTaskControlBinding.FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{substateMachinePath}/LastTransitionReason/ValueAsText", missingNodes, $"{diagnosticName} LastTransitionReason ValueAsText variable"),
                states,
                transitions);
        }
    }

    private static void SetSubstateReadStatus<TState>(
        SubstateMachineNodeSet<TState> substate,
        bool active,
        DateTime timestamp,
        ISystemContext context)
        where TState : struct, Enum
    {
        uint statusCode = active ? StatusCodes.Good : StatusCodes.BadStateNotActive;
        RoboticsTaskControlBinding.SetReadStatus(substate.CurrentState, statusCode, timestamp, context);
        RoboticsTaskControlBinding.SetReadStatus(substate.CurrentStateId, statusCode, timestamp, context);
        RoboticsTaskControlBinding.SetReadStatus(substate.LastTransition, statusCode, timestamp, context);
        RoboticsTaskControlBinding.SetReadStatus(substate.LastTransitionId, statusCode, timestamp, context);
    }
}
