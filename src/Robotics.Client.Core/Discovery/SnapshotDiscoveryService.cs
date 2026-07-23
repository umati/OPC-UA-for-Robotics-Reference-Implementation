using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Discovery;

public sealed class SnapshotDiscoveryService(Session session)
{
    private const string LockedBrowseName = "Locked";
    private const string RemainingLockTimeBrowseName = "RemainingLockTime";
    private const string ComponentNameBrowseName = "ComponentName";
    private const string ManufacturerBrowseName = "Manufacturer";
    private const string ModelBrowseName = "Model";
    private const string ProductCodeBrowseName = "ProductCode";
    private const string SerialNumberBrowseName = "SerialNumber";
    private const string CurrentStateBrowseName = "CurrentState";
    private const string LastTransitionBrowseName = "LastTransition";

    private static readonly string[] StateMachineVariableNames =
    [
        CurrentStateBrowseName,
        LastTransitionBrowseName,
        Opc.Ua.Robotics.BrowseNames.LastTransitionReason
    ];

    private static readonly string[] EquipmentVariableNames =
    [
        Opc.Ua.Robotics.BrowseNames.ActualPosition,
        Opc.Ua.Robotics.BrowseNames.ActualSpeed,
        Opc.Ua.Robotics.BrowseNames.ActualAcceleration,
        Opc.Ua.Robotics.BrowseNames.SpeedOverride,
        Opc.Ua.Robotics.BrowseNames.MotorTemperature,
        Opc.Ua.Robotics.BrowseNames.MotionDeviceCategory,
        Opc.Ua.Robotics.BrowseNames.MotionProfile,
        Opc.Ua.Robotics.BrowseNames.TotalEnergyConsumption,
        Opc.Ua.Robotics.BrowseNames.TotalPowerOnTime,
        LockedBrowseName,
        RemainingLockTimeBrowseName,
        ComponentNameBrowseName,
        ManufacturerBrowseName,
        ModelBrowseName,
        ProductCodeBrowseName,
        SerialNumberBrowseName
    ];

    private static readonly string[] MotionDeviceIdentityVariableNames =
    [
        ComponentNameBrowseName,
        ManufacturerBrowseName,
        ModelBrowseName,
        ProductCodeBrowseName,
        SerialNumberBrowseName
    ];

    private static readonly string[] AxisVariableNames =
    [
        Opc.Ua.Robotics.BrowseNames.ActualPosition,
        Opc.Ua.Robotics.BrowseNames.ActualSpeed,
        Opc.Ua.Robotics.BrowseNames.ActualAcceleration
    ];

    private static readonly string[] ControllerRuntimeVariableNames =
    [
        "UnderControl",
        "OperationalMode",
        "ProtectiveStop",
        "EmergencyStop"
    ];

    private static readonly string[] TaskRuntimeVariableNames =
    [
        "TaskProgramName",
        "TaskProgramLoaded"
    ];

    private readonly RoboticsBrowseHelpers _browse = new(session);

    public IReadOnlyList<SnapshotNode> Discover(DiscoveryReport report)
    {
        var nodes = new List<SnapshotNode>();

        foreach (MotionDeviceSystemReport system in report.MotionDeviceSystems)
        {
            nodes.AddRange(DiscoverSafetyRuntime(system));

            foreach (ControllerReport controller in system.Controllers)
            {
                if (controller.SystemOperation is not null)
                {
                    nodes.AddRange(DiscoverSystemOperation(controller.SystemOperation));
                }

                nodes.AddRange(DiscoverControllerRuntime(controller));

                foreach (TaskControlReport taskControl in controller.TaskControls)
                {
                    nodes.AddRange(DiscoverTaskControlOperation(taskControl));
                    nodes.AddRange(DiscoverTaskRuntime(taskControl));
                }
            }

            nodes.AddRange(DiscoverEquipment(system));
        }

        return Deduplicate(nodes);
    }

    private IReadOnlyList<SnapshotNode> DiscoverSafetyRuntime(MotionDeviceSystemReport system)
    {
        if (!TryParseNodeId(system.Node.NodeId, out NodeId? systemNodeId))
        {
            return [];
        }

        return FindVariablesByBrowseName(systemNodeId, ControllerRuntimeVariableNames, maxDepth: 5)
            .Select(variable => CreateSnapshotNode("SystemOperation", $"{system.Node.BrowseName}.{variable.BrowseName.Name}", variable))
            .ToArray();
    }

