using UnityEngine;
using System.Collections;

public class EnemyRemote : Enemy {
	public GameObject bulletPrb;
	public GameObject bomb;
	public GameObject attackEft;
	public GameObject moveEftPrb;
	public EffectAnimation moveEftDoc;
	//protected GameObject bltObj;
	public override void Awake (){
		base.Awake();
	}
	
	public override void Start (){
		getAnimKeyFrame();
		
		if(EnemyDataLib.Ch1_Male_Prisoner1 == data.type)
		{
			pieceAnima.addFrameScript("Attack", 17, atkAnimaScript);
			pieceAnima.addFrameScript("Attack", 18, playAtkMusic);
			pieceAnima.addFrameScript("Attack", 20, atkAnimaScript);
			pieceAnima.addFrameScript("Attack", 22, atkAnimaScript);
		}
		else  
		{
			pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		}
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		CharacterData characterD = new CharacterData();
		if(!StaticData.isTouch4)
		{
			pieceAnima.pauseAnima();
			birthPt = BattleBg.getPointInScreen();
		}
	}
	
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	public override void blinkInScreen()
	{
		if (invalidBirthPt != BirthPtInProfile)
		{
			gameObject.transform.position = BirthPtInProfile;
		}
		else if(StaticData.computeChance(30, 100) )
		{
			gameObject.transform.position = BattleBg.getPointInScreen();
		}
		else
		{
			 birthPt = getBirthPt();
			 gameObject.transform.position = birthPt;
		}
	}
	
	public override void relive (){
		
		blinkInScreen();
		setDepth();
		hpBar.resetView();
		isDead = false;
		data.isDead = false;
		initData(data);
		playAnim("Move");
		this.gameObject.collider.enabled = true;
		attackCount = Random.Range(40,60);
		if(moveEftPrb){
			gameObject.SetActiveRecursively(false);	
			
			GameObject moveEftObj = Instantiate(moveEftPrb, transform.position+new Vector3(0,56,-1), transform.rotation) as GameObject;
			moveEftDoc = moveEftObj.GetComponent<EffectAnimation>();
			
//			moveEftDoc.addFrameScript("enemyRemoteEft", 26, delayRelive);
			delayRelive();
//			Invoke("delayRelive", 0.5f);
		}else{
			//CancelInvoke("destroyThis");
			toggleEnable();
		}
	}
	
	protected void reliveInit (){
		gameObject.SetActiveRecursively(true); 
		toggleEnable();
		InvokeRepeating("startGotoMove",5,10);
	}
	
	private void startGotoMove (){
		
		if(getIsDead()){
			return;
		}
		
		moveEftDoc.transform.position = transform.position+new Vector3(0,56,-1);
		moveEftDoc.playAct("enemyRemoteEft");
		
		moveToScene();

	}
	private void moveToScene ()
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
//		model.renderer.enabled = true;
		if (BarrierMapData.Enable)
		{
			MoveToRandomPoint(1.0f);	
		}
		else{
			transform.position =  BattleBg.getPointInAround(transform.position,50,100);
			setDepth();
			moveEftDoc.transform.position = transform.position+new Vector3(0,56,-1);
			moveEftDoc.playAct("enemyRemoteEft");
		}
	}
	
	private void moveStop (string s){
		pieceAnima.pauseAnima();
	}
	private void delayRelive (){
		reliveInit();
	}
	
	//override
