# C18C.1 live 3D robot proof of concept

## Purpose and limits

The Workbench can show a neutral procedural six-axis articulated model in the existing robot image area. It is an operational visualization only: it is not safety-rated, a conformance tool, an exact vendor robot, a CAD digital twin, or a control surface. It never invokes robot methods and never invents pose data.

## Discovery and data flow

The browser reuses the existing robot-scoped `selection=all` WebSocket and the existing REST discovery/snapshot calls:

`vendor OPC UA server → Robotics.Client.Core discovery/subscription → ClientGateway robot-scoped WebSocket → Workbench per-robot snapshot merge → mapping → Three.js viewport`.

The existing typed hierarchy proves `MotionDeviceSystem → MotionDevice → Axes → AxisType`. `SnapshotDiscoveryService` now follows each discovered AxisType's live `ParameterSet` component and then its `ActualPosition` variable, retaining the exact NodeId returned by each browse response. It reads EngineeringUnits and EURange by browsing the discovered AnalogUnitType variable's properties; it does not append `/ActualPosition` to a NodeId. The snapshot includes timestamps, raw value text, discovery evidence, and exact StatusCode. WebSocket `dataChange` messages update the same robot-scoped snapshot used by the existing axis widgets. Unit metadata is included in both REST snapshots and initial live-stream snapshots. No new OPC UA session, subscription, endpoint, registry, or certificate behavior is introduced.

## Workbench hosting and local development

The gateway-hosted Workbench at port 5080 uses same-origin relative `/api/...` and `/ws/...` requests. Vite development uses the same relative browser URLs and proxies `/api` and `/ws` to `http://localhost:5080`, so the browser does not make a cross-origin request and no additional gateway CORS ports are required. This remains valid when Vite selects a fallback port.

An explicit remote gateway can be selected with `VITE_GATEWAY_BASE_URL`; the value is optional, trailing slashes are normalized, and HTTP/HTTPS bases select the corresponding `ws`/`wss` WebSocket scheme. The older `VITE_GATEWAY_URL` name remains accepted as a compatibility fallback.

## Standards truth, local evidence, and Workbench decisions

* Official specification truth: Robotics `AxisType.ParameterSet.ActualPosition` is a mandatory `Double` `AnalogUnitType`; `AnalogUnitType` requires the `EngineeringUnits` property of type `EUInformation`. If a physical position limit exists, Robotics requires `EURange` for `ActualPosition`. The OPC UA Core UN/CEFACT representation uses namespace URI `http://www.opcfoundation.org/UA/units/un/cefact`; degree is code `DD`, unit ID `17476`, display name `°`, and description `degree [unit of angle]`.
* Local NodeSet/generated-code truth: the reference instance has six typed axes named `SAxis`, `LAxis`, `UAxis`, `RAxis`, `BAxis`, and `TAxis`. Each `ActualPosition` is a `Double` `AnalogUnitType` with an existing `EngineeringUnits` child property. The generated SDK model represents that imported child as a `BaseVariableState`; it was previously present but uninitialized. The instance NodeSet does not provide `EURange` children, so this change leaves EURange unchanged rather than inventing limits.
* Reference-server implementation decision: the simulator stores and updates position, speed, and acceleration in degrees, with authoritative per-axis degree limits. A shared initialization path now assigns the verified degree `EUInformation` to each existing `ActualPosition/EngineeringUnits` child. Numeric values, Good status, source timestamps, and simulation updates remain in the existing binding path. `ActualSpeed` and `ActualAcceleration` remain unchanged and are a follow-up because this change is scoped to the confirmed Workbench blocker.
* Workbench visualization decision: the Workbench uses `ActualPosition` only when its server-provided `EngineeringUnits` identifies degrees or radians; it does not assume degrees. The 3D renderer uses the discovered value plus unit metadata, while visual joint frame, pivot, direction, and zero offset remain Workbench model decisions because the Robotics model does not define generic geometry or kinematics.
* Workbench decision: runtime BrowseNames may be rendered as QualifiedNames with namespace-index prefixes such as `5:SAxis`. Namespace indexes are not stable identities, so this proof of concept matches only the local-name portion while retaining the original qualified name in axis diagnostics and evidence. The isolated reference mapping recognizes `SAxis`, `LAxis`, `UAxis`, `RAxis`, `BAxis`, and `TAxis`; this does not make that convention vendor-generic. Unknown, duplicate, fewer-than-six, or more-than-six axis sets are not assigned by browse order. A future model manifest can replace this mapping with model node, axis identity, joint type, transform axis, direction, zero offset, and scale.

