using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
public class BattleBg : MonoBehaviour {
//	public static Bounds bounds;//added by roger, to play without this class's instance
	public static bool canUseSkill = true; // Why not put this into Hero class? 
	
	private int skillCastCount = 0;
	//GameObject enemy;
	
	public UILabel WaveTxt;
	public UILabel WaveTxtCenter;
	//add by xiaoyong 20120328  
	public static Bounds fingerBounds;
	public static Bounds actionBounds; 
	
	
	public TargetIndicator targetIndtr;
	public HealMenu healMenu;
	public GameObject dlgPrefab_battleBegin;
	
	public BattleBeginDlg battleBeginDlg;
	
	private GameObject skills;
	
	public GameObject heroStateDialogPrb;
	public Hashtable heroStateDialogHashtable = new Hashtable();
	public List<GameObject> heroStateDialogForAddList = new List<GameObject>();
	
	[HideInInspector]
	Ray ray;
	
	public ScreenOrientation orientation;
	
	public delegate void NoneParmsDelegate();
	public NoneParmsDelegate OnBattleReady;
	
	
	private static BattleBg _instance;
	[HideInInspector]
	public static BattleBg Instance{
		get{
			return _instance;
		}
	}
	public PreBattleDlg preBattleDlg;
//	public ArrayList enabledheroStateDialogButtonsType = new ArrayList();
//	public ArrayList highlightHeroStateDialogButtonsTypeList = new ArrayList();
	

	public static Vector3 getPointInScreen (){
		if (BarrierMapData.Enable){
			Vector2 pos = Vector2.zero;
			do{
				pos = BarrierMapData.Instance.GetPointInScreen();
			}while(IsOutOfActionBounce((Vector3)pos));
			return pos;
		}
		
		float randomX = UnityEngine.Random.Range(actionBounds.min.x,actionBounds.max.x);
		float randomY  = UnityEngine.Random.Range(actionBounds.min.y,actionBounds.max.y);
		return new Vector3(randomX, randomY,0);
	}
	//added by yuanq
	public static Vector3 getPointInAround(Vector3 center,float near,float far){
		int safeCount = 50;
		Vector3 correctPoint = CorrectingEndPointToActionBounds(center);
		while(true && --safeCount > 0){
			center = new Vector3( Utils.myRandom(near,far),Utils.myRandom(near,far),0) + correctPoint;
			if(actionBounds.Contains(center) 
					&& BarrierMapData.Instance.IsThePositionValid((Vector2)center)){
				return center;
			}
		}
		Debug.LogError(string.Format("Error: getPointInAround [{0}]", correctPoint));
		correctPoint = BarrierMapData.Instance.GetPointInScreen();
		return correctPoint;
	}
	public static Vector3 getPointInAround(Vector3 pos){
//		float randomX = pos.x + range * (Random.Range(-1f,1f));
//		float randomY = pos.y + range * (Random.Range(-1f,1f));
//		Vector3 point = new Vector3(randomX, randomY,0);
//		
//		return CorrectingEndPointToFingerBounds(point);
		return getPointInAround(pos,0,30.0f);
	}
	
	
	public void Awake ()
	{
		_instance = this;
		
		int y = 13;
		battleStartPosList = 
		new ArrayList ()
		{
			new BattleStartPos("",new Vector3 (-270, y, y/ 10 + StaticData.objLayer)),
			new BattleStartPos("",new Vector3 (-90, y, y/ 10 + StaticData.objLayer)),
			new BattleStartPos("",new Vector3 (90, y, y/ 10 + StaticData.objLayer)),
			new BattleStartPos("",new Vector3 (270, y, y/ 10 + StaticData.objLayer))
		};
		InitBounce();
		heroInBattle();
		preBattleDlg.setReady(false);
		//, MapMgr.Instance.currentLevelIndex
		//preBattleDlg.labelLevel.text = "LEVEL "+MapMgr.Instance.getCurrentChapter().id;
		// heroMoveToBattleStartPos();
	}
	public int heroMoveToBattleStartPosCount  = 0;
	public int heroMoveToBattleStartPosCurrentCount = 0;
	
