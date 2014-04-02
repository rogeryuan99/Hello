using UnityEngine;
using System.Collections;

public class Skill_BUG15B : SkillBase
{
	public GameObject skillEftPrb;
	public GameObject haloEftPrb;
	
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character character = caller.GetComponent<Character>();
		
		yield return new WaitForSeconds(0.5f);
		
		character.castSkill("Skill15B");
		
		Bug bug = character as Bug;
		
		bug.showSkill15BEftCallback += showSkill15BEft;
			
		bug.attackAnimaEvent += attack;
	}
	
	public void showSkill15BEft(Character character)
	{
		Bug bug = character as Bug;
		
		bug.showSkill15BEftCallback -= showSkill15BEft;
		
		float skillEftX = 0;
		float haloEftX = 0;
		if(character.model.transform.localScale.x > 0)
		{
			skillEftX = 244;
			haloEftX = 273.1709f;
		}
		else
		{
			skillEftX = -297;
			haloEftX = -277.1709f;
		}
		
		GameObject skillEft = null;
		StaticData.createObjFromPrb(ref skillEftPrb, "eft/Bug/SkillEft_BUG15B_Eft", ref skillEft, character.transform, new Vector3(skillEftX, 84, 0));
		
		
		GameObject haloEft = null;
		StaticData.createObjFromPrb(ref haloEftPrb, "eft/Bug/SkillEft_BUG15B_HaloEft", ref haloEft, character.transform, new Vector3(haloEftX, 0, 0));
//		Debug.Break();
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["BUG15B"] as SkillDef;
		
		float per = ((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		
		character.addBuff("Skill_BUG15B", skillDef.buffDurationTime, per, BuffTypes.ATK_PHY, buffFinish);	
	}
	
	public void attack(Character character, Character target)
	{
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BUG15B");
		int skillDurationTime = skillDef.skillDurationTime;
		
		if(!target.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN))
		{
			target.addAbnormalState(skillDurationTime, null, Character.ABNORMAL_NUM.LAYDOWN);
		}
			
		target.defenseAtk(character.realAtk, character.gameObject);
	}
	
	public void buffFinish(Character character, Buff self)
	{
		Bug bug = character as Bug;
			
		bug.attackAnimaEvent -= attack;
	}
}
