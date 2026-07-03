using Robotics.Shared;

namespace Robotics.Shared.Tests;

public class RobotDomainTests
{
    [Fact]
    public void RobotAxisNameContainsSixAxes()
    {
        RobotAxisName[] axes = Enum.GetValues<RobotAxisName>();

        Assert.Equal(
            [RobotAxisName.S, RobotAxisName.L, RobotAxisName.U, RobotAxisName.R, RobotAxisName.B, RobotAxisName.T],
            axes);
    }

    [Fact]
    public void RobotTelemetrySnapshotCanBeCreated()
    {
        RobotTelemetrySnapshot snapshot = new()
        {
            Manufacturer = "Reference Implementation",
            Model = "SixAxisRobot",
            SerialNumber = "SIM-6AXIS-0001",
            ProductCode = "REF-SIX-AXIS-SIM",
            IsMoving = false,
            Speed = 0,
            Acceleration = 0,
            CartesianPose = new CartesianPose
            {
                X = 0,
                Y = 0,
                Z = 0,
                Rx = 0,
                Ry = 0,
                Rz = 0
            },
            AxisStates = [],
            TimestampUtc = DateTimeOffset.UtcNow
        };

        Assert.Equal("Reference Implementation", snapshot.Manufacturer);
        Assert.Equal("SixAxisRobot", snapshot.Model);
        Assert.Empty(snapshot.AxisStates);
    }

    [Fact]
    public void AxisStateValuesCanBeInitialized()
    {
        RobotAxisState axisState = new()
        {
            AxisName = RobotAxisName.S,
            PositionDegrees = 10,
            VelocityDegreesPerSecond = 2,
            TargetPositionDegrees = 20,
            MotorLoadPercent = 15,
            TemperatureCelsius = 26,
            MinPositionDegrees = -170,
            MaxPositionDegrees = 170,
            MaxVelocityDegreesPerSecond = 150,
            MaxAccelerationDegreesPerSecondSquared = 300
        };

        Assert.Equal(RobotAxisName.S, axisState.AxisName);
        Assert.Equal(10, axisState.PositionDegrees);
        Assert.Equal(20, axisState.TargetPositionDegrees);
        Assert.Equal(-170, axisState.MinPositionDegrees);
        Assert.Equal(300, axisState.MaxAccelerationDegreesPerSecondSquared);
    }
}
