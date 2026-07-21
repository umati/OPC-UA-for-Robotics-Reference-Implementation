import { useCallback, useEffect, useRef, useState } from 'react';
import * as api from './api/gatewayClient';
import { Discovery, LiveMessage, Robot, RobotDiagnostics, RobotStatus, Snapshot } from './api/types';
import RobotCard from './components/RobotCard';
import { isGoodStatus } from './robot3d/mapping';

type RobotViewState = {
  status?: RobotStatus;
  discovery?: Discovery;
  snapshot?: Snapshot;
  equipment?: Snapshot;
  diagnostics?: RobotDiagnostics;
  live: string;
  events: LiveMessage[];
  updates: Record<string, number>;
  error?: string;
  nextRetryAt?: number;
  reconnectAttempt: number;
};

const blank = (): RobotViewState => ({ live: 'disconnected', events: [], updates: {}, reconnectAttempt: 0 });

const seedGoodValues = (snapshot?: Snapshot) => snapshot && ({
  ...snapshot,
  sections: snapshot.sections.map(section => ({
    ...section,
    values: section.values.map(value => isGoodStatus(value.statusCode)
      ? { ...value, lastGoodValueText: value.valueText, lastGoodSourceTimestamp: value.sourceTimestamp, lastGoodServerTimestamp: value.serverTimestamp, lastGoodUpdatedAt: Date.now() }
      : value)
  }))
});

