# C18C.2b generic visual topology

The Workbench visual topology is an immutable implementation model for rendering a tree-shaped direct-transform robot. It is not an OPC UA Robotics information model and must not be presented as standardized equipment, ownership, kinematic, geometry, or safety semantics.

## Boundary and flow

```text
OPC UA motion inventory
  → profile resolver
  → resolved visual joints
  → topology validator/compiler
  → topology renderer
  → Three.js scene
```

The motion inventory retains source identity, ownership, values, units, timestamps, and exact StatusCodes. The reference profile selects compatible source Axes and produces the Workbench identifiers `base`, `shoulder`, `elbow`, `wrist1`, `wrist2`, and `wrist3`. The topology only references those resolved visual-joint IDs. It contains no OPC UA NodeIds, namespace indexes, BrowseNames, namespace URIs, MotionDevice keys, or ownership.

## Model and conventions

`RobotVisualTopology` contains an ID, version, display name, declared root nodes, nodes, joints, geometry definitions, and optional metadata. Nodes hold local transforms and narrow primitive geometry. Joints connect a parent node to a child node and are `fixed`, `revolute`, or `prismatic` only in C18C.2b.

The scene uses Three.js's right-handed coordinate system. Local translations use scene units and visual rotations use radians. A node's local transform is applied to its node group. A joint's local translation and rotation establish its pivot frame; the dynamic transform is then applied to the child subtree. Revolute axes are normalized and expressed in the joint's local frame. Prismatic axes are normalized and translate along that local frame.

For a dynamic source value `q`, the Workbench applies limits first as a visual safety clamp, then computes `zeroOffset + direction × scale × q`. Revolute joints use that result as an axis-angle rotation in radians. Prismatic joints use it as a distance in scene units. Raw converted source values remain available separately from presentation state. A clamp is a Workbench diagnostic and is never a machine command or safety limit.

## Validation and traversal

Validation is pure and returns structured information, warning, and error diagnostics. It detects missing and duplicate IDs, invalid roots, unknown references, multiple parents, identical parent/child nodes, cycles, disconnected nodes, orphan joints, duplicate bindings, invalid fixed/dynamic binding combinations, missing required bindings, invalid axes, non-finite values, zero scales, invalid limits, unsupported geometry, duplicate geometry IDs, and missing geometry definitions. Any error prevents dynamic rendering; no topology is repaired or guessed.

Compilation creates sorted node and joint collections plus root, child-joint, joint, and visual-binding lookups. Roots and child joints are explicitly sorted, so traversal does not depend on input arrays, object property order, OPC UA browse order, or update order. C18C.2b is a single-root tree implementation; parallel, mobile, multi-chain, and multi-root composition are excluded.

## Reference topology and fallback

`reference-server-six-axis-v1` now owns only source compatibility. The six-axis topology owns the calibrated root yaw, link dimensions, pivots, axes, zero pose, offsets, direction, scale, and primitive materials. The generic renderer constructs the scene from topology nodes and joints and applies values by visual-joint ID. It contains no source Axis or OPC UA naming logic.

If profile resolution fails, or topology validation/binding fails, the Workbench retains connection health, identity, operational state, and generic inventory while showing the existing static/neutral fallback with a concise diagnostic. Non-Good StatusCodes remain exact; last-known Good presentation may be held under the existing policy, but no new value is fabricated. WebGL initialization failure continues to use the static image fallback.

Visual limits are not source physical ranges, profile compatibility expectations, or machine safety limits. They never authorize or constrain commands. The current reference topology has no visual limits because the previous renderer had none.

The next milestone, C18C.2c, can define additional explicit topology/profile composition boundaries. It must not infer topology from OPC UA ownership or BrowseName/browse order and should address broader robot forms only with explicit calibrated models.
