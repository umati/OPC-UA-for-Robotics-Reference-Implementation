using Opc.Ua;
using Opc.Ua.Client;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient.Discovery;

internal sealed class MethodMetadataDiscoveryService(Session session, RoboticsBrowseHelpers browse)
{
    public MethodReport CreateMissingMethodReport(string expectedName, string evidence)
    {
        return new MethodReport(
            expectedName,
            Found: false,
            BrowseName: null,
            DisplayName: null,
            NodeId: null,
            ParentNodeId: null,
            MethodArgumentsReport.Missing,
            MethodArgumentsReport.Missing,
            evidence);
    }

    public MethodReport Discover(
        string expectedName,
        ReferenceDescription methodReference,
        NodeId parentNodeId,
        string evidence)
    {
        NodeId? methodNodeId = browse.ToNodeId(methodReference.NodeId);
        if (methodNodeId is null)
        {
            return new MethodReport(
                expectedName,
                Found: true,
                RoboticsBrowseHelpers.FormatBrowseName(methodReference.BrowseName),
                methodReference.DisplayName.Text ?? methodReference.BrowseName.Name,
                methodReference.NodeId.ToString(),
                parentNodeId.ToString(),
                new MethodArgumentsReport("unreadable", null, [], "method NodeId could not be resolved into the session namespace table"),
                new MethodArgumentsReport("unreadable", null, [], "method NodeId could not be resolved into the session namespace table"),
                evidence);
        }

        return new MethodReport(
            expectedName,
            Found: true,
            RoboticsBrowseHelpers.FormatBrowseName(methodReference.BrowseName),
            methodReference.DisplayName.Text ?? methodReference.BrowseName.Name,
            methodNodeId.ToString(),
            parentNodeId.ToString(),
            DiscoverArgumentProperty(methodNodeId, BrowseNames.InputArguments),
            DiscoverArgumentProperty(methodNodeId, BrowseNames.OutputArguments),
            evidence);
    }

    private MethodArgumentsReport DiscoverArgumentProperty(NodeId methodNodeId, string browseName)
    {
        ReferenceDescription? property = browse.FindStandardProperty(methodNodeId, browseName);
        if (property is null)
        {
            return MethodArgumentsReport.Missing;
        }

        NodeId? propertyNodeId = browse.ToNodeId(property.NodeId);
        if (propertyNodeId is null)
        {
            return new MethodArgumentsReport("unreadable", property.NodeId.ToString(), [], "argument property NodeId could not be resolved into the session namespace table");
        }

        try
        {
            DataValue value = session.ReadValue(propertyNodeId);
            if (StatusCode.IsBad(value.StatusCode))
            {
                return new MethodArgumentsReport("unreadable", propertyNodeId.ToString(), [], value.StatusCode.ToString());
            }

            IReadOnlyList<Argument>? arguments = DecodeArguments(value.Value);
            if (arguments is null)
            {
                string valueType = value.Value?.GetType().FullName ?? "null";
                return new MethodArgumentsReport("unreadable", propertyNodeId.ToString(), [], $"unexpected value type {valueType}");
            }

            if (arguments.Count == 0)
            {
                return new MethodArgumentsReport("none", propertyNodeId.ToString(), [], null);
            }

            return new MethodArgumentsReport(
                "listed",
                propertyNodeId.ToString(),
                arguments.Select(ToReport).ToArray(),
                null);
        }
        catch (Exception ex) when (ex is ServiceResultException or InvalidCastException or FormatException)
        {
            return new MethodArgumentsReport("unreadable", propertyNodeId.ToString(), [], ex.Message);
        }
    }

    private static IReadOnlyList<Argument>? DecodeArguments(object? value)
    {
        return value switch
        {
            null => null,
            Argument[] arguments => arguments,
            ArgumentCollection arguments => arguments,
            ExtensionObject[] extensionObjects => extensionObjects.Select(DecodeExtensionObject).ToArray(),
            ExtensionObject extensionObject => [DecodeExtensionObject(extensionObject)],
            _ => null
        };
    }

    private static Argument DecodeExtensionObject(ExtensionObject extensionObject)
    {
        if (extensionObject.Body is Argument argument)
        {
            return argument;
        }

        if (extensionObject.Body is IEncodeable encodeable)
        {
            return (Argument)encodeable;
        }

        throw new InvalidCastException($"ExtensionObject body is not an OPC UA Argument: {extensionObject.Body?.GetType().FullName ?? "null"}");
    }

    private static MethodArgumentReport ToReport(Argument argument)
    {
        return new MethodArgumentReport(
            argument.Name ?? string.Empty,
            argument.DataType?.ToString() ?? "null",
            argument.ValueRank,
            FormatArrayDimensions(argument.ArrayDimensions),
            argument.Description?.Text ?? string.Empty);
    }

    private static string FormatArrayDimensions(UInt32Collection? dimensions)
    {
        return dimensions is null || dimensions.Count == 0
            ? "[]"
            : $"[{string.Join(", ", dimensions)}]";
    }
}
