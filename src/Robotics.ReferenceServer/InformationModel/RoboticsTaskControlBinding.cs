using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.Simulation;
using RoboticsObjectIds = Opc.Ua.Robotics.Objects;

namespace Robotics.ReferenceServer.InformationModel;

internal sealed class RoboticsTaskControlBinding
{
    private const string TaskControlBasePath = "SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl";
    private const string TaskProgramLoadedPath = $"{TaskControlBasePath}/TaskProgramLoaded";
    private const string TaskProgramNamePath = $"{TaskControlBasePath}/TaskProgramName";
    private const string TaskControlOperationPath = $"{TaskControlBasePath}/TaskControlOperation";
    private const string TaskControlStateMachinePath = $"{TaskControlOperationPath}/TaskControlStateMachine";
    private const string ReadySubstateMachinePath = $"{TaskControlStateMachinePath}/ReadySubstateMachine";

    internal static readonly Argument[] StatusOutputArguments = [CreateArgument("Status", DataTypeIds.Int32)];

    private static readonly MethodBindingDescriptor[] RequiredMethods =
    [
        new("LoadByName", [CreateArgument("Name", DataTypeIds.String)], StatusOutputArguments),
        new("LoadByNodeId", [CreateArgument("Id", DataTypeIds.ExpandedNodeId)], StatusOutputArguments),
        new("Start", [], StatusOutputArguments),
        new("Stop", [CreateArgument("StopMode", DataTypeIds.Int64)], StatusOutputArguments),
        new("UnloadProgram", [], StatusOutputArguments),
        new("UnloadByName", [CreateArgument("Name", DataTypeIds.String)], StatusOutputArguments),
        new("UnloadByNodeId", [CreateArgument("Id", DataTypeIds.ExpandedNodeId)], StatusOutputArguments),
        new("ReadySubstateMachine/ResetToProgramStart", [], StatusOutputArguments)
    ];

    private static readonly string[] RequiredStateVariablePaths =
    [
        $"{TaskControlStateMachinePath}/CurrentState",
        $"{TaskControlStateMachinePath}/CurrentState/Id",
        $"{TaskControlStateMachinePath}/LastTransition",
        $"{TaskControlStateMachinePath}/LastTransition/Id",
        $"{TaskControlStateMachinePath}/LastTransitionReason",
        $"{TaskControlStateMachinePath}/LastTransitionReason/ValueAsText",
        $"{ReadySubstateMachinePath}/CurrentState",
        $"{ReadySubstateMachinePath}/CurrentState/Id",
        $"{ReadySubstateMachinePath}/LastTransition",
        $"{ReadySubstateMachinePath}/LastTransition/Id",
        $"{ReadySubstateMachinePath}/LastTransitionReason",
        $"{ReadySubstateMachinePath}/LastTransitionReason/ValueAsText"
    ];

    private RoboticsProgramState? _lastProgramState;
    private RoboticsReadySubstate? _lastReadySubstate;
    private bool? _readySubstateActive;

    private RoboticsTaskControlBinding(
        BaseVariableState? taskProgramLoaded,
        BaseVariableState? taskProgramName,
        OperationStateMachineNodeSet stateMachine,
        IReadOnlyList<string> missingParameterNodes,
        IReadOnlyList<string> missingMethodNodes,
        IReadOnlyList<string> missingStateVariableNodes,
        bool taskControlOperationAddInFound,
        bool taskControlStateMachineFound,
        int boundMethodCount)
    {
        TaskProgramLoaded = taskProgramLoaded;
        TaskProgramName = taskProgramName;
        StateMachine = stateMachine;
        MissingParameterNodes = missingParameterNodes;
        MissingMethodNodes = missingMethodNodes;
        MissingStateVariableNodes = missingStateVariableNodes;
        TaskControlOperationAddInFound = taskControlOperationAddInFound;
        TaskControlStateMachineFound = taskControlStateMachineFound;
        BoundMethodCount = boundMethodCount;
    }

    public BaseVariableState? TaskProgramLoaded { get; }

    public BaseVariableState? TaskProgramName { get; }

    public OperationStateMachineNodeSet StateMachine { get; }

    public IReadOnlyList<string> MissingParameterNodes { get; }

    public IReadOnlyList<string> MissingMethodNodes { get; }

    public IReadOnlyList<string> MissingStateVariableNodes { get; }

    public bool TaskControlOperationAddInFound { get; }

    public bool TaskControlStateMachineFound { get; }

