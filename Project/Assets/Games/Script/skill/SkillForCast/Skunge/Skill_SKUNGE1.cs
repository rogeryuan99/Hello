using UnityEngine;
using System.Collections;

public class Skill_SKUNGE1 : SkillBase {

	private ArrayList parms;
	private Character skunge;
	private Character enemy;
	private Object eftPrefab;
	private float damage;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		skunge = caller.GetComponent<Character>();
		enemy  = target.GetComponent<Character>();
		skunge.toward(target.transform.position);
		skunge.castSkill("SkillA");
		yield return new WaitForSeconds(0.98f);

		showEft();
		
		yield return new WaitForSeconds(0.2f);
//				Time.timeScale = 0.01f;
//		int i = 0;
//		while(1>0){
//			yield return new WaitForSeconds(0.01f);
//			Debug.LogError(i++);
//		}
		DamageEnemy();
	}
	
	private void showEft(){
		if (null == eftPrefab){
			eftPrefab = Resources.Load("eft/Skunge/SkillEft_SKUNGE1_HitEffect");
		}
		GameObject hitEft = Instantiate(eftPrefab) as GameObject;
		
		bool isLeftSide = skunge.model.transform.localScale.x > 0;
		hitEft.transform.localScale = new Vector3(isLeftSide?1:-1,1,1); 
		hitEft.transform.position = skunge.transform.position + new Vector3(isLeftSide?95:-95,120,-10);
		
		
	}
	
	private void DamageEnemy(){
		if (false == enemy.getIsDead()){
			SkillDef def = SkillLib.instance.getSkillDefBySkillID("SKUNGE1");
			damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
			enemy.realDamage(enemy.getSkillDamageValue(skunge.realAtk, damage));
			StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.6f, 0f));
		}
	}
}
