using UnityEngine;
using System.Collections;

public class enemyWizard : EnemyRemote {
//
//	public GameObject bulletPrb;
//	protected GameObject bltObj;
//	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
	} 
	public override void Start (){
		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		CharacterData characterD = new CharacterData();
		pieceAnima.pauseAnima();
		birthPt = getBirthPt();
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//add by gwp at 20130219
//	public override void fearWithSeconds ( int seconds  ){
//		if(getIsDead()){return;}
//		if(abnormalState == ABNORMAL_NUM.FEAR)
//		{
//			return;
//		}
//		
//		
//		setAbnormalState(ABNORMAL_NUM.FEAR);
//		doubleSpeed();
//		Invoke("cancelFear",seconds);
//	}
	
	public override void move ( Vector3 vc3  ){ 
		if(vc3.x > 0){
		   vc3.x = vc3.x-150;
		}else{
		   vc3.x = vc3.x+150;
		}
		if(vc3.y > 240){
		   vc3.y = vc3.y-80;
		}
		if(vc3.y < (-240)){
		   vc3.y = vc3.y+80;
		}
		if(state == CAST_STATE)
		{
			return;
		}
		targetPt = vc3;
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		state = MOVE_STATE;
		playAnim("Move");
		setDirection(vc3);
		toward(vc3);
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

	public override void startAtk (){
		bool  b = isCollider();
		if(!b){
//			print("collider.bounds.Intersects:"+b);
			move(birthPt);
			return;
		}
		state = ATK_STATE;
		toward(targetObj.transform.position);
		isPlayAtkAnim = true;
		playAnim("Attack"); 
		attackTargetInvok();
		InvokeRepeating("attackTargetInvok", realAspd, realAspd);
	} 
	public override bool isCollider (){
			
		// BoxCollider bgBoxCollider = BattleBg.bgCollider.GetComponent<BoxCollider>();	
		Vector3 minVc3 = BattleBg.actionBounds.min;
		Vector3 maxVc3 = BattleBg.actionBounds.max;
		Rect rect = new Rect(minVc3.x, minVc3.y, maxVc3.x-minVc3.x, maxVc3.y-minVc3.y);
		
		Vector2 vc2 = new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y);
		return rect.Contains(vc2);
	}
	//add by gwp at 20130219
//	protected function moveStateUpdate()
//	{ 
//		float dis = Vector2.Distance(transform.position,targetPt);
//		float spd = Time.deltaTime*realMspd;
//		if(Mathf.Abs(dis)>spd){
//			if(dirVc3.y != 0){
//				setDepth();
//			}
//			transform.Translate(dirVc3*spd);
//		}else{ 
//			if(targetObj != null)
//			{ 
//				startAtk();
//			}//for reach target select act
//		}
//	}
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				if ( HeroMgr.getRandomHero() == null )
				{
					pieceAnima.pauseAnima();
					return;
				}
				playAnim("Stand");
				break;
			case "Damage":
				playAnim("Stand");
				break;
			case "Select":
				playAnim("Stand");
				break;
			case "SkillA":
				standby();
				break;
			case "SkillB":
				standby();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				Invoke("destroyThis",3);
				break;
			default:
				break;
		}
	}
