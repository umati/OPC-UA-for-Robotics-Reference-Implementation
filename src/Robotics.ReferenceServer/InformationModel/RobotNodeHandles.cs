namespace Robotics.ReferenceServer.InformationModel;

public sealed class RobotNodeHandles
{
    public object? Manufacturer { get; set; }

    public object? Model { get; set; }

    public object? SerialNumber { get; set; }

    public object? ProductCode { get; set; }

    public object? IsMoving { get; set; }

    public object? Speed { get; set; }

    public object? Acceleration { get; set; }

    public object? CartesianX { get; set; }

    public object? CartesianY { get; set; }

    public object? CartesianZ { get; set; }

    public object? CartesianRx { get; set; }

    public object? CartesianRy { get; set; }

    public object? CartesianRz { get; set; }

    public AxisNodeHandles SAxis { get; } = new();

    public AxisNodeHandles LAxis { get; } = new();

    public AxisNodeHandles UAxis { get; } = new();

    public AxisNodeHandles RAxis { get; } = new();

    public AxisNodeHandles BAxis { get; } = new();

    public AxisNodeHandles TAxis { get; } = new();

    public object? RemoteProgramLoadedName { get; set; }

    public object? RemoteProgramExecutionState { get; set; }

    public object? RemoteProgramLastResult { get; set; }
}
