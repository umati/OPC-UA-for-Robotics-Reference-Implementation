export const defaultControlBaseUrl = 'http://localhost:48080';

export interface ControlCommandResponse {
  accepted: boolean;
  message: string;
}

export class RobotControlClient {
  constructor(private baseUrl: string = defaultControlBaseUrl) {
  }

  setBaseUrl(baseUrl: string): void {
    this.baseUrl = normalizeBaseUrl(baseUrl);
  }

  startDemoMotion(): Promise<ControlCommandResponse> {
    return this.post('/control/demo/start');
  }

  stopMotion(): Promise<ControlCommandResponse> {
    return this.post('/control/motion/stop');
  }

  loadSampleProgram(sampleName: string): Promise<ControlCommandResponse> {
    return this.post('/control/programs/load-sample', { sampleName });
  }

  loadJsonProgram(programJson: string): Promise<ControlCommandResponse> {
    return this.post('/control/programs/load-json', { programJson });
  }

  startProgram(): Promise<ControlCommandResponse> {
    return this.post('/control/programs/start');
  }

  pauseProgram(): Promise<ControlCommandResponse> {
    return this.post('/control/programs/pause');
  }

  resumeProgram(): Promise<ControlCommandResponse> {
    return this.post('/control/programs/resume');
  }

  stopProgram(): Promise<ControlCommandResponse> {
    return this.post('/control/programs/stop');
  }

  private async post(path: string, body?: unknown): Promise<ControlCommandResponse> {
    const url = `${normalizeBaseUrl(this.baseUrl)}${path}`;

    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: body === undefined ? '{}' : JSON.stringify(body),
      });

      const parsed = await readControlResponse(response);
      if (!response.ok && parsed.accepted) {
        return {
          accepted: false,
          message: `HTTP ${response.status}: command response was inconsistent.`,
        };
      }

      return parsed;
    } catch (error: unknown) {
      return {
        accepted: false,
        message: error instanceof Error
          ? `Control bridge request failed: ${error.message}`
          : 'Control bridge request failed.',
      };
    }
  }
}

async function readControlResponse(response: Response): Promise<ControlCommandResponse> {
  const text = await response.text();
  if (!text) {
    return {
      accepted: false,
      message: `HTTP ${response.status}: empty response from control bridge.`,
    };
  }

  try {
    const parsed: unknown = JSON.parse(text);
    if (isControlCommandResponse(parsed)) {
      return parsed;
    }
  } catch {
    return {
      accepted: false,
      message: `HTTP ${response.status}: response was not valid JSON.`,
    };
  }

  return {
    accepted: false,
    message: `HTTP ${response.status}: response did not match the control bridge JSON contract.`,
  };
}

function isControlCommandResponse(value: unknown): value is ControlCommandResponse {
  return typeof value === 'object'
    && value !== null
    && 'accepted' in value
    && 'message' in value
    && typeof value.accepted === 'boolean'
    && typeof value.message === 'string';
}

function normalizeBaseUrl(baseUrl: string): string {
  return baseUrl.trim().replace(/\/+$/, '');
}