//	//override
//	public function setTarget( GameObject gObj  )
//	{
//		
//	}
//	
//	//override
////	public function move( Vector3 vc3  )
////	{
////		targetPt = vc3;
////		state = MOVE_STATE;
////		playAnim("Move");
////		setDirection(vc3);
////		toward(vc3);
////	}
//	
//	//override
//	public function moveToTarget( GameObject obj  )
//	{
//		if(state == ATK_STATE)
//		{
//			cancelAtk();
//		}
//		targetObj = obj;
//		startAtk();
//	}
//	public function relive()
//	{	
//		super.relive(); 
//		Vector3 ve = new Vector3(0, 0, GData.objLayer);
//		this.move(ve);
//	}
//	 // weapon change test
//	public function changeWeapon( string weaponID  ):void
//	{
//		pieceAnima.showPiece("armDownR",weaponID);
//	}
//	
//	//override
////	protected function drawAvatar()
////	{
//////		EquipData eD = EquipFactory.lib[12];
//////		HeroData heroD = data;
//////		heroD.equipObj(EquipData.WEAPON, eD);  //WEAPON=weapons
////		super.drawAvatar();
////	}
//	
//	
//	//override
//	protected function moveTargetInAtkUpdate()
//	{
//		
//	}
//	
//	//override
//	protected function atkAnimaScript()
//	{
//		if(targetObj == null)
//		{
//			return;
//		}
//		Enemy enemy = targetObj.GetComponent<Enemy>();
//		if(enemy.getIsDead())
//		{
//			return;
//		}
//		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
//		Vector3 createPt;
//		if(transform.localScale.x > 0)
//		{
//			createPt = transform.position + new Vector3(50,70,0);
//		}else{
//			createPt = transform.position + new Vector3(-50,70,0);
//		}
//		shootBullet(createPt, vc3);
//	}
//	
////	protected function checkEnemy()
////	{
////		if(targetObj != null)
////		{
////			startAtk();
////		}else{
////			super.checkEnemy();
////		}
////		
////	}
////	protected function initSkill()
////	{
//////		SkillData skD = SkillData.create("WIZARD1","DiracSea",30);
//////		SkillData skD2 = SkillData.create("WIZARD2", "NaniteCannon", 45);
////
////		heroData = (data as HeroData);
////		
////		heroData.skillList = ["Dirac Sea","Nanite Cannon"];
////		heroData.passiveList = [];
////		
////		if(!isPuppet)
////		{
////	//		HeroData heroD = SceneInit.heroDataList[5];
////	
////	
////			//gwp
//////			HeroData heroD = (data as HeroData);
////			
////			//heroD.passiveList = [SkillLib.WIZARD_1A, SkillLib.WIZARD_2B, SkillLib.WIZARD_4A];
//////			print(heroD.passiveList);
//////			heroD.buildPassiveSkill();
//////			
//////			//heroD.skillList = [SkillLib.WIZARD_LIGHTING_BOLT,SkillLib.WIZARD_ENERGY_BEAM];
//////			print(heroD.skillList);
//////			heroD.buildActiveSkill();
////			
//////			(data as HeroData).skillListBattle = [skD,skD2];
////			//end
////			heroData.buildActiveSkill();
////			heroData.buildPassiveSkill();
////		}
////		EquipCalculator.calculate(heroData);
//////		initData(SceneInit.heroDataList[5]);
//////		(data as HeroData).isSelect = true;
////	}
//	
//	protected function shootBullet( Vector3 creatVc3 ,   Vector3 endVc3  )
//	{
//		float dis_y = endVc3.y - creatVc3.y;
//		float dis_x = endVc3.x - creatVc3.x;
//		float angle = Mathf.Atan2(dis_y, dis_x);
//		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
//		
//		bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation);
//		
//		float deg = (angle*360)/(2*Mathf.PI);
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
//		iTween.MoveTo(bltObj,{"x":endVc3.x, "y":endVc3.y, "speed":1500, "easetype":"linear", 
//								"oncomplete":"removeBullet", "oncompletetarget":gameObject});
//	}
//	
//	protected function removeBullet()
//	{
//		Destroy(bltObj);
//		if(targetObj != null)
//		{
////			Enemy enemy = targetObj.GetComponent<Enemy>();
////			//add by xiaoyong for critical strike
////			int dmg;
////			if( GData.computeChance(realCStk, 1) )
////			{
////				criticalHandler();
////				dmg = enemy.defenseAtk(realAtk*2, this.gameObject);
////			}else{
////				dmg = enemy.defenseAtk(realAtk, this.gameObject);
////			}
////			trinketEfts(dmg);
////			if(enemy.getIsDead()){
////				enemyDeadHandler();
////			}
////		}else{
////			if(enemy.getIsDead()){
////				standby();
////			}
//		}
//	}
	
}
