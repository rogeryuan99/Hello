using UnityEngine;
using System;
using System.Collections;

public class Skill_BUG30A : SkillBase
{
	
	public GameObject weaponPrb;
	public GameObject weapon;
	
	public GameObject skillEftPrb;
	public GameObject skillEft;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character character= caller.GetComponent<Character>();
	
		
		yield return new WaitForSeconds(.5f);
		
		character.castSkill("Skill30A");
		
		Bug bug = character as Bug;
		
		bug.showSkill30AEftCallback += showSkill30AEft;
	}
	
	public void showSkill30AEft(Character character)
	{
		Bug bug = character as Bug;
		
		bug.showSkill30AEftCallback -= showSkill30AEft;
		
		float weaponX = 0;
		float explodeX = 0;
		
		if(character.model.transform.localScale.x > 0)
		{
			weaponX = 119;
			explodeX = 127;
		}
		else
		{
			weaponX = -119;
			explodeX = -109;
		}
		
		StaticData.createObjFromPrb(ref weaponPrb, "eft/Bug/SkillEft_BUG30A_Weapon", ref weapon, character.transform, new Vector3(weaponX, 180, 0), new Vector3(3, 3, 1));
		StaticData.createObjFromPrb(ref skillEftPrb, "eft/Bug/SkillEft_BUG30A_Explode", ref skillEft, character.transform, new Vector3(explodeX, 156.8239f, 0));
		
		Hashtable characterTable = null;
		
		if(character is Enemy)
		{
			characterTable = HeroMgr.heroHash;
		}
		else if(character is Hero)
		{
			characterTable = EnemyMgr.enemyHash;
		}
		
		ArrayList targetCharacterList = new ArrayList(characterTable.Values);
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["BUG30A"] as SkillDef;
		
		foreach(Character targetCharacter in targetCharacterList)
		{
			if(targetCharacter.getIsDead())
			{
				continue;
			}
			targetCharacter.addAbnormalState(skillDef.skillDurationTime, null, Character.ABNORMAL_NUM.STUN);
		}
		
	}
}
