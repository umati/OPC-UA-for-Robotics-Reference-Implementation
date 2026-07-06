import { jointLimits, jointNames, type JointAngles, type JointName, type UiController } from './types';

interface UiOptions {
  container: HTMLElement;
  initialAngles: JointAngles;
  onJointChange: (jointName: JointName, value: number) => void;
  onResetHome: () => void;
  onDemoStart: () => void;
  onDemoStop: () => void;
}

export function createRobotUi(options: UiOptions): UiController {
  const sliderInputs = new Map<JointName, HTMLInputElement>();
  const valueOutputs = new Map<JointName, HTMLOutputElement>();

  options.container.innerHTML = '';

  const header = document.createElement('header');
  header.className = 'panel-header';
  header.innerHTML = `
    <div>
      <p class="eyebrow">OPC UA Robotics</p>
      <h1>Visualization V1</h1>
    </div>
    <span class="status-pill" data-demo-status>Offline Manual</span>
  `;
  options.container.append(header);

  const actions = document.createElement('div');
  actions.className = 'action-row';

  const resetButton = button('Reset Home', 'secondary');
  const demoButton = button('Demo Motion', 'primary');
  const stopButton = button('Stop Demo', 'secondary');

  resetButton.addEventListener('click', options.onResetHome);
  demoButton.addEventListener('click', options.onDemoStart);
  stopButton.addEventListener('click', options.onDemoStop);

  actions.append(resetButton, demoButton, stopButton);
  options.container.append(actions);

  const sliderSection = document.createElement('section');
  sliderSection.className = 'control-section';
  sliderSection.setAttribute('aria-label', 'Manual joint sliders');

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
  telemetry.setAttribute('aria-label', 'Current joint values');
  telemetry.innerHTML = '<h2>Joint Values</h2>';

  const telemetryGrid = document.createElement('dl');
  telemetryGrid.className = 'telemetry-grid';

  for (const jointName of jointNames) {
    const cell = document.createElement('div');
    cell.className = 'telemetry-cell';

    const term = document.createElement('dt');
    term.textContent = jointName;

    const detail = document.createElement('dd');
    detail.dataset.telemetryJoint = jointName;
    detail.textContent = formatDegrees(options.initialAngles[jointName]);

    cell.append(term, detail);
    telemetryGrid.append(cell);
  }

  telemetry.append(telemetryGrid);
  options.container.append(telemetry);

  const status = options.container.querySelector<HTMLElement>('[data-demo-status]');

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
    setDemoRunning(isRunning: boolean): void {
      if (!status) {
        return;
      }

      status.textContent = isRunning ? 'Local Demo' : 'Offline Manual';
      status.classList.toggle('is-running', isRunning);
    },
  };
}

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
