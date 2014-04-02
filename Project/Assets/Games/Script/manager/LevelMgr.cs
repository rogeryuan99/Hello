using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMgr : MonoBehaviour
{
	// Jugg
	public UILabel tipTxt;
	public GameObject expBarPreb;
	
	// Jugg
	public UILabel arenaEnemyDead;
	
	public GameObject HUD_CCD;
	public GameObject HUD_RL;
	public GameObject HUD_Skill;
	public GameObject pauseButton;
	
	private bool  lostChar = false;
	public Hashtable enemyObjectTable;
	public GameObject dlgPrefab_battleEnd;
	public static ArrayList deadHeroes;//add by xiaoyong 20120908

	private int wavesMax;
	public int wavesCurrent=0;
	public int cumulateEnemies=0;
	private ArrayList wavesData;
	private int waveMax;
	private int waveCurrent;
	private ArrayList waveData;
	private int deadCount = 0;
	private int heroCount;
	private ArrayList birthPt = new ArrayList (){new Vector2 (-590, -300),new Vector2 (-590, 50),new Vector2 (590, -300),new Vector2 (590, 50)};
	private ArrayList birthPt_ip5 = new ArrayList (){new Vector2 (-720, -300),new Vector2 (-720, 50),new Vector2 (720, -300),new Vector2 (720, 50)};
//	private ArrayList birthPt = new ArrayList (){new Vector2 (0, 0),new Vector2 (-0, 0),new Vector2 (0, -0),new Vector2 (0, 0)};
//	private ArrayList birthPt_ip5 = new ArrayList (){new Vector2 (-0, -0),new Vector2 (-0, 0),new Vector2 (0, -0),new Vector2 (0, 0)};
	private float poolExp;
	private float poolSilver;
	private Hashtable poolEnemies;
//	private bool  arena = false;
	private ArrayList priceRange;
	private float dif = 0;
	private float difficulty = 2;
	private int athleticsIntegral = 0;
	private int enemyDeadNum = 0;
	private int amountEnemy = 0;
	
	public bool isRebirth;
	
//add by gwp for victory animation
//	private int expCompleteCount = 0;
	private Hashtable moveCompleteHero = new Hashtable ();
//<===gwp end

	public GameObject shielded;
	
	public static ArrayList destroyObjArray = new ArrayList();
		
	public GameObject battleButtonObj;//startbattle
	
	public UISprite battleButtonBG;
	
	public DynamicBackground dynamicBackground;
	
	public Cutscenes cutscenes;
	
	
	public List<GameObject> allEnemies = new List<GameObject>();
	private static LevelMgr _instance;
	[HideInInspector]
	public static LevelMgr Instance{
		get{
			return _instance;
		}
	}
	
	void Update()
	{
			
	}

	public static bool checkLevelResource (int levelNum, string planetName)
	{
		return true;
		//Delete by xiaoyong 20130529 , For remove dynamic download--->
//	ArrayList allData = MiniJSON.jsonDecode(planetAInfo);
//	for( int i =0; i<allData.Count ; i++)
//	{
//		Hashtable lvData = allData[i];
//		if(lvData["level"] == GData.currentLevel && lvData["planet"] == GData.currentPlanet)
//		{
//			ArrayList enemyList = lvData["wave"];
//			string mapName   = lvData["map"];
//			bool  mapLoad = CacheMgr.instance.bgIsCached( mapName );
//			bool  enemyLoad = CacheMgr.instance.enemyIsCached( enemyList );
//			if( mapLoad && enemyLoad )
//			{
//				return true;
//			}
//			break;
//		}
//		
//	}
//	return false;
//<----
	
	}

	public void getResolutionPosition ()
	{
#if UNITY_ANDROID || UNITY_EDITOR
//	float widthInInches = Screen.width/(Screen.height/320.0f);
//	ExtensionMethods.SetX(HUD_Pause.transform.position, widthInInches-70);
#endif
	}

	public void moveHUD ()
	{
		if (StaticData.isiPhone5) {
			ExtensionMethods.SetX (HUD_CCD.transform.position, -93);
//		HUD_CCD.transform.localPosition.x = -93;
			ExtensionMethods.SetX (HUD_RL.transform.position, -176);
//		HUD_RL.transform.localPosition.x = -176;
			ExtensionMethods.SetX (HUD_Skill.transform.position, -331);
//		HUD_Skill.transform.localPosition.x = -331;
//			ExtensionMethods.SetX (HUD_Pause.transform.position, 535);
//		HUD_Pause.transform.localPosition.x = 535;
		}
		getResolutionPosition ();
	}

	public void setClearCDState ()
	{
	}
	
	public void startBattle()
	{
		isRebirth = true;
		
		score = 0;
		
		StaticData.isBattleEnd = false;	
		
		enemyDeadNum = 0;
		
		setClearCDState();
		
		//MusicManager.playEffectMusic("SFX_battle_starts_1a");
		
		MsgCenter.instance.addListener (MsgCenter.ENEMY_DEAD, enemyDead);
		MsgCenter.instance.addListener (MsgCenter.HERO_DEAD, heroDead);
		MsgCenter.instance.addListener (MsgCenter.FALL_DOWN, fallDown);
		MsgCenter.instance.addListener (MsgCenter.CREATE_START_Battle, createBattle);
		//MsgCenter.instance.addListener (ExpBar.ADD_EXP_COMPLETE, expComplete);
		MsgCenter.instance.addListener (MsgCenter.CONSUME_ITEM_RELIVE, onHeroRelive);
		
		lostChar = false;
		
		heroCount = HeroMgr.heroHash.Count;
		deadHeroes = new ArrayList ();
		
		foreach (string key2 in HeroMgr.heroHash.Keys)
		{
			Hero tempHero = HeroMgr.heroHash [key2] as Hero;
			Message loadTextureEvt2 = 
				new Message (
					TargetIndicator.LOAD_TEXTURE_EVENT, 
					this, 
					tempHero.data.type
				);
			loadTextureEvt2.name = TargetIndicator.LOAD_TEXTURE_EVENT;
			loadTextureEvt2.sender = this;
			MsgCenter.instance.dispatch (loadTextureEvt2);
			tempHero.startCheckOpponent();
		}
	
		moveHUD ();
		createEnemy ();
		TrackDMgr.levelStartTime = TrackDMgr.Instance.getTimeStamp ();
	}

	public void setBGTexture()
	{
		Level currentLevel = MapMgr.Instance.getCurrentLevel ();
		Level nextLevel = MapMgr.Instance.getNextLevel ();
		
		dynamicBackground.init(currentLevel,nextLevel);
	}
	
	public void gotoNextLevelPrepare()
	{
		foreach (Hero hero in HeroMgr.heroHash.Values)
		{
			hero.pieceAnima.restart();
			hero.cancelCheckOpponent();
			hero.toward(new Vector3(5000, 0 , 0));
			hero.playAnim("Move");
		}
		
		int i = 0;
			
		foreach(Hero hero in deadHeroes)
		{
			hero.relive();
			HeroMgr.heroHash[hero.id] = hero;
			hero.gameObject.transform.position = new Vector3(-720 + i * 90 ,13, StaticData.objLayer);
			i++;
		}
	}
	
	
	public void MoveBGTexture()
	{	
		Level nextLevel = MapMgr.Instance.getNextLevel ();
		dynamicBackground.GotoLevel(nextLevel);
		//StartCoroutine(MoveBGTextureFinish());
	}
	
	public void PlayCutscenes(){
		cutscenes.GotoLevel(MapMgr.Instance.getCurrentLevel());	
	}
	
	
	public void MoveBGTextureFinish()
	{
	//	yield return new WaitForSeconds(1.0f);
//		if (!TsTheater.InTutorial) 
//			LevelMgr.Instance.pauseButton.SetActive(true);
			//GameObject.Find("PauseButton").SendMessage("Normal");
		//setBGTexture();
		if (true == BarrierMapData.Enable){
			initBarrierMapData();
		}
//		BGTexture.transform.localPosition = new Vector3(0,BGTexture.transform.localPosition.y,BGTexture.transform.localPosition.z);
		
//		SimpleSprite sp = BGTexture.GetComponent<SimpleSprite>();
//		Vector3 rp = BGTextureNext.transform.localPosition;
//		rp.x = BGTexture.transform.localPosition.x + sp.width;
//		BGTextureNext.transform.localPosition = rp;
		
		BattleBg.Instance.heroMoveToBattleStartPos();
	}

	public void PlayBgMusic ()
	{
		// if (NewLevelMgr.Instance.getCurrentLevel ().boss == Level.Boss.None) {
		// 	MusicManager.playBgMusic ("bg_battle");
		// } else {
		// 	MusicManager.playBgMusic ("bg_battle_boss");
		// }
		
		//MusicManager.playBgMusic(MapMgr.Instance.getCurrentLevel().bgMusic);
		
		if(cutscenes.isEnd){
			MusicManager.playBgMusic(MapMgr.Instance.getCurrentLevel().bgMusic);		
		}else{
			MusicManager.playBgMusic("MUS_UI_Menus");	
		}
	}


	public void createBattle (Message msg)
	{
		createEnemy ();
	}
	
	private bool isBlockedEnemyCreation = false;
	public void BlockEnemyCreation()   { isBlockedEnemyCreation = true; }
	public void UnblockEnemyCreation() { isBlockedEnemyCreation = false; }
	public void createEnemy ()
	{	
		if(true == isBlockedEnemyCreation || 
			wavesData.Count == 0)
		{
			return; // only tutorial has no wave	
		}
		if (wavesCurrent < wavesMax) 
		{
			startInvok ();
		} else {
				victoryStepsBegin ();
//			print("no more enemy create");
			
		}
	}

	public void startInvok ()
	{
	//	Debug.LogError("wavesCurrent="+wavesCurrent);
		wavesCurrent++;
		if(MapMgr.Instance.isArena){
			difficulty += 0.3f;	
		}
//		Debug.LogError("wavesCurrent="+wavesCurrent);
		waveCurrent = 1;
		if(MapMgr.Instance.isArena){
			BattleBg.Instance.WaveTxt.text = string.Format( Localization.instance.Get("UI_arena_wave"), wavesCurrent);
			BattleBg.Instance.WaveTxtCenter.text = string.Format( Localization.instance.Get("UI_arena_wave"), wavesCurrent);
			
			StartCoroutine(delayedHideWaveTxt());
		}
		waveData = wavesData [(wavesCurrent  - 1)% wavesData.Count] as ArrayList;
		waveMax = waveData.Count;
		InvokeRepeating ("createEnemyInvoke", 2f, 4);
	}
	private IEnumerator delayedHideWaveTxt(){
		yield return new WaitForSeconds(2f);
		BattleBg.Instance.WaveTxtCenter.text = "";
	}
	
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	public void createEnemyInvoke ()
	{
		string[] format = waveData [waveCurrent - 1].ToString ().Split(':');
		string type = format[0];
		GameObject enemyObj = null;
		if (StaticData.isTouch4)
		{
			enemyObj = createEnemyByType (type);
		}
		else 
		{
			ArrayList enemyObjAry = poolEnemies [type] as ArrayList;
		
			enemyObj = enemyObjAry [0] as GameObject;
			enemyObjAry.RemoveAt (0);
		}
		
		Character enemyDoc = enemyObj.GetComponent<Character> ();
		if(false == StaticData.isPVP && format.Length > 1)
		{
			string[] p = format[1].Split(',');
			(enemyDoc as Enemy).BirthPtInProfile = new Vector2(float.Parse(p[0]), float.Parse(p[1]));
		}
		if(StaticData.isPVP)
		{
			if((enemyDoc as Hero).heroAI == null)
			{
				enemyDoc.setCharacterAI(typeof(EnemyHeroAI));
			}
		}
		else
		{
			if((enemyDoc as Enemy).enemyAI == null)
			{
				enemyDoc.setCharacterAI(typeof(EnemyAI));
			}
		}		
		enemyDoc.id = EnemyMgr.getID();
		EnemyMgr.enemyHash[enemyDoc.id] = enemyDoc;
	
		enemyDoc.enemyType = type;
		
		if(StaticData.isPVP)
		{
			(enemyDoc as Hero).relive ();
			enemyDoc.startCheckOpponent();
		}
		else
		{
			(enemyDoc as Enemy).relive ();
			enemyDoc.realHp = (int)(enemyDoc.realHp * difficulty);
			enemyDoc.realDef.Multip(difficulty);
			enemyDoc.realAtk.Multip(difficulty);
			enemyDoc.realMaxHp = enemyDoc.realHp;
			
			
			enemyDoc.hpBar.initBar (enemyDoc.realHp);
			enemyDoc.resetAtkSpd();
			enemyDoc.resetMoveSpd();
		}
		
		waveCurrent++;	
		if ( waveCurrent > waveMax )
		{
			CancelInvoke ("createEnemyInvoke");
		}
		
		//DestroyObjManager.Instance.destroyObjArray.Add(enemyObj);
		LevelMgr.destroyObjArray.Add(enemyObj);
	}
	
//	public Vector2 getBirthPt (float x, float y){
//		x = BattleBg.actionBounds.size.x /2f * x;
//		y = BattleBg.actionBounds.size.y /2f * y;
//		return new UnityEngine.Vector2(x, y);
//	}
	public Vector2 getBirthPt ()
	{
		if (StaticData.isiPhone5) {
			return (Vector2)birthPt_ip5 [(int)(Random.Range (0, birthPt.Count))];
		} else {
			return (Vector2)birthPt [(int)(Random.Range (0, birthPt.Count))];
		}
	}

	public void enemyDead (Message msg)
	{
//		Debug.LogError("enemyDead");
		if (isBlockedEnemyCreation)
			return; 
		athleticsIntegral++;
		deadCount++;
		enemyDeadNum++;
	
		Character enemy = msg.sender as Character;
	
		if (!StaticData.isTouch4) 
		{
			//enemy pool  callback enemy
			ArrayList enemyObjAry = poolEnemies [enemy.enemyType] as ArrayList;
			if(enemyObjAry != null)
			{
				enemyObjAry.Add (enemy.gameObject);
				poolEnemies [enemy.enemyType] = enemyObjAry;
			}
		}
		
		poolExp += enemy.data.rewardExp * difficulty * 1.0f;
		poolSilver += enemy.data.rewardSilver * difficulty * 1.0f;
		
//	print("waveNumber:"+wavesCurrent+" deadCount:"+deadCount);
		if (deadCount == waveMax)
		{
			deadCount = 0;
			createEnemy ();
		}
	}

	public void heroDead (Message msg)
	{
		MusicManager.playEffectMusic("SFX_character_die_2a");
		lostChar = true;
		heroCount--;
		if (heroCount < 1) {	
			Debug.LogError("defeated");
			if(MapMgr.Instance.isArena){
				//arena finished
				StartCoroutine (victory_step4_showDlg());
			}else{
				StartCoroutine( levelDefeat () ); 
			}
			
		}
	}

	public void onHeroRelive (Message msg)
	{
		heroCount++;
	}

	public void jumpToRaidList ()
	{
	
	}

	public void gotoToMainMenu ()
	{
//	MapMgr.checkUnlockLevel();
		EnemyMgr.clear ();
		HeroMgr.clear ();
		Time.timeScale = 1.0f;
		//GData.paused = false;
		StaticData.isBattleEnd = true;
		GotoProxy.fadeInScene (GotoProxy.MAIN_MENU);
		GotoProxy.setSceneName (GotoProxy.MAIN_MENU);
	}

	public void hidePauseMenu ()
	{
		GameObject cam = GameObject.Find ("ButtonScript").gameObject;
	}

	public IEnumerator showDefeatMenu ()
	{
		yield return new WaitForSeconds(1);
	}

	private void levelEndAchievements ()
	{
		if (MapMgr.Instance.getCurrentLevel ().boss == Level.Boss.finalBoss) {
			if (!lostChar) {
				AchievementManager.updateAchievement ("BEAT_BOSS_NO_LOSS", 1);
			}
		}
		AchievementManager.updateAchievement ("LEVEL_COMPLETE", 1);
	
	}

	public GameObject lbg;

	public void showLoadingBG ()
	{
		//lbg.transform.localPosition = new Vector3(0,0,-500);
	}

	public void hideLoadingBG ()
	{
		//lbg.transform.localPosition = new Vector3(2000, 2000, -500);
	}
	
	private Hashtable victoryBonus(){
		Hashtable h = new Hashtable();
		//h.Add("stars",MapMgr.Instance.getCurrentLevel().winStars);
		int oldStar=MapMgr.Instance.getCurrentLevel().winStars;
		int newStar= Mathf.Min(3,oldStar +1);//rewardStar();
		MapMgr.Instance.setBonusStarForCurrentLv(newStar);		
		levelEndAchievements();
		//MusicManager.playEffectMusic("SFX_Score_Star_1a");
		poolSilver = Mathf.Ceil (poolSilver * (GlobalModifier.gold / 100.0f + 1));
		UserInfo.instance.addSilver ((int)poolSilver);
		h.Add("silver",(int)poolSilver);	
		h.Add("exp",rewardExp( oldStar));
		if(!MapMgr.Instance.isArena){
			h.Add("star",newStar);
			if(oldStar == 0){
				h.Add("equipData", rewardItem());
				h.Add("herodata",rewardHero());
				h.Add("cp",rewardCp());
			}
		}else{
			h.Add("totalWaves",wavesCurrent);
			h.Add("totalEnemies",enemyDeadNum);
			h.Add("cp", (int)poolSilver);
		}
		
		UserInfo.instance.saveAll();

		poolExp = 0;
		poolSilver = 0; 
		
		return h;
	}
//	private int rewardStar(){
//		int totalHero = 0;
//		int dieHero = 0;
//		//no hero die = 3 star
//		//some die = 2 star
//		// >50% die = 1 star
//		foreach (Hero h in HeroMgr.heroHash.Values) {
//			totalHero ++;
//			if(h.realHp <=0){
//				dieHero ++;	
//			}
//		}
//		if(dieHero == 0){
//			return 3;
//		}else if( (float)dieHero/(float)totalHero < 0.5f){
//			return 2;	
//		}else{
//			return 1;	
//		}
//	}
	private int rewardCp(){
		int cp = Formulas.rewardCommandPoints(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
		UserInfo.instance.addCommandPoints(cp);
		return cp;
	}
	private int rewardExp(int oldStars){
//		float tempExp = poolExp * (heroD.itemDM.exp / 100.0f + 1);
//		float addExp = Mathf.Ceil (tempExp) - Mathf.Ceil (tempExp) * ((heroD.lv - 1) / (heroD.lv + 20)) / 2f + 5;//add 5 by roger
		float addExp = poolExp + 5;
		if((MapMgr.Instance.currentLevelIndex == 1 || 
			MapMgr.Instance.currentLevelIndex == 2 ||
			MapMgr.Instance.currentLevelIndex == 3 ||
			MapMgr.Instance.currentLevelIndex == 4) && 
			MapMgr.Instance.currentChapterIndex == 1)
		{
			switch(oldStars){
				case 0:
					addExp = 4f;
					break;
				case 1:
					addExp = 4f;
					break;
				case 2:
					addExp = 4f;
					break;
				default:
					addExp = 0;
					break;
			}
		}else{
			switch(oldStars){
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				addExp = 0;
				break;
			}
		}
		return (int)addExp;
	}
	private EquipData rewardItem()
	{
		int i = Random.Range(0,101);
		int id = 2000;
		
		bool isRewardIso = false;
		
		if(i >= 0 && i < 25)
		{
			id = Random.Range(2000,2006);
		}
		else if(i >= 25 && i < 50)
		{
			id = Random.Range(2006,2011);
		}
		else if(i >= 50 && i < 75)
		{
			isRewardIso = true;
			id = Random.Range(4011,4032);
		}
		else if(i >= 75 && i < 100)
		{
			isRewardIso = true;
			id = Random.Range(4032,4048);
		}
		
		
		if(MapMgr.Instance.currentChapterIndex == 1 && MapMgr.Instance.currentLevelIndex == 1)
		{
			id = 2003;
			isRewardIso = false;
		}
		
		EquipData rewardEquip = null; 
		if(isRewardIso)
		{
			foreach(EquipData ed in EquipManager.Instance.inventoryItemList)
			{
				if(ed.equipDef.id == id)
				{
					ed.count++;
					rewardEquip = ed;
					break;
				}
			}
		}
		else
		{
			rewardEquip = EquipManager.Instance.allEquipHashtable[id] as EquipData;
			EquipData equipData = rewardEquip.clone();
			equipData.initUidTemp();
			EquipManager.Instance.inventoryItemList.Add(equipData);
		}
		return rewardEquip;
	}

	private HeroData rewardHero()
	{
		if(MapMgr.Instance.currentChapterIndex == 1)
		{
			string rewardHeroType = "";
			if(MapMgr.Instance.currentLevelIndex == 2)
			{
				rewardHeroType = HeroData.DRAX;
			}
			else if(MapMgr.Instance.currentLevelIndex == 6)
			{
				rewardHeroType = HeroData.GAMORA;
			}
			else if(MapMgr.Instance.currentLevelIndex == 9)
			{
				rewardHeroType = HeroData.GROOT;
			}
			else if(MapMgr.Instance.currentLevelIndex == 12)
			{
				rewardHeroType = HeroData.ROCKET;
			}
			
			if(rewardHeroType != "")
			{	
				for(int i=0; i<UserInfo.heroDataList.Count; i++)
				{
					HeroData heroData = UserInfo.heroDataList[i] as HeroData;
					if(heroData.type == rewardHeroType)
					{
						heroData.state = HeroData.State.UNLOCKED_NOT_RECRUITED;
						return heroData;
						break;
					}
					
				}
			}
		}
		return null;
	}
	
	
	private IEnumerator victory_step1_beginMove ()
	{	
		yield return new WaitForSeconds(1);
		LevelMgr.Instance.dynamicBackground.OnSuccess(true);//door
		yield return new WaitForSeconds(1); 
		moveCompleteHero.Clear ();
		
		moveCompleteHeroCurrentCount = 0;
		moveCompleteHeroCount = 0;
		
		foreach (Hero h in HeroMgr.heroHash.Values) 
		{
			Vector3 moveToPos = BattleBg.Instance.getHeroStartPosByHeroType(h.data.type);
			
			h.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveComplete);
			
			if (h.getState () == "CAST_STATE") {
				Debug.LogError("h.victoryMoveHandler");
				h.victoryMoveHandler (moveToPos);
			} else {
//				Debug.LogError("h.move");
				h.move (moveToPos);
			}
			
			moveCompleteHeroCount++;
		}
		
		
	}
	
	
//	public void outBattleRewardHero()
//	{
//		if(rewardHeroObj == null)
//		{
//			return;
//		}
//		Hero hero = rewardHeroObj.GetComponent<Hero>();
//		rewardHeroObj.transform.position = new Vector3(-720,-50, GData.objLayer);
//		hero.model.transform.localScale = new Vector3(0.8f,0.8f,1);
//		if(HeroMgr.heroHash.Count > 4)
//		{
//			HeroMgr.delHero(hero.id);
//			Destroy(rewardHeroObj);
//		}
//		rewardHeroObj = null;
//		rewardHeroType = "";
//	}

//add by gwp for victory animation
	void battleEnd ()
	{
		StaticData.isBattleEnd = true;
		foreach (string key in HeroMgr.heroHash.Keys)
		{
			Hero hero = HeroMgr.heroHash [key] as Hero;
			HeroData heroD = hero.data as HeroData;
			if (heroD.type == HeroData.MANTIS) 
			{
				(hero as Mantis).stopHeal ();
			}
			hero.battleEnd ();
		}
	}

	protected int moveCompleteHeroCurrentCount = 0;
	protected int moveCompleteHeroCount = 0;
	

	void moveComplete (Character character)
	{
		character.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveComplete);
		
		character.standby ();
		character.selecting ();
		
		moveCompleteHeroCurrentCount ++;
		
		if (moveCompleteHeroCount == moveCompleteHeroCurrentCount) 
		{
//			Debug.LogError("if (moveCompleteHeroCount == moveCompleteHeroCurrentCount) ");
			StartCoroutine (victory_step3_celebrate ());
		}
	}
	
	int score = 0;
	
	IEnumerator victory_step3_celebrate ()
	{
		yield return new WaitForSeconds(0.01f);
		float[] time = new float[]{0.4f,0.3f,0.2f,0.1f};
		int index = 0;
	
		tipTxt.text = "";
		
		score = 0;
		
		foreach (Hero hero in HeroMgr.heroHash.Values)
		{
			HeroData heroD = hero.data as HeroData;
			int lv = heroD.lv;
			
			hero.cancelCheckOpponent();
			hero.playAnim ("Celebrate");
			
		}
		StartCoroutine (victory_step4_showDlg());
	}
			
