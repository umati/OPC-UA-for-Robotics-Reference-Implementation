import { RobotVisualAssembly } from './assembly';
import { identityTransform } from './topology';

/** The current reference robot is intentionally represented as the simplest valid assembly. */
export const referenceServerSixAxisAssembly = (motionDeviceKey: string): RobotVisualAssembly => ({
  assemblyId: 'reference-server-six-axis-v1-assembly',
  displayName: 'Reference server six-axis visual assembly',
  topologyId: 'reference-server-six-axis-v1-visual-topology',
  roots: [{ rootId: 'reference-root', rootNodeId: 'base', localTransform: identityTransform() }],
  chains: [{
    chainId: 'reference-six-axis-chain', displayName: 'Six-axis articulated chain', rootNodeId: 'base',
    requiredBindings: ['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3'].map(visualJointId => ({ visualJointId, motionDeviceKey, required: true })),
    requiredVisualJointIds: ['base', 'shoulder', 'elbow', 'wrist1', 'wrist2', 'wrist3'], fallbackPolicy: 'neutral',
  }],
});
