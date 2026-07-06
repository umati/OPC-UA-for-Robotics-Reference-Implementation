# Instance NodeSets

This folder is reserved for custom OPC UA instance NodeSets owned by this reference implementation.

The planned first instance NodeSet is:

```text
SixAxisRobot.Instance.NodeSet2.xml
```

That instance NodeSet will contain the static OPC UA Robotics object structure for one neutral simulated six-axis robot. Runtime server code will later locate selected nodes from the loaded instance model and bind simulation values to those nodes.

Generated files and official type NodeSets should not be manually edited. Custom instance NodeSets may be created with UA Modeler, SiOME, or a future generator.
