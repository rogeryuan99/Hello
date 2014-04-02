using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_ROCKET30A__ : SkillBase {

	private ArrayList rocketLauncherObjs = null;
	private GameObject rocketLauncherAttackEftPrb;
	private GameObject rocketLauncherAttackEft;
	private GameObject rocketLauncherExplosionPrb;
	private List<GameObject> rocketLauncherExplosions = new List<GameObject>();
	
	
	public override IEnumerator Cast (ArrayList objs)
	{
		Debug.LogError("Cast Skill_ROCKET30A");
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		MusicManager.playEffectMusic("SFX_Rocket_Fuzzy_but_Deadly_1a");
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("SkillA");// Hero.castSkill
		
		yield return new WaitForSeconds(1f);
		
		StartCoroutine(RocketLauncherShowExplosion(objs));
	}
	
	private IEnumerator RocketLauncherShowExplosion(ArrayList objs){
		
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,15,0), 1.5f, 0.0f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.3f));
		
		for (int i=0; i<rocketLauncherExplosions.Count; i++){
			if (null != rocketLauncherExplosions[i]){
				Destroy(rocketLauncherExplosions[i]);
			}
		}
		rocketLauncherExplosions.Clear();
		
		for (int i=0; i<20; i++){
			StartCoroutine(Explosion(BattleBg.getPointInScreen(), Random.value*.2f));
		}
		
		yield return new WaitForSeconds(.1f);
		 
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("ROCKET30A").activeEffectTable;
		
		float damagePer = ((Effect)tempNumber["atk_PHY"]).num;
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		int damage = 0;
		
		foreach(Enemy enemy in enemyList)
		{
			damage = enemy.getSkillDamageValue(heroDoc.realAtk, damagePer);
			enemy.realDamage(damage);
		}
	}
	
	private IEnumerator Explosion(Vector3 pos, float delay){
		if(rocketLauncherExplosionPrb == null){
			rocketLauncherExplosionPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET1_Explosion") as GameObject;
		}
		
		yield return new WaitForSeconds(Random.value);
		
		rocketLauncherExplosions.Add(Instantiate(rocketLauncherExplosionPrb, pos, Quaternion.identity) as GameObject);
		rocketLauncherExplosions[rocketLauncherExplosions.Count-1].transform.position += new Vector3(Random.value*50f, Random.value*50f, -100);
	}
}