    public int BoundMethodCount { get; }

    public int BoundParameterVariableCount =>
        (TaskProgramLoaded is null ? 0 : 1)
        + (TaskProgramName is null ? 0 : 1);

    public int BoundStateVariableCount => StateMachine.RequiredBoundStateVariableCount;

    public int BoundReadySubstateVariableCount => StateMachine.ReadySubstate.BoundStateVariableCount;

    public bool IsParameterBindingAvailable => BoundParameterVariableCount == 2 && MissingParameterNodes.Count == 0;

    public bool IsMethodBindingAvailable =>
        TaskControlOperationAddInFound
        && TaskControlStateMachineFound
        && BoundMethodCount == RequiredMethods.Length
        && MissingMethodNodes.Count == 0;

    public bool IsStateVariableBindingAvailable => BoundStateVariableCount == RequiredStateVariablePaths.Length;

    public static RoboticsTaskControlBinding Bind(
        NodeStateCollection importedNodes,
        ISystemContext context,
        IReadOnlyDictionary<string, GenericMethodCalledEventHandler> methodHandlers)
    {
        ArgumentNullException.ThrowIfNull(importedNodes);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(methodHandlers);

        ushort instanceNamespaceIndex = GetInstanceNamespaceIndex(context);
        ushort roboticsNamespaceIndex = GetRoboticsNamespaceIndex(context);
        var missingParameterNodes = new List<string>();
        var missingMethodNodes = new List<string>();
        var missingStateVariableNodes = new List<string>();

        BaseVariableState? taskProgramLoaded = FindVariable(
            importedNodes,
            instanceNamespaceIndex,
            TaskProgramLoadedPath,
            missingParameterNodes,
            "TaskProgramLoaded variable");

        BaseVariableState? taskProgramName = FindVariable(
            importedNodes,
            instanceNamespaceIndex,
            TaskProgramNamePath,
            missingParameterNodes,
            "TaskProgramName variable");

        bool taskControlOperationAddInFound = FindNode(importedNodes, instanceNamespaceIndex, TaskControlOperationPath) is not null;
        if (!taskControlOperationAddInFound)
        {
            missingMethodNodes.Add(
                $"TaskControlOperation AddIn missing: NodeId '{CreateNodeId(TaskControlOperationPath, instanceNamespaceIndex)}' was not found.");
        }

        bool taskControlStateMachineFound = FindNode(importedNodes, instanceNamespaceIndex, TaskControlStateMachinePath) is not null;
        if (!taskControlStateMachineFound)
        {
            missingMethodNodes.Add(
                $"TaskControlStateMachine missing: NodeId '{CreateNodeId(TaskControlStateMachinePath, instanceNamespaceIndex)}' was not found.");
        }

        int boundMethodCount = 0;
        foreach (MethodBindingDescriptor descriptor in RequiredMethods)
        {
            string methodPath = $"{TaskControlStateMachinePath}/{descriptor.RelativePath}";
            if (BindOfficialMethod(
                importedNodes,
                instanceNamespaceIndex,
                methodPath,
                descriptor,
                methodHandlers.TryGetValue(descriptor.RelativePath, out GenericMethodCalledEventHandler? handler) ? handler : null,
                missingMethodNodes,
                "TaskControl",
                descriptor.RelativePath))
            {
                boundMethodCount++;
            }
        }

        OperationStateMachineNodeSet stateMachine = OperationStateMachineNodeSet.BindTaskControl(
            importedNodes,
            instanceNamespaceIndex,
            roboticsNamespaceIndex,
            missingStateVariableNodes);

        return new RoboticsTaskControlBinding(
            taskProgramLoaded,
            taskProgramName,
            stateMachine,
            missingParameterNodes,
            missingMethodNodes,
            missingStateVariableNodes,
            taskControlOperationAddInFound,
            taskControlStateMachineFound,
            boundMethodCount);
    }

    public void UpdateFromProgramSnapshot(
        RobotProgramExecutionSnapshot snapshot,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        ArgumentNullException.ThrowIfNull(context);

        DateTime timestamp = DateTime.UtcNow;
        string programName = snapshot.CurrentProgramName ?? string.Empty;
        SetValue(TaskProgramLoaded, !string.IsNullOrWhiteSpace(programName), timestamp, context);
        SetValue(TaskProgramName, programName, timestamp, context);

        RoboticsProgramState currentState = MapProgramState(snapshot);
        SetProgramState(currentState, timestamp, context, transitionReason);

        RoboticsReadySubstate? currentReadySubstate = currentState == RoboticsProgramState.Ready
            ? MapReadySubstate(snapshot)
            : null;

        if (currentReadySubstate is not null)
        {
            SetReadySubstate(currentReadySubstate.Value, timestamp, context, transitionReason);
        }
    }

