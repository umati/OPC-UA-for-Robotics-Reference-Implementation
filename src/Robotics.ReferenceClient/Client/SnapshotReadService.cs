using System.Globalization;
using Opc.Ua;
using Opc.Ua.Client;
using Robotics.ReferenceClient.Discovery;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient.Client;

internal sealed class SnapshotReadService(Session session)
{
    private readonly SnapshotDiscoveryService _discovery = new(session);

    public SnapshotReport Read(DiscoveryReport discoveryReport)
    {
        IReadOnlyList<SnapshotNode> nodes = _discovery.Discover(discoveryReport);
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
                node.Heuristic);
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
            _ => Convert.ToString(results[1].Value, CultureInfo.InvariantCulture)
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
            _ when int.TryParse(Convert.ToString(results[2].Value, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed) => parsed,
            _ => null
        };
    }

    private static bool IsDefaultTimestamp(DateTime timestamp)
    {
        return timestamp == DateTime.MinValue || timestamp == default;
    }

    private static string FormatValue(object? value)
    {
        return value switch
        {
            null => "null",
            bool boolean => boolean.ToString().ToLowerInvariant(),
            sbyte or byte or short or ushort or int or uint or long or ulong or float or double or decimal =>
                Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty,
            string text => text,
            LocalizedText localizedText => localizedText.Text ?? string.Empty,
            QualifiedName qualifiedName => RoboticsBrowseHelpers.FormatBrowseName(qualifiedName),
            NodeId nodeId => nodeId.ToString(),
            ExpandedNodeId expandedNodeId => expandedNodeId.ToString(),
            ExtensionObject extensionObject => FormatExtensionObject(extensionObject),
            Array array => FormatArray(array),
            Variant variant => FormatValue(variant.Value),
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? value.GetType().FullName ?? string.Empty
        };
    }

    private static string FormatArray(Array array)
    {
        const int maxItems = 5;
        string items = string.Join(", ", array.Cast<object?>().Take(maxItems).Select(FormatValue));
        string suffix = array.Length > maxItems ? ", ..." : string.Empty;
        return $"count={array.Length} [{items}{suffix}]";
    }

    private static string FormatExtensionObject(ExtensionObject extensionObject)
    {
        string typeId = extensionObject.TypeId?.ToString() ?? "null";
        string body = extensionObject.Body switch
        {
            null => "null",
            IEncodeable encodeable => encodeable.GetType().Name,
            byte[] bytes => $"ByteString({bytes.Length} bytes)",
            _ => extensionObject.Body.GetType().Name
        };

        return $"ExtensionObject(TypeId={typeId}, Body={body})";
    }
}
