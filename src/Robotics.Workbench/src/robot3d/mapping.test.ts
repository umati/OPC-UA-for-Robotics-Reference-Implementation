import { describe, expect, it } from 'vitest';
import type { DiscoveryNode, Snapshot, SnapshotValue } from '../api/types';
import { convertPosition, interpolatePose, localBrowseName, mapRobotAxes } from './mapping';

const axis = (browseName: string): DiscoveryNode => ({
  browseName,
  displayName: browseName,
  nodeId: `ns=99;s=${browseName}`,
  typeDefinition: 'AxisType',
  evidence: 'typed AxisType child below MotionDevice/Axes',
});

const referenceAxisNames = ['SAxis', 'LAxis', 'UAxis', 'RAxis', 'BAxis', 'TAxis'];

const snapshot = (axisNames: string[], options: {
  statusCode?: string;
  valueText?: string;
  engineeringUnits?: string | null;
  engineeringUnit?: SnapshotValue['engineeringUnit'];
  lastGoodValueText?: string;
  lastGoodUpdatedAt?: number;
  propertyBrowseName?: string;
  qualifiedLabel?: boolean;
  nodeIdFor?: (axisName: string) => string;
  discovery?: string;
} = {}): Snapshot => {
  const statusCode = options.statusCode ?? 'Good';
  const valueText = options.valueText ?? '10';
  return {
    connected: true,
    endpointUrl: 'opc.tcp://fixture',
    generatedAtUtc: new Date().toISOString(),
    sections: [{
      name: 'Axes',
      values: axisNames.map(name => ({
        label: `${options.qualifiedLabel ? '5:SixAxisRobot' : 'SixAxisRobot'}.Axes.${options.qualifiedLabel ? (name.match(/^\d+:/) ? name : `5:${name}`) : name}.ParameterSet.ActualPosition`,
        browseName: options.propertyBrowseName ?? 'ActualPosition',
        nodeId: options.nodeIdFor?.(name) ?? `ns=5;s=${name}/ActualPosition`,
        statusCode,
        valueText,
        discovery: options.discovery ?? 'heuristic',
        engineeringUnits: options.engineeringUnits === undefined ? 'deg' : options.engineeringUnits,
        engineeringUnit: options.engineeringUnit,
        lastGoodValueText: options.lastGoodValueText,
        lastGoodUpdatedAt: options.lastGoodUpdatedAt,
      })),
    }],
    warnings: [],
  };
};

const mappedInput = (options: Parameters<typeof snapshot>[1] = {}, now = 10_000) => ({
  axes: referenceAxisNames.map(axis),
  snapshots: [snapshot(referenceAxisNames, options)],
  live: 'connected',
  now,
});

describe('qualified BrowseName handling', () => {
  it('keeps an unqualified local name unchanged', () => {
    expect(localBrowseName('SAxis')).toBe('SAxis');
  });

  it('removes a one-digit namespace index prefix', () => {
    expect(localBrowseName('5:SAxis')).toBe('SAxis');
  });

  it('removes a multi-digit namespace index prefix', () => {
    expect(localBrowseName('12:LAxis')).toBe('LAxis');
  });

  it('does not strip an arbitrary colon from non-qualified text', () => {
    expect(localBrowseName('vendor:SAxis')).toBe('vendor:SAxis');
  });

  it('maps six qualified reference axes to labels using local names', () => {
    const qualifiedNames = referenceAxisNames.map(name => `5:${name}`);
    const state = mapRobotAxes({
      axes: qualifiedNames.map(axis),
      snapshots: [snapshot(qualifiedNames, {
        propertyBrowseName: '4:ActualPosition',
        qualifiedLabel: true,
        nodeIdFor: name => `ns=77;s=runtime-specific/${name}/actual-position`,
        discovery: 'standard',
        lastGoodValueText: '10',
        lastGoodUpdatedAt: 10_000,
      })],
      live: 'connected',
      now: 10_000,
    });

    expect(state.mappingStatus).toBe('mapped');
    expect(state.joints).toHaveLength(6);
    expect(state.joints.map(joint => joint.axis?.browseName)).toEqual(qualifiedNames);
    expect(state.joints[0].position?.label).toBe('5:SixAxisRobot.Axes.5:SAxis.ParameterSet.ActualPosition');
    expect(state.joints[0].position?.browseName).toBe('4:ActualPosition');
    expect(state.joints[0].position?.nodeId).toBe('ns=77;s=runtime-specific/5:SAxis/actual-position');
  });

  it.each(['ActualPosition', '4:ActualPosition', '12:ActualPosition'])('matches property BrowseName %s by local name', propertyBrowseName => {
    const state = mapRobotAxes(mappedInput({ propertyBrowseName }));
    expect(state.mappingStatus).toBe('mapped');
    expect(state.joints[0].position?.browseName).toBe(propertyBrowseName);
  });

  it('maps a verified numeric Good status without treating arbitrary numeric codes as Good', () => {
    expect(mapRobotAxes(mappedInput({ statusCode: '0' })).mappingStatus).toBe('mapped');
    expect(mapRobotAxes(mappedInput({ statusCode: '0x00000000' })).mappingStatus).toBe('mapped');
    expect(mapRobotAxes(mappedInput({ statusCode: '2150760448' })).joints[0].renderRadians).toBeUndefined();
  });

  it('reports duplicate normalized names as ambiguous', () => {
    const names = ['5:SAxis', '7:SAxis', '5:LAxis', '5:UAxis', '5:RAxis', '5:BAxis', '5:TAxis'];
    const state = mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(referenceAxisNames)], live: 'connected' });

    expect(state.mappingStatus).toBe('ambiguous');
    expect(state.joints.find(joint => joint.joint === 'S')?.reason).toContain('duplicate reference axis');
  });

  it('does not map unrelated vendor names by browse order', () => {
    const names = ['Axis1', 'Axis2', 'Axis3', 'Axis4', 'Axis5', 'Axis6'];
    const state = mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(names)], live: 'connected' });

    expect(state.mappingStatus).toBe('ambiguous');
    expect(state.joints.every(joint => joint.renderRadians === undefined)).toBe(true);
  });

  it('maps reversed discovery order by normalized identity, not browse order', () => {
    const names = [...referenceAxisNames].reverse();
    const state = mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(names)], live: 'connected' });

    expect(state.mappingStatus).toBe('mapped');
    expect(state.joints.map(joint => joint.axis?.browseName)).toEqual(referenceAxisNames);
  });
});

