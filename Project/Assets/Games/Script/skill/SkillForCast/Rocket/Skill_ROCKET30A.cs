using UnityEngine;
using System.Collections;

public class Skill_ROCKET30A : SkillBase
{
	GameObject damageEftPrb;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("Skill30A");// Hero.castSkill
		yield return new WaitForSeconds(0.3f);
			
		MusicManager.playEffectMusic("SFX_Rocket_Fuzzy_but_Deadly_1a");
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(showDamageEft(heroDoc));
	}
	
	public IEnumerator showDamageEft(Hero heroDoc)
	{
		yield return new WaitForSeconds(0.1f);
		
		if(damageEftPrb == null)
		{
			damageEftPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET30A_DamageEft") as GameObject;
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET30A");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		float damagePer = ((Effect)tempNumber["atk_PHY"]).num;
		 
		
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		int damageEftCount = 5;//Random.Range(3, 5);
		
		foreach(Enemy enemy in enemyList)
		{
			if(enemy != null && !enemy.isDead)
			{
				int damage = enemy.getSkillDamageValue(heroDoc.realAtk, damagePer);
				for(int i = 0; i < damageEftCount; ++i)
				{
					int d = damage / damageEftCount;
					if(d == 0)
					{
						d = 1;
					}
					enemy.realDamage(d);
					Vector3 damageEftPosition = getPosInEnemyBody(enemy);
					createDamageEft(enemy, damageEftPosition);
//					float s = Random.Range(1, 3);
					yield return new WaitForSeconds(0.4f);			
				}
			}
		}
	}
	
	public void createDamageEft(Character character, Vector3 pos)
	{
		GameObject damageEft = Instantiate(damageEftPrb) as GameObject;
		damageEft.transform.position = pos + new Vector3(0, 0, character.transform.position.z - 10);
		damageEft.transform.localScale = new Vector3(0.2f, 0.2f, 1);
//		Debug.Break();
	}
	
	public Vector3 getPosInEnemyBody(Enemy enemy)
	{
		BoxCollider bc = enemy.collider as BoxCollider;
		float randomX = UnityEngine.Random.Range(bc.bounds.min.x,bc.bounds.max.x);
		float randomY  = UnityEngine.Random.Range(bc.bounds.min.y,bc.bounds.max.y);
		return new Vector3(randomX, randomY,0);
	}
}
