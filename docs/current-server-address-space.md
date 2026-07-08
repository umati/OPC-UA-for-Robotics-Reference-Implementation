# Current Server Address Space

The OPC UA server can expose three address-space modes with `--model`:

| Mode | Command line | Exposed model |
| --- | --- | --- |
| Temporary | `--model temporary` | Only the temporary demo model under `Objects/Robots/SixAxisRobot`. |
| Official | `--model official` | Only the official DI/Robotics type NodeSets and imported MinimalRealistic instance model from `NodeSets/Instances/SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml`. |
| Both | `--model both` | The temporary demo model plus the imported MinimalRealistic instance model. |

If `--model` is omitted, the server uses `Both`. This remains the default during migration so existing demo clients and method behavior continue to work while official Robotics instance-node coverage is expanded.

The temporary demo model remains useful for exercising the reference server, robot simulation, telemetry, remote control, and remote program execution paths. It is kept during migration so existing clients and method behavior remain unchanged.

The imported MinimalRealistic model is the first official OPC UA Robotics instance variant loaded through the NodeSet chain. The server also loads the required OPC UA DI and OPC UA Robotics type NodeSets. Selected MinimalRealistic nodes are now bound to the same simulation snapshot and program execution snapshot that drive the temporary demo model.

In `Official` mode, the simulation still runs and updates bound MinimalRealistic instance nodes. The temporary `RemoteControl` and `RemotePrograms` methods are not exposed in this mode because they still belong to the temporary demo address space.

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

The imported model includes one MotionDeviceSystem, one MotionDevice, six axes, controller/task structure, safety state structure, and powertrain/motor structure. Bound runtime nodes currently include robot identity, axis position, axis velocity, motor temperature, and the official TaskControl program metadata variables where those variables exist in the MinimalRealistic NodeSet. Target position and motor load are not invented because the current MinimalRealistic NodeSet does not define matching runtime variables.

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
| LoadSampleProgram | Load a disk-backed sample program by name. |
| StartProgram | Start the currently loaded program. |
| PauseProgram | Pause a running program. |
| ResumeProgram | Resume a paused program. |
| StopProgram | Stop the current program. |

Available sample programs:

- `pick-and-place-demo`
- `axis-range-demo`

These methods are demo methods, not official OPC UA Robotics TaskControl methods. They remain available in `Temporary` and `Both` modes and reuse the same server-side command service as the browser HTTP control bridge.

Sample program loading reads the current JSON file from disk for every call. The resolver prefers the repository path `src/Robotics.ReferenceServer/RobotPrograms/SamplePrograms/` when it can be found by walking upward from the current directory or `AppContext.BaseDirectory`, then falls back to a colocated `RobotPrograms/SamplePrograms/` directory. A copied output file is not preferred over the repository source when the server is launched from `bin/Debug`.

## Official Robotics TaskControl

The local official Robotics NodeSet defines `TaskControlType` with the optional `TaskControlOperation/TaskControlStateMachine` add-in. The generated constants identify the official TaskControl operation methods as `Start`, `Stop`, `LoadByName`, `LoadByNodeId`, `UnloadProgram`, `UnloadByName`, and `UnloadByNodeId`. The generated constants also identify `ResetToProgramStart` on `ReadySubstateMachineType`.

The current MinimalRealistic instance NodeSet contains this official TaskControl browse structure:

```text
SixAxisRobotSystem
└── Controllers
    └── MainController
        ├── SystemOperation
        │   └── SystemOperationStateMachine
        │       ├── CurrentState
        │       │   └── Id
        │       ├── LastTransition
        │       │   └── Id
        │       ├── LastTransitionReason
        │       │   ├── EnumValues
        │       │   └── ValueAsText
        │       ├── PossibleStopModes
        │       ├── IdleSubstateMachine
        │       ├── ExecutingSubstateMachine
        │       ├── Start
        │       ├── Stop
        │       ├── GetReady
        │       └── StandDown
        └── TaskControls
            └── MainTaskControl
                ├── ComponentName
                ├── ParameterSet
                │   ├── TaskProgramLoaded
                │   └── TaskProgramName
                └── TaskControlOperation
                    └── TaskControlStateMachine
                        ├── CurrentState
                        │   └── Id
                        ├── LastTransition
                        │   └── Id
                        ├── LastTransitionReason
                        │   ├── EnumValues
                        │   └── ValueAsText
                        ├── Start
                        ├── Stop
                        ├── LoadByName
                        ├── LoadByNodeId
                        ├── UnloadProgram
                        ├── UnloadByName
                        ├── UnloadByNodeId
                        └── ReadySubstateMachine
                            ├── CurrentState
                            │   └── Id
                            ├── LastTransition
                            │   └── Id
                            ├── LastTransitionReason
                            │   ├── EnumValues
                            │   └── ValueAsText
                            └── ResetToProgramStart
```