//			//Debug.LogError("hero exp current "+heroD.nickName+":"+heroD.exp + " Add:"+addExp);
//			//Debug.LogError("expbar "+string.Format("LV:{4}, treshhold:{0} , {1}, {2}, {3}",(int)HeroData.expList [lv - 1], heroD.exp+10, hero, expTxt,lv));
////			heroD.addExp ((int)addExp);
//			expBar.expTxt.text = Mathf.Ceil (addExp) + " " + "Exp";
//			int lvUpNum = heroD.lv - lv;
//			if ((heroD.lv - 1) < HeroData.expList.Count) {
//				StartCoroutine (expBar.ChangeExpHandler (heroD.exp, lvUpNum, (int)HeroData.expList [heroD.lv - 1], (float)time [index]));
//			} else {
//				StartCoroutine (expBar.ChangeExpHandler ((int)HeroData.expList [HeroData.expList.Count - 1], lvUpNum, (int)HeroData.expList [HeroData.expList.Count - 1], (float)time [index]));
//			}		
//			
//			index++;
//			yield return new WaitForSeconds(0.1f);
//		}
//		yield return new WaitForSeconds(2f);
//		UserInfo.instance.saveAllheroes();
//	}
	
	public delegate void OnDialogOpenedDelegate();
	public OnDialogOpenedDelegate OnVictoryDialogOpened;
	IEnumerator victory_step4_showDlg ()
	{
		Hashtable rewards = this.victoryBonus();
		
		Debug.Log("victoryHandler_showAlert");
		Resources.UnloadUnusedAssets();
		yield return new WaitForSeconds(1.0f);
//		GameObject alertObj = Instantiate (this.alert, new Vector3 (-22, 97, -400), this.transform.rotation) as GameObject;
//		GameObject dlg_battleEnd = Instantiate (this.dlgPrefab_battleEnd, new Vector3 (-22, 200, -400), this.transform.rotation) as GameObject;
//		Transform alertAnchor = GameObject.Find ("AlertAnchor").transform;
//		dlg_battleEnd.transform.parent = alertAnchor;
//		dlg_battleEnd.transform.localPosition = new Vector3(0, 50, 0);
//		dlg_battleEnd.transform.localScale = Vector3.one;
//		AlertBattleEnd alertBattleEnd = dlg_battleEnd.GetComponent<AlertBattleEnd> ();
//		alertBattleEnd.isWin = true;
//		alertBattleEnd.winStar = MapMgr.Instance.getCurrentLevel().winStars;
//		alertBattleEnd.silver = this.score;
//		

//		
//		alertBattleEnd.init ();
		
		GameObject dlg_battleEnd = Instantiate (this.dlgPrefab_battleEnd) as GameObject;
		Transform alertAnchor = GameObject.Find ("AlertAnchor").transform;
		dlg_battleEnd.transform.parent = alertAnchor;
		dlg_battleEnd.transform.localPosition = new Vector3(0, 0, 0);
		dlg_battleEnd.transform.localScale = Vector3.one;
		BattleEndDlg battleEndDlg = dlg_battleEnd.GetComponent<BattleEndDlg> ();
		battleEndDlg.victory(rewards,delegate(){
			if (null != OnVictoryDialogOpened)
			{
				Debug.Log("battleEndDlgxxxxxxxx");
				OnVictoryDialogOpened();
			}		
		});
	}
