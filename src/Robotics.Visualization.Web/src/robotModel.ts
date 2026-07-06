import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';
import {
  homeJointAngles,
  jointNames,
  type JointAngles,
  type JointName,
  type PrimitiveRobotModel,
  type RobotModelLoadResult,
  type RobotVisualModel,
} from './types';

export const glbRobotAssetUrl = '/assets/robots/six-axis-reference.glb';
export const glbRobotAssetPath = 'public/assets/robots/six-axis-reference.glb';

const expectedGlbNodeNames = ['RobotRoot', 'AxisS', 'AxisL', 'AxisU', 'AxisR', 'AxisB', 'AxisT', 'Tool'] as const;

const materials = {
  base: new THREE.MeshStandardMaterial({ color: 0x2a3441, roughness: 0.64, metalness: 0.35 }),
  column: new THREE.MeshStandardMaterial({ color: 0x52616f, roughness: 0.48, metalness: 0.4 }),
  arm: new THREE.MeshStandardMaterial({ color: 0xd8dde3, roughness: 0.42, metalness: 0.24 }),
  joint: new THREE.MeshStandardMaterial({ color: 0x2f8cff, roughness: 0.38, metalness: 0.35 }),
  wrist: new THREE.MeshStandardMaterial({ color: 0x89a0b7, roughness: 0.44, metalness: 0.32 }),
  tool: new THREE.MeshStandardMaterial({ color: 0xffb454, roughness: 0.36, metalness: 0.2 }),
};

export function degreesToRadians(degrees: number): number {
  return (degrees * Math.PI) / 180;
}

export function createPrimitiveRobotModel(): PrimitiveRobotModel {
  const root = new THREE.Group();
  root.name = 'robot-base-root';

  const base = cylinder('base-cylinder', 0.48, 0.28, materials.base);
  base.position.y = 0.14;
  root.add(base);

  const sAxis = new THREE.Group();
  sAxis.name = 'S-axis-rotating-group';
  sAxis.position.y = 0.28;
  root.add(sAxis);

  const column = cylinder('rotating-column', 0.28, 0.78, materials.column);
  column.position.y = 0.39;
  sAxis.add(column);

  const lAxis = new THREE.Group();
  lAxis.name = 'L-axis-group';
  lAxis.position.set(0, 0.78, 0);
  sAxis.add(lAxis);
  lAxis.add(jointSphere('L-axis-pivot'));

  const shoulder = box('shoulder-crosshead', 0.62, 0.22, 0.44, materials.joint);
  shoulder.position.set(0, 0, 0);
  lAxis.add(shoulder);

  const upperArm = box('upper-arm-link', 0.32, 1.28, 0.34, materials.arm);
  upperArm.position.y = 0.64;
  lAxis.add(upperArm);

  const uAxis = new THREE.Group();
  uAxis.name = 'U-axis-group';
  uAxis.position.set(0, 1.28, 0);
  lAxis.add(uAxis);
  uAxis.add(jointSphere('U-axis-pivot'));

  const elbow = cylinder('elbow-housing', 0.24, 0.5, materials.joint);
  elbow.rotation.z = Math.PI / 2;
  uAxis.add(elbow);

  const forearmLength = 1.28;
  const forearm = box('lower-arm-link', forearmLength, 0.28, 0.32, materials.arm);
  forearm.position.x = forearmLength / 2;
  uAxis.add(forearm);

  const rAxis = new THREE.Group();
  rAxis.name = 'R-axis-group';
  rAxis.position.set(forearmLength, 0, 0);
  uAxis.add(rAxis);

  const rollHousing = cylinder('R-axis-roll-housing', 0.18, 0.46, materials.wrist);
  rollHousing.rotation.z = Math.PI / 2;
  rAxis.add(rollHousing);

  const bAxis = new THREE.Group();
  bAxis.name = 'B-axis-group';
  bAxis.position.set(0.34, 0, 0);
  rAxis.add(bAxis);
  bAxis.add(jointSphere('B-axis-pivot', 0.18));

  const bendHousing = box('B-axis-bend-housing', 0.42, 0.26, 0.3, materials.wrist);
  bendHousing.position.x = 0.18;
  bAxis.add(bendHousing);

  const tAxis = new THREE.Group();
  tAxis.name = 'T-axis-group';
  tAxis.position.set(0.42, 0, 0);
  bAxis.add(tAxis);

  const toolGroup = new THREE.Group();
  toolGroup.name = 'tool-group';
  tAxis.add(toolGroup);

  const flange = cylinder('tool-flange', 0.16, 0.12, materials.tool);
  flange.rotation.z = Math.PI / 2;
  toolGroup.add(flange);

  const toolMarker = cone('tool-marker', 0.11, 0.38, materials.tool);
  toolMarker.rotation.z = -Math.PI / 2;
  toolMarker.position.x = 0.26;
  toolGroup.add(toolMarker);

  const joints: Record<JointName, THREE.Group> = {
    S: sAxis,
    L: lAxis,
    U: uAxis,
    R: rAxis,
    B: bAxis,
    T: tAxis,
  };

  const model: PrimitiveRobotModel = {
    root,
    joints,
    toolGroup,
    toolObject: toolGroup,
    setJointAngles(angles): void {
      setPrimitiveJointAngles(this, angles);
    },
    getToolWorldPosition(targetVector): THREE.Vector3 {
      return this.toolObject.getWorldPosition(targetVector);
    },
    dispose(): void {
      root.traverse((object) => {
        if (object instanceof THREE.Mesh) {
          object.geometry.dispose();
        }
      });
    },
  };

  model.setJointAngles(homeJointAngles);

  return model;
}