The stable instance NodeIds used by the server binding are:

| Browse node | Stable NodeId identifier |
| --- | --- |
| MainController | `SixAxisRobot.MinimalRealistic/Controller/MainController` |
| SystemOperation | `SixAxisRobot.MinimalRealistic/Controller/MainController/SystemOperation` |
| SystemOperationStateMachine | `SixAxisRobot.MinimalRealistic/Controller/MainController/SystemOperation/SystemOperationStateMachine` |
| MainTaskControl | `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl` |
| TaskProgramLoaded | `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskProgramLoaded` |
| TaskProgramName | `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskProgramName` |

The server binds `TaskProgramLoaded` and `TaskProgramName` to the existing program executor snapshot. `TaskProgramLoaded` is true when the executor has a non-empty current program name, and `TaskProgramName` exposes that name or an empty string when no program is loaded.

The MinimalRealistic instance NodeSet now includes the official `TaskControlOperation` AddIn below `MainTaskControl`. The AddIn uses `HasAddIn`, has type definition `TaskControlOperationType`, and contains `TaskControlStateMachine` with type definition `TaskControlStateMachineType`.

Following OPC UA Part 16 section 4.5, the StateMachine instances expose instance-specific variables and methods only. They do not duplicate all `StateType` and `TransitionType` children from `TaskControlStateMachineType`, `ReadySubstateMachineType`, or `SystemOperationStateMachineType`. `CurrentState/Id` and `LastTransition/Id` point to the official Robotics type-level state and transition NodeIds.

The remote-operation state machine includes explicit official method instances for `Start`, `Stop`, `LoadByName`, `LoadByNodeId`, `UnloadProgram`, `UnloadByName`, and `UnloadByNodeId`. The ready substate machine includes `ResetToProgramStart`. Method argument nodes use the local Robotics NodeSet signatures: `Status` output uses data type `i=6`, `Name` input uses `i=12`, `Id` input uses `i=18`, and `StopMode` input uses the official local NodeSet data type `i=8`.

After NodeSet loading, the server repairs and populates the runtime `MethodState.InputArguments` and `MethodState.OutputArguments` objects for every official TaskControl and SystemOperation method. This is required because OPC UA clients can browse `InputArguments` and `OutputArguments` child variables while the UA-.NETStandard Call service still validates against the `MethodState` argument metadata attached to the loaded method object. Startup diagnostics print each official method's `BrowseName`, `NodeId`, parent NodeId, runtime argument property presence/counts, child argument-node presence, and callback assignment state.

Official Robotics method callbacks return exactly the declared output argument list. For the inspected methods below, that means exactly one output value, `Int32 Status`; diagnostic text is written to console/status variables, not appended as a second official output argument. Call diagnostics log method name, received input count, returned output count, and status value.

Official TaskControlOperation signatures:

| Official method | Input count and types | Output count and types |
| --- | --- | --- |
| `LoadByName` | 1: `Name` `String` (`i=12`) | 1: `Status` `Int32` (`i=6`) |
| `LoadByNodeId` | 1: `Id` `ExpandedNodeId` (`i=18`) | 1: `Status` `Int32` (`i=6`) |
| `Start` | 0 | 1: `Status` `Int32` (`i=6`) |
| `Stop` | 1: `StopMode` `Int64` (`i=8`) | 1: `Status` `Int32` (`i=6`) |
| `UnloadProgram` | 0 | 1: `Status` `Int32` (`i=6`) |
| `UnloadByName` | 1: `Name` `String` (`i=12`) | 1: `Status` `Int32` (`i=6`) |
| `UnloadByNodeId` | 1: `Id` `ExpandedNodeId` (`i=18`) | 1: `Status` `Int32` (`i=6`) |
| `ReadySubstateMachine/ResetToProgramStart` | 0 | 1: `Status` `Int32` (`i=6`) |

These official TaskControlOperation methods are now callable from OPC UA clients such as UaExpert and use the same reference program executor command path as the temporary `RemotePrograms` methods and browser HTTP control bridge:

| Official method | Current behavior |
| --- | --- |
| `LoadByName(Name)` | Loads the current disk JSON for `pick-and-place-demo` or `axis-range-demo` by name. The JSON file is read fresh for every call. Updates `TaskProgramLoaded` and `TaskProgramName`. |
| `Start()` | Starts the currently loaded reference program. |
| `Stop(StopMode)` | Stops the current reference program. `StopMode` must be `Int64`; `0` is accepted by the demo. |
| `UnloadProgram()` | Clears the currently loaded reference program and returns TaskControl to `Idle`. |
| `UnloadByName(Name)` | Clears the currently loaded reference program when `Name` matches the loaded program name. |
| `ReadySubstateMachine/ResetToProgramStart()` | Resets a loaded, non-running program to the first step. |

