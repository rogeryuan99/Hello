using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TsFtueManager : Singleton<TsFtueManager> {
	
	public const string LEVEL_LOADED   = "LevelLoaded";
	public const string EVENT_FINISHED = "EventFinished";
	public const string BATTLE_INIT    = "BattleInit";
	public const string BATTLE_READY   = "BattleReady";
	public const string BATTLE_STARTED = "BattleStarted";
	public const string BATTLE_VICTORY = "BattleVictory";
	public const string LEVEL_GO_NEXT  = "LevelGoNext";
	public const string DIALOG_OPENED  = "DialogOpened";
	
	private TsTriggerEventContainer eventContainer;
	private TsDynamicData serverData;
	
	public bool IsGearUpCanUse{
		get{
			return serverData.CheckHasDoneBefore(eventContainer.GetEventById("2").Ids);
		}
	}
	public bool IsTrainCanUse{
		get{
			return (serverData.CheckHasDoneBefore(eventContainer.GetEventById("10").Ids)
				&& serverData.CheckHasDoneBefore(eventContainer.GetEventById("11").Ids));
		}
	}
	public bool IsChangeCanUse{
		get{
			return serverData.CheckHasDoneBefore(eventContainer.GetEventById("4.1.1").Ids);
		}
	}
	
	public bool IsPvpCanUse{
		get{
			return false;
		}
	}
	
	public bool IsStoreCanUse{
		get{
			return false;
		}
	}
	
	public bool IsStoryCanUse{
		get{
			return serverData.CheckHasDoneBefore(eventContainer.GetEventById("-.5").Ids);;
		}
	}
	
	public bool HasDoneBefore(string id){
		return serverData.CheckHasDoneBefore(eventContainer.GetEventById(id).Ids);
	}
	
	public void Start(){
		DontDestroyOnLoad(gameObject);
	}
	
	public void Init(){
		eventContainer = TsXmlReader.ReadTriggerEvents();
		serverData = TsDynamicData.instance;
	}
	
	public void CheckEvent(string eventName){
		if (string.IsNullOrEmpty(eventName)) return;
		
		List<TsTriggerEventDef> e = eventContainer.GetEventsByName(eventName);
		List<string> existIds = new List<string>();
		if (null != e){
			for (int i=e.Count-1; i>=0; i--){
				if (serverData.CheckHasDoneBefore(e[i].Ids)
					|| existIds.Contains(e[i].Id)
					|| (!string.IsNullOrEmpty(e[i].Preconditions[0]) 
						&& !serverData.CheckPreconditions(e[i].Preconditions))){
					e.RemoveAt(i);
				}
				else{
					existIds.AddRange(e[i].Ids);
				}
			}
			// serverData.Update(e);
			for (int i=0; i<e.Count; i++){
				BlockRejectEvents(e[i].Rejects);
				SendMessage(e[i].Call, e[i].Parms);
			}
		}
	}
	
	public void OnLevelWasLoaded(int index){
		CheckEvent(string.Format("{0}_{1}", LEVEL_LOADED, Application.loadedLevelName));
	}
	
	private void OnFinishedCall(string eventId){
		CheckEvent(string.Format("{0}_{1}", EVENT_FINISHED, eventId));
	}
	
	public void CacheEventFinished(string eId){
		TsTriggerEventDef e = eventContainer.GetEventById(eId);
		serverData.CacheFinishedId(e.Ids);
		serverData.AddBranches(e.Branches);
	}
	
	public void MarkEventFinished(string[] parms){
		MarkEventFinished(parms[0]);
		OnFinishedCall(parms[0]);
	}
	public void MarkEventFinished(string eId){
		TsTriggerEventDef e = eventContainer.GetEventById(eId);
		
		serverData.FinishId(e.Ids);
		serverData.AddBranches(e.Branches);
		if (serverData.CheckBranches()){
			serverData.ClearCache();
			serverData.ClearBranches();
			Debug.LogError("CheckBranches Has Done");
		}
		//serverData.save();
		UserInfo.instance.saveAll();
	}
	
	private void BlockRejectEvents(string[] rejects){
		if (null != rejects){
			for (int i=0; i<rejects.Length; i++){
				eventContainer.GetEventById(rejects[i]).Block = true;
			}
		}
	}
	
	private bool hasStoryPlayed = false;
	public void PlayStroy(string[] parms){
		if (false == hasStoryPlayed){
			hasStoryPlayed = true;
			HomePageDlg.Instance.GotoIntro();
			MarkEventFinished(parms[0]);
		}
	}
	public void FirstInTutorial(string[] parms){
		if (false == hasStoryPlayed){
			hasStoryPlayed = true;
			MapMgr.Instance.currentChapterIndex = 888;
			MapMgr.Instance.currentLevelIndex = 1;
			CacheEventFinished(parms[0]);
			Application.LoadLevel("NormalLevel");
			// HomePageDlg.Instance.GotoIntro();
		}
	}
	
	public void StartFirstPlayTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (id) => {
			CacheEventFinished(id);
		}, parms[0]);
	}
	
	public void StartFirstInChapterDlgTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (id) => {
			serverData.ClearCache();
			MarkEventFinished(id);
			MapMgr.Instance.selectChapterAndLevel(1,1);
			GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
		}, parms[0]);
	}
	
	public void StartLevel1Tutorial(string[] parms){
		BattleBg.Instance.IsSkipHudPreBattle = true;
		LevelMgr.Instance.BlockEnemyCreation();
//		TsGrayLockOrGlowTool pauseBtn = GameObject.Find("PauseButton").GetComponent<TsGrayLockOrGlowTool>();
//		pauseBtn.GrayLock();
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
//			TsGrayLockOrGlowTool pauseBtn = GameObject.Find("PauseButton").GetComponent<TsGrayLockOrGlowTool>();
//			pauseBtn.Normal();
			LevelMgr.Instance.pauseButton.SetActive(true);
			BattleBg.Instance.IsSkipHudPreBattle = false;
			LevelMgr.Instance.UnblockEnemyCreation();
			LevelMgr.Instance.createEnemy();
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartLevel1VectoryTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1]);
	}
	
	public void StartLevel2GearUpTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1]);
	}
	
	public void StartLevel2VectoryTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1]);
	}
	
