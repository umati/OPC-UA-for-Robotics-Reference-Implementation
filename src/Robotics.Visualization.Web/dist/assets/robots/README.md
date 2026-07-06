# Robot GLB Assets

`six-axis-reference.glb` is the expected Visualization V4A robot asset. It is not committed yet unless a segmented robot model is available.

Place the file at:

```text
public/assets/robots/six-axis-reference.glb
```

The browser serves that file as:

```text
/assets/robots/six-axis-reference.glb
```

## Required Node Names

The GLB must contain these named nodes:

- `RobotRoot`
- `AxisS`
- `AxisL`
- `AxisU`
- `AxisR`
- `AxisB`
- `AxisT`
- `Tool`

If any node is missing, the visualization logs a warning, shows a model warning in the side panel, and uses the primitive fallback robot.

## Expected Hierarchy

Each moving axis must be a separate node or group so the browser can rotate that segment independently:

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

Meshes may be children of these axis nodes. A single combined mesh is not enough because the browser cannot rotate individual joints without separate transform nodes.

## Pivot and Origin Rule

Each axis node origin must be placed at the physical joint pivot for that axis. Child meshes should be positioned relative to the pivot node, not centered around the exported mesh bounds. The visualization applies rotations directly to the named axis nodes.

## Local Rotation Axes

Visualization V4A expects these local axes:

- `AxisS`: local Y
- `AxisL`: local Z
- `AxisU`: local Z
- `AxisR`: local X
- `AxisB`: local Z
- `AxisT`: local X

The primitive fallback uses the same browser-side axis mapping. If a GLB authored in Blender appears to bend in the wrong direction, correct the node local axes or parent transforms in Blender rather than changing telemetry values.

## Blender Export Notes

- Apply object scale before export.
- Use empty objects or named collections converted to transform nodes for each axis.
- Put each pivot origin at the joint center.
- Parent moving geometry under the axis node that owns it.
- Keep the required node names exact and case-sensitive.
- Export as GLB with transforms preserved.
- Avoid baking the whole robot into one mesh.
