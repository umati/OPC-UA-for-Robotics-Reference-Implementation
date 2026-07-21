import { DiscoveryNode, Snapshot, SnapshotValue } from '../api/types';
import { JointMapping, Robot3DInput, Robot3DState, UnitKind, VisualJoint, visualJoints } from './types';

const axisNames: Record<VisualJoint, string> = { S: 'SAxis', L: 'LAxis', U: 'UAxis', R: 'RAxis', B: 'BAxis', T: 'TAxis' };
const numberFromText = (text?: string) => {
  const match = text?.match(/[-+]?\d+(?:\.\d+)?(?:[eE][-+]?\d+)?/);
  return match ? Number(match[0]) : undefined;
};

export function localBrowseName(browseName: string): string {
  const qualified = browseName.match(/^\d+:(.+)$/);
  return qualified ? qualified[1] : browseName;
}

export function convertPosition(value: number, engineeringUnits?: string | null, metadata?: SnapshotValue['engineeringUnit']): { unit: UnitKind; radians?: number } {
  const unit = [metadata?.displayName, metadata?.description, engineeringUnits].filter(Boolean).join(' ').toLowerCase();
  if (/radian|\brad\b/.test(unit)) return { unit: 'radians', radians: value };
  if (/degree|\bdeg\b|°/.test(unit)) return { unit: 'degrees', radians: value * Math.PI / 180 };
  return { unit: engineeringUnits ? 'unsupported' : 'missing' };
}

function values(snapshots: (Snapshot | undefined)[]) { return snapshots.flatMap(snapshot => snapshot?.sections.flatMap(section => section.values) || []); }
function positionFor(axis: DiscoveryNode, snapshots: (Snapshot | undefined)[]): SnapshotValue | undefined {
  const expectedAxisName = localBrowseName(axis.browseName);
  const suffix = '.ParameterSet.ActualPosition';
  const matches = values(snapshots).filter(value => {
    if (localBrowseName(value.browseName) !== 'ActualPosition' || !value.label.endsWith(suffix)) return false;
    const axesMarker = '.Axes.';
    const axesIndex = value.label.lastIndexOf(axesMarker);
    if (axesIndex < 0) return false;
    const labelAxisName = value.label.slice(axesIndex + axesMarker.length, -suffix.length);
    return localBrowseName(labelAxisName) === expectedAxisName;
  });
  return matches.length === 1 ? matches[0] : undefined;
}
export function isGoodStatus(status?: string) {
  const value = status?.trim() || '';
  if (/^Good(?:$|[/\s:])/i.test(value) || ['GoodClamped', 'GoodLocalOverride', 'GoodSubNormal'].includes(value)) return true;
  if (/^0x[0-9a-f]+$/i.test(value)) return Number.parseInt(value.slice(2), 16) === 0;
  return /^\d+$/.test(value) && Number(value) === 0;
}

