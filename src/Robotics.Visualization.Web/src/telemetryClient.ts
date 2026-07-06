import { jointNames, type JointAngles, type JointName, type TelemetryConnectionStatus } from './types';

export const defaultTelemetryUrl = 'ws://localhost:48080/telemetry';

export interface AxisTelemetryData {
  positionDeg?: number;
  velocityDegPerSec?: number;
}

export interface RobotTelemetryData {
  timestampUtc?: string;
  axes?: Partial<Record<JointName, AxisTelemetryData>>;
  currentProgramName?: string | null;
  programExecutionState?: string;
  currentStepIndex?: number | null;
  totalStepCount?: number;
  currentStepType?: string | null;
  currentStepName?: string | null;
  activeTargetJointAngles?: Partial<JointAngles> | null;
  nextTargetJointAngles?: Partial<JointAngles> | null;
  queuedTargetJointAngles?: Partial<JointAngles>[];
  isMoving?: boolean;
}

interface TelemetryClientOptions {
  onStatusChange: (status: TelemetryConnectionStatus, detail?: string) => void;
  onTelemetry: (message: RobotTelemetryData) => void;
}

export class RobotTelemetryClient {
  private readonly options: TelemetryClientOptions;
  private socket: WebSocket | null = null;

  public constructor(options: TelemetryClientOptions) {
    this.options = options;
  }

  public get isConnected(): boolean {
    return this.socket?.readyState === WebSocket.OPEN;
  }

  public connect(url: string): void {
    this.disconnect();

    const trimmedUrl = url.trim();
    if (!trimmedUrl) {
      this.options.onStatusChange('error', 'Missing WebSocket URL');
      return;
    }

    this.options.onStatusChange('connecting');

    try {
      this.socket = new WebSocket(trimmedUrl);
    } catch (error) {
      this.socket = null;
      this.options.onStatusChange('error', getErrorMessage(error));
      return;
    }

    const activeSocket = this.socket;

    activeSocket.addEventListener('open', () => {
      if (this.socket === activeSocket) {
        this.options.onStatusChange('connected');
      }
    });

    activeSocket.addEventListener('message', (event: MessageEvent<string>) => {
      const message = parseTelemetryMessage(event.data);
      if (message) {
        this.options.onTelemetry(message);
      }
    });

    activeSocket.addEventListener('error', () => {
      if (this.socket === activeSocket) {
        this.options.onStatusChange('error', 'WebSocket error');
      }
    });

    activeSocket.addEventListener('close', () => {
      if (this.socket === activeSocket) {
        this.socket = null;
        this.options.onStatusChange('disconnected');
      }
    });
  }

  public disconnect(): void {
    if (!this.socket) {
      return;
    }

    const socket = this.socket;
    this.socket = null;

    if (socket.readyState === WebSocket.OPEN || socket.readyState === WebSocket.CONNECTING) {
      socket.close();
    }

    this.options.onStatusChange('disconnected');
  }
}

export function getAxisPositions(message: RobotTelemetryData): Partial<Record<JointName, number>> {
  const positions: Partial<Record<JointName, number>> = {};

  for (const jointName of jointNames) {
    const position = message.axes?.[jointName]?.positionDeg;
    if (isFiniteNumber(position)) {
      positions[jointName] = position;
    }
  }

  return positions;
}

export function getAxisVelocities(message: RobotTelemetryData): Partial<Record<JointName, number>> {
  const velocities: Partial<Record<JointName, number>> = {};

  for (const jointName of jointNames) {
    const velocity = message.axes?.[jointName]?.velocityDegPerSec;
    if (isFiniteNumber(velocity)) {
      velocities[jointName] = velocity;
    }
  }

  return velocities;
}

function parseTelemetryMessage(rawData: string): RobotTelemetryData | null {
  try {
    const parsed: unknown = JSON.parse(rawData);
    if (!isObject(parsed)) {
      return null;
    }

    const message = parsed as RobotTelemetryData;
    const axes = isObject(message.axes) ? message.axes : undefined;

    return {
      timestampUtc: typeof message.timestampUtc === 'string' ? message.timestampUtc : undefined,
      axes: parseAxes(axes),
      currentProgramName:
        typeof message.currentProgramName === 'string' || message.currentProgramName === null
          ? message.currentProgramName
          : undefined,
      programExecutionState:
        typeof message.programExecutionState === 'string' ? message.programExecutionState : undefined,
      currentStepIndex:
        typeof message.currentStepIndex === 'number' && Number.isFinite(message.currentStepIndex)
          ? message.currentStepIndex
          : message.currentStepIndex === null
            ? null
            : undefined,
      totalStepCount:
        typeof message.totalStepCount === 'number' && Number.isFinite(message.totalStepCount)
          ? message.totalStepCount
          : undefined,
      currentStepType:
        typeof message.currentStepType === 'string' || message.currentStepType === null
          ? message.currentStepType
          : undefined,
      currentStepName:
        typeof message.currentStepName === 'string' || message.currentStepName === null
          ? message.currentStepName
          : undefined,
      activeTargetJointAngles: parseNullableJointAngles(message.activeTargetJointAngles),
      nextTargetJointAngles: parseNullableJointAngles(message.nextTargetJointAngles),
      queuedTargetJointAngles: parseQueuedTargetJointAngles(message.queuedTargetJointAngles),
      isMoving: typeof message.isMoving === 'boolean' ? message.isMoving : undefined,
    };
  } catch {
    return null;
  }
}

function parseAxes(rawAxes: object | undefined): RobotTelemetryData['axes'] {
  if (!rawAxes) {
    return undefined;
  }

  const axes: Partial<Record<JointName, AxisTelemetryData>> = {};
  const axisRecord = rawAxes as Record<string, unknown>;

  for (const jointName of jointNames) {
    const rawAxis = axisRecord[jointName];
    if (!isObject(rawAxis)) {
      continue;
    }

    const axis = rawAxis as AxisTelemetryData;
    axes[jointName] = {
      positionDeg: isFiniteNumber(axis.positionDeg) ? axis.positionDeg : undefined,
      velocityDegPerSec: isFiniteNumber(axis.velocityDegPerSec) ? axis.velocityDegPerSec : undefined,
    };
  }

  return axes;
}

function parseNullableJointAngles(value: unknown): Partial<JointAngles> | null | undefined {
  if (value === null) {
    return null;
  }

  if (!isObject(value)) {
    return undefined;
  }

  const angles = parseJointAngles(value);
  return Object.keys(angles).length > 0 ? angles : undefined;
}

function parseQueuedTargetJointAngles(value: unknown): Partial<JointAngles>[] | undefined {
  if (!Array.isArray(value)) {
    return undefined;
  }

  const targets = value
    .filter(isObject)
    .map(parseJointAngles)
    .filter((angles) => Object.keys(angles).length > 0);

  return targets.length > 0 ? targets : undefined;
}

function parseJointAngles(rawAngles: Record<string, unknown>): Partial<JointAngles> {
  const angles: Partial<JointAngles> = {};

  for (const jointName of jointNames) {
    const angle = rawAngles[jointName];
    if (isFiniteNumber(angle)) {
      angles[jointName] = angle;
    }
  }

  return angles;
}

function isObject(value: unknown): value is Record<string, unknown> {
  return typeof value === 'object' && value !== null;
}

function isFiniteNumber(value: unknown): value is number {
  return typeof value === 'number' && Number.isFinite(value);
}

function getErrorMessage(error: unknown): string {
  return error instanceof Error ? error.message : 'Unable to open WebSocket';
}
