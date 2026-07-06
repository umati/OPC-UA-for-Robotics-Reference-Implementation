import type * as THREE from 'three';

export const jointNames = ['S', 'L', 'U', 'R', 'B', 'T'] as const;

export type JointName = (typeof jointNames)[number];

export type JointAngles = Record<JointName, number>;

export type JointVelocities = Partial<Record<JointName, number>>;

export type VisualizationMode = 'manual' | 'localDemo' | 'liveTelemetry' | 'presentationDemo';

export type TelemetryConnectionStatus = 'disconnected' | 'connecting' | 'connected' | 'error';

export interface VisualizationOptions {
  showWorldFrame: boolean;
  showToolFrame: boolean;
  showGrid: boolean;
  showPathTrail: boolean;
}

export type ManualControlReason = 'manual' | 'localDemo' | 'liveTelemetry' | 'presentationDemo';

export type TelemetryHeartbeat = 'live' | 'stale' | 'disconnected';

export type RobotModelStatus = 'primitive' | 'glbLoaded' | 'glbFailed' | 'glbLoading';

export interface JointLimit {
  min: number;
  max: number;
}

export const jointLimits: Record<JointName, JointLimit> = {
  S: { min: -170, max: 170 },
  L: { min: -100, max: 100 },
  U: { min: -120, max: 120 },
  R: { min: -180, max: 180 },
  B: { min: -120, max: 120 },
  T: { min: -360, max: 360 },
};

export const homeJointAngles: JointAngles = {
  S: 0,
  L: 0,
  U: 0,
  R: 0,
  B: 0,
  T: 0,
};

export interface RobotVisualModel {
  root: THREE.Object3D;
  toolObject: THREE.Object3D;
  setJointAngles: (angles: Partial<JointAngles>) => void;
  getToolWorldPosition: (targetVector: THREE.Vector3) => THREE.Vector3;
  dispose?: () => void;
}

export interface PrimitiveRobotModel extends RobotVisualModel {
  root: THREE.Group;
  joints: Record<JointName, THREE.Group>;
  toolGroup: THREE.Group;
  toolObject: THREE.Group;
}

export interface RobotModelLoadResult {
  model: RobotVisualModel;
  status: RobotModelStatus;
  message: string;
}

export interface UiController {
  setAngles: (angles: JointAngles) => void;
  setVelocities: (velocities: JointVelocities) => void;
  setMode: (mode: VisualizationMode) => void;
  setConnectionStatus: (status: TelemetryConnectionStatus, detail?: string) => void;
  setManualControlState: (isEnabled: boolean, reason: ManualControlReason) => void;
  setTelemetryHealth: (heartbeat: TelemetryHeartbeat, ageMs: number | null) => void;
  setTelemetryDetails: (details: {
    timestampUtc?: string;
    currentProgramName?: string | null;
    programExecutionState?: string;
    currentStepIndex?: number | null;
    isMoving?: boolean;
  }) => void;
  setModelStatus: (status: RobotModelStatus, message: string) => void;
  setPresentationActive: (isActive: boolean) => void;
  setVisualizationOptions: (options: VisualizationOptions) => void;
}
