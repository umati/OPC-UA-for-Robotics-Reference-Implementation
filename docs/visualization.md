# Visualization

Visualization V2 is a browser-based Three.js robot viewer that can render live joint telemetry from the running `Robotics.ReferenceServer`.

The browser does not connect to OPC UA directly and does not define robot physics. It renders the latest telemetry snapshot produced by the server simulation.

## Run the Reference Server

From the repository root:

```powershell
dotnet run --project src/Robotics.ReferenceServer
```

The server starts the OPC UA endpoint and, when available, the telemetry WebSocket endpoint:

```text
Telemetry WebSocket endpoint: ws://localhost:48080/telemetry
```

If the telemetry socket cannot bind, the server prints a warning and keeps the OPC UA server running.

## Run the Visualization

From the visualization project folder:

```powershell
cd src/Robotics.Visualization.Web
npm install
npm run dev
```

Then open the local URL printed by Vite.

## Connect Live Telemetry

1. Start `Robotics.ReferenceServer`.
2. Start the visualization with Vite.
3. In the browser side panel, keep the WebSocket URL set to:

```text
ws://localhost:48080/telemetry
```

4. Select `Connect`.

When connected, the visualization enters Live Telemetry Mode, disables manual sliders and Local Demo controls, and applies incoming `S`, `L`, `U`, `R`, `B`, and `T` axis positions to the robot model. If the socket disconnects, the last pose remains visible and manual controls become available again.

## Current Scope

- Vite, TypeScript, and Three.js.
- Perspective camera with orbit controls.
- Dark scene, grid floor, world coordinate axes, and basic lighting.
- Six-axis placeholder robot built from simple Three.js primitives.
- Kinematic hierarchy for S, L, U, R, B, T, and tool groups.
- Manual joint sliders with degree values.
- Local Demo motion retained from V1.
- Live telemetry WebSocket client for `ws://localhost:48080/telemetry`.
- Telemetry panel for joint position, joint velocity, program state, current step, moving state, and timestamp.

## V2 Limitations

- The WebSocket bridge streams JSON snapshots only; command/control remains through existing server mechanisms.
- The browser uses the placeholder Three.js robot, not an external GLB robot model.
- Broadcasts repeat the latest server simulation snapshot and do not increase simulation fidelity.
- Missing or invalid axis fields are ignored by the browser and leave the last valid pose value in place.
- No path, target frame, ghost pose, or program overlay visualization yet.
- No physics simulation in the browser.

## V3 Plan

V3 should add path curves, target frames, ghost pose comparison, and program overlay features for robot program review and debugging.
