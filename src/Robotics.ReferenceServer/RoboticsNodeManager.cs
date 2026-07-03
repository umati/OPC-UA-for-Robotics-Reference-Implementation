using Opc.Ua;
using Opc.Ua.Server;
using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsNodeManager : CustomNodeManager2
{
    private const string NamespaceUri = "urn:RoboticsReferenceServer:Robotics";
    private static readonly TimeSpan SimulationUpdateInterval = TimeSpan.FromMilliseconds(100);

    private readonly RobotSimulationService _simulationService = new();
    private readonly List<BaseVariableState> _telemetryVariables = [];
    private readonly Dictionary<RobotAxisName, AxisVariableSet> _axisVariables = [];

    private BaseDataVariableState<bool>? _isMovingVariable;
    private BaseDataVariableState<double>? _speedVariable;
    private BaseDataVariableState<double>? _accelerationVariable;
    private PoseVariableSet? _poseVariables;
    private Timer? _simulationTimer;
    private DateTimeOffset _lastSimulationUpdateUtc = DateTimeOffset.UtcNow;

    public RoboticsNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        : base(server, configuration, NamespaceUri)
    {
        SystemContext.NodeIdFactory = this;
    }

    public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
    {
        var robotsFolder = CreateRobotsFolder();
        var robot = CreateSixAxisRobot(robotsFolder);

        RefreshTelemetryVariables(_simulationService.GetSnapshot());
        AddPredefinedNode(SystemContext, robotsFolder);
        AddObjectsFolderReference(externalReferences, robotsFolder.NodeId);
        SetStartupJointTarget();
        StartSimulationUpdates();
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

        return robot;
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

            _simulationService.Update(elapsed);
            RefreshTelemetryVariables(_simulationService.GetSnapshot());

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

    private sealed record AxisVariableSet(
        BaseDataVariableState<double> PositionDegrees,
        BaseDataVariableState<double> VelocityDegreesPerSecond,
        BaseDataVariableState<double> TargetPositionDegrees,
        BaseDataVariableState<double> MotorLoadPercent,
        BaseDataVariableState<double> TemperatureCelsius);
}
