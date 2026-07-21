using Opc.Ua;
using Robotics.ReferenceServer.InformationModel;
using Robotics.ReferenceServer.Simulation;

namespace Robotics.Shared.Tests;

public class RobotEngineeringUnitMetadataTests
{
    [Fact]
    public void RotationalAxisMetadataUsesVerifiedUneceDegreeDefinition()
    {
        EUInformation metadata = RobotEngineeringUnitMetadata.Degrees;

        Assert.Equal("http://www.opcfoundation.org/UA/units/un/cefact", metadata.NamespaceUri);
        Assert.Equal(17476, metadata.UnitId);
        Assert.Equal("°", metadata.DisplayName.Text);
        Assert.Equal("degree [unit of angle]", metadata.Description.Text);
    }

    [Fact]
    public void SimulatorPublishesSixFiniteDegreePositionValuesAndPreservesIsolation()
    {
        var robot = new SixAxisRobot();
        var before = robot.GetSnapshot();

        robot.SetJointTarget(new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = 45 });
        robot.Update(TimeSpan.FromSeconds(0.1));
        var after = robot.GetSnapshot();

        Assert.Equal(6, after.AxisStates.Count);
        Assert.All(after.AxisStates, axis => Assert.True(double.IsFinite(axis.PositionDegrees)));
        Assert.NotEqual(before.AxisStates.Single(axis => axis.AxisName == RobotAxisName.S).PositionDegrees,
            after.AxisStates.Single(axis => axis.AxisName == RobotAxisName.S).PositionDegrees);
        Assert.Equal(
            before.AxisStates.Where(axis => axis.AxisName != RobotAxisName.S).Select(axis => axis.PositionDegrees),
            after.AxisStates.Where(axis => axis.AxisName != RobotAxisName.S).Select(axis => axis.PositionDegrees));
    }
}
