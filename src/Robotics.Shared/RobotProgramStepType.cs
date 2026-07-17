using System.Text.Json.Serialization;

namespace Robotics.Shared;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RobotProgramStepType
{
    MoveJoint,
    Wait
}