//	public void StartLevel3ChangeTutorial(string[] parms){
//		TsTheater.Instance.PlayPart(parms[1]);
//	}
	public void StartLevel3ChangeEnterDlgTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			CacheEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	public void StartLevel3ChangeRecruiteTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	public void StartLevel3ChangeSelectTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	public void StartLevel3ChangeEndTutorial(string[] parms){
		MarkEventFinished(parms[0]);
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	
	
	public void StartLevel3Tutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1]);
	}
	
	public void StartLevel3VectoryTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1]);
	}
	
	public void StartLevel4SkillTreeTutorial(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			CacheEventFinished(eId);
			TsTheater.InTutorial = true;
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartLevel4SkillTutorial(string[] parms){
		LevelMgr.Instance.BlockEnemyCreation();
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			LevelMgr.Instance.UnblockEnemyCreation();
			LevelMgr.Instance.createEnemy();
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartLevel4StarLordSkillTree(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartLevel4DraxSkillTree(string[] parms){
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			MarkEventFinished(eId);
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartLevel4SkillTreeFinished(string[] parms){
		MarkEventFinished(parms[0]);
		Debug.LogError("StartLevel4SkillTreeFinished");
		TsTheater.Instance.PlayPart(parms[1], (eId)=>{
			OnFinishedCall(eId);
		}, parms[0]);
	}
	
	public void StartChangeButtonTutorial(string[] parms){
		MarkEventFinished(parms[0]);
		OnFinishedCall(parms[0]);
	}
	
	#region Outside Function Call
	
	public void BlockEnemyRandomAttack(string[] parms){
		Enemy.BlockRandomAttack();
	}
	public void UnblockEnemyRandomAttack(string[] parms){
		Enemy.UnblockRandomAttack();
	}
	
	#endregion
	
	#region TestFunctions
	public void TestFunction0(string[] parms){
		Debug.LogError(string.Format("Excute Event {0}", parms[1]));
		serverData.FinishId(eventContainer.GetEventById(parms[0]).Ids);
		
		CheckEvent(string.Format("{0}_{1}", EVENT_FINISHED, parms[0]));
	}
	
	public void TestFunction1(string[] parms){
		Debug.LogError(string.Format("Excute Event {0}", parms[1]));
		serverData.FinishId(eventContainer.GetEventById(parms[0]).Ids);
		
		UserInfo.instance.saveAll();
	}
	#endregion
}
