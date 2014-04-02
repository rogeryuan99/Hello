using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character
{
	protected static Vector2 invalidBirthPt = new Vector2(float.MinValue, float.MinValue);
	protected static ArrayList birthPts=new ArrayList(){new Vector2(-500,-300),new Vector2(-500,50),new Vector2(500,-300),new Vector2(500,50)};
	protected static ArrayList birthPts_ip5=new ArrayList(){new Vector2(-700,-300),new Vector2(-700,50),new Vector2(700,-300),new Vector2(700,50)};
	protected Vector2 birthPt;
	protected Vector2 birthPtInProfile = invalidBirthPt;
	public Vector2 BirthPtInProfile{
		get{ 
			return birthPtInProfile; 
		}
		set{ 
			if (null != value && invalidBirthPt != value)
			{
				float x = BattleBg.fingerBounds.size.x / 2f * value.x;
				float y = BattleBg.fingerBounds.size.y / 2f * value.y;
				if (1 == Mathf.Abs(value.x)){
					x += (x > 0)? 100f: -100f;
				}
				if (-1 == value.y){
					y -= 100;
				}
				birthPtInProfile = new Vector2(x, y) + (Vector2)BattleBg.fingerBounds.center;
			}
			else
			{
				birthPtInProfile = invalidBirthPt;
			}
		} 
	}
	
	public delegate void DieCallbackDelegate();
	public event DieCallbackDelegate OnDie;//added by roger for tutorial
	
	public EnemyAI enemyAI;
	
	public override void Awake ()
	{
		base.Awake();
		initAbnormalStateTable();
		MsgCenter.instance.addListener(MsgCenter.HERO_RELIVE, heroRelive);
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 13;
	}
	
	public override void getCharacterAI ()
	{
		this.characterAI = gameObject.GetComponent<EnemyAI>();
		this.enemyAI = this.characterAI as EnemyAI;
	}
	
	public override void setCharacterAI(Type aiType)
	{
		base.setCharacterAI(aiType);
		getCharacterAI();
		this.enemyAI.getCharacter();
	}
	
	//xingyihua 8.9f
	protected void heroRelive (Message msg)
	{
		if(enemyType != "" && isDead == false)
		{
			startCheckOpponent();
		}
	}
	
	public override void Start ()
	{
		getAnimKeyFrame();
		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd); 
		if(!StaticData.isTouch4)
		{
			// pieceAnima.pauseAnima();
			birthPt = getBirthPt();
		}
	}
	public override void initData ( CharacterData characterD  )
	{
		base.initData(characterD);
		realMaxHp = realHp;
	}
	
	public void getAnimKeyFrame ()
	{
		switch(data.type)
		{
		case EnemyDataLib.Ch1_Guard1:
				atkAnimKeyFrame = 8;	
				break;
		case EnemyDataLib.Ch1_Guard2:
				atkAnimKeyFrame = 33;	
				break;
		case EnemyDataLib.Ch1_Korath:
				atkAnimKeyFrame = 33;	
				break;
		case EnemyDataLib.Ch1_Female_Prisoner1:
				atkAnimKeyFrame = 11;	
				break;
		case EnemyDataLib.Ch1_Female_Prisoner2:
				atkAnimKeyFrame = 17;
				break;
		case EnemyDataLib.Ch1_Male_Prisoner1:
				atkAnimKeyFrame = 13;
				break;
		case EnemyDataLib.Ch1_Male_Prisoner2:
				atkAnimKeyFrame = 33;
				break;
		case EnemyDataLib.Ch1_Male_Prisoner3:
				atkAnimKeyFrame = 10;
				break;
		case EnemyDataLib.Ch1_Male_Prisoner4:
				atkAnimKeyFrame = 11;
				break;
		case EnemyDataLib.Ch2_Levan:
				atkAnimKeyFrame = 17;
				break;
		case EnemyDataLib.Ch2_Nebula:
				atkAnimKeyFrame = 11;	
				break;
		case EnemyDataLib.Ch2_Skunge:
				atkAnimKeyFrame = 14;	
				break;
		case EnemyDataLib.Ch3_Boss_RedKing:
				atkAnimKeyFrame = 11;
				break;
		case EnemyDataLib.Ch3_Caiera:
				atkAnimKeyFrame = 17;
				break;
		case EnemyDataLib.Ch3_Miek:
				atkAnimKeyFrame = 17;
				break;
		case EnemyDataLib.Ch3_PitMonster1:
				atkAnimKeyFrame = 14;	
				break;
		case EnemyDataLib.Ch3_PitMonster2:
				atkAnimKeyFrame = 6;
				break;
		case EnemyDataLib.Ch4_TinyMinion1:
				atkAnimKeyFrame = 14;
				break;
		case EnemyDataLib.Ch4_SmallMinion2:
				atkAnimKeyFrame = 10;
				break;
		default:
			break;
		}
	}
	
	public override void fearWithSeconds()
	{
		this.setClearState();
		
		base.fearWithSeconds();
	}
	
	public override void cancelFear ()
	{ 
		base.cancelFire();
		
		this.goOnAttack();
	}
	
	public override void fireWithSeconds ()
	{
		
		base.fireWithSeconds();	
	}
	
	public override void cancelFire ()
	{ 
		base.cancelFire();
		
		this.goOnAttack();
	}
	
	public override void twineToNormal()
	{		
		base.twineToNormal();
		
		this.goOnAttack();
	}
	
	public override void twistToNormal ()
	{		
		base.twistToNormal();
		
		this.goOnAttack();
	}
	
	public override void freezeToNormal ()
	{		
		base.freezeToNormal();
		
		this.goOnAttack();
	}
	