//<===gwp end
//	IEnumerator victoryHandler_show_levelMastery()
//	{
//		yield return new WaitForSeconds(1.0f);
//		int rewardCP = Formulas.rewardCommandPoints(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
//		UserInfo.instance.addCommandPoints(rewardCP);
//		string s = " LEVEL  MASTERY! \n Bonus: [CPoint] " + rewardCP;
//		CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//		dlg.setOKDlg();
//	}
	

//add by gwp for skill tutorial skillTutorialToStep2
	void victoryStepsBegin ()
	{
		//MusicManager.playEndMusic ("bg_vector");
		//MusicManager.playEffectMusic("SFX_Chapter_End_Victory_Temp_1a",22f);
		playChapterEndVictoryMusic();
		//tipTxt.text = "Victory";
		tipTxt.text = Localization.instance.Get("UI_Battle_Victory");
		tipTxt.transform.position = new Vector3 (0, 0, -320);
		//waveNum = 2;	??
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}-{2}", 
			TsFtueManager.BATTLE_VICTORY, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
		//GameObject.Find("PauseButton").SendMessage("GrayLock");
		LevelMgr.Instance.pauseButton.SetActive(false);
		StartCoroutine (victory_step1_beginMove ());
		// Message msg = new Message (MsgCenter.LEVEL_VICTORY, this);
		// MsgCenter.instance.dispatch (msg);

		AchievementManager.incrAchievement ("10_WINS_IN_A_ROW");
	}
	
	void playChapterEndVictoryMusic(){
		if(MapMgr.Instance.getCurrentChapter().id == 6){
			switch(MapMgr.Instance.getCurrentLevel().id){
			case 4:
			case 6:
			case 12:	
				MusicManager.playEffectMusic("SFX_Chapter_End_Victory_Temp_1a",22f);
				break;
			}				
		}else{
			switch(MapMgr.Instance.getCurrentLevel().id){
			case 4:
			case 8:
			case 12:	
				MusicManager.playEffectMusic("SFX_Chapter_End_Victory_Temp_1a",22f);
				break;
			}
		}
	}

	IEnumerator levelDefeat ()
	{  
		Debug.Log(" levelDefeat ");
		poolExp = 0;
		poolSilver = 0; 		
		//MusicManager.playEffectMusic("SFX_Level_End_Lose_1c",1.5f);
		
		//tipTxt.text = "Defeat";
		tipTxt.text = Localization.instance.Get("UI_Battle_Defeat");
		tipTxt.transform.position = new Vector3 (0, 0, -320);
		
		yield return new WaitForSeconds(1.5f);

		tipTxt.text = "";

		//CommonDlg dlg = DlgManager.instance.ShowCommonDlgSmall("Try gear up your heros or train them with new skills.");
		CommonDlg dlg = DlgManager.instance.ShowCommonDlgSmall(Localization.instance.Get("UI_CommonDlg_Failure"));
		dlg.setYesText("REPLAY");
		dlg.setNoText("HOME");
		dlg.onYes = () => {
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			LevelMgr.Instance.OutBattle();
//			StaticData.paused = false;
//			SkillIconManager.Instance.destroyAllSkillIconDataList();
//			ComboController.Instance.destroyAllComboIconDataList();
//			SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//			
//			HeroMgr.clear();
//			BattleBg.Instance.DestroyAllObj();
//			LevelMgr.Instance.DestroyAllObj();
			GotoProxy.fadeInScene (GotoProxy.BATTLESCENE);
		};
		dlg.onNo = ()=>{
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			GotoProxy.gotoScene (GotoProxy.UIMAIN);
			LevelMgr.Instance.OutBattle();
//			StaticData.paused = false;
//			SkillIconManager.Instance.destroyAllSkillIconDataList();
//			ComboController.Instance.destroyAllComboIconDataList();
//			SkillEnemyManager.Instance.destroyAllSkillIconDataList();
//			HeroMgr.clear();
//			BattleBg.Instance.DestroyAllObj();
//			LevelMgr.Instance.DestroyAllObj();
			GotoProxy.gotoScene (GotoProxy.UIMAIN);	
		};

	}
	
	public void OutBattle()
	{
		StaticData.paused = false;
		SkillIconManager.Instance.destroyAllSkillIconDataList();
//		ComboController.Instance.destroyAllComboIconDataList();
		SkillEnemyManager.Instance.destroyAllSkillIconDataList();
		HeroMgr.clear();
		BattleBg.Instance.DestroyAllObj();
		LevelMgr.Instance.DestroyAllObj();
	}

	private void sendRaidResultMsg (bool success)
	{
		
	}

	private void fallDown (Message msg)
	{
		Character cha = msg.data as Character;
		GameObject obj = cha.gameObject;
		if (obj.tag == "Enemy") {
			enemyFallDown ();
		} else {
			heroFallDown ();
		}
	}

	private void heroFallDown ()
	{
		// xingyihua heroCount is 1 when hero fallDown,heroCount is 0 when hero Dead
		if (heroCount < 2) 
		{
//	 	Debug.Log("heroFallDown----------->"+heroCount);
			StaticData.isBattleEnd = true; 	
		}
	}

	private void enemyFallDown ()
	{ 
//		Debug.LogError("enemyFallDown");
		if (deadCount == waveMax - 1 && wavesCurrent >= wavesMax)
		{
//			Debug.Log("enemyFallDown----------->");
			battleEnd ();
			// Jugg
//			btnBck.controlIsEnabled = false;
		} 
	
	}
	
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	private GameObject createEnemyByType (string type)
	{
		Vector2 vc2 = getBirthPt ();
		if (enemyObjectTable [type] == null)
		{
			enemyObjectTable [type] = CacheMgr.getEnemyPrb (type);//Resources.Load("enemies/enemy"+type);
//		enemyObjectTable[type] = Resources.Load("enemies/enemy"+type);
		} 
		GameObject enemyObj = Instantiate (enemyObjectTable [type] as GameObject, new Vector3 (vc2.x, vc2.y, vc2.y / 10 + StaticData.objLayer), transform.rotation) as GameObject;
		enemyObj.transform.localScale = new Vector3(Utils.characterSize,Utils.characterSize,1);
		
		Character enemyDoc = enemyObj.GetComponent<Character> ();
		CharacterData characterD = null;
		
		if(StaticData.isPVP)
		{
			foreach(HeroData hd in UserInfo.heroDataList)
			{
				if(hd.type == type)
				{
					characterD = hd;
					break;
				}
			}
			enemyDoc.tag = "Enemy";
		}
		else
		{
			characterD = new CharacterData (EnemyDataLib.instance [type] as Hashtable);
		}
		enemyDoc.initData (characterD);
		enemyDoc.model.transform.localScale = enemyDoc.scaleSize;
		allEnemies.Add(enemyObj);
		return enemyObj;
	}

    // Author: XingYiHua _???
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	Hashtable getMaxTypes (ArrayList ary)
	{
		Hashtable hashReturn = new Hashtable ();
		if (null != ary){
			for (int i=0; i<ary.Count; i++) {
				Hashtable tempHash = new Hashtable ();//
				ArrayList array = ary [i] as ArrayList;
				for (int j=0; j<array.Count; j++){
					string type = array[j].ToString().Split(':')[0];
					tempHash [type] = tempHash.ContainsKey(type)? 
										int.Parse(tempHash[type].ToString()) + 1: 1;
				}
				foreach (string k in tempHash.Keys) {
					if (hashReturn.ContainsKey (k)) {
						int hashValue = int.Parse (hashReturn [k].ToString ());
						int val = int.Parse (tempHash [k].ToString ());
						if (hashValue < val) {
							hashReturn [k] = tempHash [k];
						}
					} else {
						hashReturn [k] = tempHash [k];
					}
				}	
			}
		}
		return hashReturn;
	}

	void Awake ()
	{
		_instance = this;
		Level lv = MapMgr.Instance.getCurrentLevel();
		cutscenes.initCutscenes(lv);
		setBGTexture();
		if (true == BarrierMapData.Enable){
			initBarrierMapData();
		}
		
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}-{2}", 
			TsFtueManager.BATTLE_INIT, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
		
		BattleBg.Instance.heroMoveToBattleStartPos();
		PlayBgMusic ();
