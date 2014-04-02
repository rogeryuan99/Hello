using UnityEngine;
using System.Collections;

public class HomePageDlg : DlgBase
{
	private static HomePageDlg _instance;
	[HideInInspector]
	public static HomePageDlg Instance{
		get{
			return _instance;
		}
	}
	
	public GameObject chapterSelectButtonSprite;
	
	public GameObject pvpButtonObj;
	public UISprite pvpButtonTxt;
	public UISprite pvpButtonBG;
	
	public GameObject storeButtonObj;
	public UISprite storeButtonTxt;
	public UISprite storeButtonBG;
	
	public GameObject teamButtonObj;
	public UISprite teamButtonTxt;
	public UISprite teamButtonBG;
	
	public GameObject storyButtonObj;
	public UISprite storyButtonTxt;
	public UISprite storyButtonBG;
	
	public UILabel MuteMusic;
	public UILabel MuteSoundFx;
	public UILabel exitLabel;
	
	[HideInInspector]public static string reservedDlg=null;

	public void Awake ()
	{
		_instance = this;
		DlgManager.instance.pushStack(this.gameObject);
		MuteMusic.text = MusicManager.Instance.isBgMute?"Music On":"Music Off";
		MuteSoundFx.text = MusicManager.Instance.isEffectMute?"SoundFX On":"SoundFX Off";
		switch(reservedDlg){
		case "LevelSelect":
			DlgManager.instance.ShowLevelSelectDlg();
			Destroy(this.gameObject);
		break;
		case "ChapterSelect":
			DlgManager.instance.ShowChapterListDlg();
			Destroy(this.gameObject);	
		break;
		}
		reservedDlg = null;
		exitLabel.gameObject.SetActive(false);
	}
	
	void Start ()
	{
		this.buttonEnabled(pvpButtonObj,   pvpButtonTxt,   pvpButtonBG,   TsFtueManager.Instance.IsPvpCanUse);
		this.buttonEnabled(storeButtonObj, storeButtonTxt, storeButtonBG, TsFtueManager.Instance.IsGearUpCanUse);//IsStoreCanUse);
		this.buttonEnabled(teamButtonObj,  teamButtonTxt,  teamButtonBG,  TsFtueManager.Instance.IsGearUpCanUse);
		this.buttonEnabled(storyButtonObj, storyButtonTxt, storyButtonBG, TsFtueManager.Instance.IsStoryCanUse);
		
		MusicManager.playBgMusic("MUS_UI_Menus");
	}

	public void OnContinueBattleBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
	}
	public void OnChapterSelectBtnClick(){
		//MusicManager.playBgMusic("");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		DlgManager.instance.ShowChapterListDlg();	
		Destroy(this.gameObject);
	}
	public void onStoreBtnClick(){
//		MusicManager.playBgMusic("");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		TeamDlg dlg = DlgManager.instance.ShowTeamDlg();
		dlg.checkISO.startsChecked = false;
		dlg.checkGear.startsChecked = false;
		dlg.checkSkill.startsChecked = false;
		dlg.checkStore.startsChecked = true;
		
		//dlg.OnStoreBtnClick(true);
		dlg.onClose = delegate {
			MusicManager.playBgMusic("MUS_UI_Menus");
			DlgManager.instance.ShowHomePageDlg();
		};
		Destroy(this.gameObject);
	}
	public void onTeamBtnClick(){
//		MusicManager.playBgMusic("");
		//MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_2a");
		
		TeamDlg dlg = DlgManager.instance.ShowTeamDlg();
		dlg.delayMusic();
		
		dlg.checkISO.startsChecked = false;
		dlg.checkGear.startsChecked = true;
		dlg.checkSkill.startsChecked = false;
		dlg.checkStore.startsChecked = false;

		this.gameObject.SetActive(false);
		dlg.onClose = delegate {
			MusicManager.playBgMusic("MUS_UI_Menus");
			DlgManager.instance.ShowHomePageDlg();
		};
		Destroy(this.gameObject);
	}
	
	public void onStoryBtnClick(){
//		MusicManager.playBgMusic("");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		GotoIntro();
	}
	public void GotoIntro(){
		GotoProxy.gotoScene("OpenAnim");
	}
	
	public void allButtonEnabled(string[] parms){
		allButtonEnabled(bool.Parse(parms[0]));
	}
	
	public void allButtonEnabled(bool isEnabled)
	{
		/*this.buttonEnabled(pvpButtonObj, pvpButtonTxt, pvpButtonBG, isEnabled);
		this.buttonEnabled(storeButtonObj, storeButtonTxt, storeButtonBG, isEnabled);
		this.buttonEnabled(teamButtonObj, teamButtonTxt, teamButtonBG, isEnabled);
		this.buttonEnabled(storyButtonObj, storyButtonTxt, storyButtonBG, isEnabled);*/
	}
	
	public void highlightChapterSelectButton(string[] parms)
	{
		chapterSelectButtonSprite.GetComponent<TweenColor>().enabled = true;
	}
	
	public void normalChapterSelectButton()
	{
		chapterSelectButtonSprite.GetComponent<TweenColor>().enabled = false;
	}
	
	public override void OnBtnBackClicked(){
		if(!exitLabel.gameObject.activeInHierarchy){
			exitLabel.gameObject.SetActive(true);
			StartCoroutine(cancelExit());
		}else{
			Debug.Log("Exit");
			Application.Quit();
		}
	}
	private IEnumerator cancelExit(){
		yield return new WaitForSeconds(2);
		exitLabel.gameObject.SetActive(false);
	}
	protected void buttonEnabled(GameObject buttonObj, UISprite textSprite, UISprite bgSprite, bool isEnabled)
	{
		buttonObj.collider.enabled = isEnabled;
		if(isEnabled)
		{
			textSprite.color = Color.white;
			bgSprite.color = Color.white;
		}
		else
		{
			textSprite.color = new Color32(128, 128, 128, 128);
			bgSprite.color = Color.gray;
		}
	}

	void OnDebugDlg()
	{
		DlgManager.instance.ShowDebugDlg();
	}
	
	void OnMusicMute(){
		MusicManager.Instance.muteBgMusic( !MusicManager.Instance.isBgMute);
		MuteMusic.text = MusicManager.Instance.isBgMute?"Music On":"Music Off";
	}
	void OnSoundFXMute(){
		MusicManager.Instance.muteEffectMusic( !MusicManager.Instance.isEffectMute);
		MuteSoundFx.text = MusicManager.Instance.isEffectMute?"SoundFX On":"SoundFX Off";
	}
	
#if UNITY_EDITOR	
	void OnGUI(){
		
		GUILayout.Space(400);
//		if(GUILayout.Button("Goto Command Test ")){
//			GameObject go = GameObject.Find("bg_music(Clone)");
//			Destroy(go);
//			Application.LoadLevel("CommandTest");
//		}
//		
		if(GUILayout.Button("dump levels")){
			MapMgr.Instance.dumpLevelJsonForArtoo();
		}
		if(GUILayout.Button("Goto PVP Test"))
		{
			StaticData.isPVP = true;
			MapMgr.Instance.currentChapterIndex = 999;
			MapMgr.Instance.currentLevelIndex = 1;
			Application.LoadLevel("NormalLevel");
		}
	}
#endif
}
