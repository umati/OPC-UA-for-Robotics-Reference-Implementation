using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Discovery;

public sealed class RoboticsDiscoveryService(Session session)
{
    private static readonly string[] TaskControlMethodNames =
    [
        Opc.Ua.Robotics.BrowseNames.LoadByName,
        Opc.Ua.Robotics.BrowseNames.Start,
        Opc.Ua.Robotics.BrowseNames.Stop,
        Opc.Ua.Robotics.BrowseNames.UnloadProgram,
        Opc.Ua.Robotics.BrowseNames.ResetToProgramStart
    ];

    private static readonly string[] SystemOperationMethodNames =
    [
        Opc.Ua.Robotics.BrowseNames.GetReady,
        Opc.Ua.Robotics.BrowseNames.Start,
        Opc.Ua.Robotics.BrowseNames.Stop,
        Opc.Ua.Robotics.BrowseNames.StandDown
    ];

    private readonly RoboticsBrowseHelpers _browse = new(session);
    private readonly MethodMetadataDiscoveryService _methodMetadata = new(session, new RoboticsBrowseHelpers(session));

    public DiscoveryReport Discover(string endpointUrl)
    {
        int roboticsNamespaceIndex = session.NamespaceUris.GetIndex(Opc.Ua.Robotics.Namespaces.Robotics);
        if (roboticsNamespaceIndex < 0)
        {
            return new DiscoveryReport(endpointUrl, Connected: true, RoboticsNamespaceIndex: null, [], ["Robotics namespace is missing."]);
        }

        var types = RoboticsTypeDefinitions.FromLocalGeneratedConstants(session.NamespaceUris);
        IReadOnlyList<MotionDeviceSystemReport> systems = DiscoverMotionDeviceSystems(types);

        return new DiscoveryReport(
            endpointUrl,
            Connected: true,
            RoboticsNamespaceIndex: roboticsNamespaceIndex,
            systems,
            []);
    }

    private IReadOnlyList<MotionDeviceSystemReport> DiscoverMotionDeviceSystems(RoboticsTypeDefinitions types)
    {
        // Implementation decision: discover systems below Objects by walking hierarchical references and proving
        // MotionDeviceSystemType through HasTypeDefinition/HasSubtype compatibility.
        var systems = new List<MotionDeviceSystemReport>();
        var visited = new HashSet<NodeId>();
        var queue = new Queue<NodeId>();
        queue.Enqueue(ObjectIds.ObjectsFolder);

        while (queue.Count > 0)
        {
            NodeId current = queue.Dequeue();
            if (!visited.Add(current))
            {
                continue;
            }

            foreach (ReferenceDescription reference in _browse.BrowseForward(
                         current,
                         ReferenceTypeIds.HierarchicalReferences,
                         includeSubtypes: true,
                         NodeClass.Object))
            {
                NodeId? nodeId = _browse.ToNodeId(reference.NodeId);
                if (nodeId is null)
                {
                    continue;
                }

                NodeId? typeDefinition = _browse.GetTypeDefinition(nodeId);
                if (_browse.IsTypeDefinitionOrSubtypeOf(typeDefinition, types.MotionDeviceSystemType))
                {
                    systems.Add(DiscoverMotionDeviceSystem(reference, nodeId, types));
                    continue;
                }

                queue.Enqueue(nodeId);
            }
        }

        return systems;
    }

