using UnityEngine;
using System.Collections;

public class Skill_ROCKET_COMBO : SkillBase
{
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		yield return new WaitForSeconds(0.5f);
		
		MusicManager.playEffectMusic("SFX_Combo3_Melee_Range_1b");
		
		Character heroDoc = caller.GetComponent<Character>();
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("SuperAttack");
		heroDoc.setDepth();
	}
}
