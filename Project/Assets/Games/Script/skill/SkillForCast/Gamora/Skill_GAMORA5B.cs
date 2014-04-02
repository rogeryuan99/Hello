using UnityEngine;
using System.Collections;

public class Skill_GAMORA5B : SkillBase {

	public ArrayList objs = null;
	public GameObject Effectt_Prb;
	private int damage = 0;
	private int time = 0;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Gamora_Poisoned_Blades_1a");
		this.objs = objs;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		if(Vector3.Distance(caller.transform.position, target.transform.position) > gamora.data.attackRange + 10.0f)
		{
			gamora.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("GAMORA5B");// GAMORA5A
			yield break;
		}
		gamora.slowdownCallback = SlowDownEffect;
		gamora.damageCallback = DamageEnemy;
		gamora.castSkill("Skill5B");
		
		yield return new WaitForSeconds(0f);
	}
	
	public void DamageEnemy(){
		MusicManager.playEffectMusic("Skill_GAMORA5A");
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.6f, 0f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.24f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.24f));
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		Character c = target.GetComponent<Character>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA5B");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		damage = c.getSkillDamageValue(gamora.realAtk, ((Effect)tempNumber["atk_PHY"]).num);
		
		float mspdValue = ((Effect)(skillDef.buffEffectTable["mspd"])).num;
		float aspdValue = ((Effect)(skillDef.buffEffectTable["mspd"])).num;
		
		time = skillDef.buffDurationTime;
		
		c.realDamage(damage);
		
		c.addBuff("Skill_GAMORA5B_Mspd", time, -mspdValue / 100.0f, BuffTypes.MSPD);
		c.addBuff("Skill_GAMORA5B_Aspd", time, -aspdValue / 100.0f, BuffTypes.ASPD);
	}
	
	public void SlowDownEffect(){
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.15f));
	}
}