//	protected function startAtk()
//	{
//		state = ATK_STATE;
//		toward(targetObj.transform.position);
//		isPlayAtkAnim = true;
//		playAnim("Attack");
//	}
	
	//override
	public override void attackTargetInvok ()
	{
		if(!this.isActionStateActive(ActionStateIndex.AttackState))
		{
			return;
		}
		if(this.targetObj != null)
		{ 
			
			Character character = targetObj.GetComponent<Character>();
			
			bool  b = this.isCollider();
			if(!b && character.getIsDead())
			{
				this.targetObj = null;
				this.standby();
				return;
			}
			if(!character.getIsDead())
			{
				if(this.enemyAI.OnAtkAnimaScriptTargetBefore())
				{
					this.enemyAI.OnAttackTargetInvokAttackTarget();
				}
			}
			else
			{
				standby();
			}
		}
	}

	
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Move");
				break;
			case "Damage":
				if (this.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN))
				{
					playAnim("Move");
				}
				break;
			case "Death":
				pieceAnima.pauseAnima();
				if (this.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN))
				{
					return;
				}
				DeathLate();

				break;
			default:
				break;
		}
	}
		
	public int attackCount = 0;
	public int attackCurrentCount = 0;
	protected Vector3 gunPosition
	{
		get{
			switch(data.type)
			{
			case EnemyDataLib.Ch1_Male_Prisoner2:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 85: -85,60,0);
				break;
			case EnemyDataLib.Ch1_Male_Prisoner1:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			case EnemyDataLib.Ch1_Korath:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			case EnemyDataLib.Ch1_Female_Prisoner1:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			case EnemyDataLib.Ch1_Guard2:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 85: -85,80,0);
				break;
			case EnemyDataLib.Ch2_Nebula:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			case EnemyDataLib.Ch3_PitMonster2:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 120: -120,90,0);
				break;
			case EnemyDataLib.Ch3_Boss_RedKing:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 150: -150,120,0);
				break;
			case EnemyDataLib.Ch4_SmallMinion2:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			default:
				return transform.position + new Vector3((model.transform.localScale.x > 0)? 70: -70,80,0);
				break;
			}
		}
	}
	
	protected void playAtkMusic(string s){
		MusicManager.playEffectMusic("SFX_enemy_range_attack_1a");	
		if(this.targetObj != null){
			Character c = this.targetObj.GetComponent<Character>();
			c.isOnceMusic = true;
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		if(EnemyDataLib.Ch1_Male_Prisoner1 != data.type){
			MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");		
		}
		if(targetObj == null)
		{
			this.enemyAI.OnAttackTargetInvokTargetIsNull();
			return;
		}
		Character character = targetObj.GetComponent<Character>();
		if(character.getIsDead())
		{
			this.enemyAI.OnAtkAnimaScriptTargetIsDeath();
			return;
		}
//		if(!this.enemyAI.OnAttackTargetInvokAttackTargetBefore())
//		{
//			return;
//		}
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		shootBullet(gunPosition,vc3);
		getFlyingDroneEftPosition();
		
		++attackCurrentCount;
		
		if(checkBadPostionAndMoveAway()) 
		{
			//bad position 
		}
		else if(attackCurrentCount >= attackCount)
		{
			attackCurrentCount = 0;
			attackCount = Random.Range(40,60); // Arron said it moves too often
			MoveToRandomPoint(1.0f);
		}
	}
	
	private bool checkBadPostionAndMoveAway()
	{
		float angle = Utils.myAngle(this.gunPosition,targetObj.transform.position);
		if(Mathf.Abs(angle) > 70 && Mathf.Abs(angle)<130){
			//Debug.LogError("bad position");
			MoveToPoint(BattleBg.getPointInAround(this.transform.position,80,100),1);
			return true;	
		}
		else 
		if (BattleBg.IsOutOfActionBounce(transform.position)){
			isMove = true;
//			characterAI.OnMoveToPositionFinished += MoveToPositionFinished;
			this.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, MoveToPositionFinished);
			move(BattleBg.getPointInScreen());
			return true;
		}
		return false;	
	}
	public override void moveToTarget ( GameObject obj  )
	{	
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		if(isDead)return;//add by xiaoyong 20120729  for enemy relive
		//xingyh 4.9f enemy out of the screen
		
		if( targetObj )
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		if(checkBadPostionAndMoveAway())
		{
			
		}
		else
		{
			startAtk();
		}
	}
	
    private void getFlyingDroneEftPosition (){
    	
    	if(attackEft != null)
		{
			GameObject eft = null;
			if(model.transform.localScale.x > 0)
	    	{
				eft = Instantiate(attackEft,new Vector3(transform.position.x+100,transform.position.y+50,transform.position.z),transform.rotation) as GameObject;
				eft.transform.localScale = new Vector3(.5f,.5f,0);
			}else{
				eft = Instantiate(attackEft,new Vector3(transform.position.x-100,transform.position.y+50,transform.position.z),transform.rotation) as GameObject;
				eft.transform.localScale = new Vector3(-.5f,.5f,0);
			}
		}
    }		
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		//if(data.type == EnemyDataLib.CP)
		//{
		//	bulletPrb.transform.localScale = new Vector3(2.5f,2.5f,1);
		//}else{
