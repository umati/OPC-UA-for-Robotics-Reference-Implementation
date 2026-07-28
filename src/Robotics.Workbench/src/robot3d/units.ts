import { SnapshotValue } from '../api/types';
import { UnitKind } from './types';

export function localBrowseName(browseName: string): string { const qualified = browseName.match(/^\d+:(.+)$/); return qualified ? qualified[1] : browseName; }
export function convertPosition(value: number, engineeringUnits?: string | null, metadata?: SnapshotValue['engineeringUnit']): { unit: UnitKind; radians?: number } {
  const unit = [metadata?.displayName, metadata?.description, engineeringUnits].filter(Boolean).join(' ').toLowerCase();
  if (/radian|\brad\b/.test(unit)) return { unit: 'radians', radians: value };
  if (/degree|\bdeg\b|°/.test(unit)) return { unit: 'degrees', radians: value * Math.PI / 180 };
  return { unit: engineeringUnits ? 'unsupported' : 'missing' };
}
export function isGoodStatus(status?: string) { const value = status?.trim() || ''; if (/^Good(?:$|[/\s:])/i.test(value) || ['GoodClamped', 'GoodLocalOverride', 'GoodSubNormal'].includes(value)) return true; if (/^0x[0-9a-f]+$/i.test(value)) return Number.parseInt(value.slice(2), 16) === 0; return /^\d+$/.test(value) && Number(value) === 0; }