export function setPrimitiveJointAngles(robot: PrimitiveRobotModel, angles: Partial<JointAngles>): void {
  for (const jointName of jointNames) {
    const angle = angles[jointName];

    if (angle === undefined) {
      continue;
    }

    const radians = degreesToRadians(angle);

    switch (jointName) {
      case 'S':
        robot.joints.S.rotation.y = radians;
        break;
      case 'L':
        robot.joints.L.rotation.z = radians;
        break;
      case 'U':
        robot.joints.U.rotation.z = radians;
        break;
      case 'R':
        robot.joints.R.rotation.x = radians;
        break;
      case 'B':
        robot.joints.B.rotation.z = radians;
        break;
      case 'T':
        robot.joints.T.rotation.x = radians;
        break;
    }
  }
}

export function resetHome(robot: RobotVisualModel): JointAngles {
  robot.setJointAngles(homeJointAngles);
  return { ...homeJointAngles };
}

export async function loadRobotVisualModel(): Promise<RobotModelLoadResult> {
  try {
    const glbModel = await loadGlbRobotModel();
    return {
      model: glbModel,
      status: 'glbLoaded',
      message: `GLB model loaded from ${glbRobotAssetPath}.`,
    };
  } catch (error) {
    const detail = error instanceof Error ? error.message : String(error);
    const warning = `GLB robot model unavailable: ${detail}. Using primitive fallback model.`;
    console.warn(warning);

    return {
      model: createPrimitiveRobotModel(),
      status: 'glbFailed',
      message: warning,
    };
  }
}

async function loadGlbRobotModel(): Promise<RobotVisualModel> {
  const loader = new GLTFLoader();
  const gltf = await loader.loadAsync(glbRobotAssetUrl);
  let nodes: Record<(typeof expectedGlbNodeNames)[number], THREE.Object3D>;

  try {
    nodes = findExpectedNodes(gltf.scene);
  } catch (error) {
    disposeObjectTree(gltf.scene);
    throw error;
  }

  nodes.RobotRoot.traverse((object) => {
    if (object instanceof THREE.Mesh) {
      object.castShadow = true;
      object.receiveShadow = true;
    }
  });

  const model: RobotVisualModel = {
    root: nodes.RobotRoot,
    toolObject: nodes.Tool,
    setJointAngles(angles): void {
      setGlbJointAngles(nodes, angles);
    },
    getToolWorldPosition(targetVector): THREE.Vector3 {
      return nodes.Tool.getWorldPosition(targetVector);
    },
    dispose(): void {
      disposeObjectTree(nodes.RobotRoot);
    },
  };

  model.setJointAngles(homeJointAngles);
  return model;
}

function disposeObjectTree(root: THREE.Object3D): void {
  root.traverse((object) => {
    if (object instanceof THREE.Mesh) {
      object.geometry.dispose();
      const materials = Array.isArray(object.material) ? object.material : [object.material];
      for (const material of materials) {
        material.dispose();
      }
    }
  });
}

function findExpectedNodes(root: THREE.Object3D): Record<(typeof expectedGlbNodeNames)[number], THREE.Object3D> {
  const nodes = {} as Record<(typeof expectedGlbNodeNames)[number], THREE.Object3D>;
  const missing: string[] = [];

  for (const nodeName of expectedGlbNodeNames) {
    const node = root.getObjectByName(nodeName);
    if (node) {
      nodes[nodeName] = node;
    } else {
      missing.push(nodeName);
    }
  }

  if (missing.length > 0) {
    throw new Error(`missing required GLB nodes: ${missing.join(', ')}`);
  }

  return nodes;
}

function setGlbJointAngles(
  nodes: Record<(typeof expectedGlbNodeNames)[number], THREE.Object3D>,
  angles: Partial<JointAngles>,
): void {
  for (const jointName of jointNames) {
    const angle = angles[jointName];

    if (angle === undefined) {
      continue;
    }

    const radians = degreesToRadians(angle);

    switch (jointName) {
      case 'S':
        nodes.AxisS.rotation.y = radians;
        break;
      case 'L':
        nodes.AxisL.rotation.z = radians;
        break;
      case 'U':
        nodes.AxisU.rotation.z = radians;
        break;
      case 'R':
        nodes.AxisR.rotation.x = radians;
        break;
      case 'B':
        nodes.AxisB.rotation.z = radians;
        break;
      case 'T':
        nodes.AxisT.rotation.x = radians;
        break;
    }
  }
}

function cylinder(name: string, radius: number, height: number, material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.CylinderGeometry(radius, radius, height, 48);
  return mesh(name, geometry, material);
}

function cone(name: string, radius: number, height: number, material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.ConeGeometry(radius, height, 32);
  return mesh(name, geometry, material);
}

function box(name: string, width: number, height: number, depth: number, material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.BoxGeometry(width, height, depth);
  return mesh(name, geometry, material);
}

function jointSphere(name: string, radius = 0.2): THREE.Mesh {
  const geometry = new THREE.SphereGeometry(radius, 32, 16);
  return mesh(name, geometry, materials.joint);
}

function mesh(name: string, geometry: THREE.BufferGeometry, material: THREE.Material): THREE.Mesh {
  const part = new THREE.Mesh(geometry, material);
  part.name = name;
  part.castShadow = true;
  part.receiveShadow = true;

  const outline = new THREE.LineSegments(
    new THREE.EdgesGeometry(geometry, 24),
    new THREE.LineBasicMaterial({ color: 0x101820, transparent: true, opacity: 0.5 }),
  );
  part.add(outline);

  return part;
}
