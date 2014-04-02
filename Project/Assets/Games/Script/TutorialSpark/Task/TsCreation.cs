using UnityEngine;
using System.Collections;

public class TsCreation : Task {
	private TsCreationDef def;
	
	public TsCreation tsInit(TsCreationDef def){
		this.taskName = def.ToString();
		this.def = def;
		return this;
	}
	
	
	public override void run (){
		if (null == TsObjectFactory.GetGameObject(def.Obj)){
			TsObjectFactory.CreateGameObject(def);
		}
		complete();
	}
}
