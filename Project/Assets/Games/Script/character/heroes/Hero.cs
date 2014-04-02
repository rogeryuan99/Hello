using UnityEngine;
using System;
using System.Collections;

public class Hero : Character
{
	
	
	public static Hero Selected{get; set;}
	protected GameObject lightCircel; //hero foot's light
	
	//add by gwp 
	public bool  usingShotgun;
	public bool  isInvincible = false;//gwp   wu di 
	
	private bool isInGroup = false;
	public bool IsInGroup{  
		get{ return isInGroup; }
		set{ 
			isInGroup = value; 
			if (!isInGroup){
				startCheckOpponent();
			}
		}
	}
	
	// Added by Funing
	public bool IsIdle{
		get{
			return (STANDBY_STATE == state);
		}
	}
	
	
	
	public delegate void AttackCallbackDelegate();
	public AttackCallbackDelegate OnAttack;//added by roger for tutorial
	public int CallOnAttackAfterAttackCount = -1;
		
	public HeroAI heroAI;
	
	public bool isVineShield;
	
	public VineShield vineShield;
	
	public override void Awake ()
	{
		initAbnormalStateTable();
		atkPosAry = new ArrayList(){"0","0","0","0","0","0","0","0"};
		//super class character Awake() has this pieceAnima
		//gwp
		setPieceAnima();
		
		atkAnimKeyFrame = 3;
		
//		hpBarCom   = hpBar.GetComponent<HPBar>();
	}
	
	public override void getCharacterAI()
	{
		this.characterAI = gameObject.GetComponent<HeroAI>();
		this.heroAI = this.characterAI as HeroAI;
	}
	
	public override void setCharacterAI(Type aiType)
	{
		base.setCharacterAI(aiType);
		getCharacterAI();
		this.heroAI.getCharacter();
	}
	
	public override void Start ()
	{
		MsgCenter.instance.addListener(MsgCenter.CONSUME_ITEM_RELIVE , onRelive);
		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		
		
//		CharacterData characterD = new CharacterData();
//		characterD.maxHp  = 200;
//		characterD.attack = 50;
//		characterD.defense= 5;
//		characterD.moveSpeed = 100;
//		characterD.attackSpeed = 1;
//		characterD.attackRange = 150;
		
//		standby();
		//Debug.Log(Application.loadedLevelName);
		if ( (GotoProxy.getSceneName() != GotoProxy.SKILLTREE) && (GotoProxy.getSceneName() != GotoProxy.COMBINED_ARMORY) && (GotoProxy.getSceneName() != GotoProxy.ARMORY) && (Application.loadedLevelName != "theGreatScene")) {
			setDepth();
		}
		hpBar.hideHpBar();
		
	}
	//add by gwp for usingShotgun and isInvincible
	public void usingShotgunWithSeconds ( int seconds  ){
		if(IsInvoking("cancelUsingShotgun")){
			CancelInvoke("cancelUsingShotgun");
		}
		usingShotgun = true;
		Invoke("cancelUsingShotgun",seconds);
	}
	
	public void cancelUsingShotgun (){
		usingShotgun = false;
	}
	
	public void isInvincibleWithSeconds ( int seconds  ){
		if(IsInvoking("cancelIsInvincible")){
			CancelInvoke("cancelIsInvincible");
		}
		isInvincible = true;
		Invoke("cancelIsInvincible",seconds);
	}
	
	public void cancelIsInvincible (){
		isInvincible = false;
	}
	//end
	
	protected virtual void initSkill ()
	{
//		SkillData skD = SkillData.create("sk03","deadBullet",10,null);
//		SkillData skD2 = SkillData.create("sk04", "crazyShoot", 12,null);
//		HeroData heroD = SceneInit.heroDataList[0];
//		heroD.skillList = new ArrayList()[skD,skD2];
//		EquipCalculator.calculate(SceneInit.heroDataList[0]);
//		initData(SceneInit.heroDataList[0]);
	}
	
	protected void buildSkills()
	{
		HeroData heroData = (data as HeroData);

		
//			DestroyListBattle();
//			heroData.buildActiveSkill();
		heroData.buildPassiveSkill();
		
	}
	
