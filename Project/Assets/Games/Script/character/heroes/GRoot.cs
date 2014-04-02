using UnityEngine;
using System.Collections;

public class GRoot : Hero
{
	
	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate SkillKeyFrameEvent;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 16;
	}
	
	public override void Start()
	{	
		base.Start();
		
		if(canShowGrootPassive("GROOT20A")){
			showGroot20APassive();
		}
		
		pieceAnima.addFrameScript("Celebrate",48, this.pauseAnima);
		
		pieceAnima.addFrameScript("GROOT15B",8, skillKeyFrameEvent);
		pieceAnima.addFrameScript("GROOT15B",80, skillKeyFrameEvent);
		
		pieceAnima.addFrameScript("GROOT30B",20, skillKeyFrameEvent);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Celebrate", 48);
		pieceAnima.removeFrameScript("GROOT15B",8);
		pieceAnima.removeFrameScript("GROOT15B",80);
		pieceAnima.removeFrameScript("GROOT30B",20);
	}
	
	public void pauseAnima(string s)
	{
		pieceAnima.pauseAnima();
	}
	
	public void skillKeyFrameEvent(string s)
	{
		if(SkillKeyFrameEvent != null)
		{
			SkillKeyFrameEvent(this);
		}
	}
	
	protected override void AnimaPlayEnd (string animaName)
	{
		switch(animaName)
		{
			case "GROOT1":
			case "GROOT5A":
			case "GROOT5B":
			case "GROOT15A":
			case "GROOT15B":
			case "GROOT30A":
			case "GROOT30B":
			case "GroupAttackB":
				SkillFinish();
				break;
		}
		base.AnimaPlayEnd (animaName);
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_Groot_Basic_1a");
		Vector3 eft;
		if(model.transform.localScale.x > 0)
		{
			eft = transform.position + new Vector3(100,80,-50);
		}else{
			eft = transform.position + new Vector3(-100,80,-50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		if(canShowGrootPassive("GROOT10B")){
			showGroot10BPassive();
		}
		
		base.atkAnimaScript("");
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		if(canShowGrootPassive("GROOT10A")){
			damage = showGroot10APassive(damage);	
		}
		return base.defenseAtk(damage, atkerObj);
	}
	
	public bool canShowGrootPassive(string s){
		HeroData hd = this.data as HeroData;
		Hashtable passive = hd.getPSkillByID(s);
		if(passive != null){
			return true;	
		}
		return false;
	}
	
	protected Vector6 showGroot10APassive(Vector6 damage){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT10A");
		int chanceValue = (int)skillDef.passiveEffectTable["universal"];
		int tempDef = (int)((Effect)skillDef.passiveEffectTable["def_PHY"]).num;
		if(StaticData.computeChance(chanceValue,100)){
			Vector6 tempDamage = damage.clone();
			tempDamage.Multip(1f-(float)tempDef/100f);
			return tempDamage;
		}
		return damage;
	}
	
	protected void showGroot10BPassive(){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT10B");
		int chanceValue = (int)skillDef.passiveEffectTable["universal"];
		int time = (int)skillDef.passiveEffectTable["universalTime"];
		if(StaticData.computeChance(chanceValue,100)){
			Character enemy = this.targetObj.GetComponent<Character>();
			float per = ((Effect)skillDef.passiveEffectTable["def_PHY"]).num;
			int hp = (int)(enemy.realMaxHp * (per / 100.0f));		
			enemy.addBuff("Skill_GROOT10B", time, hp/time, BuffTypes.DE_HP, buffFinish);
			enemy.changeStateColor(new Color(1f, 1f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);
		}
	}
	
	protected void buffFinish(Character character, Buff self){
		if(!character.isDead){
			character.model.renderer.material.color = new Color(1f, 1f, 1f, 1f);
		}
	}
	
	protected void showGroot20APassive(){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT20A");
		float tempDef = ((Effect)skillDef.passiveEffectTable["def_PHY"]).num;
		this.realDef.Multip(1f+tempDef/100f);
	}
	
	public void showGroot20BPassive(Vector6 damage,GameObject atkerObj){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT20B");
		float tempAtk = ((Effect)skillDef.passiveEffectTable["atk_PHY"]).num;	
		Vector6 tempDmage = damage.clone();
		tempDmage.Multip(tempAtk/100f);
		Character enemy = atkerObj.GetComponent<Character>();
		if(enemy != null){
			int dam = enemy.getDamageValue(tempDmage);
			enemy.realDamage(dam);	
		}
	}
	
	public void showGroot25Passive(){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT25");
		int per = (int)skillDef.passiveEffectTable["universal"];
		int hp = (int)(this.realMaxHp*((float)per/100f));
		this.addHp(hp);
	}
	
	public override void battleEnd ()
	{
		if(this.pieceAnima.GetCurrentAnimaName() == "GROOT30B")
		{
			isHealthLocked = false;
			GameObject deleteObj = GameObject.Find("SkillEft_GROOT30B_RotateEft(Clone)");
			if(deleteObj != null)
			{
				Destroy(deleteObj);
			}
			
			deleteObj = GameObject.Find("SkillEft_GROOT30B_Tree(Clone)");
			if(deleteObj != null)
			{
				Destroy(deleteObj);
			}
			transform.localScale = new Vector3(0.23f, 0.23f, 1);
			pieceAnima.restart();
		}
		base.battleEnd();
	}
}