    private IReadOnlyList<SnapshotNode> DiscoverSystemOperation(OperationReport operation)
    {
        if (!TryParseNodeId(operation.Node.NodeId, out NodeId? operationNodeId))
        {
            return [];
        }

        ReferenceDescription? stateMachine = _browse.FindQualifiedChild(
            operationNodeId,
            Opc.Ua.Robotics.BrowseNames.SystemOperationStateMachine,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        if (stateMachine is null || _browse.ToNodeId(stateMachine.NodeId) is not { } stateMachineNodeId)
        {
            return [];
        }

        return DiscoverStateMachineValues(
            "SystemOperation",
            "SystemOperationStateMachine",
            stateMachineNodeId,
            substateMachineNames: ["IdleSubstateMachine", "ExecutingSubstateMachine"]);
    }

    private IReadOnlyList<SnapshotNode> DiscoverTaskControlOperation(TaskControlReport taskControl)
    {
        if (taskControl.TaskControlOperation is null ||
            !TryParseNodeId(taskControl.TaskControlOperation.NodeId, out NodeId? operationNodeId))
        {
            return [];
        }

        ReferenceDescription? stateMachine = _browse.FindQualifiedChild(
            operationNodeId,
            Opc.Ua.Robotics.BrowseNames.TaskControlStateMachine,
            ReferenceTypeIds.HasComponent,
            NodeClass.Object);

        if (stateMachine is null || _browse.ToNodeId(stateMachine.NodeId) is not { } stateMachineNodeId)
        {
            return [];
        }

        return DiscoverStateMachineValues(
            "TaskControlOperation",
            "TaskControlStateMachine",
            stateMachineNodeId,
            substateMachineNames: ["ReadySubstateMachine"]);
    }

    private IReadOnlyList<SnapshotNode> DiscoverControllerRuntime(ControllerReport controller)
    {
        if (!TryParseNodeId(controller.Node.NodeId, out NodeId? controllerNodeId))
        {
            return [];
        }

        return FindVariablesByBrowseName(controllerNodeId, ControllerRuntimeVariableNames, maxDepth: 4)
            .Select(variable => CreateSnapshotNode("SystemOperation", $"{controller.Node.BrowseName}.{variable.BrowseName.Name}", variable))
            .ToArray();
    }

    private IReadOnlyList<SnapshotNode> DiscoverTaskRuntime(TaskControlReport taskControl)
    {
        if (!TryParseNodeId(taskControl.Node.NodeId, out NodeId? taskControlNodeId))
        {
            return [];
        }

        return FindVariablesByBrowseName(taskControlNodeId, TaskRuntimeVariableNames, maxDepth: 2)
            .Select(variable => CreateSnapshotNode("TaskControlOperation", $"{taskControl.Node.BrowseName}.{variable.BrowseName.Name}", variable))
            .ToArray();
    }

    private IReadOnlyList<SnapshotNode> DiscoverStateMachineValues(
        string sectionName,
        string labelPrefix,
        NodeId stateMachineNodeId,
        IReadOnlyList<string> substateMachineNames)
    {
        var nodes = new List<SnapshotNode>();

        foreach (ReferenceDescription variable in FindCuratedVariables(stateMachineNodeId, StateMachineVariableNames))
        {
            AddStateVariableWithProperties(nodes, sectionName, labelPrefix, variable);
        }

        foreach (string substateMachineName in substateMachineNames)
        {
            ReferenceDescription? readySubstate = _browse.FindQualifiedChild(
                stateMachineNodeId,
                substateMachineName,
                ReferenceTypeIds.HasSubStateMachine,
                NodeClass.Object);

            readySubstate ??= _browse.FindQualifiedChild(
                stateMachineNodeId,
                substateMachineName,
                ReferenceTypeIds.HasComponent,
                NodeClass.Object);

            if (readySubstate is not null && _browse.ToNodeId(readySubstate.NodeId) is { } readySubstateNodeId)
            {
                foreach (ReferenceDescription variable in FindCuratedVariables(readySubstateNodeId, StateMachineVariableNames))
                {
                    AddStateVariableWithProperties(nodes, sectionName, substateMachineName, variable);
                }
            }
        }

        return nodes;
    }

    private void AddStateVariableWithProperties(
        List<SnapshotNode> nodes,
        string sectionName,
        string labelPrefix,
        ReferenceDescription variable)
    {
        AddVariable(nodes, sectionName, $"{labelPrefix}.{variable.BrowseName.Name}", variable, heuristic: false);
        if (_browse.ToNodeId(variable.NodeId) is not { } variableNodeId)
        {
            return;
        }

        foreach (ReferenceDescription property in _browse.BrowseForward(
                     variableNodeId,
                     ReferenceTypeIds.HasProperty,
                     includeSubtypes: true,
                     NodeClass.Variable))
        {
            AddVariable(nodes, sectionName, $"{labelPrefix}.{variable.BrowseName.Name}.{property.BrowseName.Name}", property, heuristic: false);
        }
    }

    private IReadOnlyList<SnapshotNode> DiscoverEquipment(MotionDeviceSystemReport system)
    {
        var nodes = new List<SnapshotNode>();

        foreach (MotionDeviceReport motionDevice in system.MotionDevices)
        {
            AddCuratedEquipmentVariables(nodes, "MotionDevice", motionDevice.Node.NodeId, motionDevice.Node.BrowseName, MotionDeviceIdentityVariableNames, parameterSetPath: false);

            foreach (NodeDiscoveryInfo axis in motionDevice.Axes)
            {
                AddAxisEquipmentVariables(nodes, motionDevice.Node, axis, $"{motionDevice.Node.BrowseName}.Axes.{axis.BrowseName}");
            }

            foreach (PowerTrainReport powerTrain in motionDevice.PowerTrains)
            {
                foreach (MotorReport motor in powerTrain.Motors)
                {
                    AddCuratedEquipmentVariables(nodes, "PowerTrains", motor.Node.NodeId, $"{motionDevice.Node.BrowseName}.PowerTrains.{powerTrain.Node.BrowseName}.Motors.{motor.Node.BrowseName}", [Opc.Ua.Robotics.BrowseNames.MotorTemperature], parameterSetPath: true);
                }
            }
        }

        return nodes;
    }

    private void AddAxisEquipmentVariables(
        List<SnapshotNode> nodes,
        NodeDiscoveryInfo motionDevice,
        NodeDiscoveryInfo axis,
        string rootBrowseName)
    {
        if (!TryParseNodeId(axis.NodeId, out NodeId? axisNodeId))
        {
            return;
        }

        // ActualPosition is owned by the discovered AxisType instance through its
        // ParameterSet. Resolve both references from the live address space and
        // retain the returned variable NodeId; never derive a child NodeId from text.
        ReferenceDescription[] parameterSets = _browse.BrowseForward(
                axisNodeId,
                ReferenceTypeIds.HierarchicalReferences,
                includeSubtypes: true,
                NodeClass.Object)
            .Where(reference => reference.BrowseName.Name == "ParameterSet")
            .Take(2)
            .ToArray();

        if (parameterSets.Length != 1 || _browse.ToNodeId(parameterSets[0].NodeId) is not { } parameterSetNodeId)
        {
            return;
        }

        ushort roboticsNamespaceIndex = GetRoboticsNamespaceIndex();
        foreach (ReferenceDescription variable in _browse.BrowseForward(
                     parameterSetNodeId,
                     ReferenceTypeIds.HierarchicalReferences,
                     includeSubtypes: true,
                     NodeClass.Variable)
                 .Where(reference => IsBrowseNameMatch(reference.BrowseName, AxisVariableNames, roboticsNamespaceIndex)))
        {
            AddVariable(
                nodes,
                "Axes",
                $"{rootBrowseName}.ParameterSet.{variable.BrowseName.Name}",
                variable,
                heuristic: false,
                motionDeviceKey: motionDevice.StableKey,
                axisKey: axis.StableKey);
        }
    }

    private void AddCuratedEquipmentVariables(
        List<SnapshotNode> nodes,
        string sectionName,
        string rootNodeIdText,
        string rootBrowseName,
        IReadOnlyCollection<string> variableNames,
        bool parameterSetPath)
    {
        if (!TryParseNodeId(rootNodeIdText, out NodeId? rootNodeId))
        {
            return;
        }

        foreach (ReferenceDescription variable in FindVariablesByBrowseName(rootNodeId, variableNames, maxDepth: 3))
        {
            string label = parameterSetPath
                ? $"{rootBrowseName}.ParameterSet.{variable.BrowseName.Name}"
                : $"{rootBrowseName}.{variable.BrowseName.Name}";
            AddVariable(nodes, sectionName, label, variable, heuristic: true);
        }
    }

    private IReadOnlyList<ReferenceDescription> FindCuratedVariables(NodeId parentNodeId, IReadOnlyCollection<string> browseNames)
    {
        ushort roboticsNamespaceIndex = GetRoboticsNamespaceIndex();
        return _browse.BrowseForward(
                parentNodeId,
                ReferenceTypeIds.HierarchicalReferences,
                includeSubtypes: true,
                NodeClass.Variable)
            .Where(reference => IsBrowseNameMatch(reference.BrowseName, browseNames, roboticsNamespaceIndex))
            .ToArray();
    }

    private IReadOnlyList<ReferenceDescription> FindVariablesByBrowseName(
        NodeId rootNodeId,
        IReadOnlyCollection<string> browseNames,
        int maxDepth)
    {
        ushort roboticsNamespaceIndex = GetRoboticsNamespaceIndex();
        var matches = new List<ReferenceDescription>();
        var visited = new HashSet<NodeId>();
        var queue = new Queue<(NodeId NodeId, int Depth)>();
        queue.Enqueue((rootNodeId, 0));

        while (queue.Count > 0)
        {
            (NodeId current, int depth) = queue.Dequeue();
            if (!visited.Add(current) || depth > maxDepth)
            {
                continue;
            }

            foreach (ReferenceDescription variable in _browse.BrowseForward(
                         current,
                         ReferenceTypeIds.HierarchicalReferences,
                         includeSubtypes: true,
                         NodeClass.Variable))
            {
                if (IsBrowseNameMatch(variable.BrowseName, browseNames, roboticsNamespaceIndex))
                {
                    matches.Add(variable);
                }
            }

            if (depth == maxDepth)
            {
                continue;
            }

            foreach (ReferenceDescription child in _browse.BrowseForward(
                         current,
                         ReferenceTypeIds.HierarchicalReferences,
                         includeSubtypes: true,
                         NodeClass.Object | NodeClass.Variable))
            {
                if (_browse.ToNodeId(child.NodeId) is { } childNodeId)
                {
                    queue.Enqueue((childNodeId, depth + 1));
                }
            }
        }

        return matches;
    }

    private void AddVariable(
        List<SnapshotNode> nodes,
        string sectionName,
        string label,
        ReferenceDescription reference,
        bool heuristic,
        string? motionDeviceKey = null,
        string? axisKey = null)
    {
        if (_browse.ToNodeId(reference.NodeId) is not { } nodeId)
        {
            return;
        }

        nodes.Add(CreateSnapshotNode(sectionName, label, reference, heuristic, motionDeviceKey, axisKey));
    }

    private SnapshotNode CreateSnapshotNode(
        string sectionName,
        string label,
        ReferenceDescription reference,
        bool heuristic = false,
        string? motionDeviceKey = null,
        string? axisKey = null)
    {
        if (_browse.ToNodeId(reference.NodeId) is not { } nodeId)
        {
            throw new InvalidOperationException($"Runtime discovery returned an unresolvable NodeId for '{label}'.");
        }

        RuntimeIdentity identity = RuntimeIdentity.From(nodeId, session.NamespaceUris);
        return new SnapshotNode(
            sectionName,
            label,
            RoboticsBrowseHelpers.FormatBrowseName(reference.BrowseName),
            nodeId,
            heuristic,
            identity.StableKey,
            motionDeviceKey,
            axisKey);
    }

    private static IReadOnlyList<SnapshotNode> Deduplicate(IReadOnlyList<SnapshotNode> nodes)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var deduplicated = new List<SnapshotNode>();
        foreach (SnapshotNode node in nodes)
        {
            string key = $"{node.SectionName}|{node.NodeId}";
            if (seen.Add(key))
            {
                deduplicated.Add(node);
            }
        }

        return deduplicated;
    }

    private static bool IsBrowseNameMatch(QualifiedName browseName, IReadOnlyCollection<string> names, ushort roboticsNamespaceIndex)
    {
        // The curated name is exact and the search is already bounded to a proven
        // MotionDevice, Axis, Motor, Controller, or state-machine owner. Identity
        // properties in this repository use the DI namespace, while Robotics
        // runtime variables use the Robotics namespace, so namespace 3 alone would
        // incorrectly discard valid DI properties.
        return names.Contains(browseName.Name, StringComparer.Ordinal);
    }

    private ushort GetRoboticsNamespaceIndex()
    {
        int namespaceIndex = session.NamespaceUris.GetIndex(Opc.Ua.Robotics.Namespaces.Robotics);
        if (namespaceIndex < 0)
        {
            throw new InvalidOperationException("Robotics namespace is missing from the connected server namespace table.");
        }

        return (ushort)namespaceIndex;
    }

    private static bool TryParseNodeId(string text, out NodeId? nodeId)
    {
        try
        {
            nodeId = NodeId.Parse(text);
            return true;
        }
        catch (FormatException)
        {
            nodeId = null;
            return false;
        }
    }
}
