import * as THREE from 'three';

export type RobotVisual = { root: THREE.Group; joints: THREE.Group[]; dispose: () => void };
export type JointCalibration = { joint: 'base' | 'shoulder' | 'elbow' | 'wrist1' | 'wrist2' | 'wrist3'; rotationAxis: THREE.Vector3 };
// Fixed procedural geometry only; profile calibration is resolved upstream.
export const genericSixAxisCalibration: JointCalibration[] = [
  { joint: 'base', rotationAxis: new THREE.Vector3(0, 1, 0) },
  { joint: 'shoulder', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'elbow', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'wrist1', rotationAxis: new THREE.Vector3(1, 0, 0) },
  { joint: 'wrist2', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'wrist3', rotationAxis: new THREE.Vector3(1, 0, 0) },
];
export const visualJointRotation = (actualPositionRadians: number) => actualPositionRadians;
const material = (color: number, metalness = 0.15) => new THREE.MeshStandardMaterial({ color, metalness, roughness: 0.55 });
const box = (size: [number, number, number], color: number) => new THREE.Mesh(new THREE.BoxGeometry(...size), material(color));
const joint = (color = 0x4fbbb0) => new THREE.Mesh(new THREE.CylinderGeometry(0.17, 0.17, 0.18, 24), material(color, 0.45));

export function createGenericSixAxisRobot(): RobotVisual {
  const root = new THREE.Group(); root.name = 'GenericSixAxisRobot'; root.rotation.y = -0.35;
  const base = new THREE.Group(); root.add(base);
  base.add(new THREE.Mesh(new THREE.CylinderGeometry(0.55, 0.65, 0.18, 32), material(0x263d46, 0.5)));
  const joints: THREE.Group[] = [];
  const lengths = [0.58, 0.85, 0.72, 0.58, 0.42, 0.28];
  let parent = base;
  for (let index = 0; index < 6; index += 1) {
    const calibration = genericSixAxisCalibration[index];
    const group = new THREE.Group(); group.name = `Joint${index + 1}`; group.position.y = index === 0 ? 0.24 : lengths[index - 1]; group.userData.rotationAxis = calibration.rotationAxis;
    parent.add(group); group.add(joint());
    const link = box([0.22, lengths[index], 0.22], index % 2 ? 0x7b9296 : 0x477d80); link.position.y = lengths[index] / 2; group.add(link);
    joints.push(group); parent = group;
  }
  const flange = new THREE.Mesh(new THREE.CylinderGeometry(0.11, 0.11, 0.12, 20), material(0xd3a35e, 0.8)); flange.position.y = lengths[5]; parent.add(flange);
  return { root, joints, dispose: () => { root.traverse(object => { if (object instanceof THREE.Mesh) { object.geometry.dispose(); const materials = Array.isArray(object.material) ? object.material : [object.material]; materials.forEach(item => item.dispose()); } }); } };
}
