# Current Server Address Space

The current OPC UA server address space is a temporary demo model. It is useful for exercising the reference server, robot simulation, telemetry, remote control, and remote program execution paths.

It is not yet the full OPC UA Robotics v1.02 companion specification model. The official OPC UA NodeSet files still need to be loaded and used as the source of truth before this becomes a specification-aligned Robotics address space.

## Browse Tree

```text
Objects
└── Robots
    └── SixAxisRobot
        ├── Manufacturer
        ├── Model
        ├── SerialNumber
        ├── ProductCode
        ├── IsMoving
        ├── Speed
        ├── Acceleration
        ├── CartesianPose
        ├── Axes
        ├── RemoteControl
        └── RemotePrograms
```

## Robot Identity

| Variable | Value |
| --- | --- |
| Manufacturer | Reference Implementation |
| Model | SixAxisRobot |
| SerialNumber | SIM-6AXIS-0001 |
| ProductCode | REF-SIX-AXIS-SIM |

## RemoteControl

The temporary `RemoteControl` object exposes simple demo control methods:

| Method | Purpose |
| --- | --- |
| MoveJoints | Command joint targets directly. |
| ResetToHome | Return the simulated robot to its home position. |
| StartDemoMotion | Start built-in demonstration motion. |
| StopMotion | Stop active robot motion. |

## RemotePrograms

The temporary `RemotePrograms` object exposes JSON-based robot program methods:

| Method | Purpose |
| --- | --- |
| LoadProgramFromJson | Load a robot program from a JSON string. |
| LoadSampleProgram | Load a built-in sample program by name. |
| StartProgram | Start the currently loaded program. |
| PauseProgram | Pause a running program. |
| ResumeProgram | Resume a paused program. |
| StopProgram | Stop the current program. |

Available sample programs:

- `pick-and-place-demo`
- `axis-range-demo`

## Implementation Status

The current implementation has a working minimal OPC UA server, a neutral simulated six-axis robot, live telemetry updates, simple `RemoteControl` methods, JSON robot program contracts, and exposed `RemotePrograms` methods.

The next model milestone is to load the official OPC UA NodeSet files, including OPC UA DI and OPC UA Robotics, then map these temporary demo nodes to the official Robotics concepts.
