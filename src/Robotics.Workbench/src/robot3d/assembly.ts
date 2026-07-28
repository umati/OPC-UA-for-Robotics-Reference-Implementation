import * as THREE from 'three';
import { ResolvedVisualJoint } from './types';
import { LocalTransform, ValidatedVisualTopology } from './topology';

/** Workbench-only visual composition. It is not an OPC UA Robotics concept. */
export type AssemblyFallbackPolicy = 'static' | 'neutral' | 'omit';
export type ChainBindingScope = Readonly<{ visualJointId: string; motionDeviceKey: string; required?: boolean }>;
export type VisualAssemblyRoot = Readonly<{ rootId: string; rootNodeId: string; localTransform: LocalTransform }>;
export type VisualChainDefinition = Readonly<{
  chainId: string;
  displayName: string;
  rootNodeId: string;
  requiredBindings: readonly ChainBindingScope[];
  optionalBindings?: readonly ChainBindingScope[];
  requiredVisualJointIds: readonly string[];
  optionalVisualJointIds?: readonly string[];
  renderingStrategy?: 'dynamic' | 'static';
  fallbackPolicy: AssemblyFallbackPolicy;
  presentation?: Readonly<Record<string, string>>;
}>;
export type VisualParentAttachment = Readonly<{
  attachmentId: string;
  parentChainId?: string;
  parentNodeId: string;
  childChainId: string;
  childRootNodeId: string;
  localTransform: LocalTransform;
}>;
export type RobotVisualAssembly = Readonly<{
  assemblyId: string;
  displayName: string;
  topologyId: string;
  roots: readonly VisualAssemblyRoot[];
  chains: readonly VisualChainDefinition[];
  attachments?: readonly VisualParentAttachment[];
  sharedFixedNodeIds?: readonly string[];
}>;

export type ChainQuality = 'resolvedHealthy' | 'resolvedWithWarnings' | 'degraded' | 'unavailable' | 'notMatched' | 'ambiguous' | 'invalidTopology' | 'invalidBinding';
export type ChainBindingResult = Readonly<{ visualJointId: string; motionDeviceKey: string; sourceAxisKey?: string; status: 'resolved' | 'notMatched' | 'ambiguous' | 'invalid'; joint?: ResolvedVisualJoint }>;
export type ChainQualitySummary = Readonly<{ chainId: string; quality: ChainQuality; sourceNonGood: boolean; sourceUnavailable: boolean; parentPlacementDegraded: boolean; renderAvailable: boolean; exactStatusCodes: readonly string[] }>;
export type AssemblyDiagnostic = Readonly<{ code: string; severity: 'information' | 'warning' | 'error'; assemblyId: string; chainId?: string; nodeId?: string; message: string }>;
export type AssemblyValidationResult = Readonly<{ valid: boolean; diagnostics: readonly AssemblyDiagnostic[] }>;
export type ResolvedVisualChain = Readonly<{ definition: VisualChainDefinition; bindings: readonly ChainBindingResult[]; quality: ChainQualitySummary }>;
export type AssemblyTraversal = Readonly<{ rootOrder: readonly string[]; nodeToChain: ReadonlyMap<string, string>; jointToChain: ReadonlyMap<string, string>; visualJointToChain: ReadonlyMap<string, string>; motionDeviceToChains: ReadonlyMap<string, readonly string[]>; parentAttachmentByChain: ReadonlyMap<string, VisualParentAttachment>; sharedFixedNodes: readonly string[] }>;
export type ResolvedVisualAssembly = Readonly<{ definition: RobotVisualAssembly; topology: ValidatedVisualTopology; chains: readonly ResolvedVisualChain[]; diagnostics: readonly AssemblyDiagnostic[]; valid: boolean; traversal: AssemblyTraversal }>;

const finiteTransform = (t: LocalTransform) => [t.translation.x, t.translation.y, t.translation.z, t.rotation.x, t.rotation.y, t.rotation.z].every(Number.isFinite);
const error = (assemblyId: string, code: string, message: string, extra: Partial<AssemblyDiagnostic> = {}): AssemblyDiagnostic => ({ assemblyId, code, severity: 'error', message, ...extra });
const descendants = (topology: ValidatedVisualTopology, root: string) => {
  const result = new Set<string>(); const visit = (nodeId: string) => { if (result.has(nodeId)) return; result.add(nodeId); (topology.traversal.childJointIdsByNodeId.get(nodeId) || []).forEach(jointId => visit(topology.traversal.jointById.get(jointId)!.childNodeId)); }; visit(root); return result;
};

