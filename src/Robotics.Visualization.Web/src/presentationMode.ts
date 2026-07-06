import type { RobotModelStatus, TelemetryHeartbeat } from './types';

export type PresentationMotionSource = 'liveTelemetry' | 'localDemo';

export interface PresentationSnapshot {
  modelStatus: RobotModelStatus;
  motionSource: PresentationMotionSource;
  programExecutionState?: string;
  currentStepIndex?: number | null;
  heartbeat: TelemetryHeartbeat;
  telemetryAgeMs: number | null;
}

export interface PresentationOverlay {
  setActive: (isActive: boolean) => void;
  setSnapshot: (snapshot: PresentationSnapshot) => void;
}

export function createPresentationOverlay(host: HTMLElement): PresentationOverlay {
  const overlay = document.createElement('aside');
  overlay.className = 'presentation-overlay';
  overlay.setAttribute('aria-label', 'Presentation demo status');
  overlay.hidden = true;
  overlay.innerHTML = `
    <div class="presentation-title">
      <p>Presentation Demo Mode</p>
      <h2>OPC UA Robotics Reference Implementation</h2>
    </div>
    <dl class="presentation-facts">
      <div><dt>Model</dt><dd data-presentation-model>Primitive fallback model</dd></div>
      <div><dt>Motion Source</dt><dd data-presentation-motion>Local Demo</dd></div>
      <div><dt>Program State</dt><dd data-presentation-state>-</dd></div>
      <div><dt>Step</dt><dd data-presentation-step>-</dd></div>
      <div><dt>Heartbeat</dt><dd data-presentation-heartbeat>Disconnected</dd></div>
      <div><dt>Freshness</dt><dd data-presentation-age>-</dd></div>
    </dl>
  `;
  host.append(overlay);

  const model = requiredElement(overlay, '[data-presentation-model]');
  const motion = requiredElement(overlay, '[data-presentation-motion]');
  const state = requiredElement(overlay, '[data-presentation-state]');
  const step = requiredElement(overlay, '[data-presentation-step]');
  const heartbeat = requiredElement(overlay, '[data-presentation-heartbeat]');
  const age = requiredElement(overlay, '[data-presentation-age]');

  return {
    setActive(isActive: boolean): void {
      overlay.hidden = !isActive;
    },
    setSnapshot(snapshot: PresentationSnapshot): void {
      model.textContent = modelStatusLabels[snapshot.modelStatus];
      motion.textContent = motionSourceLabels[snapshot.motionSource];
      state.textContent = snapshot.programExecutionState || '-';
      step.textContent = snapshot.currentStepIndex === null || snapshot.currentStepIndex === undefined
        ? '-'
        : snapshot.currentStepIndex.toString();
      heartbeat.textContent = heartbeatLabels[snapshot.heartbeat];
      heartbeat.dataset.heartbeat = snapshot.heartbeat;
      age.textContent = snapshot.telemetryAgeMs === null
        ? '-'
        : `${Math.max(0, Math.round(snapshot.telemetryAgeMs))} ms`;
    },
  };
}

const modelStatusLabels: Record<RobotModelStatus, string> = {
  primitive: 'Procedural fallback',
  glbLoaded: 'GLB model',
  glbFailed: 'Procedural fallback',
  glbLoading: 'Loading GLB model',
};

const motionSourceLabels: Record<PresentationMotionSource, string> = {
  liveTelemetry: 'Live Telemetry',
  localDemo: 'Local Demo',
};

const heartbeatLabels: Record<TelemetryHeartbeat, string> = {
  live: 'Live',
  stale: 'Stale',
  disconnected: 'Disconnected',
};

function requiredElement(container: HTMLElement, selector: string): HTMLElement {
  const element = container.querySelector<HTMLElement>(selector);
  if (!element) {
    throw new Error(`Missing presentation overlay element: ${selector}`);
  }

  return element;
}
