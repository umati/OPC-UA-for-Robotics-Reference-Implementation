using System.Globalization;
using Opc.Ua;
using Robotics.ReferenceClient.Discovery;

namespace Robotics.ReferenceClient.Client;

internal static class SafeValueRenderer
{
    public static string Format(object? value)
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
            Variant variant => Format(variant.Value),
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? value.GetType().FullName ?? string.Empty
        };
    }

    private static string FormatArray(Array array)
    {
        const int maxItems = 5;
        string items = string.Join(", ", array.Cast<object?>().Take(maxItems).Select(Format));
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
