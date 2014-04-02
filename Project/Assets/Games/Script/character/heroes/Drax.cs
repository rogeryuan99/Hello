using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drax : Hero {
	
	public delegate void AttackAnimaEvent(Drax drax, Character target);
	public event AttackAnimaEvent attackAnimaEvent;
	// public delegate void noParmsDelegate();
	public noParmsDelegate damageCallback;
	public noParmsDelegate skillFinishedCallback;
	public noParmsDelegate movingUpdateCallback;
	public noParmsDelegate showSkill30EftCallback;
	public noParmsDelegate showSkill30BodyEftCallback;
	public noParmsDelegate showSkill30BMusicCallback;
	
	private List<GameObject> attackFlashList = new List<GameObject>();
	private int autoRegenHpValue;
			
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	
	public override void Start()
	{
		base.Start();
		//pieceAnima.addFrameScript("Attack", 10, showAttackEft);
		// pieceAnima.addFrameScript("SkillA", 30, this.pauseAnima);
		pieceAnima.addFrameScript("SkillA_a", 22, this.switchMoving);
		pieceAnima.addFrameScript("SkillA_a", 26, this.damageEnemy);
		pieceAnima.addFrameScript("SkillA_b", 10, this.switchMoving);
		pieceAnima.addFrameScript("GroupAttackB", 1, this.pauseAnima);
		
		pieceAnima.addFrameScript("Skill30", 3, showSkill30Eft);
		pieceAnima.addFrameScript("Skill30", 14, showSkill30BodyEft);
		
		pieceAnima.addFrameScript("Skill30A", 8, skill30AAnimaScript);
		
		pieceAnima.addFrameScript("Skill30B",9,showSkill30BMusic);
		pieceAnima.addFrameScript("Skill30B", 16, skill30BAnimaScript);
		pieceAnima.addFrameScript("Skill30B", 26, skill30BAnimaScript);
		
//		pieceAnima.addFrameScript("Skill5A", 23, this.pauseAnima);
	}
	
	public void OnDestroy()
	{
		//pieceAnima.removeFrameScript("Attack", 10);
		pieceAnima.removeFrameScript("SkillA_a", 22);
		pieceAnima.removeFrameScript("SkillA_a", 26);
		pieceAnima.removeFrameScript("SkillA_b", 10);
		pieceAnima.removeFrameScript("GroupAttackB", 1);
		pieceAnima.removeFrameScript("Skill30A", 14);
		
		pieceAnima.removeFrameScript("Skill30", 3);
		pieceAnima.removeFrameScript("Skill30", 14);
		
		pieceAnima.removeFrameScript("Skill30B", 9);
		pieceAnima.removeFrameScript("Skill30B", 16);
		pieceAnima.removeFrameScript("Skill30B", 26);
	}
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		HeroData heroD = this.data as HeroData;
		Hashtable passive1 =  heroD.getPSkillByID("DRAX25");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX25");
			int uValue = (int)skillDef.passiveEffectTable["universal"];
			autoRegenHpValue = uValue;
			if(!IsInvoking("autoRegenHp"))InvokeRepeating("autoRegenHp", 0, 1.0f);
		}
	}
	
	public void pauseAnima(string s)
	{
		pieceAnima.pauseAnima();
	}
	
	public void showSkill30BodyEft(string s)
	{
		if(showSkill30BodyEftCallback != null)
		{
			showSkill30BodyEftCallback();
		}
	}
	
	public void showSkill30BMusic(string s)
	{
		if(showSkill30BMusicCallback != null)
		{
			showSkill30BMusicCallback();	
		}
	}
	
	public void showSkill30Eft(string s)
	{
		if(showSkill30EftCallback != null)
		{
			showSkill30EftCallback();
		}
	}
	
	public void damageEnemy(string s){
		if(damageCallback != null){
			damageCallback();
		}
	}
	
	public void finishedSkill(string s){
		if(skillFinishedCallback != null){
			skillFinishedCallback();
		}
	}
	
	public void switchMoving(string s){
		if(movingUpdateCallback != null){
			movingUpdateCallback();
		}
	}
	
	
	protected override void AnimaPlayEnd (string animaName)
	{
		switch(animaName)
		{
			case "SkillA_a":
				damageEnemy("");	
				break;
			case "Skill30":
				SkillFinish();
				break;
		}
		base.AnimaPlayEnd (animaName);
	}
	
	protected void skill30AAnimaScript (string s)
	{
		if(attackAnimaEvent != null && targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(!target.isDead)
			{
				attackAnimaEvent(this, target);
				passiveSkill20A();
			}
		}
	}
	
	protected void skill30BAnimaScript (string s)
	{
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
		}
		else	
		{
			
			base.atkAnimaScript("");
			passiveSkill20A();
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
		}
		else	
		{
			MusicManager.playEffectMusic("SFX_Drax_Basic_1a");
			if(attackAnimaEvent != null && targetObj != null)
			{
				Character target = targetObj.GetComponent<Character>();
				if(!target.isDead)
				{
					attackAnimaEvent(this, target);
					return;
				}
			}
			bool isTriggerPassiveSkill10A = passiveSkill10A();
			if(isTriggerPassiveSkill10A){
				HeroData data = this.data as HeroData;
				Hashtable psTable = data.passiveHash["DRAX10A"] as Hashtable;
				int atk_PHY = 100;
				Character target = this.targetObj.GetComponent<Character>();
				int damage = target.getSkillDamageValue(this.heroAI.hero.realAtk, atk_PHY);
				target.realDamage(damage);	
			}else{
				base.atkAnimaScript("");
			}
			passiveSkill10B();
			passiveSkill20A();
			passiveSkill20B();
		}
	}
	
	private bool passiveSkill10A(){
		bool isTriggerPassiveSkill10A;
		HeroData data = this.data as HeroData;
		Hashtable psTable = data.passiveHash["DRAX10A"] as Hashtable;
		if(null != psTable && psTable.ContainsKey("chance") && StaticData.computeChance((int)psTable["chance"],100)){
			isTriggerPassiveSkill10A = true; 
			MusicManager.playEffectMusic("SFX_character_heavy_damage_1a");
			attackFlashScale(new Vector3(2f,2.5f,1));
		}else{
			isTriggerPassiveSkill10A = false; 
			attackFlashScale(new Vector3(1,1,1));
		}
		return isTriggerPassiveSkill10A;
	}
	
	
	
	private void attackFlashScale(Vector3 scale){
		if(attackFlashList.Count == 0){
			PackedSprite[] weaponPackedSprites = this.model.GetComponentsInChildren<PackedSprite>();
			for(int i = 0; i < weaponPackedSprites.Length; i++)
			{
				GameObject weaponObj = weaponPackedSprites[i].gameObject;
				if(weaponObj.name.Contains("MEDIUM_Punch_FX_02"))
				{
					attackFlashList.Add(weaponObj);
				}
			}
		}
		foreach(GameObject attackFlash in attackFlashList){
			attackFlash.transform.localScale = scale;
		}
	}
	
	
	private void passiveSkill10B(){
		HeroData data = this.data as HeroData;
		Hashtable psTable = data.passiveHash["DRAX10B"] as Hashtable;
		if(null != psTable && psTable.ContainsKey("debuff_time") && psTable.ContainsKey("debuff_mspd") && psTable.ContainsKey("debuff_hp")){
			int time = (int)psTable["debuff_time"];
			int mspdValue = (int)psTable["debuff_mspd"];
			int hp = (int)psTable["debuff_hp"];
			Character target = this.targetObj.GetComponent<Character>();
			
			target.addBuff("Skill_DRAX10B_Mspd", time, -mspdValue / 100.0f, BuffTypes.MSPD);
			target.addBuff("Skill_DRAX10B_HP", time, hp / 100.0f, BuffTypes.DE_HP_HEALING);
		}
	}
	
	private void passiveSkill20A(){
		HeroData heroD = this.data as HeroData;
		Hashtable passiveSkill20A =  heroD.getPSkillByID("DRAX20A");
		if(passiveSkill20A != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX20A");
			int chance = (int)skillDef.passiveEffectTable["chance"];
			int time = (int)skillDef.passiveEffectTable["debuff_time"];
			if(StaticData.computeChance(chance,100) && isContainBuff(BuffTypes.ATK_PHY)){
				Character target = this.targetObj.GetComponent<Character>();
				State s = new State(time, null);
				target.addAbnormalState(s, Character.ABNORMAL_NUM.LAYDOWN);
			}
		}
	}
	
	private void passiveSkill20B(){
		HeroData heroD = this.data as HeroData;
		Hashtable passiveSkill20A =  heroD.getPSkillByID("DRAX20B");
		if(passiveSkill20A != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX20B");
			int chance = (int)skillDef.passiveEffectTable["chance"];
			if(StaticData.computeChance(chance,100)){
				Character target = this.targetObj.GetComponent<Character>();
				float distanceX = (this.model.transform.localScale.x > 0)? 150 : -150;
				iTween.MoveTo(target.gameObject, new Hashtable(){
					{"x", target.transform.position.x + distanceX},
					{"y", target.transform.position.y},
					{"time",  0.2f},
					{"easeType", "liner"}
				});
			}
		}
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
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
		this.attackAnimaName = "Attack";
		CancelInvoke("autoRegenHp");
		base.dead();	
	}
	
	private void autoRegenHp()
	{
		int regenValue = (int)((autoRegenHpValue/100.0f)*this.realMaxHp);
		this.addHp(regenValue);
	}
}
