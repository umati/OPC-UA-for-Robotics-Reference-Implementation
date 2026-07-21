import { Discovery, Snapshot, CallResponse, LiveMessage, Robot, RobotDiagnostics } from './types';

const configuredGateway = import.meta.env.VITE_GATEWAY_BASE_URL || import.meta.env.VITE_GATEWAY_URL || '';

export const normalizeGatewayBase = (base?: string | null) => (base || '').trim().replace(/\/+$/, '');
export const DEFAULT_GATEWAY = normalizeGatewayBase(configuredGateway);

export const buildApiUrl = (base: string, path: string) => `${normalizeGatewayBase(base)}${path.startsWith('/') ? path : `/${path}`}`;

export const buildWebSocketUrl = (base: string, path: string, locationHref = typeof window === 'undefined' ? 'http://localhost/' : window.location.href) => {
  const normalizedBase = normalizeGatewayBase(base);
  const origin = normalizedBase || new URL(locationHref).origin;
  const url = new URL(path.startsWith('/') ? path : `/${path}`, origin);
  url.protocol = url.protocol === 'https:' ? 'wss:' : 'ws:';
  return url;
};

const json = async <T>(base: string, path: string, options?: RequestInit): Promise<T> => {
  const response = await fetch(buildApiUrl(base, path), {
    ...options,
    headers: { 'Content-Type': 'application/json', ...(options?.headers || {}) },
  });
  const body = await response.json().catch(() => ({}));
  if (!response.ok) throw new Error(body.details || body.error || `HTTP ${response.status}`);
  return body;
};

export const getHealth = (base: string) => json<any>(base, '/health');
export const getOpcUaStatus = (base: string) => json<any>(base, '/api/opcua/status');
export const getDiscovery = (base: string) => json<Discovery>(base, '/api/robotics/discovery');
export const getSnapshot = (base: string, selection: 'all' | 'states' | 'equipment') => json<Snapshot>(base, `/api/robotics/snapshot?selection=${selection}`);
export const getMetadata = (base: string) => json<any>(base, '/api/metadata');
export const getRobots = (base: string) => json<Robot[]>(base, '/api/robots');
export const getRobotStatus = (base: string, id: string) => json<any>(base, `/api/robots/${encodeURIComponent(id)}/status`);
export const getRobotDiscovery = (base: string, id: string) => json<Discovery>(base, `/api/robots/${encodeURIComponent(id)}/discovery`);
export const getRobotSnapshot = (base: string, id: string, selection: 'all' | 'states' | 'equipment') => json<Snapshot>(base, `/api/robots/${encodeURIComponent(id)}/snapshot?selection=${selection}`);
export const getRobotDiagnostics = (base: string, id: string) => json<RobotDiagnostics>(base, `/api/robots/${encodeURIComponent(id)}/diagnostics`);

const call = (base: string, path: string, body: any = {}) => json<CallResponse>(base, path, { method: 'POST', body: JSON.stringify(body) });
export const callSystemGetReady = (base: string) => call(base, '/api/robotics/system/get-ready');
export const callSystemStart = (base: string) => call(base, '/api/robotics/system/start');
export const callSystemStop = (base: string, stopMode = 0) => call(base, '/api/robotics/system/stop', { stopMode });
export const callSystemStandDown = (base: string) => call(base, '/api/robotics/system/stand-down');
export const callTaskLoadByName = (base: string, programName: string) => call(base, '/api/robotics/task/load-by-name', { programName });
export const callTaskStart = (base: string) => call(base, '/api/robotics/task/start');
export const callTaskStop = (base: string, stopMode = 0) => call(base, '/api/robotics/task/stop', { stopMode });
export const callTaskResetToProgramStart = (base: string) => call(base, '/api/robotics/task/reset-to-program-start');
export const callTaskUnloadProgram = (base: string) => call(base, '/api/robotics/task/unload-program');

export const createLiveWebSocket = (base: string, handler: (message: LiveMessage) => void, onState: (state: string) => void) => {
  const url = buildWebSocketUrl(base, '/ws/robotics/live');
  url.search = 'selection=all&sendInitialSnapshot=true';
  const socket = new WebSocket(url);
  socket.onopen = () => onState('connected');
  socket.onclose = () => onState('disconnected');
  socket.onerror = () => onState('error');
  socket.onmessage = event => { try { handler(JSON.parse(event.data)); } catch { /* Ignore malformed live messages. */ } };
  return socket;
};

export const createRobotLiveSocket = (base: string, robotId: string, options: Record<string, string | number | boolean> = {}) => {
  const url = buildWebSocketUrl(base, `/ws/robots/${encodeURIComponent(robotId)}/live`);
  Object.entries(options).forEach(([key, value]) => url.searchParams.set(key, String(value)));
  return new WebSocket(url);
};

const robotPath = (id: string, path: string) => `/api/robots/${encodeURIComponent(id)}${path}`;
export const callRobotSystemGetReady = (base: string, id: string) => call(base, robotPath(id, '/system/get-ready'));
export const callRobotSystemStart = (base: string, id: string) => call(base, robotPath(id, '/system/start'));
export const callRobotSystemStop = (base: string, id: string, stopMode = 0) => call(base, robotPath(id, '/system/stop'), { stopMode });
export const callRobotSystemStandDown = (base: string, id: string) => call(base, robotPath(id, '/system/stand-down'));
export const callRobotTaskLoadByName = (base: string, id: string, programName: string) => call(base, robotPath(id, '/task/load-by-name'), { programName });
export const callRobotTaskStart = (base: string, id: string) => call(base, robotPath(id, '/task/start'));
export const callRobotTaskStop = (base: string, id: string, stopMode = 0) => call(base, robotPath(id, '/task/stop'), { stopMode });
export const callRobotTaskResetToProgramStart = (base: string, id: string) => call(base, robotPath(id, '/task/reset-to-program-start'));
export const callRobotTaskUnloadProgram = (base: string, id: string) => call(base, robotPath(id, '/task/unload-program'));
