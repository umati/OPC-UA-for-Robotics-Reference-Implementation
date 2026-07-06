# NodeSets

This folder must contain the official NodeSet XML files needed to load the OPC UA Robotics information model.

Expected files:

- `Opc.Ua.NodeSet2.xml`
- `Opc.Ua.Di.NodeSet2.xml`
- `Opc.Ua.Robotics.NodeSet2.xml`

These files should come from official OPC Foundation sources or from the official `UA-Nodeset` repository.

The server will load these files at runtime in a later milestone. The current implementation does not load them yet.
