using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.ControlBridge;
using Robotics.ReferenceServer.InformationModel;
using Robotics.ReferenceServer.Simulation;
using Robotics.ReferenceServer.Telemetry;
using Robotics.Shared;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsNodeManager : CustomNodeManager2
{
    private const string NamespaceUri = "urn:RoboticsReferenceServer:Robotics";
    private static readonly TimeSpan SimulationUpdateInterval = TimeSpan.FromMilliseconds(100);

    private readonly RobotSimulationService _simulationService;
    private readonly RobotProgramExecutor _programExecutor;
    private readonly RobotControlCommandService _controlCommands;
    private readonly RobotNodeBinder _robotNodeBinder = new();
    private readonly RobotAddressSpaceMode _addressSpaceMode;
    private readonly IRobotTelemetryPublisher? _telemetryPublisher;
    private readonly List<BaseVariableState> _telemetryVariables = [];
    private readonly Dictionary<RobotAxisName, AxisVariableSet> _axisVariables = [];

    private BaseDataVariableState<bool>? _isMovingVariable;
    private BaseDataVariableState<double>? _speedVariable;
    private BaseDataVariableState<double>? _accelerationVariable;
    private PoseVariableSet? _poseVariables;
    private RemoteControlStatusVariableSet? _remoteControlStatusVariables;
    private RemoteProgramStatusVariableSet? _remoteProgramStatusVariables;
    private RobotNodeHandles? _importedRobotNodeHandles;
    private Timer? _simulationTimer;
    private DateTimeOffset _lastSimulationUpdateUtc = DateTimeOffset.UtcNow;

    public RoboticsNodeManager(
        IServerInternal server,
        ApplicationConfiguration configuration,
        RobotAddressSpaceMode addressSpaceMode = RobotAddressSpaceMode.Both,
        IRobotTelemetryPublisher? telemetryPublisher = null,
        RobotControlBridgeServiceRegistry? controlBridgeServiceRegistry = null)
        : base(
            server,
            configuration,
            NamespaceUri,
            NodeSetLoader.DiNamespaceUri,
            NodeSetLoader.RoboticsNamespaceUri,
            NodeSetLoader.InstanceNamespaceUri)
    {
        _addressSpaceMode = addressSpaceMode;
        _telemetryPublisher = telemetryPublisher;
        _simulationService = new RobotSimulationService();
        _programExecutor = new RobotProgramExecutor(_simulationService);
        _controlCommands = new RobotControlCommandService(
            _simulationService,
            _programExecutor,
            Lock,
            UpdateRemoteControlStatus,
            UpdateRemoteProgramCommandStatus);
        controlBridgeServiceRegistry?.SetCommandService(_controlCommands);
        SystemContext.NodeIdFactory = this;
    }

    public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
    {
        if (_addressSpaceMode is RobotAddressSpaceMode.Official or RobotAddressSpaceMode.Both)
        {
            // The imported MinimalRealistic instance NodeSet is the first official Robotics model variant.
            // Selected imported variables are bound to simulation values below; richer binding remains a later milestone.
            NodeStateCollection importedRoboticsNodes = NodeSetLoader.LoadRequiredNodeSets(SystemContext);
            AddImportedRoboticsNodes(importedRoboticsNodes, externalReferences);
        }

        if (_addressSpaceMode is RobotAddressSpaceMode.Temporary or RobotAddressSpaceMode.Both)
        {
            // Keep the temporary demo nodes during migration so existing telemetry and methods remain available.
            var robotsFolder = CreateRobotsFolder();
            CreateSixAxisRobot(robotsFolder);

            AddPredefinedNode(SystemContext, robotsFolder);
            AddObjectsFolderReference(externalReferences, robotsFolder.NodeId);
        }
        else
        {
            // The temporary RemoteControl/RemotePrograms methods are not recreated on official TaskControl nodes yet.
            Console.WriteLine(
                "RemoteControl and RemotePrograms are available only in Temporary/Both mode until official Robotics remote-operation mapping is implemented.");
        }

        SetStartupJointTarget();

        RobotTelemetrySnapshot startupRobotSnapshot = _simulationService.GetSnapshot();
        RobotProgramExecutionSnapshot startupProgramSnapshot = _programExecutor.GetSnapshot();
        RefreshTelemetryVariables(startupRobotSnapshot);
        RefreshRemoteProgramStatus(startupProgramSnapshot);
        _telemetryPublisher?.Publish(startupRobotSnapshot, startupProgramSnapshot);

        StartSimulationUpdates();
    }

    private void AddImportedRoboticsNodes(
        NodeStateCollection importedRoboticsNodes,
        IDictionary<NodeId, IList<IReference>> externalReferences)
    {
        NodeId minimalRealisticRootNodeId = NodeSetLoader.GetMinimalRealisticRootNodeId(SystemContext);
        if (importedRoboticsNodes.Find(node => Equals(node.NodeId, minimalRealisticRootNodeId)) is null)
        {
            throw new InvalidOperationException(
                $"The imported MinimalRealistic instance root '{minimalRealisticRootNodeId}' was not found after NodeSet loading.");
        }

        foreach (NodeState importedNode in importedRoboticsNodes)
        {
            AddPredefinedNode(SystemContext, importedNode);
        }

        AddReverseReferences(externalReferences);
        BindImportedRoboticsNodes(importedRoboticsNodes);
    }

    private void BindImportedRoboticsNodes(NodeStateCollection importedRoboticsNodes)
    {
        Console.WriteLine("MinimalRealistic instance node binding started.");

        RobotNodeHandles handles;
        try
        {
            handles = _robotNodeBinder.Bind(importedRoboticsNodes, SystemContext);
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Warning: MinimalRealistic binding failed and will be skipped. {GetBindingFallbackMessage()} {exception.Message}");
            _importedRobotNodeHandles = null;
            return;
        }

        _importedRobotNodeHandles = handles;

        Console.WriteLine(
            $"MinimalRealistic instance node binding completed: {handles.BoundNodeCount} nodes successfully bound.");

        foreach (string missingNode in handles.MissingExpectedNodes)
        {
            Console.WriteLine($"Warning: MinimalRealistic binding: {missingNode}");
        }

        if (handles.BoundNodeCount < 10)
        {
            Console.WriteLine(
                $"Warning: MinimalRealistic binding found too few nodes to be useful. {GetBindingFallbackMessage()}");
        }
    }

    private string GetBindingFallbackMessage()
    {
        return _addressSpaceMode is RobotAddressSpaceMode.Temporary or RobotAddressSpaceMode.Both
            ? "The server will keep running with the temporary demo nodes active."
            : "The server will keep running without official simulation value bindings.";
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _simulationTimer?.Dispose();
            _simulationTimer = null;
        }

        base.Dispose(disposing);
    }

    private FolderState CreateRobotsFolder()
    {
        var robotsFolder = new FolderState(null)
        {
            SymbolicName = "Robots",
            ReferenceTypeId = ReferenceTypeIds.Organizes,
            TypeDefinitionId = ObjectTypeIds.FolderType,
            NodeId = new NodeId("Robots", NamespaceIndex),
            BrowseName = new QualifiedName("Robots", NamespaceIndex),
            DisplayName = new LocalizedText("Robots"),
            Description = new LocalizedText("Folder for robot instances."),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            EventNotifier = EventNotifiers.None
        };

        robotsFolder.AddReference(ReferenceTypeIds.Organizes, true, ObjectIds.ObjectsFolder);

        return robotsFolder;
    }

    private BaseObjectState CreateSixAxisRobot(FolderState robotsFolder)
    {
        var robot = CreateObject(robotsFolder, "Robots.SixAxisRobot", "SixAxisRobot", ReferenceTypeIds.Organizes);

        CreateVariable(robot, "Robots.SixAxisRobot.Manufacturer", "Manufacturer", DataTypeIds.String, SixAxisRobot.Manufacturer);
        CreateVariable(robot, "Robots.SixAxisRobot.Model", "Model", DataTypeIds.String, SixAxisRobot.Model);
        CreateVariable(robot, "Robots.SixAxisRobot.SerialNumber", "SerialNumber", DataTypeIds.String, SixAxisRobot.SerialNumber);
        CreateVariable(robot, "Robots.SixAxisRobot.ProductCode", "ProductCode", DataTypeIds.String, SixAxisRobot.ProductCode);

        _isMovingVariable = CreateVariable(robot, "Robots.SixAxisRobot.IsMoving", "IsMoving", DataTypeIds.Boolean, false);
        _speedVariable = CreateVariable(robot, "Robots.SixAxisRobot.Speed", "Speed", DataTypeIds.Double, 0.0);
        _accelerationVariable = CreateVariable(robot, "Robots.SixAxisRobot.Acceleration", "Acceleration", DataTypeIds.Double, 0.0);

        var cartesianPose = CreateObject(robot, "Robots.SixAxisRobot.CartesianPose", "CartesianPose", ReferenceTypeIds.HasComponent);
        _poseVariables = new PoseVariableSet(
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.X", "X", DataTypeIds.Double, 0.0),
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.Y", "Y", DataTypeIds.Double, 0.0),
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.Z", "Z", DataTypeIds.Double, 0.0),
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.Rx", "Rx", DataTypeIds.Double, 0.0),
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.Ry", "Ry", DataTypeIds.Double, 0.0),
            CreateVariable(cartesianPose, "Robots.SixAxisRobot.CartesianPose.Rz", "Rz", DataTypeIds.Double, 0.0));

        var axes = CreateFolder(robot, "Robots.SixAxisRobot.Axes", "Axes", ReferenceTypeIds.HasComponent);
        foreach (RobotAxisName axisName in Enum.GetValues<RobotAxisName>())
        {
            CreateAxis(axes, axisName);
        }

        CreateRemoteControl(robot);
        CreateRemotePrograms(robot);

        return robot;
    }

    private void CreateRemoteControl(NodeState robot)
    {
        var remoteControl = CreateFolder(robot, "Robots.SixAxisRobot.RemoteControl", "RemoteControl", ReferenceTypeIds.HasComponent);

        _remoteControlStatusVariables = new RemoteControlStatusVariableSet(
            CreateVariable(remoteControl, "Robots.SixAxisRobot.RemoteControl.LastCommandName", "LastCommandName", DataTypeIds.String, string.Empty),
            CreateVariable(remoteControl, "Robots.SixAxisRobot.RemoteControl.LastCommandAccepted", "LastCommandAccepted", DataTypeIds.Boolean, false),
            CreateVariable(remoteControl, "Robots.SixAxisRobot.RemoteControl.LastCommandMessage", "LastCommandMessage", DataTypeIds.String, "No command has been called."),
            CreateVariable(remoteControl, "Robots.SixAxisRobot.RemoteControl.LastCommandTimestampUtc", "LastCommandTimestampUtc", DataTypeIds.DateTime, DateTime.MinValue));

        CreateMethod(
            remoteControl,
            "Robots.SixAxisRobot.RemoteControl.MoveJoints",
            "MoveJoints",
            OnMoveJoints,
            [
                CreateDoubleArgument("STargetDegrees"),
                CreateDoubleArgument("LTargetDegrees"),
                CreateDoubleArgument("UTargetDegrees"),
                CreateDoubleArgument("RTargetDegrees"),
                CreateDoubleArgument("BTargetDegrees"),
                CreateDoubleArgument("TTargetDegrees")
            ]);

        CreateMethod(remoteControl, "Robots.SixAxisRobot.RemoteControl.ResetToHome", "ResetToHome", OnResetToHome);
        CreateMethod(remoteControl, "Robots.SixAxisRobot.RemoteControl.StartDemoMotion", "StartDemoMotion", OnStartDemoMotion);
        CreateMethod(remoteControl, "Robots.SixAxisRobot.RemoteControl.StopMotion", "StopMotion", OnStopMotion);
    }

    private void CreateRemotePrograms(NodeState robot)
    {
        var remotePrograms = CreateFolder(robot, "Robots.SixAxisRobot.RemotePrograms", "RemotePrograms", ReferenceTypeIds.HasComponent);

        _remoteProgramStatusVariables = new RemoteProgramStatusVariableSet(
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.CurrentProgramName", "CurrentProgramName", DataTypeIds.String, string.Empty),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.ProgramExecutionState", "ProgramExecutionState", DataTypeIds.String, RobotProgramExecutionState.Idle.ToString()),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.CurrentStepIndex", "CurrentStepIndex", DataTypeIds.Int32, -1),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.LastProgramCommandName", "LastProgramCommandName", DataTypeIds.String, string.Empty),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.LastProgramCommandAccepted", "LastProgramCommandAccepted", DataTypeIds.Boolean, false),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.LastProgramMessage", "LastProgramMessage", DataTypeIds.String, "No program command has been called."),
            CreateVariable(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.LastProgramCommandTimestampUtc", "LastProgramCommandTimestampUtc", DataTypeIds.DateTime, DateTime.MinValue));

        CreateMethod(
            remotePrograms,
            "Robots.SixAxisRobot.RemotePrograms.LoadProgramFromJson",
            "LoadProgramFromJson",
            OnLoadProgramFromJson,
            [CreateStringArgument("ProgramJson")]);

        CreateMethod(
            remotePrograms,
            "Robots.SixAxisRobot.RemotePrograms.LoadSampleProgram",
            "LoadSampleProgram",
            OnLoadSampleProgram,
            [CreateStringArgument("SampleProgramName")]);

        CreateMethod(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.StartProgram", "StartProgram", OnStartProgram);
        CreateMethod(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.PauseProgram", "PauseProgram", OnPauseProgram);
        CreateMethod(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.ResumeProgram", "ResumeProgram", OnResumeProgram);
        CreateMethod(remotePrograms, "Robots.SixAxisRobot.RemotePrograms.StopProgram", "StopProgram", OnStopProgram);
    }

    private void CreateAxis(NodeState axesFolder, RobotAxisName axisName)
    {
        string axisBrowseName = $"{axisName}Axis";
        string axisPath = $"Robots.SixAxisRobot.Axes.{axisBrowseName}";
        var axis = CreateObject(axesFolder, axisPath, axisBrowseName, ReferenceTypeIds.Organizes);

        _axisVariables[axisName] = new AxisVariableSet(
            CreateVariable(axis, $"{axisPath}.PositionDegrees", "PositionDegrees", DataTypeIds.Double, 0.0),
            CreateVariable(axis, $"{axisPath}.VelocityDegreesPerSecond", "VelocityDegreesPerSecond", DataTypeIds.Double, 0.0),
            CreateVariable(axis, $"{axisPath}.TargetPositionDegrees", "TargetPositionDegrees", DataTypeIds.Double, 0.0),
            CreateVariable(axis, $"{axisPath}.MotorLoadPercent", "MotorLoadPercent", DataTypeIds.Double, 0.0),
            CreateVariable(axis, $"{axisPath}.TemperatureCelsius", "TemperatureCelsius", DataTypeIds.Double, 0.0));
    }

    private FolderState CreateFolder(NodeState parent, string nodeId, string browseName, NodeId referenceTypeId)
    {
        var folder = new FolderState(parent)
        {
            SymbolicName = browseName,
            ReferenceTypeId = referenceTypeId,
            TypeDefinitionId = ObjectTypeIds.FolderType,
            NodeId = new NodeId(nodeId, NamespaceIndex),
            BrowseName = new QualifiedName(browseName, NamespaceIndex),
            DisplayName = new LocalizedText(browseName),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            EventNotifier = EventNotifiers.None
        };

        parent.AddChild(folder);
        return folder;
    }

    private BaseObjectState CreateObject(NodeState parent, string nodeId, string browseName, NodeId referenceTypeId)
    {
        var instance = new BaseObjectState(parent)
        {
            SymbolicName = browseName,
            ReferenceTypeId = referenceTypeId,
            TypeDefinitionId = ObjectTypeIds.BaseObjectType,
            NodeId = new NodeId(nodeId, NamespaceIndex),
            BrowseName = new QualifiedName(browseName, NamespaceIndex),
            DisplayName = new LocalizedText(browseName),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            EventNotifier = EventNotifiers.None
        };

        parent.AddChild(instance);
        return instance;
    }

    private BaseDataVariableState<T> CreateVariable<T>(
        NodeState parent,
        string nodeId,
        string browseName,
        NodeId dataTypeId,
        T initialValue)
    {
        var variable = new BaseDataVariableState<T>(parent)
        {
            SymbolicName = browseName,
            ReferenceTypeId = ReferenceTypeIds.HasComponent,
            TypeDefinitionId = VariableTypeIds.BaseDataVariableType,
            NodeId = new NodeId(nodeId, NamespaceIndex),
            BrowseName = new QualifiedName(browseName, NamespaceIndex),
            DisplayName = new LocalizedText(browseName),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            DataType = dataTypeId,
            ValueRank = ValueRanks.Scalar,
            AccessLevel = AccessLevels.CurrentRead,
            UserAccessLevel = AccessLevels.CurrentRead,
            Historizing = false,
            MinimumSamplingInterval = SimulationUpdateInterval.TotalMilliseconds,
            Value = initialValue,
            StatusCode = StatusCodes.Good,
            Timestamp = DateTime.UtcNow
        };

        parent.AddChild(variable);
        _telemetryVariables.Add(variable);
        return variable;
    }

    private MethodState CreateMethod(
        NodeState parent,
        string nodeId,
        string browseName,
        GenericMethodCalledEventHandler handler,
        Argument[]? inputArguments = null)
    {
        var method = new MethodState(parent)
        {
            SymbolicName = browseName,
            ReferenceTypeId = ReferenceTypeIds.HasComponent,
            NodeId = new NodeId(nodeId, NamespaceIndex),
            BrowseName = new QualifiedName(browseName, NamespaceIndex),
            DisplayName = new LocalizedText(browseName),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            Executable = true,
            UserExecutable = true,
            OnCallMethod = handler
        };

        if (inputArguments is not null)
        {
            method.InputArguments = new PropertyState<Argument[]>(method)
            {
                SymbolicName = BrowseNames.InputArguments,
                ReferenceTypeId = ReferenceTypeIds.HasProperty,
                TypeDefinitionId = VariableTypeIds.PropertyType,
                NodeId = new NodeId($"{nodeId}.InputArguments", NamespaceIndex),
                BrowseName = BrowseNames.InputArguments,
                DisplayName = BrowseNames.InputArguments,
                WriteMask = AttributeWriteMask.None,
                UserWriteMask = AttributeWriteMask.None,
                DataType = DataTypeIds.Argument,
                ValueRank = ValueRanks.OneDimension,
                AccessLevel = AccessLevels.CurrentRead,
                UserAccessLevel = AccessLevels.CurrentRead,
                Historizing = false,
                Value = inputArguments,
                StatusCode = StatusCodes.Good,
                Timestamp = DateTime.UtcNow
            };
        }

        parent.AddChild(method);
        return method;
    }

    private ServiceResult OnMoveJoints(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        if (inputArguments.Count != 6)
        {
            _controlCommands.RejectMoveJoints("MoveJoints requires six joint target inputs.");
            return StatusCodes.BadInvalidArgument;
        }

        try
        {
            RobotControlCommandResult result = _controlCommands.MoveJoints(
                Convert.ToDouble(inputArguments[0]),
                Convert.ToDouble(inputArguments[1]),
                Convert.ToDouble(inputArguments[2]),
                Convert.ToDouble(inputArguments[3]),
                Convert.ToDouble(inputArguments[4]),
                Convert.ToDouble(inputArguments[5]));

            return ToServiceResult(result);
        }
        catch (Exception exception) when (exception is InvalidCastException or FormatException or OverflowException)
        {
            _controlCommands.RejectMoveJoints("MoveJoints inputs must be double values.");
            return StatusCodes.BadInvalidArgument;
        }
    }

    private ServiceResult OnResetToHome(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.ResetToHome());
    }

    private ServiceResult OnStartDemoMotion(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.StartDemoMotion());
    }

    private ServiceResult OnStopMotion(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.StopMotion());
    }

    private ServiceResult OnLoadProgramFromJson(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        string? programJson = inputArguments.Count == 1 ? inputArguments[0] as string : null;
        return ToServiceResult(_controlCommands.LoadProgramFromJson(programJson));
    }

    private ServiceResult OnLoadSampleProgram(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        string? sampleProgramName = inputArguments.Count == 1 ? inputArguments[0] as string : null;
        return ToServiceResult(_controlCommands.LoadSampleProgram(sampleProgramName));
    }

    private ServiceResult OnStartProgram(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.StartProgram());
    }

    private ServiceResult OnPauseProgram(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        _controlCommands.PauseProgram();
        return ServiceResult.Good;
    }

    private ServiceResult OnResumeProgram(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.ResumeProgram());
    }

    private ServiceResult OnStopProgram(
        ISystemContext context,
        MethodState method,
        IList<object> inputArguments,
        IList<object> outputArguments)
    {
        return ToServiceResult(_controlCommands.StopProgram());
    }

    private void UpdateRemoteControlStatus(string commandName, bool accepted, string message)
    {
        DateTime timestamp = DateTime.UtcNow;

        SetVariableValue(_remoteControlStatusVariables?.LastCommandName, commandName, timestamp);
        SetVariableValue(_remoteControlStatusVariables?.LastCommandAccepted, accepted, timestamp);
        SetVariableValue(_remoteControlStatusVariables?.LastCommandMessage, message, timestamp);
        SetVariableValue(_remoteControlStatusVariables?.LastCommandTimestampUtc, timestamp, timestamp);

        _remoteControlStatusVariables?.LastCommandName.ClearChangeMasks(SystemContext, false);
        _remoteControlStatusVariables?.LastCommandAccepted.ClearChangeMasks(SystemContext, false);
        _remoteControlStatusVariables?.LastCommandMessage.ClearChangeMasks(SystemContext, false);
        _remoteControlStatusVariables?.LastCommandTimestampUtc.ClearChangeMasks(SystemContext, false);
    }

    private void UpdateRemoteProgramCommandStatus(string commandName, bool accepted, string message)
    {
        DateTime timestamp = DateTime.UtcNow;

        SetVariableValue(_remoteProgramStatusVariables?.LastProgramCommandName, commandName, timestamp);
        SetVariableValue(_remoteProgramStatusVariables?.LastProgramCommandAccepted, accepted, timestamp);
        SetVariableValue(_remoteProgramStatusVariables?.LastProgramMessage, message, timestamp);
        SetVariableValue(_remoteProgramStatusVariables?.LastProgramCommandTimestampUtc, timestamp, timestamp);
        RefreshRemoteProgramStatus(_programExecutor.GetSnapshot());

        _remoteProgramStatusVariables?.LastProgramCommandName.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.LastProgramCommandAccepted.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.LastProgramMessage.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.LastProgramCommandTimestampUtc.ClearChangeMasks(SystemContext, false);
    }

    private void RefreshRemoteProgramStatus(RobotProgramExecutionSnapshot snapshot)
    {
        DateTime timestamp = DateTime.UtcNow;

        SetVariableValue(_remoteProgramStatusVariables?.CurrentProgramName, snapshot.CurrentProgramName ?? string.Empty, timestamp);
        SetVariableValue(_remoteProgramStatusVariables?.ProgramExecutionState, snapshot.State.ToString(), timestamp);
        SetVariableValue(_remoteProgramStatusVariables?.CurrentStepIndex, snapshot.CurrentStepIndex, timestamp);

        if (!string.IsNullOrWhiteSpace(snapshot.LastErrorMessage))
        {
            SetVariableValue(_remoteProgramStatusVariables?.LastProgramMessage, snapshot.LastErrorMessage, timestamp);
        }

        _remoteProgramStatusVariables?.CurrentProgramName.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.ProgramExecutionState.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.CurrentStepIndex.ClearChangeMasks(SystemContext, false);
        _remoteProgramStatusVariables?.LastProgramMessage.ClearChangeMasks(SystemContext, false);
    }

    private static Argument CreateDoubleArgument(string name)
    {
        return new Argument
        {
            Name = name,
            DataType = DataTypeIds.Double,
            ValueRank = ValueRanks.Scalar,
            Description = new LocalizedText(name)
        };
    }

    private static Argument CreateStringArgument(string name)
    {
        return new Argument
        {
            Name = name,
            DataType = DataTypeIds.String,
            ValueRank = ValueRanks.Scalar,
            Description = new LocalizedText(name)
        };
    }

    private static ServiceResult ToServiceResult(RobotControlCommandResult result)
    {
        if (result.Accepted)
        {
            return ServiceResult.Good;
        }

        return result.FailureKind switch
        {
            RobotControlCommandFailureKind.InvalidArgument => StatusCodes.BadInvalidArgument,
            RobotControlCommandFailureKind.InvalidState => StatusCodes.BadInvalidState,
            RobotControlCommandFailureKind.NotFound => StatusCodes.BadNotFound,
            RobotControlCommandFailureKind.Unexpected => StatusCodes.BadUnexpectedError,
            _ => StatusCodes.BadUnexpectedError
        };
    }

    private void SetStartupJointTarget()
    {
        _simulationService.SetJointTargets(
        [
            new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = 45 },
            new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = -30 },
            new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = 60 },
            new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = 20 },
            new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = 15 },
            new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = 90 }
        ]);
    }

    private void StartSimulationUpdates()
    {
        _simulationTimer ??= new Timer(UpdateSimulation, null, TimeSpan.Zero, SimulationUpdateInterval);
    }

    private void UpdateSimulation(object? state)
    {
        lock (Lock)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            TimeSpan elapsed = now - _lastSimulationUpdateUtc;
            _lastSimulationUpdateUtc = now;

            _programExecutor.Update(elapsed);
            _simulationService.Update(elapsed);

            RobotTelemetrySnapshot robotSnapshot = _simulationService.GetSnapshot();
            RobotProgramExecutionSnapshot programSnapshot = _programExecutor.GetSnapshot();
            RefreshTelemetryVariables(robotSnapshot);
            RefreshRemoteProgramStatus(programSnapshot);
            _telemetryPublisher?.Publish(robotSnapshot, programSnapshot);

            foreach (BaseVariableState variable in _telemetryVariables)
            {
                variable.ClearChangeMasks(SystemContext, false);
            }
        }
    }

    private void RefreshTelemetryVariables(RobotTelemetrySnapshot snapshot)
    {
        DateTime timestamp = snapshot.TimestampUtc.UtcDateTime;

        SetVariableValue(_isMovingVariable, snapshot.IsMoving, timestamp);
        SetVariableValue(_speedVariable, snapshot.Speed, timestamp);
        SetVariableValue(_accelerationVariable, snapshot.Acceleration, timestamp);

        if (_poseVariables is not null)
        {
            SetVariableValue(_poseVariables.X, snapshot.CartesianPose.X, timestamp);
            SetVariableValue(_poseVariables.Y, snapshot.CartesianPose.Y, timestamp);
            SetVariableValue(_poseVariables.Z, snapshot.CartesianPose.Z, timestamp);
            SetVariableValue(_poseVariables.Rx, snapshot.CartesianPose.Rx, timestamp);
            SetVariableValue(_poseVariables.Ry, snapshot.CartesianPose.Ry, timestamp);
            SetVariableValue(_poseVariables.Rz, snapshot.CartesianPose.Rz, timestamp);
        }

        foreach (RobotAxisState axisState in snapshot.AxisStates)
        {
            if (!_axisVariables.TryGetValue(axisState.AxisName, out AxisVariableSet? axisVariables))
            {
                continue;
            }

            SetVariableValue(axisVariables.PositionDegrees, axisState.PositionDegrees, timestamp);
            SetVariableValue(axisVariables.VelocityDegreesPerSecond, axisState.VelocityDegreesPerSecond, timestamp);
            SetVariableValue(axisVariables.TargetPositionDegrees, axisState.TargetPositionDegrees, timestamp);
            SetVariableValue(axisVariables.MotorLoadPercent, axisState.MotorLoadPercent, timestamp);
            SetVariableValue(axisVariables.TemperatureCelsius, axisState.TemperatureCelsius, timestamp);
        }

        _robotNodeBinder.UpdateFromSnapshot(_importedRobotNodeHandles, snapshot, SystemContext);
    }

    private static void SetVariableValue<T>(BaseDataVariableState<T>? variable, T value, DateTime timestamp)
    {
        if (variable is null)
        {
            return;
        }

        variable.Value = value;
        variable.StatusCode = StatusCodes.Good;
        variable.Timestamp = timestamp;
    }

    private static void AddObjectsFolderReference(
        IDictionary<NodeId, IList<IReference>> externalReferences,
        NodeId robotsFolderId)
    {
        if (!externalReferences.TryGetValue(ObjectIds.ObjectsFolder, out var references))
        {
            externalReferences[ObjectIds.ObjectsFolder] = references = new List<IReference>();
        }

        references.Add(new NodeStateReference(ReferenceTypeIds.Organizes, false, robotsFolderId));
    }

    private sealed record PoseVariableSet(
        BaseDataVariableState<double> X,
        BaseDataVariableState<double> Y,
        BaseDataVariableState<double> Z,
        BaseDataVariableState<double> Rx,
        BaseDataVariableState<double> Ry,
        BaseDataVariableState<double> Rz);

    private sealed record RemoteControlStatusVariableSet(
        BaseDataVariableState<string> LastCommandName,
        BaseDataVariableState<bool> LastCommandAccepted,
        BaseDataVariableState<string> LastCommandMessage,
        BaseDataVariableState<DateTime> LastCommandTimestampUtc);

    private sealed record RemoteProgramStatusVariableSet(
        BaseDataVariableState<string> CurrentProgramName,
        BaseDataVariableState<string> ProgramExecutionState,
        BaseDataVariableState<int> CurrentStepIndex,
        BaseDataVariableState<string> LastProgramCommandName,
        BaseDataVariableState<bool> LastProgramCommandAccepted,
        BaseDataVariableState<string> LastProgramMessage,
        BaseDataVariableState<DateTime> LastProgramCommandTimestampUtc);

    private sealed record AxisVariableSet(
        BaseDataVariableState<double> PositionDegrees,
        BaseDataVariableState<double> VelocityDegreesPerSecond,
        BaseDataVariableState<double> TargetPositionDegrees,
        BaseDataVariableState<double> MotorLoadPercent,
        BaseDataVariableState<double> TemperatureCelsius);
}
