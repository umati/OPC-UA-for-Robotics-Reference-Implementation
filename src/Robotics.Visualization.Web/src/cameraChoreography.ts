import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';

const presentationTarget = new THREE.Vector3(0.82, 1.34, 0.02);
const presentationRadius = 6.25;
const presentationHeight = 3.15;
const presentationAngularSpeed = 0.075;

export class CameraChoreography {
  private readonly camera: THREE.PerspectiveCamera;
  private readonly controls: OrbitControls;
  private readonly target = presentationTarget.clone();
  private readonly position = new THREE.Vector3();
  private isRunning = false;
  private startMs = 0;
  private startAngle = 0;

  public constructor(camera: THREE.PerspectiveCamera, controls: OrbitControls) {
    this.camera = camera;
    this.controls = controls;
  }

  public start(nowMs: number): void {
    this.isRunning = true;
    this.startMs = nowMs;
    this.startAngle = Math.atan2(
      this.camera.position.z - this.target.z,
      this.camera.position.x - this.target.x,
    );
    this.moveToPresentationAngle();
  }

  public stop(): void {
    this.isRunning = false;
    this.controls.target.copy(this.target);
    this.controls.update();
  }

  public resetCamera(): void {
    this.isRunning = false;
    this.moveToPresentationAngle();
  }

  public update(nowMs: number): void {
    if (!this.isRunning) {
      return;
    }

    const elapsedSeconds = (nowMs - this.startMs) / 1000;
    const angle = this.startAngle + elapsedSeconds * presentationAngularSpeed;
    const breathingHeight = Math.sin(elapsedSeconds * 0.22) * 0.18;

    this.position.set(
      this.target.x + Math.cos(angle) * presentationRadius,
      presentationHeight + breathingHeight,
      this.target.z + Math.sin(angle) * presentationRadius,
    );

    this.camera.position.copy(this.position);
    this.controls.target.copy(this.target);
    this.controls.update();
  }

  private moveToPresentationAngle(): void {
    this.camera.position.set(4.85, 3.25, 5.2);
    this.controls.target.copy(this.target);
    this.controls.update();
  }
}