export function validateVisualAssembly(assembly: RobotVisualAssembly, topology: ValidatedVisualTopology, resolvedJoints: readonly ResolvedVisualJoint[] = []): AssemblyValidationResult {
  const diagnostics: AssemblyDiagnostic[] = []; const id = assembly.assemblyId || '';
  if (!id) diagnostics.push(error(id, 'missing-assembly-id', 'Assembly ID is required.'));
  if (assembly.topologyId !== topology.topologyId) diagnostics.push(error(id, 'topology-mismatch', `Assembly references topology ${assembly.topologyId}, not ${topology.topologyId}.`));
  const roots = new Map<string, VisualAssemblyRoot>(); assembly.roots.forEach(root => { if (roots.has(root.rootId)) diagnostics.push(error(id, 'duplicate-assembly-root', `Assembly root ${root.rootId} is duplicated.`, { nodeId: root.rootNodeId })); roots.set(root.rootId, root); if (!topology.traversal.nodeById.has(root.rootNodeId)) diagnostics.push(error(id, 'missing-shared-root', `Assembly root node ${root.rootNodeId} is unknown.`, { nodeId: root.rootNodeId })); if (!finiteTransform(root.localTransform)) diagnostics.push(error(id, 'non-finite-root-transform', `Assembly root ${root.rootId} has a non-finite transform.`)); });
  if (!assembly.roots.length) diagnostics.push(error(id, 'missing-shared-root', 'At least one assembly root is required.'));
  const chains = new Map<string, VisualChainDefinition>(); const dynamicOwner = new Map<string, string>(); const visualOwner = new Map<string, string>(); const deviceOwner = new Map<string, string>();
  assembly.chains.forEach(chain => {
    if (chains.has(chain.chainId)) diagnostics.push(error(id, 'duplicate-chain-id', `Chain ${chain.chainId} is duplicated.`, { chainId: chain.chainId })); chains.set(chain.chainId, chain);
    if (!topology.traversal.nodeById.has(chain.rootNodeId)) diagnostics.push(error(id, 'unknown-chain-root', `Chain ${chain.chainId} references unknown root ${chain.rootNodeId}.`, { chainId: chain.chainId, nodeId: chain.rootNodeId }));
    if (!chain.requiredVisualJointIds.length && chain.renderingStrategy !== 'static') diagnostics.push(error(id, 'empty-required-chain', `Dynamic chain ${chain.chainId} has no required visual joints.`, { chainId: chain.chainId }));
    if (!['dynamic', 'static'].includes(chain.renderingStrategy || 'dynamic') || !['static', 'neutral', 'omit'].includes(chain.fallbackPolicy)) diagnostics.push(error(id, 'invalid-chain-fallback-policy', `Chain ${chain.chainId} has an invalid fallback policy.`, { chainId: chain.chainId }));
    const scope = new Map<string, ChainBindingScope>(); [...chain.requiredBindings, ...(chain.optionalBindings || [])].forEach(binding => { if (scope.has(binding.visualJointId)) diagnostics.push(error(id, 'duplicate-motion-device-binding', `Chain ${chain.chainId} declares visual joint ${binding.visualJointId} more than once.`, { chainId: chain.chainId })); scope.set(binding.visualJointId, binding); });
    const membership = descendants(topology, chain.rootNodeId); const subtreeJoints = topology.joints.filter(joint => membership.has(joint.parentNodeId) && membership.has(joint.childNodeId));
    chain.requiredVisualJointIds.forEach(visualJointId => { const joint = topology.traversal.bindingByVisualJointId.get(visualJointId); if (!joint) diagnostics.push(error(id, 'unknown-visual-joint-reference', `Chain ${chain.chainId} references unknown visual joint ${visualJointId}.`, { chainId: chain.chainId })); else if (!subtreeJoints.some(candidate => candidate.jointId === joint.jointId)) diagnostics.push(error(id, 'joint-outside-chain-subtree', `Visual joint ${visualJointId} is outside chain ${chain.chainId}'s declared subtree.`, { chainId: chain.chainId, nodeId: chain.rootNodeId })); });
    subtreeJoints.filter(joint => joint.jointType !== 'fixed' && joint.resolvedVisualJointId).forEach(joint => { const visualJointId = joint.resolvedVisualJointId!; if (visualOwner.has(visualJointId) && visualOwner.get(visualJointId) !== chain.chainId) diagnostics.push(error(id, 'dynamic-joint-multiple-chains', `Visual joint ${visualJointId} belongs to multiple dynamic chains.`, { chainId: chain.chainId })); visualOwner.set(visualJointId, chain.chainId); if (dynamicOwner.has(joint.jointId) && dynamicOwner.get(joint.jointId) !== chain.chainId) diagnostics.push(error(id, 'dynamic-joint-multiple-chains', `Dynamic joint ${joint.jointId} belongs to multiple chains.`, { chainId: chain.chainId })); dynamicOwner.set(joint.jointId, chain.chainId); });
    [...chain.requiredBindings, ...(chain.optionalBindings || [])].forEach(binding => { const existing = deviceOwner.get(binding.motionDeviceKey); if (existing && existing !== chain.chainId) deviceOwner.set(binding.motionDeviceKey, `${existing}|${chain.chainId}`); else deviceOwner.set(binding.motionDeviceKey, chain.chainId); });
  });
  const attachments = new Map<string, VisualParentAttachment>(); const attachedChildren = new Set<string>(); (assembly.attachments || []).forEach(attachment => { if (attachments.has(attachment.childChainId)) diagnostics.push(error(id, 'conflicting-parent-attachment', `Chain ${attachment.childChainId} has more than one parent attachment.`, { chainId: attachment.childChainId })); attachments.set(attachment.childChainId, attachment); if (!chains.has(attachment.childChainId) || (attachment.parentChainId && !chains.has(attachment.parentChainId))) diagnostics.push(error(id, 'unknown-parent-attachment-chain', `Attachment ${attachment.attachmentId} references an unknown chain.`, { chainId: attachment.childChainId })); if (!topology.traversal.nodeById.has(attachment.parentNodeId) || !topology.traversal.nodeById.has(attachment.childRootNodeId)) diagnostics.push(error(id, 'unknown-parent-attachment-node', `Attachment ${attachment.attachmentId} references an unknown node.`, { chainId: attachment.childChainId })); attachedChildren.add(attachment.childChainId); if (!finiteTransform(attachment.localTransform)) diagnostics.push(error(id, 'non-finite-attachment-transform', `Attachment ${attachment.attachmentId} has a non-finite transform.`, { chainId: attachment.childChainId })); });
  const declaredRootNodes = new Set(assembly.roots.map(root => root.rootNodeId)); const reachableFromDeclaredRoot = new Set([...declaredRootNodes].flatMap(root => [...descendants(topology, root)])); assembly.chains.forEach(chain => { if (!reachableFromDeclaredRoot.has(chain.rootNodeId) && !attachedChildren.has(chain.chainId)) diagnostics.push(error(id, 'chain-root-outside-assembly', `Chain ${chain.chainId} root ${chain.rootNodeId} is not an assembly root or mounted child.`, { chainId: chain.chainId })); });
  const visit = (chainId: string, path = new Set<string>()) => { if (path.has(chainId)) { diagnostics.push(error(id, 'cyclic-visual-attachment', `Visual attachment cycle includes chain ${chainId}.`, { chainId })); return; } const next = new Set(path); next.add(chainId); const parent = attachments.get(chainId)?.parentChainId; if (parent) visit(parent, next); }; assembly.chains.forEach(chain => visit(chain.chainId));
  const reachable = new Set<string>(); const walkNode = (nodeId: string) => { if (reachable.has(nodeId)) return; reachable.add(nodeId); (topology.traversal.childJointIdsByNodeId.get(nodeId) || []).forEach(jointId => walkNode(topology.traversal.jointById.get(jointId)!.childNodeId)); }; assembly.roots.forEach(root => walkNode(root.rootNodeId)); (assembly.attachments || []).forEach(attachment => { if (reachable.has(attachment.parentNodeId)) walkNode(attachment.childRootNodeId); }); topology.nodes.forEach(node => { if (!reachable.has(node.nodeId)) diagnostics.push(error(id, 'unreachable-assembly-node', `Node ${node.nodeId} is unreachable from every assembly root.`, { nodeId: node.nodeId })); });
  const sourceAxisOwners = new Map<string, string>(); resolvedJoints.forEach(joint => { const binding = assembly.chains.flatMap(chain => [...chain.requiredBindings, ...(chain.optionalBindings || [])].map(item => ({ chain, item }))).find(item => item.item.visualJointId === joint.visualJointId); if (binding && binding.item.motionDeviceKey !== joint.sourceMotionDeviceKey) diagnostics.push(error(id, 'wrong-motion-device-ownership', `Visual joint ${joint.visualJointId} resolved from ${joint.sourceMotionDeviceKey}, expected ${binding.item.motionDeviceKey}.`, { chainId: binding.chain.chainId })); if (binding) { const previous = sourceAxisOwners.get(joint.sourceAxisKey); if (previous && previous !== binding.chain.chainId) diagnostics.push(error(id, 'source-axis-multiple-chains', `Source Axis ${joint.sourceAxisKey} is bound to unrelated chains ${previous} and ${binding.chain.chainId}.`, { chainId: binding.chain.chainId })); sourceAxisOwners.set(joint.sourceAxisKey, binding.chain.chainId); } });
  return { valid: !diagnostics.some(item => item.severity === 'error'), diagnostics };
}

