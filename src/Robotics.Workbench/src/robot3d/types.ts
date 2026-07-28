import { DiscoveryNode, MotionInventory, Snapshot, SnapshotValue } from '../api/types';

export type VisualJoint = 'base' | 'shoulder' | 'elbow' | 'wrist1' | 'wrist2' | 'wrist3' | (string & {});
export const visualJoints: VisualJoint[] = ['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3'];
export type MappingStatus = 'matched' | 'notMatched' | 'ambiguous' | 'invalid';
export type Freshness = 'live' | 'stale' | 'disconnected' | 'unavailable';
export type UnitKind = 'degrees' | 'radians' | 'unsupported' | 'missing';
export type DiagnosticSeverity = 'informational' | 'warning' | 'error';
export type ProfileResolutionDiagnostic = { code: string; severity: DiagnosticSeverity; message: string; visualJointId?: VisualJoint; axisKey?: string };

export type AxisBindingSelector = { browseName: string; expectedMotionDevice: 'single-device-scope'; required: true; unit: 'angular'; motionProfile?: string };
export type VisualJointDefinition = { visualJointId: VisualJoint; axis: AxisBindingSelector };
export type RobotVisualProfile = { profileId: 'reference-server-six-axis-v1'; displayName: string; joints: readonly VisualJointDefinition[] };

export type ResolvedVisualJoint = {
  visualJointId: VisualJoint;
  profileId: string;
  sourceMotionDeviceKey: string;
  sourceAxisKey: string;
  sourceAxis: DiscoveryNode;
  requiredUnitCategory: 'angular';
  conversion: { unit: UnitKind; scale: number };
  /** Source conversion only; visual calibration belongs to the topology. */
  direction?: 1 | -1;
  zeroOffsetRadians?: number;
  visualScale?: number;
  rawValue?: number;
  convertedVisualValue?: number;
  lastGoodConvertedValue?: number;
  renderRadians?: number;
  statusCode?: string;
  sourceTimestamp?: string | null;
  serverTimestamp?: string | null;
  freshness: Freshness;
  diagnosticState: 'bound' | 'sourceNonGood' | 'sourceUnavailable' | 'invalid';
  actualPosition?: SnapshotValue;
};

export type ProfileResolutionResult = { status: MappingStatus; profile: RobotVisualProfile; joints: ResolvedVisualJoint[]; diagnostics: ProfileResolutionDiagnostic[]; additionalUnboundAxisKeys: string[] };

export type JointMapping = ResolvedVisualJoint & { joint: VisualJoint; axis?: DiscoveryNode; position?: SnapshotValue; status: MappingStatus; evidence: string; unit: UnitKind; lastGoodAt?: number; reason?: string };

export type Robot3DState = {
  mappingStatus: MappingStatus;
  freshness: Freshness;
  joints: JointMapping[];
  explanation: string;
  source: 'robot-scoped snapshot and WebSocket dataChange stream';
};

export type Robot3DInput = {
  axes: DiscoveryNode[];
  snapshots: (Snapshot | undefined)[];
  motionInventory?: MotionInventory | null;
  /** Internal resolver alias; gateway contracts remain motionInventory. */
  inventory?: MotionInventory | null;
  live: string;
  /** Optional authoritative health signal; absent means a connected socket is healthy. */
  streamHealth?: 'healthy' | 'stale';
  now?: number;
};
