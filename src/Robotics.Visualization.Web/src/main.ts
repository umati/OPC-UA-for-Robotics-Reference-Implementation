import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import './style.css';
import { CameraChoreography } from './cameraChoreography';
import { createToolFrame, createWorldFrame } from './frameHelpers';
import { PathTrail } from './pathTrail';
import { createPresentationOverlay, type PresentationMotionSource } from './presentationMode';
import { createPrimitiveRobotModel, loadRobotVisualModel, resetHome } from './robotModel';
import { createStatusOverlay } from './statusOverlay';
import { getAxisPositions, getAxisVelocities, RobotTelemetryClient, type RobotTelemetryData } from './telemetryClient';
import { createRobotUi } from './ui';
import {
  homeJointAngles,
  type JointAngles,
  type JointName,
  type RobotModelLoadResult,
  type RobotModelStatus,
  type RobotVisualModel,
  type TelemetryHeartbeat,
  type UiController,
  type VisualizationMode,
  type VisualizationOptions,
} from './types';

// The browser renders telemetry only. Robot simulation and program execution stay server-owned.

const sceneHost = requiredElement<HTMLDivElement>('scene');
const controlsHost = requiredElement<HTMLElement>('controls');
const statusOverlay = createStatusOverlay(sceneHost);
const presentationOverlay = createPresentationOverlay(sceneHost);

const scene = new THREE.Scene();
scene.background = new THREE.Color(0x070b10);
scene.fog = new THREE.Fog(0x070b10, 7.5, 13);

const camera = new THREE.PerspectiveCamera(45, sceneHost.clientWidth / sceneHost.clientHeight, 0.1, 100);
camera.position.set(4.9, 3.25, 5.15);

const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
renderer.setSize(sceneHost.clientWidth, sceneHost.clientHeight);
renderer.shadowMap.enabled = true;
renderer.shadowMap.type = THREE.PCFSoftShadowMap;
sceneHost.append(renderer.domElement);

const orbitControls = new OrbitControls(camera, renderer.domElement);
orbitControls.enableDamping = true;
orbitControls.target.set(0.82, 1.36, 0.02);
orbitControls.maxPolarAngle = Math.PI * 0.48;
orbitControls.minDistance = 2.2;
orbitControls.maxDistance = 9;
orbitControls.update();

const cameraChoreography = new CameraChoreography(camera, orbitControls);

const grid = new THREE.GridHelper(8.5, 34, 0x5fa6ff, 0x263341);
const gridMaterial = grid.material as THREE.Material;
gridMaterial.transparent = true;
gridMaterial.opacity = 0.3;
scene.add(grid);

const worldFrame = createWorldFrame();
scene.add(worldFrame);

const ambientLight = new THREE.HemisphereLight(0xe8f4ff, 0x111820, 1.5);
scene.add(ambientLight);

const keyLight = new THREE.DirectionalLight(0xffffff, 2.9);
keyLight.position.set(4.8, 6.2, 3.4);
keyLight.castShadow = true;
keyLight.shadow.mapSize.set(2048, 2048);
keyLight.shadow.camera.left = -4;
keyLight.shadow.camera.right = 4;
keyLight.shadow.camera.top = 4;
keyLight.shadow.camera.bottom = -4;
scene.add(keyLight);

const fillLight = new THREE.DirectionalLight(0x7fb4ff, 0.85);
fillLight.position.set(-3.5, 2.4, -3.7);
scene.add(fillLight);

const rimLight = new THREE.DirectionalLight(0x7cffd4, 0.68);
rimLight.position.set(-2.2, 4.1, 3.9);
scene.add(rimLight);

const floor = new THREE.Mesh(
  new THREE.PlaneGeometry(9, 9),
  new THREE.MeshStandardMaterial({ color: 0x0a1016, roughness: 0.82, metalness: 0.12 }),
);
floor.rotation.x = -Math.PI / 2;
floor.receiveShadow = true;
scene.add(floor);

let robot: RobotVisualModel = createPrimitiveRobotModel();
scene.add(robot.root);

const toolFrame = createToolFrame();
attachToolFrame('primitive');

const pathTrail = new PathTrail({ maxPoints: 1000, minDistance: 0.012 });
scene.add(pathTrail.line);

let jointAngles: JointAngles = { ...homeJointAngles };
let demoRunning = false;
let demoStartMs = 0;
let ui: UiController;
let liveTelemetryActive = false;
let lastTelemetryReceivedMs: number | null = null;
let telemetryAgeMs: number | null = null;
let heartbeat: TelemetryHeartbeat = 'disconnected';
let visualizationMode: Exclude<VisualizationMode, 'presentationDemo'> = 'manual';
let presentationActive = false;
let presentationStartedLocalDemo = false;
let robotModelStatus: RobotModelStatus = 'primitive';
let latestTelemetryDetails: {
  timestampUtc?: string;
  currentProgramName?: string | null;
  programExecutionState?: string;
  currentStepIndex?: number | null;
  isMoving?: boolean;
} = {};
let visualizationOptions: VisualizationOptions = {
  showWorldFrame: true,
  showToolFrame: true,
  showGrid: true,
  showPathTrail: true,
};
const toolWorldPosition = new THREE.Vector3();

