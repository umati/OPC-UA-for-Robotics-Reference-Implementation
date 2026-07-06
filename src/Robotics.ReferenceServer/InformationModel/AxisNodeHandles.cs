namespace Robotics.ReferenceServer.InformationModel;

public sealed class AxisNodeHandles
{
    public object? PositionDegrees { get; set; }

    public object? VelocityDegreesPerSecond { get; set; }

    public object? TargetPositionDegrees { get; set; }

    public object? MotorLoadPercent { get; set; }

    public object? TemperatureCelsius { get; set; }
}
