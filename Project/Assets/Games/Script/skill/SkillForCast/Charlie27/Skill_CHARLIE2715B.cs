using UnityEngine;
using System.Collections;

public class Skill_CHARLIE2715B : SkillBase {
	private ArrayList parms;
	private Character charlie27;
	private Character enemy;
	private Object grenadePrefab;
	private Object blastPrefab;
	private float damage;
	private Vector3 createPos;
	private Vector3 targetPos;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		LoadResources();
		
		charlie27.toward(enemy.transform.position);
		charlie27.castSkill("Skill15B");
		yield return new WaitForSeconds(1.1f);
		
		bool isTowardsRight = charlie27.model.transform.localScale.x > 0;
		createPos = charlie27.transform.position + new Vector3(isTowardsRight? 120f: -120f, 250f, -1f);
		targetPos = enemy.transform.position + new Vector3((isTowardsRight? Random.Range(-80f, 0f): Random.Range(0f, 80f)),
																Random.Range(0f, 80f), -1f);
		ThrowGrenadeOnce(createPos, targetPos);
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		charlie27 = caller.GetComponent<Character>();
		enemy = target.GetComponent<Character>();
		
		if (null == grenadePrefab){
			grenadePrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_15B_Grenade");
		}
		if (null == blastPrefab){
			blastPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_15B_Blast");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CHARLIE2715B");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
	}
	
	private void ThrowGrenadeOnce(Vector3 sPos, Vector3 ePos){
		GameObject grenade = Instantiate(grenadePrefab) as GameObject;
		grenade.transform.position = sPos;
		float time = Vector3.Distance(sPos, ePos) / Random.Range(600f,1000f);
		iTween.MoveBy(grenade, new Hashtable(){
			{"x", ePos.x - sPos.x}, 
			{"time", time},
			{"easetype","linear"},
			{"oncomplete", "BlastGrenadeAndDamageEnemy"},
			{"oncompletetarget",gameObject},
			{"oncompleteparams",grenade}
		});
		
		float difY = ePos.y - sPos.y;
		float paraCurveY = (difY>0? difY:0) + Random.Range(50f,100f)*time;
		float paraCurveTime = time/3f * (difY>0? 2:1);
		iTween.MoveAdd(grenade, new Hashtable(){
			{"y", paraCurveY},
			{"time", paraCurveTime},
			{"easetype","easeOutQuad"}
		});
		iTween.MoveAdd(grenade, new Hashtable(){
			// {"y", ePos.y - sPos.y - paraCurveY},
			{"y", ePos.y - sPos.y - paraCurveY},
			{"delay", paraCurveTime},
			{"time", time - paraCurveTime},
			{"easetype","easeInQuad"}
		});
	}
	
	private void BlastGrenadeAndDamageEnemy(GameObject grenade){
		GameObject blast = Instantiate(blastPrefab) as GameObject;
		blast.transform.position = (grenade).transform.position;
		
		enemy.realDamage(enemy.getSkillDamageValue(charlie27.realAtk, damage));
		
		if (null != grenade){
			iTween.Stop(grenade);
			Destroy(grenade);
		}
	}
}
