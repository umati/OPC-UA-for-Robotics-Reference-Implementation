import * as THREE from 'three';

interface PathTrailOptions {
  maxPoints: number;
  minDistance: number;
}

export class PathTrail {
  public readonly line: THREE.Line;

  private readonly points: THREE.Vector3[] = [];
  private readonly maxPoints: number;
  private readonly minDistanceSq: number;
  private readonly material: THREE.LineBasicMaterial;

  public constructor(options: PathTrailOptions) {
    this.maxPoints = options.maxPoints;
    this.minDistanceSq = options.minDistance * options.minDistance;
    this.material = new THREE.LineBasicMaterial({
      color: 0x48d6ff,
      transparent: true,
      opacity: 0.86,
      linewidth: 2,
    });

    this.line = new THREE.Line(new THREE.BufferGeometry(), this.material);
    this.line.name = 'tcp-path-trail';
    this.line.frustumCulled = false;
  }

  public addPoint(point: THREE.Vector3): void {
    const lastPoint = this.points[this.points.length - 1];
    if (lastPoint && lastPoint.distanceToSquared(point) < this.minDistanceSq) {
      return;
    }

    this.points.push(point.clone());
    while (this.points.length > this.maxPoints) {
      this.points.shift();
    }

    this.updateGeometry();
  }

  public clear(): void {
    this.points.length = 0;
    this.updateGeometry();
  }

  public setVisible(isVisible: boolean): void {
    this.line.visible = isVisible;
  }

  private updateGeometry(): void {
    this.line.geometry.dispose();
    this.line.geometry = new THREE.BufferGeometry().setFromPoints(this.points);
  }
}
