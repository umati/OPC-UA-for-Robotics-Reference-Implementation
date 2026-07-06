using Opc.Ua;
using Opc.Ua.Export;

namespace Robotics.ReferenceServer.InformationModel;

internal static class NodeSetLoader
{
    public const string DiNamespaceUri = "http://opcfoundation.org/UA/DI/";
    public const string RoboticsNamespaceUri = "http://opcfoundation.org/UA/Robotics/";
    public const string InstanceNamespaceUri = "urn:RoboticsReferenceImplementation:Instances";

    private const string NodeSetsDirectoryName = "NodeSets";
    private const string ProjectNodeSetsRelativePath = "src/Robotics.ReferenceServer/NodeSets";
    private const string MinimalRealisticRootIdentifier = "SixAxisRobot.MinimalRealistic/System";

    // Load only this explicit chain for now. IA and Machinery NodeSets are later integration layers.
    private static readonly string[] RequiredNodeSetRelativePaths =
    [
        "Opc.Ua.Di.NodeSet2.xml",
        "Opc.Ua.Robotics.NodeSet2.xml",
        "Instances/SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml"
    ];

    public static NodeStateCollection LoadRequiredNodeSets(ISystemContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        string nodeSetsDirectory = ResolveNodeSetsDirectory();
        var importedNodes = new NodeStateCollection();

        foreach (string relativePath in RequiredNodeSetRelativePaths)
        {
            string nodeSetPath = GetNodeSetPath(nodeSetsDirectory, relativePath);
            if (!File.Exists(nodeSetPath))
            {
                throw new FileNotFoundException(
                    $"Required OPC UA NodeSet file '{relativePath}' was not found at '{nodeSetPath}'. " +
                    "Ensure the NodeSets folder contains the explicit DI, Robotics, and MinimalRealistic instance NodeSets.",
                    nodeSetPath);
            }

            using FileStream stream = File.OpenRead(nodeSetPath);
            UANodeSet nodeSet = UANodeSet.Read(stream);
            nodeSet.Import(context, importedNodes, true);
        }

        return importedNodes;
    }

    public static NodeId GetMinimalRealisticRootNodeId(ISystemContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        int namespaceIndex = context.NamespaceUris.GetIndex(InstanceNamespaceUri);
        if (namespaceIndex < 0 || namespaceIndex > ushort.MaxValue)
        {
            throw new InvalidOperationException(
                $"The imported instance namespace '{InstanceNamespaceUri}' is not registered in the server namespace table.");
        }

        return new NodeId(MinimalRealisticRootIdentifier, (ushort)namespaceIndex);
    }

    private static string ResolveNodeSetsDirectory()
    {
        string[] candidates = GetNodeSetsDirectoryCandidates();

        foreach (string candidate in candidates)
        {
            if (HasAllRequiredNodeSets(candidate))
            {
                return candidate;
            }
        }

        string? firstExistingCandidate = candidates.FirstOrDefault(Directory.Exists);
        if (firstExistingCandidate is not null)
        {
            return firstExistingCandidate;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the OPC UA NodeSets directory. Checked: " + string.Join(", ", candidates));
    }

    private static string[] GetNodeSetsDirectoryCandidates()
    {
        var candidates = new List<string>();

        AddCandidate(candidates, Path.Combine(AppContext.BaseDirectory, NodeSetsDirectoryName));
        AddCandidate(candidates, Path.Combine(Directory.GetCurrentDirectory(), NodeSetsDirectoryName));
        AddCandidate(candidates, Path.Combine(Directory.GetCurrentDirectory(), ProjectNodeSetsRelativePath));

        AddParentCandidates(candidates, AppContext.BaseDirectory);
        AddParentCandidates(candidates, Directory.GetCurrentDirectory());

        return [.. candidates];
    }

    private static void AddParentCandidates(List<string> candidates, string startDirectory)
    {
        DirectoryInfo? directory = new DirectoryInfo(startDirectory);
        while (directory is not null)
        {
            AddCandidate(candidates, Path.Combine(directory.FullName, ProjectNodeSetsRelativePath));
            AddCandidate(candidates, Path.Combine(directory.FullName, NodeSetsDirectoryName));
            directory = directory.Parent;
        }
    }

    private static void AddCandidate(List<string> candidates, string path)
    {
        string fullPath = Path.GetFullPath(path);
        if (!candidates.Contains(fullPath, StringComparer.OrdinalIgnoreCase))
        {
            candidates.Add(fullPath);
        }
    }

    private static bool HasAllRequiredNodeSets(string nodeSetsDirectory)
    {
        return Directory.Exists(nodeSetsDirectory)
            && RequiredNodeSetRelativePaths.All(relativePath => File.Exists(GetNodeSetPath(nodeSetsDirectory, relativePath)));
    }

    private static string GetNodeSetPath(string nodeSetsDirectory, string relativePath)
    {
        return Path.Combine(nodeSetsDirectory, relativePath.Replace('/', Path.DirectorySeparatorChar));
    }
}
