using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CAIERA30B : SkillBase
{
	public GameObject firstFirePrb;
	public GameObject firstFire;
	
	public GameObject haloPrb;
	
	public GameObject secondFireFrontPrb;	
	public GameObject secondFireFront;
	public GameObject secondFireBehindPrb;
	public GameObject secondFireBehind;
	
	public GameObject thirdFireFrontPrb;
	public GameObject thirdFireFront;
	
	public GameObject thirdFireBehindPrb;
	public GameObject thirdFireBehind;
	
	public Character character;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		yield return new WaitForSeconds(0.5f);
		
		character = caller.GetComponent<Character>();
		
		
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.showSkill30BHaloEftCallback += showSkill30BShowHaloEft;
			
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.showSkill30BHaloEftCallback += showSkill30BShowHaloEft;
		}
		
		character.castSkill("Skill30B");
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["CAIERA30B"] as SkillDef;
		
		int buffime = skillDef.buffDurationTime;
		
		character.hurtBeforeState = Character.HurtBeforeState.NOTHURT;
		
		character.realAtk.ENG = character.realAtk.PHY;
		character.realAtk.PHY = 0;
		
		character.addBuff("Skill_CAIERA30B", buffime, 0, BuffTypes.DEF_PHY, buffFinish);
				
		StaticData.createObjFromPrb(ref firstFirePrb, "eft/Caiera/Skill_CAIERA30B_FirstFire", ref firstFire, character.transform, new Vector3(0, 0, 1));
		
		firstFire.GetComponent<PackedSprite>().SetAnimCompleteDelegate(showSecondFire);
		
		MusicManager.playEffectMusic("SFX_Caiera_Shadow_Warrior_1a");
	}
	
	public void showSkill30BShowHaloEft(Character c)
	{
		if(c is Caiera)
		{
			Caiera caiera = c as Caiera;
			caiera.showSkill30BHaloEftCallback -= showSkill30BShowHaloEft;
			
		}
		else if(c is Ch3_Caiera)
		{
			Ch3_Caiera caiera = c as Ch3_Caiera;
			caiera.showSkill30BHaloEftCallback -= showSkill30BShowHaloEft;
		}
		
		float x = 0; 
		if(c.model.transform.localScale.x > 0)
		{
			x = 94;
		}
		else
		{
			x = -114;
		}
		
		
		GameObject halo = null;
		
		StaticData.createObjFromPrb(ref haloPrb, "eft/Caiera/Skill_CAIERA30B_Halo", ref halo, c.transform, new Vector3(x, 916, 0));

	}
	
	public void showSecondFire(SpriteBase sprite)
	{
		Destroy(firstFire);
		
		Destroy(secondFireFront);
		Destroy(secondFireBehind);
		
		StaticData.createObjFromPrb(ref secondFireFrontPrb, "eft/Caiera/Skill_CAIERA30B_SecondFireFront", ref secondFireFront, character.transform, new Vector3(0, -26, 0));
		
		StaticData.createObjFromPrb(ref secondFireBehindPrb, "eft/Caiera/Skill_CAIERA30B_SecondFireBehind", ref secondFireBehind, character.transform, new Vector3(0, 101, 1));
		
		
		secondFireBehind.GetComponent<PackedSprite>().SetAnimCompleteDelegate(showThirdFire);
	}
	
	public void showThirdFire(SpriteBase sprite)
	{
		Destroy(secondFireFront);
		Destroy(secondFireBehind);
		
		Destroy(thirdFireFront);
		Destroy(thirdFireBehind);
		
		StaticData.createObjFromPrb(ref thirdFireFrontPrb, "eft/Caiera/Skill_CAIERA30B_ThirdFireFront", ref thirdFireFront, character.transform, new Vector3(0, 259, 0));
		
		StaticData.createObjFromPrb(ref thirdFireBehindPrb, "eft/Caiera/Skill_CAIERA30B_ThirdFireBehind", ref thirdFireBehind, character.transform, new Vector3(0, 270, 1));		
	
	}
	
	public void buffFinish(Character character, Buff self)
	{
		Destroy(firstFire);
		Destroy(secondFireFront);
		Destroy(secondFireBehind);
		Destroy(thirdFireFront);
		Destroy(thirdFireBehind);
		
		character.hurtBeforeState = Character.HurtBeforeState.HURT;
		
		character.realAtk.PHY = character.realAtk.ENG;
		character.realAtk.ENG = 0;
	}
}