export function summarizeChainQuality(chain: VisualChainDefinition, bindings: readonly ChainBindingResult[], parentPlacementDegraded = false): ChainQualitySummary { const exactStatusCodes = bindings.map(item => item.joint?.statusCode).filter((item): item is string => !!item).sort(); const sourceNonGood = bindings.some(item => item.joint?.diagnosticState === 'sourceNonGood'); const sourceUnavailable = bindings.some(item => !item.joint || item.joint.diagnosticState === 'sourceUnavailable' || item.joint.diagnosticState === 'invalid'); const missingRequired = chain.requiredBindings.some(item => !bindings.some(binding => binding.visualJointId === item.visualJointId && binding.status === 'resolved')); const quality: ChainQuality = missingRequired ? 'invalidBinding' : parentPlacementDegraded ? 'degraded' : sourceNonGood ? 'degraded' : sourceUnavailable ? 'unavailable' : bindings.some(item => item.joint?.freshness === 'stale') ? 'resolvedWithWarnings' : 'resolvedHealthy'; const omitUnavailable = chain.fallbackPolicy === 'omit' && (sourceUnavailable || sourceNonGood); return { chainId: chain.chainId, quality, sourceNonGood, sourceUnavailable, parentPlacementDegraded, renderAvailable: quality !== 'invalidBinding' && !omitUnavailable && !(parentPlacementDegraded && chain.fallbackPolicy === 'omit'), exactStatusCodes } }

