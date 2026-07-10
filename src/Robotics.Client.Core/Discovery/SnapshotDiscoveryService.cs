using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Discovery;

public sealed class SnapshotDiscoveryService(Session session)
{
    private const int MaxEquipmentValuesPerSection = 12;
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

    private readonly RoboticsBrowseHelpers _browse = new(session);

    public IReadOnlyList<SnapshotNode> Discover(DiscoveryReport report)
    {
        var nodes = new List<SnapshotNode>();

        foreach (MotionDeviceSystemReport system in report.MotionDeviceSystems)
        {
            foreach (ControllerReport controller in system.Controllers)
            {
                if (controller.SystemOperation is not null)
                {
                    nodes.AddRange(DiscoverSystemOperation(controller.SystemOperation));
                }

                foreach (TaskControlReport taskControl in controller.TaskControls)
                {
                    nodes.AddRange(DiscoverTaskControlOperation(taskControl));
                }
            }

            nodes.AddRange(DiscoverEquipment(system));
        }

        return Deduplicate(nodes);
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
            includeReadySubstate: false);
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
            includeReadySubstate: true);
    }

    private IReadOnlyList<SnapshotNode> DiscoverStateMachineValues(
        string sectionName,
        string labelPrefix,
        NodeId stateMachineNodeId,
        bool includeReadySubstate)
    {
        var nodes = new List<SnapshotNode>();

        foreach (ReferenceDescription variable in FindCuratedVariables(stateMachineNodeId, StateMachineVariableNames))
        {
            AddStateVariableWithProperties(nodes, sectionName, labelPrefix, variable);
        }

        if (includeReadySubstate)
        {
            ReferenceDescription? readySubstate = _browse.FindQualifiedChild(
                stateMachineNodeId,
                Opc.Ua.Robotics.BrowseNames.ReadySubstateMachine,
                ReferenceTypeIds.HasSubStateMachine,
                NodeClass.Object);

            readySubstate ??= _browse.FindQualifiedChild(
                stateMachineNodeId,
                Opc.Ua.Robotics.BrowseNames.ReadySubstateMachine,
                ReferenceTypeIds.HasComponent,
                NodeClass.Object);

            if (readySubstate is not null && _browse.ToNodeId(readySubstate.NodeId) is { } readySubstateNodeId)
            {
                foreach (ReferenceDescription variable in FindCuratedVariables(readySubstateNodeId, StateMachineVariableNames))
                {
                    AddStateVariableWithProperties(nodes, sectionName, "ReadySubstateMachine", variable);
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
            AddCuratedEquipmentVariables(nodes, "MotionDevice", motionDevice.Node.NodeId, motionDevice.Node.BrowseName);

            foreach (NodeDiscoveryInfo axis in motionDevice.Axes)
            {
                AddCuratedEquipmentVariables(nodes, "Axes", axis.NodeId, axis.BrowseName);
            }

            foreach (NodeDiscoveryInfo powerTrain in motionDevice.PowerTrains)
            {
                AddCuratedEquipmentVariables(nodes, "PowerTrains", powerTrain.NodeId, powerTrain.BrowseName);
            }
        }

        return nodes
            .GroupBy(node => node.SectionName, StringComparer.Ordinal)
            .SelectMany(group => group.Take(MaxEquipmentValuesPerSection))
            .ToArray();
    }

    private void AddCuratedEquipmentVariables(
        List<SnapshotNode> nodes,
        string sectionName,
        string rootNodeIdText,
        string rootBrowseName)
    {
        if (!TryParseNodeId(rootNodeIdText, out NodeId? rootNodeId))
        {
            return;
        }

        foreach (ReferenceDescription variable in FindVariablesByBrowseName(rootNodeId, EquipmentVariableNames, maxDepth: 3)
                     .Take(MaxEquipmentValuesPerSection))
        {
            AddVariable(nodes, sectionName, $"{rootBrowseName}.{variable.BrowseName.Name}", variable, heuristic: true);
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

        while (queue.Count > 0 && matches.Count < MaxEquipmentValuesPerSection)
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
        bool heuristic)
    {
        if (_browse.ToNodeId(reference.NodeId) is not { } nodeId)
        {
            return;
        }

        nodes.Add(new SnapshotNode(
            sectionName,
            label,
            RoboticsBrowseHelpers.FormatBrowseName(reference.BrowseName),
            nodeId,
            heuristic));
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
        return names.Contains(browseName.Name) &&
               (browseName.NamespaceIndex == 0 || browseName.NamespaceIndex == roboticsNamespaceIndex);
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
