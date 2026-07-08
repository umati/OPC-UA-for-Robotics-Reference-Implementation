# OPC UA Robotics Notes

## Namespace

Robotics namespace URI:

`http://opcfoundation.org/UA/Robotics/`

## Demo Robot

The demo robot is a neutral simulated 6-axis industrial robot.

It is not intended to represent any specific commercial robot brand, model, or product.

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

## Model Generation Strategy

UA-ModelCompiler will generate .NET classes and constants from the official OPC UA NodeSet files for OPC UA Robotics v1.02.

Generated code will be isolated in a separate generated project and should not be manually edited.

The generation script is `tools/modelcompiler/generate-robotics-model.ps1`. It reads official NodeSet XML files from `src/Robotics.ReferenceServer/NodeSets/` and writes generated output to `generated/opcua-di/` and `generated/opcua-robotics/`.

The current generated model target is OPC UA Robotics. OPC UA DI is included as the required dependency and is generated first because Robotics generated code references DI generated classes such as `ComponentTypeState`. OPC UA IA and OPC UA Machinery remain future optional integration layers and are not part of the current generation target.

Generated files are linked into `src/Robotics.OpcUa.RoboticsModel.Generated/` so generated model code remains separate from hand-written server code. The generated project intentionally includes both DI and Robotics generated code for now.

The generated Robotics model code is now isolated in its own project and the reference server references that project as preparation for official Robotics model instantiation. The server can expose the temporary demo address space, the official DI/Robotics type NodeSets plus the MinimalRealistic instance NodeSet, or both through runtime model mode selection. It binds selected imported instance variables to simulation values. It does not instantiate official Robotics objects programmatically yet.

Model mode selection allows migration from temporary demo nodes to official instance nodes without breaking existing clients during the transition. The current temporary demo nodes will later be mapped to the generated Robotics types so the server can instantiate the official Robotics model objects.

## Instance NodeSet Strategy

The planned official robot structure will come from a custom instance NodeSet, starting with `SixAxisRobot.Instance.NodeSet2.xml`. That instance NodeSet will describe the static OPC UA Robotics object structure for one neutral simulated six-axis robot.

Runtime values will continue to come from the simulation. Server code should locate selected loaded instance nodes and bind those nodes to simulation values instead of programmatically recreating the full Robotics information model in C#.

This hybrid approach keeps the reference implementation maintainable: official and custom model structure stays in NodeSet files, while runtime code focuses on binding live values, methods, and behavior.

Instance NodeSet variants will allow the same simulated six-axis robot backend to be exposed through different OPC UA Robotics representations. The planned robot model variant strategy is documented in `docs/robotics-model-variants.md`.

## First Instance NodeSet

The first instance NodeSet is `src/Robotics.ReferenceServer/NodeSets/Instances/SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml`.

It is intentionally minimal and is intended to validate the instance NodeSet loading and runtime binding chain before a richer robot model variant is added. It includes one `MotionDeviceSystemType`, one `MotionDeviceType`, six `AxisType` axes, required controller/task/safety structure, and the mandatory powertrain/motor structure identified from the local official Robotics NodeSet.

This instance NodeSet does not claim full coverage of the entire OPC UA Robotics specification. It is a first conformance test target for the static structure and future simulation binding path.

The OPC UA base `RequiredModel` value uses the base model version and publication date declared by the local DI NodeSet. The local Robotics NodeSet declares an older OPC UA base requirement, so this should be rechecked when the official type NodeSet set is normalized.

## NodeSet Loading Chain

The server-side NodeSet loading chain has started.

The reference server now explicitly loads the local OPC UA DI NodeSet, the local OPC UA Robotics NodeSet, and the first MinimalRealistic instance NodeSet. OPC UA IA and OPC UA Machinery NodeSets are intentionally not loaded yet.

The imported instance model now has selected runtime bindings. The first binding pass updates robot identity, axis position, axis velocity, and motor temperature where those variables exist in the MinimalRealistic NodeSet.

## Runtime Binding

Runtime binding has started for the MinimalRealistic instance model.

