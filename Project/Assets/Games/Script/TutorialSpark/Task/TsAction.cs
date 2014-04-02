using UnityEngine;
using System.Collections;

public class TsAction : Task {
	
	/* 	<action obj="xxxDialog" call="FillData" parms="{0}Title{1}Description{2}headIcon"/>
		<action obj="FingerTip" call="PromptToClickSomething" parms="DialogButton1"/>
		<action obj="UserAction" call="WaitToClickSomething" parms="DialogButton1"/>
	*/
	private TsActionDef def;
	
	public TsAction tsInit(TsActionDef def){
		this.taskName = def.ToString();
		this.def = def;
		return this;
	}
	
	public override void run (){
		GameObject obj = TsObjectFactory.GetGameObject(def.Obj);
		if (null == obj){
			Debug.LogError("Action object is not exist in the current scene. \n" + def.ToString());
			return;
		}
		if (typeof(TsUserBehaviorProcessor).Name == def.Obj){
			TsUserBehaviorProcessor.Instance.OnFinished += complete;
		}
		
		obj.SendMessage(def.Call, TsParmsTranslator.Translate(def.Parms));
		
		
		if (typeof(TsUserBehaviorProcessor).Name != def.Obj)
			complete();
	}
	protected override void complete (){
		TsUserBehaviorProcessor.Instance.OnFinished -= complete;
		base.complete ();
	}
}
