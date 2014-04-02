using UnityEngine;
using System.Collections;

public class Skill_SKUNGE5A : SkillBase {
	private ArrayList parms;
	private Character skunge;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		LoadResources();
		
		skunge.castSkill("Skill5A");
		
		yield return new WaitForSeconds(0f);
		if (skunge is Enemy)
			(skunge as Ch2_Skunge).isTrigger5A = true;
		else
			(skunge as Skunge).isTrigger5A = true;
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		skunge = caller.GetComponent<Character>();
	}
}