export function resolveVisualAssembly(assembly: RobotVisualAssembly, topology: ValidatedVisualTopology, joints: readonly ResolvedVisualJoint[]): ResolvedVisualAssembly {
  const validation = validateVisualAssembly(assembly, topology, joints); const byVisual = new Map(joints.map(joint => [joint.visualJointId, joint])); const chains = [...assembly.chains].sort((a, b) => a.chainId.localeCompare(b.chainId)).map(definition => { const scopes = [...definition.requiredBindings, ...(definition.optionalBindings || [])]; const bindings = scopes.map(scope => { const joint = byVisual.get(scope.visualJointId); return { visualJointId: scope.visualJointId, motionDeviceKey: scope.motionDeviceKey, sourceAxisKey: joint?.sourceAxisKey, status: !joint ? 'notMatched' as const : joint.sourceMotionDeviceKey !== scope.motionDeviceKey ? 'invalid' as const : 'resolved' as const, joint }; }); return { definition, bindings, quality: summarizeChainQuality(definition, bindings, false) }; });
  const byChain = new Map(chains.map(chain => [chain.definition.chainId, chain])); const withDependencyQuality = chains.map(chain => { const parentId = assembly.attachments?.find(attachment => attachment.childChainId === chain.definition.chainId)?.parentChainId; const parent = parentId ? byChain.get(parentId) : undefined; const parentDegraded = !!parent && ['degraded', 'unavailable', 'invalidBinding', 'invalidTopology'].includes(parent.quality.quality); return { ...chain, quality: summarizeChainQuality(chain.definition, chain.bindings, parentDegraded) }; });
  const nodeToChain = new Map<string, string>(); const jointToChain = new Map<string, string>(); const visualJointToChain = new Map<string, string>(); chains.forEach(chain => { const nodes = descendants(topology, chain.definition.rootNodeId); nodes.forEach(nodeId => nodeToChain.set(nodeId, chain.definition.chainId)); topology.joints.filter(joint => nodes.has(joint.parentNodeId) && nodes.has(joint.childNodeId)).forEach(joint => { jointToChain.set(joint.jointId, chain.definition.chainId); if (joint.resolvedVisualJointId) visualJointToChain.set(joint.resolvedVisualJointId, chain.definition.chainId); }); });
  const motionDeviceToChains = new Map<string, readonly string[]>(); chains.forEach(chain => chain.bindings.forEach(binding => { const old = motionDeviceToChains.get(binding.motionDeviceKey) || []; motionDeviceToChains.set(binding.motionDeviceKey, [...old, chain.definition.chainId].filter((value, index, all) => all.indexOf(value) === index).sort()); }));
  return Object.freeze({ definition: assembly, topology, chains: withDependencyQuality, diagnostics: validation.diagnostics, valid: validation.valid, traversal: { rootOrder: assembly.roots.map(root => root.rootId).sort(), nodeToChain, jointToChain, visualJointToChain, motionDeviceToChains, parentAttachmentByChain: new Map((assembly.attachments || []).map(item => [item.childChainId, item])), sharedFixedNodes: [...(assembly.sharedFixedNodeIds || [])].sort() } });
}

export function composeAttachmentTransform(parent: THREE.Object3D, child: THREE.Object3D, attachment: VisualParentAttachment) { child.position.set(attachment.localTransform.translation.x, attachment.localTransform.translation.y, attachment.localTransform.translation.z); child.rotation.set(attachment.localTransform.rotation.x, attachment.localTransform.rotation.y, attachment.localTransform.rotation.z, attachment.localTransform.rotation.order || 'XYZ'); parent.add(child); }
