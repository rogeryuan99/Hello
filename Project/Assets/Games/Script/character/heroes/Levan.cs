using UnityEngine;
using System.Collections;

public class Levan: Hero {
	public GameObject bulletPrb;
	public GameObject bomb;
	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate SkillKeyFrameEvent;
	public noParmsDelegate showSkill15EftCallback;
	
	//for passive 20
	private Hashtable alreadyAddedAspd = new Hashtable();
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	
	public override void Start()
	{
		base.Start();
		
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
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		HeroData heroD = this.data as HeroData;
		Hashtable passive1 =  heroD.getPSkillByID("LEVAN20");
		if(passive1 != null)
		{
			// alreadyAddedAspd = new Hashtable();
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
		}
		passive1 =  heroD.getPSkillByID("LEVAN25");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN25");
			int atkPer = (int)skillDef.passiveEffectTable["sk_damage"];
			int atkSpd = (int)skillDef.passiveEffectTable["universal"];
			ArrayList heroesClone = new ArrayList(HeroMgr.heroHash.Values);
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
	}
	
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
	
	protected override void atkAnimaScript (string s)
	{
		MusicManager.playEffectMusic("SFX_Drax_Basic_1a");
		
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
		}
		else	
		{
			if(this.attackAnimaName == "Attack"){
				base.atkAnimaScript("");
			}else if(this.attackAnimaName == "Skill15A_b"){
				atkAnimaScriptRemote(s);
			}
			
			Character target = this.targetObj.GetComponent<Character>();
			HeroData heroD = this.data as HeroData;
			Hashtable passive1 =  heroD.getPSkillByID("LEVAN10");
			if(passive1 != null)
			{
				if(!target.getIsDead())
				{
					SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN10");
					int dotDamage = (int)skillDef.passiveEffectTable["universal"];
					int time      = (int)skillDef.passiveEffectTable["universalTime"];
					int dotTotalHp = (int)(target.realMaxHp*(dotDamage/100.0f));
					target.addBuff("Skill_LEVAN10", time, dotTotalHp/time, BuffTypes.DE_HP);
				}
			}
		}
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
	
	protected void atkAnimaScriptRemote (string s){
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		
		if(targetObj == null){
			return;
		}
		
		Character character = targetObj.GetComponent<Character>();
		
		if(character.getIsDead()){
			return;
		}
		
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = transform.position + new Vector3(80,40,-50);
		}else{
//			print("left");
			createPt = transform.position + new Vector3(-80,40,-50);
		}
		shootBullet(createPt, vc3);
		
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
			
//			showHarmEft(data.type);
		
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
			case "Attack":
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
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  )
	{
		
		return base.defenseAtk(damage, atkerObj);
	}
	
	public override void battleEnd ()
	{
		this.attackAnimaName = "Attack";
		base.battleEnd ();
	}
	
	public override void dead(string s)
	{
		foreach(ArrayList ary in alreadyAddedAspd.Values)
		{
			Character character = (Hero)ary[0];
			character.realAspd -= (float)ary[1];
		}
		alreadyAddedAspd.Clear();
		this.attackAnimaName = "Attack";
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
//		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
//		{
//			return;
//		}
//		
//		if( targetObj )
//		{
//			Character targetDoc =  targetObj.GetComponent<Character>(); 
//			targetDoc.dropAtkPosition(this);
//		}
//		targetObj = obj;
//		
//		// if (!IsInGroup){
//		if (!IsInGroup &&
//			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
//			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
//			(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
//			))
//		{
//			
//			startAtk();
//			
//		}
//		else
//		{
//			base.moveToTarget(obj);
//		}
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
		
		startAtk();
	}
	
	public override bool checkOpponent ()
	{
		if(isDead || StaticData.isBattleEnd){
			this.cancelCheckOpponent();
			return false;
		}
		if(state != MOVE_STATE  && state != CAST_STATE)
		{
			if(targetObj != null && 
				!isDead	&&
				!IsInGroup &&
				((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
				(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
				(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
				))
			{
				startAtk();
				Debug.LogError("Attack!!2");
			}else{
				return base.checkOpponent();
			}
		}
		return false;
	}
}
