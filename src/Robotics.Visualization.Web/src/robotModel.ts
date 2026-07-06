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

interface RobotMaterialSet {
  body: THREE.MeshStandardMaterial;
  dark: THREE.MeshStandardMaterial;
  accent: THREE.MeshStandardMaterial;
  metal: THREE.MeshStandardMaterial;
  light: THREE.MeshStandardMaterial;
  warning: THREE.MeshStandardMaterial;
}

export function degreesToRadians(degrees: number): number {
  return (degrees * Math.PI) / 180;
}

export function createPrimitiveRobotModel(): PrimitiveRobotModel {
  const materials = createRobotMaterialSet();
  const root = new THREE.Group();
  root.name = 'robot-base-root';

  // V4B is still a procedural fallback model. V4C can replace the visual
  // meshes with a segmented GLB as long as these pivots and parent groups stay
  // aligned with the live S/L/U/R/B/T joint contract.
  const baseGroup = new THREE.Group();
  baseGroup.name = 'procedural-base-pedestal';
  root.add(baseGroup);
  baseGroup.add(cylinder('base-lower-plinth', 0.66, 0.16, materials.dark, 72, 0, 0.12));
  baseGroup.add(cylinder('base-brushed-metal-ring', 0.52, 0.08, materials.metal, 72, 0, 0.24));
  baseGroup.add(cylinder('base-top-turntable', 0.42, 0.16, materials.body, 72, 0, 0.34));
  baseGroup.add(createStatusLight('base-ready-status-light', materials.light, 0.06, new THREE.Vector3(0.36, 0.43, 0.12)));

  const sAxis = new THREE.Group();
  sAxis.name = 'S-axis-rotating-group';
  sAxis.position.y = 0.42;
  root.add(sAxis);

  const column = createJointHousing('rotating-shoulder-column', 0.3, 0.78, 'y', materials, 'S');
  column.position.y = 0.39;
  sAxis.add(column);
  sAxis.add(createAxisLabel('S', new THREE.Vector3(-0.48, 0.2, 0.28)));
  sAxis.add(createAxisRing('S-axis-rotation-ring', 0.5, 'y', materials.accent));

  const lAxis = new THREE.Group();
  lAxis.name = 'L-axis-group';
  lAxis.position.set(0, 0.78, 0);
  sAxis.add(lAxis);

  const shoulder = createJointHousing('L-axis-shoulder-housing', 0.3, 0.68, 'x', materials, 'L');
  lAxis.add(shoulder);
  lAxis.add(createAxisLabel('L', new THREE.Vector3(-0.48, 0.25, 0.3)));

  const upperArm = createArmSegment('upper-arm-cover', 1.28, 'y', materials);
  upperArm.position.y = 0.64;
  lAxis.add(upperArm);

  const uAxis = new THREE.Group();
  uAxis.name = 'U-axis-group';
  uAxis.position.set(0, 1.28, 0);
  lAxis.add(uAxis);

  const elbow = createJointHousing('U-axis-elbow-housing', 0.27, 0.58, 'x', materials, 'U');
  uAxis.add(elbow);
  uAxis.add(createAxisLabel('U', new THREE.Vector3(-0.43, 0.26, 0.27)));

  const forearmLength = 1.28;
  const forearm = createArmSegment('forearm-cover', forearmLength, 'x', materials);
  forearm.position.x = forearmLength / 2;
  uAxis.add(forearm);

  const rAxis = new THREE.Group();
  rAxis.name = 'R-axis-group';
  rAxis.position.set(forearmLength, 0, 0);
  uAxis.add(rAxis);

  const rollHousing = createJointHousing('R-axis-roll-housing', 0.19, 0.48, 'x', materials, 'R');
  rAxis.add(rollHousing);
  rAxis.add(createAxisLabel('R', new THREE.Vector3(0.02, 0.32, 0.24)));

  const bAxis = new THREE.Group();
  bAxis.name = 'B-axis-group';
  bAxis.position.set(0.34, 0, 0);
  rAxis.add(bAxis);

  const bendHousing = createJointHousing('B-axis-bend-housing', 0.18, 0.4, 'z', materials, 'B');
  bendHousing.position.x = 0.18;
  bAxis.add(bendHousing);
  bAxis.add(createAxisLabel('B', new THREE.Vector3(0.1, 0.28, 0.26)));

  const tAxis = new THREE.Group();
  tAxis.name = 'T-axis-group';
  tAxis.position.set(0.42, 0, 0);
  bAxis.add(tAxis);

  const toolGroup = new THREE.Group();
  toolGroup.name = 'tool-group';
  tAxis.add(toolGroup);

  const flange = cylinder('tool-flange', 0.18, 0.12, materials.metal, 48);
  flange.rotation.z = Math.PI / 2;
  toolGroup.add(flange);
  toolGroup.add(createAxisLabel('T', new THREE.Vector3(0.08, 0.25, 0.2)));

  const nozzle = cylinder('compact-tool-nozzle', 0.09, 0.22, materials.dark, 32);
  nozzle.rotation.z = Math.PI / 2;
  nozzle.position.x = 0.17;
  toolGroup.add(nozzle);

  const toolMarker = cone('tool-marker', 0.085, 0.24, materials.warning);
  toolMarker.rotation.z = -Math.PI / 2;
  toolMarker.position.x = 0.38;
  toolGroup.add(toolMarker);

  const toolTip = new THREE.Group();
  toolTip.name = 'tool-center-point';
  toolTip.position.x = 0.5;
  toolGroup.add(toolTip);

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
    toolObject: toolTip,
    setJointAngles(angles): void {
      setPrimitiveJointAngles(this, angles);
    },
    getToolWorldPosition(targetVector): THREE.Vector3 {
      return this.toolObject.getWorldPosition(targetVector);
    },
    dispose(): void {
      disposeObjectTree(root);
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
      disposeMaterials(object.material);
    } else if (object instanceof THREE.Sprite) {
      disposeMaterials(object.material);
    } else if (object instanceof THREE.LineSegments) {
      object.geometry.dispose();
      disposeMaterials(object.material);
    }
  });
}

