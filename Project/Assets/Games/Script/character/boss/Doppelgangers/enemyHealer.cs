using UnityEngine;
using System.Collections;

public class enemyHealer : EnemyRemote {

	GameObject healPrb;
	
	private GameObject targetEnemy; 
	public override void Awake (){
		base.Awake();
		scaleSize = new Vector3(0.65f, 0.65f, 1);
	}
	public override void initData ( CharacterData characterD  ){
		data = characterD;
		realHp  = characterD.maxHp;
//		realDef = (int) characterD.defense;
//		realAtk = (int)characterD.attack;
		realMspd= characterD.moveSpeed;
		realAspd= characterD.attackSpeed;
		realMaxHp = realHp;
		
		// delete by why 2014.2.7
//		realCStk=characterD.criticalStk/100.0f;
//		realEvade= DataModifier.EVADE_VALUE * characterD.evade;
//		realStk= DataModifier.STK_VALUE * characterD.strike;
		hpBar.initBar(realHp);
	}
	public void startHeal (){ 
		InvokeRepeating("healTarget", realAspd, realAspd);
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	public override void toggleEnable (){
		id = EnemyMgr.getID();
		EnemyMgr.enemyHash[id] = this;
		
		Hero hero = HeroMgr.getRandomHero();
		Vector3 vc3;
		if(hero.gameObject.transform.position.x > 0){
			vc3 = new Vector3(hero.gameObject.transform.position.x-100,hero.gameObject.transform.position.y,hero.gameObject.transform.position.z);
		}else{
			vc3 = new Vector3(hero.gameObject.transform.position.x+100,hero.gameObject.transform.position.y,hero.gameObject.transform.position.z);
		}
		move(vc3);

		startHeal();
//		isEnabled = true;
	}
	//override
	public override string getTargetTagType (){
		return "EnemyHealer";
	}
	//override
	public override void move ( Vector3 vc3  ){ 
	//add by gwp at 20130219
		if(state == ATK_STATE)
		{
			cancelAtk();
		} 
		
		targetPt = vc3;
		state = MOVE_STATE;
		playAnim("Move");
		setDirection(vc3);
		toward(vc3);
	}
	
	//override
	public override void setTarget ( GameObject gObj  ){
		
	}
	
	public void stopMoving (){
		if(state != ATK_STATE)
		{
			standby();
		}
	}
	// weapon change test
	protected void drawAvatar (){
//		EquipData eD = EquipFactory.lib[4];
//		HeroData heroD = data;
//		heroD.equipObj(EquipData.WEAPON, eD);
		//super.drawAvatar();
	}
	
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("sash",weaponID);
	}
	
//add by gwp at 20130219
//	public override void fearWithSeconds ( int seconds  ){
//		if(getIsDead()){return;}
//		if(abnormalState == ABNORMAL_NUM.FEAR)
//		{
//			return;
//		}
//		
//		abnormalState = ABNORMAL_NUM.FEAR;
//		if(!getIsDead()){
//			move(BattleBg.getPointInScreen());
//		}
//		doubleSpeed();
//		Invoke("cancelFear",seconds);
//	}
	
	
//	protected void drawAvatar (){}
	protected override void moveTargetInAtkUpdate (){}
	//add by gwp at 20130219
//	public override void cancelFear (){  
//		
//		abnormalState = ABNORMAL_NUM.NORMAL;
//		if(getIsDead()){
//			return;
//		}
//		normalSpeed();
//	}
	
	protected override void atkAnimaScript (string s){ 
		if(targetEnemy != null){
			Enemy enemy = targetEnemy.GetComponent<Enemy>();
			if(!enemy.getIsDead()){
//				enemy.addHp(realAtk);
				Vector3 pt = enemy.transform.position + new Vector3(0,10,0);
				GameObject healingObj = Instantiate(healPrb, pt, this.transform.rotation) as GameObject;
//				print("enemy add HP :"+realAtk);
			}
		}
	}
//	protected function initSkill()
//	{
//		if(!isPuppet)
//		{
//			SkillData skD = SkillData.create("sk04","ShockTherapy",30);
//			SkillData skD2 = SkillData.create("sk03", "RadiationOfScience", 45);
//			//HeroData heroD = SceneInit.heroDataList[4];
//			
//			//gwp
//			(data as HeroData).skillListBattel = [skD,skD2];
//			//end
//		}
//		
		
//		EquipCalculator.calculate(SceneInit.heroDataList[4]);
//		initData(SceneInit.heroDataList[4]);
//	}
	
