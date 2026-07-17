using Opc.Ua;
using Robotics.Shared;

namespace Robotics.ReferenceServer.InformationModel;

public sealed class RobotNodeBinder
{
    private const string MotionDevicePath = "SixAxisRobot.MinimalRealistic/MotionDevice/SixAxisRobot";
    private const string AxisPathPrefix = "SixAxisRobot.MinimalRealistic/Axis";
    private const string MotorPathPrefix = "SixAxisRobot.MinimalRealistic/Motor";

    private static readonly IReadOnlyDictionary<RobotAxisName, AxisNodePath> AxisNodePaths =
        new Dictionary<RobotAxisName, AxisNodePath>
        {
            [RobotAxisName.S] = new("SAxis", "SMotor"),
            [RobotAxisName.L] = new("LAxis", "LMotor"),
            [RobotAxisName.U] = new("UAxis", "UMotor"),
            [RobotAxisName.R] = new("RAxis", "RMotor"),
            [RobotAxisName.B] = new("BAxis", "BMotor"),
            [RobotAxisName.T] = new("TAxis", "TMotor")
        };

    public RobotNodeHandles Bind(NodeStateCollection importedNodes, ISystemContext context)
    {
        ArgumentNullException.ThrowIfNull(importedNodes);
        ArgumentNullException.ThrowIfNull(context);

        ushort instanceNamespaceIndex = GetInstanceNamespaceIndex(context);
        var handles = new RobotNodeHandles();

        handles.Manufacturer = FindVariable(importedNodes, instanceNamespaceIndex, $"{MotionDevicePath}/Manufacturer", handles, "MotionDevice Manufacturer");
        handles.Model = FindVariable(importedNodes, instanceNamespaceIndex, $"{MotionDevicePath}/Model", handles, "MotionDevice Model");
        handles.SerialNumber = FindVariable(importedNodes, instanceNamespaceIndex, $"{MotionDevicePath}/SerialNumber", handles, "MotionDevice SerialNumber");
        handles.ProductCode = FindVariable(importedNodes, instanceNamespaceIndex, $"{MotionDevicePath}/ProductCode", handles, "MotionDevice ProductCode");

        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.S, handles.SAxis, handles);
        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.L, handles.LAxis, handles);
        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.U, handles.UAxis, handles);
        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.R, handles.RAxis, handles);
        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.B, handles.BAxis, handles);
        BindAxis(importedNodes, instanceNamespaceIndex, RobotAxisName.T, handles.TAxis, handles);

        return handles;
    }

    public void UpdateFromSnapshot(RobotNodeHandles? handles, RobotTelemetrySnapshot snapshot, ISystemContext context)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        ArgumentNullException.ThrowIfNull(context);

        if (handles is null)
        {
            return;
        }

        DateTime timestamp = snapshot.TimestampUtc.UtcDateTime;

        SetStringLikeValue(handles.Manufacturer, snapshot.Manufacturer, timestamp, context);
        SetStringLikeValue(handles.Model, snapshot.Model, timestamp, context);
        SetStringLikeValue(handles.SerialNumber, snapshot.SerialNumber, timestamp, context);
        SetStringLikeValue(handles.ProductCode, snapshot.ProductCode, timestamp, context);

        foreach (RobotAxisState axisState in snapshot.AxisStates)
        {
            AxisNodeHandles axisHandles = GetAxisHandles(handles, axisState.AxisName);
            SetValue(axisHandles.PositionDegrees, axisState.PositionDegrees, timestamp, context);
            SetValue(axisHandles.VelocityDegreesPerSecond, axisState.VelocityDegreesPerSecond, timestamp, context);
            SetValue(axisHandles.TargetPositionDegrees, axisState.TargetPositionDegrees, timestamp, context);
            SetValue(axisHandles.MotorLoadPercent, axisState.MotorLoadPercent, timestamp, context);
            SetValue(axisHandles.TemperatureCelsius, axisState.TemperatureCelsius, timestamp, context);
        }
    }

    private static void BindAxis(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        RobotAxisName axisName,
        AxisNodeHandles axisHandles,
        RobotNodeHandles robotHandles)
    {
        AxisNodePath path = AxisNodePaths[axisName];
        string axisBasePath = $"{AxisPathPrefix}/{path.AxisObjectName}";
        string motorBasePath = $"{MotorPathPrefix}/{path.MotorObjectName}";

        axisHandles.AxisObject = FindNode(importedNodes, instanceNamespaceIndex, axisBasePath, robotHandles, $"{path.AxisObjectName} object");
        axisHandles.PositionDegrees = FindVariable(importedNodes, instanceNamespaceIndex, $"{axisBasePath}/ActualPosition", robotHandles, $"{path.AxisObjectName} ActualPosition");
        axisHandles.VelocityDegreesPerSecond = FindVariable(importedNodes, instanceNamespaceIndex, $"{axisBasePath}/ActualSpeed", robotHandles, $"{path.AxisObjectName} ActualSpeed");
        axisHandles.TemperatureCelsius = FindVariable(importedNodes, instanceNamespaceIndex, $"{motorBasePath}/MotorTemperature", robotHandles, $"{path.MotorObjectName} MotorTemperature");

        // MinimalRealistic has ActualAcceleration, but RobotAxisState does not expose current per-axis acceleration yet.
        // It also does not define target-position or motor-load runtime variables, so those values are not bound yet.
        robotHandles.MissingExpectedNodes.Add($"{path.AxisObjectName} target position: no matching MinimalRealistic variable exists.");
        robotHandles.MissingExpectedNodes.Add($"{path.AxisObjectName} motor load: no matching MinimalRealistic variable exists.");
    }

    private static NodeState? FindNode(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string identifier,
        RobotNodeHandles handles,
        string label)
    {
        NodeId nodeId = new(identifier, instanceNamespaceIndex);
        NodeState? node = importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));

        if (node is null)
        {
            handles.MissingExpectedNodes.Add($"{label}: NodeId '{nodeId}' was not found.");
        }

        return node;
    }

    private static BaseVariableState? FindVariable(
        NodeStateCollection importedNodes,
        ushort instanceNamespaceIndex,
        string identifier,
        RobotNodeHandles handles,
        string label)
    {
        NodeId nodeId = new(identifier, instanceNamespaceIndex);
        NodeState? node = importedNodes.Find(candidate => Equals(candidate.NodeId, nodeId));

        if (node is BaseVariableState variable)
        {
            return variable;
        }

        string message = node is null
            ? $"{label}: NodeId '{nodeId}' was not found."
            : $"{label}: NodeId '{nodeId}' is not a variable node.";

        handles.MissingExpectedNodes.Add(message);
        return null;
    }

    private static ushort GetInstanceNamespaceIndex(ISystemContext context)
    {
        int namespaceIndex = context.NamespaceUris.GetIndex(NodeSetLoader.InstanceNamespaceUri);
        if (namespaceIndex < 0 || namespaceIndex > ushort.MaxValue)
        {
            throw new InvalidOperationException(
                $"The imported instance namespace '{NodeSetLoader.InstanceNamespaceUri}' is not registered in the server namespace table.");
        }

        return (ushort)namespaceIndex;
    }

    private static AxisNodeHandles GetAxisHandles(RobotNodeHandles handles, RobotAxisName axisName)
    {
        return axisName switch
        {
            RobotAxisName.S => handles.SAxis,
            RobotAxisName.L => handles.LAxis,
            RobotAxisName.U => handles.UAxis,
            RobotAxisName.R => handles.RAxis,
            RobotAxisName.B => handles.BAxis,
            RobotAxisName.T => handles.TAxis,
            _ => throw new ArgumentOutOfRangeException(nameof(axisName), axisName, "Unsupported robot axis.")
        };
    }

    private static void SetStringLikeValue(BaseVariableState? variable, string value, DateTime timestamp, ISystemContext context)
    {
        if (variable is null)
        {
            return;
        }

        object opcValue = Equals(variable.DataType, DataTypeIds.LocalizedText)
            ? new LocalizedText(value)
            : value;

        SetValue(variable, opcValue, timestamp, context);
    }

    private static void SetValue(BaseVariableState? variable, object value, DateTime timestamp, ISystemContext context)
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

    private sealed record AxisNodePath(string AxisObjectName, string MotorObjectName);
}
