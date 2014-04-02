using UnityEngine;
using System.Collections;

public class Skill_CHARLIE2730A : SkillBase {
	
	private ArrayList parms;
	private Character charlie27;
	private Object grenadePrefab;
	private Object blastPrefab;
	private int range;
	private float damage;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		LoadResources();
		
		charlie27.castSkill("Skill30A");
		yield return new WaitForSeconds(1.1f);
		
		StartCoroutine(ThrowGrenades(charlie27.model.transform.localScale.x > 0));
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		charlie27 = caller.GetComponent<Character>();
		
		if (null == grenadePrefab){
			grenadePrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30A_Grenade");
		}
		if (null == blastPrefab){
			blastPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30A_Blast");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CHARLIE2730A");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
		range = (int)def.activeEffectTable["AOERadius"];
	}
	
	private IEnumerator ThrowGrenades(bool isTowardsRight){
		float createX = isTowardsRight? 120: -120;
		float firstX  = isTowardsRight? Random.Range(100f,250f): -Random.Range(100f,250f);
		float secondX = isTowardsRight? Random.Range(250f,400f): -Random.Range(250f,400f);
		
		Vector3 createPos = charlie27.transform.position + new Vector3(createX, 250f, -1f);
		Vector3 targetPos = charlie27.transform.position + new Vector3(firstX, Random.Range(0f,100f), 0f);
		ThrowGrenadeOnce(createPos, targetPos);
		targetPos = charlie27.transform.position + new Vector3(secondX, Random.Range(0f,-100f), 0f);
		ThrowGrenadeOnce(createPos, targetPos);
		
		yield return new WaitForSeconds(.5f);
		
		createPos = charlie27.transform.position + new Vector3(-createX, 250f, -1f);
		targetPos = charlie27.transform.position + new Vector3(-firstX, Random.Range(0f,100f), 0f);
		ThrowGrenadeOnce(createPos, targetPos);
		targetPos = charlie27.transform.position + new Vector3(-secondX, Random.Range(0f,-100f), 0f);
		ThrowGrenadeOnce(createPos, targetPos);
	}
	
	private void ThrowGrenadeOnce(Vector3 sPos, Vector3 ePos){
		GameObject grenade = Instantiate(grenadePrefab) as GameObject;
		grenade.transform.position = sPos;
		float time = Vector3.Distance(sPos, ePos) / Random.Range(400f,800f);
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
		// float paraCurveY = Random.Range(50f,100f)*time;
		float paraCurveTime = time/3f;
		iTween.MoveAdd(grenade, new Hashtable(){
			{"y", paraCurveY},
			{"time", paraCurveTime},
			{"easetype","easeOutQuad"}
		});
		iTween.MoveAdd(grenade, new Hashtable(){
			{"y", ePos.y - sPos.y - paraCurveY},
			{"delay", paraCurveTime},
			{"time", time - paraCurveTime},
			{"easetype","easeInQuad"}
		});
	}
	
	private void BlastGrenadeAndDamageEnemy(GameObject grenade){
		GameObject blast = Instantiate(blastPrefab) as GameObject;
		blast.transform.position = (grenade).transform.position;
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		foreach(Enemy enemy in enemyList){
			if (Vector2.Distance(grenade.transform.position, enemy.transform.position) < range){
				enemy.realDamage(enemy.getSkillDamageValue(charlie27.realAtk, damage));
			}
		}
		
		if (null != grenade){
			iTween.Stop(grenade);
			Destroy(grenade);
		}
	}
}
