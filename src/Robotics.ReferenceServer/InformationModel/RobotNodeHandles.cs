using Opc.Ua;

namespace Robotics.ReferenceServer.InformationModel;

public sealed class RobotNodeHandles
{
    public BaseVariableState? Manufacturer { get; set; }

    public BaseVariableState? Model { get; set; }

    public BaseVariableState? SerialNumber { get; set; }

    public BaseVariableState? ProductCode { get; set; }

    public BaseVariableState? IsMoving { get; set; }

    public BaseVariableState? Speed { get; set; }

    public BaseVariableState? Acceleration { get; set; }

    public BaseVariableState? CartesianX { get; set; }

    public BaseVariableState? CartesianY { get; set; }

    public BaseVariableState? CartesianZ { get; set; }

    public BaseVariableState? CartesianRx { get; set; }

    public BaseVariableState? CartesianRy { get; set; }

    public BaseVariableState? CartesianRz { get; set; }

    public AxisNodeHandles SAxis { get; } = new();

    public AxisNodeHandles LAxis { get; } = new();

    public AxisNodeHandles UAxis { get; } = new();

    public AxisNodeHandles RAxis { get; } = new();

    public AxisNodeHandles BAxis { get; } = new();

    public AxisNodeHandles TAxis { get; } = new();

    public BaseVariableState? RemoteProgramLoadedName { get; set; }

    public BaseVariableState? RemoteProgramExecutionState { get; set; }

    public BaseVariableState? RemoteProgramLastResult { get; set; }

    public List<string> MissingExpectedNodes { get; } = [];

    public int BoundNodeCount =>
        BoundVariables.Count()
        + (SAxis.AxisObject is null ? 0 : 1)
        + (LAxis.AxisObject is null ? 0 : 1)
        + (UAxis.AxisObject is null ? 0 : 1)
        + (RAxis.AxisObject is null ? 0 : 1)
        + (BAxis.AxisObject is null ? 0 : 1)
        + (TAxis.AxisObject is null ? 0 : 1);

    public IEnumerable<BaseVariableState> BoundVariables
    {
        get
        {
            if (Manufacturer is not null)
            {
                yield return Manufacturer;
            }

            if (Model is not null)
            {
                yield return Model;
            }

            if (SerialNumber is not null)
            {
                yield return SerialNumber;
            }

            if (ProductCode is not null)
            {
                yield return ProductCode;
            }

            foreach (BaseVariableState variable in SAxis.BoundVariables)
            {
                yield return variable;
            }

            foreach (BaseVariableState variable in LAxis.BoundVariables)
            {
                yield return variable;
            }

            foreach (BaseVariableState variable in UAxis.BoundVariables)
            {
                yield return variable;
            }

            foreach (BaseVariableState variable in RAxis.BoundVariables)
            {
                yield return variable;
            }

            foreach (BaseVariableState variable in BAxis.BoundVariables)
            {
                yield return variable;
            }

            foreach (BaseVariableState variable in TAxis.BoundVariables)
            {
                yield return variable;
            }
        }
    }
}
