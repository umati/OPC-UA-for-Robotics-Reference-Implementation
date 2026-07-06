import * as THREE from 'three';
import type { JointAngles, ProgramTelemetryDetails, RobotVisualModel, VisualizationMode, VisualizationOptions } from './types';

interface ProgramPreviewUpdate {
  robot: RobotVisualModel;
  currentJointAngles: JointAngles;
  telemetryDetails: ProgramTelemetryDetails;
  visualizationOptions: VisualizationOptions;
  mode: VisualizationMode;
  liveTelemetryActive: boolean;
}

interface TargetMarker {
  group: THREE.Group;
  label: THREE.Sprite;
  material: THREE.MeshStandardMaterial;
  labelMaterial: THREE.SpriteMaterial;
  labelTexture: THREE.CanvasTexture;
}

export class ProgramPreview {
  private readonly root = new THREE.Group();
  private readonly currentTargetMarker = createTargetMarker('Current Target', 0x7cf3d5);
  private readonly nextTargetMarker = createTargetMarker('Next Target', 0xffc76a);
  private readonly ghostToolPose = createGhostToolPose();
  private readonly futurePathMaterial = new THREE.LineDashedMaterial({
    color: 0x9bbcff,
    dashSize: 0.08,
    gapSize: 0.055,
    transparent: true,
    opacity: 0.72,
  });
  private readonly futurePathLine = new THREE.Line(new THREE.BufferGeometry(), this.futurePathMaterial);
  private readonly scratchCurrentPosition = new THREE.Vector3();
  private readonly scratchActivePosition = new THREE.Vector3();
  private readonly scratchNextPosition = new THREE.Vector3();

  public constructor(scene: THREE.Scene) {
    this.root.name = 'program-preview-root';
    this.currentTargetMarker.group.name = 'current-target-preview';
    this.nextTargetMarker.group.name = 'next-target-preview';
    this.futurePathLine.name = 'approximate-future-path-preview';
    this.futurePathLine.frustumCulled = false;

    this.root.add(this.currentTargetMarker.group, this.nextTargetMarker.group, this.ghostToolPose, this.futurePathLine);
    scene.add(this.root);
    this.setVisible(false);
  }

  public update(update: ProgramPreviewUpdate): void {
    const canUseServerProgramData = update.liveTelemetryActive
      && (update.mode === 'liveTelemetry' || update.mode === 'presentationDemo');
    const activeTarget = update.telemetryDetails.activeTargetJointAngles;
    const nextTarget = update.telemetryDetails.nextTargetJointAngles;
    const hasActiveTarget = canUseServerProgramData && hasAnyJointAngle(activeTarget);
    const hasNextTarget = canUseServerProgramData && hasAnyJointAngle(nextTarget);

    this.currentTargetMarker.group.visible = false;
    this.nextTargetMarker.group.visible = false;
    this.ghostToolPose.visible = false;
    this.futurePathLine.visible = false;

    if (!hasActiveTarget && !hasNextTarget) {
      this.setVisible(false);
      return;
    }

    this.setVisible(true);
    update.robot.getToolWorldPosition(this.scratchCurrentPosition);

    if (hasActiveTarget && activeTarget) {
      this.estimateToolPosition(
        update.robot,
        update.currentJointAngles,
        activeTarget,
        this.scratchActivePosition,
      );
      this.currentTargetMarker.group.position.copy(this.scratchActivePosition);
      this.currentTargetMarker.group.visible = update.visualizationOptions.showTargetMarkers;
      this.ghostToolPose.position.copy(this.scratchActivePosition);
      this.ghostToolPose.visible = update.visualizationOptions.showGhostPose;
    }

    if (hasNextTarget && nextTarget) {
      this.estimateToolPosition(
        update.robot,
        update.currentJointAngles,
        nextTarget,
        this.scratchNextPosition,
      );
      this.nextTargetMarker.group.position.copy(this.scratchNextPosition);
      this.nextTargetMarker.group.visible = update.visualizationOptions.showTargetMarkers;
    }

    if (hasActiveTarget && hasNextTarget && update.visualizationOptions.showFuturePreview) {
      this.updateFuturePath([
        this.scratchCurrentPosition,
        this.scratchActivePosition,
        this.scratchNextPosition,
      ]);
      this.futurePathLine.visible = true;
    }
  }

  public dispose(): void {
    disposeMarker(this.currentTargetMarker);
    disposeMarker(this.nextTargetMarker);
    disposeObject(this.ghostToolPose);
    this.futurePathLine.geometry.dispose();
    this.futurePathMaterial.dispose();
    this.root.removeFromParent();
  }

  private setVisible(isVisible: boolean): void {
    this.root.visible = isVisible;
  }

  private estimateToolPosition(
    robot: RobotVisualModel,
    currentJointAngles: JointAngles,
    targetJointAngles: Partial<JointAngles>,
    targetVector: THREE.Vector3,
  ): void {
    // This is a browser-only visual estimate from server-provided program target metadata.
    // It is not robot physics and is not an exact OPC UA Robotics trajectory.
    robot.setJointAngles({ ...currentJointAngles, ...targetJointAngles });
    robot.root.updateMatrixWorld(true);
    robot.getToolWorldPosition(targetVector);
    robot.setJointAngles(currentJointAngles);
    robot.root.updateMatrixWorld(true);
  }

