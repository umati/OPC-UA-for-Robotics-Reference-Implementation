using Opc.Ua;
using Opc.Ua.Server;

namespace Robotics.ReferenceServer;

internal sealed class RoboticsNodeManager : CustomNodeManager2
{
    private const string NamespaceUri = "urn:RoboticsReferenceServer:Robotics";

    public RoboticsNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        : base(server, configuration, NamespaceUri)
    {
        SystemContext.NodeIdFactory = this;
    }

    public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
    {
        var robotsFolder = CreateRobotsFolder();

        AddPredefinedNode(SystemContext, robotsFolder);
        AddObjectsFolderReference(externalReferences, robotsFolder.NodeId);
    }

    private FolderState CreateRobotsFolder()
    {
        var robotsFolder = new FolderState(null)
        {
            SymbolicName = "Robots",
            ReferenceTypeId = ReferenceTypeIds.Organizes,
            TypeDefinitionId = ObjectTypeIds.FolderType,
            NodeId = new NodeId("Robots", NamespaceIndex),
            BrowseName = new QualifiedName("Robots", NamespaceIndex),
            DisplayName = new LocalizedText("Robots"),
            Description = new LocalizedText("Folder for robot instances."),
            WriteMask = AttributeWriteMask.None,
            UserWriteMask = AttributeWriteMask.None,
            EventNotifier = EventNotifiers.None
        };

        robotsFolder.AddReference(ReferenceTypeIds.Organizes, true, ObjectIds.ObjectsFolder);

        return robotsFolder;
    }

    private static void AddObjectsFolderReference(
        IDictionary<NodeId, IList<IReference>> externalReferences,
        NodeId robotsFolderId)
    {
        if (!externalReferences.TryGetValue(ObjectIds.ObjectsFolder, out var references))
        {
            externalReferences[ObjectIds.ObjectsFolder] = references = new List<IReference>();
        }

        references.Add(new NodeStateReference(ReferenceTypeIds.Organizes, false, robotsFolderId));
    }
}