	public ArrayList battleStartPosList;
		
	
	public class BattleStartPos
	{
		public string userHeroType = "";
		public Vector3 pos = Vector3.zero;
		public BattleStartPos(string userHeroType, Vector3 v)
		{
			this.userHeroType = userHeroType;
			this.pos = v;
		}
		
	}
	
	public Vector3 getHeroStartPosByHeroType(string heroType)
	{
		foreach(BattleStartPos battleStartPos in battleStartPosList)
		{
			if(battleStartPos.userHeroType == "")
			{
				continue;
			}
			else if(battleStartPos.userHeroType == heroType)
			{
				return battleStartPos.pos;
			}
		}
		return Vector3.zero;
	}
	
	public void heroTeleportToBattleStartPos()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			hero.transform.position = BattleBg.Instance.getHeroStartPosByHeroType(hero.data.type);
			BattleBg.Instance.moveToBattleStartPositionFinished(hero);
		}
	}
	
	public void heroMoveToBattleStartPos()
	{
		//LevelMgr.Instance.pauseButton.SetActive(false);
		heroMoveToBattleStartPosCount = 0;
		heroMoveToBattleStartPosCurrentCount = 0;
				
		foreach(BattleStartPos battleStartPos in battleStartPosList)
		{
			if(battleStartPos.userHeroType == "")
			{
				continue;
			}
			Hero hero = HeroMgr.getHeroByType(battleStartPos.userHeroType);
			hero.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveToBattleStartPositionFinished);
			
			if (!IsSkipHudPreBattle)
			{
				this.createHeroStateDialog(hero);
			}

			hero.move(battleStartPos.pos);
		
			heroMoveToBattleStartPosCount++;	
		}
		if(heroMoveToBattleStartPosCount == 0){
			BattleBg.Instance.moveToBattleStartPositionFinished(null);
		}
		
		if (!IsSkipHudPreBattle)
		{
//			this.createBattleBeginDlg();
			
		}
	}
	
	public bool IsSkipHudPreBattle = false;
	public void moveToBattleStartPositionFinished(Character character)
	{
		if(character !=null){
			character.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveToBattleStartPositionFinished);
			character.standby();
			character.selecting();
			heroMoveToBattleStartPosCurrentCount++;
			
			if (!IsSkipHudPreBattle && StaticData.isBattleEnd)
			{
				heroStateDialogInScreen(character as Hero);
	//			disabledHeroStateDialogButtons(enabledheroStateDialogButtonsType);
	//		    highlightHeroStateDialogButtons(highlightHeroStateDialogButtonsTypeList, true);
			}
		}
		if(heroMoveToBattleStartPosCurrentCount == heroMoveToBattleStartPosCount)
		{
			
//			enabledheroStateDialogButtonsType.Clear();
//			highlightHeroStateDialogButtonsTypeList.Clear();
			if (!IsSkipHudPreBattle && StaticData.isBattleEnd)
			{
//				LevelMgr.Instance.battleButtonObj.SetActive(true);
				//this.createBattleBeginDlg(); too late here
				
				preBattleDlg.gameObject.SetActive(true);
				if(heroMoveToBattleStartPosCurrentCount == 0)
				{
					preBattleDlg.showEmptyTeam(true);
					preBattleDlg.setReady(false);
				}else{
					preBattleDlg.showEmptyTeam(false);
					preBattleDlg.setReady(true);
				}
				this.createHeroStateDialogForAdd();
			}
			else if (IsSkipHudPreBattle && StaticData.isBattleEnd)
			{
				LevelMgr.Instance.OnBattleBtnClick();
			}
			if (null != OnBattleReady)
			{
				OnBattleReady();
			}
			
			TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}-{2}", 
			TsFtueManager.BATTLE_READY, MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
			
			
			int costStamina = Formulas.getCostStaminaByLevel(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
			
			foreach(Hero hero in HeroMgr.heroHash.Values)
			{				
				if(hero != null && ((hero.data as HeroData).stamina - costStamina) < 0)
				{
					//MusicManager.playEffectMusic("SFX_hero_low_health_2a");	
					break;
				}	
			}
		}
	}
	
	public void heroInBattle()
	{
		int index = 0;
		for(int i=0; i<UserInfo.heroDataList.Count; i++)
		{
			HeroData heroData = UserInfo.heroDataList[i] as HeroData;
				
			if(heroData.state == HeroData.State.SELECTED)
			{
				if(HeroMgr.getHeroByType(heroData.type) == null)
				{
					GameObject heroObj = Instantiate(CacheMgr.getHeroPrb(heroData.type),new Vector3(-720 + index * 90 ,13, 13/ 10 + StaticData.objLayer),transform.rotation) as GameObject;
					heroObj.transform.localScale = new Vector3(Utils.characterSize,Utils.characterSize,1);

					Hero hero = heroObj.GetComponent<Hero>();
					
					hero.initData(heroData);
					
					if(hero.heroAI == null)
					{
						hero.setCharacterAI(typeof(HeroAI));
					}
					
					hero.id = HeroMgr.getID();
					HeroMgr.heroHash[hero.id] = hero;
					
//					createHeroStateDialog(hero);
					SkillIconManager.Instance.createSkillIconDataSkillByHeroData(heroData);
//					ComboController.Instance.createComboIconDataSkillByHeroData(heroData);
					
					foreach(BattleStartPos battleStartPos in battleStartPosList)
					{
						if(battleStartPos.userHeroType == "")
						{
							battleStartPos.userHeroType = heroData.type;
							break;
						}
					}
				}
				index++;
			}
		}
		clearHeroStateDialogForAdd();
	}
	
	public void createBattleBeginDlg()
	{
		Debug.Log("createBattleBeginDlg");
		GameObject go = GameObject.Find("BattleBeginDlg(Clone)");
		
		if(go != null)
		{
			Debug.Log("destroy BattleBeginDlg");
			Destroy(go);
		}
		
		GameObject dlg_battleBegin = Instantiate (this.dlgPrefab_battleBegin) as GameObject;
		Transform alertAnchor = GameObject.Find ("AlertAnchor").transform;
		dlg_battleBegin.transform.parent = alertAnchor;
		dlg_battleBegin.transform.localPosition = new Vector3(0, 0, 0);
		dlg_battleBegin.transform.localScale = Vector3.one;
		
		battleBeginDlg = dlg_battleBegin.GetComponent<BattleBeginDlg>();
		
		//MusicManager.playEffectMusic("SFX_pre-battle_1a");
	}
	
	public void closeBattleBeginDlg()
	{
		GameObject go = GameObject.Find("BattleBeginDlg(Clone)");
		if(go != null)
		{
			battleBeginDlg = null;
			Destroy(go);	
		}
	}
	
	private void showBattleBeginDlg()
	{
		Debug.LogError("Show the dlg now");
	}

	
	public void heroOutBattle(bool isJudgeStamina, int costStamina)
	{
		for(int i=0; i<UserInfo.heroDataList.Count; i++)
		{
			HeroData heroData = UserInfo.heroDataList[i] as HeroData;
				
			if(heroData.state != HeroData.State.SELECTED || ((heroData.stamina - costStamina) < 0 && isJudgeStamina))
			{
				if(HeroMgr.getHeroByType(heroData.type) != null)
				{
					Hero hero = HeroMgr.getHeroByType(heroData.type);

					BoxCollider bc = hero.GetComponent<BoxCollider>();
					bc.enabled = false;
					SkillIconManager.Instance.destroySkillIconDataList(heroData.type);
//					ComboController.Instance.destroyComboIconDataList(heroData.type);
					GameObject heroStateDialogObjTemp = heroStateDialogHashtable[heroData.type] as GameObject;
					heroStateDialogHashtable.Remove(heroData.type);
					Destroy(heroStateDialogObjTemp);
					
					HeroMgr.delHero(hero.id);
					
					hero.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveToBattleStartPositionFinished);
					hero.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, heroOutToBattleFinish);
					
					hero.move(new Vector3(-720, hero.gameObject.transform.localPosition.y, hero.gameObject.transform.localPosition.z));
					//for rocket passive skill 25
					if(hero.data.type == HeroData.ROCKET)
					{
						Rocket rocketDoc = hero as Rocket;
						rocketDoc.destroyDrone();
					}
					foreach(BattleStartPos battleStartPos in battleStartPosList )
					{
						if(battleStartPos.userHeroType == heroData.type)
						{
							battleStartPos.userHeroType = "";
							break;
						}
					}
				}
			}
			
			if(heroStateDialogHashtable.Count == 0){
				//empty team
				preBattleDlg.showEmptyTeam(true);
				preBattleDlg.setReady(false);
			}
		}
		this.createHeroStateDialogForAdd();
	}
	
	public void heroOutToBattleFinish(Character c)
	{
		c.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, heroOutToBattleFinish);
		Destroy(c.gameObject);
	}
	
	public Transform heroStateDialogRoot;
	
	public void createAllHeroStateDialog()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			createHeroStateDialog(hero);
		}
	}
	private void clearHeroStateDialogForAdd(){
		foreach(GameObject heroStateDialog in heroStateDialogForAddList)
		{
			Destroy(heroStateDialog);
		}
		heroStateDialogForAddList.Clear();
	}
	private void createHeroStateDialogForAdd(){
		int n = 0;
		foreach(BattleStartPos battleStartPos in battleStartPosList)
		{
			if(battleStartPos.userHeroType != "") continue;
			
			GameObject heroStateDialogObjTemp = Instantiate(heroStateDialogPrb) as GameObject;
			heroStateDialogObjTemp.transform.parent = heroStateDialogRoot;
			heroStateDialogObjTemp.transform.localPosition = battleStartPos.pos + new Vector3(0,120,0);
			heroStateDialogObjTemp.transform.localScale = Vector3.one;
			HeroStateDialogNew heroStateDialogNew = heroStateDialogObjTemp.GetComponent<HeroStateDialogNew>();
			heroStateDialogNew.init(null, n++);
			heroStateDialogForAddList.Add(heroStateDialogObjTemp);
		}
	}
	
	public void createHeroStateDialog(Hero hero)
	{
		HeroData heroData = (hero.data as HeroData);

		if(heroStateDialogHashtable.Contains(heroData.type)){
			Debug.Log("state dialog for "+heroData.type +" already exist");
			return;
		}
		
		GameObject heroStateDialogObjTemp = Instantiate(heroStateDialogPrb) as GameObject;
		
		heroStateDialogObjTemp.transform.parent = heroStateDialogRoot;
//		 heroStateDialogObjTemp.transform.localPosition = hero.transform.position + new Vector3(0,-50,0);
//		 heroStateDialogObjTemp.transform.localPosition = new Vector3(heroStateDialogObjTemp.transform.localPosition.x, heroStateDialogObjTemp.transform.localPosition.y, 0);
		heroStateDialogObjTemp.transform.localPosition = new Vector3(-5000f, 0, 0);
		heroStateDialogObjTemp.transform.localScale = Vector3.one;
		
		
		HeroStateDialogNew heroStateDialogNew = heroStateDialogObjTemp.GetComponent<HeroStateDialogNew>();
		heroStateDialogNew.init(hero,0);
		//heroStateDialog.initLevelLabel(heroData.lv);
		
		heroStateDialogHashtable[heroData.type] = heroStateDialogObjTemp;
	}
	
	
	
	public void heroStateDialogInScreen(Hero hero)
	{
		string heroType = (hero.data as HeroData).type;
		GameObject heroStateDialogObjTemp = heroStateDialogHashtable[heroType] as GameObject;
		if(heroStateDialogObjTemp != null)
		{
			HeroStateDialog heroStateDialogTemp = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
		
			heroStateDialogObjTemp.transform.localPosition = hero.transform.position + new Vector3(0,120,0);
		}
		
	}
	
	public void deleteHeroStateDialogs()
	{
		preBattleDlg.gameObject.SetActive(false);
		LevelMgr.Instance.pauseButton.SetActive(true);
		foreach(GameObject heroStateDialog in heroStateDialogHashtable.Values)
		{
			Destroy(heroStateDialog);
		}
		heroStateDialogHashtable.Clear();
		clearHeroStateDialogForAdd();
	}
	
