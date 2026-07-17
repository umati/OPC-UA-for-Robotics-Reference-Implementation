import type { TelemetryConnectionStatus, TelemetryHeartbeat, VisualizationMode } from './types';

interface TelemetryDetails {
  currentProgramName?: string | null;
  programExecutionState?: string;
  currentStepIndex?: number | null;
  isMoving?: boolean;
}

export interface StatusOverlay {
  setMode: (mode: VisualizationMode) => void;
  setConnectionStatus: (status: TelemetryConnectionStatus, detail?: string) => void;
  setTelemetryHealth: (heartbeat: TelemetryHeartbeat, ageMs: number | null) => void;
  setTelemetryDetails: (details: TelemetryDetails) => void;
}

export function createStatusOverlay(host: HTMLElement): StatusOverlay {
  const overlay = document.createElement('aside');
  overlay.className = 'scene-status-overlay';
  overlay.innerHTML = `
    <div class="overlay-row overlay-primary">
      <span data-overlay-mode>Manual Mode</span>
      <span data-overlay-heartbeat="disconnected">Disconnected</span>
    </div>
    <dl>
      <div><dt>Connection</dt><dd data-overlay-connection>disconnected</dd></div>
      <div><dt>Age</dt><dd data-overlay-age>-</dd></div>
      <div><dt>Program</dt><dd data-overlay-program>-</dd></div>
      <div><dt>State</dt><dd data-overlay-state>-</dd></div>
      <div><dt>Step</dt><dd data-overlay-step>-</dd></div>
      <div><dt>Moving</dt><dd data-overlay-moving>-</dd></div>
    </dl>
  `;
  host.append(overlay);

  const mode = requiredOverlayElement(overlay, '[data-overlay-mode]');
  const heartbeatOutput = requiredOverlayElement(overlay, '[data-overlay-heartbeat]');
  const connection = requiredOverlayElement(overlay, '[data-overlay-connection]');
  const age = requiredOverlayElement(overlay, '[data-overlay-age]');
  const program = requiredOverlayElement(overlay, '[data-overlay-program]');
  const state = requiredOverlayElement(overlay, '[data-overlay-state]');
  const step = requiredOverlayElement(overlay, '[data-overlay-step]');
  const moving = requiredOverlayElement(overlay, '[data-overlay-moving]');

  return {
    setMode(nextMode: VisualizationMode): void {
      mode.textContent = modeLabels[nextMode];
    },
    setConnectionStatus(status: TelemetryConnectionStatus, detail?: string): void {
      connection.textContent = detail ? `${status}: ${detail}` : status;
      connection.dataset.status = status;
    },
    setTelemetryHealth(heartbeat: TelemetryHeartbeat, ageMs: number | null): void {
      heartbeatOutput.textContent = heartbeatLabels[heartbeat];
      heartbeatOutput.dataset.overlayHeartbeat = heartbeat;
      age.textContent = ageMs === null ? '-' : `${Math.max(0, Math.round(ageMs))} ms`;
    },
    setTelemetryDetails(details: TelemetryDetails): void {
      program.textContent = details.currentProgramName || '-';
      state.textContent = details.programExecutionState || '-';
      step.textContent = details.currentStepIndex === null || details.currentStepIndex === undefined
        ? '-'
        : details.currentStepIndex.toString();
      moving.textContent = details.isMoving === undefined ? '-' : details.isMoving ? 'true' : 'false';
    },
  };
}

const modeLabels: Record<VisualizationMode, string> = {
  manual: 'Manual Mode',
  localDemo: 'Local Demo Mode',
  liveTelemetry: 'Live Telemetry Mode',
  presentationDemo: 'Presentation Demo Mode',
};

const heartbeatLabels: Record<TelemetryHeartbeat, string> = {
  live: 'Live',
  stale: 'Stale',
  disconnected: 'Disconnected',
};

function requiredOverlayElement(container: HTMLElement, selector: string): HTMLElement {
  const element = container.querySelector<HTMLElement>(selector);
  if (!element) {
    throw new Error(`Missing status overlay element: ${selector}`);
  }

  return element;
}
