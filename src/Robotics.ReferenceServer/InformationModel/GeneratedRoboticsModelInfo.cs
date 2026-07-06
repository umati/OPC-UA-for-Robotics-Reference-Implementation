using RoboticsBrowseNames = Opc.Ua.Robotics.BrowseNames;
using RoboticsNamespaces = Opc.Ua.Robotics.Namespaces;

namespace Robotics.ReferenceServer.InformationModel;

public static class GeneratedRoboticsModelInfo
{
    public static string NamespaceUri => RoboticsNamespaces.Robotics;

    public static IReadOnlyList<string> KnownGeneratedNames { get; } =
    [
        nameof(RoboticsNamespaces.Robotics),
        RoboticsBrowseNames.MotionDeviceType,
        RoboticsBrowseNames.ControllerType,
        RoboticsBrowseNames.TaskControlType
    ];

    public static string Status => "Generated OPC UA Robotics model assembly is referenced; runtime address space is unchanged.";
}