//	public void consumeStaminaHeroStateDialog()
//	{
//		foreach(GameObject heroStateDialogObjTemp in heroStateDialogHashtable.Values)
//		{
//			HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//			
//			heroStateDialog.changeStaminaBar();
//			
//			heroStateDialog.consumeStaminaLabelGameLabel.gameObject.SetActive(true);
//		}
//	}
	
	public void startBattle()
	{
		// if (BarrierMapData.Enable)
		// 	initBarrierMapData();
		orientation = Screen.orientation;
		
//		enabledheroStateDialogButtonsType.Clear();
//		highlightHeroStateDialogButtonsTypeList.Clear();
		
		StartCoroutine(initLate());
	}
	
	public IEnumerator initLate()
	{
		yield return new WaitForSeconds(2.0f);
		FingerHandler.Instance.Init(healMenu);
		MsgCenter.instance.addListener(MsgCenter.FALL_DOWN, heroFallDown);
	}
	
	private void InitBounce(){
		float h = Camera.mainCamera.orthographicSize * 2;
		float w = Camera.mainCamera.orthographicSize * 2 * Screen.width / Screen.height;

		fingerBounds = new Bounds();
		fingerBounds.SetMinMax(new Vector3(- (w/2-10),-h/2+10,-10000),new Vector3((w/2-10),h/2-TopMargin,10000));
		actionBounds = new Bounds();
		actionBounds.SetMinMax(new Vector3(- (w/2-MarginLR),-h/2+BottomMargin,-10000),new Vector3((w/2-MarginLR),h/2-TopMargin,10000));	
	
	}
	