export default function App() {
  const [gateway, setGateway] = useState(api.DEFAULT_GATEWAY);
  const [robots, setRobots] = useState<Robot[]>([]);
  const [views, setViews] = useState<Record<string, RobotViewState>>({});
  const [health, setHealth] = useState('checking');
  const [error, setError] = useState('');
  const [lastRefresh, setLastRefresh] = useState<Date>();
  const [showcase, setShowcase] = useState(false);
  const [metadata, setMetadata] = useState<any>();
  const sockets = useRef<Record<string, WebSocket>>({});
  const timers = useRef<Record<string, number>>({});
  const attempts = useRef<Record<string, number>>({});
  const stopping = useRef(false);

  const update = useCallback((id: string, fn: (view: RobotViewState) => RobotViewState) => {
    setViews(current => ({ ...current, [id]: fn(current[id] || blank()) }));
  }, []);

  const cancelRetry = useCallback((id: string) => {
    const timer = timers.current[id];
    if (timer !== undefined) {
      window.clearTimeout(timer);
      delete timers.current[id];
    }
    update(id, view => ({ ...view, nextRetryAt: undefined }));
  }, [update]);

  const connectRobot = useCallback((robot: Robot) => {
    if (stopping.current || !robot.enabled) return;

    const existing = sockets.current[robot.id];
    if (existing && (existing.readyState === WebSocket.CONNECTING || existing.readyState === WebSocket.OPEN)) return;

    cancelRetry(robot.id);
    const socket = api.createRobotLiveSocket(gateway, robot.id, { selection: 'all', sendInitialSnapshot: true });
    sockets.current[robot.id] = socket;
    update(robot.id, view => ({ ...view, live: 'connecting', error: undefined }));

    socket.onopen = () => {
      attempts.current[robot.id] = 0;
      update(robot.id, view => ({ ...view, live: 'connected', nextRetryAt: undefined, error: undefined, reconnectAttempt: 0 }));
    };

    socket.onclose = () => {
      if (sockets.current[robot.id] === socket) delete sockets.current[robot.id];
      if (stopping.current) return;

      const attempt = (attempts.current[robot.id] || 0) + 1;
      attempts.current[robot.id] = attempt;
      const delay = Math.min(30000, 1000 * Math.pow(2, attempt - 1));
      const retryAt = Date.now() + delay;

      update(robot.id, view => ({
        ...view,
        live: 'disconnected',
        nextRetryAt: retryAt,
        reconnectAttempt: attempt,
        error: view.error || 'Live WebSocket disconnected'
      }));

      timers.current[robot.id] = window.setTimeout(() => {
        delete timers.current[robot.id];
        connectRobot(robot);
      }, delay);
    };

    socket.onerror = () => update(robot.id, view => ({
      ...view,
      live: 'error',
      error: view.error || 'Live WebSocket failed; automatic retry is waiting.'
    }));

    socket.onmessage = event => {
      try {
        const message: LiveMessage = JSON.parse(event.data);
        if (message.type === 'snapshot' && Array.isArray(message.sections)) {
          update(robot.id, view => ({ ...view, snapshot: seedGoodValues({ connected: true, endpointUrl: message.endpointUrl || robot.endpointUrl, generatedAtUtc: message.generatedAtUtc || new Date().toISOString(), sections: message.sections, warnings: [] }) }));
          return;
        }
        update(robot.id, view => {
          const merge = (snapshot?: Snapshot) => snapshot && ({
            ...snapshot,
            sections: snapshot.sections.map(section => ({
              ...section,
              values: section.values.map(value => {
                if (value.nodeId !== message.nodeId) return value;
                const good = isGoodStatus(message.statusCode);
                return {
                  ...value,
                  valueText: good ? (message.valueText ?? value.valueText) : (value.lastGoodValueText ?? value.valueText),
                  statusCode: message.statusCode ?? value.statusCode,
                  sourceTimestamp: message.sourceTimestamp ?? value.sourceTimestamp,
                  serverTimestamp: message.serverTimestamp ?? value.serverTimestamp,
                  ...(good ? {
                    lastGoodValueText: message.valueText ?? value.valueText,
                    lastGoodSourceTimestamp: message.sourceTimestamp ?? value.sourceTimestamp,
                    lastGoodServerTimestamp: message.serverTimestamp ?? value.serverTimestamp,
                    lastGoodUpdatedAt: Date.now()
                  } : {})
                };
              })
            }))
          });

          return {
            ...view,
            events: [message, ...view.events].slice(0, 40),
            snapshot: merge(view.snapshot),
            equipment: merge(view.equipment),
            updates: message.nodeId ? { ...view.updates, [message.nodeId]: Date.now() } : view.updates
          };
        });
      } catch {
        // Ignore malformed live messages; the connection remains managed by the socket handlers.
      }
    };
  }, [cancelRetry, gateway, update]);

  const loadRobot = useCallback(async (robot: Robot) => {
    update(robot.id, view => ({ ...view, error: undefined }));
    try {
      const [status, discovery, snapshot, equipment, diagnostics] = await Promise.all([
        api.getRobotStatus(gateway, robot.id).catch(exception => ({
          robotId: robot.id,
          displayName: robot.displayName,
          endpointUrl: robot.endpointUrl,
          connected: false,
          roboticsNamespaceFound: false,
          error: exception instanceof Error ? exception.message : String(exception)
        })),
        api.getRobotDiscovery(gateway, robot.id),
        api.getRobotSnapshot(gateway, robot.id, 'states'),
        api.getRobotSnapshot(gateway, robot.id, 'equipment'),
        api.getRobotDiagnostics(gateway, robot.id).catch(() => undefined)
      ]);
      update(robot.id, view => ({
        ...view,
        status,
        discovery,
        snapshot: seedGoodValues(snapshot),
        equipment: seedGoodValues(equipment),
        diagnostics,
        error: status.connected ? '' : status.error || undefined
      }));
    } catch (exception) {
      update(robot.id, view => ({
        ...view,
        error: exception instanceof Error ? exception.message : String(exception),
        status: {
          ...view.status,
          connected: false,
          robotId: robot.id,
          displayName: robot.displayName,
          endpointUrl: robot.endpointUrl,
          roboticsNamespaceFound: false
        }
      }));
    }
  }, [gateway, update]);

  const refresh = useCallback(async () => {
    setHealth('checking');
    setError('');
    try {
      const [list, info] = await Promise.all([api.getRobots(gateway), api.getMetadata(gateway)]);
      setMetadata(info);
      setRobots(list);
      setViews(current => Object.fromEntries(list.map(robot => [robot.id, current[robot.id] || blank()])));
      await Promise.all(list.map(loadRobot));
      setHealth('online');
      setLastRefresh(new Date());
    } catch (exception) {
      setHealth('offline');
      setError(exception instanceof Error ? exception.message : 'Gateway unreachable');
    }
  }, [gateway, loadRobot]);

  useEffect(() => {
    stopping.current = false;
    refresh();
    return () => {
      stopping.current = true;
      Object.values(timers.current).forEach(timer => window.clearTimeout(timer));
      timers.current = {};
      Object.values(sockets.current).forEach(socket => socket.close());
      sockets.current = {};
    };
  }, [refresh]);

  useEffect(() => {
    const active = new Set(robots.map(robot => robot.id));
    Object.keys(timers.current).filter(id => !active.has(id)).forEach(cancelRetry);
    Object.keys(sockets.current).filter(id => !active.has(id)).forEach(id => {
      sockets.current[id].close();
      delete sockets.current[id];
    });
    robots.forEach(robot => {
      if (!sockets.current[robot.id] && robot.enabled) connectRobot(robot);
    });
  }, [robots, connectRobot, cancelRetry]);

  useEffect(() => {
    if (metadata?.version) document.title = `OPC UA Robotics Workbench ${metadata.version}`;
  }, [metadata]);

  return <div className={showcase ? 'app showcase' : 'app'}>
    <header><div><span className="eyebrow">REFERENCE IMPLEMENTATION · C18A.1</span><h1>OPC UA Robotics <i>Fleet</i></h1><p>A calm, standards-led view of connected motion systems</p></div><div className="header-right"><div className="header-mark">◈<span>ROBOTICS<br />MODEL</span></div><button className="showcase-toggle" onClick={() => setShowcase(!showcase)}>▣ {showcase ? 'Exit tradeshow' : 'Tradeshow mode'}</button></div></header>
    <section className="fleet-toolbar card"><div><span className={`status-dot ${health}`} /><b>Gateway {health}</b><span className="fleet-count">{robots.length} configured robot{robots.length === 1 ? '' : 's'}</span>{lastRefresh && <small>Updated {lastRefresh.toLocaleTimeString()}</small>}</div><div className="url-row"><input aria-label="Gateway URL" value={gateway} onChange={event => setGateway(event.target.value)} /><button onClick={refresh}>↻ Refresh fleet</button></div></section>
    {error && <div className="fleet-error alert"><b>Gateway issue</b><span>{error}</span></div>}
    <main className="fleet-grid">{robots.length ? robots.map(robot => <RobotCard key={robot.id} robot={robot} gateway={gateway} {...(views[robot.id] || blank())} onRefresh={() => loadRobot(robot)} onLive={() => connectRobot(robot)} />) : <div className="card empty"><b>No enabled robots configured</b><span>The gateway registry returned no enabled robots.</span></div>}</main>
    <footer>Reference implementation · values and evidence are reported by the gateway · OPC UA semantics remain server-owned</footer>
  </div>;
}
