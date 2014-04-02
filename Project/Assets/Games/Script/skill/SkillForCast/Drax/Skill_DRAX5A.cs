using UnityEngine;
using System.Collections;

public class Skill_DRAX5A : SkillBase
{

	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		MusicManager.playEffectMusic("SFX_Drax_Large_Angry_Man_1a");
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("Skill5A");// Hero.castSkill
		
		yield return new WaitForSeconds(0.5f);
		
		GameObject skillEftObj = heroDoc.model.transform.FindChild("effect_2").GetChild(0).gameObject;
		
		
		
//		skillEftObj.GetComponent<PackedSprite>().PlayAnim(0);
		
		HeroData tempHeroData = (heroDoc.data as HeroData);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX5A");
		
		Hashtable tempNumber = skillDef.buffEffectTable;
		
		int time = skillDef.buffDurationTime;
		
		float atkPer = ((Effect)tempNumber["atk_PHY"]).num;
		float defPer = ((Effect)tempNumber["def_PHY"]).num;
		
		
		heroDoc.addBuff("Skill_DRAX5A_1",time,atkPer,BuffTypes.ATK_PHY);
		heroDoc.addBuff("Skill_DRAX5A_2",time,-defPer,BuffTypes.DEF_PHY);
		
//		heroDoc.unFlash();
//		heroDoc.flash(0.8f,0.2f,0);
	//	heroDoc.flashEffectName = "SpaceCadetsMarch";
	}
}
