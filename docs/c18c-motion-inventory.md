# C18C.2a.1 generic motion inventory

This milestone establishes an equipment inventory boundary. `Robotics.Client.Core` discovers the typed hierarchy `MotionDeviceSystem → MotionDevice → Axis` and retains that ownership in the normalized inventory. The Gateway exposes the inventory additively on robot-scoped snapshots and on the initial live WebSocket snapshot. Axis values and later DataChange messages carry the same owner-aware identity fields.

The inventory is deliberately not a visual topology. OPC UA ownership does not define a Three.js parent-child chain, joint transform, mesh, visual zero, or rendering order. Axis browse order is therefore never treated as visual order. The names `SAxis`, `LAxis`, `UAxis`, `RAxis`, `BAxis`, and `TAxis` are local reference-server evidence only; they are not used as generic OPC UA Robotics semantics.

Each discovered node retains the original OPC UA NodeId for provenance and diagnostics, while `stableKey` is derived from the namespace URI, NodeId identifier type, and identifier. Namespace indexes, BrowseNames, DisplayNames, browse order, array position, and role names are not the sole identity. The Gateway does not place these raw identifiers in URL paths.

The normalized inventory retains every discovered Axis, including seventh and external axes, duplicate BrowseNames under different MotionDevices, unfamiliar names, and axes without a current visual binding. Missing or unsupported ActualPosition/unit metadata and non-Good StatusCodes remain visible as diagnostics; a stationary Good axis is not considered unhealthy.

The existing Workbench renderer remains behind an explicit reference-server compatibility projection. It recognizes the six local names only when the reference set is uniquely proven, and it retains the calibrated visual zero, direction, unit conversion, interpolation, snapshot seeding, live updates, and fallback behavior. Extra or unfamiliar inventory axes remain non-visual and observable. This adapter must not be mistaken for generic kinematic reconstruction.

The next milestone, C18C.2a.2, should isolate reference profiles from the generic inventory. It may define an explicit visual manifest, but it must not infer a complete kinematic skeleton from OPC UA ownership or browse order.