describe('robot 3D unit conversion', () => {
  it('converts degrees to renderer radians', () => {
    expect(convertPosition(180, 'deg')).toEqual({ unit: 'degrees', radians: Math.PI });
  });

  it('keeps radians unchanged', () => {
    expect(convertPosition(Math.PI, 'radian')).toEqual({ unit: 'radians', radians: Math.PI });
  });

  it('accepts decoded degree metadata even when the legacy text is unavailable', () => {
    expect(convertPosition(180, null, { displayName: '°', description: 'degree [unit of angle]', unitId: 17476 })).toEqual({ unit: 'degrees', radians: Math.PI });
    expect(mapRobotAxes(mappedInput({ engineeringUnits: null, engineeringUnit: { displayName: '°', description: 'degree [unit of angle]', unitId: 17476 } })).mappingStatus).toBe('mapped');
  });

  it('accepts decoded radian metadata', () => {
    expect(convertPosition(Math.PI, null, { displayName: 'rad', description: 'radian', unitId: 0 })).toEqual({ unit: 'radians', radians: Math.PI });
  });

  it('rejects unsupported units instead of assuming degrees', () => {
    expect(convertPosition(1, 'millimetre')).toEqual({ unit: 'unsupported' });
    expect(mapRobotAxes(mappedInput({ engineeringUnits: 'linear' })).mappingStatus).toBe('unsupported');
    expect(mapRobotAxes(mappedInput({ engineeringUnits: 'ExtensionObject(TypeId=i=887, Body=EUInformation)' })).mappingStatus).toBe('unsupported');
    expect(convertPosition(1, null, { displayName: 'widget', description: 'vendor widget unit', unitId: 77 })).toEqual({ unit: 'unsupported' });
  });

  it('handles missing units according to the proof-of-concept policy', () => {
    expect(convertPosition(1, null)).toEqual({ unit: 'missing' });
    const state = mapRobotAxes(mappedInput({ engineeringUnits: null }));
    expect(state.mappingStatus).toBe('unsupported');
    expect(state.joints[0].position?.engineeringUnits).toBeNull();
  });
});