    public void SetProgramState(
        RoboticsProgramState currentState,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        SetProgramState(currentState, DateTime.UtcNow, context, transitionReason);
    }

    public void SetReadySubstate(
        RoboticsReadySubstate currentReadySubstate,
        ISystemContext context,
        RoboticsTransitionReason transitionReason = RoboticsTransitionReason.External)
    {
        SetReadySubstate(currentReadySubstate, DateTime.UtcNow, context, transitionReason);
    }

    private void SetProgramState(
        RoboticsProgramState currentState,
        DateTime timestamp,
        ISystemContext context,
        RoboticsTransitionReason transitionReason)
    {
        SetState(StateMachine.CurrentState, StateMachine.CurrentStateId, StateMachine.GetState(currentState), timestamp, context);

        if (_lastProgramState is not null && _lastProgramState != currentState)
        {
            StateMachineNode? transition = StateMachine.GetTransition(_lastProgramState.Value, currentState);
            if (transition is not null)
            {
                SetState(StateMachine.LastTransition, StateMachine.LastTransitionId, transition, timestamp, context);
                SetTransitionReason(StateMachine.LastTransitionReason, StateMachine.LastTransitionReasonValueAsText, transitionReason, timestamp, context);
            }
        }

        _lastProgramState = currentState;
        SetReadySubstateActive(
            currentState == RoboticsProgramState.Ready,
            currentState,
            timestamp,
            context);

        if (currentState == RoboticsProgramState.Idle)
        {
            _lastReadySubstate = null;
        }
    }

    private void SetReadySubstate(
        RoboticsReadySubstate currentReadySubstate,
        DateTime timestamp,
        ISystemContext context,
        RoboticsTransitionReason transitionReason)
    {
        SetState(
            StateMachine.ReadySubstate.CurrentState,
            StateMachine.ReadySubstate.CurrentStateId,
            StateMachine.ReadySubstate.GetState(currentReadySubstate),
            timestamp,
            context);

        if (_lastReadySubstate is not null && _lastReadySubstate != currentReadySubstate)
        {
            StateMachineNode? transition = StateMachine.ReadySubstate.GetTransition(_lastReadySubstate.Value, currentReadySubstate);
            if (transition is not null)
            {
                SetState(
                    StateMachine.ReadySubstate.LastTransition,
                    StateMachine.ReadySubstate.LastTransitionId,
                    transition,
                    timestamp,
                    context);
                SetTransitionReason(
                    StateMachine.ReadySubstate.LastTransitionReason,
                    StateMachine.ReadySubstate.LastTransitionReasonValueAsText,
                    transitionReason,
                    timestamp,
                    context);
            }
        }

        _lastReadySubstate = currentReadySubstate;
        SetSubstateReadStatus(StateMachine.ReadySubstate, IsReadySubstateActive, timestamp, context);
    }

    private bool IsReadySubstateActive => _lastProgramState == RoboticsProgramState.Ready;

    private void SetReadySubstateActive(
        bool active,
        RoboticsProgramState parentState,
        DateTime timestamp,
        ISystemContext context)
    {
        if (_readySubstateActive == active)
        {
            return;
        }

        bool hadPreviousState = _readySubstateActive.HasValue;
        _readySubstateActive = active;
        SetSubstateReadStatus(StateMachine.ReadySubstate, active, timestamp, context);

        if (!hadPreviousState)
        {
            return;
        }

        Console.WriteLine(active
            ? "ReadySubstateMachine active: TaskControl re-entered Ready."
            : $"ReadySubstateMachine inactive: TaskControl left Ready and entered {parentState}.");
    }

    private static RoboticsProgramState MapProgramState(RobotProgramExecutionSnapshot snapshot)
    {
        if (string.IsNullOrWhiteSpace(snapshot.CurrentProgramName))
        {
            return RoboticsProgramState.Idle;
        }

        return snapshot.State == RobotProgramExecutionState.Running
            ? RoboticsProgramState.Executing
            : RoboticsProgramState.Ready;
    }