	protected void drawAvatar (){
		HeroData tempHeroD = data as HeroData;
		EquipData weapon = tempHeroD.getEquipData(EquipData.Slots.WEAPON);
		if(weapon != null)
		{
//			print(weapon.graphicsID);
			changeWeapon(weapon.equipDef.graphicsID);
		}
//		EquipData armor = tempHeroD.getEquipData(EquipData.ARMOR);
//		if(armor)
//		{
//			print(armor.graphicsID);
//			changeArmor(armor.graphicsID);
//		}
	}
	
	public void upData()
	{
		
	}
	
	public override void initData ( CharacterData characterD  )
	{
//			this.attack += (this.lv*this.atkProportion);
//			this.attack.Add((this.lv -1)*this.atkProportion);
//			this.defense += this.lv*this.defProportion;
//			this.defense.Add((this.lv-1)*this.defProportion);
//			this.maxHp += (int)((this.lv-1)*this.hpProportion);		
		HeroData heroD = characterD as HeroData;
		data = heroD;
		data.isDead = false;
		
		buildSkills();
		
		EffectCalculator.calculate(heroD);
		initSkill();
		drawAvatar();
		
		ClearSkillContainer();
		
		this.reCalctAtkAndDef();
		
		hpBar.initBar(realHp);
	}
	
	public override void resetDef()
	{
		HeroData heroD = this.data as HeroData;
		initRealDef(heroD);
	}
	
	public override void resetMoveSpd ()
	{
		HeroData heroD = (HeroData) this.data;
		realMspd = (heroD.moveSpeed+ heroD.itemAdd.moveSpd + heroD.skillAdd.moveSpd) * (1 + (heroD.itemMult.moveSpd + heroD.skillMult.moveSpd) / 100.0f);
	}
	
	public override void resetAtkSpd ()
	{
		HeroData heroD = (HeroData) this.data;
		realAspd = (heroD.attackSpeed + heroD.itemAdd.atkSpd + heroD.skillAdd.atkSpd) * (1 + (heroD.itemMult.atkSpd + heroD.skillMult.atkSpd) / 100.0f);
	}
	
	//add by gwp at 20130220 
	private Vector3 _moveToPoint;
	// for victory move only, if skill casting, wait till it finish, then move.
	public void victoryMoveHandler ( Vector3 vc3  ){ 
		_moveToPoint = vc3;
		Invoke("moveContinue",0.5f); 
	}
	public void moveContinue ()
	{
		if(state == CAST_STATE)
		{
			Invoke("moveContinue",0.5f);
		}
		else
		{
			move(_moveToPoint);
		}
	}//end
	
	public void moveToTargetDirectly (GameObject obj)
	{ 
		MusicManager.playEffectMusic("SFX_character_zoom_2a");
		if (this.targetObj != null) 
		{
			Character targetDoc = this.targetObj.GetComponent<Character> (); 
			targetDoc.dropAtkPosition (this);
		}
		
		this.targetObj = obj;
		// this.playAnim ("Stand");
		this.state = Character.MOVE_TARGET_DIRECTLY_STATE ;
		this.multiSpeed(4f);
	}
	
