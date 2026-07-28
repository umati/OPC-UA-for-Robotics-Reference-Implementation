import { describe, expect, it } from 'vitest';
import * as THREE from 'three';
import { createTopologyAssembly, applyTopologyJoint } from './topologyRenderer';
import { resolveVisualAssembly, summarizeChainQuality, validateVisualAssembly } from './assembly';
import { dualArmSharedBaseFixture, railMountedSixAxisArmFixture, robotPlusIndependentPositionerFixture } from './syntheticAssemblies';

describe('multi-chain visual assemblies', () => {
  it('validates independent roots and preserves deterministic compilation order', () => {
    const fixture = robotPlusIndependentPositionerFixture; const resolved = resolveVisualAssembly({ ...fixture.assembly, chains: [...fixture.assembly.chains].reverse(), roots: [...fixture.assembly.roots].reverse() }, fixture.topology, [...fixture.joints].reverse());
    expect(resolved.valid).toBe(true); expect(resolved.traversal.rootOrder).toEqual(['arm', 'positioner']); expect(resolved.chains.map(chain => chain.definition.chainId)).toEqual(['arm-chain', 'positioner-chain']); expect(resolved.traversal.motionDeviceToChains.get('positioner-device')).toEqual(['positioner-chain']);
  });

  it('validates a mounted child chain and rejects structural errors', () => {
    const fixture = railMountedSixAxisArmFixture; const resolved = resolveVisualAssembly(fixture.assembly, fixture.topology, fixture.joints); expect(resolved.valid).toBe(true); expect(resolved.traversal.parentAttachmentByChain.get('arm-chain')?.parentChainId).toBe('rail-chain');
    expect(validateVisualAssembly({ ...fixture.assembly, chains: [...fixture.assembly.chains, fixture.assembly.chains[0]] }, fixture.topology, fixture.joints).diagnostics.some(item => item.code === 'duplicate-chain-id')).toBe(true);
    expect(validateVisualAssembly({ ...fixture.assembly, attachments: [{ ...fixture.assembly.attachments![0], parentChainId: 'arm-chain' }] }, fixture.topology, fixture.joints).diagnostics.some(item => item.code === 'cyclic-visual-attachment')).toBe(true);
    expect(validateVisualAssembly({ ...fixture.assembly, chains: [{ ...fixture.assembly.chains[1], rootNodeId: 'missing' }, fixture.assembly.chains[0]] }, fixture.topology, fixture.joints).diagnostics.some(item => item.code === 'unknown-chain-root')).toBe(true);
  });

  it('keeps shared fixed geometry and duplicate BrowseName scopes independent', () => {
    const fixture = dualArmSharedBaseFixture; const resolved = resolveVisualAssembly(fixture.assembly, fixture.topology, fixture.joints); expect(resolved.valid).toBe(true); expect(resolved.traversal.sharedFixedNodes).toEqual(['shared-base']); expect(resolved.traversal.visualJointToChain.get('left-1')).toBe('left-arm'); expect(resolved.traversal.visualJointToChain.get('right-1')).toBe('right-arm');
    const wrongOwner = fixture.joints.map(joint => joint.visualJointId === 'right-1' ? { ...joint, sourceMotionDeviceKey: 'left-device' } : joint); expect(validateVisualAssembly(fixture.assembly, fixture.topology, wrongOwner).diagnostics.some(item => item.code === 'wrong-motion-device-ownership')).toBe(true);
  });

  it('aggregates source and parent quality without hiding exact StatusCodes', () => {
    const fixture = railMountedSixAxisArmFixture; const badRail = fixture.joints.map(joint => joint.visualJointId === 'rail' ? { ...joint, diagnosticState: 'sourceNonGood' as const, freshness: 'unavailable' as const, statusCode: 'BadWaitingForInitialData' } : joint); const resolved = resolveVisualAssembly(fixture.assembly, fixture.topology, badRail); expect(resolved.chains.find(chain => chain.definition.chainId === 'rail-chain')?.quality.quality).toBe('degraded'); expect(resolved.chains.find(chain => chain.definition.chainId === 'arm-chain')?.quality.parentPlacementDegraded).toBe(true); expect(resolved.chains.find(chain => chain.definition.chainId === 'arm-chain')?.quality.sourceNonGood).toBe(false); expect(resolved.chains.find(chain => chain.definition.chainId === 'rail-chain')?.quality.exactStatusCodes).toEqual(['BadWaitingForInitialData']);
    const optional = robotPlusIndependentPositionerFixture; const optionalBad = optional.joints.map(joint => joint.visualJointId === 'positioner-1' ? { ...joint, diagnosticState: 'sourceUnavailable' as const, freshness: 'unavailable' as const } : joint); const optionalResolved = resolveVisualAssembly(optional.assembly, optional.topology, optionalBad); expect(optionalResolved.chains.find(chain => chain.definition.chainId === 'arm-chain')?.quality.quality).toBe('resolvedHealthy'); expect(optionalResolved.chains.find(chain => chain.definition.chainId === 'positioner-chain')?.quality.renderAvailable).toBe(false);
    expect(summarizeChainQuality(optional.assembly.chains[0], [{ visualJointId: 'arm-1', motionDeviceKey: 'arm-device', status: 'resolved', joint: optional.joints[0] }]).quality).toBe('resolvedHealthy');
  });

  it('composes mounted transforms and isolates independent roots', () => {
    const rail = resolveVisualAssembly(railMountedSixAxisArmFixture.assembly, railMountedSixAxisArmFixture.topology, railMountedSixAxisArmFixture.joints); const railVisual = createTopologyAssembly(rail); applyTopologyJoint(rail.topology.traversal.jointById.get('rail')!, railVisual.dynamicJoints.get('rail')!, 2); railVisual.root.updateMatrixWorld(true); expect(railVisual.nodeGroups.get('arm-root')!.getWorldPosition(new THREE.Vector3()).x).toBeCloseTo(2); railVisual.dispose();
    const independent = resolveVisualAssembly(robotPlusIndependentPositionerFixture.assembly, robotPlusIndependentPositionerFixture.topology, robotPlusIndependentPositionerFixture.joints); const visual = createTopologyAssembly(independent); applyTopologyJoint(independent.topology.traversal.jointById.get('positioner-j')!, visual.dynamicJoints.get('positioner-j')!, Math.PI / 2); visual.root.updateMatrixWorld(true); expect(visual.nodeGroups.get('arm-root')!.getWorldPosition(new THREE.Vector3()).x).toBeCloseTo(0); visual.dispose();
  });

  it('isolates left and right updates in the shared-root fixture', () => {
    const resolved = resolveVisualAssembly(dualArmSharedBaseFixture.assembly, dualArmSharedBaseFixture.topology, dualArmSharedBaseFixture.joints); const visual = createTopologyAssembly(resolved); applyTopologyJoint(resolved.topology.traversal.jointById.get('left-j')!, visual.dynamicJoints.get('left-j')!, Math.PI / 2); visual.root.updateMatrixWorld(true); const right = visual.nodeGroups.get('right-tip')!.getWorldPosition(new THREE.Vector3()); expect(right.x).toBeCloseTo(0); visual.dispose();
  });
});
