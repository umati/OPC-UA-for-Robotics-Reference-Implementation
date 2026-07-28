import * as THREE from 'three';
import { ResolvedVisualJoint } from './types';
import { resolveJointMotion, ValidatedVisualTopology, VisualGeometryDefinition, VisualNodeDefinition } from './topology';
import { ResolvedVisualAssembly, composeAttachmentTransform } from './assembly';

export type TopologyRobotVisual = { root: THREE.Group; dynamicJoints: Map<string, THREE.Group>; nodeGroups: Map<string, THREE.Group>; dispose: () => void };
export type TopologyRuntimeDiagnostic = Readonly<{ code: 'visual-limit-clamped'; severity: 'warning'; jointId: string; message: string }>;
const material = (role?: string) => new THREE.MeshStandardMaterial({ color: role === 'base' ? 0x263d46 : role === 'flange' ? 0xd3a35e : role === 'joint' ? 0x4fbbb0 : role === 'link-dark' ? 0x7b9296 : 0x477d80, metalness: role === 'flange' || role === 'joint' ? 0.45 : 0.15, roughness: 0.55 });
const applyTransform = (object: THREE.Object3D, transform: VisualNodeDefinition['localTransform']) => { object.position.set(transform.translation.x, transform.translation.y, transform.translation.z); object.rotation.set(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.order || 'XYZ'); };
const createGeometry = (definition: VisualGeometryDefinition) => { if (definition.kind === 'box') { const d = definition.dimensions!; return new THREE.Mesh(new THREE.BoxGeometry(d.x, d.y, d.z), material(definition.materialRole)); } if (definition.kind === 'cylinder') return new THREE.Mesh(new THREE.CylinderGeometry(definition.topRadius ?? definition.radius!, definition.bottomRadius ?? definition.radius!, definition.height!, definition.segments ?? 24), material(definition.materialRole)); if (definition.kind === 'sphere') return new THREE.Mesh(new THREE.SphereGeometry(definition.radius!, definition.segments ?? 20, 12), material(definition.materialRole)); return new THREE.Group(); };

export function createTopologyRobot(topology: ValidatedVisualTopology): TopologyRobotVisual {
  const root = new THREE.Group(); root.name = topology.topologyId;
  const nodeGroups = new Map<string, THREE.Group>(); const dynamicJoints = new Map<string, THREE.Group>(); const geometryById = new Map((topology.geometries || []).map(geometry => [geometry.geometryId, geometry]));
  topology.nodes.forEach(node => { const group = new THREE.Group(); group.name = node.nodeId; applyTransform(group, node.localTransform); (node.geometryIds || []).forEach(id => { const definition = geometryById.get(id); if (!definition) return; const mesh = createGeometry(definition); if (definition.kind === 'box') mesh.position.y = definition.dimensions!.y / 2; group.add(mesh); }); nodeGroups.set(node.nodeId, group); });
  const attach = (nodeId: string, parent: THREE.Object3D) => { const group = nodeGroups.get(nodeId)!; parent.add(group); (topology.traversal.childJointIdsByNodeId.get(nodeId) || []).slice().sort().forEach(jointId => { const joint = topology.traversal.jointById.get(jointId)!; const pivot = new THREE.Group(); pivot.name = joint.jointId; applyTransform(pivot, joint.localTransform); group.add(pivot); dynamicJoints.set(joint.jointId, pivot); attach(joint.childNodeId, pivot); }); };
  topology.traversal.rootNodeIds.forEach(nodeId => attach(nodeId, root));
  return { root, dynamicJoints, nodeGroups, dispose: () => root.traverse(object => { if (object instanceof THREE.Mesh) { object.geometry.dispose(); const materials = Array.isArray(object.material) ? object.material : [object.material]; materials.forEach(item => item.dispose()); } }) };
}

/** Builds all topology roots in deterministic assembly order and applies only explicit attachments. */
export function createTopologyAssembly(assembly: ResolvedVisualAssembly): TopologyRobotVisual {
  const visual = createTopologyRobot(assembly.topology);
  assembly.traversal.rootOrder.forEach(rootId => { const definition = assembly.definition.roots.find(root => root.rootId === rootId); const node = definition && visual.nodeGroups.get(definition.rootNodeId); if (node) { const wrapper = new THREE.Group(); wrapper.name = `assembly-root:${rootId}`; applyTransform(wrapper, definition.localTransform); node.parent?.remove(node); wrapper.add(node); visual.root.add(wrapper); } });
  [...assembly.traversal.parentAttachmentByChain.entries()].sort(([a], [b]) => a.localeCompare(b)).forEach(([, attachment]) => { const parent = visual.nodeGroups.get(attachment.parentNodeId); const child = visual.nodeGroups.get(attachment.childRootNodeId); if (parent && child) composeAttachmentTransform(parent, child, attachment); });
  return visual;
}

export function applyTopologyJoint(joint: ValidatedVisualTopology['joints'][number], group: THREE.Group, sourceValue?: number) {
  const motion = resolveJointMotion(joint, sourceValue); const runtimeDiagnostic: TopologyRuntimeDiagnostic | undefined = motion.clamped ? { code: 'visual-limit-clamped', severity: 'warning', jointId: joint.jointId, message: `Source value for ${joint.jointId} exceeded its Workbench visual limit and was clamped for rendering.` } : undefined; group.userData.topologyDiagnostic = runtimeDiagnostic; group.position.set(joint.localTransform.translation.x + motion.translation.x, joint.localTransform.translation.y + motion.translation.y, joint.localTransform.translation.z + motion.translation.z); const localRotation = new THREE.Euler(joint.localTransform.rotation.x, joint.localTransform.rotation.y, joint.localTransform.rotation.z, joint.localTransform.rotation.order || 'XYZ'); group.quaternion.setFromEuler(localRotation); if (joint.jointType === 'revolute') { const axis = new THREE.Vector3(joint.axis!.x, joint.axis!.y, joint.axis!.z).normalize(); group.quaternion.multiply(new THREE.Quaternion().setFromAxisAngle(axis, motion.rotationRadians)); } return motion;
}
