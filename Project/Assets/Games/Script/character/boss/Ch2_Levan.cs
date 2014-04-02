using UnityEngine;
using System.Collections;

public class Ch2_Levan : Enemy
{
	public GameObject bulletPrb;
	public GameObject bomb;
	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate SkillKeyFrameEvent;
	public noParmsDelegate showSkill15EftCallback;
	
	//for passive 20
	private Hashtable alreadyAddedAspd;
	
	public override void Start ()
	{
		initSkill();
		base.Start ();
		
		pieceAnima.addFrameScript("Skill15A_b",1,showSkill15Eft);
		pieceAnima.addFrameScript("Skill15A_b",21,atkAnimaScript);
		pieceAnima.addFrameScript("Skill30A",31, skillKeyFrameEvent);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill15A_b",1);
		pieceAnima.removeFrameScript("Skill15A_b",21);
		pieceAnima.removeFrameScript("Skill30A",31);
	}
	
	public override void Awake ()
	{
		base.Awake ();
		atkAnimKeyFrame = 10;
	}
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		
		alreadyAddedAspd = new Hashtable();
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN20");
		int increaseValue = (int)skillDef.passiveEffectTable["universal"];
		ArrayList heroesClone = new ArrayList(HeroMgr.heroHash.Values);
		foreach(Character character in heroesClone)
		{
			if(character != this)
			{
				float aspdValue = character.data.attackSpeed / (1.0f + increaseValue/100.0f) - character.data.attackSpeed;
				character.realAspd += aspdValue;
				ArrayList list = new ArrayList();
				list.Add(character);
				list.Add(aspdValue);
				alreadyAddedAspd[character.data.type] = list;
			}
		}
		
		skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN25");
		int atkPer = (int)skillDef.passiveEffectTable["sk_damage"];
		int atkSpd = (int)skillDef.passiveEffectTable["universal"];
		foreach(Character character in heroesClone)
		{
			if(character.data.type == "NEBULA")
			{
				float aspdValue = data.attackSpeed / (1.0f + atkSpd/100.0f) - data.attackSpeed;
				realAspd += aspdValue;
				realAtk.Multip(1+atkPer/100.0f);
				break;
			}
		}
	}
	
	public void showSkill15Eft(string s )
	{
		if(null != showSkill15EftCallback){
			showSkill15EftCallback();
		}
	}
	
	public void skill15AAnimaScript(string s)
	{
		if(SkillKeyFrameEvent != null && targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(!target.isDead)
			{
				SkillKeyFrameEvent(this);
			}
		}	
	}
	
	public void skillKeyFrameEvent(string s)
	{
		if(SkillKeyFrameEvent != null)
		{
			SkillKeyFrameEvent(this);
		}
	}

	public override void blinkInScreen()
	{
		gameObject.transform.position = BattleBg.getPointInScreen();
	}
	
	public override void attackTargetInvok ()
	{
		if(this.attackAnimaName == "Attack")
		{
			base.attackTargetInvok();
		}
		else
		{
			attackTargetInvokRemote();
		}
	}
	
	public void attackTargetInvokRemote ()
	{
		if(!this.isActionStateActive(Character.ActionStateIndex.AttackState))
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
	
	
	protected override void atkAnimaScript (string s)
	{
		
		if(this.attackAnimaName == "Attack")
		{
			base.atkAnimaScript(s);
			Character target = this.targetObj.GetComponent<Character>();
			if(!target.getIsDead())
			{
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN10");
				int dotDamage = (int)skillDef.passiveEffectTable["universal"];
				int time      = (int)skillDef.passiveEffectTable["universalTime"];
				int dotTotalHp = (int)(target.realMaxHp*(dotDamage/100.0f));
				target.addBuff("Skill_LEVAN10", time, dotTotalHp/time, BuffTypes.DE_HP);
			}
		}
		else
		{
			atkAnimaScriptRemote(s);
		}
	}
	
	public int attackCount = 0;
	public int attackCurrentCount = 0;
	
	protected void atkAnimaScriptRemote (string s)
	{
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		
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
		else if(attackCurrentCount == attackCount)
		{
			attackCurrentCount = 0;
			attackCount = Random.Range(40,60); // Arron said it moves too often
			MoveToRandomPoint(1.0f);
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

		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0, deg));
		
		
		bltObj.transform.localScale = new  Vector3(1.5f,1.5f,1);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "z",endVc3.z-10},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected virtual void removeBullet (GameObject bltObj)
	{

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
	
	protected override void AnimaPlayEnd ( string animaName  )
	{
		switch(animaName)
		{
			case "Skill1":
			case "SkillA":
			case "Skill5A":
			case "Skill30A":
			case "Skill15A":
				SkillFinish();
				break;
			
			case "Skill15A_a":
			case "Skill15A_b":
				if(!TsTheater.InTutorial)
				{
					if(targetObj != null)
					{
						this.state = Character.STANDBY_STATE;
						moveToTarget(targetObj);
					}
					else
					{
						standby();
					}
				}
				else
				{
					standby();
				}
				break;
		}
		base.AnimaPlayEnd(animaName);
	}
	
	protected void initSkill()
	{
		SkillEnemyManager.Instance.createSkillIcon("LEVAN1");
		SkillEnemyManager.Instance.createSkillIcon("LEVAN5A");
		SkillEnemyManager.Instance.createSkillIcon("LEVAN15A");
		SkillEnemyManager.Instance.createSkillIcon("LEVAN30A");
	}
	
	//override
	protected override void moveTargetInAtkUpdate ()
	{
		if(attackAnimaName == "Attack")
		{
			base.moveTargetInAtkUpdate();
		}
	}
	
	public override void UpdateBarrierPathToTarget()
	{
		if(attackAnimaName == "Attack")
		{
			base.UpdateBarrierPathToTarget();
		}
	}
	
	protected override void checkAtkerDefense ( GameObject atker  )
	{
		if(attackAnimaName == "Attack")
		{
			base.checkAtkerDefense(atker);
		}
		else if(attackAnimaName == "Skill15A_b")
		{
			checkAtkerDefenseRemote(atker);
		}
	}
	
	protected void checkAtkerDefenseRemote (GameObject atker)
	{
		if(!this.isActionStateActive(Character.ActionStateIndex.MoveState))
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
	public override void dead (string s=null)
	{
		foreach(ArrayList ary in alreadyAddedAspd.Values)
		{
			Character character = (Hero)ary[0];
			character.realAspd -= (float)ary[1];
		}
		alreadyAddedAspd.Clear();
		if(attackAnimaName == "Attack")
		{
			if(isDead)return;
			
		    CancelInvoke("moveToScene");	
			CancelInvoke("startGotoMove");
		}
		
		base.dead();
	}
	
	public override void moveToTarget ( GameObject obj  )
	{
		if(attackAnimaName == "Attack")
		{
			base.moveToTarget(obj);
		}
		else
		{
			moveToTargetRemote(obj);
		}
	}
	
	
	public void moveToTargetRemote ( GameObject obj  )
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
	
	public bool isMove = false;
	
	private bool checkBadPostionAndMoveAway()
	{
		float angle = Utils.myAngle(this.gunPosition,targetObj.transform.position);
		if(Mathf.Abs(angle) > 70 && Mathf.Abs(angle) < 130)
		{
			//Debug.LogError("bad position");
			MoveToPoint(BattleBg.getPointInAround(this.transform.position,80,100),1);
			return true;	
		}
		else if (BattleBg.IsOutOfActionBounce(transform.position))
		{
			isMove = true;
//			characterAI.OnMoveToPositionFinished += MoveToPositionFinished;
			this.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, MoveToPositionFinished);
			move(BattleBg.getPointInScreen());
			return true;
		}
		return false;	
	}
	
	public void MoveToPositionFinished(Character c = null)
	{
		normalSpeed();
		toggleEnable();
		isMove = false;
//		characterAI.OnMoveToPositionFinished -= MoveToPositionFinished;
		this.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, MoveToPositionFinished);
	}
	
	protected Vector3 gunPosition
	{
		get{
			return transform.position + new Vector3((model.transform.localScale.x > 0)? 85: -85,60,0);
		}
	}
	
	public void MoveToRandomPoint(float speed)
	{
		if(this.isActionStateActive(Character.ActionStateIndex.MoveState))
		{
			MoveToPoint(BattleBg.getPointInAround(gameObject.transform.position,50,100),speed);
		}
	}
	
	public void MoveToPoint(Vector3 point,float speed)
	{
		MoveToPoint(point);
		// move(BattleBg.getPointInScreen());
		multiSpeed(speed);
	}
	
	public void MoveToPoint(Vector3 point)
	{
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

	public void FixedUpdate()
	{
		if(this.attackAnimaName == "Skill15A_b")
		{
			OutOfActionBounds();
		}
	}
}
