import { DiscoveryNode, MotionInventory, MotionInventoryAxis, Snapshot, SnapshotValue } from '../api/types';
import { RobotVisualProfile, ProfileResolutionDiagnostic, ProfileResolutionResult, ResolvedVisualJoint, VisualJoint, visualJoints } from './types';
import { convertPosition, isGoodStatus, localBrowseName } from './units';

// Local Workbench calibration and selectors for the reference server only. These names are not OPC UA Robotics semantics.
export const referenceServerSixAxisProfile: RobotVisualProfile = Object.freeze({
  profileId: 'reference-server-six-axis-v1', displayName: 'Reference server six-axis visual profile',
  joints: Object.freeze([
    { visualJointId: 'base', axis: { browseName: 'SAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
    { visualJointId: 'shoulder', axis: { browseName: 'LAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
    { visualJointId: 'elbow', axis: { browseName: 'UAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
    { visualJointId: 'wrist1', axis: { browseName: 'RAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
    { visualJointId: 'wrist2', axis: { browseName: 'BAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
    { visualJointId: 'wrist3', axis: { browseName: 'TAxis', expectedMotionDevice: 'single-device-scope', required: true, unit: 'angular' } },
  ]),
}) as RobotVisualProfile;

const values = (snapshots: (Snapshot | undefined)[]) => snapshots.flatMap(snapshot => snapshot?.sections.flatMap(section => section.values) || []);
const axisEntries = (inventory: MotionInventory | null | undefined, fallback: DiscoveryNode[]): MotionInventoryAxis[] => inventory?.motionDeviceSystems.flatMap(system => system.motionDevices.flatMap(device => device.axes)) || fallback.map(axis => ({ axis, motionDeviceKey: axis.stableKey || 'discovered-motion-device', stableKey: axis.stableKey || axis.nodeId, diagnostics: [] }));
const positionFor = (axis: MotionInventoryAxis, snapshots: (Snapshot | undefined)[]): SnapshotValue | undefined => {
  const matches = values(snapshots).filter(value => value.axisKey === axis.stableKey || value.stableKey === axis.stableKey).filter(value => localBrowseName(value.browseName) === 'ActualPosition');
  return matches.length === 1 ? matches[0] : undefined;
};
const numberFromText = (text?: string) => { const match = text?.match(/[-+]?\d+(?:\.\d+)?(?:[eE][-+]?\d+)?/); return match ? Number(match[0]) : undefined; };

export function resolveReferenceServerVisualProfile(input: { inventory?: MotionInventory | null; axes: DiscoveryNode[]; snapshots: (Snapshot | undefined)[]; live: string; streamHealth?: 'healthy' | 'stale' }): ProfileResolutionResult {
  const entries = axisEntries(input.inventory, input.axes);
  const devices = new Map<string, MotionInventoryAxis[]>(); entries.forEach(entry => devices.set(entry.motionDeviceKey, [...(devices.get(entry.motionDeviceKey) || []), entry]));
  const duplicateDevice = [...devices.values()].find(axes => referenceServerSixAxisProfile.joints.some(definition => axes.filter(axis => localBrowseName(axis.axis.browseName) === definition.axis.browseName).length > 1));
  if (duplicateDevice) { const duplicate = referenceServerSixAxisProfile.joints.find(definition => duplicateDevice.filter(axis => localBrowseName(axis.axis.browseName) === definition.axis.browseName).length > 1); return { status: 'ambiguous', profile: referenceServerSixAxisProfile, joints: [], diagnostics: [{ code: 'duplicate-candidate-axis', severity: 'error', message: `Multiple candidates exist for ${duplicate?.axis.browseName || 'a required visual binding'}.` }], additionalUnboundAxisKeys: duplicateDevice.map(axis => axis.stableKey) }; }
  const candidates = [...devices.entries()].filter(([, axes]) => referenceServerSixAxisProfile.joints.every(definition => axes.filter(axis => localBrowseName(axis.axis.browseName) === definition.axis.browseName).length === 1));
  const diagnostics: ProfileResolutionDiagnostic[] = [];
  if (candidates.length === 0) { diagnostics.push({ code: 'profile-not-applicable', severity: 'informational', message: 'reference-server-six-axis-v1 is not applicable to this motion inventory.' }); return { status: 'notMatched', profile: referenceServerSixAxisProfile, joints: [], diagnostics, additionalUnboundAxisKeys: entries.map(entry => entry.stableKey) }; }
  if (candidates.length > 1) { diagnostics.push({ code: 'ambiguous-motion-device-scope', severity: 'error', message: 'More than one MotionDevice contains the complete reference binding set.' }); return { status: 'ambiguous', profile: referenceServerSixAxisProfile, joints: [], diagnostics, additionalUnboundAxisKeys: [] }; }
  const [deviceKey, deviceAxes] = candidates[0];
  const used = new Set<string>(); const joints: ResolvedVisualJoint[] = [];
  for (const definition of referenceServerSixAxisProfile.joints) {
    const matches = deviceAxes.filter(axis => localBrowseName(axis.axis.browseName) === definition.axis.browseName);
    if (matches.length !== 1) { diagnostics.push({ code: matches.length ? 'duplicate-candidate-axis' : 'required-axis-not-found', severity: 'error', message: `${definition.axis.browseName} has ${matches.length} candidates in MotionDevice ${deviceKey}.`, visualJointId: definition.visualJointId }); continue; }
    const entry = matches[0]; if (used.has(entry.stableKey)) { diagnostics.push({ code: 'axis-bound-multiple-times', severity: 'error', message: `Axis ${entry.stableKey} would be bound more than once.`, visualJointId: definition.visualJointId, axisKey: entry.stableKey }); continue; } used.add(entry.stableKey);
    const position = entry.actualPosition || positionFor(entry, input.snapshots); const raw = numberFromText(position?.valueText); const conversion = raw === undefined ? { unit: 'missing' as const } : convertPosition(raw, position?.engineeringUnits, position?.engineeringUnit);
    if (!position) diagnostics.push({ code: 'source-value-unavailable', severity: 'warning', message: `ActualPosition is unavailable for ${definition.axis.browseName}.`, visualJointId: definition.visualJointId, axisKey: entry.stableKey });
    else if (!position.engineeringUnits && !position.engineeringUnit) diagnostics.push({ code: 'missing-engineering-unit', severity: 'warning', message: `Engineering-unit metadata is missing for ${definition.axis.browseName}; no unit is guessed.`, visualJointId: definition.visualJointId, axisKey: entry.stableKey });
    else if (conversion.unit === 'unsupported') diagnostics.push({ code: 'incompatible-engineering-unit', severity: 'error', message: `Engineering-unit metadata for ${definition.axis.browseName} is not angular.`, visualJointId: definition.visualJointId, axisKey: entry.stableKey });
    if (position && !isGoodStatus(position.statusCode)) diagnostics.push({ code: 'source-value-non-good', severity: 'warning', message: `${definition.axis.browseName} has StatusCode ${position.statusCode}.`, visualJointId: definition.visualJointId, axisKey: entry.stableKey });
    const good = !!position && isGoodStatus(position.statusCode) && conversion.radians !== undefined;
    const lastGoodRaw = numberFromText(position?.lastGoodValueText); const lastGood = lastGoodRaw === undefined ? undefined : convertPosition(lastGoodRaw, position?.engineeringUnits, position?.engineeringUnit).radians;
    joints.push({ visualJointId: definition.visualJointId, profileId: referenceServerSixAxisProfile.profileId, sourceMotionDeviceKey: deviceKey, sourceAxisKey: entry.stableKey, sourceAxis: entry.axis, requiredUnitCategory: 'angular', conversion: { unit: conversion.unit, scale: 1 }, rawValue: raw, convertedVisualValue: good ? conversion.radians : undefined, lastGoodConvertedValue: lastGood, statusCode: position?.statusCode, sourceTimestamp: position?.sourceTimestamp, serverTimestamp: position?.serverTimestamp, freshness: input.live === 'disconnected' || input.live === 'error' ? 'disconnected' : input.live === 'stale' || input.streamHealth === 'stale' ? 'stale' : good ? 'live' : 'unavailable', diagnosticState: good ? 'bound' : position && !isGoodStatus(position.statusCode) ? 'sourceNonGood' : position ? 'invalid' : 'sourceUnavailable', actualPosition: position });
  }
  const extra = entries.filter(entry => entry.motionDeviceKey !== deviceKey || !used.has(entry.stableKey)).map(entry => entry.stableKey); if (extra.length) diagnostics.push({ code: 'additional-unbound-axis', severity: 'informational', message: `${extra.length} discovered Axis value(s) remain available but unbound to this visual profile.` });
  if (joints.length !== 6 || diagnostics.some(item => item.severity === 'error')) return { status: diagnostics.some(item => item.code.includes('duplicate') || item.code.includes('multiple')) ? 'ambiguous' : 'invalid', profile: referenceServerSixAxisProfile, joints, diagnostics, additionalUnboundAxisKeys: extra };
  diagnostics.push({ code: 'profile-matched', severity: 'informational', message: 'reference-server-six-axis-v1 matched the complete local reference binding set.' }); return { status: 'matched', profile: referenceServerSixAxisProfile, joints, diagnostics, additionalUnboundAxisKeys: extra };
}

