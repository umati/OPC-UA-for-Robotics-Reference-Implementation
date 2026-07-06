using Opc.Ua;

namespace Robotics.ReferenceServer.InformationModel;

public sealed class AxisNodeHandles
{
    public NodeState? AxisObject { get; set; }

    public BaseVariableState? PositionDegrees { get; set; }

    public BaseVariableState? VelocityDegreesPerSecond { get; set; }

    public BaseVariableState? TargetPositionDegrees { get; set; }

    public BaseVariableState? MotorLoadPercent { get; set; }

    public BaseVariableState? TemperatureCelsius { get; set; }

    public int BoundNodeCount => (AxisObject is null ? 0 : 1) + BoundVariables.Count();

    public IEnumerable<BaseVariableState> BoundVariables
    {
        get
        {
            if (PositionDegrees is not null)
            {
                yield return PositionDegrees;
            }

            if (VelocityDegreesPerSecond is not null)
            {
                yield return VelocityDegreesPerSecond;
            }

            if (TargetPositionDegrees is not null)
            {
                yield return TargetPositionDegrees;
            }

            if (MotorLoadPercent is not null)
            {
                yield return MotorLoadPercent;
            }

            if (TemperatureCelsius is not null)
            {
                yield return TemperatureCelsius;
            }
        }
    }
}
