# Instance NodeSets

This folder is reserved for custom OPC UA instance NodeSets owned by this reference implementation.

The first instance NodeSet variant is:

```text
SixAxisRobot.MinimalRealistic.Instance.NodeSet2.xml
```

It is intended as a realistic minimal OPC UA Robotics representation of one neutral simulated six-axis robot. It does not attempt to expose every optional Robotics information model element.

The instance NodeSet contains static robot structure only. Runtime server code will later locate selected nodes from the loaded instance model and bind values from the existing `RobotSimulationService`.

Additional instance NodeSet variants may be added later for richer reference behavior, demos, and negative conformance test targets.

Generated files and official type NodeSets should not be manually edited. Custom instance NodeSets may be created with UA Modeler, SiOME, or a future generator.