	public override void moveTargetDirectlyStateUpdate()
	{
		if(this.targetObj == null)
		{
			this.moveWhenTargetDead();
			return;
		}
		else
		{
			Character chtr = this.targetObj.GetComponent<Character>();
			if(chtr.getIsDead())
			{
				this.moveWhenTargetDead();
				return;
			}
		}
		
		this.toward(this.targetObj.transform.position);
		
		Character targetCharacter = this.targetObj.GetComponent<Character>();
		
		Vector2 vc2 = targetCharacter.getAtkPosition(this);
		float dis2 = Vector2.Distance(transform.position, vc2);
		float spd2 = Time.deltaTime*this.realMspd;
		if(dis2>spd2*2)
		{
			this.setDirection(vc2);
			if(this.dirVc3.y != 0)
			{
				this.setDepth();
			}
			transform.Translate(this.dirVc3 * Time.deltaTime * this.realMspd);
		}
		else
		{
//			if (OnMoveToTargetDirectlyFinished != null)
//			{
//				OnMoveToTargetDirectlyFinished();
//			}
			this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished);
			this.normalSpeed();
			transform.position = new Vector3(vc2.x, vc2.y, transform.position.z);
//			if (gameObject.tag != this.targetObj.tag)
//			{
//				this.startAtk();
//			}
//			else
//			{
//				this.standby();
//			}
		}
		
	}
	//end
	
	protected virtual void initRealAtk (HeroData heroD)
	{
		realAtk.Zero();
		Vector6 temp = new Vector6();
		float p = (heroD.lv-1)*heroD.atkProportion;
		temp.Add(new Vector6(p,p,p,p,p,p));
		temp.Add(heroD.itemMult.attack).Add(heroD.skillMult.attack).Div(100).Add(1);
		realAtk.Add(heroD.attack).Add(heroD.itemAdd.attack).Add(heroD.skillAdd.attack).Multip(temp).Multip(0.3f);
	}
	
	protected virtual void initRealDef (HeroData heroD)
	{
		realDef.Zero();
		Vector6 temp = new Vector6();
		float p = (heroD.lv-1)*heroD.defProportion;
		temp.Add(new Vector6(p,p,p,p,p,p));
		temp.Add(heroD.itemMult.defense).Add(heroD.skillMult.defense).Div(100).Add(1);
		realDef.Add(heroD.defense).Add(heroD.itemAdd.defense).Add(heroD.skillAdd.defense).Multip(temp);
	}
	
	public float getInitRealDef (HeroData heroD)
	{
		
		Vector6 initRealDef = new Vector6();
		initRealDef.Zero();
		Vector6 temp = new Vector6();
		float p = (heroD.lv-1)*heroD.defProportion;
		temp.Add(new Vector6(p,p,p,p,p,p));
		temp.Add(heroD.itemMult.defense).Add(heroD.skillMult.defense).Div(100).Add(1);
		initRealDef.Add(heroD.defense).Add(heroD.itemAdd.defense).Add(heroD.skillAdd.defense).Multip(temp);
		return initRealDef.PHY;
	}
	
	
	public void reCalctAtkAndDef ()
	{
		HeroData heroD = (HeroData) this.data;
		Debug.Log("reCalctAtkAndDef for "+ heroD.type);
		EffectCalculator.calculate(heroD);
		
		initRealDef(heroD);
		initRealAtk(heroD);
				
		resetMoveSpd();
		
		resetAtkSpd();
		
		realMaxHp  = (int)( heroD.maxHp*(1+(heroD.lv-1)*heroD.hpProportion/100f  + heroD.itemAdd.maxHp + heroD.skillAdd.maxHp) * (1 + (heroD.itemMult.maxHp + heroD.skillMult.maxHp / 100f)));
		realHp = realMaxHp;
		
	}
	
	public override void hideHpBar ()
	{
		if(isVineShield && vineShield != null)
		{
			vineShield.hpBar.hideHpBar();
		}
		
		hpBar.hideHpBar();
	}
	
	   
	//add by gwp for battle end
	public virtual void battleEnd ()
	{
		currentColor = normalColor;
		model.renderer.material.color = currentColor;
		
		hideHpBar();
		Debug.Log("stopUseSkill bu zou yuan yin ????????????????");
		standby();
		clearBuff();
		if(this.isVineShield)
		{
			if(this.vineShield != null)
			{
				this.vineShield.battleEnd();
			}
		}
	}
	//<===gwp end
//	public ArrayList getSkillList (){
//		HeroData tempD = data as HeroData;
//		return tempD.skillListBattle;
//	}
	
	
//	public string getSkIdCanCastFromContainer(){
//		string target = (null == getTarget())? "NONE": getTarget().tag.ToUpper();
//		
//		return getSkIdCanCastFromContainer(target);
//	}
	
