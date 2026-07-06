# Visualization

Visualization V3 is a browser-based Three.js robot viewer that can render live joint telemetry from the running `Robotics.ReferenceServer`.

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

When connected, the visualization enters Live Telemetry Mode, disables manual sliders and Local Demo controls, and applies incoming `S`, `L`, `U`, `R`, `B`, and `T` axis positions to the robot model. The sliders remain visible and their values follow live telemetry. If the socket disconnects, the last pose remains visible and manual controls become available again.

The heartbeat indicator reports:

- `Live` when telemetry messages are less than 1 second old.
- `Stale` when the WebSocket is connected but no telemetry message has arrived for more than 1 second.
- `Disconnected` when the WebSocket is not connected.

## V3 Controls

- `Reset Home`: returns the placeholder robot to the home pose in Manual Mode.
- `Local Demo`: runs the browser-side demo animation retained from V1. Sliders remain visible but disabled because the local animation owns the pose.
- `Stop Demo`: stops the local animation and returns to Manual Mode.
- `Connect` / `Disconnect`: controls the live telemetry WebSocket connection.
- `World frame`: toggles the base/world coordinate frame helper.
- `Tool frame`: toggles the coordinate frame attached to the end-effector tool group.
- `Grid`: toggles the engineering floor grid.
- `Path trail`: toggles rendering of the recent TCP/tool path.
- `Clear Path`: clears the accumulated path buffer.

## Path Trail and Frames

V3 computes the current tool world position from the existing Three.js `toolGroup` after joint updates and during animation frames. Recent positions are stored in a bounded buffer and rendered as a Three.js line, giving a live TCP/tool path without adding browser-side physics.

The world frame marks the base scene reference. The tool frame is attached to the end effector, so it follows the placeholder robot wrist and tool orientation.

## Current Scope

- Vite, TypeScript, and Three.js.
- Perspective camera with orbit controls.
- Dark scene, grid floor, world coordinate axes, and basic lighting.
- Six-axis placeholder robot built from simple Three.js primitives.
- Kinematic hierarchy for S, L, U, R, B, T, and tool groups.
- Manual joint sliders with degree values.
- Local Demo motion retained from V1.
- Live telemetry WebSocket client for `ws://localhost:48080/telemetry`.
- Scene overlay and side-panel status for mode, connection state, heartbeat, telemetry age, joint position, joint velocity, program state, current step, moving state, and timestamp.
- Live TCP/tool path trail with clear and visibility controls.
- World/base and tool coordinate frame helpers.

## V3 Limitations

- The WebSocket bridge streams JSON snapshots only; command/control remains through existing server mechanisms.
- The browser uses the placeholder Three.js robot, not an external GLB robot model.
- Broadcasts repeat the latest server simulation snapshot and do not increase simulation fidelity.
- Missing or invalid axis fields are ignored by the browser and leave the last valid pose value in place.
- No target frame, ghost pose, or program timeline overlay visualization yet.
- No physics simulation in the browser.

## V4 Plan

V4 should replace the primitive placeholder robot with a segmented GLB model whose joints map cleanly to the existing S, L, U, R, B, and T kinematic groups. After that model swap, target frames, ghost pose comparison, and richer program overlays can be added for robot program review and debugging.
