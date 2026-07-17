import {Snapshot} from '../api/types';
import {selectSystemExecutingSubstate,selectSystemIdleSubstate,selectSystemTopState,selectTaskReadySubstate,selectTaskTopState,SelectedRuntimeValue} from '../selectors/runtimeValueSelectors';

const topStates=['Idle','Ready','Executing'];
const normalized=(value?:string)=>value?.replace(/[^a-z0-9]/gi,'').toLowerCase()||'';
const matches=(value:SelectedRuntimeValue,state:string)=>normalized(value.value?.valueText)===normalized(state);
const provenance=(value:SelectedRuntimeValue)=><details className="selector-provenance"><summary>Selected runtime entry</summary>{value.value?<pre>{JSON.stringify({rule:value.rule,label:value.value.label,browseName:value.value.browseName,nodeId:value.value.nodeId,valueText:value.value.valueText,statusCode:value.value.statusCode,sourceTimestamp:value.value.sourceTimestamp,serverTimestamp:value.value.serverTimestamp},null,2)}</pre>:<span>No valid runtime entry selected. Rule: {value.rule}</span>}</details>;

function Hierarchy({title,top,substates}:{title:string;top:SelectedRuntimeValue;substates:Record<string,SelectedRuntimeValue>}){
  const active=top.value?.valueText;
  const activeSub: SelectedRuntimeValue | undefined = active ? substates[normalized(active)] : undefined;
  return <section className="state-machine"><div className="state-machine-head"><span>{title}</span><b>{active||'Unknown / Unmapped'}</b></div><div className="hierarchy-flow">{topStates.map(state=><div className={`parent-state ${matches(top,state)?'active':''}`} key={state}><div className="parent-state-label"><i>{state.slice(0,1)}</i><span>{state}</span></div>{matches(top,state)&&activeSub?.value&&<div className="active-substate"><small>active substate</small><b>{activeSub.value.valueText}</b></div>}</div>)}</div><small>Parent state is runtime-selected; substates are shown only when active and valid.</small>{provenance(top)}{activeSub&&provenance(activeSub)}</section>;
}

export default function StateMachineView({snapshot,title,kind}:{snapshot?:Snapshot;title:string;kind:'system'|'task'}){
  if(kind==='system') return <Hierarchy title={title} top={selectSystemTopState(snapshot)} substates={{idle:selectSystemIdleSubstate(snapshot),executing:selectSystemExecutingSubstate(snapshot)}}/>;
  return <Hierarchy title={title} top={selectTaskTopState(snapshot)} substates={{ready:selectTaskReadySubstate(snapshot)}}/>;
}
