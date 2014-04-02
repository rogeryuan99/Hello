using UnityEngine;
using System.Collections;

public class TsStep : Task {
	public TsStep tsInit (TsStepDef def){
		for (int i=0; i<def.Creations.Length; i++){
			addTask(Task.Create<TsCreation>().tsInit(def.Creations[i]));
		}
		for (int i=0; i<def.Actions.Length; i++){
			addTask(Task.Create<TsAction>().tsInit(def.Actions[i]));
		}
		return this;
	}
}