Binding uses stable NodeIds from the instance NodeSet, with reliable BrowsePaths reserved for future cases where a stable NodeId is not available. `DisplayName` should not be used for binding because it is client-facing text, not an identity contract.

The current MinimalRealistic NodeSet does not define target-position or motor-load runtime variables, so those simulation values are skipped until a richer instance NodeSet adds matching nodes.

## Remote Operation, TaskControl, and SystemOperation

The temporary `RemoteControl` and `RemotePrograms` objects are local demo controls. They are useful for exercising the simulation, command service, telemetry stream, browser control bridge, and JSON program executor, but they are not the official OPC UA Robotics remote-operation model.

The local official Robotics NodeSet defines the relevant TaskControl concepts in `TaskControlType`, `TaskControlOperationType`, and `TaskControlStateMachineType`. The generated Robotics constants and classes identify official TaskControl operation methods including `Start`, `Stop`, `LoadByName`, `LoadByNodeId`, `UnloadProgram`, `UnloadByName`, and `UnloadByNodeId`. The generated constants also identify the reset-equivalent `ResetToProgramStart` method on `ReadySubstateMachineType`. The generated method signatures expose one integer `Status` output. In the inspected local NodeSet, `Stop` takes `StopMode` with data type `i=8`; `LoadByName` and `UnloadByName` take a `String Name`; `LoadByNodeId` and `UnloadByNodeId` take an `ExpandedNodeId Id`.

Official Robotics method callbacks must return exactly the declared `OutputArguments` count and data types. For the inspected TaskControl, ReadySubstateMachine, and SystemOperation methods, the official output list is exactly one `Int32 Status` value. Diagnostic messages are logged to the console or exposed through demo status variables; they are not returned as a second official method output.

The current MinimalRealistic instance NodeSet includes a `TaskControls` folder, a `MainTaskControl` object typed as `TaskControlType`, and the official `TaskProgramLoaded` and `TaskProgramName` variables under its `ParameterSet`. These are basic `TaskControlType` parameter variables, not remote-operation method nodes. The server updates those two variables from the existing `RobotProgramExecutor` snapshot using their stable instance NodeIds:

- `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskProgramLoaded`
- `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskProgramName`

Remote operation is a separate optional add-in below `TaskControlType`: `TaskControlOperation` contains `TaskControlStateMachine`, and the official methods belong to that state machine. `TaskControlType` parameters and the AddIn have different roles:

- `TaskProgramLoaded` and `TaskProgramName` are TaskControl parameter variables that report program metadata.
- `TaskControlOperation` is the official remote-operation AddIn. It is referenced from the TaskControl by `HasAddIn`.
- `TaskControlStateMachine` carries official command methods, current/last transition variables, and the `ReadySubstateMachine`. The state and transition definitions come from `TaskControlStateMachineType`.

The MinimalRealistic instance NodeSet now includes the official `TaskControlOperation` AddIn under `MainTaskControl`, typed as `TaskControlOperationType`, with `TaskControlStateMachine` typed as `TaskControlStateMachineType`. Following OPC UA Part 16 section 4.5, the instance does not duplicate the type-level `StateType` and `TransitionType` objects. The state and transition objects are defined by the official Robotics `TaskControlStateMachineType` and `ReadySubstateMachineType`; the instance exposes instance-specific variables such as `CurrentState`, `CurrentState/Id`, `LastTransition`, `LastTransition/Id`, and `LastTransitionReason`.

`AvailableStates` and `AvailableTransitions` were not added because the local official Robotics NodeSet loaded by this server does not expose those children on the relevant Robotics StateMachineTypes.

The previous copied instance children `Idle`, `Ready`, `Executing`, `IdleToIdle`, `IdleToReady`, `ReadyToIdle`, `ReadyToExecuting`, `ExecutingToReady`, `ExecutingToIdle`, `AtProgramStart`, `Suspended`, `ProgramStartToSuspended`, and `SuspendedToProgramStart` were removed. They mirrored the type definition and made clients see duplicate physical state/transition nodes below each StateMachine instance.

