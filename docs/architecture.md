# Architecture

This project builds reference implementations for OPC UA for Robotics v1.02. The first deliverable is the OPC UA server.

The server uses the OPC Foundation .NET SDK. The Robotics companion specification should not be manually recreated by hand; official NodeSet files should be treated as the source of truth for the information model.

## Server Layers

1. OPC UA host
2. Information model loading/instantiation
3. Robot simulation with physically plausible joint-space motion, including joint limits, velocity limits, acceleration limits, and smooth deceleration
4. Program execution

## Future Layers

1. Client dashboard
2. Remote operation client
3. 3D visualization