#if UNITY_EDITOR
	public void OnGUI()
	{
		NgGUIDraw.DrawBox(ToScreenSpace(fingerBounds), Color.blue,3,false);
		NgGUIDraw.DrawBox(ToScreenSpace(actionBounds), Color.red,3,false);
	}
	
	 public Rect ToScreenSpace(Bounds bounds)
    {
        var origin = Camera.mainCamera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, 0.0f));
        var extents = Camera.mainCamera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y, 0.0f));
 
        return new Rect(origin.x, Screen.height - origin.y, extents.x - origin.x, origin.y - extents.y);
    }
#endif
	
	private const int TopMargin = 180;
	private const int BottomMargin = 50;
	private const int MarginLR = 50;
	
	public void Update (){
		orientation = Screen.orientation;
		if(StaticData.isBattleEnd){
			healMenu.gameObject.SetActiveRecursively(false);
		}
		FingerHandler.Instance.Update();
		
		if(StaticData.isTouch4)
		{
			if(Time.time%2 == 0)
			{
				System.GC.Collect();
			}
			if(Time.time%5 == 4)
			{
				Resources.UnloadUnusedAssets();
			}
		}
	}
	
	public void FixedUpdate (){
		if(orientation!= Screen.orientation){
			ScreenChangeMove();
		}
	}
	
	public void ScreenChangeMove (){
	//	if(mLine.enabled){
	//	
	//		Vector2 touchPtUp = Input.mousePosition;//Input.GetTouch(0).position;
	//		fingerUpHandler(touchPtUp);  		
	//	}
	}
	
	
	
	// public void OnApplicationPause ( bool pause  ){
	//	line.EndDrawing();
	// }
	
	// public void onHideDrawLine ( Message msg  ){
	// 	line.EndDrawing();
	// }
	
	public Hero isTapHero ( Vector3 vc3  ){
		foreach( DictionaryEntry dic1 in HeroMgr.heroHash)
		{
			Hero hero_b = dic1.Value as Hero;
			Rect rect = StaticData.get2DRect(hero_b.gameObject);
			if( rect.Contains(vc3) )
			{
				return hero_b;
			}
		}
			return null;
	}
	
