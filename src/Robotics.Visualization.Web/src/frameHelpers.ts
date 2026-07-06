import * as THREE from 'three';

export function createWorldFrame(): THREE.Group {
  const frame = new THREE.Group();
  frame.name = 'world-coordinate-frame';
  frame.position.set(-2.75, 0.035, -2.75);

  const axes = new THREE.AxesHelper(1.2);
  frame.add(axes);
  frame.add(makeFrameBase());

  return frame;
}

export function createToolFrame(): THREE.Group {
  const frame = new THREE.Group();
  frame.name = 'tool-coordinate-frame';
  frame.position.x = 0.48;

  const axes = new THREE.AxesHelper(0.48);
  frame.add(axes);

  const ring = new THREE.Mesh(
    new THREE.TorusGeometry(0.11, 0.006, 8, 36),
    new THREE.MeshBasicMaterial({ color: 0xffd28a, transparent: true, opacity: 0.9 }),
  );
  ring.rotation.y = Math.PI / 2;
  frame.add(ring);

  return frame;
}

function makeFrameBase(): THREE.Mesh {
  const base = new THREE.Mesh(
    new THREE.CylinderGeometry(0.16, 0.16, 0.025, 32),
    new THREE.MeshStandardMaterial({
      color: 0x162234,
      roughness: 0.72,
      metalness: 0.1,
    }),
  );
  base.receiveShadow = true;
  return base;
}
