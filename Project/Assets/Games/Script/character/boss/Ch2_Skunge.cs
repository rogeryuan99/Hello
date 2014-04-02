using UnityEngine;
using System.Collections;

public class Ch2_Skunge : Enemy {
	private Object eftPrefab;
	
	public delegate void ParmsDelegate(Character character);
	public ParmsDelegate showSkill15AMusicHaloEftCallBack;
	
	public bool isTrigger5A;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 15;
	}
	
	public override void Start()
	{
		SkillEnemyManager.Instance.createSkillIcon("SKUNGE1");
		SkillEnemyManager.Instance.createSkillIcon("SKUNGE5A");
		SkillEnemyManager.Instance.createSkillIcon("SKUNGE15A");
		SkillEnemyManager.Instance.createSkillIcon("SKUNGE30A");
		
		base.Start();
		
		pieceAnima.addFrameScript("Skill15A",25,showSkill15AMusicHalo);
		pieceAnima.addFrameScript("Skill30A_b",15,atkAnimaScript);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill15A",25);
		pieceAnima.removeFrameScript("Skill30A_b",15);
	}
	
	private void showSkill15AMusicHalo(string s){
		if(showSkill15AMusicHaloEftCallBack != null){
			showSkill15AMusicHaloEftCallBack(this);
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		if(this.attackAnimaName == "Attack")
		{
			if(isTrigger5A){
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("SKUNGE5A");
				float def = ((Effect)skillDef.activeEffectTable["def_PHY"]).num;
				int time = skillDef.skillDurationTime;
				Character target = targetObj.GetComponent<Character>();
				int defValue =  getSkillDamageValue(target.realDef, def);
				target.addBuff("SKILL_SKUNGE5A",time,target.realDef.PHY*(def/100f),BuffTypes.DE_DEF_PHY);
				isTrigger5A = false;
			}
			base.atkAnimaScript(s);
		}else if(this.attackAnimaName == "Skill30A_b"){
			atkAnimaScriptDoubleDamage();
		}
	}
	
	private void atkAnimaScriptDoubleDamage(){
		int curFrame = pieceAnima.getCurrentFrame();
		if(this.targetObj == null){
			return;
		}else{
			if (null == eftPrefab){
				eftPrefab = Resources.Load("eft/Skunge/SkillEft_SKUNGE30A_WeaponHalo");
			}
			StartCoroutine(createWeaponHaloEft());
		}
	}
	
	private IEnumerator createWeaponHaloEft(){
		GameObject weaponHaloEft = Instantiate(eftPrefab) as GameObject;
		weaponHaloEft.transform.parent = this.transform;
		if(this.model.transform.localScale.x > 0){
			weaponHaloEft.transform.localPosition = new Vector3(180,390,0);
			weaponHaloEft.transform.localScale = new Vector3(5,5,1);	
		}else{
			weaponHaloEft.transform.localPosition = new Vector3(-180,390,0);
			weaponHaloEft.transform.localScale = new Vector3(-5,5,1);	
		}
		
		showDoubleDamage();
		
		yield return new WaitForSeconds(.6f);
		
		GameObject weaponHaloEft2 = Instantiate(eftPrefab) as GameObject;
		weaponHaloEft2.transform.parent = this.transform;
		if(this.model.transform.localScale.x > 0){
			weaponHaloEft2.transform.localPosition = new Vector3(180,390,0);
			weaponHaloEft2.transform.localScale = new Vector3(5,5,1);	
		}else{
			weaponHaloEft2.transform.localPosition = new Vector3(-180,390,0);
			weaponHaloEft2.transform.localScale = new Vector3(-5,5,1);	
		}
		
		showDoubleDamage();
	}
	
	private void showDoubleDamage(){
		if(targetObj == null) return;
		Character target = this.targetObj.GetComponent<Character>();
		if(target.getIsDead()) return;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("SKUNGE30A");
		int buffTime = (int)skillDef.buffDurationTime;
		float per = ((Effect)skillDef.buffEffectTable["hp"]).num;
		int hp = (int)(target.realMaxHp * (per / 100.0f));
		int damage = target.getSkillDamageValue(this.realAtk,0f);
		target.realDamage(damage);
		if(!target.isSkunge30Buff){
			target.addBuff("Skill_SKUNGE30A", buffTime, hp/buffTime, BuffTypes.DE_HP, buffFinish);
			target.changeStateColor(new Color(1f, 1f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);	
			target.isSkunge30Buff = true;
		}	
	}
	
	private void buffFinish(Character character, Buff self){
		if(!character.getIsDead()){
			character.model.renderer.material.color = new Color(1f, 1f, 1f, 1f);
			character.isSkunge30Buff = false;
		}
	}
	
	protected void atkAnimaScriptRemate (string s)
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
			
			case "Skill30A_a":
			case "Skill30A_b":
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
}
