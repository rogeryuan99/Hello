using UnityEngine;
using System.Collections;

public class Skill_ROCKET5B : SkillBase {
	
	private ArrayList objs;
	private GameObject rocketLauncherExplosionPrb;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		this.objs = objs;
		MusicManager.playEffectMusic("SFX_Rocket_Paint_the_Target_1a");
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.toward(target.transform.position);
		rocket.damageEnemyCallback = DamageEnemy;
		rocket.castSkill("Skill5B");
		
		yield return new WaitForSeconds(0f);
	}
	
	private void DamageEnemy()
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("ROCKET5B").activeEffectTable;
		
		float damagePer = ((Effect)tempNumber["atk_PHY"]).num;
		
		Character character = target.GetComponent<Character>();
		
		int damage = character.getSkillDamageValue(heroDoc.realAtk, damagePer);
		
		for (int i=0; i<12; i++)
		{
			StartCoroutine(Explosion(BattleBg.getPointInScreen(), Random.value*.35f, damage));
		}
	}
	
	private IEnumerator Explosion(Vector3 pos, float delay, int damage)
	{
		GameObject target = objs[2] as GameObject;
		Character character = target.GetComponent<Character>();
		
		if(rocketLauncherExplosionPrb == null){
			rocketLauncherExplosionPrb = Resources.Load("eft/Rocket/SpecialEffects_82") as GameObject;
		}
		
		yield return new WaitForSeconds(delay);
		
		GameObject explosion = Instantiate(rocketLauncherExplosionPrb, pos, Quaternion.identity) as GameObject;
		explosion.transform.position = character.transform.position + new Vector3(Random.value*50f-25f, Random.value*80f, -100f);
		character.realDamage(damage);
	}
}
