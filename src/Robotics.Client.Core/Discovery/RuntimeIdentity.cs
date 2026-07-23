using Opc.Ua;

namespace Robotics.Client.Core.Discovery;

/// <summary>Stable, diagnostic-preserving identity for a node in a connected OPC UA namespace table.</summary>
public sealed record RuntimeIdentity(string StableKey, string NamespaceUri, string NodeId)
{
    public static RuntimeIdentity From(NodeId nodeId, NamespaceTable namespaces)
    {
        string namespaceUri = namespaces.GetString(nodeId.NamespaceIndex) ?? string.Empty;
        string identifier = Convert.ToString(nodeId.Identifier, System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;
        string stableKey = $"nsu={Uri.EscapeDataString(namespaceUri)};type={nodeId.IdType};id={Uri.EscapeDataString(identifier)}";
        return new RuntimeIdentity(stableKey, namespaceUri, nodeId.ToString());
    }
}
