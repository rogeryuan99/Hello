using UnityEngine;
using System.Collections;


public class TsTheater : Singleton<TsTheater> {
	
	public static bool InTutorial= false;
	
	public delegate void CallBackDelegate(string id);
	public CallBackDelegate OnFinished;
	private string ftueEventId = string.Empty;
	
	private string partName = null;
	public void PreparePart(string partName){
		this.partName = partName;
	}
	void OnLevelWasLoaded(){
		StartCoroutine(delayedStartTutorial());
//		if(partName != null){
//			PlayPart(partName);	
//			partName = null;
//		}
	}
	public void ChangeScene(string[] parms){
		Application.LoadLevel("UIMain");
	}
	private IEnumerator delayedStartTutorial(){
		yield return new WaitForSeconds(.01f);
		if(partName != null){
			PlayPart(partName);	
			partName = null;
		}
	}
	public void PlayPart(string partName){
		PlayPart(partName, null, string.Empty);
	}
	public void PlayPart(string partName, CallBackDelegate onFinishedCall, string ftueEventId_){
		OnFinished = onFinishedCall;
		ftueEventId = ftueEventId_;
		
		PlayPart(TsXmlReader.ReadPart(partName));
	}
	public void PlayPart(TsPartDef def){
		Task chapterQueue = Task.Create<Task>();
		InTutorial = true;
		for (int i=0; i<def.Charpters.Length; i++){
			chapterQueue.addTask(Task.Create<TsChapter>().tsInit(def.Charpters[i]));
		}
		chapterQueue.start();
		chapterQueue.taskCompleteDelegate = OnPartEnd;
	}
	
	public void OnPartEnd(Task task){
		InTutorial = false;
		TsUserBehaviorProcessor.Instance.UnlockCombo(null);
		if (null != OnFinished){
			OnFinished(ftueEventId);
		}
	}
}
