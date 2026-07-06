# Visualization

Visualization V5 is a browser-based Three.js robot viewer that can render live joint telemetry from the running `Robotics.ReferenceServer` and includes a polished one-click presentation demo mode.

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

## V5 Controls

- `Reset Home`: returns the active robot model to the home pose in Manual Mode.
- `Local Demo`: runs the browser-side demo animation retained from V1. Sliders remain visible but disabled because the local animation owns the pose.
- `Stop Demo`: stops the local animation and returns to Manual Mode.
- `Start Jaw-Drop Demo`: enters Presentation Demo Mode, clears the path trail, enables the path trail, world frame, tool frame, and grid, moves the camera to a strong presentation angle, starts slow camera choreography, and shows the presentation overlay.
- `Stop Demo Presentation`: exits Presentation Demo Mode, stops camera choreography, and leaves orbit controls usable.
- `Connect` / `Disconnect`: controls the live telemetry WebSocket connection.
- `Reload Model`: attempts to load the segmented GLB robot again and rebuilds the active visual model.
- `World frame`: toggles the base/world coordinate frame helper.
- `Tool frame`: toggles the coordinate frame attached to the end-effector tool group.
- `Grid`: toggles the engineering floor grid.
- `Path trail`: toggles rendering of the recent TCP/tool path.
- `Clear Path`: clears the accumulated path buffer.
- `Reset Camera`: restores the strong presentation camera angle.

Keyboard shortcuts:

- `D`: toggles Presentation Demo Mode.
- `C`: clears the path trail.
- `R`: resets the camera.

## Visualization V5: Presentation Demo Mode

V5 adds a distinct Presentation Demo Mode for colleague demos. It is separate from Manual Mode, Local Demo Mode, and Live Telemetry Mode in the UI, but it does not replace the underlying motion source.

When `Start Jaw-Drop Demo` is selected, the viewer:

- Clears the existing TCP/tool path trail.
- Enables the path trail, world frame, tool frame, and grid.
- Moves the camera to a polished three-quarter presentation angle.
- Starts a slow orbit around the robot while looking at the robot center/tool area.
- Shows a presentation overlay titled `OPC UA Robotics Reference Implementation`.
- Shows model status, motion source, program state, current step, and heartbeat/freshness.
- Visually de-emphasizes dense debug controls in the side panel.

If live telemetry is connected, Presentation Demo Mode keeps using live telemetry for robot motion. The browser remains renderer-only and does not start local browser-side motion.

If live telemetry is not connected, Presentation Demo Mode starts or continues Local Demo motion and clearly reports `Local Demo` as the motion source. It does not claim that the pose is live server telemetry. If live telemetry disconnects during a presentation, the viewer falls back to Local Demo motion and the overlay heartbeat changes away from `Live`.

The `Stop Demo Presentation` button stops camera choreography and hides the presentation overlay. If Presentation Demo Mode started Local Demo motion itself, stopping the presentation also stops that local motion and returns to Manual Mode. If Local Demo was already running before presentation mode started, it remains the motion source after presentation mode stops.

The side panel includes a compact `Standards Story` disclosure for presentations:

- Official OPC UA Robotics instance model.
- Simulation-bound live values.
- WebSocket visualization bridge.
- Browser renders only, server remains source of truth.

Suggested colleague demo flow:

1. Start the reference server and visualization.
2. Connect the WebSocket when live server-backed motion should be shown.
3. Select `Start Jaw-Drop Demo`.
4. Let the slow camera orbit run while discussing the Standards Story.
5. Use `C` to clear the path trail before a fresh motion segment.
6. Select `Stop Demo Presentation` or press `D` to return to the normal controls.

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

The primitive fallback uses the same browser-side mapping, including `B` around local Z. If a future GLB uses a different authoring convention, adjust the GLB node pivots and local axes so the named nodes match this contract.

## Visualization V4B: Premium Procedural Fallback Robot

V4B upgrades only the primitive fallback robot visuals. The fallback is still procedural Three.js geometry, but it now presents as a demo-worthy stylized industrial six-axis robot when `public/assets/robots/six-axis-reference.glb` is not available.

The fallback includes:

- A solid pedestal and rotating shoulder column.
- Cylindrical shoulder, elbow, wrist roll, wrist bend, and tool housings.
- Capped upper-arm and forearm covers with dark service-spine accents.
- Subtle circular joint rings and canvas-sprite axis labels for `S`, `L`, `U`, `R`, `B`, and `T`.
- Professional industrial materials with light body panels, dark joint shells, metallic caps, small accent rings, and subtle emissive status/tool details.
- A tool-center-point group at the procedural end-effector tip so the path trail and tool frame follow the visible TCP.

The important contract is unchanged: visual mesh complexity is subordinate to the pivot hierarchy. The fallback continues to use the same `S`, `L`, `U`, `R`, `B`, and `T` joint groups and the same rotation mapping as V4A. Manual sliders, Reset Home, Local Demo, Live Telemetry Mode, model reload, grid/frame toggles, and the path trail continue to work through the shared robot model abstraction.

The segmented GLB remains the future high-fidelity path. V4C can replace the procedural meshes with a properly authored GLB as long as the named nodes, pivots, and local axes keep matching the V4A contract.

## Path Trail and Frames

V4B computes the current tool world position from the active model's tool object after joint updates and during animation frames. For the GLB model this is the `Tool` node. For the primitive fallback this is the procedural tool-center-point group at the visible end-effector tip. Recent positions are stored in a bounded buffer and rendered as a Three.js line, giving a live TCP/tool path without adding browser-side physics.

The world frame marks the base scene reference. The tool frame is attached to the active end-effector object, so it follows either the GLB `Tool` node or the primitive tool-center-point group.

## Current Scope

- Vite, TypeScript, and Three.js.
- Perspective camera with orbit controls.
- Dark engineering scene, grid floor, world coordinate axes, shadows, and polished studio-style lighting.
- Segmented GLB robot loading with primitive fallback.
- Demo-worthy six-axis procedural fallback robot built from Three.js primitives.
- Kinematic hierarchy for S, L, U, R, B, T, and tool groups.
- Manual joint sliders with degree values.
- Local Demo motion retained from V1.
- Live telemetry WebSocket client for `ws://localhost:48080/telemetry`.
- Scene overlay and side-panel status for mode, connection state, heartbeat, telemetry age, joint position, joint velocity, program state, current step, moving state, and timestamp.
- Presentation Demo Mode with one-click path/frame/grid setup, slow camera choreography, motion-source-aware overlay, and Standards Story panel.
- Live TCP/tool path trail with clear and visibility controls.
- World/base and tool coordinate frame helpers.
- Side-panel model status and GLB reload control.
- Keyboard shortcuts for presentation mode, path clearing, and camera reset.

## V5 Limitations

- The WebSocket bridge streams JSON snapshots only; command/control remains through existing server mechanisms.
- The GLB asset is optional and may not be present in the repository yet.
- GLB validation checks required node names, but it cannot prove pivots and local axes were authored correctly.
- Broadcasts repeat the latest server simulation snapshot and do not increase simulation fidelity.
- Missing or invalid axis fields are ignored by the browser and leave the last valid pose value in place.
- No target frame, ghost pose, or program timeline overlay visualization yet.
- No physics simulation in the browser.

## Visualization Plan

After V5, target frames, ghost pose comparison, richer program overlays, and the final segmented GLB robot can be added for robot program review and debugging.