	private void healTarget (){   
	//add by gwp at 20130219
		if(state == MOVE_STATE){
			return;
		}
		if(! this.isDead){
			state = ATK_STATE; 
			Enemy enemy =Doppelgangers.getAddHpObj(); 
			if(enemy != null){		
					targetEnemy = enemy.gameObject;	
					if(!enemy.getIsDead())
					{
						toward(targetEnemy.transform.position);
						isPlayAtkAnim = true;
						playAnim("Attack");
					}		
			}
		}
	} 
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{ 
			case "Attack":
//				print("attack-------->"); 	
				playAnim("Stand"); 
				break;
			case "Damage": 
				playAnim("Stand"); 
//				print("damage-------->");
				break;
			case "Skill":
//				print("skill-------->");
				playAnim("Stand"); 
				base.checkOpponent();
				break;
			case "Death":
				pieceAnima.pauseAnima(); 
				CancelInvoke("healTarget"); 
				Invoke("destroyThis",3); 
//				print("death-------->");
				break;
			default:
				break;
		}
	}

	public override void dead (string s=null){ 
		if(isDead)return;
		if(state == ATK_STATE)
		{
			cancelAtk();
		} 
		
		state = DEAD_STATE;
		isDead = true;
		data.isDead = true;
		playAnim("Death");
		iTween.Stop(gameObject);
		this.gameObject.collider.enabled = false;
		characterDeadMsg();
	}
	public override void characterDeadMsg (){
		Message msg = new Message(MsgCenter.COPY_HERO_DEAD, this);
		msg.data = this.data.type;
		MsgCenter.instance.dispatch(msg);
	}

//	public function startHeal( GameObject gameObj  )
//	{
//		Hero hero = gameObj.GetComponent<Hero>();
//		startHeal(hero);
//	}
//	public function startHeal( Hero hero  )
//	{
//		state = ATK_STATE;
//		targetObj = hero.gameObject;
//		if(!hero.getIsDead())
//		{
//			healTarget();
//			CancelInvoke("healTarget");
//			InvokeRepeating("healTarget", realAspd, realAspd);
//		}
//	}
//	
//	//override
//	public function getTargetTagType()
//	{
//		return "Player";
//	}
//	//override
//	public function move( Vector3 vc3  )
//	{
//		targetPt = vc3;
//		state = MOVE_STATE;
//		playAnim("Move");
//		setDirection(vc3);
//		toward(vc3);
//	}
//	
//	//override
//	public function setTarget( GameObject gObj  )
//	{
//		
//	}
//	
//	public function stopMoving()
//	{
//		if(state != ATK_STATE)
//		{
//			standby();
//		}
//	}
//	// weapon change test
////	protected function drawAvatar()
////	{
//////		EquipData eD = EquipFactory.lib[4];
//////		HeroData heroD = data;
//////		heroD.equipObj(EquipData.WEAPON, eD);
////		super.drawAvatar();
////	}
//	
//	public function changeWeapon( string weaponID  ):void
//	{
//		pieceAnima.showPiece("sash",weaponID);
//	}
//	
//	
////	protected void drawAvatar (){}
//	protected void startCheckEnemy (){}
//	protected void autoSelectTarget ( GameObject atkerObj  ){}
//	protected void moveTargetInAtkUpdate (){};
//	protected function standby()
//	{
//		if(state == ATK_STATE)
//		{
//			CancelInvoke("healTarget");
//		}
//		state = STANDBY_STATE;
//		doAnim("Stand");
//	}
//	
//	protected function atkAnimaScript()
//	{
//		Hero hero = targetObj.GetComponent<Hero>();
//		hero.addHp(realAtk);
//		Vector3 pt = hero.transform.position + Vector3(0,10,0);
//		GameObject healingObj = Instantiate(healPrb, pt, this.transform.rotation);
//		print("hero add HP :"+realAtk);
//	}
//	
////	protected function initSkill()
////	{
////	
////		heroData = (data as HeroData);
////		
////		heroData.skillList = ["Shock Therapy","Overclock"];
////		heroData.passiveList = [];
////		
////		if(!isPuppet)
////		{
//////			SkillData skD = SkillData.create("HEALER1","ShockTherapy",30);
//////			SkillData skD2 = SkillData.create("HEALER6", "Overclock", 45);
//////			//HeroData heroD = SceneInit.heroDataList[4];
//////			
//////			//gwp
//////			(data as HeroData).skillListBattle = [skD,skD2];
//////			//end
////			heroData.buildActiveSkill();
////			heroData.buildPassiveSkill();
////		}
////		EquipCalculator.calculate(heroData);
////		
//////		initData(SceneInit.heroDataList[4]);
////	}
//	
//	private function healTarget()
//	{
//		if( state != MOVE_STATE )
//		{
//			
//			if(targetObj != null)
//			{
//				Hero hero = targetObj.GetComponent<Hero>();
//				if(!hero.getIsDead())
//				{
//					toward(targetObj.transform.position);
//					isPlayAtkAnim = true;
//					playAnim("Attack");
//				}
//			}
//		}
//	}
}
