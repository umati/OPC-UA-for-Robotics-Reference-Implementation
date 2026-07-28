# C18C.2c multi-chain visual assemblies

This document describes a Workbench implementation boundary. Visual assemblies and chains are local rendering concepts; they are not OPC UA Robotics information-model concepts and do not assert kinematic, equipment, safety, certification, or conformance semantics.

## Boundary and flow

```text
OPC UA motion inventory
→ profile resolution with MotionDevice-scoped bindings
→ resolved visual joints
→ validated visual assembly
→ chain-quality aggregation
→ multi-root topology renderer
→ Three.js scene
```

The OPC UA equipment graph remains authoritative for discovered MotionDevice ownership, Axes, PowerTrains, controllers, stable runtime identities, engineering units, timestamps, and exact StatusCodes. The Workbench visual assembly graph is separate:

- A `VisualChainDefinition` names a rendering subtree and explicitly declares required or optional MotionDevice-scoped bindings.
- A `VisualAssemblyRoot` declares a scene root and its transform. Independent roots have no visual parent relationship.
- Shared fixed geometry is represented by topology nodes or shared-root metadata and does not require dynamic Axis bindings.
- A `VisualParentAttachment` explicitly mounts one chain below a node in another chain. Ownership, controller sharing, browse order, and BrowseNames never create an attachment.

One topology can therefore contain independent roots, a shared fixed base, or a mounted child root. A dynamic visual joint has one chain owner in this milestone. A chain may bind Axes from one MotionDevice or several MotionDevices, but every binding retains its expected MotionDevice key and is rejected when resolution supplies another owner.

## Quality and fallback

Chain quality is derived presentation state, not a replacement for per-Axis source quality. Aggregation is deterministic:

1. missing or wrong-owner required bindings produce `invalidBinding`;
2. an explicitly degraded parent placement produces `degraded` while child source quality remains separate;
3. non-Good source values produce `degraded` and preserve their exact StatusCodes;
4. unavailable source values produce `unavailable`;
5. stale-but-valid values produce `resolvedWithWarnings`;
6. otherwise the chain is `resolvedHealthy`.

An optional independent chain may use `static`, `neutral`, or `omit` fallback without disabling healthy chains. A mounted child is not placed using fabricated parent motion. If a required parent is unavailable, the child reports `parentPlacementDegraded`; its own Good Axis telemetry is not relabeled as bad. Invalid assemblies never receive guessed dynamic animation.

Compilation sorts roots, chains, nodes, joints, and lookup values. It produces node-to-chain, joint-to-chain, visual-joint-to-chain, MotionDevice-to-chain, attachment, shared-node, root-order, and diagnostics lookups independent of array order, browse order, WebSocket order, or object insertion order.

## Synthetic architecture fixtures

The internal fixtures prove three forms without exposing synthetic robots as production profiles:

- `railMountedSixAxisArmFixture`: a prismatic rail from `rail-device` explicitly mounts an articulated chain from `arm-device` below the carriage. Rail degradation affects placement, not the arm’s source quality.
- `robotPlusIndependentPositionerFixture`: an arm and a positioner are independent roots; positioner failure does not disable arm rendering.
- `dualArmSharedBaseFixture`: left and right chains use distinct MotionDevice keys below one fixed shared base. Duplicate source names remain safe because binding uses stable ownership, not names alone.

These fixtures do not claim that OPC UA Robotics defines rail, dual-arm, positioner, or humanoid visual kinematics. They are narrow Workbench tests of explicit composition.

## Compatibility and exclusions

The Gateway, snapshot, WebSocket, motionInventory, command, operational-state, same-origin, certificate, and stable-key contracts remain unchanged. `VerifyRobotCommands.ps1` remains unchanged because its existing ownership and stable-key checks cover the public data needed by this milestone.

The reference-server-six-axis-v1 remains a one-root, one-chain assembly using the existing calibrated topology, zero pose, geometry, pivots, directions, interpolation, snapshot seeding, live updates, stationary Live semantics, and static/WebGL fallbacks. The renderer is no longer structurally special-cased for one chain.

This milestone excludes solver kinematics, mobile localization, floating bases, humanoid/hands semantics, external profiles, profile editors/downloads, mesh import, production robot-family profiles, pose-driven rendering, automatic attachment inference, release packaging, installers, and unrelated UI redesign.

The intended next milestone is **C18C.2d**, which may address the next explicitly scoped Workbench composition or presentation requirement after standards and model-boundary review.