describe('robot 3D sample quality and freshness', () => {
  it('does not update the visual pose for a Bad StatusCode sample', () => {
    const baseline = mapRobotAxes(mappedInput({ valueText: '10', lastGoodValueText: '10' }));
    const bad = mapRobotAxes(mappedInput({ statusCode: 'BadWaitingForInitialData', valueText: '20', lastGoodValueText: '10' }));

    expect(bad.joints[0].position?.statusCode).toBe('BadWaitingForInitialData');
    expect(bad.joints[0].renderRadians).toBe(baseline.joints[0].renderRadians);
  });

  it('does not treat Uncertain as Good', () => {
    const uncertain = mapRobotAxes(mappedInput({ statusCode: 'Uncertain', valueText: '20', lastGoodValueText: '10' }));

    expect(uncertain.joints[0].renderRadians).toBe(convertPosition(10, 'deg').radians);
    expect(uncertain.joints[0].position?.statusCode).toBe('Uncertain');
  });

  it('does not replace a missing value with zero', () => {
    const missing = mapRobotAxes(mappedInput({ valueText: '', lastGoodValueText: undefined }));

    expect(missing.joints[0].rawValue).toBeUndefined();
    expect(missing.joints[0].renderRadians).toBeUndefined();
  });

  it('leaves renderRadians undefined for a Bad sample without prior Good evidence', () => {
    const bad = mapRobotAxes(mappedInput({ statusCode: 'Bad', valueText: '20' }));
    expect(bad.joints[0].renderRadians).toBeUndefined();
  });

  it('keeps the heuristic unavailable runtime value non-animating', () => {
    const qualifiedNames = referenceAxisNames.map(name => `5:${name}`);
    const state = mapRobotAxes({
      axes: qualifiedNames.map(axis),
      snapshots: [snapshot(qualifiedNames, {
        qualifiedLabel: true,
        propertyBrowseName: '4:ActualPosition',
        statusCode: '2150760448',
        valueText: '<not available>',
        engineeringUnits: null,
        discovery: 'heuristic',
      })],
      live: 'connected',
    });
    expect(state.joints[0].position?.label).toBe('5:SixAxisRobot.Axes.5:SAxis.ParameterSet.ActualPosition');
    expect(state.joints[0].position?.browseName).toBe('4:ActualPosition');
    expect(state.joints[0].position?.statusCode).toBe('2150760448');
    expect(state.mappingStatus).not.toBe('mapped');
    expect(state.joints[0].status).toBe('unsupported');
    expect(state.joints[0].renderRadians).toBeUndefined();
  });

  it('does not select an ambiguous ActualPosition candidate', () => {
    const state = mapRobotAxes({
      ...mappedInput(),
      snapshots: [snapshot(referenceAxisNames), snapshot(referenceAxisNames)],
    });
    expect(state.joints[0].status).toBe('unavailable');
    expect(state.joints[0].renderRadians).toBeUndefined();
  });

  it('transitions to stale after the freshness bound', () => {
    expect(mapRobotAxes(mappedInput({ lastGoodUpdatedAt: 6_999 }, 10_000)).freshness).toBe('stale');
  });

  it('transitions to disconnected when the live stream is disconnected', () => {
    expect(mapRobotAxes({ ...mappedInput(), live: 'disconnected' }).freshness).toBe('disconnected');
  });
});

describe('robot 3D reference-axis mapping', () => {
  it('rejects fewer than six axes', () => {
    const names = referenceAxisNames.slice(0, 5);
    expect(mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(names)], live: 'connected' }).mappingStatus).toBe('unavailable');
  });

  it('rejects more than six axes', () => {
    const names = [...referenceAxisNames, 'ExtraAxis'];
    expect(mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(referenceAxisNames)], live: 'connected' }).mappingStatus).toBe('unavailable');
  });

  it('rejects duplicate reference axes as ambiguous', () => {
    const names = [...referenceAxisNames.slice(0, 5), 'SAxis'];
    const state = mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(names)], live: 'connected' });
    expect(state.mappingStatus).toBe('ambiguous');
    expect(state.joints.find(joint => joint.joint === 'S')?.reason).toContain('duplicate reference axis');
  });

  it('rejects an ambiguous generic axis mapping', () => {
    const names = referenceAxisNames.map(name => name === 'SAxis' ? 'Axis1' : name);
    expect(mapRobotAxes({ axes: names.map(axis), snapshots: [snapshot(names)], live: 'connected' }).mappingStatus).toBe('ambiguous');
  });
});

describe('robot 3D presentation interpolation', () => {
  it('interpolates between genuine samples within the bounded window', () => {
    expect(interpolatePose([0], [10], 120, 240)).toEqual([5]);
  });

  it('does not extrapolate indefinitely beyond the bounded window', () => {
    expect(interpolatePose([0], [10], 999, 240)).toEqual([10]);
  });
});

describe('robot 3D state isolation', () => {
  it('keeps independent robot mappings isolated', () => {
    const robotA = mapRobotAxes(mappedInput({ valueText: '10', lastGoodValueText: '10' }));
    const robotB = mapRobotAxes({
      ...mappedInput({ statusCode: 'Bad', valueText: '20', lastGoodValueText: undefined }),
    });

    expect(robotA.joints[0].renderRadians).toBeDefined();
    expect(robotB.joints[0].renderRadians).toBeUndefined();
    expect(robotA.joints[0].renderRadians).not.toBe(robotB.joints[0].renderRadians);
  });
});