    private MotionDeviceSystemReport DiscoverMotionDeviceSystem(
        ReferenceDescription systemReference,
        NodeId systemNodeId,
        RoboticsTypeDefinitions types)
    {
        ReferenceDescription? controllersFolder = _browse.FindQualifiedChild(
            systemNodeId,
            Opc.Ua.Robotics.BrowseNames.Controllers,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        ReferenceDescription? motionDevicesFolder = _browse.FindQualifiedChild(
            systemNodeId,
            Opc.Ua.Robotics.BrowseNames.MotionDevices,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        IReadOnlyList<ControllerReport> controllers = controllersFolder is null
            ? []
            : DiscoverControllers(controllersFolder, types);

        IReadOnlyList<MotionDeviceReport> motionDevices = motionDevicesFolder is null
            ? []
            : DiscoverMotionDevices(motionDevicesFolder, types);

        return new MotionDeviceSystemReport(
            _browse.ToNodeInfo(systemReference),
            controllersFolder is null ? null : _browse.ToNodeInfo(controllersFolder),
            motionDevicesFolder is null ? null : _browse.ToNodeInfo(motionDevicesFolder),
            controllers,
            motionDevices);
    }

    private IReadOnlyList<ControllerReport> DiscoverControllers(ReferenceDescription controllersFolder, RoboticsTypeDefinitions types)
    {
        NodeId? folderNodeId = _browse.ToNodeId(controllersFolder.NodeId);
        if (folderNodeId is null)
        {
            return [];
        }

        return _browse.FindChildrenByType(folderNodeId, ReferenceTypeIds.HasComponent, types.ControllerType)
            .Select(reference => DiscoverController(reference, types))
            .ToArray();
    }

    private ControllerReport DiscoverController(ReferenceDescription controllerReference, RoboticsTypeDefinitions types)
    {
        NodeId? controllerNodeId = _browse.ToNodeId(controllerReference.NodeId);
        if (controllerNodeId is null)
        {
            return new ControllerReport(_browse.ToNodeInfo(controllerReference), null, [], null);
        }

        ReferenceDescription? taskControlsFolder = _browse.FindQualifiedChild(
            controllerNodeId,
            Opc.Ua.Robotics.BrowseNames.TaskControls,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        IReadOnlyList<TaskControlReport> taskControls = taskControlsFolder is null
            ? []
            : DiscoverTaskControls(taskControlsFolder, types);

        OperationReport? systemOperation = DiscoverSystemOperation(controllerNodeId, types);

        return new ControllerReport(
            _browse.ToNodeInfo(controllerReference),
            taskControlsFolder is null ? null : _browse.ToNodeInfo(taskControlsFolder),
            taskControls,
            systemOperation);
    }

    private OperationReport? DiscoverSystemOperation(NodeId controllerNodeId, RoboticsTypeDefinitions types)
    {
        ReferenceDescription? systemOperation = _browse.FindQualifiedChild(
            controllerNodeId,
            Opc.Ua.Robotics.BrowseNames.SystemOperation,
            ReferenceTypeIds.HasAddIn,
            NodeClass.Object);

        if (systemOperation is null)
        {
            return null;
        }

        NodeId? nodeId = _browse.ToNodeId(systemOperation.NodeId);
        if (nodeId is null)
        {
            return new OperationReport(_browse.ToNodeInfo(systemOperation), null, IsExpectedType: false, []);
        }

        bool isExpectedType = _browse.IsTypeDefinitionOrSubtypeOf(_browse.GetTypeDefinition(nodeId), types.SystemOperationType);
        IReadOnlyList<MethodReport> methods = DiscoverOperationMethods(
            nodeId,
            Opc.Ua.Robotics.BrowseNames.SystemOperationStateMachine,
            SystemOperationMethodNames);

        return new OperationReport(_browse.ToNodeInfo(systemOperation), null, isExpectedType, methods);
    }

    private IReadOnlyList<TaskControlReport> DiscoverTaskControls(ReferenceDescription taskControlsFolder, RoboticsTypeDefinitions types)
    {
        NodeId? folderNodeId = _browse.ToNodeId(taskControlsFolder.NodeId);
        if (folderNodeId is null)
        {
            return [];
        }

        return _browse.FindChildrenByType(folderNodeId, ReferenceTypeIds.HasComponent, types.TaskControlType)
            .Select(reference => DiscoverTaskControl(reference, types))
            .ToArray();
    }

    private TaskControlReport DiscoverTaskControl(ReferenceDescription taskControlReference, RoboticsTypeDefinitions types)
    {
        NodeId? taskControlNodeId = _browse.ToNodeId(taskControlReference.NodeId);
        if (taskControlNodeId is null)
        {
            return new TaskControlReport(_browse.ToNodeInfo(taskControlReference), null, []);
        }

        ReferenceDescription? operation = _browse.FindQualifiedChild(
            taskControlNodeId,
            Opc.Ua.Robotics.BrowseNames.TaskControlOperation,
            ReferenceTypeIds.HasAddIn,
            NodeClass.Object);

        if (operation is null)
        {
            return new TaskControlReport(_browse.ToNodeInfo(taskControlReference), null, []);
        }

        NodeId? operationNodeId = _browse.ToNodeId(operation.NodeId);
        if (operationNodeId is null)
        {
            return new TaskControlReport(_browse.ToNodeInfo(taskControlReference), _browse.ToNodeInfo(operation), []);
        }

        bool isExpectedType = _browse.IsTypeDefinitionOrSubtypeOf(_browse.GetTypeDefinition(operationNodeId), types.TaskControlOperationType);
        IReadOnlyList<MethodReport> methods = DiscoverOperationMethods(
            operationNodeId,
            Opc.Ua.Robotics.BrowseNames.TaskControlStateMachine,
            TaskControlMethodNames);

        return new TaskControlReport(
            _browse.ToNodeInfo(taskControlReference),
            _browse.ToNodeInfo(operation) with { Evidence = isExpectedType ? "HasAddIn + TaskControlOperationType" : "HasAddIn + unresolved/foreign type" },
            methods);
    }

    private IReadOnlyList<MethodReport> DiscoverOperationMethods(
        NodeId operationNodeId,
        string stateMachineBrowseName,
        IReadOnlyList<string> expectedMethodNames)
    {
        ReferenceDescription? stateMachine = _browse.FindQualifiedChild(
            operationNodeId,
            stateMachineBrowseName,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        if (stateMachine is null)
        {
            return expectedMethodNames
                .Select(name => _methodMetadata.CreateMissingMethodReport(name, "state machine unresolved"))
                .ToArray();
        }

        NodeId? stateMachineNodeId = _browse.ToNodeId(stateMachine.NodeId);
        if (stateMachineNodeId is null)
        {
            return expectedMethodNames
                .Select(name => _methodMetadata.CreateMissingMethodReport(name, "state machine NodeId unresolved"))
                .ToArray();
        }

        var directMethods = _browse.FindMethods(stateMachineNodeId, expectedMethodNames)
            .ToDictionary(reference => reference.BrowseName.Name, reference => reference, StringComparer.Ordinal);

        var methodReports = new List<MethodReport>();
        foreach (string expectedMethodName in expectedMethodNames)
        {
            if (directMethods.TryGetValue(expectedMethodName, out ReferenceDescription? method))
            {
                methodReports.Add(_methodMetadata.Discover(
                    expectedMethodName,
                    method,
                    stateMachineNodeId,
                    "namespace-qualified method browse under operation state machine"));
                continue;
            }

            // Local NodeSet/generated-code truth: ResetToProgramStart is hosted under ReadySubstateMachine
            // in this repository's generated Robotics model/instance NodeSet.
            // Implementation decision: use this documented local fallback when direct state-machine browse fails.
            if (expectedMethodName == Opc.Ua.Robotics.BrowseNames.ResetToProgramStart)
            {
                MethodReport fallback = DiscoverReadySubstateMethod(stateMachineNodeId, expectedMethodName);
                methodReports.Add(fallback);
                continue;
            }

            methodReports.Add(_methodMetadata.CreateMissingMethodReport(expectedMethodName, "not found by qualified method browse"));
        }

        return methodReports;
    }

    private MethodReport DiscoverReadySubstateMethod(NodeId stateMachineNodeId, string methodName)
    {
        ReferenceDescription? readySubstate = _browse.FindQualifiedChild(
            stateMachineNodeId,
            "ReadySubstateMachine",
            ReferenceTypeIds.HasSubStateMachine,
            NodeClass.Object);

        if (readySubstate is null)
        {
            readySubstate = _browse.FindQualifiedChild(
                stateMachineNodeId,
                "ReadySubstateMachine",
                ReferenceTypeIds.HasComponent,
                NodeClass.Object);
        }

        NodeId? readySubstateNodeId = readySubstate is null ? null : _browse.ToNodeId(readySubstate.NodeId);
        if (readySubstateNodeId is null)
        {
            return _methodMetadata.CreateMissingMethodReport(methodName, "ReadySubstateMachine fallback unresolved");
        }

        ReferenceDescription? method = _browse.FindMethods(readySubstateNodeId, [methodName]).FirstOrDefault();
        if (method is null)
        {
            return _methodMetadata.CreateMissingMethodReport(methodName, "not found under ReadySubstateMachine fallback");
        }

        return _methodMetadata.Discover(
            methodName,
            method,
            readySubstateNodeId,
            "local NodeSet fallback: ReadySubstateMachine qualified method browse");
    }

    private IReadOnlyList<MotionDeviceReport> DiscoverMotionDevices(ReferenceDescription motionDevicesFolder, RoboticsTypeDefinitions types)
    {
        NodeId? folderNodeId = _browse.ToNodeId(motionDevicesFolder.NodeId);
        if (folderNodeId is null)
        {
            return [];
        }

        return _browse.FindChildrenByType(folderNodeId, ReferenceTypeIds.HasComponent, types.MotionDeviceType)
            .Select(reference => DiscoverMotionDevice(reference, types))
            .ToArray();
    }

    private MotionDeviceReport DiscoverMotionDevice(ReferenceDescription motionDeviceReference, RoboticsTypeDefinitions types)
    {
        NodeId? motionDeviceNodeId = _browse.ToNodeId(motionDeviceReference.NodeId);
        if (motionDeviceNodeId is null)
        {
            return new MotionDeviceReport(_browse.ToNodeInfo(motionDeviceReference), null, null, [], []);
        }

        ReferenceDescription? axesFolder = _browse.FindQualifiedChild(
            motionDeviceNodeId,
            Opc.Ua.Robotics.BrowseNames.Axes,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        ReferenceDescription? powerTrainsFolder = _browse.FindQualifiedChild(
            motionDeviceNodeId,
            Opc.Ua.Robotics.BrowseNames.PowerTrains,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        IReadOnlyList<NodeDiscoveryInfo> axes = axesFolder is null
            ? []
            : DiscoverTypedChildren(axesFolder, types.AxisType);

        IReadOnlyList<PowerTrainReport> powerTrains = powerTrainsFolder is null
            ? []
            : DiscoverPowerTrains(powerTrainsFolder, types);

        return new MotionDeviceReport(
            _browse.ToNodeInfo(motionDeviceReference),
            axesFolder is null ? null : _browse.ToNodeInfo(axesFolder),
            powerTrainsFolder is null ? null : _browse.ToNodeInfo(powerTrainsFolder),
            axes,
            powerTrains);
    }

    private IReadOnlyList<PowerTrainReport> DiscoverPowerTrains(ReferenceDescription folderReference, RoboticsTypeDefinitions types)
    {
        return DiscoverTypedChildren(folderReference, types.PowerTrainType)
            .Select(powerTrain =>
            {
                NodeId? powerTrainNodeId = NodeId.Parse(powerTrain.NodeId);
                IReadOnlyList<MotorReport> motors = _browse.BrowseForward(
                        powerTrainNodeId,
                        ReferenceTypeIds.HasComponent,
                        includeSubtypes: true,
                        NodeClass.Object)
                    .Where(reference => _browse.IsTypeDefinitionOrSubtypeOf(_browse.GetTypeDefinition(_browse.ToNodeId(reference.NodeId)!), types.MotorType))
                    .Select(reference => new MotorReport(_browse.ToNodeInfo(reference)))
                    .ToArray();
                return new PowerTrainReport(powerTrain, motors);
            })
            .ToArray();
    }

    private IReadOnlyList<NodeDiscoveryInfo> DiscoverTypedChildren(ReferenceDescription folderReference, NodeId expectedType)
    {
        NodeId? folderNodeId = _browse.ToNodeId(folderReference.NodeId);
        if (folderNodeId is null)
        {
            return [];
        }

        return _browse.FindChildrenByType(folderNodeId, ReferenceTypeIds.HasComponent, expectedType)
            .Select(_browse.ToNodeInfo)
            .ToArray();
    }
}
