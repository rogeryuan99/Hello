using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_MANTIS1 : SkillBase {
	
	private ArrayList parms;
	private int healValue;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		parms = objs;
		
		Mantis mantis = caller.GetComponent<Mantis>();
		mantis.castSkill("SkillA");
		
		yield return new WaitForSeconds(0.8f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Psychic_Heal_1a");
		Heal();
	}
	
	private void Heal(){
		GameObject caller     = parms[1] as GameObject;
		Mantis     mantis     = caller.GetComponent<Mantis>();
		SkillDef   skillDef   = SkillLib.instance.getSkillDefBySkillID("MANTIS1");
		Hashtable  tempNumber = skillDef.activeEffectTable;
		float      tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		
		ArrayList heros = new ArrayList(HeroMgr.heroHash.Values);
		foreach(Character hero in heros){
			int v = hero.getSkillDamageValue(mantis.realAtk, tempAtkPer);
			hero.addHp(v);
			hero.changeStateColor(new Color(.5f, .5f, 1f, 1f));
		}
	}
}
