import { useState } from 'react';
import { CallResponse } from '../api/types';
import * as api from '../api/gatewayClient';
import StatusBadge from './StatusBadge';

export default function CommandPanel({
  base,
  onDone,
}: {
  base: string;
  onDone: () => void;
}) {
  const [name, setName] = useState('axis-range-demo');
  const [result, setResult] = useState<CallResponse>();
  const [busy, setBusy] = useState('');

  const run = async (id: string, fn: () => Promise<CallResponse>) => {
    setBusy(id);

    try {
      setResult(await fn());
      onDone();
    } catch (e) {
      setResult({
        target: 'Gateway request',
        success: false,
        callStatusCode: e instanceof Error ? e.message : String(e),
        outputArguments: [],
      });
    } finally {
      setBusy('');
    }
  };

  const system: [string, string, () => Promise<CallResponse>][] = [
    ['ready', 'Get Ready', () => api.callSystemGetReady(base)],
    ['start', 'Start', () => api.callSystemStart(base)],
    ['stop', 'Stop', () => api.callSystemStop(base)],
    ['standdown', 'Stand Down', () => api.callSystemStandDown(base)],
  ];

  const task: [string, string, () => Promise<CallResponse>][] = [
    ['taskstart', 'Start Task', () => api.callTaskStart(base)],
    ['taskstop', 'Stop Task', () => api.callTaskStop(base)],
    ['reset', 'Reset To Program Start', () => api.callTaskResetToProgramStart(base)],
    ['unload', 'Unload Program', () => api.callTaskUnloadProgram(base)],
  ];

  const group = (title: string, items: typeof system) => (
    <div className="command-group">
      {title && <h3>{title}</h3>}

      <div className="command-grid">
        {items.map(([id, text, fn]) => (
          <button key={id} disabled={!!busy} onClick={() => run(id, fn)}>
            {busy === id && <span className="spinner" />}
            {text}
          </button>
        ))}
      </div>
    </div>
  );

  return (
    <section className="card commands">
      <div className="section-title">
        <span className="panel-icon">▣</span>
        Commands
        <small>RUNTIME METHOD METADATA VALIDATED</small>
      </div>

      {group('SystemOperation', system)}

      <div className="task-load">
        <h3>TaskControlOperation</h3>

        <div className="load-row">
          <input
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Program name"
          />

          <button disabled={!!busy} onClick={() => run('load', () => api.callTaskLoadByName(base, name))}>
            {busy === 'load' && <span className="spinner" />}
            Load Program
          </button>
        </div>

        {group('', task)}
      </div>

      {result && (
        <div
          className={`result-card ${
            result.success && /^Good/i.test(result.callStatusCode || '')
              ? 'result-good'
              : 'result-bad'
          }`}
        >
          <div className="result-head">
            <b>{result.success ? 'Call completed' : 'Call returned a non-Good result'}</b>
            <StatusBadge value={result.callStatusCode ?? undefined} />
          </div>

          <div className="result-meta">Target · {result.target}</div>

          <details>
            <summary>Technical details</summary>
            <pre>
              {JSON.stringify(
                {
                  objectId: (result as any).objectId,
                  methodId: (result as any).methodId,
                  outputArguments: result.outputArguments,
                  warnings: result.warnings,
                },
                null,
                2,
              )}
            </pre>
          </details>
        </div>
      )}
    </section>
  );
}