//		battleButtonObj.SetActive(false);
		
		enemyObjectTable = new Hashtable ();
		poolEnemies = new Hashtable ();
		
		
		initEnemy();
		
	}
	
	private void initBarrierMapData(){
		BarrierMapData.Instance.Load(string.Format("L{0}_{1}",MapMgr.Instance.currentChapterIndex,MapMgr.Instance.currentLevelIndex));
		// BarrierMapData.Instance.Load("Kyln_2");
		BarrierMapData.Instance.CalculateGrid();
		// BarrierMapData.Instance.ShowGrid();
	}
	
	public void initEnemy()
	{	
		enemyObjectTable.Clear();
		poolEnemies.Clear();
		
		Level currentLevel = MapMgr.Instance.getCurrentLevel ();
		
		wavesData = currentLevel.wave;
		wavesMax = (null == wavesData)? 0: wavesData.Count;
		if(MapMgr.Instance.isArena){
			wavesMax = 888888;	
		}

		wavesCurrent = 0;
		Hashtable typesHash = getMaxTypes (wavesData);
		
		if(MapMgr.Instance.currentChapterIndex == 1 && MapMgr.Instance.currentLevelIndex == 1)
		{
			difficulty = 2f;
		}
		else
		{
			difficulty = 2 + 0.5f * (float)currentLevel.chapter.id + 0.3f * (float)currentLevel.id;
		}
		
		if(StaticData.isPVP)
		{
			difficulty = 1;
		}
		if(MapMgr.Instance.isArena){
			difficulty = 2 + 0.5f * (float)currentLevel.chapter.id;	
		}
		int oldStar=MapMgr.Instance.getCurrentLevel().winStars;
		difficulty += oldStar*0.2f;
		
		foreach (string key in typesHash.Keys) 
		{
			Message loadTextureEvt = new Message (TargetIndicator.LOAD_TEXTURE_EVENT, this, key);
			loadTextureEvt.name = TargetIndicator.LOAD_TEXTURE_EVENT;
			loadTextureEvt.sender = this;
		
			MsgCenter.instance.dispatch (loadTextureEvt);
		
			if (!StaticData.isTouch4) 
			{
				int amount = (int)typesHash [key];
				for (int i=0; i<amount; i++)
				{	
					if (poolEnemies [key] == null)
					{
						poolEnemies [key] = new ArrayList () { createEnemyByType (key) };
					} 
					else 
					{
						ArrayList tempAry = poolEnemies [key] as ArrayList;
						tempAry.Add (createEnemyByType (key));
						poolEnemies [key] = tempAry;
					}
				}
			}
		}
	}
	
