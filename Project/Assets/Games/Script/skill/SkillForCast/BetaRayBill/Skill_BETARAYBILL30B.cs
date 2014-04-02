using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL30B : SkillBase {
	
	private ArrayList parms;
	private Character bill;
	private int       range;
	private float     damage;
	private Object    lightingPrefab;
	private const int HIT_COUNT = 10;
	
	public override IEnumerator Cast (ArrayList objs){
		
		parms = objs;
		LoadResources();
		
		bill.castSkill("Skill30B");
		yield return new WaitForSeconds(.5f);
		CreateLighting();
		for (int i=0; i<HIT_COUNT; i++){
			DamageEnemy();
			StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,40,0), 1f/HIT_COUNT, 0f));
			yield return new WaitForSeconds(.1f);
		}
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		bill = caller.GetComponent<Character>();
		
		if (null == lightingPrefab){
			lightingPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL30B_Lighting");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL30B");
		range = (int)def.activeEffectTable["AOERadius"];
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
	}
	
	private void CreateLighting(){
		GameObject lighting = Instantiate(lightingPrefab) as GameObject;
	 	lighting.transform.position = bill.transform.position + new Vector3(0f, 220f, -1f);
	}
	
	private void DamageEnemy(){
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		foreach(Character enemy in enemyList){
			if (Vector3.Distance(enemy.transform.position, bill.transform.position) < range){
				enemy.realDamage(enemy.getSkillDamageValue(bill.realAtk, damage)/HIT_COUNT);
			}
		}
	}
}
