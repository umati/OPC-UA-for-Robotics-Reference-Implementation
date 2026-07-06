# Visualization

Visualization V4A is a browser-based Three.js robot viewer that can render live joint telemetry from the running `Robotics.ReferenceServer`.

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

## V4A Controls

- `Reset Home`: returns the active robot model to the home pose in Manual Mode.
- `Local Demo`: runs the browser-side demo animation retained from V1. Sliders remain visible but disabled because the local animation owns the pose.
- `Stop Demo`: stops the local animation and returns to Manual Mode.
- `Connect` / `Disconnect`: controls the live telemetry WebSocket connection.
- `Reload Model`: attempts to load the segmented GLB robot again and rebuilds the active visual model.
- `World frame`: toggles the base/world coordinate frame helper.
- `Tool frame`: toggles the coordinate frame attached to the end-effector tool group.
- `Grid`: toggles the engineering floor grid.
- `Path trail`: toggles rendering of the recent TCP/tool path.
- `Clear Path`: clears the accumulated path buffer.

## Visualization V4A: Segmented GLB Model Support

V4A attempts to load a segmented GLB robot model from:

```text
public/assets/robots/six-axis-reference.glb
```

At runtime Vite serves that asset as:

```text
/assets/robots/six-axis-reference.glb
```

The GLB file is optional. If it is missing, invalid, or does not contain the expected named nodes, the browser logs a warning, shows `GLB load failed, using primitive fallback` in the side panel, and continues with the existing primitive robot model. Manual sliders, Reset Home, Local Demo, Live Telemetry Mode, path trail, clear path, frame toggles, grid toggle, and status overlays continue to work with whichever model is active.

The model status indicator reports:

- `Primitive fallback model` before the GLB load attempt or when the primitive model is active.
- `GLB model loaded` when the segmented GLB is active.
- `GLB load failed, using primitive fallback` when the GLB could not be used.

The required GLB node names are:

- `RobotRoot`
- `AxisS`
- `AxisL`
- `AxisU`
- `AxisR`
- `AxisB`
- `AxisT`
- `Tool`

A single robot mesh is not enough for this viewer. The browser receives joint angles and applies them as transforms to the S, L, U, R, B, and T axes. Each moving axis must therefore be a separate node or group with its origin at the physical joint pivot. Geometry can be parented under those nodes, but the moving transforms must remain separate.

The expected hierarchy is:

```text
RobotRoot
  AxisS
    AxisL
      AxisU
        AxisR
          AxisB
            AxisT
              Tool
```

The expected local GLB rotation axes are:

- `AxisS`: local Y
- `AxisL`: local Z
- `AxisU`: local Z
- `AxisR`: local X
- `AxisB`: local Z
- `AxisT`: local X

The primitive fallback preserves its current visual behavior and uses the same browser-side mapping, including `B` around local Z. If a future GLB uses a different authoring convention, adjust the GLB node pivots and local axes so the named nodes match this contract.

## Path Trail and Frames

V4A computes the current tool world position from the active model's tool object after joint updates and during animation frames. For the GLB model this is the `Tool` node. For the primitive fallback this is the existing primitive tool group. Recent positions are stored in a bounded buffer and rendered as a Three.js line, giving a live TCP/tool path without adding browser-side physics.

The world frame marks the base scene reference. The tool frame is attached to the active end-effector object, so it follows either the GLB `Tool` node or the primitive tool group.

## Current Scope

- Vite, TypeScript, and Three.js.
- Perspective camera with orbit controls.
- Dark scene, grid floor, world coordinate axes, and basic lighting.
- Segmented GLB robot loading with primitive fallback.
- Six-axis placeholder robot built from simple Three.js primitives.
- Kinematic hierarchy for S, L, U, R, B, T, and tool groups.
- Manual joint sliders with degree values.
- Local Demo motion retained from V1.
- Live telemetry WebSocket client for `ws://localhost:48080/telemetry`.
- Scene overlay and side-panel status for mode, connection state, heartbeat, telemetry age, joint position, joint velocity, program state, current step, moving state, and timestamp.
- Live TCP/tool path trail with clear and visibility controls.
- World/base and tool coordinate frame helpers.
- Side-panel model status and GLB reload control.

## V4A Limitations

- The WebSocket bridge streams JSON snapshots only; command/control remains through existing server mechanisms.
- The GLB asset is optional and may not be present in the repository yet.
- GLB validation checks required node names, but it cannot prove pivots and local axes were authored correctly.
- Broadcasts repeat the latest server simulation snapshot and do not increase simulation fidelity.
- Missing or invalid axis fields are ignored by the browser and leave the last valid pose value in place.
- No target frame, ghost pose, or program timeline overlay visualization yet.
- No physics simulation in the browser.

## V4 Plan

After V4A, target frames, ghost pose comparison, and richer program overlays can be added for robot program review and debugging.
