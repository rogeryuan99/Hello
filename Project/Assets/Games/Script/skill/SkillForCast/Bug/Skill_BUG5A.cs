using UnityEngine;
using System.Collections;

public class Skill_BUG5A : SkillBase {
	
	private const float HIT_COUNT = 15;
	private float damage = 0f;
	private int range = 0;
	private ArrayList parms;
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character bug = caller.GetComponent<Character>();
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BUG5A");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
		range = (int)def.activeEffectTable["AOERadius"];
		bug.castSkill("Skill5A");
		
		yield return new WaitForSeconds(.5f);
		
		float delay = 0f;
		for (int i=0; i<HIT_COUNT; i++){
			Invoke("DamageEnemy", delay);
			delay += 0.1f;
		}
	}
	
	private void DamageEnemy(){
		GameObject caller = parms[1] as GameObject;
		Character bug = caller.GetComponent<Character>();
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		for (int i=enemyList.Count-1; i>=0; i--){
			Character e = enemyList[i] as Character;
			if (Vector3.Distance(e.transform.position, bug.transform.position) < range)
				e.realDamage((int)(e.getSkillDamageValue(bug.realAtk, damage)/HIT_COUNT));
		}
	}
}
