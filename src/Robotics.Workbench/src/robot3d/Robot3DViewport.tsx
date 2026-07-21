import { useEffect, useMemo, useRef, useState } from 'react';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { DiscoveryNode, Snapshot } from '../api/types';
import RobotImagePlaceholder from '../components/RobotImagePlaceholder';
import { createGenericSixAxisRobot } from './GenericSixAxisRobot';
import { interpolatePose, mapRobotAxes } from './mapping';
import { Robot3DState } from './types';

type Props = { axes: DiscoveryNode[]; snapshots: (Snapshot | undefined)[]; live: string; imageUrl?: string };
const statusLabel = (state: Robot3DState) => state.freshness === 'live' ? 'Live' : state.freshness === 'stale' ? 'Stale' : state.freshness === 'disconnected' ? 'Disconnected' : 'Unavailable';

export default function Robot3DViewport({ axes, snapshots, live, imageUrl }: Props) {
  const [now, setNow] = useState(Date.now()); const [showDiagnostics, setShowDiagnostics] = useState(false); const [failed, setFailed] = useState(false);
  const state = useMemo(() => mapRobotAxes({ axes, snapshots, live, now }), [axes, snapshots, live, now]);
  const host = useRef<HTMLDivElement>(null); const pose = useRef(state); const resetCamera = useRef<() => void>(() => undefined);
  pose.current = state;
  useEffect(() => { const timer = window.setInterval(() => setNow(Date.now()), 500); return () => window.clearInterval(timer); }, []);
  useEffect(() => {
    const element = host.current; if (!element || state.mappingStatus !== 'mapped' || failed) return;
    let renderer: THREE.WebGLRenderer; try { renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true }); } catch { setFailed(true); return; }
    const scene = new THREE.Scene(); scene.background = new THREE.Color(0x09181d); const camera = new THREE.PerspectiveCamera(30, 1, 0.1, 100); camera.position.set(4.6, 3.0, 5.8);
    const controls = new OrbitControls(camera, renderer.domElement); controls.enableDamping = true; controls.minDistance = 2.4; controls.maxDistance = 9; resetCamera.current = () => { camera.position.set(4.6, 3.0, 5.8); controls.target.set(0, 1.25, 0); controls.update(); }; const robot = createGenericSixAxisRobot(); scene.add(robot.root);
    scene.add(new THREE.HemisphereLight(0xc5fff5, 0x102229, 2)); const key = new THREE.DirectionalLight(0xffffff, 2.1); key.position.set(3, 5, 4); scene.add(key);
    const grid = new THREE.GridHelper(5, 20, 0x25444b, 0x173139); grid.position.y = 0.12; scene.add(grid); element.replaceChildren(renderer.domElement);
    const resize = () => { const width = Math.max(1, element.clientWidth), height = Math.max(1, element.clientHeight); renderer.setSize(width, height, false); camera.aspect = width / height; camera.updateProjectionMatrix(); }; const observer = new ResizeObserver(resize); observer.observe(element); resize();
    const actualRotation = (item: THREE.Group) => { const axis = item.userData.rotationAxis as THREE.Vector3; const component = axis.x ? 'x' : axis.y ? 'y' : 'z'; return (item.rotation[component] - (item.userData.zeroRotation || 0)) * (item.userData.direction || 1); };
    const setActualRotation = (item: THREE.Group, value: number) => { const axis = item.userData.rotationAxis as THREE.Vector3; const component = axis.x ? 'x' : axis.y ? 'y' : 'z'; item.rotation[component] = (item.userData.zeroRotation || 0) + (item.userData.direction || 1) * value; };
    // Seed both ends of the interpolation from the already validated snapshot.
    // Unresolved joints retain the calibrated visual zero rather than becoming fake data.
    let frame = 0; let target = pose.current.joints.map(item => item.renderRadians ?? 0); let previous = target.slice(); let targetAt = performance.now();
    robot.joints.forEach((item, index) => setActualRotation(item, target[index]));
    const render = (time: number) => { if (document.hidden) { frame = requestAnimationFrame(render); return; } const next = pose.current.joints.map((item, index) => item.renderRadians ?? target[index] ?? 0); if (next.some((value, index) => value !== target[index])) { previous = robot.joints.map(actualRotation); target = next; targetAt = time; } const values = interpolatePose(previous, target, time - targetAt); robot.joints.forEach((item, index) => setActualRotation(item, values[index])); controls.update(); renderer.render(scene, camera); frame = requestAnimationFrame(render); }; frame = requestAnimationFrame(render);
    return () => { cancelAnimationFrame(frame); observer.disconnect(); controls.dispose(); robot.dispose(); renderer.dispose(); resetCamera.current = () => undefined; element.replaceChildren(); };
  }, [state.mappingStatus, failed]);
  const fallback = <div className="robot-image-fallback"><RobotImagePlaceholder imageUrl={imageUrl}/><span className="robot3d-fallback-note">3D unavailable · static image fallback</span></div>;
  if (failed || state.mappingStatus !== 'mapped') return <div className="robot3d-panel">{fallback}<div className="robot3d-explanation">{failed ? 'WebGL could not be initialized.' : state.explanation}</div><span className="robot3d-status unavailable">Unavailable</span></div>;
  return <div className="robot3d-panel"><div ref={host} className="robot3d-canvas" aria-label="Generic six-axis operational visualization"/><div className={`robot3d-status ${state.freshness.toLowerCase()}`}>{statusLabel(state)}</div><button className="robot3d-reset" onClick={() => resetCamera.current()}>Reset camera</button><button className="robot3d-details" onClick={() => setShowDiagnostics(!showDiagnostics)}>{showDiagnostics ? 'Hide mapping details' : 'Joint details'}</button>{showDiagnostics && <div className="robot3d-diagnostics"><p>{state.explanation}</p>{state.joints.map(joint => <div key={joint.joint}><b>J{joint.joint}</b> <span>{joint.axis?.browseName || 'unmapped'} · {joint.unit} · {joint.position?.statusCode || 'Unavailable'}</span><small>{joint.position ? JSON.stringify({ nodeId: joint.position.nodeId, engineeringUnits: joint.position.engineeringUnits, engineeringUnit: joint.position.engineeringUnit, engineeringUnitsRaw: joint.position.engineeringUnitsRaw }) : joint.evidence}</small></div>)}</div>}</div>;
}
