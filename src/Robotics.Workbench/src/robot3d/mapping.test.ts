import { describe, expect, it } from 'vitest';
import type { DiscoveryNode, MotionInventory, Snapshot } from '../api/types';
import { convertPosition, interpolatePose, mapRobotAxes, resolveVisualJoints } from './mapping';
import { genericSixAxisCalibration, visualJointRotation } from './GenericSixAxisRobot';

const names = ['SAxis', 'LAxis', 'UAxis', 'RAxis', 'BAxis', 'TAxis'];
const axis = (name: string, device = 'device-a'): DiscoveryNode => ({ browseName: name, displayName: name, nodeId: `ns=9;s=${device}/${name}`, stableKey: `${device}/${name}`, typeDefinition: 'AxisType', evidence: 'typed AxisType child', namespaceUri: 'urn:local' });
const inventory = (axisNames = names, device = 'device-a'): MotionInventory => ({ robotIdentity: 'fixture', diagnostics: [], motionDeviceSystems: [{ system: axis('MotionDeviceSystem', device), motionDevices: [{ motionDevice: axis(device, device), axes: axisNames.map(name => ({ axis: axis(name, device), motionDeviceKey: device, stableKey: `${device}/${name}`, diagnostics: [] })), diagnostics: [] }] }] });
const snapshot = (axisNames = names, options: { statusCode?: string; engineeringUnits?: string | null; valueText?: string } = {}): Snapshot => ({ connected: true, endpointUrl: 'opc.tcp://fixture', generatedAtUtc: new Date().toISOString(), warnings: [], motionInventory: inventory(axisNames), sections: [{ name: 'Axes', values: axisNames.map(name => ({ label: `${name}.ParameterSet.ActualPosition`, browseName: 'ActualPosition', nodeId: `ns=9;s=${name}/ActualPosition`, axisKey: `device-a/${name}`, motionDeviceKey: 'device-a', stableKey: `device-a/${name}`, statusCode: options.statusCode || 'Good', valueText: options.valueText || '10', discovery: 'standard', engineeringUnits: options.engineeringUnits === undefined ? 'deg' : options.engineeringUnits })) }] });

describe('reference-server-six-axis-v1 resolution', () => {
  it('matches by stable ownership and visual IDs, independent of collection order', () => {
    const result = resolveVisualJoints({ inventory: inventory([...names].reverse()), axes: [], snapshots: [snapshot([...names].reverse())], live: 'connected' });
    expect(result.status).toBe('matched'); expect(result.joints.map(joint => joint.visualJointId)).toEqual(['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3']); expect(result.joints[0].sourceAxis.browseName).toBe('SAxis');
  });
  it('leaves a seventh Axis available but unbound', () => {
    const result = resolveVisualJoints({ inventory: inventory([...names, 'Axis7']), axes: [], snapshots: [snapshot([...names, 'Axis7'])], live: 'connected' });
    expect(result.status).toBe('matched'); expect(result.additionalUnboundAxisKeys).toContain('device-a/Axis7'); expect(result.diagnostics.find(item => item.code === 'additional-unbound-axis')?.severity).toBe('informational');
  });
  it('does not cross MotionDevice ownership when BrowseNames duplicate', () => {
    const second = inventory(names, 'device-b'); const first = inventory(names, 'device-a'); const combined: MotionInventory = { robotIdentity: 'fixture', diagnostics: [], motionDeviceSystems: [{ system: first.motionDeviceSystems[0].system, motionDevices: [...first.motionDeviceSystems[0].motionDevices, ...second.motionDeviceSystems[0].motionDevices] }] };
    expect(resolveVisualJoints({ inventory: combined, axes: [], snapshots: [snapshot()], live: 'connected' }).status).toBe('ambiguous');
  });
  it('reports missing required Axis safely', () => {
    const missing = resolveVisualJoints({ inventory: inventory(names.slice(0, 5)), axes: [], snapshots: [], live: 'connected' }); expect(missing.status).toBe('notMatched'); expect(missing.diagnostics[0].code).toBe('profile-not-applicable');
  });
  it('rejects incompatible units without guessing', () => {
    const result = resolveVisualJoints({ inventory: inventory(), axes: [], snapshots: [snapshot(names, { engineeringUnits: 'millimetre' })], live: 'connected' }); expect(result.status).toBe('invalid'); expect(result.joints[0].renderRadians).toBeUndefined();
  });
  it('preserves exact non-Good StatusCode and does not animate fabricated data', () => {
    const result = resolveVisualJoints({ inventory: inventory(), axes: [], snapshots: [snapshot(names, { statusCode: 'BadWaitingForInitialData', valueText: '20' })], live: 'connected' }); expect(result.joints[0].statusCode).toBe('BadWaitingForInitialData'); expect(result.joints[0].renderRadians).toBeUndefined();
  });
  it('keeps missing units raw and diagnostic', () => {
    const result = resolveVisualJoints({ inventory: inventory(), axes: [], snapshots: [snapshot(names, { engineeringUnits: null })], live: 'connected' }); expect(result.joints[0].rawValue).toBe(10); expect(result.joints[0].conversion.unit).toBe('missing'); expect(result.diagnostics.some(item => item.code === 'missing-engineering-unit')).toBe(true);
  });
  it('uses visual-joint IDs at the renderer boundary', () => {
    const state = mapRobotAxes({ axes: [], motionInventory: inventory(), snapshots: [snapshot()], live: 'connected' }); expect(state.joints.map(joint => joint.visualJointId)).toEqual(['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3']); expect(state.joints.every(joint => !['S', 'L', 'U', 'R', 'B', 'T'].includes(joint.visualJointId))).toBe(true);
  });
});

describe('reference visual calibration and interpolation', () => {
  it('preserves the current calibrated zero and direction decisions', () => {
    expect(genericSixAxisCalibration.map(item => item.joint)).toEqual(['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3']); expect(visualJointRotation(0)).toBe(0);
  });
  it('retains known degree/radian conversion and bounded interpolation', () => {
    expect(convertPosition(180, 'deg')).toEqual({ unit: 'degrees', radians: Math.PI }); expect(convertPosition(Math.PI, 'radian')).toEqual({ unit: 'radians', radians: Math.PI }); expect(interpolatePose([0], [10], 120, 240)).toEqual([5]); expect(interpolatePose([0], [10], 999, 240)).toEqual([10]);
  });
});
