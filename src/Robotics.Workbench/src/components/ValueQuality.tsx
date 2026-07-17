import {SnapshotValue} from '../api/types';

export default function ValueQuality({value}:{value?:SnapshotValue}){
  if(!value)return <span className="quality quality-missing">Not discovered</span>;
  const good=/^Good/i.test(value.statusCode||'');
  if(good)return <span className="quality quality-live">Live · Good</span>;
  const age=value.lastGoodUpdatedAt===undefined?undefined:Math.max(0,Math.round((Date.now()-value.lastGoodUpdatedAt)/1000));
  return <span className="quality quality-degraded">{value.lastGoodValueText!==undefined?`Last good · ${age===undefined?'—':age+'s'} ago`:'Non-Good · no valid value'}</span>;
}
