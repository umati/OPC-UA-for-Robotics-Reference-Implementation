# OPC UA Robotics Notes

## Namespace

Robotics namespace URI:

`http://opcfoundation.org/UA/Robotics/`

## Demo Robot

The demo robot is a simulated 6-axis industrial robot.

It is not intended to represent any specific commercial robot.

The simulation should use physically plausible joint-space motion, including joint limits, velocity limits, acceleration limits, and smooth deceleration.

## Axis Mapping

| Axis | Meaning |
| --- | --- |
| S-axis | Base rotation |
| L-axis | Lower arm forward/backward |
| U-axis | Upper arm up/down |
| R-axis | Arm rotation |
| B-axis | Wrist bend |
| T-axis | Tool flange rotation |

## Example Robot

| Field | Value |
| --- | --- |
| Manufacturer | Reference Implementation |
| Model | SixAxisRobot |
| SerialNumber | SIM-6AXIS-0001 |
| ProductCode | REF-SIX-AXIS-SIM |
