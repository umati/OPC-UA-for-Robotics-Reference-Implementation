# Current Server Address Space

The OPC UA server can expose three address-space modes with `--model`:

| Mode | Command line | Exposed model |
| --- | --- | --- |
| Temporary | `--model temporary` | Only the temporary demo model under `Objects/Robots/SixAxisRobot`. |
| Official | `--model official` | Only the official DI/Robotics type NodeSets and imported MinimalRealistic instance model from `NodeSets/Instances/SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml`. |
| Both | `--model both` | The temporary demo model plus the imported MinimalRealistic instance model. |

If `--model` is omitted, the server uses `Both`. This remains the default during migration so existing demo clients and method behavior continue to work while official Robotics instance-node coverage is expanded.

The temporary demo model remains useful for exercising the reference server, robot simulation, telemetry, remote control, and remote program execution paths. It is kept during migration so existing clients and method behavior remain unchanged.

The imported MinimalRealistic model is the first official OPC UA Robotics instance variant loaded through the NodeSet chain. The server also loads the required OPC UA DI and OPC UA Robotics type NodeSets. Selected MinimalRealistic nodes are now bound to the same simulation snapshot that drives the temporary demo model.

In `Official` mode, the simulation still runs and updates bound MinimalRealistic instance nodes. The temporary `RemoteControl` and `RemotePrograms` methods are not exposed in this mode yet because they still belong to the temporary demo address space. They will be mapped to official Robotics remote-operation concepts in a later milestone.

## Browse Tree

### Temporary Demo Model

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

### Imported MinimalRealistic Instance Model

```text
Objects
└── SixAxisRobotSystem
    ├── Controllers
    ├── MotionDevices
    └── SafetyStates
```

The imported model includes one MotionDeviceSystem, one MotionDevice, six axes, controller/task structure, safety state structure, and powertrain/motor structure. Bound runtime nodes currently include robot identity, axis position, axis velocity, and motor temperature where those variables exist in the MinimalRealistic NodeSet. Target position and motor load are not invented because the current MinimalRealistic NodeSet does not define matching runtime variables.

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

The current implementation has a working minimal OPC UA server, a neutral simulated six-axis robot, live telemetry updates, simple `RemoteControl` methods, JSON robot program contracts, exposed `RemotePrograms` methods, imported official DI/Robotics type NodeSets, the MinimalRealistic instance NodeSet, selected simulation bindings into that imported instance model, and runtime address-space mode selection.

The temporary demo nodes remain available during migration and should stay in place until the official instance NodeSet model is richer, bound, and verified.
