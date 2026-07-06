import {
  jointLimits,
  jointNames,
  type JointAngles,
  type JointName,
  type JointVelocities,
  type TelemetryConnectionStatus,
  type UiController,
  type VisualizationMode,
} from './types';
import { defaultTelemetryUrl } from './telemetryClient';

interface UiOptions {
  container: HTMLElement;
  initialAngles: JointAngles;
  onJointChange: (jointName: JointName, value: number) => void;
  onResetHome: () => void;
  onDemoStart: () => void;
  onDemoStop: () => void;
  onTelemetryConnect: (url: string) => void;
  onTelemetryDisconnect: () => void;
}

export function createRobotUi(options: UiOptions): UiController {
  const sliderInputs = new Map<JointName, HTMLInputElement>();
  const valueOutputs = new Map<JointName, HTMLOutputElement>();
  const velocityOutputs = new Map<JointName, HTMLElement>();

  options.container.innerHTML = '';

  const header = document.createElement('header');
  header.className = 'panel-header';
  header.innerHTML = `
    <div>
      <p class="eyebrow">OPC UA Robotics</p>
      <h1>Visualization V2</h1>
    </div>
    <span class="status-pill" data-mode-status>Manual Mode</span>
  `;
  options.container.append(header);

  const connectionSection = document.createElement('section');
  connectionSection.className = 'connection-panel';
  connectionSection.setAttribute('aria-label', 'Live telemetry connection');

  const urlLabel = document.createElement('label');
  urlLabel.className = 'url-field';

  const urlLabelText = document.createElement('span');
  urlLabelText.textContent = 'WebSocket URL';

  const urlInput = document.createElement('input');
  urlInput.type = 'url';
  urlInput.value = defaultTelemetryUrl;
  urlInput.spellcheck = false;

  urlLabel.append(urlLabelText, urlInput);

  const connectionActions = document.createElement('div');
  connectionActions.className = 'button-row';

  const connectButton = button('Connect', 'primary');
  const disconnectButton = button('Disconnect', 'secondary');
  disconnectButton.disabled = true;

  connectButton.addEventListener('click', () => options.onTelemetryConnect(urlInput.value));
  disconnectButton.addEventListener('click', options.onTelemetryDisconnect);

  connectionActions.append(connectButton, disconnectButton);

  const connectionStatus = document.createElement('p');
  connectionStatus.className = 'connection-status';
  connectionStatus.dataset.connectionStatus = 'true';
  connectionStatus.textContent = 'disconnected';

  connectionSection.append(urlLabel, connectionActions, connectionStatus);
  options.container.append(connectionSection);

  const actions = document.createElement('div');
  actions.className = 'action-row';

  const resetButton = button('Reset Home', 'secondary');
  const demoButton = button('Local Demo', 'primary');
  const stopButton = button('Stop Demo', 'secondary');

  resetButton.addEventListener('click', options.onResetHome);
  demoButton.addEventListener('click', options.onDemoStart);
  stopButton.addEventListener('click', options.onDemoStop);

  actions.append(resetButton, demoButton, stopButton);
  options.container.append(actions);

  const sliderSection = document.createElement('section');
  sliderSection.className = 'control-section';
  sliderSection.setAttribute('aria-label', 'Manual joint sliders');

  const manualStatus = document.createElement('p');
  manualStatus.className = 'manual-status';
  manualStatus.dataset.manualStatus = 'true';
  manualStatus.textContent = 'Manual joint controls';
  sliderSection.append(manualStatus);

  for (const jointName of jointNames) {
    const row = document.createElement('label');
    row.className = 'joint-row';
    row.htmlFor = `joint-${jointName}`;

    const name = document.createElement('span');
    name.className = 'joint-name';
    name.textContent = jointName;

    const slider = document.createElement('input');
    slider.id = `joint-${jointName}`;
    slider.type = 'range';
    slider.min = jointLimits[jointName].min.toString();
    slider.max = jointLimits[jointName].max.toString();
    slider.step = '1';
    slider.value = options.initialAngles[jointName].toString();

    const value = document.createElement('output');
    value.className = 'joint-value';
    value.setAttribute('for', slider.id);
    value.textContent = formatDegrees(options.initialAngles[jointName]);

    slider.addEventListener('input', () => {
      const angle = Number(slider.value);
      value.textContent = formatDegrees(angle);
      options.onJointChange(jointName, angle);
    });

    row.append(name, slider, value);
    sliderSection.append(row);
    sliderInputs.set(jointName, slider);
    valueOutputs.set(jointName, value);
  }

  options.container.append(sliderSection);

  const telemetry = document.createElement('section');
  telemetry.className = 'telemetry-panel';
  telemetry.setAttribute('aria-label', 'Current telemetry values');
  telemetry.innerHTML = '<h2>Telemetry</h2>';

  const telemetryGrid = document.createElement('dl');
  telemetryGrid.className = 'telemetry-grid';

  for (const jointName of jointNames) {
    const cell = document.createElement('div');
    cell.className = 'telemetry-cell';

    const term = document.createElement('dt');
    term.textContent = jointName;

    const detail = document.createElement('dd');

    const position = document.createElement('span');
    position.dataset.telemetryJoint = jointName;
    position.textContent = formatDegrees(options.initialAngles[jointName]);

    const velocity = document.createElement('small');
    velocity.dataset.telemetryVelocity = jointName;
    velocity.textContent = formatVelocity(0);

    detail.append(position, velocity);
    cell.append(term, detail);
    telemetryGrid.append(cell);
    velocityOutputs.set(jointName, velocity);
  }

  telemetry.append(telemetryGrid);

  const programGrid = document.createElement('dl');
  programGrid.className = 'program-grid';
  programGrid.innerHTML = `
    <div><dt>Program</dt><dd data-program-name>-</dd></div>
    <div><dt>State</dt><dd data-program-state>-</dd></div>
    <div><dt>Step</dt><dd data-program-step>-</dd></div>
    <div><dt>Moving</dt><dd data-moving-state>-</dd></div>
    <div class="wide"><dt>Timestamp</dt><dd data-timestamp>-</dd></div>
  `;
  telemetry.append(programGrid);
  options.container.append(telemetry);

  const modeStatus = options.container.querySelector<HTMLElement>('[data-mode-status]');
  const programName = options.container.querySelector<HTMLElement>('[data-program-name]');
  const programState = options.container.querySelector<HTMLElement>('[data-program-state]');
  const programStep = options.container.querySelector<HTMLElement>('[data-program-step]');
  const movingState = options.container.querySelector<HTMLElement>('[data-moving-state]');
  const timestamp = options.container.querySelector<HTMLElement>('[data-timestamp]');

  return {
    setAngles(angles: JointAngles): void {
      for (const jointName of jointNames) {
        const angle = angles[jointName];
        const slider = sliderInputs.get(jointName);
        const value = valueOutputs.get(jointName);
        const telemetryValue = telemetry.querySelector<HTMLElement>(`[data-telemetry-joint="${jointName}"]`);

        if (slider) {
          slider.value = angle.toFixed(0);
        }

        if (value) {
          value.textContent = formatDegrees(angle);
        }

        if (telemetryValue) {
          telemetryValue.textContent = formatDegrees(angle);
        }
      }
    },
    setVelocities(velocities: JointVelocities): void {
      for (const jointName of jointNames) {
        const velocity = velocityOutputs.get(jointName);
        if (velocity) {
          velocity.textContent = formatVelocity(velocities[jointName] ?? 0);
        }
      }
    },
    setMode(mode: VisualizationMode): void {
      if (!modeStatus) {
        return;
      }

      modeStatus.textContent = modeLabels[mode];
      modeStatus.classList.toggle('is-running', mode === 'localDemo');
      modeStatus.classList.toggle('is-live', mode === 'liveTelemetry');
    },
    setConnectionStatus(status: TelemetryConnectionStatus, detail?: string): void {
      connectionStatus.textContent = detail ? `${status}: ${detail}` : status;
      connectionStatus.dataset.status = status;
      connectButton.disabled = status === 'connecting' || status === 'connected';
      disconnectButton.disabled = status !== 'connecting' && status !== 'connected';
      urlInput.disabled = status === 'connecting' || status === 'connected';
    },
    setManualControlsEnabled(isEnabled: boolean): void {
      for (const slider of sliderInputs.values()) {
        slider.disabled = !isEnabled;
      }

      resetButton.disabled = !isEnabled;
      demoButton.disabled = !isEnabled;
      stopButton.disabled = !isEnabled;
      manualStatus.textContent = isEnabled
        ? 'Manual joint controls'
        : 'Manual controls overridden by live telemetry';
      sliderSection.classList.toggle('is-disabled', !isEnabled);
    },
    setTelemetryDetails(details): void {
      if (programName) {
        programName.textContent = details.currentProgramName || '-';
      }

      if (programState) {
        programState.textContent = details.programExecutionState || '-';
      }

      if (programStep) {
        programStep.textContent = details.currentStepIndex === null || details.currentStepIndex === undefined
          ? '-'
          : details.currentStepIndex.toString();
      }

      if (movingState) {
        movingState.textContent = details.isMoving === undefined ? '-' : details.isMoving ? 'true' : 'false';
      }

      if (timestamp) {
        timestamp.textContent = details.timestampUtc || '-';
      }
    },
  };
}

const modeLabels: Record<VisualizationMode, string> = {
  manual: 'Manual Mode',
  localDemo: 'Local Demo Mode',
  liveTelemetry: 'Live Telemetry Mode',
};

function button(label: string, variant: 'primary' | 'secondary'): HTMLButtonElement {
  const element = document.createElement('button');
  element.type = 'button';
  element.className = `button ${variant}`;
  element.textContent = label;
  return element;
}

function formatDegrees(value: number): string {
  const rounded = Math.abs(value) < 0.05 ? 0 : value;
  return `${rounded.toFixed(0)} deg`;
}

function formatVelocity(value: number): string {
  const rounded = Math.abs(value) < 0.005 ? 0 : value;
  return `${rounded.toFixed(1)} deg/s`;
}