  private updateFuturePath(points: THREE.Vector3[]): void {
    this.futurePathLine.geometry.dispose();
    this.futurePathLine.geometry = new THREE.BufferGeometry().setFromPoints(points);
    this.futurePathLine.computeLineDistances();
  }
}

function hasAnyJointAngle(angles: Partial<JointAngles> | null | undefined): boolean {
  return !!angles && Object.keys(angles).length > 0;
}

function createTargetMarker(label: string, color: number): TargetMarker {
  const material = new THREE.MeshStandardMaterial({
    color,
    emissive: color,
    emissiveIntensity: 0.36,
    roughness: 0.28,
    metalness: 0.1,
  });
  const marker = new THREE.Mesh(new THREE.SphereGeometry(0.07, 28, 14), material);
  marker.name = `${label.toLowerCase().replace(/\s+/g, '-')}-sphere`;

  const ringMaterial = new THREE.MeshBasicMaterial({ color, transparent: true, opacity: 0.72 });
  const ring = new THREE.Mesh(new THREE.TorusGeometry(0.14, 0.006, 8, 48), ringMaterial);
  ring.name = `${label.toLowerCase().replace(/\s+/g, '-')}-ring`;
  ring.rotation.x = Math.PI / 2;

  const labelSprite = createLabel(label, color);
  labelSprite.position.set(0, 0.2, 0);

  const group = new THREE.Group();
  group.add(marker, ring, labelSprite);

  return {
    group,
    label: labelSprite,
    material,
    labelMaterial: labelSprite.material,
    labelTexture: labelSprite.material.map as THREE.CanvasTexture,
  };
}

function createGhostToolPose(): THREE.Group {
  const group = new THREE.Group();
  group.name = 'ghost-target-tool-pose';

  const material = new THREE.MeshBasicMaterial({
    color: 0x7cf3d5,
    transparent: true,
    opacity: 0.22,
    depthWrite: false,
  });

  const sphere = new THREE.Mesh(new THREE.SphereGeometry(0.13, 32, 16), material);
  sphere.name = 'ghost-tool-envelope';
  const xAxis = createGhostAxis(0xff6b6b, new THREE.Vector3(0.24, 0, 0));
  const yAxis = createGhostAxis(0x7cf3d5, new THREE.Vector3(0, 0.24, 0));
  const zAxis = createGhostAxis(0x79a8ff, new THREE.Vector3(0, 0, 0.24));
  group.add(sphere, xAxis, yAxis, zAxis);
  return group;
}

function createGhostAxis(color: number, end: THREE.Vector3): THREE.Line {
  const geometry = new THREE.BufferGeometry().setFromPoints([new THREE.Vector3(), end]);
  const material = new THREE.LineBasicMaterial({ color, transparent: true, opacity: 0.62 });
  return new THREE.Line(geometry, material);
}

function createLabel(text: string, color: number): THREE.Sprite {
  const canvas = document.createElement('canvas');
  canvas.width = 256;
  canvas.height = 88;

  const context = canvas.getContext('2d');
  if (!context) {
    throw new Error('Unable to create program preview label canvas context.');
  }

  context.clearRect(0, 0, canvas.width, canvas.height);
  context.fillStyle = 'rgba(7, 11, 16, 0.82)';
  roundedRect(context, 12, 16, 232, 48, 10);
  context.fill();
  context.strokeStyle = `#${color.toString(16).padStart(6, '0')}`;
  context.lineWidth = 3;
  roundedRect(context, 12, 16, 232, 48, 10);
  context.stroke();
  context.fillStyle = '#eef5fb';
  context.font = '700 24px Arial, sans-serif';
  context.textAlign = 'center';
  context.textBaseline = 'middle';
  context.fillText(text, 128, 41);

  const texture = new THREE.CanvasTexture(canvas);
  texture.colorSpace = THREE.SRGBColorSpace;
  const material = new THREE.SpriteMaterial({ map: texture, transparent: true, depthWrite: false });
  const sprite = new THREE.Sprite(material);
  sprite.scale.set(0.62, 0.21, 1);
  return sprite;
}

function roundedRect(
  context: CanvasRenderingContext2D,
  x: number,
  y: number,
  width: number,
  height: number,
  radius: number,
): void {
  context.beginPath();
  context.moveTo(x + radius, y);
  context.lineTo(x + width - radius, y);
  context.quadraticCurveTo(x + width, y, x + width, y + radius);
  context.lineTo(x + width, y + height - radius);
  context.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
  context.lineTo(x + radius, y + height);
  context.quadraticCurveTo(x, y + height, x, y + height - radius);
  context.lineTo(x, y + radius);
  context.quadraticCurveTo(x, y, x + radius, y);
}

function disposeMarker(marker: TargetMarker): void {
  disposeObject(marker.group);
  marker.labelTexture.dispose();
  marker.labelMaterial.dispose();
  marker.material.dispose();
}

function disposeObject(root: THREE.Object3D): void {
  root.traverse((object) => {
    if (object instanceof THREE.Mesh || object instanceof THREE.Line) {
      object.geometry.dispose();
      disposeMaterials(object.material);
    } else if (object instanceof THREE.Sprite) {
      disposeMaterials(object.material);
    }
  });
}

function disposeMaterials(materialOrMaterials: THREE.Material | THREE.Material[]): void {
  const materials = Array.isArray(materialOrMaterials) ? materialOrMaterials : [materialOrMaterials];
  for (const material of materials) {
    material.dispose();
  }
}
