import { DiscoveryNode, Snapshot, SnapshotValue } from '../api/types';

export type VisualJoint = 'S' | 'L' | 'U' | 'R' | 'B' | 'T';
export const visualJoints: VisualJoint[] = ['S', 'L', 'U', 'R', 'B', 'T'];
export type MappingStatus = 'mapped' | 'ambiguous' | 'unsupported' | 'unavailable';
export type Freshness = 'live' | 'stale' | 'disconnected' | 'unavailable';
export type UnitKind = 'degrees' | 'radians' | 'unsupported' | 'missing';

export type JointMapping = {
  joint: VisualJoint;
  axis?: DiscoveryNode;
  position?: SnapshotValue;
  status: MappingStatus;
  evidence: string;
  unit: UnitKind;
  rawValue?: number;
  renderRadians?: number;
  lastGoodAt?: number;
  freshness: Freshness;
  reason?: string;
};

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
  live: string;
  /** Optional authoritative health signal; absent means a connected socket is healthy. */
  streamHealth?: 'healthy' | 'stale';
  now?: number;
};