OPC UA Part 16 section 4.5.2 also defines the read semantics for SubStateMachines that are used only in a particular parent state: when the parent StateMachine is not in that owning state, a client reading the SubStateMachine `CurrentState` receives `Bad_StateNotActive`. The reference server applies the same inactive read status to the SubStateMachine `CurrentState/Id`, `LastTransition`, and `LastTransition/Id` variables. It keeps the last internal value for continuity, but exposes `BadStateNotActive` while the substate machine is inactive and returns to `Good` when the parent state re-enters the owning state.

Robotics substate-machine active conditions:

| Substate machine | Active only while parent state is |
| --- | --- |
| `SystemOperationStateMachine/IdleSubstateMachine` | `SystemOperationStateMachine = Idle` |
| `SystemOperationStateMachine/ExecutingSubstateMachine` | `SystemOperationStateMachine = Executing` |
| `TaskControlStateMachine/ReadySubstateMachine` | `TaskControlStateMachine = Ready` |

The official TaskControlOperation method nodes are bound to the existing reference program command service instead of duplicating command logic. The same command service is used by:

- Temporary OPC UA `RemotePrograms` methods.
- The browser HTTP control bridge.
- Official `TaskControlOperation/TaskControlStateMachine` methods.

Current official TaskControlOperation mapping:

| Official method | Reference implementation mapping |
| --- | --- |
| `LoadByName(Name)` | Loads the current sample JSON file from disk by normalized sample name. Supported names are `pick-and-place-demo` and `axis-range-demo`. |
| `Start()` | Calls the existing `RobotProgramExecutor.Start()` path through the shared command service. |
| `Stop(StopMode)` | Requires one `Int64 StopMode`. `StopMode = 0` calls the existing stop path. Other stop modes return unsupported until stop-mode-specific behavior exists. |
| `UnloadProgram()` | Clears the currently loaded reference program and returns to no loaded program. |
| `UnloadByName(Name)` | Clears the currently loaded reference program only when `Name` matches the loaded program name. |
| `ResetToProgramStart()` | Resets a loaded, non-running reference program to step 0. |

`LoadByNodeId(Id)` and `UnloadByNodeId(Id)` are intentionally unsupported. The current reference program store is JSON/sample-name based and does not expose vendor-native program modules or OPC UA `FileType` instances for task programs. These methods return `Bad_NotSupported` and a non-success official `Status` output instead of pretending that NodeId loading exists.

`LoadByName` and the temporary `LoadSampleProgram` resolve sample files by walking upward from the current directory and `AppContext.BaseDirectory`. The resolver prefers `src/Robotics.ReferenceServer/RobotPrograms/SamplePrograms/` in the repository, then a colocated `RobotPrograms/SamplePrograms/` directory. The server reads and deserializes the JSON file on every load call and logs the normalized program name, full source path, and step count. Unloading and loading again therefore picks up edits to `axis-range-demo.json` or `pick-and-place-demo.json` without rebuilding the server.

TaskControl state variables are mapped conservatively from `RobotProgramExecutionState`:

| Executor state | Official TaskControl state |
| --- | --- |
| No loaded program | `Idle` |
| Loaded, completed, or reset program | `Ready` plus `ReadySubstateMachine/AtProgramStart` |
| Running program | `Executing` |
| Paused or manually stopped program | `Ready` plus `ReadySubstateMachine/Suspended` |
| Unloaded program | `Idle`; the ready substate is not active |

`CurrentState/Id` and `LastTransition/Id` are set to official Robotics type-level NodeIds. For TaskControl these are `ns=3;i=5040` Idle, `ns=3;i=5041` Ready, `ns=3;i=5042` Executing, `ns=3;i=5043` IdleToIdle, `ns=3;i=5044` IdleToReady, `ns=3;i=5045` ReadyToIdle, `ns=3;i=5046` ReadyToExecuting, `ns=3;i=5047` ExecutingToReady, and `ns=3;i=5048` ExecutingToIdle. For the ready substate machine they are `ns=3;i=5023` AtProgramStart, `ns=3;i=5024` Suspended, `ns=3;i=5025` ProgramStartToSuspended, and `ns=3;i=5026` SuspendedToProgramStart.

