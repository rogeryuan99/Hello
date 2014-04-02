using UnityEngine;
using System.Collections;

public class Ch2_Nebula : EnemyRemote {
	
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
		SkillEnemyManager.Instance.createSkillIcon("NEBULA1");
		SkillEnemyManager.Instance.createSkillIcon("NEBULA5A");
		SkillEnemyManager.Instance.createSkillIcon("NEBULA15A");
		SkillEnemyManager.Instance.createSkillIcon("NEBULA30A");
		
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
		showSkill20APassive();
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
			case "Skill30A_a":
				SkillFinish();
				break;
			
			//case "Skill30A_a":
			case "Skill30A_b":
				if(!TsTheater.InTutorial)
				{
					if(targetObj != null)
					{
						this.state = Character.STANDBY_STATE;
						//moveToTarget(targetObj);
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