//	public void clearAllCD (){
//		SkillData skD = null;
//		if((BattleObjects.skHero.data as HeroData).skillListBattle.Count>0)
//		{
//			skD = (BattleObjects.skHero.data as HeroData).skillListBattle[0] as SkillData;
//			skD.reset();
//		}
//		if((BattleObjects.skHero.data as HeroData).skillListBattle.Count>1)
//		{
//			skD = (BattleObjects.skHero.data as HeroData).skillListBattle[1] as SkillData;
//			skD.reset();
//		}
//	}
	
	public void DestroyAllObj()
	{
		MsgCenter.instance.removeListener(MsgCenter.FALL_DOWN, heroFallDown);
	}
	
	public void OnDestroy (){
		FingerHandler.Release();
		DestroyAllObj();
		HeroMgr.clear ();
	}
	
	public void heroFallDown ( Message msg  ){
		Character falldownChr = msg.sender as Character;
	//	print("deadHero"+falldownChr);
	//	print("BattleObjects.skHero"+BattleObjects.skHero);
		if(BattleObjects.skHero != null)
		{
			if(BattleObjects.skHero.data.type == falldownChr.data.type)
			{
				// falldownChr.cancelHighLight();
				SkillIconManager.Instance.clearAllData();
			}
		}
		
	}
	
	public static Vector3 CorrectingEndPointToFingerBounds(Vector3 endVc3){
		Vector3 minVc3 = fingerBounds.min;
		Vector3 maxVc3 = fingerBounds.max;
		
		if(IsOutOfFingerBounce(endVc3)){
			if(endVc3.x > maxVc3.x && endVc3.x > 0) endVc3.x = maxVc3.x;
			if(endVc3.x < minVc3.x && endVc3.x < 0) endVc3.x = minVc3.x;
			if(endVc3.y > maxVc3.y && endVc3.y > 0) endVc3.y = maxVc3.y;
			if(endVc3.y < minVc3.y && endVc3.y < 0) endVc3.y = minVc3.y;
		}
		
		endVc3.z = StaticData.circleLayer;
		
		return endVc3;
	}
	public static Vector3 CorrectingEndPointToActionBounds(Vector3 endVc3){
		Vector3 minVc3 = actionBounds.min;
		Vector3 maxVc3 = actionBounds.max;
		
		if(IsOutOfFingerBounce(endVc3)){
			if(endVc3.x > maxVc3.x && endVc3.x > 0) endVc3.x = maxVc3.x;
			if(endVc3.x < minVc3.x && endVc3.x < 0) endVc3.x = minVc3.x;
			if(endVc3.y > maxVc3.y && endVc3.y > 0) endVc3.y = maxVc3.y;
			if(endVc3.y < minVc3.y && endVc3.y < 0) endVc3.y = minVc3.y;
		}
		
		endVc3.z = StaticData.circleLayer;
		
		return endVc3;
	}
	
	public static bool IsOutOfFingerBounce(Vector3 point){
		//Debug.Log("IsOutOfFingerBounce point="+point+ " fingerBounds="+fingerBounds);
		return !fingerBounds.Contains(point);
	}
	public static bool IsOutOfActionBounce(Vector3 point){
		//Debug.Log("IsOutOfFingerBounce point="+point+ " fingerBounds="+fingerBounds);
		return !actionBounds.Contains(point);
	}
	public static bool IsXValueOutOfActionBounce(float x){
		float halfWidth = actionBounds.size.x/2f;
		return (actionBounds.center.x - halfWidth > x
				|| actionBounds.center.x + halfWidth < x);
	}
	// Never Used
	public void setSkEffect ( string skName  ){
	//	nameTxt.SetActiveRecursively(true);
	//	SpriteText sptTxt = nameTxt.GetComponent<SpriteText>();
	//	sptTxt.Text = skName;
	//	yield return new WaitForSeconds(3);
	//	nameTxt.SetActiveRecursively(false);
	}
	
