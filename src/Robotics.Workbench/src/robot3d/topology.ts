import * as THREE from 'three';

export type VisualJointType = 'fixed' | 'revolute' | 'prismatic';
export type GeometryKind = 'box' | 'cylinder' | 'sphere' | 'group';
export type Vector3Definition = Readonly<{ x: number; y: number; z: number }>;
export type RotationDefinition = Readonly<{ x: number; y: number; z: number; order?: THREE.EulerOrder }>;
export type LocalTransform = Readonly<{ translation: Vector3Definition; rotation: RotationDefinition }>;
export type JointLimitDefinition = Readonly<{ lower: number; upper: number }>;

export type VisualGeometryDefinition = Readonly<{
  geometryId: string;
  kind: GeometryKind;
  dimensions?: Vector3Definition;
  radius?: number;
  topRadius?: number;
  bottomRadius?: number;
  height?: number;
  segments?: number;
  materialRole?: string;
  label?: string;
}>;

export type VisualNodeDefinition = Readonly<{
  nodeId: string;
  parentJointId?: string;
  localTransform: LocalTransform;
  geometryIds?: readonly string[];
  presentation?: Readonly<Record<string, string>>;
}>;

export type VisualJointTopologyDefinition = Readonly<{
  jointId: string;
  jointType: VisualJointType;
  parentNodeId: string;
  childNodeId: string;
  localTransform: LocalTransform;
  axis?: Vector3Definition;
  resolvedVisualJointId?: string;
  bindingRequired?: boolean;
  direction?: number;
  scale?: number;
  zeroOffset?: number;
  limit?: JointLimitDefinition;
}>;

export type RobotVisualTopology = Readonly<{
  topologyId: string;
  version: string;
  displayName: string;
  rootNodeIds: readonly string[];
  singleRoot?: boolean;
  nodes: readonly VisualNodeDefinition[];
  joints: readonly VisualJointTopologyDefinition[];
  geometries?: readonly VisualGeometryDefinition[];
  metadata?: Readonly<Record<string, string>>;
}>;

export type TopologyDiagnosticSeverity = 'information' | 'warning' | 'error';
export type TopologyValidationDiagnostic = Readonly<{
  code: string;
  severity: TopologyDiagnosticSeverity;
  topologyId: string;
  message: string;
  nodeId?: string;
  jointId?: string;
}>;
export type TopologyValidationResult = Readonly<{ valid: boolean; diagnostics: readonly TopologyValidationDiagnostic[] }>;

export type ValidatedVisualTopology = Readonly<RobotVisualTopology & {
  traversal: Readonly<{
    rootNodeIds: readonly string[];
    nodeById: ReadonlyMap<string, VisualNodeDefinition>;
    jointById: ReadonlyMap<string, VisualJointTopologyDefinition>;
    childJointIdsByNodeId: ReadonlyMap<string, readonly string[]>;
    bindingByVisualJointId: ReadonlyMap<string, VisualJointTopologyDefinition>;
  }>;
}>;

export const identityTransform = (): LocalTransform => ({ translation: { x: 0, y: 0, z: 0 }, rotation: { x: 0, y: 0, z: 0, order: 'XYZ' } });
export const vectorLength = (value: Vector3Definition) => Math.hypot(value.x, value.y, value.z);
export function deepFreeze<T>(value: T): T { if (value && typeof value === 'object' && !Object.isFrozen(value)) { Object.freeze(value); Object.values(value as Record<string, unknown>).forEach(item => deepFreeze(item)); } return value; }
const normalized = (value: Vector3Definition) => { const length = vectorLength(value); return { x: value.x / length, y: value.y / length, z: value.z / length }; };

export function resolveJointMotion(joint: VisualJointTopologyDefinition, sourceValue?: number) {
  const value = sourceValue === undefined ? 0 : sourceValue;
  const direction = joint.direction ?? 1;
  const scale = joint.scale ?? 1;
  const zeroOffset = joint.zeroOffset ?? 0;
  const limited = joint.limit ? Math.max(joint.limit.lower, Math.min(joint.limit.upper, value)) : value;
  return {
    sourceValue,
    value: limited,
    clamped: limited !== value,
    rotationRadians: joint.jointType === 'revolute' ? zeroOffset + direction * scale * limited : 0,
    translation: joint.jointType === 'prismatic' ? (() => { const axis = normalized(joint.axis!); const distance = zeroOffset + direction * scale * limited; return { x: axis.x * distance, y: axis.y * distance, z: axis.z * distance }; })() : { x: 0, y: 0, z: 0 }
  };
}
