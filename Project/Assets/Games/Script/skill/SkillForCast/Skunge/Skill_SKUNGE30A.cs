using UnityEngine;
using System.Collections;

public class Skill_SKUNGE30A : SkillBase {
	private Character character;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		
		character = caller.GetComponent<Character>();
		character.castSkill("Skill30A_a");
		character.attackAnimaName = "Skill30A_b";
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("SKUNGE30A");
		int buffTime = (int)skillDef.skillDurationTime;
		character.addBuff("Skill_SKUNGE30A",buffTime,0,BuffTypes.ATK_PHY,buffFinish);
		
		yield return new WaitForSeconds(0f);
	}
	
	private void buffFinish(Character character, Buff self){
		if(!character.getIsDead()){
			character.attackAnimaName = "Attack";
		}
	}
	
}
