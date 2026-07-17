using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Client;

public sealed class SnapshotReadService(Session session)
{
    private readonly SnapshotDiscoveryService _discovery = new(session);

    public SnapshotReport Read(DiscoveryReport discoveryReport)
    {
        IReadOnlyList<SnapshotNode> nodes = _discovery.Discover(discoveryReport);
        return ReadNodes(nodes);
    }

    public SnapshotReport ReadNodes(IReadOnlyList<SnapshotNode> nodes)
    {
        var valuesBySection = new Dictionary<string, List<SnapshotValueReport>>(StringComparer.Ordinal)
        {
            ["SystemOperation"] = [],
            ["TaskControlOperation"] = [],
            ["MotionDevice"] = [],
            ["Axes"] = [],
            ["PowerTrains"] = []
        };

        foreach (SnapshotNode node in nodes)
        {
            if (!valuesBySection.TryGetValue(node.SectionName, out List<SnapshotValueReport>? sectionValues))
            {
                sectionValues = [];
                valuesBySection[node.SectionName] = sectionValues;
            }

            sectionValues.Add(ReadNode(node));
        }

        return new SnapshotReport(
            valuesBySection
                .Select(section => new SnapshotSectionReport(section.Key, section.Value))
                .ToArray());
    }

    private SnapshotValueReport ReadNode(SnapshotNode node)
    {
        try
        {
            var nodesToRead = new ReadValueIdCollection
            {
                new()
                {
                    NodeId = node.NodeId,
                    AttributeId = Attributes.Value
                },
                new()
                {
                    NodeId = node.NodeId,
                    AttributeId = Attributes.DataType
                },
                new()
                {
                    NodeId = node.NodeId,
                    AttributeId = Attributes.ValueRank
                }
            };

            session.Read(
                requestHeader: null,
                maxAge: 0,
                timestampsToReturn: TimestampsToReturn.Both,
                nodesToRead: nodesToRead,
                results: out DataValueCollection results,
                diagnosticInfos: out DiagnosticInfoCollection diagnosticInfos);

            DataValue value = results.Count > 0
                ? results[0]
                : new DataValue { StatusCode = new StatusCode(StatusCodes.BadUnexpectedError) };

            (string? engineeringUnits, string? euRange) = ReadAnalogProperties(node.NodeId);
            return new SnapshotValueReport(
                node.Label,
                node.BrowseName,
                node.NodeId.ToString(),
                ReadDataType(results),
                ReadValueRank(results),
                value.StatusCode.ToString(),
                IsDefaultTimestamp(value.SourceTimestamp) ? null : value.SourceTimestamp,
                IsDefaultTimestamp(value.ServerTimestamp) ? null : value.ServerTimestamp,
                StatusCode.IsNotGood(value.StatusCode) ? "<not available>" : FormatValue(value.Value),
                node.Heuristic,
                engineeringUnits,
                euRange);
        }
        catch (ServiceResultException ex)
        {
            return Failed(node, ex.StatusCode.ToString());
        }
        catch (Exception ex) when (ex is InvalidCastException or FormatException or ArgumentException)
        {
            return Failed(node, ex.Message);
        }
    }

    private static SnapshotValueReport Failed(SnapshotNode node, string statusOrMessage)
    {
        return new SnapshotValueReport(
            node.Label,
            node.BrowseName,
            node.NodeId.ToString(),
            DataType: null,
            ValueRank: null,
            statusOrMessage,
            SourceTimestamp: null,
            ServerTimestamp: null,
            "<not available>",
            node.Heuristic);
    }

    private static string? ReadDataType(DataValueCollection results)
    {
        if (results.Count <= 1 || StatusCode.IsNotGood(results[1].StatusCode))
        {
            return null;
        }

        return results[1].Value switch
        {
            NodeId nodeId => nodeId.ToString(),
            ExpandedNodeId expandedNodeId => expandedNodeId.ToString(),
            null => null,
            _ => Convert.ToString(results[1].Value, System.Globalization.CultureInfo.InvariantCulture)
        };
    }

    private static int? ReadValueRank(DataValueCollection results)
    {
        if (results.Count <= 2 || StatusCode.IsNotGood(results[2].StatusCode))
        {
            return null;
        }

        return results[2].Value switch
        {
            int valueRank => valueRank,
            short valueRank => valueRank,
            null => null,
            _ when int.TryParse(Convert.ToString(results[2].Value, System.Globalization.CultureInfo.InvariantCulture), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out int parsed) => parsed,
            _ => null
        };
    }

    private static bool IsDefaultTimestamp(DateTime timestamp)
    {
        return timestamp == DateTime.MinValue || timestamp == default;
    }

    private static string FormatValue(object? value)
    {
        return SafeValueRenderer.Format(value);
    }

    private (string? EngineeringUnits, string? EURange) ReadAnalogProperties(NodeId nodeId)
    {
        var browser = new Browser(session)
        {
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = ReferenceTypeIds.HasProperty,
            IncludeSubtypes = true,
            NodeClassMask = (uint)NodeClass.Variable,
            ContinueUntilDone = true,
            ResultMask = (uint)BrowseResultMask.All
        };
        ReferenceDescription[] properties = browser.Browse(nodeId).Cast<ReferenceDescription>()
            .Where(property => property.BrowseName.NamespaceIndex == 0 &&
                               (property.BrowseName.Name == "EngineeringUnits" || property.BrowseName.Name == "EURange"))
            .ToArray();

        string? engineeringUnits = null;
        string? euRange = null;
        foreach (ReferenceDescription property in properties)
        {
            NodeId? propertyNodeId = ExpandedNodeId.ToNodeId(property.NodeId, session.NamespaceUris);
            if (propertyNodeId is null)
            {
                continue;
            }

            DataValue value = session.ReadValue(propertyNodeId);
            if (StatusCode.IsNotGood(value.StatusCode))
            {
                continue;
            }

            if (property.BrowseName.Name == "EngineeringUnits")
            {
                engineeringUnits = value.Value switch
                {
                    EUInformation information => information.DisplayName?.Text,
                    _ => SafeValueRenderer.Format(value.Value)
                };
            }
            else if (property.BrowseName.Name == "EURange")
            {
                euRange = value.Value switch
                {
                    Opc.Ua.Range range => $"{range.Low.ToString(System.Globalization.CultureInfo.InvariantCulture)} … {range.High.ToString(System.Globalization.CultureInfo.InvariantCulture)}",
                    _ => SafeValueRenderer.Format(value.Value)
                };
            }
        }

        return (engineeringUnits, euRange);
    }
}
