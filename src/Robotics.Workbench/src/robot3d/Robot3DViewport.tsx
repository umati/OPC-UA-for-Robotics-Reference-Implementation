import { useEffect, useMemo, useRef, useState } from 'react';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { DiscoveryNode, MotionInventory, Snapshot } from '../api/types';
import RobotImagePlaceholder from '../components/RobotImagePlaceholder';
import { interpolatePose, mapRobotAxes } from './mapping';
import { Robot3DState } from './types';
import { referenceServerSixAxisTopology } from './referenceServerSixAxisTopology';
import { applyTopologyJoint, createTopologyAssembly } from './topologyRenderer';
import { compileVisualTopology, validateVisualTopology } from './topologyValidator';
import { referenceServerSixAxisAssembly } from './referenceServerSixAxisAssembly';
import { resolveVisualAssembly } from './assembly';

type Props = { axes: DiscoveryNode[]; snapshots: (Snapshot | undefined)[]; motionInventory?: MotionInventory | null; live: string; imageUrl?: string };
const statusLabel = (state: Robot3DState) => state.freshness === 'live' ? 'Live' : state.freshness === 'stale' ? 'Stale' : state.freshness === 'disconnected' ? 'Disconnected' : 'Unavailable';
const topologyValidation = validateVisualTopology(referenceServerSixAxisTopology);
const topology = topologyValidation.valid ? compileVisualTopology(referenceServerSixAxisTopology) : undefined;

export default function Robot3DViewport({ axes, snapshots, motionInventory, live, imageUrl }: Props) {
  const [now, setNow] = useState(Date.now()); const [showDiagnostics, setShowDiagnostics] = useState(false); const [failed, setFailed] = useState(false);
  const state = useMemo(() => mapRobotAxes({ axes, snapshots, motionInventory, live, now }), [axes, snapshots, motionInventory, live, now]);
  const host = useRef<HTMLDivElement>(null); const pose = useRef(state); const resetCamera = useRef<() => void>(() => undefined);
  pose.current = state;
  useEffect(() => { const timer = window.setInterval(() => setNow(Date.now()), 500); return () => window.clearInterval(timer); }, []);
  useEffect(() => {
    const element = host.current; const topologyForRender = topology; if (!element || state.mappingStatus !== 'matched' || failed || !topologyForRender) return;
    const resolved = state.joints.filter(item => item.sourceMotionDeviceKey).map(item => item); const assembly = resolveVisualAssembly(referenceServerSixAxisAssembly(resolved[0]?.sourceMotionDeviceKey || ''), topologyForRender, resolved); if (!assembly.valid) return;
    let renderer: THREE.WebGLRenderer; try { renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true }); } catch { setFailed(true); return; }
    const scene = new THREE.Scene(); scene.background = new THREE.Color(0x09181d); const camera = new THREE.PerspectiveCamera(30, 1, 0.1, 100); camera.position.set(4.6, 3.0, 5.8);
    const controls = new OrbitControls(camera, renderer.domElement); controls.enableDamping = true; controls.minDistance = 2.4; controls.maxDistance = 9; resetCamera.current = () => { camera.position.set(4.6, 3.0, 5.8); controls.target.set(0, 1.25, 0); controls.update(); }; const robot = createTopologyAssembly(assembly); scene.add(robot.root);
    scene.add(new THREE.HemisphereLight(0xc5fff5, 0x102229, 2)); const key = new THREE.DirectionalLight(0xffffff, 2.1); key.position.set(3, 5, 4); scene.add(key);
    const grid = new THREE.GridHelper(5, 20, 0x25444b, 0x173139); grid.position.y = 0.12; scene.add(grid); element.replaceChildren(renderer.domElement);
    const resize = () => { const width = Math.max(1, element.clientWidth), height = Math.max(1, element.clientHeight); renderer.setSize(width, height, false); camera.aspect = width / height; camera.updateProjectionMatrix(); }; const observer = new ResizeObserver(resize); observer.observe(element); resize();
    // Seed both ends of the interpolation from the already validated snapshot.
    // Unresolved joints retain the calibrated visual zero rather than becoming fake data.
    let frame = 0; let target = new Map(pose.current.joints.map(item => [item.visualJointId, item.convertedVisualValue ?? item.lastGoodConvertedValue ?? 0])); let previous = new Map(target); let targetAt = performance.now();
    const applyPose = (values: Map<string, number>) => assembly.chains.forEach(chain => chain.bindings.forEach(binding => { const topologyJoint = assembly.topology.traversal.bindingByVisualJointId.get(binding.visualJointId); const group = topologyJoint && robot.dynamicJoints.get(topologyJoint.jointId); if (topologyJoint && group) applyTopologyJoint(topologyJoint, group, values.get(binding.visualJointId)); })); applyPose(target);
    const render = (time: number) => { if (document.hidden) { frame = requestAnimationFrame(render); return; } const next = new Map(pose.current.joints.map(item => [item.visualJointId, item.convertedVisualValue ?? item.lastGoodConvertedValue ?? target.get(item.visualJointId) ?? 0])); if ([...next].some(([id, value]) => value !== target.get(id))) { previous = new Map(target); target = next; targetAt = time; } const values = new Map([...target].map(([id, value]) => [id, interpolatePose([previous.get(id) ?? value], [value], time - targetAt)[0]])); applyPose(values); controls.update(); renderer.render(scene, camera); frame = requestAnimationFrame(render); }; frame = requestAnimationFrame(render);
    return () => { cancelAnimationFrame(frame); observer.disconnect(); controls.dispose(); robot.dispose(); renderer.dispose(); resetCamera.current = () => undefined; element.replaceChildren(); };
  }, [state.mappingStatus, failed]);
  const fallback = <div className="robot-image-fallback"><RobotImagePlaceholder imageUrl={imageUrl}/><span className="robot3d-fallback-note">3D unavailable · static image fallback</span></div>;
  if (failed || !topology || state.mappingStatus !== 'matched') return <div className="robot3d-panel">{fallback}<div className="robot3d-explanation">{failed ? 'WebGL could not be initialized.' : !topology ? 'Visual topology is invalid.' : state.explanation}</div><span className="robot3d-status unavailable">Unavailable</span></div>;
  return <div className="robot3d-panel"><div ref={host} className="robot3d-canvas" aria-label="Generic six-axis operational visualization"/><div className={`robot3d-status ${state.freshness.toLowerCase()}`}>{statusLabel(state)}</div><button className="robot3d-reset" onClick={() => resetCamera.current()}>Reset camera</button><button className="robot3d-details" onClick={() => setShowDiagnostics(!showDiagnostics)}>{showDiagnostics ? 'Hide mapping details' : 'Joint details'}</button>{showDiagnostics && <div className="robot3d-diagnostics"><p>{state.explanation}</p>{state.joints.map(joint => <div key={joint.joint}><b>{joint.visualJointId}</b> <span>{joint.unit} · {joint.statusCode || 'Unavailable'}</span><small>{joint.evidence}</small></div>)}</div>}</div>;
}