//	public override void setAbnormalState ( ABNORMAL_NUM abnormal  )
//	{
//		if(abnormal == ABNORMAL_NUM.NORMAL && 
//			(this.abnormalState == ABNORMAL_NUM.FEAR || this.abnormalState == ABNORMAL_NUM.FIRE || this.abnormalState == ABNORMAL_NUM.TWINE) && 
//			(this.abnormalState == ABNORMAL_NUM.TWIST || this.abnormalState == ABNORMAL_NUM.FREEZE || this.abnormalState == ABNORMAL_NUM.STUN))
//		{
//			if(!getIsDead())
//			{
//				startCheckOpponent();
//			}
//		}
//		base.setAbnormalState(abnormal);
//	}
	
	public virtual void toggleEnable ()
	{
		startCheckOpponent();
//		isEnabled = true;
	}
	
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	public virtual void blinkInScreen()
	{
		if (invalidBirthPt != BirthPtInProfile){
			gameObject.transform.position = BirthPtInProfile;
		}
		else if (StaticData.computeChance(30, 100) )
		{
			gameObject.transform.position = BattleBg.getPointInScreen();
		}
		else
		{
			gameObject.transform.position = birthPt;
		}
	}
	
	public virtual void relive ()
	{
		blinkInScreen();
		
		setDepth();
		hpBar.resetView();
		isDead = false;
		data.isDead = false;
		initData(data);
		playAnim("Move");
		this.gameObject.collider.enabled = true;
		setAbnormalState( ABNORMAL_NUM.NORMAL);
		clearAbormalState();
		
		toggleEnable();
	}
	
	
	
	public override void cancelLayDown ()
	{
		base.cancelLayDown();
		goOnAttack();
	}
	
	public override void attackTargetInvok ()
	{
		if(!this.isActionStateActive(ActionStateIndex.AttackState) || this.state == CAST_STATE)
		{
			return;
		}
		
		if(this.targetObj != null)
		{ 
			//xingyh 4.9f enemy out of the screen
			Character character = this.targetObj.GetComponent<Character>();
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
				this.enemyAI.OnAttackTargetInvokTargetIsDeath();
			}
		}
		else
		{
			this.enemyAI.OnAttackTargetInvokTargetIsNull();
		}
	}
	
	protected virtual void atkAnimaScript (string s)
	{
		if(this.targetObj!=null)
		{
			Character opponent = this.targetObj.GetComponent<Character>();		
			if(opponent.getIsDead())
			{
				this.enemyAI.OnAtkAnimaScriptTargetIsDeath();
			}
			else
			{
				this.enemyAI.OnAtkAnimaScriptTarget(opponent);
			}
		}
		else
		{
			this.enemyAI.OnAtkAnimaScriptTargetIsNull();
		}
	}
	
	public void playAtkEft ()
	{
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		if(slashEft == null)
		{
			return;
		}
		getInStanPosition(data.type);
		
		showHarmEft(data.type);
	}
	
	public void showHarmEft(string type)
	{
		if(harmEftPrb == null || targetObj == null)
		{
			return;
		}
		
		Vector3 Vec3 = Vector3.zero;
		Vector3 Vec3_other = Vector3.zero;
		
		if(type == EnemyDataLib.Ch1_Korath)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Guard1)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Guard2)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Female_Prisoner1)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Female_Prisoner2)
		{
			Vec3 = new Vector3(20,60,-50);
			Vec3_other = new Vector3(-20,60,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner1)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner2)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner3)
		{
			Vec3 = new Vector3(0,0,-50);
			Vec3_other = new Vector3(-0,0,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner4)
		{
			Vec3 = new Vector3(0,0,-50);
			Vec3_other = new Vector3(-0,0,-50);
		}
		else if(type == EnemyDataLib.Ch2_Levan)
		{
			Vec3 = new Vector3(0,50,-50);
			Vec3_other = new Vector3(-0,50,-50);
		}
		else if(type == EnemyDataLib.Ch2_Nebula)
		{
			Vec3 = new Vector3(-10,50,-50);
			Vec3_other = new Vector3(10,50,-50);	
		}
		else if(type == EnemyDataLib.Ch2_Skunge)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch3_Boss_RedKing)
		{
			Vec3 = new Vector3(20,80,-50);
			Vec3_other = new Vector3(-20,80,-50);
		}
		else if(type == EnemyDataLib.Ch3_Caiera)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);
		}
		else if(type == EnemyDataLib.Ch3_Miek)
		{
			Vec3 = new Vector3(20,50,-50);
			Vec3_other = new Vector3(-20,50,-50);	
		}
		else if(type == EnemyDataLib.Ch3_PitMonster1)
		{
			Vec3 = new Vector3(0,60,-50);
			Vec3_other = new Vector3(-0,60,-50);	
		}
		else if(type == EnemyDataLib.Ch3_PitMonster2)
		{
			Vec3 = new Vector3(0,50,-50);
			Vec3_other = new Vector3(-0,50,-50);
		}
		else if(type == EnemyDataLib.Ch4_TinyMinion1)
		{
			Vec3 = new Vector3(0,50,-50);
			Vec3_other = new Vector3(-0,50,-50);
		}
		else if(type == EnemyDataLib.Ch4_SmallMinion2)
		{
			Vec3 = new Vector3(0,50,-50);
			Vec3_other = new Vector3(-0,50,-50);
		}
		
		
		GameObject harmEft = null;
		
		Character charachter = targetObj.GetComponent<Character>();
		
		if(charachter.model.transform.localScale.x > 0)
		{
			harmEft = Instantiate(harmEftPrb, targetObj.transform.position+Vec3, transform.rotation) as GameObject;
			
		}else{
			harmEft = Instantiate(harmEftPrb, targetObj.transform.position+Vec3_other, transform.rotation) as GameObject;
			
		}
		
		//fake effect , roger
		harmEft.transform.parent = targetObj.transform;	
		
		if(charachter.model.transform.localScale.x > 0)
		{
			harmEft.transform.localScale = new Vector3(3, 3, 3);
		}
		else
		{
			harmEft.transform.localScale = new Vector3(-3, 3, 3);
		}
	}
	
	protected void getInStanPosition ( string type  ){
		
		Vector3 Vec3 = Vector3.zero;
		Vector3 Vec3_other = Vector3.zero;
		if(type == EnemyDataLib.Ch1_Female_Prisoner2)
		{
			Vec3 = new Vector3(20,60,-50);
			Vec3_other = new Vector3(-20,60,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner3)
		{
			Vec3 = new Vector3(20,60,-50);
			Vec3_other = new Vector3(-20,60,-50);
		}
		else if(type == EnemyDataLib.Ch1_Male_Prisoner4)
		{
			Vec3 = new Vector3(20,60,-50);
			Vec3_other = new Vector3(-20,60,-50);
		}
		else if(data.type == EnemyDataLib.Ch1_Guard1)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		else if(data.type == EnemyDataLib.Ch2_Levan)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		else if(data.type == EnemyDataLib.Ch2_Skunge)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		else if(data.type == EnemyDataLib.Ch3_Caiera)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		else if(data.type == EnemyDataLib.Ch3_Miek)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		else if(data.type == EnemyDataLib.Ch3_PitMonster1)
		{
			Vec3 = new Vector3(130,60,-50);
			Vec3_other = new Vector3(-130,60,-50);
		}
		else if(data.type == EnemyDataLib.Ch4_TinyMinion1)
		{
			Vec3 = new Vector3(100,50,-50);
			Vec3_other = new Vector3(-100,50,-50);
		}
		
		GameObject atkEft = null;
		if(model.transform.localScale.x > 0)
		{
			atkEft = Instantiate(slashEft, transform.position+Vec3, transform.rotation) as GameObject;
			
		}else{
			atkEft = Instantiate(slashEft, transform.position+Vec3_other, transform.rotation) as GameObject;
			
		}
		
		//fake effect , roger
		atkEft.transform.parent = this.gameObject.transform;	
		
		if(model.transform.localScale.x > 0)
		{
			atkEft.transform.localScale = new Vector3(3, 3, 3);
		}
		else
		{
			atkEft.transform.localScale = new Vector3(-3, 3, 3);
		}
	}

	
	protected virtual void AnimaPlayEnd ( string animaName  )
	{
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Damage":
				// modified by xiaoyong 20130604 for daily quest---->
				if (this.isAbnormalStateActive(ABNORMAL_NUM.LAYDOWN))
				{
					playAnim("Move");
				}
				//<---------------------
				break;
			case "Skill":
				playAnim("Move");
				break;
			case "Death":
				pieceAnima.pauseAnima();
				if (this.isAbnormalStateActive(ABNORMAL_NUM.LAYDOWN))
				{
					return;
				}
				DeathLate();
				break;
			default:
				break;
		}
	}
	
	public virtual void DeathLate()
	{		
//	 	if(this.abnormalState == ABNORMAL_NUM.TWINE)
//		{
//			Invoke("twineToNormal",1);
//		}
//		if(this.abnormalState == ABNORMAL_NUM.TWIST)
//		{
//			Invoke("twistToNormal",1);
//		}
//		if(this.abnormalState == ABNORMAL_NUM.STUN)
//		{
//			Invoke("stunToNormal",1);	
//		}
	
		Invoke("destroyThis", 1);
		CallOnDie();
	}
	
	protected void CallOnDie()
	{
		if(OnDie != null)
		{
			OnDie();
		}
	}
	
	public virtual void goOnAttack()
	{
		if(isDead)
		{
			return;
		}
		
		if(targetObj != null)
		{
			Character character = targetObj.GetComponent<Character>();
			if(!character.getIsDead())
			{
				moveToTarget(targetObj);
			}
			else
			{
				this.startCheckOpponent();
			}
		}
		else
		{
			startCheckOpponent();
		}
	}
	
	public override void dead (string s=null)
	{
		if (this.isDead)
			return;
				
		BirthPtInProfile = invalidBirthPt;
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
		this.characterDeadMsg();
		
	}
	
	public virtual void characterDeadMsg ()
	{
		Message msg = new Message(MsgCenter.ENEMY_DEAD, this);
		MsgCenter.instance.dispatch(msg);
	}
	
	public virtual void removeInList ()
	{
		EnemyMgr.delEnemy(id);
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  )
	{
		int dmg;
		dmg = base.defenseAtk(damage, atkerObj);
		
		StartCoroutine(this.delayedCheckAtkerDefense(this.realAspd * 2, atkerObj));
		
		return dmg;
	}
	
	public override SkillIconData pickASkillDataFromContainer(string skId)
	{
//		ArrayList skills = getSkillList();
		SkillIconData data = SkillEnemyManager.Instance.getSkillIconData(skId);
			
		skContainer.Remove(skId);
		
		return data;
	}
	
	public IEnumerator delayedCheckAtkerDefense(float delay,GameObject atkerObj)
	{
		yield return new WaitForSeconds(delay);
		if(!this.isDead && this.targetObj != atkerObj)
		{
			checkAtkerDefense(atkerObj);
		}
	}
	
	public override void moveWhenTargetDead ()
	{
		this.enemyAI.OnMoveWhenTargetDead();
	}
	
	// Reader: FuNing _2014_02_07
	// Mender: FuNing _2014_02_07
	protected Vector2 getBirthPt ()
	{
		Vector2 pos;
		pos = (invalidBirthPt == BirthPtInProfile)?
				(StaticData.isiPhone5)? (Vector2)birthPts_ip5[ int.Parse( UnityEngine.Random.Range(0,birthPts.Count).ToString() )]
										: (Vector2)birthPts[ int.Parse( UnityEngine.Random.Range(0,birthPts.Count).ToString() )]
				: BirthPtInProfile;
		return BarrierMapData.Instance.GetClosestValidPositionOnTheEdge(pos);
		/*if (GData.isiPhone5) {
			return (Vector2)birthPts_ip5[ int.Parse( Random.Range(0,birthPts.Count).ToString() )];
		}else {
			return (Vector2)birthPts[ int.Parse( Random.Range(0,birthPts.Count).ToString() )];
		}*/
		/*Vector2 pos = (Vector2)birthPts_ip5[ int.Parse( Random.Range(0,birthPts.Count).ToString() )];
		BarrierMapData.Instance.GetClosestValidPositionOnTheEdge(pos);
		
		return pos;*/
	}
	/*
		Attack tank after hit; if only 1 revived hero start attack 
	*/
	
	protected override void checkAtkerDefense ( GameObject atker  )
	{
//		if(this.abnormalState == Character.ABNORMAL_NUM.TAUNT || 
//			this.abnormalState == Character.ABNORMAL_NUM.LAYDOWN ||
//			this.abnormalState == Character.ABNORMAL_NUM.FIRE ||
//			this.abnormalState == Character.ABNORMAL_NUM.FEAR ||
//			this.abnormalState == Character.ABNORMAL_NUM.STUN)
//		{
//			return;
//		}
		
		if(this.targetObj != null &&
			!this.isDead)
		{
			this.enemyAI.OnCheckAtkerDefense(atker);
		}
	}
	
	private static bool isBlockRandomAttack = false;
	
	public static void BlockRandomAttack()  { isBlockRandomAttack = true;  }
	public static void UnblockRandomAttack(){ isBlockRandomAttack = false; }
	
	public override bool checkOpponent ()
	{
		if (isBlockRandomAttack || state == CAST_STATE) 
			return false;
		
		// Character opponent = this.enemyAI.getOpponent();
		Character opponent = (this.isAbnormalStateActive(Character.ABNORMAL_NUM.CREAZY))? EnemyMgr.getRandomEnemy(this): this.enemyAI.getOpponent();
		
		//for NullReferenceException when last hero dead 
		if(opponent == null)return false;
		
		if(opponent.lossTargetBeforeState == Character.LossTargetBeforeState.LOSSTARGET){
			opponent = null;
		}
		if(opponent!=null)
		{
			cancelCheckOpponent();
			this.enemyAI.OnGetOpponentLater(opponent);
			
			return true;
		}
		return false;
	}
	
	protected void miniBossAchievement (){
		AchievementManager.updateAchievement("BEAT_1_MINIBOSS", 1);
	}
	
	protected void finalBossAchievement (){
		AchievementManager.updateAchievement("BEAT_1_FINALBOSS", 1);
	}
	
	void OnDestroy ()
	{
		MsgCenter.instance.removeListener(MsgCenter.HERO_RELIVE, heroRelive);
	}
	
	public override void standby ()
	{
		if(this.state != Character.STANDBY_STATE)
		{
			this.startCheckOpponent();
		}
		base.standby();
	}
	
	//add by xiaoyong 20130604 for daily quest
	public override void selecting ()
	{
	
	}
	
	protected virtual void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		Debug.LogError("should be override, roger");
	}
	///<-----
}
