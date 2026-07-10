using Opc.Ua;

namespace Robotics.Client.Core.Discovery;

public sealed class RoboticsTypeDefinitions
{
    private RoboticsTypeDefinitions(
        NodeId motionDeviceSystemType,
        NodeId controllerType,
        NodeId motionDeviceType,
        NodeId axisType,
        NodeId powerTrainType,
        NodeId taskControlType,
        NodeId taskControlOperationType,
        NodeId systemOperationType)
    {
        MotionDeviceSystemType = motionDeviceSystemType;
        ControllerType = controllerType;
        MotionDeviceType = motionDeviceType;
        AxisType = axisType;
        PowerTrainType = powerTrainType;
        TaskControlType = taskControlType;
        TaskControlOperationType = taskControlOperationType;
        SystemOperationType = systemOperationType;
    }

    public NodeId MotionDeviceSystemType { get; }

    public NodeId ControllerType { get; }

    public NodeId MotionDeviceType { get; }

    public NodeId AxisType { get; }

    public NodeId PowerTrainType { get; }

    public NodeId TaskControlType { get; }

    public NodeId TaskControlOperationType { get; }

    public NodeId SystemOperationType { get; }

    public static RoboticsTypeDefinitions FromLocalGeneratedConstants(NamespaceTable namespaceUris)
    {
        // Official specification truth: Robotics defines these ObjectTypes in the Robotics namespace.
        // Local NodeSet/generated-code truth: the numeric identifiers below come from the generated
        // Opc.Ua.Robotics.Constants.cs checked into this repository.
        // Implementation decision: create runtime NodeIds using the connected server's namespace table.
        return new RoboticsTypeDefinitions(
            Create(Opc.Ua.Robotics.ObjectTypes.MotionDeviceSystemType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.ControllerType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.MotionDeviceType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.AxisType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.PowerTrainType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.TaskControlType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.TaskControlOperationType, namespaceUris),
            Create(Opc.Ua.Robotics.ObjectTypes.SystemOperationType, namespaceUris));
    }

    private static NodeId Create(uint identifier, NamespaceTable namespaceUris)
    {
        return NodeId.Create(identifier, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
    }
}
