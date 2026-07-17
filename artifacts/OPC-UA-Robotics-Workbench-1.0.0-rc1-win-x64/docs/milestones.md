# Milestones

## Complete

- Minimal OPC UA server.
- Simulated six-axis robot.
- Live telemetry.
- Simple RemoteControl methods.
- JSON robot program contracts.
- RemotePrograms method exposure.
- UA-ModelCompiler generation script.
- Installed or built UA-ModelCompiler.
- Generated DI model classes.
- Generated Robotics model classes.
- Generated Robotics model project.
- Reference server prepared to consume generated model.
- First MinimalRealistic instance NodeSet created.
- NodeSet loading into server started.
- Official DI and Robotics type NodeSets loaded.
- MinimalRealistic instance NodeSet loaded.
- Bind simulation to MinimalRealistic instance nodes.
- Address-space mode selection for Temporary, Official, and Both models.
- Motion validation report mode.
- Visualization V1: manual-slider browser viewer.
- Visualization V2: live telemetry WebSocket connection.
- Visualization V3: path trail, coordinate frames, polished live overlay.
- Visualization V4A: segmented GLB loading with primitive fallback.
- Visualization V4B: premium procedural fallback robot.
- Visualization V5: presentation demo mode.
- Visualization V6: live program/path preview.
- Visualization V7: browser control panel and JSON program upload through local command bridge.
- Official Robotics TaskControl inspection and partial metadata binding: inspected the local official Robotics NodeSet/generated classes, verified official TaskControl method names/signatures, bound `TaskProgramLoaded` and `TaskProgramName` using the MinimalRealistic instance NodeSet's actual stable NodeIds, and added unavailable diagnostics for missing official method/state-machine instance nodes.
- MinimalRealistic TaskControlOperation AddIn extension added: the instance NodeSet now includes the official `TaskControlOperation` AddIn, `TaskControlStateMachine`, official TaskControl method nodes, and `ReadySubstateMachine` structure.
- MinimalRealistic SystemOperation AddIn extension added: the instance NodeSet now includes the official controller-level `SystemOperation` AddIn under `MainController`, `SystemOperationStateMachine`, official system-operation methods, and idle/executing substate-machine structure.
- Official Robotics TaskControlOperation method binding added: official `LoadByName`, `Start`, `Stop`, `UnloadProgram`, `UnloadByName`, and `ResetToProgramStart` now call the same reference program executor command service used by temporary OPC UA methods and the browser control bridge; `LoadByNodeId` and `UnloadByNodeId` are explicitly unsupported until an OPC UA program file/module model exists.
- StateMachine instance cleanup completed: TaskControl, ReadySubstateMachine, and SystemOperation StateMachine instances no longer duplicate type-level state/transition objects; runtime `CurrentState/Id` and `LastTransition/Id` use official Robotics type-level NodeIds.
- Official Robotics method runtime argument binding fix completed: loaded TaskControl method nodes now have repaired `MethodState.InputArguments`/`MethodState.OutputArguments` metadata for `LoadByName`, `LoadByNodeId`, `Start`, `Stop`, `UnloadProgram`, `UnloadByName`, `UnloadByNodeId`, and `ReadySubstateMachine/ResetToProgramStart`, with startup diagnostics for BrowseName, NodeId, parent NodeId, runtime argument counts, child argument-node presence, and callback assignment.
- Official Robotics TaskControl/SystemOperation state monitoring added: TaskControl state variables and SystemOperation state variables now use official Robotics type-level state and transition NodeIds with conservative executor-state mapping.
- Observable Robotics substate-machine demo behavior implemented: SystemOperation `GetReady` exposes `IdleSubstateMachine` `StandBy -> GettingReady`, SystemOperation `Start` exposes `ExecutingSubstateMachine = Running`, SystemOperation `Stop` exposes `Running -> Stopping -> Ready`, and TaskControl `ReadySubstateMachine` now distinguishes `AtProgramStart` from manual-stop `Suspended`.
- Official Robotics output-argument strictness completed: official TaskControl, ReadySubstateMachine, and SystemOperation callbacks clear and return exactly the declared single `Int32 Status` output, with call diagnostics for method name, received input count, returned output count, and status value.
- Official Robotics SystemOperation method binding completed: `GetReady`, `Start`, `Stop`, and `StandDown` are bound to conservative controller/system demo callbacks with exact local NodeSet signatures instead of returning `Bad_NotImplemented`.
- Disk-backed sample reload completed: `LoadByName` and `LoadSampleProgram` resolve `axis-range-demo` and `pick-and-place-demo` to current sample JSON files on disk, prefer repository sources over copied output files, read JSON fresh for each call, and log source path plus step count.
- OPC UA Part 16 substate active/inactive read semantics implemented: inactive `IdleSubstateMachine`, `ExecutingSubstateMachine`, and `ReadySubstateMachine` state variables now expose `Bad_StateNotActive` read status while retaining their last internal values.

## Next

- Define an OPC UA program repository/file model before implementing `LoadByNodeId` and `UnloadByNodeId`.
- Add stop-mode-specific behavior if the reference executor should distinguish official `StopMode` values.
- Add RichReference instance NodeSet.
- Map remaining temporary demo nodes to official Robotics concepts.
- Instantiate official Robotics model objects.
- Gradually retire temporary demo nodes after official model is working.

## Later

- Add official Robotics TaskControl mapping for target metadata and richer program module/file information.
- Add .NET OPC UA client / conformance client, tentatively `Robotics.ReferenceClient`.
- Add negative conformance test target NodeSets.
- Build conformance unit testing client/tool.
- Expose asset management variables using the official information model.
- Expose condition monitoring variables using the official information model.
- Build client dashboard.
- Extend 3D visualization with richer standards-aligned program timeline overlays.