//			bulletPrb.transform.localScale = new  Vector3(1.5f,1.5f,1);
		//}
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0, deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		if(bltObj.name == "RedKing_Bullet(Clone)") bltObj.transform.localScale = new  Vector3(0.7f,0.7f,1);
		else if(this.name == "enemyCh2_Nebula(Clone)") bltObj.transform.localScale = new Vector3(0.064f, 0.064f, 1);
		else bltObj.transform.localScale = new  Vector3(1.5f,1.5f,1);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "z",endVc3.z-10},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	public override void dead (string s=null)
	{
		if(isDead)return;
		
	    CancelInvoke("moveToScene");	
		CancelInvoke("startGotoMove");
		base.dead();
	}
	
	protected virtual void removeBullet (GameObject bltObj)
	{
//		if(data.type == EnemyDataLib.CR)
//		{
//			GameObject bombObj = Instantiate(bomb,bltObj.transform.position,transform.rotation);
//			Destroy(bltObj);
//		}
		
		if(!StaticData.isTouch4)
		{
			GameObject bombObj = null;
			if(bomb!= null)
			{	
				bombObj = Instantiate(bomb,bltObj.transform.position,transform.rotation) as GameObject;
				if(bombObj!=null)
				{
					PackedSprite bltObjInfo = bombObj.GetComponent<PackedSprite>();
					bltObjInfo.PlayAnim("Explosion"); 
				}
			}
		}
		Character character = null;
		if(targetObj != null)
		{
			character = targetObj.GetComponent<Character>();
			
			showHarmEft(data.type);
			
//			if(StaticData.computeChance((int)realCStk*100, 100) )
//			{
//				criticalHandler();
//				if(!cstkAnimPrb)
//				{
//					cstkAnimPrb = Resources.Load("eft/CritEft") as GameObject;
//				}
//				GameObject cstkAnimObj = Instantiate(cstkAnimPrb, targetObj.transform.position+new Vector3(0,45,-1),this.gameObject.transform.rotation) as GameObject;
//				damage += realAtk;
//			}
			
			character.defenseAtk(realAtk, this.gameObject);
			if(character.getIsDead())
			{
				startCheckOpponent();
			}
		}
		else
		{
			startCheckOpponent();
		}
		Destroy(bltObj);
	}
	
	
	protected override void checkAtkerDefense ( GameObject atker  )
	{
		if(!this.isActionStateActive(ActionStateIndex.AttackState))
		{
			return;
		}
		if(targetObj != null)
		{
			Character character = atker.GetComponent<Character>();
			Character currentCharacter = targetObj.GetComponent<Character>();
			
			if (character.data.type != currentCharacter.data.type)
			{
				targetObj = atker;
			}
		}
	}
	
	//override
	protected override void moveTargetInAtkUpdate ()
	{
		
	}
	
	public bool isMove = false;
	
	public void MoveToPositionFinished(Character c = null)
	{
		normalSpeed();
		toggleEnable();
		isMove = false;
//		characterAI.OnMoveToPositionFinished -= MoveToPositionFinished;
		this.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, MoveToPositionFinished);
	}
	
	public void MoveToRandomPoint(float speed)
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		MoveToPoint(BattleBg.getPointInAround(gameObject.transform.position,50,100),speed);
		
	}
	
	public void MoveToPoint(Vector3 point,float speed)
	{

		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		MoveToPoint(point);
		// move(BattleBg.getPointInScreen());
		multiSpeed(speed);

	}
	
	public void MoveToPoint(Vector3 point)
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		
		isMove = true;
		
		this.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, MoveToPositionFinished);
		move(point);
		
	}
	
	
	public void OutOfActionBounds()
	{
		if(!BattleBg.actionBounds.Contains(targetPt) &&
			!BattleBg.actionBounds.Contains(gameObject.transform.position) && 
			!getIsDead() &&
			enemyType !="" && // is in screen
			!isMove)
		{
			MoveToRandomPoint(1.0f);
		}
	}
	
	/*public override void Update ()
	{
		
		OutOfActionBounds();
		
		base.Update();
	}*/
	public void FixedUpdate()
	{
		OutOfActionBounds();
	}
	
	public override void UpdateBarrierPathToTarget()
	{
		//do not follow the target, do nothing. by roger
	}
}
