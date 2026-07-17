import { defaultControlBaseUrl, RobotControlClient, type ControlCommandResponse } from './controlClient';

const sampleProgramNames = ['pick-and-place-demo', 'axis-range-demo'] as const;

type SampleProgramName = (typeof sampleProgramNames)[number];

interface CommandStatus {
  commandName: string;
  result: ControlCommandResponse;
  timestamp: Date;
}

export function createControlPanel(): HTMLElement {
  const client = new RobotControlClient(defaultControlBaseUrl);
  let currentProgramJson = '';
  let lastCommandStatus: CommandStatus | null = null;

  const section = document.createElement('section');
  section.className = 'server-control-panel';
  section.setAttribute('aria-label', 'Server control');

  const heading = document.createElement('h2');
  heading.textContent = 'Server Control';

  const note = document.createElement('p');
  note.className = 'control-note';
  note.textContent = 'Browser controls use local demo bridge. OPC UA server remains the source of truth.';

  const baseUrlLabel = document.createElement('label');
  baseUrlLabel.className = 'url-field';

  const baseUrlText = document.createElement('span');
  baseUrlText.textContent = 'Control Base URL';

  const baseUrlInput = document.createElement('input');
  baseUrlInput.type = 'url';
  baseUrlInput.value = defaultControlBaseUrl;
  baseUrlInput.spellcheck = false;
  baseUrlInput.addEventListener('change', () => client.setBaseUrl(baseUrlInput.value));
  baseUrlLabel.append(baseUrlText, baseUrlInput);

  const commandGrid = document.createElement('div');
  commandGrid.className = 'control-command-grid';

  const startDemoButton = commandButton('Start Demo Motion', () => runCommand('Start Demo Motion', () => client.startDemoMotion()));
  const stopMotionButton = commandButton('Stop Motion', () => runCommand('Stop Motion', () => client.stopMotion()));
  const startProgramButton = commandButton('Start Program', () => runCommand('Start Program', () => client.startProgram()));
  const pauseProgramButton = commandButton('Pause Program', () => runCommand('Pause Program', () => client.pauseProgram()));
  const resumeProgramButton = commandButton('Resume Program', () => runCommand('Resume Program', () => client.resumeProgram()));
  const stopProgramButton = commandButton('Stop Program', () => runCommand('Stop Program', () => client.stopProgram()));
  commandGrid.append(startDemoButton, stopMotionButton, startProgramButton, pauseProgramButton, resumeProgramButton, stopProgramButton);

  const sampleRow = document.createElement('div');
  sampleRow.className = 'control-sample-row';

  const sampleLabel = document.createElement('label');
  sampleLabel.className = 'select-field';

  const sampleText = document.createElement('span');
  sampleText.textContent = 'Sample Program';

  const sampleSelect = document.createElement('select');
  for (const sampleName of sampleProgramNames) {
    const option = document.createElement('option');
    option.value = sampleName;
    option.textContent = sampleName;
    sampleSelect.append(option);
  }
  sampleLabel.append(sampleText, sampleSelect);

  const loadSampleButton = commandButton('Load Sample Program', () => {
    const sampleName = sampleSelect.value as SampleProgramName;
    return runCommand('Load Sample Program', () => client.loadSampleProgram(sampleName));
  });
  sampleRow.append(sampleLabel, loadSampleButton);

  const uploadArea = document.createElement('div');
  uploadArea.className = 'control-upload';

  const fileLabel = document.createElement('label');
  fileLabel.className = 'file-field';
  const fileText = document.createElement('span');
  fileText.textContent = 'JSON Program Upload';
  const fileInput = document.createElement('input');
  fileInput.type = 'file';
  fileInput.accept = '.json,application/json';
  fileInput.addEventListener('change', () => {
    void readSelectedFile(fileInput);
  });
  fileLabel.append(fileText, fileInput);

  const preview = document.createElement('textarea');
  preview.className = 'program-json-preview';
  preview.rows = 7;
  preview.spellcheck = false;
  preview.placeholder = '{ "programName": "..." }';
  preview.addEventListener('input', () => {
    currentProgramJson = preview.value;
  });

  const loadUploadedButton = commandButton('Load Uploaded Program', () => {
    currentProgramJson = preview.value;
    return runCommand('Load Uploaded Program', () => client.loadJsonProgram(currentProgramJson));
  });

  uploadArea.append(fileLabel, preview, loadUploadedButton);

  const resultArea = document.createElement('dl');
  resultArea.className = 'command-result';
  resultArea.innerHTML = `
    <div><dt>Last command</dt><dd data-command-name>-</dd></div>
    <div><dt>Accepted</dt><dd data-command-accepted>-</dd></div>
    <div class="wide"><dt>Message</dt><dd data-command-message>-</dd></div>
    <div class="wide"><dt>Timestamp</dt><dd data-command-timestamp>-</dd></div>
  `;

  section.append(heading, note, baseUrlLabel, commandGrid, sampleRow, uploadArea, resultArea);
  renderStatus();
  return section;

  async function runCommand(
    commandName: string,
    send: () => Promise<ControlCommandResponse>,
  ): Promise<void> {
    client.setBaseUrl(baseUrlInput.value);
    setBusy(true);
    setPending(commandName);

    try {
      const result = await send();
      lastCommandStatus = {
        commandName,
        result,
        timestamp: new Date(),
      };
      renderStatus();
    } finally {
      setBusy(false);
    }
  }

  async function readSelectedFile(input: HTMLInputElement): Promise<void> {
    const file = input.files?.[0];
    if (!file) {
      return;
    }

    try {
      currentProgramJson = await file.text();
      preview.value = currentProgramJson;
      lastCommandStatus = {
        commandName: 'Read JSON Program File',
        result: {
          accepted: true,
          message: `Loaded ${file.name} into the preview.`,
        },
        timestamp: new Date(),
      };
    } catch (error: unknown) {
      lastCommandStatus = {
        commandName: 'Read JSON Program File',
        result: {
          accepted: false,
          message: error instanceof Error ? error.message : 'Could not read selected file.',
        },
        timestamp: new Date(),
      };
    }

    renderStatus();
  }

  function setPending(commandName: string): void {
    lastCommandStatus = {
      commandName,
      result: {
        accepted: false,
        message: 'Request pending...',
      },
      timestamp: new Date(),
    };
    renderStatus();
  }

  function renderStatus(): void {
    const commandName = resultArea.querySelector<HTMLElement>('[data-command-name]');
    const accepted = resultArea.querySelector<HTMLElement>('[data-command-accepted]');
    const message = resultArea.querySelector<HTMLElement>('[data-command-message]');
    const timestamp = resultArea.querySelector<HTMLElement>('[data-command-timestamp]');

    if (!lastCommandStatus) {
      return;
    }

    if (commandName) {
      commandName.textContent = lastCommandStatus.commandName;
    }

    if (accepted) {
      accepted.textContent = lastCommandStatus.result.accepted ? 'true' : 'false';
      accepted.dataset.accepted = lastCommandStatus.result.accepted ? 'true' : 'false';
    }

    if (message) {
      message.textContent = lastCommandStatus.result.message;
      message.title = lastCommandStatus.result.message;
    }

    if (timestamp) {
      timestamp.textContent = lastCommandStatus.timestamp.toLocaleString();
    }
  }

  function setBusy(isBusy: boolean): void {
    const inputs = section.querySelectorAll<HTMLButtonElement | HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>(
      'button, input, select, textarea',
    );
    inputs.forEach((input) => {
      input.disabled = isBusy;
    });
  }
}

function commandButton(label: string, onClick: () => Promise<void>): HTMLButtonElement {
  const element = document.createElement('button');
  element.type = 'button';
  element.className = 'button secondary';
  element.textContent = label;
  element.addEventListener('click', () => {
    void onClick();
  });
  return element;
}
