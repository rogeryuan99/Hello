using UnityEngine;
using System.Collections;

public class Skill_BUG5B : SkillBase {
	
	private Object hitEftPrefab;
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character bug   = caller.GetComponent<Character>();
		Character enemy = target.GetComponent<Character>();
		
		if(Vector3.Distance(caller.transform.position, target.transform.position) > bug.data.attackRange + 10f)
		{
			bug.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("BUG5B");// DRAX1
			yield break;
		}
		
		bug.toward(target.transform.position);
		bug.castSkill("Skill5B");
		
		yield return new WaitForSeconds(.5f);
		if (null == hitEftPrefab){
			hitEftPrefab = Resources.Load("eft/Bug/SkillEft_BUG5B_HitEffect");
		}
		GameObject hitEft = Instantiate(hitEftPrefab) as GameObject;
		bool isLeftSide = bug.model.transform.localScale.x > 0;
		hitEft.transform.localScale = new Vector3(isLeftSide? 1f:-1f, 1f, 1f);
		hitEft.transform.position = target.transform.position + new Vector3(isLeftSide? -20f: 20f, 50f, -1f);
		
		yield return new WaitForSeconds(.2f);
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BUG5B");
		enemy.addAbnormalState(new State(def.buffDurationTime, null), Character.ABNORMAL_NUM.LAYDOWN);
	}
}
