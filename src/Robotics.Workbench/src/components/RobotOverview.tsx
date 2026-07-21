import { Discovery, Snapshot, Robot } from '../api/types';
import Robot3DViewport from '../robot3d/Robot3DViewport';
import RobotIdentity from './RobotIdentity';
import StatusBadge from './StatusBadge';
import type { SelectedRuntimeValue } from '../selectors/runtimeValueSelectors';

type Props = { robot: Robot; discovery?: Discovery; snapshot?: Snapshot; equipment?: Snapshot; axes: import('../api/types').DiscoveryNode[]; live: string; states: { system: SelectedRuntimeValue; task: SelectedRuntimeValue; control: SelectedRuntimeValue; mode: SelectedRuntimeValue }; onRefresh: () => void; onLive: () => void };

const SummaryCard = ({ label, selected }: { label: string; selected: SelectedRuntimeValue }) => <div className="kpi"><small>{label}</small><strong>{selected.value?.valueText || 'Unavailable'}</strong><StatusBadge value={selected.value?.statusCode} /></div>;

export default function RobotOverview({ robot, discovery, snapshot, equipment, axes, live, states, onRefresh, onLive }: Props) {
  return <section className="robot-overview">
    <Robot3DViewport axes={axes} snapshots={[snapshot, equipment]} live={live} imageUrl={robot.imageUrl}/>
    <div className="robot-overview-info">
      <div className="robot-overview-heading"><div><span className="eyebrow">ROBOT / {robot.id}</span><h2>{robot.displayName}</h2><RobotIdentity discovery={discovery} snapshot={snapshot} equipment={equipment}/></div><div className="robot-card-actions"><button aria-label="Refresh robot" onClick={onRefresh}>↻</button><button className="live-button" onClick={onLive}><span className={`status-dot ${live}`}/>{live === 'connected' ? 'Live' : 'Connect live'}</button></div></div>
      <div className="robot-kpis"><SummaryCard label="System operation" selected={states.system}/><SummaryCard label="Task control" selected={states.task}/><SummaryCard label="Control" selected={states.control}/><SummaryCard label="Mode" selected={states.mode}/></div>
    </div>
  </section>;
}
