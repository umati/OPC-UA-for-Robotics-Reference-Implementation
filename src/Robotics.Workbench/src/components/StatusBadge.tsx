export default function StatusBadge({value}:{value:string}){const good=/^Good/i.test(value);return <span className={`badge ${good?'good':'bad'}`}>{value||'Unknown'}</span>}
