# C18C.2a.1 generic motion inventory

This milestone establishes an equipment inventory boundary. `Robotics.Client.Core` discovers the typed hierarchy `MotionDeviceSystem → MotionDevice → Axis` and retains that ownership in the normalized inventory. The Gateway exposes the inventory additively on robot-scoped snapshots and on the initial live WebSocket snapshot. Axis values and later DataChange messages carry the same owner-aware identity fields.

The inventory is deliberately not a visual topology. OPC UA ownership does not define a Three.js parent-child chain, joint transform, mesh, visual zero, or rendering order. Axis browse order is therefore never treated as visual order. The names `SAxis`, `LAxis`, `UAxis`, `RAxis`, `BAxis`, and `TAxis` are local reference-server evidence only; they are not used as generic OPC UA Robotics semantics.

Each discovered node retains the original OPC UA NodeId for provenance and diagnostics, while `stableKey` is derived from the namespace URI, NodeId identifier type, and identifier. Namespace indexes, BrowseNames, DisplayNames, browse order, array position, and role names are not the sole identity. The Gateway does not place these raw identifiers in URL paths.

The normalized inventory retains every discovered Axis, including seventh and external axes, duplicate BrowseNames under different MotionDevices, unfamiliar names, and axes without a current visual binding. Missing or unsupported ActualPosition/unit metadata and non-Good StatusCodes remain visible as diagnostics; a stationary Good axis is not considered unhealthy.

The existing Workbench renderer remains behind an explicit reference-server compatibility projection. It recognizes the six local names only when the reference set is uniquely proven, and it retains the calibrated visual zero, direction, unit conversion, interpolation, snapshot seeding, live updates, and fallback behavior. Extra or unfamiliar inventory axes remain non-visual and observable. This adapter must not be mistaken for generic kinematic reconstruction.

## C18C.2a.2 visual-profile boundary

The Workbench now resolves the generic motion inventory through the immutable internal `reference-server-six-axis-v1` profile before rendering:

```text
OPC UA motion inventory
  → visual-profile resolution
  → resolved visual joints
  → bounded interpolation
  → Three.js scene
```

The profile is a local Workbench decision. Its `base`, `shoulder`, `elbow`, `wrist1`, `wrist2`, and `wrist3` visual-joint identifiers are distinct from the source Axis identities. The profile selects the local `SAxis`, `LAxis`, `UAxis`, `RAxis`, `BAxis`, and `TAxis` BrowseNames and expected angular units. The C18C.2b topology owns visual direction, calibrated zero offsets, rendering scale, hierarchy, axes, and geometry. Those names and visual decisions are not standardized OPC UA Robotics semantics.

Resolution is scoped to one MotionDevice and uses stable MotionDevice/Axis keys from the normalized inventory. It never uses namespace indexes, browse order, alphabetical order, DisplayName, or MotionDevice ownership as visual parenthood. Missing or duplicate required bindings fail safely; an extra Axis remains in the generic inventory and is reported as informationally unbound. A source non-Good StatusCode remains exact and prevents fabricated animation. Missing or undecodable unit metadata is retained as raw evidence and produces a diagnostic rather than a guessed conversion.

Initial REST snapshots and WebSocket `snapshot`/`dataChange` updates enter the same robot-scoped inventory and resolve to the same stable source Axis keys. If the profile is not applicable, ambiguous, or invalid, the generic inventory, OPC UA connection health, and operational-state presentation remain available while the renderer uses the existing static/neutral fallback. Visual mapping failure is not reported as OPC UA disconnection or as a bad source StatusCode.

This is intentionally not a generic topology schema: it does not author parent-child graphs, joint types, meshes, or arbitrary robot-family renderers. C18C.2b is the boundary for that future topology work.

The C18C.2b visual topology boundary is documented in [`c18c-visual-topology.md`](c18c-visual-topology.md). It makes the Workbench's explicit tree-shaped visual model separate from inventory and profile resolution. The next milestone, C18C.2c, may extend explicit calibrated models but must not infer a complete kinematic skeleton from OPC UA ownership or browse order.