//	public void showTeamChangeDlg()
//	{	
//		Resources.UnloadUnusedAssets();
//		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg(NewLevelMgr.Instance.currentChapterIndex, NewLevelMgr.Instance.currentLevelIndex);
//		dlg.transform.parent = GameObject.Find("AlertAnchor").transform;
//	
	void cheat_victory ()
	{
		ArrayList a = new ArrayList(EnemyMgr.enemyHash.Values);
		foreach(Enemy enemy in a)
		{
			if (!enemy.getIsDead ())
			{
				int realHp = enemy.realHp;
				enemy.realDamage (realHp);
				
			}
		}
//		foreach(string key in EnemyMgr.enemyHash.Keys)
//		{
//			Enemy enemy = EnemyMgr.enemyHash[key] as Enemy;
//			if (!enemy.getIsDead ())
//			{
//				int realHp = enemy.realHp;
//				enemy.realDamage (realHp);
//				
//			}
//		}
	}
	
	public void DestroyAllObj()
	{
		foreach(GameObject destroyObj in LevelMgr.destroyObjArray)
		{
			Destroy(destroyObj);
		}
		LevelMgr.destroyObjArray.Clear();
	
		EnemyMgr.clear ();
		enemyObjectTable.Clear();
		poolEnemies.Clear();
		athleticsIntegral = 0;
		Time.timeScale = 1.0f;
		StaticData.paused = false;
		StaticData.isBattleEnd = true;
	
		MsgCenter.instance.removeListener (MsgCenter.ENEMY_DEAD, enemyDead);
		MsgCenter.instance.removeListener (MsgCenter.HERO_DEAD, heroDead);
		MsgCenter.instance.removeListener (MsgCenter.FALL_DOWN, fallDown);
		MsgCenter.instance.removeListener (MsgCenter.CREATE_START_Battle, createBattle);
		//MsgCenter.instance.removeListener (ExpBar.ADD_EXP_COMPLETE, expComplete);
		MsgCenter.instance.removeListener (MsgCenter.CONSUME_ITEM_RELIVE, onHeroRelive);
	}

	void OnDestroy ()
	{
		DestroyAllObj();
	}
	
	public void pauseButtonClick(){
		if(!StaticData.paused)
		{
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			
			StaticData.paused = true;
			pauseButton.SetActive(false);
			
			PauseDialog pauseDialog = DlgManager.instance.showBattlePauseDlg();
			Transform alertAnchor = GameObject.Find ("AlertAnchor").transform;
			pauseDialog.transform.parent = alertAnchor;
			pauseDialog.transform.localPosition = new Vector3(0, 0, 0);
			pauseDialog.transform.localScale = Vector3.one;
			pauseDialog.lastTimeScale = Time.timeScale;
			pauseDialog.onClose = delegate() {
				
			};
			Time.timeScale = 0.0f;
		}
	}
	
	public void onGearUpClick()
	{
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_simple_1b");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamDlg dlg = DlgManager.instance.ShowTeamDlg();
		dlg.delayMusic();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.init((HeroMgr.getHeroByIndex(0).data as HeroData));
		dlg.onClose = delegate {
			foreach(Hero heroTemp in HeroMgr.heroHash.Values)
			{
				heroTemp.initData(heroTemp.data);
			}
		};
	}
	
	public void onChangeTeamBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate {
			
		};
	}
	
	public void OnBattleBtnClick()
	{	
		int costStamina = 0;//Formulas.getCostStaminaByLevel(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
		if(!BattleBg.Instance.IsSkipHudPreBattle)
		{
			//costStamina = Formulas.getCostStaminaByLevel(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
			bool isAllHerosNoStamina = true;
			bool isInBattleHerosNoStamina = true;
			
			foreach(HeroData heroData in UserInfo.heroDataList)
			{
				if(heroData.state == HeroData.State.LOCKED || heroData.state == HeroData.State.UNLOCKED_NOT_RECRUITED)
				{
					continue;
				}
				
				Hero hero = HeroMgr.getHeroByType(heroData.type);
				
				if(hero == null && (heroData.stamina - costStamina) >= 0)
				{
					isAllHerosNoStamina = false;
				}
				else if(hero != null && (heroData.stamina - costStamina) >= 0)
				{
					isInBattleHerosNoStamina = false;
					isAllHerosNoStamina = false;
				}
			}
			
			if(isAllHerosNoStamina)
			{
				MusicManager.playEffectMusic("SFX_Error_Message_1c");
//				CommonDlg dlg = DlgManager.instance.ShowCommonDlg("All heros are out of stamina!");
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(Localization.instance.Get("UI_CommonDlg_AllHerosNoStamina"));
				dlg.setOKDlg();
				//dlg.onOk = onGearUpClick;
				
				LevelMgr.Instance.enabledBattleButton(true);
				return;
			}
			else if(isInBattleHerosNoStamina)
			{
				MusicManager.playEffectMusic("SFX_Error_Message_1c");
				//CommonDlg dlg = DlgManager.instance.ShowCommonDlg("All heroes in your team have run out of stamina! You can change your team.");
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(Localization.instance.Get("UI_CommonDlg_InBattleHerosNoStamina"));
				dlg.setOKDlg();
				dlg.onOk = onChangeTeamBtnClick;
				
				LevelMgr.Instance.enabledBattleButton(true);
				return;
			}
		}
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}-{2}", 
			TsFtueManager.BATTLE_STARTED, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
