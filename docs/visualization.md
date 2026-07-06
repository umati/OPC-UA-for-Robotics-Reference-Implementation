# Visualization

Visualization V1 is a browser-based, offline 3D robot viewer for checking the robot hierarchy, pivots, joint axes, camera, lighting, and controls before live telemetry is added.

The V1 browser app is manual-slider only. It does not connect to OPC UA, does not use WebSocket telemetry, and does not define robot physics for the final architecture.

## Run V1

From the visualization project folder:

```powershell
cd src/Robotics.Visualization.Web
npm install
npm run dev
```

Then open the local URL printed by Vite.

## Current Scope

- Vite, TypeScript, and Three.js.
- Perspective camera with orbit controls.
- Dark scene, grid floor, world coordinate axes, and basic lighting.
- Six-axis placeholder robot built from simple Three.js primitives.
- Kinematic hierarchy for S, L, U, R, B, T, and tool groups.
- Manual joint sliders with degree values.
- Current joint value panel.
- Reset Home, Demo Motion, and Stop Demo controls.

## Current Limitations

- No OPC UA connection.
- No WebSocket telemetry.
- No server-side data binding.
- No external GLB robot model.
- No path, target frame, or program visualization.
- No physics simulation in the browser.

## V2 Plan

V2 should add a WebSocket telemetry bridge that streams joint values from the reference server or a server-side adapter into the browser. The browser should remain a visualization client and should apply telemetry to the kinematic hierarchy without redefining motion validation or simulation behavior.

## V3 Plan

V3 should add path curves, target frames, ghost pose comparison, and program overlay features for robot program review and debugging.