//	public void heroStateDialogAllButtonEnabled(bool isButtonEnabled)
//	{
//		if(heroStateDialogHashtable.Count <= 0)
//		{
//			return;
//		}
//		
//		foreach(GameObject heroStateDialogObjTemp in heroStateDialogHashtable.Values)
//		{
//			HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//			heroStateDialog.enableAllButton(isButtonEnabled);
//		}
//	}
//	
//	public void enabledheroStateDialogChangeButton(string heroType, bool isButtonEnabled)
//	{
//		this.enabledheroStateDialogButton(HeroStateDialog.ButtonType.ChangeButton, heroType, isButtonEnabled);
//	}
//	
//	public void enabledheroStateDialogTrainButton(string heroType, bool isButtonEnabled)
//	{
//		this.enabledheroStateDialogButton(HeroStateDialog.ButtonType.TrainButton, heroType, isButtonEnabled);
//	}
//	
//	protected void enabledheroStateDialogButton(HeroStateDialog.ButtonType buttonType, string heroType, bool isButtonEnabled)
//	{
//		if(heroStateDialogHashtable.Count <= 0)
//		{
//			return;
//		}
//		GameObject heroStateDialogObjTemp = heroStateDialogHashtable[heroType] as GameObject;
//		HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//		
//		if(buttonType == HeroStateDialog.ButtonType.ChangeButton)
//		{
//			heroStateDialog.enabledChangeButton(isButtonEnabled);
//		}
//		else if(buttonType == HeroStateDialog.ButtonType.TrainButton)
//		{
//			heroStateDialog.enabledTrainButton(isButtonEnabled);
//		}
//	}
//	
//	public void enabledHeroStateDialogButton(HeroStateDialog.ButtonType buttonType)
//	{
//		enabledHeroStateDialogButton(buttonType, true);
//	}
//	
//	public void disabledHeroStateDialogButton(HeroStateDialog.ButtonType buttonType)
//	{
//		enabledHeroStateDialogButton(buttonType, false);
//	}
//	
//	public void enabledHeroStateDialogButton(HeroStateDialog.ButtonType buttonType, bool isButtonEnabled)
//	{
//		if(heroStateDialogHashtable.Count <= 0)
//		{
//			return;
//		}
//		
//		foreach(GameObject heroStateDialogObjTemp in heroStateDialogHashtable.Values)
//		{
//			enabledHeroStateDialogButton(heroStateDialogObjTemp, buttonType, isButtonEnabled);
//		}
//	}
	