These official methods are bound but intentionally return `Bad_NotSupported` and a non-success official `Status` output because the reference program model does not yet expose vendor-native program modules as OPC UA `FileType` nodes:

| Official method | Current behavior |
| --- | --- |
| `LoadByNodeId(Id)` | Unsupported. NodeId-based program loading is not faked. |
| `UnloadByNodeId(Id)` | Unsupported. NodeId-based program unloading is not faked. |

TaskControl state variables are updated from the reference program executor. No program loaded maps to `Idle`; a loaded but non-running program maps to `Ready`; a running program maps to `Executing`; a manual stop or pause maps back to `Ready` with `ReadySubstateMachine/Suspended`; load, reset, and natural completion map to `ReadySubstateMachine/AtProgramStart`.

Following OPC UA Part 16 section 4.5.2, `ReadySubstateMachine` is active only while `TaskControlStateMachine` is `Ready`. When TaskControl is `Idle` or `Executing`, reads of `ReadySubstateMachine/CurrentState`, `ReadySubstateMachine/CurrentState/Id`, `ReadySubstateMachine/LastTransition`, and `ReadySubstateMachine/LastTransition/Id` return `Bad_StateNotActive`. The variables retain their last values internally and return to `Good` when TaskControl re-enters `Ready`.

Observable TaskControl substate behavior:

| Demo action | Observable TaskControl behavior |
| --- | --- |
| `LoadByName(Name)` success | `TaskControlStateMachine = Ready`, `LastTransition = IdleToReady` when loading from `Idle`, `ReadySubstateMachine = AtProgramStart`, `TaskProgramLoaded = true`, `TaskProgramName = Name`. |
| `Start()` success | `TaskControlStateMachine = Executing`, `LastTransition = ReadyToExecuting`; the ready substate is retained internally but is not treated as active while the main state is `Executing`. |
| Manual `Stop(0)` during execution | Stops the program without resetting the executor pointer, returns `TaskControlStateMachine = Ready`, sets `ReadySubstateMachine = Suspended`, and uses `ProgramStartToSuspended` when the previous ready substate was `AtProgramStart`. |
| Natural program completion | Returns `TaskControlStateMachine = Ready`, resets the executor pointer to step 0, and sets `ReadySubstateMachine = AtProgramStart`. |
| `ResetToProgramStart()` success | Valid for a loaded, non-running program; resets the executor pointer to step 0 and sets `ReadySubstateMachine = AtProgramStart`. |
| `UnloadProgram()` success | Returns `TaskControlStateMachine = Idle`, clears `TaskProgramLoaded` and `TaskProgramName`; the ready substate may retain its last value but is not active while TaskControl is `Idle`. |

`LastTransitionReason` uses `1 External` for UaExpert/client-triggered methods and `3 System` for timer-detected natural completion. `5 Application` is not currently used by the reference demo.

TaskControl `CurrentState/Id` and `LastTransition/Id` point to official type-level NodeIds: `ns=3;i=5040` Idle, `ns=3;i=5041` Ready, `ns=3;i=5042` Executing, `ns=3;i=5043` IdleToIdle, `ns=3;i=5044` IdleToReady, `ns=3;i=5045` ReadyToIdle, `ns=3;i=5046` ReadyToExecuting, `ns=3;i=5047` ExecutingToReady, and `ns=3;i=5048` ExecutingToIdle. Ready substate IDs point to `ns=3;i=5023` AtProgramStart, `ns=3;i=5024` Suspended, `ns=3;i=5025` ProgramStartToSuspended, and `ns=3;i=5026` SuspendedToProgramStart.

The TaskControl instance NodeIds use the controller/task-controls hierarchy consistently, for example `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskControlOperation`.

The MinimalRealistic instance NodeSet also includes the official `SystemOperation` AddIn directly below `MainController`. `MainController` has a forward `HasAddIn` reference to `SystemOperation`; `SystemOperation` has the inverse `HasAddIn` reference back to `MainController`, type definition `SystemOperationType`, and a mandatory `SystemOperationStateMachine` child typed as `SystemOperationStateMachineType`.

