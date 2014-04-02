using UnityEngine;
using System.Collections;

public class TsChapter : Task {
	public TsChapter tsInit(TsChapterDef def){
		this.taskName = def.ToString();
		for (int i=0; i<def.Steps.Length; i++){
			addTask( Task.Create<TsStep>().tsInit(def.Steps[i]));
		}
		return this;
	}
}
