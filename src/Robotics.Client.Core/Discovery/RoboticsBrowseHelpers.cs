using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Discovery;

public sealed class RoboticsBrowseHelpers(Session session)
{
    public IReadOnlyList<ReferenceDescription> BrowseForward(
        NodeId nodeId,
        NodeId referenceTypeId,
        bool includeSubtypes,
        NodeClass nodeClassMask)
    {
        return Browse(nodeId, referenceTypeId, includeSubtypes, nodeClassMask, BrowseDirection.Forward);
    }

    private IReadOnlyList<ReferenceDescription> BrowseInverse(
        NodeId nodeId,
        NodeId referenceTypeId,
        bool includeSubtypes,
        NodeClass nodeClassMask)
    {
        return Browse(nodeId, referenceTypeId, includeSubtypes, nodeClassMask, BrowseDirection.Inverse);
    }

    private IReadOnlyList<ReferenceDescription> Browse(
        NodeId nodeId,
        NodeId referenceTypeId,
        bool includeSubtypes,
        NodeClass nodeClassMask,
        BrowseDirection browseDirection)
    {
        var browser = new Browser(session)
        {
            BrowseDirection = browseDirection,
            ReferenceTypeId = referenceTypeId,
            IncludeSubtypes = includeSubtypes,
            NodeClassMask = (uint)nodeClassMask,
            ContinueUntilDone = true,
            ResultMask = (uint)BrowseResultMask.All
        };

        return browser.Browse(nodeId).Cast<ReferenceDescription>().ToArray();
    }

    public NodeId? ToNodeId(ExpandedNodeId expandedNodeId)
    {
        return ExpandedNodeId.ToNodeId(expandedNodeId, session.NamespaceUris);
    }

    public NodeId? GetTypeDefinition(NodeId nodeId)
    {
        ReferenceDescription? typeReference = BrowseForward(
                nodeId,
                ReferenceTypeIds.HasTypeDefinition,
                includeSubtypes: false,
                NodeClass.ObjectType | NodeClass.VariableType)
            .FirstOrDefault();

        return typeReference is null ? null : ToNodeId(typeReference.NodeId);
    }

    public bool IsTypeDefinitionOrSubtypeOf(NodeId? candidateType, NodeId expectedBaseType)
    {
        if (candidateType is null)
        {
            return false;
        }

        if (candidateType == expectedBaseType)
        {
            return true;
        }

        var visited = new HashSet<NodeId>();
        var queue = new Queue<NodeId>();
        queue.Enqueue(candidateType);

        while (queue.Count > 0)
        {
            NodeId current = queue.Dequeue();
            if (!visited.Add(current))
            {
                continue;
            }

            if (current == expectedBaseType)
            {
                return true;
            }

            foreach (ReferenceDescription supertype in BrowseInverse(
                         current,
                         ReferenceTypeIds.HasSubtype,
                         includeSubtypes: false,
                         NodeClass.ObjectType | NodeClass.VariableType))
            {
                NodeId? supertypeNodeId = ToNodeId(supertype.NodeId);
                if (supertypeNodeId is not null)
                {
                    queue.Enqueue(supertypeNodeId);
                }
            }
        }

        return false;
    }

    public NodeDiscoveryInfo ToNodeInfo(ReferenceDescription reference)
    {
        NodeId? nodeId = ToNodeId(reference.NodeId);
        NodeId? typeDefinition = nodeId is null ? null : GetTypeDefinition(nodeId);

        RuntimeIdentity? identity = nodeId is null ? null : RuntimeIdentity.From(nodeId, session.NamespaceUris);
        return new NodeDiscoveryInfo(
            FormatBrowseName(reference.BrowseName),
            reference.DisplayName.Text ?? reference.BrowseName.Name,
            nodeId?.ToString() ?? reference.NodeId.ToString(),
            typeDefinition?.ToString() ?? "unresolved",
            "reference browse + HasTypeDefinition",
            identity?.StableKey,
            identity?.NamespaceUri);
    }

    public ReferenceDescription? FindQualifiedChild(NodeId parentNodeId, string roboticsBrowseName, NodeId referenceTypeId, NodeClass nodeClassMask)
    {
        // Official specification truth: browse names are namespace-qualified, not display labels.
        // Local NodeSet/generated-code truth: BrowseNames constants are generated from the Robotics NodeSet.
        // Implementation decision: use the connected server's namespace index for qualified browse matching.
        var expected = new QualifiedName(roboticsBrowseName, GetRoboticsNamespaceIndex());

        return BrowseForward(parentNodeId, referenceTypeId, includeSubtypes: true, nodeClassMask)
            .FirstOrDefault(reference => reference.BrowseName == expected);
    }

    public ReferenceDescription? FindStandardProperty(NodeId parentNodeId, string standardBrowseName)
    {
        // Official specification truth: InputArguments and OutputArguments are standard OPC UA BrowseNames
        // in namespace 0 and are reached from the Method node via HasProperty when present.
        // Implementation decision: match the qualified BrowseName, never the DisplayName.
        var expected = new QualifiedName(standardBrowseName, namespaceIndex: 0);

        return BrowseForward(parentNodeId, ReferenceTypeIds.HasProperty, includeSubtypes: true, NodeClass.Variable)
            .FirstOrDefault(reference => reference.BrowseName == expected);
    }

    public IReadOnlyList<ReferenceDescription> FindChildrenByType(NodeId parentNodeId, NodeId referenceTypeId, NodeId expectedType)
    {
        return BrowseForward(parentNodeId, referenceTypeId, includeSubtypes: true, NodeClass.Object)
            .Where(reference =>
            {
                NodeId? nodeId = ToNodeId(reference.NodeId);
                return nodeId is not null && IsTypeDefinitionOrSubtypeOf(GetTypeDefinition(nodeId), expectedType);
            })
            .ToArray();
    }

    public IReadOnlyList<ReferenceDescription> FindMethods(NodeId parentNodeId, IEnumerable<string> methodBrowseNames)
    {
        var expectedNames = methodBrowseNames
            .Select(name => new QualifiedName(name, GetRoboticsNamespaceIndex()))
            .ToHashSet();

        return BrowseForward(parentNodeId, ReferenceTypeIds.HasComponent, includeSubtypes: true, NodeClass.Method)
            .Where(reference => expectedNames.Contains(reference.BrowseName))
            .ToArray();
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

    public static string FormatBrowseName(QualifiedName browseName)
    {
        return browseName.NamespaceIndex == 0
            ? browseName.Name
            : $"{browseName.NamespaceIndex}:{browseName.Name}";
    }
}