const telemetryClient = new RobotTelemetryClient({
  onStatusChange(status, detail): void {
    ui.setConnectionStatus(status, detail);
    statusOverlay.setConnectionStatus(status, detail);

    if (status === 'connected') {
      liveTelemetryActive = true;
      stopDemo();
      setVisualizationMode('liveTelemetry');
      ui.setManualControlState(false, presentationActive ? 'presentationDemo' : 'liveTelemetry');
      return;
    }

    if (status === 'disconnected' || status === 'error') {
      liveTelemetryActive = false;
      lastTelemetryReceivedMs = null;
      updateTelemetryHealth(performance.now());

      if (presentationActive) {
        if (!demoRunning) {
          demoRunning = true;
          demoStartMs = performance.now();
          presentationStartedLocalDemo = true;
        }
        ui.setManualControlState(false, 'presentationDemo');
        setVisualizationMode('localDemo');
        return;
      }

      ui.setManualControlState(true, 'manual');
      setVisualizationMode(demoRunning ? 'localDemo' : 'manual');
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
    robot.setJointAngles(jointAngles);
    addToolPathPoint();
    ui.setAngles(jointAngles);
    setVisualizationMode('manual');
    ui.setManualControlState(true, 'manual');
  },
  onResetHome(): void {
    if (liveTelemetryActive) {
      return;
    }

    stopDemo();
    jointAngles = resetHome(robot);
    addToolPathPoint();
    ui.setAngles(jointAngles);
    ui.setVelocities({});
    setVisualizationMode('manual');
    ui.setManualControlState(true, 'manual');
  },
  onDemoStart(): void {
    if (liveTelemetryActive) {
      return;
    }

    stopPresentation();
    demoRunning = true;
    demoStartMs = performance.now();
    setVisualizationMode('localDemo');
    ui.setManualControlState(false, 'localDemo');
  },
  onDemoStop(): void {
    stopDemo();
  },
  onPresentationStart(): void {
    startPresentation();
  },
  onPresentationStop(): void {
    stopPresentation();
  },
  onTelemetryConnect(url: string): void {
    stopDemo();
    setVisualizationMode('liveTelemetry');
    ui.setManualControlState(false, 'liveTelemetry');
    telemetryClient.connect(url);
  },
  onTelemetryDisconnect(): void {
    telemetryClient.disconnect();
  },
  onClearPath(): void {
    pathTrail.clear();
  },
  onResetCamera(): void {
    cameraChoreography.resetCamera();
  },
  onModelReload(): void {
    void reloadRobotModel();
  },
  onVisualizationOptionChange(options: VisualizationOptions): void {
    applyVisualizationOptions(options);
  },
});

ui.setManualControlState(true, 'manual');
ui.setTelemetryHealth('disconnected', null);
ui.setModelStatus('primitive', 'Primitive fallback model');
statusOverlay.setTelemetryHealth('disconnected', null);
void reloadRobotModel();

window.addEventListener('resize', resizeRenderer);
window.addEventListener('keydown', handleKeyboardShortcut);
resizeRenderer();
renderer.setAnimationLoop(render);

function render(nowMs: number): void {
  if (demoRunning) {
    jointAngles = demoAngles((nowMs - demoStartMs) / 1000);
    robot.setJointAngles(jointAngles);
    ui.setAngles(jointAngles);
    addToolPathPoint();
  }

  if (liveTelemetryActive) {
    addToolPathPoint();
  }

  updateTelemetryHealth(nowMs);
  if (presentationActive) {
    cameraChoreography.update(nowMs);
    updatePresentationOverlay();
  } else {
    orbitControls.update();
  }

  renderer.render(scene, camera);
}

function stopDemo(): void {
  if (!demoRunning) {
    return;
  }

  demoRunning = false;

  if (!liveTelemetryActive && !presentationActive) {
    setVisualizationMode('manual');
    ui.setManualControlState(true, 'manual');
  }
}

function startPresentation(): void {
  if (presentationActive) {
    return;
  }

  pathTrail.clear();
  applyVisualizationOptions({
    showWorldFrame: true,
    showToolFrame: true,
    showGrid: true,
    showPathTrail: true,
  });
  ui.setVisualizationOptions(visualizationOptions);

  presentationActive = true;
  presentationStartedLocalDemo = false;

  if (liveTelemetryActive) {
    setVisualizationMode('liveTelemetry');
  } else {
    presentationStartedLocalDemo = !demoRunning;
    if (!demoRunning) {
      demoRunning = true;
      demoStartMs = performance.now();
    }
    setVisualizationMode('localDemo');
  }

  ui.setManualControlState(false, 'presentationDemo');
  ui.setPresentationActive(true);
  presentationOverlay.setActive(true);
  cameraChoreography.start(performance.now());
  updatePresentationOverlay();
}

function stopPresentation(): void {
  if (!presentationActive) {
    return;
  }

  presentationActive = false;
  cameraChoreography.stop();
  presentationOverlay.setActive(false);
  ui.setPresentationActive(false);

  if (presentationStartedLocalDemo && !liveTelemetryActive) {
    presentationStartedLocalDemo = false;
    stopDemo();
    return;
  }

  presentationStartedLocalDemo = false;

  if (liveTelemetryActive) {
    setVisualizationMode('liveTelemetry');
    ui.setManualControlState(false, 'liveTelemetry');
  } else if (demoRunning) {
    setVisualizationMode('localDemo');
    ui.setManualControlState(false, 'localDemo');
  } else {
    setVisualizationMode('manual');
    ui.setManualControlState(true, 'manual');
  }
}

function applyLiveTelemetry(message: RobotTelemetryData): void {
  lastTelemetryReceivedMs = performance.now();
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
    robot.setJointAngles(jointAngles);
    ui.setAngles(jointAngles);
    addToolPathPoint();
  }

  ui.setVelocities(getAxisVelocities(message));
  const telemetryDetails = {
    timestampUtc: message.timestampUtc,
    currentProgramName: message.currentProgramName,
    programExecutionState: message.programExecutionState,
    currentStepIndex: message.currentStepIndex,
    isMoving: message.isMoving,
  };
  latestTelemetryDetails = telemetryDetails;
  ui.setTelemetryDetails(telemetryDetails);
  statusOverlay.setTelemetryDetails(telemetryDetails);
}

