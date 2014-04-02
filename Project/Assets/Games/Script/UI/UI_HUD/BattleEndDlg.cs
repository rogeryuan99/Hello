using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleEndDlg : MonoBehaviour {
	[HideInInspector]private int input_starts = 0;
	[HideInInspector]private int input_coins = 0;
	[HideInInspector]private int input_exp = 0;
	[HideInInspector]private int input_cp = 0;
	[HideInInspector]private EquipData input_rewardItem = null;
	[HideInInspector]private HeroData input_rewardHero = null;
	[HideInInspector]private int input_totalWaves = 0;
	[HideInInspector]private int input_totalEnemies = 0;
	
	public UILabel levelNameLabel;
	public GameObject btn_Page2;
	public GameObject step0_group;
	public UILabel step0_title;
	public UISprite step0_heroHead;
	public List<UISprite> step1_stars;
	public GameObject step2_coin_group;
	public UILabel step2_coins;
	public GameObject step3_cp_group;
	public UILabel step3_cp;
	public GameObject step4_Gear_group;
	public UILabel step4_gear_name;
	public UISprite step4_gearImg;
	
	public UIAtlas equipsAtlas;
	public UIAtlas isoAtlas;
	
	public UILabel step7_arenaTotalWaves;
	public UILabel step7_arenaTotalEnemies;
	public GameObject nextButton;
	public GameObject heroStateTemplate;
	public delegate void CallBack();
	private CallBack showCompletedCallback; 
	public void victory(Hashtable h,CallBack showCompletedCallback){
		Debug.Log("victory");
		this.showCompletedCallback = showCompletedCallback;
		if(h.Contains("silver"))  input_coins = (int) h["silver"];	
		if(h.Contains("star"))  input_starts = (int)  h["star"];
		if(h.Contains("exp"))  input_exp = (int) h["exp"]; 
		if(h.Contains("cp"))  input_cp = (int) h["cp"]; 
		if(h.Contains("equipData"))  input_rewardItem = h["equipData"] as EquipData;
		if(h.Contains("herodata"))  input_rewardHero = h["herodata"] as HeroData;	
		if(h.Contains("totalWaves"))  input_totalWaves = (int)h["totalWaves"] ;	
		if(h.Contains("totalEnemies"))  input_totalEnemies = (int)h["totalEnemies"] ;	
		Debug.Log(" rewards: "+Utils.dumpHashTable(h));
	}
	public void Start(){
//		Debug.LogError("Start");
		clear();	
		//levelNameLabel.text = MapMgr.Instance.getCurrentChapter().name + " " + MapMgr.Instance.getCurrentLevel().id;
		if(!MapMgr.Instance.isArena){
			levelNameLabel.text = string.Format(Localization.instance.Get("UI_BattleEndDlg_Title"),
											Localization.instance.Get("UI_ChapterName_" + MapMgr.Instance.getCurrentChapter().id),
											MapMgr.Instance.getCurrentLevel().id);
		}else{
			levelNameLabel.text = Localization.instance.Get("UI_ChapterName_" + MapMgr.Instance.getCurrentChapter().id+"_arena");
		}
		if(input_rewardHero != null){
			StartCoroutine(play_page1());
		}else{
			StartCoroutine(play_page2());
		}
		nextButton.SetActive(!MapMgr.Instance.isArena);
	}
	private void clear(){
		btn_Page2.SetActive(false);
		this.step0_group.SetActive(false);
		foreach(UISprite star in step1_stars){
			star.gameObject.SetActive(false);		
		}
		this.step2_coin_group.SetActive(false);
		this.step3_cp_group.SetActive(false);
		this.step4_Gear_group.SetActive(false);
		this.heroStateTemplate.SetActive(false);
	}
	private IEnumerator play_page1(){
		yield return new WaitForSeconds(0.1f);
		
		//this.step0_title.text = "Unlock hero\n"+input_rewardHero.nickName;
		this.step0_title.text = string.Format(Localization.instance.Get("UI_BattleEndDlg_UnlockHero"),input_rewardHero.nickName);
		this.step0_heroHead.spriteName = input_rewardHero.type;
		this.step0_group.SetActive(true);
		
	}
	private IEnumerator play_page2(){
		step0_group.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		MusicManager.playEffectMusic("SFX_Score_Star_1a");
		for(int m=0;m<step1_stars.Count;m++){
			yield return new WaitForSeconds(0.1f);
			step1_stars[m].spriteName = "star_dark";
			step1_stars[m].gameObject.SetActive(true);
			if(m<input_starts){
				yield return new WaitForSeconds(0.2f);
				step1_stars[m].spriteName = "star";
			}
		}
		yield return new WaitForSeconds(0.2f);
		step2_coins.text = input_coins.ToString();
		step2_coin_group.SetActive(true);
		
		if(input_cp>0){
			yield return new WaitForSeconds(0.2f);
			step3_cp.text = input_cp.ToString();
			step3_cp_group.SetActive(true);
		}
		MusicManager.playEffectMusic("SFX_rewards_1a");
		yield return new WaitForSeconds(0.2f);
		//step4_gearImg.spriteName = "";
		if(input_rewardItem!=null){
			step4_Gear_group.SetActive(true);
			//step4_gear_name.text = input_rewardItem.equipDef.equipName;
			if(input_rewardItem.equipDef.id>=4000 && input_rewardItem.equipDef.id<=4999){
				step4_gearImg.atlas = isoAtlas;
				step4_gearImg.spriteName = input_rewardItem.equipDef.iconID;
				if(step4_gearImg.GetAtlasSprite()==null){
					this.step4_gearImg.spriteName = "ISO_default";
				}
				step4_gear_name.text = string.Format("{0}",Localization.instance.Get("ISO_Name_"+input_rewardItem.equipDef.id));
			}else{
				step4_gearImg.atlas = equipsAtlas;
				step4_gearImg.spriteName = input_rewardItem.equipDef.iconID;
				if(step4_gearImg.GetAtlasSprite()==null){
					this.step4_gearImg.spriteName = "GearDefault";
				}
				step4_gear_name.text = string.Format("{0}",Localization.instance.Get("Gear_Name_"+input_rewardItem.equipDef.id));
			}
		}

		int n = 0;
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
//			Debug.LogError("new colum");
			GameObject colum = heroStateTemplate;
			if(n>0){
				colum = Instantiate(heroStateTemplate) as GameObject;	
				colum.transform.parent = heroStateTemplate.transform.parent;
				colum.transform.localScale = heroStateTemplate.transform.localScale;
				colum.transform.localPosition = heroStateTemplate.transform.localPosition + new Vector3(160*n,0,0); 
				
			}
			colum.SetActive(true);
			BattleEndDlgHeroState hs = colum.GetComponent<BattleEndDlgHeroState>();
			hs.hero = hero;
			hs.addXp(input_exp);
			n ++;
		}
		if(MapMgr.Instance.isArena){
			yield return new WaitForSeconds(0.3f);
			step7_arenaTotalWaves.text = "Total Waves : ";
			yield return new WaitForSeconds(0.3f);
			step7_arenaTotalWaves.text += input_totalWaves;
			
			yield return new WaitForSeconds(0.3f);
			step7_arenaTotalEnemies.text = "Total Enemies : ";
			yield return new WaitForSeconds(0.3f);
			step7_arenaTotalEnemies.text += input_totalEnemies;
		}else{
			step7_arenaTotalWaves.text = "";
			step7_arenaTotalEnemies.text = "";
		}
		
		btn_Page2.SetActive(true);
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_AlertBattleEnd_{1}-{2}", 
			TsFtueManager.DIALOG_OPENED, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
		showCompletedCallback();
	}
	
	
	void OnBtnOK(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		StartCoroutine(play_page2());		
	}
	

	public void homeBtnClicked ()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(stopBtnClick()) return;
		else{
			MusicManager.Instance.replayBgMusic("SFX_Chapter_End_Victory_Temp_1a");
		}
		StartCoroutine(delayedOnHomeBtnClick());	
	}
	
	private bool stopBtnClick(){
//why
//		foreach(AudioSource eftMusic in MusicManager.Instance.effectMusicObjs){
//			if(eftMusic.isPlaying && eftMusic.clip == MusicManager.Instance.getAudioClipByName("SFX_Chapter_End_Victory_Temp_1a")){
//				return true;
//			}
//		}	
		return false;
	}
	
	private IEnumerator delayedOnHomeBtnClick ()
	{
		yield return new WaitForEndOfFrame();

		GotoProxy.gotoScene (GotoProxy.UIMAIN);	
		LevelMgr.Instance.OutBattle();
//		StaticData.paused = false;
//		SkillIconManager.Instance.destroyAllSkillIconDataList();
//		ComboController.Instance.destroyAllComboIconDataList();
//		SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//		HeroMgr.clear();
//		BattleBg.Instance.DestroyAllObj();
//		LevelMgr.Instance.DestroyAllObj();
		GotoProxy.gotoScene (GotoProxy.UIMAIN);		
	}

	public void onNextBtnClick (){
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(stopBtnClick()) return;
		else{
			MusicManager.Instance.replayBgMusic("SFX_Chapter_End_Victory_Temp_1a");
		}
		StartCoroutine(delayedOnNextBtnClick());	
		//MusicManager.Instance.replayBgMusic("SFX_Chapter_End_Victory_Temp_1a");
	}
	private IEnumerator delayedOnNextBtnClick ()
	{
		yield return new WaitForEndOfFrame();
		if(MapMgr.Instance.currentLevelIndex == 12){
			HomePageDlg.reservedDlg = "ChapterSelect";
			LevelMgr.Instance.OutBattle();
			GotoProxy.gotoScene (GotoProxy.UIMAIN);		
		}else{
	//		LevelMgr.Instance.outBattleRewardHero();
			LevelMgr.Instance.PlayCutscenes();
			MapMgr.Instance.nextLevel();
			LevelMgr.Instance.MoveBGTexture();
			LevelMgr.Instance.gotoNextLevelPrepare();
			BattleBg.Instance.DestroyAllObj();
			LevelMgr.Instance.DestroyAllObj();
			LevelMgr.Instance.initEnemy();
			LevelMgr.Instance.PlayBgMusic();
			//BattleBg.Instance.createAllHeroStateDialog();
			TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}-{2}", 
				TsFtueManager.LEVEL_GO_NEXT, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
			Destroy(gameObject);
		}
	}

	public void onReplayBtnClick (){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(stopBtnClick()) return;
		else{
			MusicManager.Instance.replayBgMusic("SFX_Chapter_End_Victory_Temp_1a");
		}
		StartCoroutine(delayedOnReplayBtnClick());	
		//MusicManager.Instance.replayBgMusic("SFX_Chapter_End_Victory_Temp_1a");
	}
	private IEnumerator delayedOnReplayBtnClick ()
	{
		yield return new WaitForEndOfFrame();
		LevelMgr.Instance.OutBattle();
//		StaticData.paused = false;
//		SkillIconManager.Instance.destroyAllSkillIconDataList();
//		ComboController.Instance.destroyAllComboIconDataList();
//		SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//		HeroMgr.clear();
//		BattleBg.Instance.DestroyAllObj();
//		LevelMgr.Instance.DestroyAllObj();
		GotoProxy.fadeInScene (GotoProxy.BATTLESCENE);
	}

	public void jumpToRaidList ()
	{
	}

	public void gotoToEquip ()
	{
		BattleBg.Instance.DestroyAllObj();
		LevelMgr.Instance.DestroyAllObj();
		CacheMgr.loadScene (GotoProxy.ARMORY);
		GotoProxy.fadeInScene (GotoProxy.ARMORY);
	}	
}