export function mapRobotAxes(input: Robot3DInput): Robot3DState {
  const names = new Map<string, DiscoveryNode[]>();
  input.axes.forEach(axis => {
    const name = localBrowseName(axis.browseName);
    names.set(name, [...(names.get(name) || []), axis]);
  });
  const mappingIssues = input.axes.length < 6 ? `Only ${input.axes.length} AxisType instances were discovered; six are required.` : input.axes.length > 6 ? `${input.axes.length} AxisType instances were discovered; the visual mapping is intentionally not guessed.` : undefined;
  const joints: JointMapping[] = visualJoints.map(joint => {
    const candidates = names.get(axisNames[joint]) || [];
    const axis = candidates[0];
    const position = candidates.length === 1 && axis ? positionFor(axis, input.snapshots) : undefined;
    if (candidates.length > 1) return { joint, axis, status: 'ambiguous', unit: 'missing', freshness: 'unavailable', evidence: `Multiple runtime axes were discovered with the normalized name ${axisNames[joint]}: ${candidates.map(candidate => candidate.browseName).join(', ')}`, reason: 'A duplicate reference axis cannot be assigned to this visual joint without a manifest.' };
    if (mappingIssues) return { joint, axis, position, status: 'unavailable', unit: 'missing', freshness: 'unavailable', evidence: `Typed AxisType ownership; expected ${axisNames[joint]}`, reason: mappingIssues };
    if (!axis) return { joint, status: 'ambiguous', unit: 'missing', freshness: 'unavailable', evidence: `No uniquely identified runtime axis named ${axisNames[joint]}`, reason: 'A generic vendor axis cannot be assigned to this visual joint without a manifest.' };
    if (!position) return { joint, axis, status: 'unavailable', unit: 'missing', freshness: 'unavailable', evidence: `Axis ${axis.nodeId} (${axis.browseName}) owns ActualPosition by the existing label path`, reason: 'ActualPosition was not present in the robot-scoped snapshot.' };
    const raw = numberFromText(position.valueText);
    const converted = raw === undefined ? { unit: 'unsupported' as UnitKind } : convertPosition(raw, position.engineeringUnits, position.engineeringUnit);
    const lastGoodRaw = numberFromText(position.lastGoodValueText);
    const lastGoodConverted = lastGoodRaw === undefined ? { unit: converted.unit as UnitKind } : convertPosition(lastGoodRaw, position.engineeringUnits, position.engineeringUnit);
    const lastGoodAt = position.lastGoodUpdatedAt;
    // A data-change timestamp is motion/value history, not a subscription heartbeat.
    // With no explicit health signal, an open socket is the strongest available
    // evidence that an unchanged but valid value is still usable.
    const freshness = input.live === 'disconnected' || input.live === 'error' ? 'disconnected' : input.live === 'stale' || input.streamHealth === 'stale' ? 'stale' : isGoodStatus(position.statusCode) && raw !== undefined && converted.radians !== undefined ? 'live' : 'unavailable';
    if (!isGoodStatus(position.statusCode) || raw === undefined || converted.radians === undefined) {
      return { joint, axis, position, status: converted.unit === 'unsupported' || converted.unit === 'missing' ? 'unsupported' : 'mapped', unit: converted.unit, rawValue: raw, renderRadians: isGoodStatus(position.statusCode) ? converted.radians : lastGoodConverted.radians, lastGoodAt, freshness, evidence: `Axis ${axis.nodeId} (${axis.browseName}); ActualPosition ${position.nodeId}; ${position.discovery} discovery`, reason: !isGoodStatus(position.statusCode) ? `Latest StatusCode is ${position.statusCode}; retaining the last Good pose.` : 'Position is not a finite numeric value or has unsupported EngineeringUnits.' };
    }
    return { joint, axis, position, status: 'mapped', unit: converted.unit, rawValue: raw, renderRadians: converted.radians, lastGoodAt, freshness, evidence: `Axis ${axis.nodeId} (${axis.browseName}); ActualPosition ${position.nodeId}; ${position.discovery} discovery`, };
  });
  const mappingStatus = joints.every(joint => joint.status === 'mapped') ? 'mapped' : joints.some(joint => joint.status === 'ambiguous') ? 'ambiguous' : joints.some(joint => joint.status === 'unsupported') ? 'unsupported' : 'unavailable';
  const freshness: Robot3DState['freshness'] = input.live === 'disconnected' || input.live === 'error' ? 'disconnected' : input.live === 'stale' || joints.some(joint => joint.freshness === 'stale') ? 'stale' : joints.every(joint => joint.freshness === 'live') ? 'live' : 'unavailable';
  return { mappingStatus, freshness, joints, explanation: mappingStatus === 'mapped' ? 'Generic operational visualization; not safety-rated and not an exact vendor digital twin.' : joints.find(joint => joint.reason)?.reason || 'Live six-axis mapping is not currently available.', source: 'robot-scoped snapshot and WebSocket dataChange stream' };
}

export function interpolatePose(previous: number[], target: number[], elapsedMs: number, durationMs = 240): number[] {
  const amount = Math.max(0, Math.min(1, elapsedMs / durationMs));
  return target.map((value, index) => previous[index] + (value - previous[index]) * amount);
}
