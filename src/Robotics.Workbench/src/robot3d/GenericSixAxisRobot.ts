import * as THREE from 'three';
import { referenceServerSixAxisTopology } from './referenceServerSixAxisTopology';
import { createTopologyRobot } from './topologyRenderer';
import { compileVisualTopology } from './topologyValidator';

export type RobotVisual = { root: THREE.Group; joints: THREE.Group[]; dispose: () => void };
export type JointCalibration = { joint: 'base' | 'shoulder' | 'elbow' | 'wrist1' | 'wrist2' | 'wrist3'; rotationAxis: THREE.Vector3 };

export const genericSixAxisCalibration: JointCalibration[] = [
  { joint: 'base', rotationAxis: new THREE.Vector3(0, 1, 0) },
  { joint: 'shoulder', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'elbow', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'wrist1', rotationAxis: new THREE.Vector3(1, 0, 0) },
  { joint: 'wrist2', rotationAxis: new THREE.Vector3(0, 0, 1) },
  { joint: 'wrist3', rotationAxis: new THREE.Vector3(1, 0, 0) },
];

export const visualJointRotation = (actualPositionRadians: number) => actualPositionRadians;

const jointIds = ['joint-base', 'joint-shoulder', 'joint-elbow', 'joint-wrist1', 'joint-wrist2', 'joint-wrist3'];

/** Compatibility wrapper retained for the viewport; construction is topology-driven. */
export function createGenericSixAxisRobot(): RobotVisual {
  const robot = createTopologyRobot(compileVisualTopology(referenceServerSixAxisTopology));
  return { root: robot.root, joints: jointIds.map(id => robot.dynamicJoints.get(id)!), dispose: robot.dispose };
}