//	public string getSkIdCanCastFromContainer(string curTarget){
//		string skId = string.Empty;
//		
//		for (int i=0; i<skContainer.Count; i++){
//			string temId = (string)skContainer[i];
//			string containerTarget = (SkillLib.instance.allHeroSkillHash[temId] as SkillDef).target;
//			
//			
//			if (curTarget == containerTarget){
//				skId = temId;
//				i = skContainer.Count;
//			}
//		}
//		
//		return skId;
//	}
	
	
	
	
	public override void changeWeapon ( string weaponID  )
	{

	}
	public void changeArmor ( string aromorID  )
	{
//		pieceAnima.showPiece("sash",aromorID);
	}
	
	public override void selecting ()
	{
		if (!this.isPlayAtkAnim && 
			this.state != Character.CAST_STATE &&
			this.state != Character.MOVE_TARGET_STATE &&
			this.state != Character.MOVE_TARGET_DIRECTLY_STATE &&
			this.state != Character.MOVE_STATE)
		{
			this.playAnim("Stand");
		}
	}
	
	public void toggleEnable ()
	{
		
//		isEnabled = true;
	}
	
	private double previousTimescale;
	
	protected void resurrectionTutorail ()
	{
//		if(TutorialUI.instance.isShowTutorial(TutorialUI.USE_RESURRECTION)){
//			previousTimescale = Time.timeScale;
//			Time.timeScale = 0.0f;
//			TutorialUI.instance.startResurrectionTutorial();
//			TutorialUI.instance.resurrectionStep1(resurrectionHandler);
//		}
	}
	
	public void resurrectionHandler ()
	{
		Time.timeScale = (float)previousTimescale;
		GameObject reliveBtn = GameObject.Find("ItemIconsRL");
	}
	
	public virtual void blinkInScreen()
	{
//		if( StaticData.computeChance(30, 100) )
//		{
			gameObject.transform.position = BattleBg.getPointInScreen();
//		}
//		else
//		{
//			gameObject.transform.position = birthPt;
//		}
	}
	
	public virtual void relive ()
	{
		if(tag == "Enemy")
		{
			blinkInScreen();
		}
		
		isDead = false;
		data.isDead = false;
		
		initData(data);
		//gwp add 
		unFlash();
		//<==gwp end
		hpBar.resetView();
		this.gameObject.collider.enabled = true;
		playAnim("Standy");
		toggleEnable();
	}
	
	public override void dead (string s=null)
	{
		if (this.isDead)
			return;
	
		if(this.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN))
		{
			base.dead("NoPlayDeathAnimations");
			this.DeathLate();
		}
		else
		{  
			base.dead();
		}	
		
		this.removeInList();
	}
	
