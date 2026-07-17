using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;

namespace Robotics.ReferenceServer.Telemetry;

internal interface IRobotTelemetryPublisher
{
    void Publish(RobotTelemetrySnapshot robotSnapshot, RobotProgramExecutionSnapshot programSnapshot);
}
