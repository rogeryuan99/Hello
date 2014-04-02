using UnityEngine;
using System.Collections;

public class Nebula : Hero {
	public GameObject bulletPrb;
	public GameObject atkEft;
	public GameObject HitEft;
	
	public GameObject atkGroupEft;
	
	public delegate void ParmsDelegate(Character character);
	public ParmsDelegate showSkill1BulletEftCallBack;
	public ParmsDelegate showSkill1FireEftCallBack;
	public ParmsDelegate showSkill30AHaloEftCallBack;
	public ParmsDelegate showSkill30AStarEftCallBack;
	public ParmsDelegate showSkill30ABulletEftCallBack;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 11;
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("SkillA",29,showSkill1BulletEft);
		pieceAnima.addFrameScript("SkillA",46,showSkill1FireEft);
		pieceAnima.addFrameScript("Skill30A_a",20,showSkill30AHaloEft);
		pieceAnima.addFrameScript("Skill30A_a",35,showSkill30AStarEft);
		pieceAnima.addFrameScript("Skill30A_b",15,showSkill30ABulletEft);
		pieceAnima.addFrameScript("Skill30A_b",23,atkAnimaScript);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA",29);
		pieceAnima.removeFrameScript("SkillA",46);
		pieceAnima.removeFrameScript("Skill30A_a",20);
		pieceAnima.removeFrameScript("Skill30A_a",35);
		pieceAnima.removeFrameScript("Skill30A_b",15);
		pieceAnima.removeFrameScript("Skill30A_b",23);
	}
	
	private void showSkill1BulletEft(string s){
		if(showSkill1BulletEftCallBack != null){
			showSkill1BulletEftCallBack(this);
		}
	}
	
	private void showSkill1FireEft(string s){
		if(showSkill1FireEftCallBack != null){
			showSkill1FireEftCallBack(this);
		}
	}
	
	private void showSkill30AHaloEft(string s){
		if(showSkill30AHaloEftCallBack != null){
			showSkill30AHaloEftCallBack(this);
		}
	}
	
	private void showSkill30AStarEft(string s){
		if(showSkill30AStarEftCallBack != null){
			showSkill30AStarEftCallBack(this);	
		}
	}
	
	private void showSkill30ABulletEft(string s){
		if(showSkill30ABulletEftCallBack != null){
			showSkill30ABulletEftCallBack(this);
		}
	}
	
	public override void moveToTarget ( GameObject obj  )
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		if( targetObj )
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		
		if (!IsInGroup &&
			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
			(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
			))
		{
			startAtk();
		}
		else
		{
			base.moveToTarget(obj);
		}
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
			}else{
				return base.checkOpponent();
			}
		}
		return false;
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_StarLord_Basic_1a");
		if(targetObj == null)
		{
			return;
		}
		Character target = targetObj.GetComponent<Character>();
		if(target.getIsDead())
		{
			return;
		}
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(model.transform.localScale.x > 0)
		{
			createPt = transform.position + new Vector3(80,40,-50);
		}else{
			createPt = transform.position + new Vector3(-80,40,-50);
		}
		shootBullet(createPt, vc3);
		
//		if(canShowNebulaPassive("NEBULA20A")){
//			showSkill20APassive();
//		}
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		if(data.type == HeroData.STARLORD)
		{
			Vector3 atkEftPos= transform.position + new Vector3(0, 50,-10);
			GameObject atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale = this.model.transform.localScale;
		}
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected virtual void removeBullet (GameObject bltObj)
	{
		GameObject HitEftObj = null;
		if(HitEft)
		{
			HitEftObj = Instantiate(HitEft, bltObj.transform.position, transform.rotation) as GameObject;//+Vector3(0,100,-50),
		}
		
		Destroy(bltObj);
		if(targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(HitEftObj != null)
			{
				HitEftObj.transform.parent = targetObj.transform;
			}
			int dmg;
			
			dmg = target.defenseAtk(realAtk, this.gameObject);
		}
	}
	
	public bool canShowNebulaPassive(string s){
		HeroData hd = this.data as HeroData;
		Hashtable passive = hd.getPSkillByID(s);
		if(passive != null){
			return true;	
		}
		return false;
	}
	
	public int showSkill10APassive(float atkSpd){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("NEBULA10A");
		int tempAspd = (int)skillDef.passiveEffectTable["universal"];
		int aspd = (int)(atkSpd*(1f + (float)tempAspd/100f));
		return aspd;	
	}
	
	public int showSkill20APassive(){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("NEBULA20A");
		int tempAtk = (int)skillDef.passiveEffectTable["universal"];
		return tempAtk;
	}
	
	public void showSkill25APassive(Character c){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("NEBULA25A");
		int tempHp = (int)skillDef.passiveEffectTable["universal"];
		float hp = c.realMaxHp * ((float)tempHp / 100.0f);
		int time = (int)(100f/(float)tempHp);
		c.addBuff("Skill_NEBULA25A", time, hp, BuffTypes.DE_HP, buffFinish);
		c.changeStateColor(new Color(1f, 1f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);
	}
	
	protected void buffFinish(Character character, Buff self){
		if(!character.isDead){
			character.model.renderer.material.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
