import type * as THREE from 'three';

export const jointNames = ['S', 'L', 'U', 'R', 'B', 'T'] as const;

export type JointName = (typeof jointNames)[number];

export type JointAngles = Record<JointName, number>;

export type JointVelocities = Partial<Record<JointName, number>>;

export type VisualizationMode = 'manual' | 'localDemo' | 'liveTelemetry';

export type TelemetryConnectionStatus = 'disconnected' | 'connecting' | 'connected' | 'error';

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

export interface RobotModel {
  root: THREE.Group;
  joints: Record<JointName, THREE.Group>;
  toolGroup: THREE.Group;
}

export interface UiController {
  setAngles: (angles: JointAngles) => void;
  setVelocities: (velocities: JointVelocities) => void;
  setMode: (mode: VisualizationMode) => void;
  setConnectionStatus: (status: TelemetryConnectionStatus, detail?: string) => void;
  setManualControlsEnabled: (isEnabled: boolean) => void;
  setTelemetryDetails: (details: {
    timestampUtc?: string;
    currentProgramName?: string | null;
    programExecutionState?: string;
    currentStepIndex?: number | null;
    isMoving?: boolean;
  }) => void;
}