function addToolPathPoint(): void {
  robot.getToolWorldPosition(toolWorldPosition);
  pathTrail.addPoint(toolWorldPosition);
}

let robotReloadToken = 0;

async function reloadRobotModel(): Promise<void> {
  const reloadToken = ++robotReloadToken;
  robotModelStatus = 'glbLoading';
  ui.setModelStatus('glbLoading', 'Loading GLB model...');

  const result = await loadRobotVisualModel();

  if (reloadToken !== robotReloadToken) {
    result.model.dispose?.();
    return;
  }

  replaceRobotModel(result);
}

function replaceRobotModel(result: RobotModelLoadResult): void {
  scene.remove(robot.root);
  toolFrame.removeFromParent();
  robot.dispose?.();

  robot = result.model;
  robot.setJointAngles(jointAngles);
  scene.add(robot.root);
  attachToolFrame(result.status);
  pathTrail.clear();
  addToolPathPoint();
  robotModelStatus = result.status;
  ui.setModelStatus(result.status, result.message);
}

function attachToolFrame(_modelStatus: RobotModelStatus): void {
  toolFrame.position.set(0, 0, 0);
  robot.toolObject.add(toolFrame);
}

function updateTelemetryHealth(nowMs: number): void {
  let nextHeartbeat: TelemetryHeartbeat;
  let ageMs: number | null = null;

  if (!liveTelemetryActive || !telemetryClient.isConnected) {
    nextHeartbeat = 'disconnected';
  } else if (lastTelemetryReceivedMs === null) {
    nextHeartbeat = 'stale';
  } else {
    ageMs = nowMs - lastTelemetryReceivedMs;
    nextHeartbeat = ageMs <= 1000 ? 'live' : 'stale';
  }

  telemetryAgeMs = ageMs;

  if (nextHeartbeat !== heartbeat || ageMs !== null) {
    heartbeat = nextHeartbeat;
    ui.setTelemetryHealth(nextHeartbeat, ageMs);
    statusOverlay.setTelemetryHealth(nextHeartbeat, ageMs);
  }
}

function setVisualizationMode(mode: Exclude<VisualizationMode, 'presentationDemo'>): void {
  visualizationMode = mode;
  const displayedMode: VisualizationMode = presentationActive ? 'presentationDemo' : mode;
  ui.setMode(displayedMode);
  statusOverlay.setMode(displayedMode);
}

function applyVisualizationOptions(options: VisualizationOptions): void {
  visualizationOptions = { ...options };
  worldFrame.visible = visualizationOptions.showWorldFrame;
  toolFrame.visible = visualizationOptions.showToolFrame;
  grid.visible = visualizationOptions.showGrid;
  pathTrail.setVisible(visualizationOptions.showPathTrail);
}

function updatePresentationOverlay(): void {
  presentationOverlay.setSnapshot({
    modelStatus: robotModelStatus,
    motionSource: getPresentationMotionSource(),
    programExecutionState: latestTelemetryDetails.programExecutionState,
    currentStepIndex: latestTelemetryDetails.currentStepIndex,
    heartbeat,
    telemetryAgeMs,
  });
}

function getPresentationMotionSource(): PresentationMotionSource {
  return liveTelemetryActive && visualizationMode === 'liveTelemetry' ? 'liveTelemetry' : 'localDemo';
}

function handleKeyboardShortcut(event: KeyboardEvent): void {
  const target = event.target;
  if (target instanceof HTMLInputElement || target instanceof HTMLTextAreaElement || target instanceof HTMLSelectElement) {
    return;
  }

  if (event.key === 'd' || event.key === 'D') {
    if (presentationActive) {
      stopPresentation();
    } else {
      startPresentation();
    }
  } else if (event.key === 'c' || event.key === 'C') {
    pathTrail.clear();
  } else if (event.key === 'r' || event.key === 'R') {
    cameraChoreography.resetCamera();
  }
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
