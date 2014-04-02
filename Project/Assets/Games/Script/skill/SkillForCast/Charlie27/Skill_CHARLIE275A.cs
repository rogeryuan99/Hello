using UnityEngine;
using System.Collections;

public class Skill_CHARLIE275A : SkillBase {
	
	private Object rotationPrefab;
	private Object lightPrefab;
	private const int HIT_COUNT = 6;
	private int   radius;
	private float damage;
	private Character charlie27;
	
	private ArrayList parms;
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		Character charlie27 = caller.GetComponent<Character>();
		
		charlie27.castSkill("Skill5A");
		
		yield return new WaitForSeconds(.75f);
		
		LoadResources();
		StartCoroutine(CreateEffect());
		StartCoroutine(DamageEnemy());
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		charlie27 = caller.GetComponent<Character>();
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CHARLIE275A");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
		radius = (int)def.activeEffectTable["AOERadius"];
		
		if (null == rotationPrefab){
			rotationPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5A_Rotation");
		}
		if (null == lightPrefab){
			lightPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5A_Light");
		}
	}
	
	private IEnumerator CreateEffect(){
		bool isLeftSide = charlie27.model.transform.localScale.x > 0;
		
		GameObject eft = Instantiate(rotationPrefab) as GameObject;
		eft.transform.localScale = isLeftSide? new Vector3(1.18f, 1.18f, 1f): new Vector3(-1.18f, 1.18f, 1f);
		eft.transform.position = charlie27.transform.position + new Vector3(0f, 70f, 0f);
		
		yield return new WaitForSeconds(.55f);
		
		GameObject light = Instantiate(lightPrefab) as GameObject;
		light.transform.localScale = isLeftSide? new Vector3(1.2f, 1.2f, 1f): new Vector3(-1.2f, 1.2f, 1f);
		light.transform.position = charlie27.transform.position + new Vector3(0f, 105f, -5f);
	}
	
	private IEnumerator DamageEnemy(){
		for (int i=0; i<HIT_COUNT; i++){
			DamageOnce();
			yield return new WaitForSeconds(.1f);
		}
	}
	
	private void DamageOnce(){
		ArrayList enemyList = new ArrayList((charlie27 is Charlie27)? EnemyMgr.enemyHash.Values: HeroMgr.heroHash.Values);
		foreach(Character enemy in enemyList){
			if (Vector3.Distance(charlie27.transform.position, enemy.transform.position) < radius){
				enemy.realDamage(enemy.getSkillDamageValue(charlie27.realAtk , damage)/HIT_COUNT);
			}
		}
	}
}