//	public void castSkill ( string skName  )
//	{
//		if(!isDead)
//		{
//			if(data.type == HeroData.HEALER){
//				state = CAST_STATE; 
//				standby();
//				state = CAST_STATE;
//			}else{
//				standby();
//				state = CAST_STATE;
//			}
////			Debug.Log(">>>>>>skillname:"+skName);
//			playAnim(skName);
//		}
//	}
	
	
	public int getHp ()
	{
		return realHp;
	}
	
	
	public GameObject enemyEft = null;
	
	protected override void checkAtkerDefense ( GameObject atker  )
	{
		
	}
	
	public override bool checkOpponent ()
	{
		if ( isDead || 
			this.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN) || 
			StaticData.isBattleEnd ||
			state == CAST_STATE)
		{
			cancelCheckOpponent();
			return false;
		}
		
		if (state != MOVE_STATE 
				&& state != MOVE_TARGET_STATE 
				&& state != MOVE_TARGET_DIRECTLY_STATE
				&& state != CAST_STATE)
		{
			
			if(this.heroAI.checkOpponent())
			{
				return true;
			}
			else
			{
				standby();
			}
		}
		return false;
	}
	
	public override Character getOpponent(Character primaryOpponent = null)
	{
		return this.heroAI.getOpponent(primaryOpponent);
	}
	
	public override void attackTargetInvok ()
	{
		if(!this.isActionStateActive(ActionStateIndex.AttackState))
		{
			return;
		}
		
		if(StaticData.isBattleEnd)
		{
			this.cancelAtk();
			return;
		}
		
		if(this.targetObj != null)
		{ 
			//xingyh 4.9f enemy out of the screen
			bool  b = this.isCollider();
			Character character = this.targetObj.GetComponent<Character>();
			if(!b && character.getIsDead())
			{
				this.targetObj = null;
				Debug.Log("attackTargetInvok 1  ???????????????????????");
				this.standby();
				return;
			}
			if(!character.getIsDead())
			{
				if(this.heroAI.OnAttackTargetInvokAttackTargetBefore())
				{
					this.heroAI.OnAttackTargetInvokAttackTarget();
				}
			}
			else
			{
				Debug.Log("if(!character.getIsDead())");
				this.heroAI.OnAttackTargetInvokTargetIsDeath();
				
			}
			
			if (!this.isOnAttackNull() && 
				0 == this.CallOnAttackAfterAttackCount--
				)
			{
				this.executeOnAttack();
			}
		}
		else
		{
			Debug.Log("attackTargetInvok 3  ???????????????????????");
			
			this.heroAI.OnAttackTargetInvokTargetIsNull();
		}
	}
	
	public bool isOnAttackNull()
	{
		return OnAttack == null;
	}
	
	public void executeOnAttack()
	{
		OnAttack();
	}
	
	
	// public void popSkill (){
		// if (skContainer.Count > 0)
		// 	skContainer.RemoveAt(0);
	// }
	
	public virtual void removeInList ()
	{
		if(tag != "Enemy")
		{
			HeroMgr.delHero(id);
		}
		else
		{
			EnemyMgr.delEnemy(id);
		}
	}
	
	public override string getTargetTagType ()
	{
		if(tag != "Enemy")
		{
			return "Enemy";
		}
		else
		{
			return "Hero";
		}
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  )
	{
		if(atkerObj.GetComponent<Character>().data.type == EnemyDataLib.Ch1_Male_Prisoner1){
			if(this.isOnceMusic){
				if(StaticData.computeChance(1,2)){
					MusicManager.playEffectMusic("SFX_character_damage_2a");		
				}else{
					MusicManager.playEffectMusic("SFX_character_damage_2b");	
				}
				this.isOnceMusic = false;
			}
		}else{
			if(StaticData.computeChance(1,2)){
				MusicManager.playEffectMusic("SFX_character_damage_2a");		
			}else{
				MusicManager.playEffectMusic("SFX_character_damage_2b");	
			}	
		}

		if(state != CAST_STATE && data.type != HeroData.MANTIS)
		{
			StartCoroutine(this.delayedAutoSelectTarget(0.5f,atkerObj));
		}
		
		int dam = base.defenseAtk(damage, atkerObj);
		//xingyihua healmenu hp change
		Message msg = new Message(MsgCenter.HERO_HP_CHANGE, this);
		msg.data = this;
		MsgCenter.instance.dispatch(msg);
		
		foreach(Hero hero in HeroMgr.heroHash.Values){
			if(hero is GRoot){
				GRoot groot = hero as GRoot;
				HeroData hd = groot.data as HeroData;
				if(hd.state == HeroData.State.SELECTED && !groot.isDead && groot.canShowGrootPassive("GROOT20B")){
					VineShield vs = this.transform.GetComponentInChildren<VineShield>();
					if(vs != null){
						groot.showGroot20BPassive(damage,atkerObj);	
					}
				}
			}
		}
		
		return dam;
	}
	
	public override void realDamage (int dam)
	{
		if(!this.isVineShield)
		{
			base.realDamage (dam);
		}
		else
		{
			
		}
	}
	
	public IEnumerator delayedAutoSelectTarget(float delay,GameObject atkerObj)
	{
		yield return new WaitForSeconds(delay); 
		
		if(!this.isDead && this.targetObj != atkerObj)
		{
			autoSelectTarget(atkerObj);
		}
	}
	//xingyihua 8.20f
	public override void addHp ( int hpNum  )
	{
		base.addHp(hpNum);
		Message msg = new Message(MsgCenter.HERO_HP_CHANGE, this);
		msg.data = this;
		MsgCenter.instance.dispatch(msg);
		StartCoroutine(changeVineShieldHpBar());
	}
	
	public IEnumerator changeVineShieldHpBar()
	{
		yield return new WaitForSeconds(0.5f);
		if(this.isVineShield && vineShield != null)
		{
			vineShield.initHPBar();
		}
	}
	
	protected virtual void autoSelectTarget ( GameObject atkerObj  )
	{		
		if (null != targetObj)
		{
			if(atkerObj.tag == gameObject.tag && !this.isAtkSameTag)
			{
				return;
			}
			moveToTarget(targetObj);
		}
		else if(null == targetObj && state == STANDBY_STATE)
		{
			if(atkerObj != null)
			{
				Character target = atkerObj.GetComponent<Character>();
				if(!target.getIsDead() && CAST_STATE != state)
				{
					if(atkerObj.tag == gameObject.tag && !this.isAtkSameTag)
					{
						return;
					}
					moveToTarget(atkerObj);
				}
			}
		}
	}
	
	protected virtual void atkAnimaScript (string s)
	{
		if(this.targetObj == null)
		{
			return;
		}
		Character target = this.targetObj.GetComponent<Character>();
		if(target.getIsDead())
		{
			return;
		}
		
		this.heroAI.OnAtkAnimaScriptTarget(target);
		
		if(target.getIsDead())
		{
			HeroData tempHeroD = this.data as HeroData;
			EquipData weapon = tempHeroD.getEquipData(EquipData.Slots.WEAPON);
			if(weapon != null)
			{
				//print(weapon.graphicsID);
				if (weapon.equipDef.iconID == "TARGETWEAPONNAME")
				{
					AchievementManager.incrAchievement("KILL_X_WITH_WEAPON");
				}
			}
		}
	}
	
	
	
	/*public void showLightCircel (){
		// isShowCircel = true;
		
		if(lightCircel.transform.parent != null)
		{
			Hero oldHero = lightCircel.transform.parent.gameObject.GetComponent<Hero>();
			if(oldHero!=null && oldHero.data.type != this.data.type)
			{
//				Debug.Log(oldHero.data.type+"   <----> "+this.data.type);
				oldHero.hideLightCircel();
				oldHero.hideHpBar();
			}
		}
		lightCircel.transform.parent = gameObject.transform;
		lightCircel.transform.position = transform.position;
		lightCircel.transform.position +=new Vector3(0,0,1);
//		lightCircel.transform.position +=1;
		showHpBar();
		lightCircel.renderer.enabled = true;
	}
	
	public void hideLightCircel (){
		// isShowCircel = false;
//		hideHpBar();
	}*/
	protected virtual void AnimaPlayEnd ( string animaName  )
	{
//		Debug.LogError("   <  "+(data as HeroData).type +"-"+ animaName+" End");
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Stand");
				break;
			case "Damage":
				playAnim("Stand");
				break;
			case "Select":
				playAnim("Stand");
				break;
			case "SuperAttack":
			case "SkillA":
			case "SkillA_b":
			case "Skill5A":
			case "Skill5B":
			case "Skill15A":
			case "Skill15A_a":
			case "Skill15B":
			case "Skill15B_b":
			case "Skill30A":
			case "Skill30A_a":
			case "Skill30B":
			case "SkillB":
			case "GroupAttackB":
				{
					SkillFinish();
					break;
				}
			case "Cure":
				standby();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				if (this.isAbnormalStateActive(ABNORMAL_NUM.LAYDOWN))
				{
					return;
				}
//				Invoke("destroyThis",1);
				DeathLate();
				break;
			default:
				break;
		}
	}
	
	public void DeathLate()
	{
		characterDeadMsg();
		cancelUsingShotgun();
		cancelIsInvincible();
		resurrectionTutorail();
		StartCoroutine(outBattle());
	}
	
	public virtual void characterDeadMsg ()
	{
		if(tag != "Enemy")
		{
			LevelMgr.deadHeroes.Add(this);
			Message msg = new Message(MsgCenter.HERO_DEAD, this);
			MsgCenter.instance.dispatch(msg);
		}
		else
		{
			Message msg = new Message(MsgCenter.ENEMY_DEAD, this);
			MsgCenter.instance.dispatch(msg);
		}
	}
	
	public IEnumerator outBattle()
	{
		yield return new WaitForSeconds(2.0f);
		this.gameObject.transform.position = new Vector3 (-1024, 0, 0);
	}
	
	public void showHP()
	{
		
		showHpBar();
	}
	public override void showHpBar ()
	{
//		if(TutorialBattleBg.getIsTutorial())
//		{
//			if(data.type == HeroData.MARINE)
//			{
//				transform.Find("HpBar").Find("hpRed").gameObject.SetActiveRecursively(false);
//			}
//			if(TutorialBattleBg.isHideHpBar)
//			{
//				hideHpBar();
//				return;
//			}
//		}
		
		if(isVineShield && vineShield != null)
		{
			vineShield.hpBar.showHpBar();
		}
		base.showHpBar();
		bool  isSelected = BattleObjects.skHero == this;
		if(BattleObjects.skHero != null &&
			!BattleObjects.skHero.Equals(this))
		{
			BattleObjects.skHero.hideHpBar();
		}
//		iTween.ScaleTo(hpBar.gameObject, {"x":1,"y":1,"z":1,"time":0.2f});
		//Debug.Log(isSelected);
		if(!isSelected)
		{
			if( IsInvoking("hideHpBar") )
			{
				CancelInvoke("hideHpBar");
				Invoke("hideHpBar",6);
			}else{
				Invoke("hideHpBar",6);
			}
		}
		else
		{
			//iTween.ScaleTo(hpBar.gameObject, {"x":1,"y":1,"z":1});
			if( IsInvoking("hideHpBar") )
			{
				CancelInvoke("hideHpBar");
			}
		}
	}
	
	void setHideHpBar (){
		Debug.Log("setHideHpBar!");
		hideHpBar();
	}
	
	public override void standby ()
	{
//		if(StaticData.isBattleEnd){
//			float dis = Vector2.Distance(transform.position,targetPt);
//			float spd = Time.deltaTime*realMspd;
//			if(Mathf.Abs(dis)>spd){
//				state = STANDBY_STATE;
//				move(targetPt);
//				return;
//			}
//		}
		if(this.state != Character.STANDBY_STATE)
		{
			this.startCheckOpponent();
		}
		base.standby();
	}
	
	public override void moveWhenTargetDead ()
	{
		Debug.Log("moveWhenTargetDead ling yi ge yuan yin ???????????????");
		this.heroAI.OnMoveWhenTargetDead();
	}
	
	protected void startRelive ()
	{
		relive();
		Message msg3 = new Message(MsgCenter.HERO_RELIVE, this);
		msg3.data = this;
		MsgCenter.instance.dispatch(msg3);
	}
	
	public void trinketEfts ( int dmg  )
	{
//		if(data.itemDM.atkHealPart > 0)
//		{
//			foreach(string key in HeroMgr.heroHash.Keys)
//			{
//				Hero hero = HeroMgr.heroHash[key] as Hero;
//				if(hero.data.type != data.type)
//				{
//					hero.addHp((int)(dmg*(data.itemDM.atkHealPart/100.0f)));
//				}
//			}
//		}
//		if(data.itemDM.atkHealSelf > 0)
//		{
//			addHp((int)(dmg*(data.itemDM.atkHealSelf/100.0f)));
//		}
	}
	
	private void onRelive ( Message evt  )
	{
		if(evt.data == data.type)
		{
			GameObject reliveEftPrb = Resources.Load("eft/reliveEft") as GameObject;
			GameObject reliveObj = null;
			if(data.type == HeroData.HEALER)
			{
				reliveObj = Instantiate(reliveEftPrb, this.transform.position+new Vector3(0,-2,-1), this.transform.rotation) as GameObject;
			}else{
				if(model.transform.localScale.x < 0)
				{
					reliveObj = Instantiate(reliveEftPrb, this.transform.position+new Vector3(30,-2,-1), this.transform.rotation) as GameObject;
				}else{
					reliveObj = Instantiate(reliveEftPrb, this.transform.position+new Vector3(-80,-2,-1), this.transform.rotation) as GameObject;
				}
			}
			Invoke("startRelive",1);
//			Message msg3 = new Message(MsgCenter.HERO_RELIVE, this);
//			msg3.data = this;
//			MsgCenter.instance.dispatch(msg3);
		}
	}
	
	
	
	protected virtual void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		
	}
	
//	public void DestroyListBattle()
//	{
//		(data as HeroData).DestroyListBattle();
//	}
	
	void OnDestroy (){
		
		MsgCenter.instance.removeListener(MsgCenter.CONSUME_ITEM_RELIVE , onRelive);
		if(lightCircel != null)
		{
				lightCircel.transform.position = new Vector3(lightCircel.transform.position.x, lightCircel.transform.position.y, 1000);
//			lightCircel.transform.position.z = 1000;
			lightCircel.transform.parent = null;
		}
	}
}

