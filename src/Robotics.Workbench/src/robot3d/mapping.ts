import { Robot3DInput, Robot3DState, JointMapping, VisualJoint, visualJoints } from './types';
import { resolveReferenceServerVisualProfile } from './referenceServerSixAxisProfile';
export { convertPosition, isGoodStatus, localBrowseName } from './units';

export function resolveVisualJoints(input: Robot3DInput) {
  return resolveReferenceServerVisualProfile({ ...input, inventory: input.inventory || input.motionInventory });
}

export function mapRobotAxes(input: Robot3DInput): Robot3DState {
  const result = resolveVisualJoints(input);
  const byId = new Map(result.joints.map(joint => [joint.visualJointId, joint]));
  const joints: JointMapping[] = visualJoints.map((joint: VisualJoint) => {
    const resolved = byId.get(joint);
    return resolved ? { ...resolved, joint, axis: resolved.sourceAxis, position: resolved.actualPosition, status: result.status, evidence: resolved.actualPosition ? `Resolved by ${result.profile.profileId}; source Axis ${resolved.sourceAxisKey}` : 'Resolved source Axis has no ActualPosition snapshot value.', unit: resolved.conversion.unit, lastGoodAt: resolved.actualPosition?.lastGoodUpdatedAt, reason: result.diagnostics.find(item => item.visualJointId === joint)?.message } : { joint, visualJointId: joint, profileId: result.profile.profileId, sourceMotionDeviceKey: '', sourceAxisKey: '', sourceAxis: { browseName: '', displayName: '', nodeId: '', typeDefinition: '', evidence: '' }, requiredUnitCategory: 'angular', conversion: { unit: 'missing', scale: 1 }, direction: 1, zeroOffsetRadians: 0, visualScale: 1, status: result.status, evidence: result.diagnostics.find(item => item.visualJointId === joint)?.message || 'No resolved source Axis.', unit: 'missing', freshness: 'unavailable', diagnosticState: 'invalid', reason: result.diagnostics.find(item => item.visualJointId === joint)?.message };
  });
  const freshness = input.live === 'disconnected' || input.live === 'error' ? 'disconnected' : input.live === 'stale' || input.streamHealth === 'stale' ? 'stale' : joints.every(joint => joint.freshness === 'live') ? 'live' : 'unavailable';
  return { mappingStatus: result.status, freshness, joints, explanation: result.status === 'matched' ? 'Generic operational visualization; not safety-rated and not an exact vendor digital twin.' : result.diagnostics.find(item => item.severity !== 'informational')?.message || 'No compatible visual profile is configured or resolved.', source: 'robot-scoped snapshot and WebSocket dataChange stream' };
}

export function interpolatePose(previous: number[], target: number[], elapsedMs: number, durationMs = 240): number[] { const amount = Math.max(0, Math.min(1, elapsedMs / durationMs)); return target.map((value, index) => previous[index] + (value - previous[index]) * amount); }