    private static RoboticsReadySubstate MapReadySubstate(RobotProgramExecutionSnapshot snapshot)
    {
        return snapshot.State is RobotProgramExecutionState.Paused or RobotProgramExecutionState.Stopped
            ? RoboticsReadySubstate.Suspended
            : RoboticsReadySubstate.AtProgramStart;
    }

    private static MethodState? FindMethod(NodeStateCollection importedNodes, ushort instanceNamespaceIndex, string identifier)
    {
        return FindNode(importedNodes, instanceNamespaceIndex, identifier) as MethodState;
    }

    internal static NodeState? FindNode(NodeStateCollection importedNodes, ushort instanceNamespaceIndex, string identifier)
    {
        NodeId nodeId = CreateNodeId(identifier, instanceNamespaceIndex);
        return importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));
    }

    private static BaseVariableState? FindVariable(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string identifier,
        List<string> missingNodes,
        string label)
    {
        NodeId nodeId = CreateNodeId(identifier, instanceNamespaceIndex);
        NodeState? node = importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));

        if (node is BaseVariableState variable)
        {
            return variable;
        }

        string message = node is null
            ? $"{label}: NodeId '{nodeId}' was not found."
            : $"{label}: NodeId '{nodeId}' is not a variable node.";

        missingNodes.Add(message);
        return null;
    }

    internal static BaseVariableState? FindVariableOrMissing(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string identifier,
        List<string> missingNodes,
        string label)
    {
        return FindVariable(importedNodes, instanceNamespaceIndex, identifier, missingNodes, label);
    }

    internal static StateMachineNode? FindStateMachineNode(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string identifier,
        List<string> missingNodes,
        string label)
    {
        NodeId nodeId = CreateNodeId(identifier, instanceNamespaceIndex);
        NodeState? node = importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));
        if (node is not null)
        {
            return new StateMachineNode(node.NodeId, GetDisplayName(node, label));
        }

        missingNodes.Add($"{label}: NodeId '{nodeId}' was not found.");
        return null;
    }

    internal static NodeId CreateNodeId(string identifier, ushort instanceNamespaceIndex)
    {
        return new NodeId(identifier, instanceNamespaceIndex);
    }

    internal static ushort GetInstanceNamespaceIndex(ISystemContext context)
    {
        int namespaceIndex = context.NamespaceUris.GetIndex(NodeSetLoader.InstanceNamespaceUri);
        if (namespaceIndex < 0 || namespaceIndex > ushort.MaxValue)
        {
            throw new InvalidOperationException(
                $"The imported instance namespace '{NodeSetLoader.InstanceNamespaceUri}' is not registered in the server namespace table.");
        }

        return (ushort)namespaceIndex;
    }

    internal static ushort GetRoboticsNamespaceIndex(ISystemContext context)
    {
        int namespaceIndex = context.NamespaceUris.GetIndex(NodeSetLoader.RoboticsNamespaceUri);
        if (namespaceIndex < 0 || namespaceIndex > ushort.MaxValue)
        {
            throw new InvalidOperationException(
                $"The Robotics namespace '{NodeSetLoader.RoboticsNamespaceUri}' is not registered in the server namespace table.");
        }

        return (ushort)namespaceIndex;
    }

    internal static void SetValue(BaseVariableState? variable, object value, DateTime timestamp, ISystemContext context)
    {
        if (variable is null)
        {
            return;
        }

        variable.Value = value;
        variable.StatusCode = StatusCodes.Good;
        variable.Timestamp = timestamp;
        variable.ClearChangeMasks(context, false);
    }

    internal static void SetReadStatus(
        BaseVariableState? variable,
        uint statusCode,
        DateTime timestamp,
        ISystemContext context)
    {
        if (variable is null)
        {
            return;
        }

        variable.StatusCode = statusCode;
        variable.Timestamp = timestamp;
        variable.ClearChangeMasks(context, false);
    }

    internal static string GetDisplayName(NodeState node, string fallback)
    {
        return string.IsNullOrWhiteSpace(node.DisplayName?.Text)
            ? fallback
            : node.DisplayName.Text;
    }

    internal static void SetState(
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

        SetValue(currentState, new LocalizedText(state.DisplayName), timestamp, context);
        SetValue(currentStateId, state.NodeId, timestamp, context);
    }

    internal static void SetTransitionReason(
        BaseVariableState? reason,
        BaseVariableState? valueAsText,
        RoboticsTransitionReason transitionReason,
        DateTime timestamp,
        ISystemContext context)
    {
        SetValue(reason, (short)transitionReason, timestamp, context);
        SetValue(valueAsText, new LocalizedText(GetTransitionReasonText(transitionReason)), timestamp, context);
    }

    internal static string GetTransitionReasonText(RoboticsTransitionReason transitionReason)
    {
        return transitionReason switch
        {
            RoboticsTransitionReason.External => "External",
            RoboticsTransitionReason.System => "System",
            RoboticsTransitionReason.Application => "Application",
            _ => transitionReason.ToString()
        };
    }

    internal sealed class OperationStateMachineNodeSet
    {
        private readonly IReadOnlyDictionary<RoboticsProgramState, StateMachineNode?> _states;
        private readonly IReadOnlyDictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?> _transitions;

        private OperationStateMachineNodeSet(
            BaseVariableState? currentState,
            BaseVariableState? currentStateId,
            BaseVariableState? lastTransition,
            BaseVariableState? lastTransitionId,
            BaseVariableState? lastTransitionReason,
            BaseVariableState? lastTransitionReasonValueAsText,
            ReadySubstateMachineNodeSet readySubstate,
            IReadOnlyDictionary<RoboticsProgramState, StateMachineNode?> states,
            IReadOnlyDictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?> transitions)
        {
            CurrentState = currentState;
            CurrentStateId = currentStateId;
            LastTransition = lastTransition;
            LastTransitionId = lastTransitionId;
            LastTransitionReason = lastTransitionReason;
            LastTransitionReasonValueAsText = lastTransitionReasonValueAsText;
            ReadySubstate = readySubstate;
            _states = states;
            _transitions = transitions;
        }

        public BaseVariableState? CurrentState { get; }

        public BaseVariableState? CurrentStateId { get; }

        public BaseVariableState? LastTransition { get; }

        public BaseVariableState? LastTransitionId { get; }

        public BaseVariableState? LastTransitionReason { get; }

        public BaseVariableState? LastTransitionReasonValueAsText { get; }

        public ReadySubstateMachineNodeSet ReadySubstate { get; }

        public int RequiredBoundStateVariableCount =>
            (CurrentState is null ? 0 : 1)
            + (CurrentStateId is null ? 0 : 1)
            + (LastTransition is null ? 0 : 1)
            + (LastTransitionId is null ? 0 : 1)
            + (LastTransitionReason is null ? 0 : 1)
            + (LastTransitionReasonValueAsText is null ? 0 : 1)
            + ReadySubstate.BoundStateVariableCount;

        public StateMachineNode? GetState(RoboticsProgramState state)
        {
            return _states.TryGetValue(state, out StateMachineNode? node) ? node : null;
        }

        public StateMachineNode? GetTransition(RoboticsProgramState from, RoboticsProgramState to)
        {
            return _transitions.TryGetValue((from, to), out StateMachineNode? node) ? node : null;
        }

        public static OperationStateMachineNodeSet BindTaskControl(
            NodeStateCollection importedNodes,
            ushort instanceNamespaceIndex,
            ushort roboticsNamespaceIndex,
            List<string> missingNodes)
        {
            var states = new Dictionary<RoboticsProgramState, StateMachineNode?>
            {
                [RoboticsProgramState.Idle] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_Idle, "Idle"),
                [RoboticsProgramState.Ready] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_Ready, "Ready"),
                [RoboticsProgramState.Executing] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_Executing, "Executing")
            };

            var transitions = new Dictionary<(RoboticsProgramState From, RoboticsProgramState To), StateMachineNode?>
            {
                [(RoboticsProgramState.Idle, RoboticsProgramState.Idle)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_IdleToIdle, "IdleToIdle"),
                [(RoboticsProgramState.Idle, RoboticsProgramState.Ready)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_IdleToReady, "IdleToReady"),
                [(RoboticsProgramState.Ready, RoboticsProgramState.Idle)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_ReadyToIdle, "ReadyToIdle"),
                [(RoboticsProgramState.Ready, RoboticsProgramState.Executing)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_ReadyToExecuting, "ReadyToExecuting"),
                [(RoboticsProgramState.Executing, RoboticsProgramState.Ready)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_ExecutingToReady, "ExecutingToReady"),
                [(RoboticsProgramState.Executing, RoboticsProgramState.Idle)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.TaskControlStateMachineType_ExecutingToIdle, "ExecutingToIdle")
            };

            return new OperationStateMachineNodeSet(
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/CurrentState", missingNodes, "TaskControl CurrentState variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/CurrentState/Id", missingNodes, "TaskControl CurrentState Id variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/LastTransition", missingNodes, "TaskControl LastTransition variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/LastTransition/Id", missingNodes, "TaskControl LastTransition Id variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/LastTransitionReason", missingNodes, "TaskControl LastTransitionReason variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{TaskControlStateMachinePath}/LastTransitionReason/ValueAsText", missingNodes, "TaskControl LastTransitionReason ValueAsText variable"),
                ReadySubstateMachineNodeSet.Bind(importedNodes, instanceNamespaceIndex, roboticsNamespaceIndex, missingNodes),
                states,
                transitions);
        }
    }

    internal sealed class ReadySubstateMachineNodeSet
    {
        private readonly IReadOnlyDictionary<RoboticsReadySubstate, StateMachineNode?> _states;
        private readonly IReadOnlyDictionary<(RoboticsReadySubstate From, RoboticsReadySubstate To), StateMachineNode?> _transitions;

        private ReadySubstateMachineNodeSet(
            BaseVariableState? currentState,
            BaseVariableState? currentStateId,
            BaseVariableState? lastTransition,
            BaseVariableState? lastTransitionId,
            BaseVariableState? lastTransitionReason,
            BaseVariableState? lastTransitionReasonValueAsText,
            IReadOnlyDictionary<RoboticsReadySubstate, StateMachineNode?> states,
            IReadOnlyDictionary<(RoboticsReadySubstate From, RoboticsReadySubstate To), StateMachineNode?> transitions)
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

        public StateMachineNode? GetState(RoboticsReadySubstate state)
        {
            return _states.TryGetValue(state, out StateMachineNode? node) ? node : null;
        }

        public StateMachineNode? GetTransition(RoboticsReadySubstate from, RoboticsReadySubstate to)
        {
            return _transitions.TryGetValue((from, to), out StateMachineNode? node) ? node : null;
        }

        public static ReadySubstateMachineNodeSet Bind(
            NodeStateCollection importedNodes,
            ushort instanceNamespaceIndex,
            ushort roboticsNamespaceIndex,
            List<string> missingNodes)
        {
            var states = new Dictionary<RoboticsReadySubstate, StateMachineNode?>
            {
                [RoboticsReadySubstate.AtProgramStart] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ReadySubstateMachineType_AtProgramStart, "AtProgramStart"),
                [RoboticsReadySubstate.Suspended] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ReadySubstateMachineType_Suspended, "Suspended")
            };

            var transitions = new Dictionary<(RoboticsReadySubstate From, RoboticsReadySubstate To), StateMachineNode?>
            {
                [(RoboticsReadySubstate.AtProgramStart, RoboticsReadySubstate.Suspended)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ReadySubstateMachineType_ProgramStartToSuspended, "ProgramStartToSuspended"),
                [(RoboticsReadySubstate.Suspended, RoboticsReadySubstate.AtProgramStart)] = CreateOfficialStateMachineNode(importedNodes, roboticsNamespaceIndex, RoboticsObjectIds.ReadySubstateMachineType_SuspendedToProgramStart, "SuspendedToProgramStart")
            };

            return new ReadySubstateMachineNodeSet(
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/CurrentState", missingNodes, "ReadySubstateMachine CurrentState variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/CurrentState/Id", missingNodes, "ReadySubstateMachine CurrentState Id variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/LastTransition", missingNodes, "ReadySubstateMachine LastTransition variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/LastTransition/Id", missingNodes, "ReadySubstateMachine LastTransition Id variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/LastTransitionReason", missingNodes, "ReadySubstateMachine LastTransitionReason variable"),
                FindVariableOrMissing(importedNodes, instanceNamespaceIndex, $"{ReadySubstateMachinePath}/LastTransitionReason/ValueAsText", missingNodes, "ReadySubstateMachine LastTransitionReason ValueAsText variable"),
                states,
                transitions);
        }
    }

    private static void SetSubstateReadStatus(
        ReadySubstateMachineNodeSet substate,
        bool active,
        DateTime timestamp,
        ISystemContext context)
    {
        uint statusCode = active ? StatusCodes.Good : StatusCodes.BadStateNotActive;
        SetReadStatus(substate.CurrentState, statusCode, timestamp, context);
        SetReadStatus(substate.CurrentStateId, statusCode, timestamp, context);
        SetReadStatus(substate.LastTransition, statusCode, timestamp, context);
        SetReadStatus(substate.LastTransitionId, statusCode, timestamp, context);
    }

    internal static StateMachineNode CreateOfficialStateMachineNode(
        NodeStateCollection importedNodes,
        ushort roboticsNamespaceIndex,
        uint numericIdentifier,
        string fallbackDisplayName)
    {
        var nodeId = new NodeId(numericIdentifier, roboticsNamespaceIndex);
        NodeState? node = importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));
        return new StateMachineNode(nodeId, node is null ? fallbackDisplayName : GetDisplayName(node, fallbackDisplayName));
    }

    internal static bool BindOfficialMethod(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string methodPath,
        MethodBindingDescriptor descriptor,
        GenericMethodCalledEventHandler? handler,
        List<string> missingMethodNodes,
        string methodGroupName,
        string methodLabel)
    {
        if (FindMethod(importedNodes, instanceNamespaceIndex, methodPath) is not { } method)
        {
            missingMethodNodes.Add(
                $"Official {methodGroupName} method missing: NodeId '{CreateNodeId(methodPath, instanceNamespaceIndex)}' was not found.");
            return false;
        }

        ArgumentPropertyBinding argumentBinding = RepairArgumentProperties(
            importedNodes,
            instanceNamespaceIndex,
            methodPath,
            method,
            descriptor);

        if (handler is null)
        {
            missingMethodNodes.Add($"Official {methodGroupName} method '{methodLabel}' has no server callback.");
            LogMethodBinding(method, methodPath, instanceNamespaceIndex, argumentBinding, callbackAssigned: false);
            return false;
        }

        method.Executable = true;
        method.UserExecutable = true;
        method.OnCallMethod = handler;
        LogMethodBinding(method, methodPath, instanceNamespaceIndex, argumentBinding, callbackAssigned: true);

        int inputArgumentCount = GetArgumentCount(method.InputArguments);
        int outputArgumentCount = GetArgumentCount(method.OutputArguments);
        if (inputArgumentCount != descriptor.ExpectedInputArguments.Length)
        {
            missingMethodNodes.Add(
                $"Official {methodGroupName} method '{methodLabel}' has {inputArgumentCount} runtime InputArguments after repair; expected {descriptor.ExpectedInputArguments.Length}.");
        }

        if (outputArgumentCount != descriptor.ExpectedOutputArguments.Length)
        {
            missingMethodNodes.Add(
                $"Official {methodGroupName} method '{methodLabel}' has {outputArgumentCount} runtime OutputArguments after repair; expected {descriptor.ExpectedOutputArguments.Length}.");
        }

        return true;
    }

    private static ArgumentPropertyBinding RepairArgumentProperties(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string methodPath,
        MethodState method,
        MethodBindingDescriptor descriptor)
    {
        NodeState? inputArgumentNode = FindNode(importedNodes, instanceNamespaceIndex, $"{methodPath}/InputArguments");
        NodeState? outputArgumentNode = FindNode(importedNodes, instanceNamespaceIndex, $"{methodPath}/OutputArguments");

        method.InputArguments = RepairArgumentProperty(
            inputArgumentNode,
            method,
            BrowseNames.InputArguments,
            CreateNodeId($"{methodPath}/InputArguments", instanceNamespaceIndex),
            descriptor.ExpectedInputArguments);

        method.OutputArguments = RepairArgumentProperty(
            outputArgumentNode,
            method,
            BrowseNames.OutputArguments,
            CreateNodeId($"{methodPath}/OutputArguments", instanceNamespaceIndex),
            descriptor.ExpectedOutputArguments);

        return new ArgumentPropertyBinding(inputArgumentNode is not null, outputArgumentNode is not null);
    }

    private static PropertyState<Argument[]>? RepairArgumentProperty(
        NodeState? node,
        MethodState method,
        string browseName,
        NodeId nodeId,
        Argument[] expectedArguments)
    {
        if (expectedArguments.Length == 0 && node is null)
        {
            return null;
        }

        if (node is PropertyState<Argument[]> property)
        {
            PopulateArgumentVariable(property, browseName, expectedArguments);
            return property;
        }

        if (node is BaseVariableState variable)
        {
            PopulateArgumentVariable(variable, browseName, expectedArguments);
            return CreateRuntimeArgumentProperty(method, variable, browseName, expectedArguments);
        }

        return new PropertyState<Argument[]>(method)
        {
            SymbolicName = browseName,
            ReferenceTypeId = ReferenceTypeIds.HasProperty,
            TypeDefinitionId = VariableTypeIds.PropertyType,
            NodeId = nodeId,
            BrowseName = browseName,
            DisplayName = browseName,
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            DataType = DataTypeIds.Argument,
            ValueRank = ValueRanks.OneDimension,
            AccessLevel = AccessLevels.CurrentRead,
            UserAccessLevel = AccessLevels.CurrentRead,
            Historizing = false,
            Value = expectedArguments,
            StatusCode = StatusCodes.Good,
            Timestamp = DateTime.UtcNow
        };
    }

    private static PropertyState<Argument[]> CreateRuntimeArgumentProperty(
        MethodState method,
        BaseVariableState variable,
        string browseName,
        Argument[] arguments)
    {
        return new PropertyState<Argument[]>(method)
        {
            SymbolicName = browseName,
            ReferenceTypeId = ReferenceTypeIds.HasProperty,
            TypeDefinitionId = VariableTypeIds.PropertyType,
            NodeId = variable.NodeId,
            BrowseName = browseName,
            DisplayName = browseName,
            WriteMask = variable.WriteMask,
            UserWriteMask = variable.UserWriteMask,
            DataType = DataTypeIds.Argument,
            ValueRank = ValueRanks.OneDimension,
            AccessLevel = variable.AccessLevel,
            UserAccessLevel = variable.UserAccessLevel,
            Historizing = variable.Historizing,
            Value = arguments,
            StatusCode = StatusCodes.Good,
            Timestamp = DateTime.UtcNow
        };
    }

    private static void PopulateArgumentVariable(
        BaseVariableState variable,
        string browseName,
        Argument[] arguments)
    {
        variable.SymbolicName = browseName;
        variable.DataType = DataTypeIds.Argument;
        variable.ValueRank = ValueRanks.OneDimension;
        variable.Value = arguments;
        variable.StatusCode = StatusCodes.Good;
        variable.Timestamp = DateTime.UtcNow;
    }

    private static void LogMethodBinding(
        MethodState method,
        string methodPath,
        ushort instanceNamespaceIndex,
        ArgumentPropertyBinding argumentBinding,
        bool callbackAssigned)
    {
        string parentPath = methodPath[..methodPath.LastIndexOf('/')];
        Console.WriteLine(
            "Official Robotics method binding diagnostics: "
            + $"BrowseName={method.BrowseName}, "
            + $"NodeId={method.NodeId}, "
            + $"ParentNodeId={CreateNodeId(parentPath, instanceNamespaceIndex)}, "
            + $"MethodState.InputArgumentsExists={method.InputArguments is not null}, "
            + $"MethodState.InputArguments.ValueCount={GetArgumentCount(method.InputArguments)}, "
            + $"MethodState.OutputArgumentsExists={method.OutputArguments is not null}, "
            + $"MethodState.OutputArguments.ValueCount={GetArgumentCount(method.OutputArguments)}, "
            + $"InputArgumentsChildPropertyNodeExists={argumentBinding.InputArgumentsChildPropertyNodeExists}, "
            + $"OutputArgumentsChildPropertyNodeExists={argumentBinding.OutputArgumentsChildPropertyNodeExists}, "
            + $"OnCallMethodAssigned={callbackAssigned}");
    }

    private static int GetArgumentCount(PropertyState<Argument[]>? argumentProperty)
    {
        return argumentProperty?.Value?.Length ?? 0;
    }

    internal static Argument CreateArgument(string name, NodeId dataType)
    {
        return new Argument
        {
            Name = name,
            DataType = dataType,
            ValueRank = ValueRanks.Scalar,
            Description = new LocalizedText(name)
        };
    }

    private sealed record ArgumentPropertyBinding(
        bool InputArgumentsChildPropertyNodeExists,
        bool OutputArgumentsChildPropertyNodeExists);

    internal sealed record MethodBindingDescriptor(
        string RelativePath,
        Argument[] ExpectedInputArguments,
        Argument[] ExpectedOutputArguments);
}

internal sealed record StateMachineNode(NodeId NodeId, string DisplayName);

internal enum RoboticsProgramState
{
    Idle,
    Ready,
    Executing
}

internal enum RoboticsReadySubstate
{
    AtProgramStart,
    Suspended
}

internal enum RoboticsIdleSubstate
{
    StandBy,
    GettingReady
}

internal enum RoboticsExecutingSubstate
{
    Running,
    Stopping
}

internal enum RoboticsTransitionReason : short
{
    External = 1,
    System = 3,
    Application = 5
}