The SystemOperation state machine includes official methods `Start`, `Stop`, `GetReady`, and `StandDown`. Its `Stop` method uses the local official `StopMode` input data type `i=8`, and all four methods expose the official `Status` output data type `i=6`. SystemOperation `CurrentState/Id` and `LastTransition/Id` point to official type-level NodeIds: `ns=3;i=5030` Idle, `ns=3;i=5031` Ready, `ns=3;i=5032` Executing, `ns=3;i=5033` IdleToIdle, `ns=3;i=5034` IdleToReady, `ns=3;i=5035` ReadyToIdle, `ns=3;i=5036` ReadyToExecuting, `ns=3;i=5037` ExecutingToReady, and `ns=3;i=5038` ExecutingToIdle. Idle substate IDs point to `ns=3;i=5015` StandBy, `ns=3;i=5017` GettingReady, `ns=3;i=5018` GettingReadyToStandBy, and `ns=3;i=5019` StandByToGettingReady. Executing substate IDs point to `ns=3;i=5020` Running, `ns=3;i=5021` Stopping, and `ns=3;i=5022` RunningToStopping.

Official SystemOperation signatures:

| Official method | Input count and types | Output count and types |
| --- | --- | --- |
| `GetReady` | 0 | 1: `Status` `Int32` (`i=6`) |
| `Start` | 0 | 1: `Status` `Int32` (`i=6`) |
| `Stop` | 1: `StopMode` `Int64` (`i=8`) | 1: `Status` `Int32` (`i=6`) |
| `StandDown` | 0 | 1: `Status` `Int32` (`i=6`) |

The server binds SystemOperation state monitoring, command methods, and the Idle/Executing substate machines. Initial state is `SystemOperationStateMachine = Idle`, `IdleSubstateMachine = StandBy`, and `ExecutingSubstateMachine = Running` as an initialized but inactive substate.

Following OPC UA Part 16 section 4.5.2, `IdleSubstateMachine` is active only while `SystemOperationStateMachine` is `Idle`, and `ExecutingSubstateMachine` is active only while `SystemOperationStateMachine` is `Executing`. When either substate machine is inactive, reads of its `CurrentState`, `CurrentState/Id`, `LastTransition`, and `LastTransition/Id` variables return `Bad_StateNotActive`. The variables retain their last values internally and return to `Good` when SystemOperation re-enters the owning state.

Observable SystemOperation substate behavior:

| Demo action | Observable SystemOperation behavior |
| --- | --- |
| `GetReady()` from `Idle` | `IdleSubstateMachine` changes `StandBy -> GettingReady`, waits about 1.2 s, then `SystemOperationStateMachine = Ready` with `LastTransition = IdleToReady`. |
| `GetReady()` from `Ready` | Returns `Status = 0` and keeps state stable. |
| `GetReady()` from `Executing` | Returns `Status = 1` / invalid state. |
| `Start()` from `Ready` | Sets `SystemOperationStateMachine = Executing`, `LastTransition = ReadyToExecuting`, and `ExecutingSubstateMachine = Running`; if a task program is loaded it is started through the existing executor path. Starting with no loaded program is accepted as a controller-level demo start. |
| `Start()` from `Idle` | Returns `Status = 1`; call `GetReady()` first. |
| `Start()` from `Executing` | Returns `Status = 0` and keeps state stable. |
| `Stop(0)` from `Executing` | Sets `ExecutingSubstateMachine` `Running -> Stopping`, stops the running program if one exists, waits about 1.2 s, then sets `SystemOperationStateMachine = Ready` with `LastTransition = ExecutingToReady`. |
| `Stop(0)` from `Ready` | Returns `Status = 0` and keeps state stable. |
| `Stop(0)` from `Idle` | Returns `Status = 1` / invalid state. |
| `StandDown()` from `Ready` or `Idle` | Sets `SystemOperationStateMachine = Idle`, sets `IdleSubstateMachine = StandBy`, and returns `Status = 0`. |
| `StandDown()` from `Executing` | Returns `Status = 1`; the safer demo behavior is to require `Stop(0)` first. |

Other `StopMode` values return unsupported with the correct one-output official method signature.

## Implementation Status

The current implementation has a working minimal OPC UA server, a neutral simulated six-axis robot, live telemetry updates, simple `RemoteControl` methods, JSON robot program contracts, exposed `RemotePrograms` methods, imported official DI/Robotics type NodeSets, the MinimalRealistic instance NodeSet, selected simulation and program metadata bindings into that imported instance model, callable official TaskControlOperation and SystemOperation methods for the supported reference demo operations, observable official TaskControl/SystemOperation substate-machine bindings, disk-backed sample program reload, and runtime address-space mode selection.

The temporary demo nodes remain available during migration and should stay in place until the official instance NodeSet model is richer, bound, and verified.
