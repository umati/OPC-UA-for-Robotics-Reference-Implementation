import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import './style.css';
import { createRobotModel, resetHome, setJointAngles } from './robotModel';
import { getAxisPositions, getAxisVelocities, RobotTelemetryClient, type RobotTelemetryData } from './telemetryClient';
import { createRobotUi } from './ui';
import { homeJointAngles, type JointAngles, type JointName, type UiController } from './types';

// The browser renders telemetry only. Robot simulation and program execution stay server-owned.

const sceneHost = requiredElement<HTMLDivElement>('scene');
const controlsHost = requiredElement<HTMLElement>('controls');

const scene = new THREE.Scene();
scene.background = new THREE.Color(0x090d13);

const camera = new THREE.PerspectiveCamera(45, sceneHost.clientWidth / sceneHost.clientHeight, 0.1, 100);
camera.position.set(3.8, 2.9, 4.3);

const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
renderer.setSize(sceneHost.clientWidth, sceneHost.clientHeight);
renderer.shadowMap.enabled = true;
renderer.shadowMap.type = THREE.PCFSoftShadowMap;
sceneHost.append(renderer.domElement);

const orbitControls = new OrbitControls(camera, renderer.domElement);
orbitControls.enableDamping = true;
orbitControls.target.set(0.7, 1.25, 0);
orbitControls.maxPolarAngle = Math.PI * 0.48;
orbitControls.minDistance = 2;
orbitControls.maxDistance = 8;
orbitControls.update();

const grid = new THREE.GridHelper(7, 28, 0x2f8cff, 0x25313d);
const gridMaterial = grid.material as THREE.Material;
gridMaterial.transparent = true;
gridMaterial.opacity = 0.42;
scene.add(grid);

const axes = new THREE.AxesHelper(1.25);
axes.position.set(-2.7, 0.02, -2.7);
scene.add(axes);

const ambientLight = new THREE.HemisphereLight(0xe8f4ff, 0x15191f, 1.9);
scene.add(ambientLight);

const keyLight = new THREE.DirectionalLight(0xffffff, 2.4);
keyLight.position.set(3.6, 5.2, 2.4);
keyLight.castShadow = true;
keyLight.shadow.mapSize.set(2048, 2048);
scene.add(keyLight);

const fillLight = new THREE.DirectionalLight(0x6aa8ff, 0.9);
fillLight.position.set(-3.2, 2.1, -3.4);
scene.add(fillLight);

const floor = new THREE.Mesh(
  new THREE.PlaneGeometry(7, 7),
  new THREE.ShadowMaterial({ color: 0x000000, opacity: 0.28 }),
);
floor.rotation.x = -Math.PI / 2;
floor.receiveShadow = true;
scene.add(floor);

const robot = createRobotModel();
scene.add(robot.root);

let jointAngles: JointAngles = { ...homeJointAngles };
let demoRunning = false;
let demoStartMs = 0;
let ui: UiController;
let liveTelemetryActive = false;

const telemetryClient = new RobotTelemetryClient({
  onStatusChange(status, detail): void {
    ui.setConnectionStatus(status, detail);

    if (status === 'connected') {
      liveTelemetryActive = true;
      stopDemo();
      ui.setMode('liveTelemetry');
      ui.setManualControlsEnabled(false);
      return;
    }

    if (status === 'disconnected' || status === 'error') {
      liveTelemetryActive = false;
      ui.setManualControlsEnabled(true);
      ui.setMode(demoRunning ? 'localDemo' : 'manual');
    }
  },
  onTelemetry(message): void {
    applyLiveTelemetry(message);
  },
});

ui = createRobotUi({
  container: controlsHost,
  initialAngles: jointAngles,
  onJointChange(jointName: JointName, value: number): void {
    if (liveTelemetryActive) {
      return;
    }

    stopDemo();
    jointAngles = { ...jointAngles, [jointName]: value };
    setJointAngles(robot, jointAngles);
    ui.setAngles(jointAngles);
    ui.setMode('manual');
  },
  onResetHome(): void {
    if (liveTelemetryActive) {
      return;
    }

    stopDemo();
    jointAngles = resetHome(robot);
    ui.setAngles(jointAngles);
    ui.setVelocities({});
    ui.setMode('manual');
  },
  onDemoStart(): void {
    if (liveTelemetryActive) {
      return;
    }

    demoRunning = true;
    demoStartMs = performance.now();
    ui.setMode('localDemo');
  },
  onDemoStop(): void {
    stopDemo();
  },
  onTelemetryConnect(url: string): void {
    stopDemo();
    ui.setMode('liveTelemetry');
    ui.setManualControlsEnabled(false);
    telemetryClient.connect(url);
  },
  onTelemetryDisconnect(): void {
    telemetryClient.disconnect();
  },
});

window.addEventListener('resize', resizeRenderer);
resizeRenderer();
renderer.setAnimationLoop(render);

function render(nowMs: number): void {
  if (demoRunning) {
    jointAngles = demoAngles((nowMs - demoStartMs) / 1000);
    setJointAngles(robot, jointAngles);
    ui.setAngles(jointAngles);
  }

  orbitControls.update();
  renderer.render(scene, camera);
}

function stopDemo(): void {
  if (!demoRunning) {
    return;
  }

  demoRunning = false;

  if (!liveTelemetryActive) {
    ui.setMode('manual');
  }
}

function applyLiveTelemetry(message: RobotTelemetryData): void {
  const telemetryAngles = getAxisPositions(message);
  const updatedJointAngles = { ...jointAngles };
  let hasValidPosition = false;

  for (const jointName of Object.keys(telemetryAngles) as JointName[]) {
    const angle = telemetryAngles[jointName];
    if (angle === undefined) {
      continue;
    }

    updatedJointAngles[jointName] = angle;
    hasValidPosition = true;
  }

  if (hasValidPosition) {
    jointAngles = updatedJointAngles;
    setJointAngles(robot, jointAngles);
    ui.setAngles(jointAngles);
  }

  ui.setVelocities(getAxisVelocities(message));
  ui.setTelemetryDetails({
    timestampUtc: message.timestampUtc,
    currentProgramName: message.currentProgramName,
    programExecutionState: message.programExecutionState,
    currentStepIndex: message.currentStepIndex,
    isMoving: message.isMoving,
  });
}

function demoAngles(seconds: number): JointAngles {
  return {
    S: Math.sin(seconds * 0.55) * 70,
    L: Math.sin(seconds * 0.78) * 34,
    U: Math.sin(seconds * 0.67 + 0.9) * 42,
    R: Math.sin(seconds * 1.08 + 0.3) * 95,
    B: Math.sin(seconds * 0.92 + 1.2) * 38,
    T: Math.sin(seconds * 1.34) * 180,
  };
}

function resizeRenderer(): void {
  const width = sceneHost.clientWidth;
  const height = sceneHost.clientHeight;

  camera.aspect = width / height;
  camera.updateProjectionMatrix();
  renderer.setSize(width, height);
}

function requiredElement<T extends HTMLElement>(id: string): T {
  const element = document.getElementById(id);

  if (!element) {
    throw new Error(`Missing required element: #${id}`);
  }

  return element as T;
}
