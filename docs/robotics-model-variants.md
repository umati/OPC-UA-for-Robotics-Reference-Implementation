# Robotics Model Variants

Real robots may implement only a subset of the OPC UA for Robotics information model. A useful reference implementation should therefore support multiple robot model variants instead of assuming one perfect robot model.

All planned robot model variants can connect to the same neutral simulated six-axis robot backend. The simulation backend and the OPC UA information model representation are separate concerns:

- `RobotSimulationService` provides movement and runtime state.
- Instance NodeSet variants provide different OPC UA representations of the robot.
- `RobotNodeBinder` maps simulation values to whichever instance NodeSet variant is loaded.

In OPC UA, "Profile" is formal terminology and should not be overloaded for these server/model choices. This project uses robot model variant, instance NodeSet variant, conformance test target, and negative test variant instead.

## Planned Variants

| Variant | Purpose | Intended use | Expected conformance behavior | Included model areas | Known missing areas | Intentional failures |
| --- | --- | --- | --- | --- | --- | --- |
| MinimalRealistic | Represent a small but plausible industrial robot implementation. | Baseline reference-quality robot model variant for early integration; first instance NodeSet is `SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml`. | Expected to pass checks for the model areas it claims to implement. | Identity, basic motion device structure, selected axes, selected runtime telemetry. | Rich diagnostics, advanced task control, condition monitoring, extended asset data. | No. |
| RichReference | Exercise a broad reference-quality Robotics model. | Main long-term reference implementation and documentation target. | Expected to pass broad conformance checks once complete. | Identity, motion devices, axes, controllers, task controls, runtime telemetry, richer metadata. | Areas not yet implemented by the project milestone. | No. |
| RemoteOperationDemo | Emphasize remote control and program execution workflows. | Demo clients, tutorials, method behavior experiments. | Expected to pass structural checks for included nodes, but may include demo-oriented methods beyond the reference-quality core. | Identity, motion device structure, RemoteControl, RemotePrograms, command status. | Full condition monitoring, full asset management, broad diagnostic coverage. | No. |
| ConditionMonitoringDemo | Emphasize diagnostic and condition monitoring data. | Demonstrations of alarms, conditions, status variables, and health dashboards. | Expected to pass checks for included monitoring nodes once modeled correctly. | Identity, motion device structure, selected condition and diagnostic areas, telemetry. | Full remote operation coverage, full production asset metadata. | No. |
| NegativeMissingMandatoryNodes | Omit mandatory nodes deliberately. | Conformance unit testing tool validation. | Expected to fail mandatory-node checks. | Enough structure to make the missing mandatory nodes detectable. | Mandatory nodes intentionally removed. | Yes. |
| NegativeWrongDataTypes | Use incorrect DataTypes deliberately. | Conformance unit testing tool validation. | Expected to fail DataType checks. | Nodes with known BrowseNames and intentionally wrong DataTypes. | Correct DataType assignments for targeted nodes. | Yes. |
| NegativeInvalidStateMachine | Model invalid state machine relationships deliberately. | Conformance unit testing tool validation. | Expected to fail state machine checks. | State machine nodes and transitions with deliberate defects. | Valid transition topology for targeted state machine areas. | Yes. |
| NegativeBrokenReferences | Break required references deliberately. | Conformance unit testing tool validation. | Expected to fail reference integrity checks. | Nodes with missing, wrong, or dangling references. | Correct reference graph for targeted areas. | Yes. |

## Variant Categories

Reference-quality variants are intended to become credible examples of how a real OPC UA Robotics server could expose a robot. `MinimalRealistic` should stay compact and plausible. `RichReference` can grow into a broader implementation that exercises more of the companion specification.

Demo variants are designed to make a feature area easy to see and test. `RemoteOperationDemo` and `ConditionMonitoringDemo` may be more focused than a production robot model, but they should still avoid known invalid modeling unless they are explicitly converted into negative test variants.

Negative conformance test targets intentionally contain modeling defects. They exist to prove that a future conformance unit testing tool detects failures, not to demonstrate compliant server behavior.

## Negative Variants

Negative variants should never be presented as compliant reference implementations. They must be clearly labeled as negative test variants in documentation, file names, and test output.

Their only purpose is to validate future conformance checks by creating known-bad instance NodeSet variants. A conformance unit testing tool should fail these variants for the expected reasons and should make unexpected passes visible.

## Future Conformance Unit Testing Ideas

A future conformance unit testing client/tool can load or inspect instance NodeSet variants and validate:

- mandatory nodes
- TypeDefinitions
- DataTypes
- BrowseNames
- references
- state machine transitions
- method signatures
- method StatusCodes
- namespace URIs

The same simulated robot backend can remain unchanged while the conformance test target changes. This keeps runtime behavior and information model validation separate.
