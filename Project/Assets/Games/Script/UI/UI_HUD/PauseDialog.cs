using UnityEngine;
using System.Collections;

public class PauseDialog : DlgBase 
{
	public GameObject replayBtn;
	
	void OnEnable(){
		if(TsTheater.InTutorial){
			replayBtn.SetActive(false);	
		}
	}
	public float lastTimeScale = 0.0f;
	
	public void homeButtonClicked()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		LevelMgr.Instance.OutBattle();
//		StaticData.paused = false;
//		SkillIconManager.Instance.destroyAllSkillIconDataList();
//		ComboController.Instance.destroyAllComboIconDataList();
//		SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//		HeroMgr.clear();
//		BattleBg.Instance.DestroyAllObj();
//		LevelMgr.Instance.DestroyAllObj();
		GotoProxy.gotoScene (GotoProxy.UIMAIN);	
		
		LevelMgr.Instance.pauseButton.SetActive(true);
		TsTheater.InTutorial = false;
	}
	
	public void continueButtonClicked()
	{
		resume();
	}
	
	public void replayButtonClicked()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		LevelMgr.Instance.OutBattle();
//		StaticData.paused = false;
//		SkillIconManager.Instance.destroyAllSkillIconDataList();
//		ComboController.Instance.destroyAllComboIconDataList();
//		SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//		HeroMgr.clear();
//		BattleBg.Instance.DestroyAllObj();
//		LevelMgr.Instance.DestroyAllObj();
		
		LevelMgr.Instance.pauseButton.SetActive(true);
		GotoProxy.fadeInScene (GotoProxy.BATTLESCENE);
	}
	public override void OnBtnBackClicked()
	{
		resume();
	}
	private void resume(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		StaticData.paused = false;
		Time.timeScale = lastTimeScale;
		LevelMgr.Instance.pauseButton.SetActive(true);
		
		base.OnBtnBackClicked();
	}
}
