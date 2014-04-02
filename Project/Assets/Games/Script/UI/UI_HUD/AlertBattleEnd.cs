using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlertBattleEnd : MonoBehaviour
{
	public UILabel chapterLevelText;
	public UIButton homeBtn;
	public UIButton nextBtn;
	public UIButton tryAgainBtn;
	public UISprite star1;
	public UISprite star2;
	public UISprite star3;
	public UISprite star4;
	
	
	public GameObject rewardSliverObj;
	public GameObject rewardGoldObj;
	public GameObject rewardCommandPointsObj;
	public GameObject rewardItemObj;
	public GameObject rewardHeroObj;
	
	public GameObject rewardObj;
	
	public Camera heroCamera;
	public int score
	{
		set
		{
			scoreText.text = "Score : " + value;
		}
	}
		
	public int silver = 0;
	public int gold = 0;
	public int commandPoints = 0;
	
	public UILabel scoreText;
	public UILabel silverValue;
	public UILabel goldValue;
	public UILabel commandPointsValue;
	
	
	public int rewardItemId;
	public string rewardItemIconId;
	public UISprite rewardItemIcon;
	
	
	public string rewardHeroType;
	public SimpleSprite rewardHeroHeadIcon;

		
	[HideInInspector]
	public bool isWin = true;
	[HideInInspector]
	public int winStar = 0;
	
	private Color StarLight = new Color(1,1,1,1);
	private Color StarDark = new Color(0,0,0,1);
	
	public UIAtlas isoAtlas;
	public UIAtlas equipAtas;
	
	public void Start()
	{
		rewardGoldObj.SetActive(false);
		rewardCommandPointsObj.SetActive(false);
	}
	
	public void init ()
	{	
		if (isWin) {
			MapMgr.Instance.getCurrentLevel ().winStars = winStar;
			//GData.currentLevel += 1;
			nextBtn.isEnabled = true;
			star1.color = (winStar >= 1)?StarLight:StarDark;
			star2.color = (winStar >= 2)?StarLight:StarDark;
			star3.color = (winStar >= 3)?StarLight:StarDark;
			star4.color = (winStar >= 4)?StarLight:StarDark;
			StartCoroutine(delayedShowBonus());
		} else {
			star1.color = StarDark;
			star2.color = StarDark;
			star3.color = StarDark;
			star4.color = StarDark;
			nextBtn.isEnabled = false;
		}
		
		chapterLevelText.text = string.Format ("Planet {0}-{1}", MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_AlertBattleEnd_{1}-{2}", 
			TsFtueManager.DIALOG_OPENED, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
	}
	
	private IEnumerator delayedShowBonus(){
		
		yield return new WaitForSeconds(1);
		showReward();
		iTween.MoveTo(heroCamera.gameObject,
			iTween.Hash(
			"position", new Vector3(282766,0,0),
			"time",1.5f,
			"easytype",iTween.EaseType.linear,
			"islocal",true
//			,
//			"oncomplete","showReward",
//			"oncompletetarget",gameObject
			));
		
		iTween.MoveTo(rewardObj,
			iTween.Hash(
			"position", new Vector3(0,0,0),
			"time",1.5f,
			"easytype",iTween.EaseType.linear,
			"islocal",true
//			,
//			"oncomplete","showReward",
//			"oncompletetarget",gameObject
			));
	}
	
	protected void delayedShowBonus2()
	{
		
	}
	
	float y = -37.72858f;
	
	protected void showReward()
	{
		silverValue.text = silver.ToString();
		
		goldValue.text = gold.ToString();
		
		commandPointsValue.text = commandPoints.ToString();
		
		rewardItemObj.SetActive(false);
		rewardHeroObj.SetActive(false);
		
		if(rewardItemIconId != "")
		{
//			Texture icon = Resources.Load("itemIcons/"+ rewardItemIconId) as Texture;
//			rewardItemIcon.gameObject.renderer.material.mainTexture = icon;
			
			string defaultIconName = "GearDefault";
			
			
			if(rewardItemId >= 4011 && rewardItemId <= 4047)
			{
				rewardItemIcon.atlas = this.isoAtlas;
				defaultIconName = "ISO_default";
			}
			else
			{
				rewardItemIcon.atlas = this.equipAtas;
			}
			
			rewardItemIcon.spriteName = rewardItemIconId;
			if(rewardItemIcon.GetAtlasSprite() == null)
			{
				rewardItemIcon.spriteName = defaultIconName;	
			}
			rewardItemIcon.MakePixelPerfect();
			rewardItemObj.SetActive(true);
		}
		
		if(rewardHeroType != "")
		{
			Texture icon = Resources.Load("HeroHead/"+ rewardHeroType) as Texture;
			rewardHeroHeadIcon.gameObject.renderer.material.mainTexture = icon;
			rewardHeroObj.SetActive(true);
		}
		
		if(rewardItemIconId != "" && rewardHeroType == "")
		{
			rewardSliverObj.transform.localPosition = new Vector3(-161,y,0);
			rewardItemObj.transform.localPosition = new Vector3(123,y,0);
//			rewardGoldObj.transform.localPosition = new Vector3(-52,y,0);
//			rewardCommandPointsObj.transform.localPosition = new Vector3(110,y,0);
			
		}
		else if(rewardItemIconId == "" && rewardHeroType != "")
		{
			rewardSliverObj.transform.localPosition = new Vector3(-161,y,0);
			rewardHeroObj.transform.localPosition = new Vector3(123,y,0);
//			rewardGoldObj.transform.localPosition = new Vector3(-52,y,0);
//			rewardCommandPointsObj.transform.localPosition = new Vector3(110,y,0);
			
		}
		else if(rewardItemIconId == "" && rewardHeroType == "")
		{
			rewardSliverObj.transform.localPosition = new Vector3(-53,y,0);
//			rewardGoldObj.transform.localPosition = new Vector3(-141,y,0);
//			rewardCommandPointsObj.transform.localPosition = new Vector3(-30,y,0);
			
			
		}
	}
	
	public void homeBtnClicked ()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		StartCoroutine(delayedOnHomeBtnClick());	
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
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		StartCoroutine(delayedOnNextBtnClick());	
	}
	private IEnumerator delayedOnNextBtnClick ()
	{
		yield return new WaitForEndOfFrame();
//		LevelMgr.Instance.outBattleRewardHero();
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

	public void onReplayBtnClick (){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		StartCoroutine(delayedOnReplayBtnClick());	
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