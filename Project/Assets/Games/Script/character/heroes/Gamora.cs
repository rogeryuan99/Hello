using UnityEngine;
using System.Collections;

public class Gamora : Hero
{
	// public delegate void noParmsDelegate();
	public event noParmsDelegate showSkillEftEvent;
	public delegate IEnumerator ShowSkillEftEventEx();
	public event ShowSkillEftEventEx showSkillEftEventEx;
	public noParmsDelegate damageCallback;
	public noParmsDelegate slowdownCallback;
	public noParmsDelegate skillFinishedCallback;
	public noParmsDelegate gamora15AAttack1Callback;
	public noParmsDelegate gamora15AAttack2Callback;
	public noParmsDelegate gamora15AAttack3Callback;
	
	public int autoRegenHpValue;
	
	public GameObject attackEft;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 16;
	}
	
	protected override void AnimaPlayEnd (string animaName)
	{
		switch(animaName)
		{
			case "GAMORA1":
			case "GAMORA15A":
			case "Skill15B_b":
			case "Skill30A_b":
				SkillFinish();
				break;
		}
		base.AnimaPlayEnd (animaName);
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("SkillA",  18, showSkillEft);
		pieceAnima.addFrameScript("SkillA", 28, skillPauseAnima);
		pieceAnima.addFrameScript("Skill5A", 2,  showSkillEftEx);
		pieceAnima.addFrameScript("Skill5B", 15, slowDownEffect);
		pieceAnima.addFrameScript("Skill5B", 26, damageEnemy);
		pieceAnima.addFrameScript("GroupAttackB", 1, this.pauseAnima);
		pieceAnima.addFrameScript("Skill15B_a", 8, this.pauseAnima);
		pieceAnima.addFrameScript("Skill15B_b", 3, slowDownEffect);
		pieceAnima.addFrameScript("Skill15B_b", 9, damageEnemy);
		pieceAnima.addFrameScript("Skill15B_b", 21, damageEnemy);
		pieceAnima.addFrameScript("Skill15B_b", 37, slowDownEffect);
		pieceAnima.addFrameScript("Skill15B_b", 44, damageEnemy);
		pieceAnima.addFrameScript("Skill15B_b", 62, finishedSkill);
		pieceAnima.addFrameScript("Skill30A_a", 24, pauseAnima);
		pieceAnima.addFrameScript("Skill30A_c", 30, finishedSkill);
		pieceAnima.addFrameScript("Skill30B"  , 23, slowDownEffect);
		pieceAnima.addFrameScript("Skill30B"  , 26, damageEnemy);
		pieceAnima.addFrameScript("Celebrate", 33, this.pauseAnima);
		
		pieceAnima.addFrameScript("GAMORA15A"  , 26, gamora15AAttack1);
		pieceAnima.addFrameScript("GAMORA15A"  , 30, gamora15AAttack2);
		pieceAnima.addFrameScript("GAMORA15A"  , 34, gamora15AAttack3);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA", 18);
		pieceAnima.removeFrameScript("SkillA", 28);
		pieceAnima.removeFrameScript("Skill5A", 2);
		pieceAnima.removeFrameScript("Skill5B", 15);
		pieceAnima.removeFrameScript("Skill5B", 26);
		pieceAnima.removeFrameScript("GroupAttackB", 1);
		pieceAnima.removeFrameScript("Skill15B_a", 8);
		pieceAnima.removeFrameScript("Skill15B_b", 3);
		pieceAnima.removeFrameScript("Skill15B_b", 11);
		pieceAnima.removeFrameScript("Skill15B_b", 23);
		pieceAnima.removeFrameScript("Skill15B_b", 35);
		pieceAnima.removeFrameScript("Skill15B_b", 46);
		pieceAnima.removeFrameScript("Skill30A_a", 24);
		pieceAnima.removeFrameScript("Skill30A_c", 30);
		pieceAnima.removeFrameScript("Skill30B",   23);
		pieceAnima.removeFrameScript("Skill30B",   26);
		pieceAnima.removeFrameScript("Celebrate", 33);
		pieceAnima.removeFrameScript("GAMORA15A", 26);
		pieceAnima.removeFrameScript("GAMORA15A", 30);
		pieceAnima.removeFrameScript("GAMORA15A", 34);
	}
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		HeroData heroD = this.data as HeroData;
		Hashtable passive1 =  heroD.getPSkillByID("GAMORA25");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA25");
			int uValue = (int)skillDef.passiveEffectTable["universal"];
			autoRegenHpValue = uValue;
			if(!IsInvoking("autoRegenHp"))InvokeRepeating("autoRegenHp", 0, 1.0f);
		}
	}
	
	public void pauseAnima(string s)
	{
		pieceAnima.pauseAnima();
	}
	
	
	
	public void showSkillEft(string s)
	{
		if(this.showSkillEftEvent != null)
		{
			this.showSkillEftEvent();
		}
	}
	public void showSkillEftEx(string s)
	{
		if(this.showSkillEftEventEx != null)
		{
			StartCoroutine(this.showSkillEftEventEx());
		}
	}
	public void damageEnemy(string s){
		if(damageCallback != null){
			damageCallback();
		}
	}
	public void slowDownEffect(string s){
		if (slowdownCallback != null){
			slowdownCallback();
		}
	}
	public void finishedSkill(string s){
		if (skillFinishedCallback != null){
			skillFinishedCallback();
		}
	}
	
	public void gamora15AAttack1(string s){
		if(gamora15AAttack1Callback != null){
			gamora15AAttack1Callback();
		}
	}
	public void gamora15AAttack2(string s){
		if(gamora15AAttack2Callback != null){
			gamora15AAttack2Callback();
		}
	}
	
	public void gamora15AAttack3(string s){
		if(gamora15AAttack3Callback != null){
			gamora15AAttack3Callback();
		}
	}
	
	public void skillPauseAnima(string s)
	{
		pieceAnima.pauseAnima();
		
		StartCoroutine(resumeSkill());
	}
	
	public IEnumerator resumeSkill()
	{
		yield return new WaitForSeconds(0.5f);
		
		pieceAnima.restart();
	}
	
	protected override void atkAnimaScript (string s)
	{
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
			return;
		} 
		
		MusicManager.playEffectMusic("SFX_Gamora_Basic_1a");
		Vector3 eft = (model.transform.localScale.x > 0)
						? transform.position + new Vector3(100,40,-50)
						: transform.position + new Vector3(-100,40,-50);
