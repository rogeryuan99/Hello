using UnityEngine;
using System.Collections;

public class Ch3_RedKing : EnemyRemote {
	
	public delegate void SkillAnimaEvent(Character character);
	public SkillAnimaEvent skillAnimaEventCallback;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate showSkill1EftCallback;
	public event ParmsDelegate showSkill1DamageEftCallback;
	public event ParmsDelegate showSkill30EftCallback;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 11;
	}
	
	public override void Start()
	{
		SkillEnemyManager.Instance.createSkillIcon("REDKING1");
		SkillEnemyManager.Instance.createSkillIcon("REDKING5A");
		SkillEnemyManager.Instance.createSkillIcon("REDKING15A");
		SkillEnemyManager.Instance.createSkillIcon("REDKING30A");
		
		base.Start();
		pieceAnima.addFrameScript("SkillA", 11, showSkill1Eft);
		pieceAnima.addFrameScript("SkillA", 33, showSkill1DamageEft);
		pieceAnima.addFrameScript("Skill30A",25,showSkill30Eft);
		
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA", 11);
		pieceAnima.removeFrameScript("SkillA", 33);
		pieceAnima.removeFrameScript("Skill30A",25);
	}
	
	public void showSkill1Eft(string s)
	{
		if(showSkill1EftCallback != null)
		{
			showSkill1EftCallback(this);
		}
	}
	
	public void showSkill1DamageEft(string s)
	{
		if(showSkill1DamageEftCallback != null)
		{
			showSkill1DamageEftCallback(this);
		}
	}
	
	public void showSkill30Eft(string s){
		if(showSkill30EftCallback != null){
			showSkill30EftCallback(this);
		}
	}
	
	public void skillAnimaEvent(string s)
	{
		if(skillAnimaEventCallback != null)
		{
			skillAnimaEventCallback(this);
		}
	}
	
	protected override void AnimaPlayEnd ( string animaName  )
	{
		switch(animaName)
		{			
			case "SkillA":
			case "Skill5A":
			case "Skill15A":
			case "Skill30A":
				SkillFinish();
				break;
		}
		base.AnimaPlayEnd(animaName);
	}
	
//	public override void moveToTarget ( GameObject obj  )
//	{
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
//		if (!IsInGroup &&
//			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
//			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
//			(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
//			))
//		{
//			startAtk();
//		}
//		else
//		{
//			base.moveToTarget(obj);
//		}
//	}
//	
//	
//	public override bool checkOpponent ()
//	{
//		if(isDead || StaticData.isBattleEnd){
//			this.cancelCheckOpponent();
//			return false;
//		}
//		if(state != MOVE_STATE  && state != CAST_STATE)
//		{
//			if(targetObj != null && 
//				!isDead	&&
//				!IsInGroup &&
//				((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
//				(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
//				(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
//				))
//			{
//				startAtk();
//			}else{
//				return base.checkOpponent();
//			}
//		}
//		return false;
//	}
//	
//	protected override void atkAnimaScript (string s){
//		MusicManager.playEffectMusic("SFX_StarLord_Basic_1a");
//		if(targetObj == null)
//		{
//			return;
//		}
//		Character target = targetObj.GetComponent<Character>();
//		if(target.getIsDead())
//		{
//			return;
//		}
//		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
//		Vector3 createPt;
//		if(model.transform.localScale.x > 0)
//		{
//			createPt = transform.position + new Vector3(80,40,-50);
//		}else{
//			createPt = transform.position + new Vector3(-80,40,-50);
//		}
//		shootBullet(createPt, vc3);
//	}
//	
//	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
//		float dis_y = endVc3.y - creatVc3.y;
//		float dis_x = endVc3.x - creatVc3.x;
//		float angle = Mathf.Atan2(dis_y, dis_x);
//		
//		if(data.type == HeroData.STARLORD)
//		{
//			Vector3 atkEftPos= transform.position + new Vector3(0, 50,-10);
//			GameObject atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
//			atkEftObj.transform.localScale = this.model.transform.localScale;
//		}
//		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
//		
//		float deg = (angle*360)/(2*Mathf.PI);
//		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
//								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
//	}
//	
//	protected virtual void removeBullet (GameObject bltObj)
//	{
//		GameObject HitEftObj = null;
//		if(HitEft)
//		{
//			HitEftObj = Instantiate(HitEft, bltObj.transform.position, transform.rotation) as GameObject;//+Vector3(0,100,-50),
//		}
//		
//		Destroy(bltObj);
//		if(targetObj != null)
//		{
//			Character target = targetObj.GetComponent<Character>();
//			if(HitEftObj != null)
//			{
//				HitEftObj.transform.parent = targetObj.transform;
//			}
//			int dmg;
//			dmg = target.defenseAtk(realAtk, this.gameObject);
//		}
//	}
}
