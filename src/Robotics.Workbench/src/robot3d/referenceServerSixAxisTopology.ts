import { deepFreeze, identityTransform, RobotVisualTopology } from './topology';

const v = (x: number, y: number, z: number) => ({ x, y, z });
const transform = (y = 0, rotation = v(0, 0, 0)) => ({ translation: v(0, y, 0), rotation: { ...rotation, order: 'XYZ' as const } });
const box = (geometryId: string, height: number, materialRole: string) => ({ geometryId, kind: 'box' as const, dimensions: v(0.22, height, 0.22), materialRole });
const jointGeometry = (geometryId: string) => ({ geometryId, kind: 'cylinder' as const, radius: 0.17, height: 0.18, segments: 24, materialRole: 'joint' });

const lengths = [0.58, 0.85, 0.72, 0.58, 0.42, 0.28] as const;
const nodes: RobotVisualTopology['nodes'] = [
  { nodeId: 'base', localTransform: { ...identityTransform(), rotation: { x: 0, y: -0.35, z: 0, order: 'XYZ' as const } }, geometryIds: ['base-geometry'] },
  ...lengths.map((_, index) => ({ nodeId: `link${index + 1}`, parentJointId: ['joint-base', 'joint-shoulder', 'joint-elbow', 'joint-wrist1', 'joint-wrist2', 'joint-wrist3'][index], localTransform: identityTransform(), geometryIds: [`joint-${index + 1}`, `link-${index + 1}`] })),
  { nodeId: 'flange', parentJointId: 'flange-fixed', localTransform: identityTransform(), geometryIds: ['flange-geometry'] }
] as const;

export const referenceServerSixAxisTopology: RobotVisualTopology = deepFreeze({
  topologyId: 'reference-server-six-axis-v1-visual-topology', version: 'C18C.2b', displayName: 'Reference server six-axis visual topology', rootNodeIds: ['base'], singleRoot: true,
  nodes,
  joints: [
    { jointId: 'joint-base', jointType: 'revolute', parentNodeId: 'base', childNodeId: 'link1', localTransform: transform(0.24), axis: v(0, 1, 0), resolvedVisualJointId: 'base', direction: 1, scale: 1, zeroOffset: 0 },
    { jointId: 'joint-shoulder', jointType: 'revolute', parentNodeId: 'link1', childNodeId: 'link2', localTransform: transform(lengths[0]), axis: v(0, 0, 1), resolvedVisualJointId: 'shoulder', direction: 1, scale: 1, zeroOffset: -65 * Math.PI / 180 },
    { jointId: 'joint-elbow', jointType: 'revolute', parentNodeId: 'link2', childNodeId: 'link3', localTransform: transform(lengths[1]), axis: v(0, 0, 1), resolvedVisualJointId: 'elbow', direction: 1, scale: 1, zeroOffset: 75 * Math.PI / 180 },
    { jointId: 'joint-wrist1', jointType: 'revolute', parentNodeId: 'link3', childNodeId: 'link4', localTransform: transform(lengths[2]), axis: v(1, 0, 0), resolvedVisualJointId: 'wrist1', direction: 1, scale: 1, zeroOffset: 0 },
    { jointId: 'joint-wrist2', jointType: 'revolute', parentNodeId: 'link4', childNodeId: 'link5', localTransform: transform(lengths[3]), axis: v(0, 0, 1), resolvedVisualJointId: 'wrist2', direction: 1, scale: 1, zeroOffset: 0 },
    { jointId: 'joint-wrist3', jointType: 'revolute', parentNodeId: 'link5', childNodeId: 'link6', localTransform: transform(lengths[4]), axis: v(1, 0, 0), resolvedVisualJointId: 'wrist3', direction: 1, scale: 1, zeroOffset: 0 },
    { jointId: 'flange-fixed', jointType: 'fixed', parentNodeId: 'link6', childNodeId: 'flange', localTransform: transform(lengths[5]) }
  ] as RobotVisualTopology['joints'],
  geometries: [
    { geometryId: 'base-geometry', kind: 'cylinder', topRadius: 0.55, bottomRadius: 0.65, height: 0.18, segments: 32, materialRole: 'base' },
    ...lengths.flatMap((length, index) => [jointGeometry(`joint-${index + 1}`), box(`link-${index + 1}`, length, index % 2 ? 'link-dark' : 'link-light')]),
    { geometryId: 'flange-geometry', kind: 'cylinder', radius: 0.11, height: 0.12, segments: 20, materialRole: 'flange' }
  ] as RobotVisualTopology['geometries'],
  metadata: { coordinateSystem: 'right-handed Three.js scene; topology translations use scene units; rotations use radians' }
});
