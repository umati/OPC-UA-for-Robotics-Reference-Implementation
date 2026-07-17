# Instance NodeSets

This folder is reserved for custom OPC UA instance NodeSets owned by this reference implementation.

The first instance NodeSet variant is:

```text
SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml
```

It is intended as a realistic minimal OPC UA Robotics representation of one neutral simulated six-axis robot. It does not attempt to expose every optional Robotics information model element.

The instance NodeSet contains static robot structure plus selected official TaskControl remote-operation nodes. Runtime server code locates selected nodes from the loaded instance model and binds values from the existing `RobotSimulationService`.

`SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml` includes `Controller/MainController/TaskControls/MainTaskControl/TaskControlOperation` as the official `TaskControlOperationType` AddIn using `HasAddIn`. The AddIn contains a `TaskControlStateMachine` with the official TaskControl methods and ready substate machine structure intended for standards-aligned remote-operation binding.

StateMachine instances intentionally do not copy all type-level `StateType` and `TransitionType` children. Runtime `CurrentState/Id` and `LastTransition/Id` values point to the official Robotics type-level NodeIds from the local Robotics NodeSet.

Additional instance NodeSet variants may be added later for richer reference behavior, demos, and negative conformance test targets.

Generated files and official type NodeSets should not be manually edited. Custom instance NodeSets may be created with UA Modeler, SiOME, or a future generator.