//	public void setEnabledheroStateDialogButtonsType(string[] buttonTypeList)
//	{
//		enabledheroStateDialogButtonsType.Clear();
//		enabledheroStateDialogButtonsType.AddRange(buttonTypeList);
//	}
//	
//	public void enabledHeroStateDialogButtons(ArrayList buttonTypeList)
//	{
//		enabledheroStateDialogButtons(buttonTypeList, true);
//	}
//	
//	public void disabledHeroStateDialogButtons(string[] buttonTypeList)
//	{
//		enabledheroStateDialogButtons(new ArrayList(buttonTypeList), false);
//	}
//	
//	public void disabledHeroStateDialogButtons(ArrayList buttonTypeList)
//	{
//		enabledheroStateDialogButtons(buttonTypeList, false);
//	}
//	
//	protected void enabledheroStateDialogButtons(ArrayList buttonTypeList, bool isButtonEnabled)
//	{
//		if(heroStateDialogHashtable.Count <= 0 || buttonTypeList.Count <= 0)
//		{
//			return;
//		}
//		
//		foreach(GameObject heroStateDialogObjTemp in heroStateDialogHashtable.Values)
//		{
//			foreach(string buttonTypeStr in buttonTypeList)
//			{
//				HeroStateDialog.ButtonType buttonType = (HeroStateDialog.ButtonType)Enum.Parse(typeof(HeroStateDialog.ButtonType), buttonTypeStr);
//				// Debug.LogError(buttonTypeStr)
//				enabledHeroStateDialogButton(heroStateDialogObjTemp, buttonType, isButtonEnabled);
//			}
//		}
//	}
//	
//	protected void enabledHeroStateDialogButton(GameObject heroStateDialogObjTemp, HeroStateDialog.ButtonType buttonType, bool isButtonEnabled)
//	{
//		HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//				
//		if(buttonType == HeroStateDialog.ButtonType.ChangeButton)
//		{
//			heroStateDialog.enabledChangeButton(isButtonEnabled);
//		}
//		else if(buttonType == HeroStateDialog.ButtonType.TrainButton)
//		{
//			heroStateDialog.enabledTrainButton(isButtonEnabled);
//		}
//	}
//	
//	
//	
//	public void setHighlightHeroStateDialogButtonsType(string[] buttonTypeList)
//	{
//		highlightHeroStateDialogButtonsTypeList.Clear();
//		highlightHeroStateDialogButtonsTypeList.AddRange(buttonTypeList);
//	}
//	
//	public void normalHeroStateDialogButtons(string[] buttonTypeList)
//	{
//		highlightHeroStateDialogButtons(new ArrayList(buttonTypeList), false);
//	}
//	
//	public void highlightHeroStateDialogButtons(ArrayList highlightButtonsTypeList, bool isEnabled)
//	{
//		if(heroStateDialogHashtable.Count <= 0 || highlightButtonsTypeList.Count <= 0)
//		{
//			return;
//		}
//		
//		foreach(GameObject heroStateDialogObjTemp in heroStateDialogHashtable.Values)
//		{
//			foreach(string buttonTypeStr in highlightButtonsTypeList)
//			{
//				HeroStateDialog.ButtonType buttonType = (HeroStateDialog.ButtonType)Enum.Parse(typeof(HeroStateDialog.ButtonType), buttonTypeStr);
//				HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//				heroStateDialog.highlightButtonByButtonType(buttonType, isEnabled);
//			}
//		}
//	}
//	
//	public void setHeroStateDialogCostStamina(string heroType, int costStamina)
//	{
//		GameObject heroStateDialogObjTemp = heroStateDialogHashtable[heroType] as GameObject;
//		HeroStateDialog heroStateDialog = heroStateDialogObjTemp.GetComponent<HeroStateDialog>();
//		heroStateDialog.costStamina = costStamina;
//	}
	
	/*public void skillSlotHandler1 (){
		skillSlotHandler(0);
	}
	public void skillSlotHandler2 (){
		skillSlotHandler(1);
	}
	private void skillSlotHandler(int n){		
		if(!BattleBg.canUseSkill){
			return;
		}
		BattleBg.canUseSkill = false;
		ArrayList skillAry = BattleObjects.skHero.getSkillList();
		SkillData skilldata = skillAry[n] as SkillData;
		SkillManager.isPop = false;
		skillManager.callSkill(skilldata.skillName,new ArrayList(){BattleBg.Instance.gameObject,BattleObjects.skHero.gameObject, BattleObjects.skHero.getTarget()});
		skillCastCount++;
		castSkillAchievements();
	}
	
	public void castSkillAchievements (){
		if (skillCastCount == 20) {
			AchievementManager.updateAchievement("20SKILL_1BATTLE", 1);
		}
	}
	
//	void OnGUI(){
//		if(GUILayout.Button("shake")){
//			iTween.ShakePosition(Camera.mainCamera.gameObject,new Vector3(10,10,0),2 );
//		}
//	}*/
}
