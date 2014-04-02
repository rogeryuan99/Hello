using UnityEngine;
using System.Collections;

public class Skill_MANTIS15A : SkillBase
{
	
	public GameObject skillEft_MANTIS15APrb;
	
	public GameObject skillEft_MANTIS15A;
		
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Mantis mantis = caller.GetComponent<Mantis>();
		
		mantis.castSkill("Skill15A");
		mantis.skill15AKeyFrameEventCallback += showSkillEft;
		
		yield return new WaitForSeconds(0.3f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Psychic_Distraction_1a");
	}
	
	public void showSkillEft(Character character)
	{
		Mantis mantis = character as Mantis;
		
		mantis.skill15AKeyFrameEventCallback -= showSkillEft;
		//mantis.hurtBeforeState = Character.HurtBeforeState.NOTHURT;
		mantis.lossTargetBeforeState = Character.LossTargetBeforeState.LOSSTARGET;
		foreach(Enemy e in EnemyMgr.enemyHash.Values){
			if(e.targetObj != null && e.targetObj == character.gameObject){
				character.dropAtkPosition(e);
				e.targetObj = null;	
				e.checkOpponent();
			}
		}	
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["MANTIS15A"] as SkillDef;
		
		int buffime = skillDef.buffDurationTime;
		
		if(skillEft_MANTIS15APrb == null)
		{
			skillEft_MANTIS15APrb = Resources.Load("eft/Mantis/SkillEft_MANTIS15A") as GameObject;
		}
		
		Destroy(skillEft_MANTIS15A);
		
		skillEft_MANTIS15A = Instantiate(skillEft_MANTIS15APrb) as GameObject;
		
		skillEft_MANTIS15A.transform.parent = mantis.transform;
		skillEft_MANTIS15A.transform.localPosition = new Vector3(0, 630, 0);
		skillEft_MANTIS15A.renderer.material.color = new Color32(128, 128, 128, 0);
//		Debug.Break();
		
		mantis.addBuff("Skill_MANTIS15A", buffime, 0, BuffTypes.DEF_PHY, buffFinish);	
		StartCoroutine(changeSkillEft_MANTIS15A());
		StartCoroutine(moveSkillEft_MANTIS15A1());
	}
	
	public IEnumerator changeSkillEft_MANTIS15A()
	{
		int count = 5;
		int current = 0;
		
		while(current != count)
		{
			skillEft_MANTIS15A.renderer.material.color += new Color32(0, 0, 0, 51);
			yield return new WaitForSeconds(0.1f);
			current++;
		}
	}
	
	public IEnumerator moveSkillEft_MANTIS15A1()
	{
		int count = 4;
		int current = 0;
		
		while(current != count)
		{
			skillEft_MANTIS15A.transform.localPosition -= new Vector3(0, 40, 0);
			yield return new WaitForSeconds(0.1f);
			current++;
		}
		StartCoroutine(moveSkillEft_MANTIS15A2());
	}
	
	public IEnumerator moveSkillEft_MANTIS15A2()
	{
		int count = 4;
		int current = 0;
		
		while(current != count)
		{
			skillEft_MANTIS15A.transform.localPosition -= new Vector3(0, 40, 0);
			yield return new WaitForSeconds(0.01f);
			current++;
		}
	}
			
	
	public void buffFinish(Character character, Buff self)
	{
		StopCoroutine("changeSkillEft_MANTIS15A");
		StopCoroutine("moveSkillEft_MANTIS15A1");
		StopCoroutine("moveSkillEft_MANTIS15A2");
		Destroy(skillEft_MANTIS15A);
//		character.hurtBeforeState = Character.HurtBeforeState.HURT;
		character.lossTargetBeforeState = Character.LossTargetBeforeState.NONE;
	}
}