//		TsUserClickSomething clickWaiting = battleButtonObj.GetComponent<TsUserClickSomething>();
//		if (null != clickWaiting){
//			clickWaiting.OnFinished();
//			Destroy(clickWaiting);
//		}
//		
//		battleButtonObj.SetActive(false);	
		
		
		//BattleBg.Instance.heroOutBattle(true, costStamina);
		
		foreach (string key in HeroMgr.heroHash.Keys)
		{
			Hero hero = HeroMgr.heroHash[key] as Hero;
			
//			if(!hero.OnMoveToPositionFinishedByParamIsNull())
//			{
//				hero.OnMoveToPositionFinishedByParam -= BattleBg.Instance.moveToBattleStartPositionFinished;
//			}
			HeroData heroData = (hero.data as HeroData);
			

		
			hero.initData(hero.data);
			
//			(hero.data as HeroData).consumeStamina(1);
			
			SkillIconManager.Instance.createSkillIconDataSkillByHeroData(hero.data as HeroData);
//			ComboController.Instance.createComboIconDataSkillByHeroData(hero.data as HeroData);
			
			heroData.consumeStamina(costStamina);
			
//			BattleBg.Instance.setHeroStateDialogCostStamina(heroData.type, costStamina);
		}
		
		UserInfo.instance.saveAllheroes();
		
		//if(BattleBg.Instance.battleBeginDlg!=null)BattleBg.Instance.battleBeginDlg.setConsumeStaminaLabelGameLabel(costStamina);
		