## EngineeringUnits decoding contract

These four statements are intentionally separate:

1. **Official OPC UA truth.** [`EUInformation` in OPC UA Part 8](https://reference.opcfoundation.org/specs/OPC-10000-8/5.6.4.3) is a structure. Its `NamespaceUri` identifies the organization defining `UnitId`; `UnitId` is the programmatic identifier; `DisplayName` is normally the symbol; and `Description` is the full unit name. [`i=887`](https://reference.opcfoundation.org/nodesets/192/110095) identifies the `EUInformation` data type in the OPC UA namespace. It does not identify degrees. The standard's [UN/CEFACT mapping](https://reference.opcfoundation.org/Core/Part8/v105/docs/5.6.4.4) documents the code-to-UnitId conversion and the radian code `C81`.
2. **Local SDK/runtime truth.** With UA-.NETStandard 1.5.378.145, the gateway can receive `EUInformation` directly, or an `ExtensionObject` whose `Body` is an `EUInformation`; encoded ExtensionObjects are resolved through the SDK's `ExtensionObject.ToEncodeable` helper. The confirmed runtime currently arrives as an ExtensionObject with a decoded `EUInformation` body, which the previous renderer reduced to `ExtensionObject(TypeId=i=887, Body=EUInformation)`.
3. **Gateway serialization decision.** The gateway decodes the fields into an additive, SDK-independent contract. `engineeringUnits` is the first available non-empty `DisplayName`, then `Description`; `engineeringUnit` carries `namespaceUri`, `unitId`, `displayName`, and `description`. `engineeringUnitsRaw` is populated only when decoding fails. `EURange` retains its old text and adds `euRangeMetadata: { low, high }` when it can be decoded.
4. **Workbench rendering decision.** The Workbench checks structured display/description text together with the compatibility string. Degree symbols, degree/deg text, and rad/radian text are accepted. Unknown, missing, or unsupported units stay non-animating. The Workbench never interprets `i=887` or a generic ExtensionObject diagnostic as degrees.

For a verified degree value, the additive JSON is:

```json
{
  "engineeringUnits": "°",
  "engineeringUnit": {
    "namespaceUri": "http://www.opcfoundation.org/UA/units/un/cefact",
    "unitId": 17476,
    "displayName": "°",
    "description": "degree [unit of angle]",
    "rawDiagnostic": null
  },
  "engineeringUnitsRaw": null
}
```

For a radian value, the same contract is:

```json
{
  "engineeringUnits": "rad",
  "engineeringUnit": {
    "namespaceUri": "http://www.opcfoundation.org/UA/units/un/cefact",
    "unitId": 4405297,
    "displayName": "rad",
    "description": "radian",
    "rawDiagnostic": null
  },
  "engineeringUnitsRaw": null
}
```

The exact `UnitId` and namespace are carried from the server; the gateway does not guess them or hardcode a namespace index. A valid but unsupported unit remains fully visible in `engineeringUnit` and is reported by the Workbench as unsupported. An undecodable ExtensionObject has null primary unit fields and retains the SDK-independent raw diagnostic in `engineeringUnitsRaw`; snapshot generation continues.

## Units, quality, freshness, and interpolation

Degrees and radians are converted to renderer radians only when EngineeringUnits identifies them. Linear, missing, or otherwise unsupported units are marked unsupported and do not animate. Non-Good samples never move a joint; the last known Good pose may remain visible while its exact StatusCode is shown in joint details. For example, `2150760448` is `0x80320000`, the SDK's `BadWaitingForInitialData`; it is not Good and must not be rendered as a new pose. Values are never replaced by zero.

The visual renderer interpolates presentation transforms between genuine Good samples over a bounded 240 ms window. It does not extrapolate and does not apply speculative shortest-path wrap handling. A connected stream becomes stale after 3 seconds without a Good sample; WebSocket disconnection is shown as Disconnected and the view does not claim to be live.

## Fallback and performance

The static vendor image/placeholder remains available when WebGL fails, mapping is ambiguous, data is unavailable, or the robot has a non-six-axis layout. The viewport has pointer orbit, bounded zoom, reset camera, a compact status indicator, and a joint diagnostics disclosure with axis identity, raw/status metadata, units, and evidence. Each card owns its scene, render loop, resize observer, controls, geometries, and materials. The loop pauses scene rendering when the document is hidden, and no global loop is created.

The procedural model is deliberately low-complexity and unbranded. Future GLB support can preserve the same six joint groups while replacing only geometry and using a mapping manifest; this milestone does not claim that a GLB or exact kinematic model can be derived from OPC UA Robotics data.
