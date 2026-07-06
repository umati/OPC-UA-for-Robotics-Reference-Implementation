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