//		if(model.transform.localScale.x > 0)
//		{
//			eft = transform.position + new Vector3(100,40,-50);
//		}else{
//			eft = transform.position + new Vector3(-100,40,-50);
//		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		base.atkAnimaScript("");
		
		if(this.targetObj != null)
		{
			Character target = this.targetObj.GetComponent<Character>();
			HeroData heroD = this.data as HeroData;
			Hashtable passive1;
			if(target.getIsDead() && !getIsDead())
			{
				passive1 =  heroD.getPSkillByID("GAMORA10B");
				if(passive1 != null)
				{
					SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA10B");
					int uValue = (int)skillDef.passiveEffectTable["universal"];
					int uTime = (int)skillDef.passiveEffectTable["universalTime"];
					this.flash(0,0,1.0f);
					addBuff("GAMORA10B",uTime, uValue/100.0f, BuffTypes.ASPD,stopFlash);
				}
			}
			
			passive1 =  heroD.getPSkillByID("GAMORA20A");
			if(passive1 != null)
			{
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA20A");
				int uValue    = (int)skillDef.passiveEffectTable["universal"];
				int aoeRadius = (int)skillDef.passiveEffectTable["AOERadius"];
				StaticData.splashDamage(target, this, EnemyMgr.enemyHash.Values, this.realAtk.clone().Multip(uValue/100.0f), aoeRadius);
			}
			
		}
	}
	
	private void stopFlash(Character character, Buff self)
	{
		this.unFlash();	
	}
	
	private void autoRegenHp()
	{
		int regenValue = (int)((autoRegenHpValue/100.0f)*this.realMaxHp);
		this.addHp(regenValue);
	}
	
	public override void dead (string s)
	{
		CancelInvoke("autoRegenHp");
		base.dead (s);
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
	
		return base.defenseAtk(damage, atkerObj);
	}
}
