import {
  jointLimits,
  jointNames,
  type JointAngles,
  type JointName,
  type JointVelocities,
  type ManualControlReason,
  type ProgramTelemetryDetails,
  type RobotModelStatus,
  type TelemetryHeartbeat,
  type TelemetryConnectionStatus,
  type UiController,
  type VisualizationOptions,
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
  onPresentationStart: () => void;
  onPresentationStop: () => void;
  onTelemetryConnect: (url: string) => void;
  onTelemetryDisconnect: () => void;
  onClearPath: () => void;
  onResetCamera: () => void;
  onModelReload: () => void;
  onVisualizationOptionChange: (options: VisualizationOptions) => void;
}

export function createRobotUi(options: UiOptions): UiController {
  const sliderInputs = new Map<JointName, HTMLInputElement>();
  const valueOutputs = new Map<JointName, HTMLOutputElement>();
  const velocityOutputs = new Map<JointName, HTMLElement>();
  const optionInputs = new Map<keyof VisualizationOptions, HTMLInputElement>();
  const visualizationOptions: VisualizationOptions = {
    showWorldFrame: true,
    showToolFrame: true,
    showGrid: true,
    showPathTrail: true,
    showTargetMarkers: true,
    showGhostPose: true,
    showFuturePreview: true,
  };
  let currentMode: VisualizationMode = 'manual';
  let currentProgramDetails: ProgramTelemetryDetails = {};

  options.container.innerHTML = '';

  const header = document.createElement('header');
  header.className = 'panel-header';
  header.innerHTML = `
    <div>
      <p class="eyebrow">OPC UA Robotics</p>
      <h1>Visualization V6</h1>
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

  const presentationActions = document.createElement('div');
  presentationActions.className = 'presentation-actions';

  const startPresentationButton = button('Start Jaw-Drop Demo', 'primary');
  const stopPresentationButton = button('Stop Demo Presentation', 'secondary');
  stopPresentationButton.disabled = true;

  startPresentationButton.addEventListener('click', options.onPresentationStart);
  stopPresentationButton.addEventListener('click', options.onPresentationStop);

  presentationActions.append(startPresentationButton, stopPresentationButton);
  options.container.append(presentationActions);

  const modelSection = document.createElement('section');
  modelSection.className = 'model-panel';
  modelSection.setAttribute('aria-label', 'Robot model status');
  modelSection.innerHTML = `
    <h2>Model</h2>
    <p class="model-status" data-model-status="primitive">Primitive fallback model</p>
  `;

  const reloadModelButton = button('Reload Model', 'secondary');
  reloadModelButton.addEventListener('click', options.onModelReload);
  modelSection.append(reloadModelButton);
  options.container.append(modelSection);

  const viewSection = document.createElement('section');
  viewSection.className = 'view-section';
  viewSection.setAttribute('aria-label', 'Visualization display options');
  viewSection.innerHTML = '<h2>View</h2>';

  const toggles = document.createElement('div');
  toggles.className = 'toggle-grid';
  addVisualizationToggle('showWorldFrame', 'World frame');
  addVisualizationToggle('showToolFrame', 'Tool frame');
  addVisualizationToggle('showGrid', 'Grid');
  addVisualizationToggle('showPathTrail', 'Path trail');
  addVisualizationToggle('showTargetMarkers', 'Target markers');
  addVisualizationToggle('showGhostPose', 'Ghost pose');
  addVisualizationToggle('showFuturePreview', 'Future preview');

  const clearPathButton = button('Clear Path', 'secondary');
  const resetCameraButton = button('Reset Camera', 'secondary');
  clearPathButton.addEventListener('click', options.onClearPath);
  resetCameraButton.addEventListener('click', options.onResetCamera);

  const viewActions = document.createElement('div');
  viewActions.className = 'button-row';
  viewActions.append(clearPathButton, resetCameraButton);

  viewSection.append(toggles, viewActions);
  options.container.append(viewSection);

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
  telemetry.setAttribute('aria-label', 'Current telemetry status and values');
  telemetry.innerHTML = '<h2>Status</h2>';

  const statusGrid = document.createElement('dl');
  statusGrid.className = 'status-grid';
  statusGrid.innerHTML = `
    <div><dt>Mode</dt><dd data-status-mode>Manual Mode</dd></div>
    <div><dt>Connection</dt><dd data-status-connection>disconnected</dd></div>
    <div><dt>Heartbeat</dt><dd data-status-heartbeat>Disconnected</dd></div>
    <div><dt>Telemetry age</dt><dd data-status-age>-</dd></div>
  `;
  telemetry.append(statusGrid);

  const telemetryHeading = document.createElement('h2');
  telemetryHeading.textContent = 'Joints';
  telemetry.append(telemetryHeading);

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

  const programPreview = document.createElement('section');
  programPreview.className = 'program-preview-panel';
  programPreview.setAttribute('aria-label', 'Program target preview');
  programPreview.innerHTML = `
    <h2>Program Preview</h2>
    <p class="program-preview-message" data-program-preview-message>No program target data available</p>
    <dl class="program-grid">
      <div><dt>Program</dt><dd data-preview-program>-</dd></div>
      <div><dt>State</dt><dd data-preview-state>-</dd></div>
      <div><dt>Step</dt><dd data-preview-step>-</dd></div>
      <div><dt>Step Type</dt><dd data-preview-step-type>-</dd></div>
      <div class="wide"><dt>Step Name</dt><dd data-preview-step-name>-</dd></div>
      <div class="wide"><dt>Current Target</dt><dd data-preview-current-target>-</dd></div>
      <div class="wide"><dt>Next Target</dt><dd data-preview-next-target>-</dd></div>
      <div class="wide"><dt>Preview Basis</dt><dd>Approximate visual preview from server target metadata</dd></div>
    </dl>
  `;
  options.container.append(programPreview);

  const note = document.createElement('section');
  note.className = 'note-panel';
  note.innerHTML = `
    <p>V6 uses a segmented GLB model when available and falls back to the procedural robot.</p>
    <p>Live physics comes from server telemetry; in Live Telemetry Mode the browser is only a renderer.</p>
  `;
  options.container.append(note);

  const standardsStory = document.createElement('details');
  standardsStory.className = 'standards-story';
  standardsStory.open = false;
  standardsStory.innerHTML = `
    <summary>Standards Story</summary>
    <ul>
      <li>Official OPC UA Robotics instance model</li>
      <li>Simulation-bound live values</li>
      <li>WebSocket visualization bridge</li>
      <li>Program metadata preview when emitted by server telemetry</li>
      <li>Browser renders only, server remains source of truth</li>
    </ul>
  `;
  options.container.append(standardsStory);

  const modeStatus = options.container.querySelector<HTMLElement>('[data-mode-status]');
  const statusMode = options.container.querySelector<HTMLElement>('[data-status-mode]');
  const statusConnection = options.container.querySelector<HTMLElement>('[data-status-connection]');
  const statusHeartbeat = options.container.querySelector<HTMLElement>('[data-status-heartbeat]');
  const statusAge = options.container.querySelector<HTMLElement>('[data-status-age]');
  const programName = options.container.querySelector<HTMLElement>('[data-program-name]');
  const programState = options.container.querySelector<HTMLElement>('[data-program-state]');
  const programStep = options.container.querySelector<HTMLElement>('[data-program-step]');
  const movingState = options.container.querySelector<HTMLElement>('[data-moving-state]');
  const timestamp = options.container.querySelector<HTMLElement>('[data-timestamp]');
  const modelStatus = options.container.querySelector<HTMLElement>('[data-model-status]');
  const previewMessage = options.container.querySelector<HTMLElement>('[data-program-preview-message]');
  const previewProgram = options.container.querySelector<HTMLElement>('[data-preview-program]');
  const previewState = options.container.querySelector<HTMLElement>('[data-preview-state]');
  const previewStep = options.container.querySelector<HTMLElement>('[data-preview-step]');
  const previewStepType = options.container.querySelector<HTMLElement>('[data-preview-step-type]');
  const previewStepName = options.container.querySelector<HTMLElement>('[data-preview-step-name]');
  const previewCurrentTarget = options.container.querySelector<HTMLElement>('[data-preview-current-target]');
  const previewNextTarget = options.container.querySelector<HTMLElement>('[data-preview-next-target]');

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
      currentMode = mode;
      if (!modeStatus) {
        return;
      }

      modeStatus.textContent = modeLabels[mode];
      modeStatus.classList.toggle('is-running', mode === 'localDemo');
      modeStatus.classList.toggle('is-live', mode === 'liveTelemetry');
      modeStatus.classList.toggle('is-presentation', mode === 'presentationDemo');

      if (statusMode) {
        statusMode.textContent = modeLabels[mode];
      }

      updateProgramPreviewPanel();
    },
    setConnectionStatus(status: TelemetryConnectionStatus, detail?: string): void {
      connectionStatus.textContent = detail ? `${status}: ${detail}` : status;
      connectionStatus.dataset.status = status;
      if (statusConnection) {
        statusConnection.textContent = detail ? `${status}: ${detail}` : status;
        statusConnection.dataset.status = status;
      }
      connectButton.disabled = status === 'connecting' || status === 'connected';
      disconnectButton.disabled = status !== 'connecting' && status !== 'connected';
      urlInput.disabled = status === 'connecting' || status === 'connected';
    },
    setManualControlState(isEnabled: boolean, reason: ManualControlReason): void {
      for (const slider of sliderInputs.values()) {
        slider.disabled = !isEnabled;
      }

      resetButton.disabled = !isEnabled;
      demoButton.disabled = !isEnabled;
      stopButton.disabled = reason !== 'localDemo';
      manualStatus.textContent = isEnabled
        ? 'Manual joint controls'
        : manualStatusLabels[reason];
      sliderSection.classList.toggle('is-disabled', !isEnabled);
    },
    setTelemetryHealth(heartbeat: TelemetryHeartbeat, ageMs: number | null): void {
      if (statusHeartbeat) {
        statusHeartbeat.textContent = heartbeatLabels[heartbeat];
        statusHeartbeat.dataset.heartbeat = heartbeat;
      }

      if (statusAge) {
        statusAge.textContent = ageMs === null ? '-' : `${Math.max(0, Math.round(ageMs))} ms`;
      }
    },
    setTelemetryDetails(details): void {
      currentProgramDetails = details;
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

      updateProgramPreviewPanel();
    },
    setModelStatus(status: RobotModelStatus, message: string): void {
      if (!modelStatus) {
        return;
      }

      modelStatus.dataset.modelStatus = status;
      modelStatus.textContent = modelStatusLabels[status];
      modelStatus.title = message;
      reloadModelButton.disabled = status === 'glbLoading';
    },
    setPresentationActive(isActive: boolean): void {
      startPresentationButton.disabled = isActive;
      stopPresentationButton.disabled = !isActive;
      options.container.classList.toggle('is-presentation-active', isActive);
    },
    setVisualizationOptions(nextOptions: VisualizationOptions): void {
      for (const key of Object.keys(nextOptions) as (keyof VisualizationOptions)[]) {
        visualizationOptions[key] = nextOptions[key];
        const input = optionInputs.get(key);
        if (input) {
          input.checked = nextOptions[key];
        }
      }
    },
  };

  function addVisualizationToggle(key: keyof VisualizationOptions, label: string): void {
    const toggle = checkbox(label, visualizationOptions[key], (value) => updateOption(key, value));
    optionInputs.set(key, toggle.input);
    toggles.append(toggle.row);
  }

  function updateOption(key: keyof VisualizationOptions, value: boolean): void {
    visualizationOptions[key] = value;
    options.onVisualizationOptionChange({ ...visualizationOptions });
  }

  function updateProgramPreviewPanel(): void {
    const details = currentProgramDetails;
    const hasProgramMetadata = !!details.currentProgramName
      || details.currentStepIndex !== undefined
      || !!details.currentStepType
      || hasAnyJointAngle(details.activeTargetJointAngles)
      || hasAnyJointAngle(details.nextTargetJointAngles);

    if (previewMessage) {
      previewMessage.textContent = getProgramPreviewMessage(currentMode, hasProgramMetadata);
      previewMessage.dataset.previewState = hasProgramMetadata ? 'available' : 'empty';
    }

    if (previewProgram) {
      previewProgram.textContent = details.currentProgramName || '-';
    }

    if (previewState) {
      previewState.textContent = details.programExecutionState || '-';
    }

    if (previewStep) {
      previewStep.textContent = formatStep(details.currentStepIndex, details.totalStepCount);
    }

    if (previewStepType) {
      previewStepType.textContent = details.currentStepType || '-';
    }

    if (previewStepName) {
      previewStepName.textContent = details.currentStepName || '-';
    }

    if (previewCurrentTarget) {
      previewCurrentTarget.textContent = formatJointTarget(details.activeTargetJointAngles);
      previewCurrentTarget.title = previewCurrentTarget.textContent;
    }

    if (previewNextTarget) {
      previewNextTarget.textContent = formatJointTarget(details.nextTargetJointAngles);
      previewNextTarget.title = previewNextTarget.textContent;
    }
  }
}

const modeLabels: Record<VisualizationMode, string> = {
  manual: 'Manual Mode',
  localDemo: 'Local Demo Mode',
  liveTelemetry: 'Live Telemetry Mode',
  presentationDemo: 'Presentation Demo Mode',
};

const manualStatusLabels: Record<ManualControlReason, string> = {
  manual: 'Manual joint controls',
  localDemo: 'Sliders follow Local Demo animation',
  liveTelemetry: 'Sliders follow live telemetry',
  presentationDemo: 'Presentation mode is controlling the demo view',
};

const heartbeatLabels: Record<TelemetryHeartbeat, string> = {
  live: 'Live',
  stale: 'Stale',
  disconnected: 'Disconnected',
};

const modelStatusLabels: Record<RobotModelStatus, string> = {
  primitive: 'Primitive fallback model',
  glbLoaded: 'GLB model loaded',
  glbFailed: 'GLB load failed, using primitive fallback',
  glbLoading: 'Loading GLB model',
};

function button(label: string, variant: 'primary' | 'secondary'): HTMLButtonElement {
  const element = document.createElement('button');
  element.type = 'button';
  element.className = `button ${variant}`;
  element.textContent = label;
  return element;
}

function checkbox(
  label: string,
  checked: boolean,
  onChange: (value: boolean) => void,
): { row: HTMLLabelElement; input: HTMLInputElement } {
  const row = document.createElement('label');
  row.className = 'toggle-row';

  const input = document.createElement('input');
  input.type = 'checkbox';
  input.checked = checked;
  input.addEventListener('change', () => onChange(input.checked));

  const text = document.createElement('span');
  text.textContent = label;

  row.append(input, text);
  return { row, input };
}

function formatDegrees(value: number): string {
  const rounded = Math.abs(value) < 0.05 ? 0 : value;
  return `${rounded.toFixed(0)} deg`;
}

function formatVelocity(value: number): string {
  const rounded = Math.abs(value) < 0.005 ? 0 : value;
  return `${rounded.toFixed(1)} deg/s`;
}

function formatStep(index: number | null | undefined, total: number | undefined): string {
  if (index === null || index === undefined) {
    return total && total > 0 ? `- / ${total}` : '-';
  }

  return total && total > 0 ? `${index + 1} / ${total}` : index.toString();
}

function formatJointTarget(target: Partial<JointAngles> | null | undefined): string {
  if (!hasAnyJointAngle(target)) {
    return '-';
  }

  return jointNames
    .filter((jointName) => target?.[jointName] !== undefined)
    .map((jointName) => `${jointName} ${formatDegrees(target?.[jointName] ?? 0)}`)
    .join('  ');
}

function hasAnyJointAngle(target: Partial<JointAngles> | null | undefined): boolean {
  return !!target && Object.keys(target).length > 0;
}

function getProgramPreviewMessage(mode: VisualizationMode, hasProgramMetadata: boolean): string {
  if (hasProgramMetadata) {
    return 'Server program metadata available';
  }

  if (mode === 'localDemo') {
    return 'Local Demo motion has no server program metadata';
  }

  if (mode === 'presentationDemo') {
    return 'Presentation local demo has no server program metadata';
  }

  if (mode === 'manual') {
    return 'No server program target available in Manual Mode';
  }

  return 'No program target data available';
}