function disposeMaterials(materialOrMaterials: THREE.Material | THREE.Material[]): void {
  const materials = Array.isArray(materialOrMaterials) ? materialOrMaterials : [materialOrMaterials];
  for (const material of materials) {
    const maybeMappedMaterial = material as THREE.Material & { map?: THREE.Texture | null };
    maybeMappedMaterial.map?.dispose();
    material.dispose();
  }
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

function createRobotMaterialSet(): RobotMaterialSet {
  return {
    body: new THREE.MeshStandardMaterial({ color: 0xdbe2e8, roughness: 0.38, metalness: 0.26 }),
    dark: new THREE.MeshStandardMaterial({ color: 0x202a33, roughness: 0.58, metalness: 0.42 }),
    accent: new THREE.MeshStandardMaterial({ color: 0x3f9cff, roughness: 0.34, metalness: 0.32 }),
    metal: new THREE.MeshStandardMaterial({ color: 0x8f9ba6, roughness: 0.28, metalness: 0.78 }),
    light: new THREE.MeshStandardMaterial({
      color: 0x75ffd8,
      emissive: 0x32f4c0,
      emissiveIntensity: 1.6,
      roughness: 0.3,
      metalness: 0.1,
    }),
    warning: new THREE.MeshStandardMaterial({
      color: 0xffb347,
      emissive: 0x7a3c00,
      emissiveIntensity: 0.22,
      roughness: 0.34,
      metalness: 0.18,
    }),
  };
}

function createJointHousing(
  name: string,
  radius: number,
  length: number,
  axis: 'x' | 'y' | 'z',
  materials: RobotMaterialSet,
  label: JointName,
): THREE.Group {
  const group = new THREE.Group();
  group.name = name;

  const housing = cylinder(`${name}-dark-shell`, radius, length, materials.dark, 64);
  orientAlongAxis(housing, axis);
  group.add(housing);

  const leftCap = cylinder(`${name}-left-cap-${label}`, radius * 1.06, 0.055, materials.metal, 64);
  const rightCap = cylinder(`${name}-right-cap-${label}`, radius * 1.06, 0.055, materials.metal, 64);
  orientAlongAxis(leftCap, axis);
  orientAlongAxis(rightCap, axis);
  setAxisOffset(leftCap, axis, -length / 2 - 0.028);
  setAxisOffset(rightCap, axis, length / 2 + 0.028);
  group.add(leftCap, rightCap);

  const axisRing = createAxisRing(`${name}-axis-ring-${label}`, radius * 1.28, axis, materials.accent);
  group.add(axisRing);

  return group;
}

function createArmSegment(name: string, length: number, axis: 'x' | 'y', materials: RobotMaterialSet): THREE.Group {
  const group = new THREE.Group();
  group.name = name;

  const cover =
    axis === 'x'
      ? box(`${name}-main-cover`, length, 0.24, 0.3, materials.body)
      : box(`${name}-main-cover`, 0.3, length, 0.28, materials.body);
  group.add(cover);

  const spine =
    axis === 'x'
      ? box(`${name}-dark-service-spine`, length * 0.86, 0.055, 0.335, materials.dark)
      : box(`${name}-dark-service-spine`, 0.335, length * 0.86, 0.055, materials.dark);
  spine.position.z = axis === 'x' ? -0.015 : -0.16;
  group.add(spine);

  const capA = cylinder(`${name}-rounded-cap-a`, 0.17, 0.32, materials.metal, 48);
  const capB = cylinder(`${name}-rounded-cap-b`, 0.17, 0.32, materials.metal, 48);
  orientAlongAxis(capA, axis);
  orientAlongAxis(capB, axis);
  setAxisOffset(capA, axis, -length / 2);
  setAxisOffset(capB, axis, length / 2);
  group.add(capA, capB);

  return group;
}

function createAxisLabel(label: JointName, position: THREE.Vector3): THREE.Sprite {
  const canvas = document.createElement('canvas');
  canvas.width = 96;
  canvas.height = 96;

  const context = canvas.getContext('2d');
  if (!context) {
    throw new Error('Unable to create axis label canvas context.');
  }

  context.clearRect(0, 0, canvas.width, canvas.height);
  context.fillStyle = 'rgba(8, 13, 18, 0.72)';
  context.beginPath();
  context.arc(48, 48, 34, 0, Math.PI * 2);
  context.fill();
  context.strokeStyle = 'rgba(112, 190, 255, 0.82)';
  context.lineWidth = 4;
  context.stroke();
  context.fillStyle = '#eaf6ff';
  context.font = '600 42px Arial, sans-serif';
  context.textAlign = 'center';
  context.textBaseline = 'middle';
  context.fillText(label, 48, 50);

  const texture = new THREE.CanvasTexture(canvas);
  texture.colorSpace = THREE.SRGBColorSpace;
  const material = new THREE.SpriteMaterial({ map: texture, transparent: true, depthWrite: false });
  const sprite = new THREE.Sprite(material);
  sprite.name = `${label}-axis-label`;
  sprite.position.copy(position);
  sprite.scale.setScalar(0.18);
  return sprite;
}

function createAxisRing(name: string, radius: number, axis: 'x' | 'y' | 'z', material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.TorusGeometry(radius, 0.009, 8, 72);
  const ring = mesh(name, geometry, material, false);
  if (axis === 'x') {
    ring.rotation.y = Math.PI / 2;
  } else if (axis === 'y') {
    ring.rotation.x = Math.PI / 2;
  }

  return ring;
}

function createStatusLight(
  name: string,
  material: THREE.Material,
  radius: number,
  position: THREE.Vector3,
): THREE.Mesh {
  const light = mesh(name, new THREE.SphereGeometry(radius, 24, 12), material, false);
  light.position.copy(position);
  return light;
}

function cylinder(
  name: string,
  radius: number,
  height: number,
  material: THREE.Material,
  radialSegments = 48,
  x = 0,
  y = 0,
  z = 0,
): THREE.Mesh {
  const geometry = new THREE.CylinderGeometry(radius, radius, height, radialSegments);
  const part = mesh(name, geometry, material);
  part.position.set(x, y, z);
  return part;
}

function cone(name: string, radius: number, height: number, material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.ConeGeometry(radius, height, 32);
  return mesh(name, geometry, material);
}

function box(name: string, width: number, height: number, depth: number, material: THREE.Material): THREE.Mesh {
  const geometry = new THREE.BoxGeometry(width, height, depth);
  return mesh(name, geometry, material);
}

function orientAlongAxis(object: THREE.Object3D, axis: 'x' | 'y' | 'z'): void {
  if (axis === 'x') {
    object.rotation.z = Math.PI / 2;
  } else if (axis === 'z') {
    object.rotation.x = Math.PI / 2;
  }
}

function setAxisOffset(object: THREE.Object3D, axis: 'x' | 'y' | 'z', value: number): void {
  if (axis === 'x') {
    object.position.x = value;
  } else if (axis === 'y') {
    object.position.y = value;
  } else {
    object.position.z = value;
  }
}

function mesh(
  name: string,
  geometry: THREE.BufferGeometry,
  material: THREE.Material,
  withOutline = true,
): THREE.Mesh {
  const part = new THREE.Mesh(geometry, material);
  part.name = name;
  part.castShadow = true;
  part.receiveShadow = true;

  if (!withOutline) {
    return part;
  }

  const outline = new THREE.LineSegments(
    new THREE.EdgesGeometry(geometry, 24),
    new THREE.LineBasicMaterial({ color: 0x101820, transparent: true, opacity: 0.5 }),
  );
  part.add(outline);

  return part;
}
