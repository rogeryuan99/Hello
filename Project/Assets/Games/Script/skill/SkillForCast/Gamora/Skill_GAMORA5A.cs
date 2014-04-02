using UnityEngine;
using System.Collections;

public class Skill_GAMORA5A : SkillBase {
	
	private GameObject skillEft_Prb = null;
	public ArrayList skillEft_Objs = null;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Gamora gamora = caller.GetComponent<Gamora>();
		MusicManager.playEffectMusic("SFX_Gamora_Cleave_1a");
		if(Vector3.Distance(caller.transform.position, target.transform.position) > gamora.data.attackRange + 10.0f)
		{
			gamora.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("GAMORA5A");// GAMORA5A
			yield break;
		}
		skillEft_Objs = objs;
		gamora.castSkill("Skill5A");
		gamora.showSkillEftEventEx += showSkill_Eft;
	}
	
	public IEnumerator showSkill_Eft()
	{
		GameObject caller = skillEft_Objs[1] as GameObject;
		GameObject target = skillEft_Objs[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		Character e = target.GetComponent<Character>();
		gamora.showSkillEftEventEx -= showSkill_Eft;
		
//		if(skillEft_Prb == null)
//		{
//			skillEft_Prb = Resources.Load("eft/Gamora/SkillEft_GAMORA5A") as GameObject;
//		} 
		
		MusicManager.playEffectMusic("Skill_GAMORA5A");
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.6f, .35f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.24f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.24f));
		
		// Instantiate(skillEft_Prb,new Vector3(target.transform.position.x,target.transform.position.y,-100),caller.transform.rotation);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA5A");
		
		Hashtable tempNumber = skillDef.activeEffectTable; // GAMORA5A
		
		int tempAtkPer = (int)(((Effect)tempNumber["atk_PHY"]).num);
		int tempAtk = e.getSkillDamageValue(gamora.realAtk, tempAtkPer);
		
		yield return new  WaitForSeconds(.64f);
		
		
		e.realDamage(tempAtk);
	}
	
}