//		BattleBg.Instance.consumeStaminaHeroStateDialog();
		BattleBg.Instance.deleteHeroStateDialogs();
				
		StartCoroutine(OnBattleBtnClickLate());
	}
	
	public IEnumerator OnBattleBtnClickLate()
	{
		yield return new WaitForSeconds(1.0f);
		
//		BattleBg.Instance.closeBattleBeginDlg();
		LevelMgr.Instance.startBattle();
		BattleBg.Instance.startBattle();
	}
	
	public void OnBattleBtnClickForMusic(){
		StartCoroutine(OnBattleBtnClickLateMusic());	
	}
	
	public IEnumerator OnBattleBtnClickLateMusic(){
		yield return new WaitForSeconds(1.5f);
		//MusicManager.playEffectMusic("SFX_battle_starts_1a");
	}
	
	public void enabledBattleButton(string[] enableStr){
		enabledBattleButton(bool.Parse(enableStr[0]));
	}
	
	public void enabledBattleButton(bool isEnable)
	{
		GameObject dlgGo = GameObject.Find("BattleBeginDlg(Clone)") as GameObject;
		BattleBeginDlg dlg = dlgGo.GetComponent<BattleBeginDlg>();
		dlg.enableBattleButton(isEnable);
		
	}
	
	public float curLevelDifficulty{
		get{
			return difficulty;	
		}
	}
	
	private bool showCheats = false;
	void OnGUI(){
		GUILayout.Space(200);
		showCheats = GUILayout.Toggle( showCheats,"    Show \n Cheat Buttons");
		
		if(showCheats == false) return;
		
		if(GUILayout.Button(" cheat \n    Kill Faster  "))
		{
			cheat_victory();
		}
		if(GUILayout.Button(" cheat \n    Die Faster  "))
		{
			foreach(HeroData hd in UserInfo.heroDataList)
			{
				Hero h = HeroMgr.getHeroByType(hd.type);
				if( h!= null && h.realHp >0){
					h.realHp = 0;
					return;
				}
			}
		}
		if(GUILayout.Button(" cheat \n    Unlock all heros  "))
		{
			foreach(HeroData hd in UserInfo.heroDataList)
			{
				if(hd.state == HeroData.State.LOCKED)
					hd.state = HeroData.State.UNLOCKED_NOT_RECRUITED;
			}
		}
		if(GUILayout.Button(" cheat \n    Unlock change button  "))
		{
//			GameObject go = GameObject.Find("BattleBeginDlg(Clone)") as GameObject;
//			BattleBeginDlg dlg = go.GetComponent<BattleBeginDlg>();
//			BattleBeginDlgHeroState[] heroStates = GameObject.FindObjectsOfType(typeof(BattleBeginDlgHeroState)) as BattleBeginDlgHeroState[];
//			foreach(BattleBeginDlgHeroState hs in heroStates){
//				hs.changeButtonObj.SendMessage("Normal");	
//			}
		}
		if(GUILayout.Button(" cheat \n    Unlock train button  "))
		{
//			GameObject go = GameObject.Find("BattleBeginDlg(Clone)") as GameObject;
//			BattleBeginDlg dlg = go.GetComponent<BattleBeginDlg>();
//			BattleBeginDlgHeroState[] heroStates = GameObject.FindObjectsOfType(typeof(BattleBeginDlgHeroState)) as BattleBeginDlgHeroState[];
//			foreach(BattleBeginDlgHeroState hs in heroStates){
//				hs.trainButtonObj.SendMessage("Normal");	
//			}
		}
		if(GUILayout.Button(" cheat \n    Hero Level Up "))
		{
			foreach(Hero h in HeroMgr.heroHash.Values){
				((HeroData)h.data).lv = Mathf.Min(((HeroData)h.data).lv+1,30);
			}
//			BattleBeginDlgHeroState[] heroStates = GameObject.FindObjectsOfType(typeof(BattleBeginDlgHeroState)) as BattleBeginDlgHeroState[];
//			foreach(BattleBeginDlgHeroState hs in heroStates){
//				hs.hero = hs.hero;
//			}
		}
		
	}
}