Robotics v1.02 uses `ReadySubstateMachineType` to tell a client whether the loaded program pointer is at the beginning or suspended somewhere in the program. The reference demo now makes this observable: `LoadByName`, natural completion, and `ResetToProgramStart` expose `AtProgramStart`; manual `Stop(0)` during execution exposes `Suspended`. `ResetToProgramStart` is the explicit operation that returns the executor pointer to step 0.

The local official Robotics NodeSet also defines `SystemOperationType` and `SystemOperationStateMachineType`. `ControllerType` has an optional `SystemOperation` AddIn referenced by `HasAddIn`; that AddIn is typed as `SystemOperationType` and contains the mandatory `SystemOperationStateMachine` child typed as `SystemOperationStateMachineType`.

`TaskControlOperation` and `SystemOperation` are separate remote-operation surfaces. `TaskControlOperation` is task/program-level and belongs under a `TaskControlType` instance. `SystemOperation` is controller/system-level and belongs directly under a `ControllerType` instance. In MinimalRealistic, `TaskControlOperation` stays under `MainTaskControl`, while `SystemOperation` is under `MainController`.

The MinimalRealistic instance NodeSet now includes the official `SystemOperation` AddIn under `MainController`. It uses the stable NodeIds:

- `SixAxisRobot.MinimalRealistic/Controller/MainController/SystemOperation`
- `SixAxisRobot.MinimalRealistic/Controller/MainController/SystemOperation/SystemOperationStateMachine`

The instantiated SystemOperation state machine includes `IdleSubstateMachine`, `ExecutingSubstateMachine`, `PossibleStopModes`, and official methods `Start`, `Stop`, `GetReady`, and `StandDown`. It does not duplicate the `SystemOperationStateMachineType` state and transition objects. Runtime state IDs point to official type-level NodeIds: `ns=3;i=5030` Idle, `ns=3;i=5031` Ready, `ns=3;i=5032` Executing, `ns=3;i=5033` IdleToIdle, `ns=3;i=5034` IdleToReady, `ns=3;i=5035` ReadyToIdle, `ns=3;i=5036` ReadyToExecuting, `ns=3;i=5037` ExecutingToReady, and `ns=3;i=5038` ExecutingToIdle. Idle substate IDs point to `ns=3;i=5015` StandBy, `ns=3;i=5017` GettingReady, `ns=3;i=5018` GettingReadyToStandBy, and `ns=3;i=5019` StandByToGettingReady. Executing substate IDs point to `ns=3;i=5020` Running, `ns=3;i=5021` Stopping, and `ns=3;i=5022` RunningToStopping. The inspected local NodeSet defines `Status` output data type `i=6` for these methods and `StopMode` input data type `i=8` for `Stop`.

Robotics v1.02 uses `IdleSubstateMachineType` when the `Idle -> Ready` transition takes significant time and `ExecutingSubstateMachineType` when stopping from `Executing -> Ready` takes significant time. These substates let a client see more information while the main state is still transitioning. The demo reflects that intent with short observable delays: `GetReady()` shows `StandBy -> GettingReady` before the main state becomes `Ready`, and `Stop(0)` from `Executing` shows `Running -> Stopping` before the main state becomes `Ready`.

The server binds SystemOperation state monitoring and the official `GetReady`, `Start`, `Stop`, and `StandDown` methods with the exact local NodeSet signatures. The conservative demo mapping keeps `TaskControlOperation` as the task/program surface and uses `SystemOperation` as the controller/system surface:

| SystemOperation method | Demo mapping |
| --- | --- |
| `GetReady()` | From `Idle`, shows `IdleSubstateMachine` `StandBy -> GettingReady`, waits about 1.2 s, then sets SystemOperation to `Ready`; from `Ready`, returns success without changing state; from `Executing`, returns `Status = 1`. |
| `Start()` | From `Ready`, sets SystemOperation to `Executing`, sets `ExecutingSubstateMachine = Running`, and starts the loaded task program when one exists. From `Idle`, returns `Status = 1`; from `Executing`, returns success without changing state. |
| `Stop(StopMode)` | With `StopMode = 0` from `Executing`, shows `Running -> Stopping`, stops the loaded program when one is running, waits about 1.2 s, then returns SystemOperation to `Ready`. From `Ready`, returns success without changing state; from `Idle`, returns `Status = 1`. Other stop modes are unsupported. |
| `StandDown()` | From `Ready` or `Idle`, maps SystemOperation to `Idle` and sets `IdleSubstateMachine = StandBy`. From `Executing`, returns `Status = 1`; the demo requires `Stop(0)` first. |

`LastTransitionReason` is mapped conservatively: `1 External` for UaExpert/client-triggered methods and `3 System` for internal timer-detected natural completion. `5 Application` is documented by the specification but not currently used by this reference path.

The TaskControl NodeIds use the controller/task-controls hierarchy:

- `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl`
- `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskControlOperation`
- `SixAxisRobot.MinimalRealistic/Controller/MainController/TaskControls/MainTaskControl/TaskControlOperation/TaskControlStateMachine`

Current unresolved standard-mapping gaps:

- Define an official program repository or file model before mapping `LoadByNodeId` and `UnloadByNodeId` to real program loading/unloading.
- Add stop-mode-specific executor behavior if the reference implementation needs to distinguish official `StopMode` values.
- Decide how temporary pause/resume behavior maps to official state-machine concepts. The inspected Robotics TaskControl method set does not define methods named `Pause` or `Resume`.

## Motion Validation

The reference server includes an offline motion validation report mode:

`--validate-motion`

This mode exercises the existing `RobotSimulationService` without starting the OPC UA server, loading NodeSets, using UaExpert, or requiring an OPC UA client. It checks simulated joint-space motion before visualization or demo usage by running representative moves, collecting per-axis position, velocity, acceleration, joint-limit, and teleport-like jump observations, and reporting an overall PASS/FAIL result.

Motion validation checks bounded physical plausibility and uses tolerance-based settling instead of exact target equality or exact zero velocity. Finite target moves must settle within the configured validation tolerance and timeout; bounded demo-style motion is checked for safety-limit and teleport-like violations over a fixed simulated duration.

Axis motion snaps to the target only inside a small final tolerance and only when the remaining velocity can be removed within the current integration step without exceeding the axis acceleration limit. This avoids residual limit-cycle motion near the target while preserving acceleration-limited movement outside the final settling window.

## Browser Visualization Telemetry

Visualization V2 consumes server telemetry from `ws://localhost:48080/telemetry` while the reference server is running.

The browser visualization does not define robot physics, motion validation, program execution, or simulation behavior. It renders the current joint positions and status fields emitted by the server-side simulation, which remains the source of truth.

Visualization V6 can also show program metadata from the server telemetry stream when those optional fields are present. The current implementation can expose the loaded program name, execution state, step index/count, step type, and server-known joint targets for the active and next move steps. The browser uses those values only for approximate visual target markers, ghost tool pose, and future path preview.

Visualization V7 adds a local browser demo/operator UI that sends command requests to a lightweight HTTP bridge at `http://localhost:48080/control`. That bridge is server-side infrastructure for local demos. It reuses the same command handling as the temporary `RemoteControl` and `RemotePrograms` method callbacks where possible, while telemetry continues to stream from `ws://localhost:48080/telemetry`.

The architecture is intentionally separated:

- Browser demo/operator UI: renders telemetry and sends local demo commands.
- Local command bridge: accepts browser HTTP requests and dispatches them to server-owned command handling.
- OPC UA server and simulation: remain the authoritative state and motion source.
- Future standards-pure OPC UA client: should call OPC UA services/methods directly instead of using the local browser bridge.

This telemetry and command preview is not the final OPC UA Robotics standards mapping. The temporary `RemotePrograms` behavior, visualization metadata, and bridge commands still need to be mapped to official Robotics TaskControl concepts in a later standards-aligned milestone. A future `Robotics.ReferenceClient` should provide a .NET OPC UA client/conformance-client path for direct standards-oriented validation.
