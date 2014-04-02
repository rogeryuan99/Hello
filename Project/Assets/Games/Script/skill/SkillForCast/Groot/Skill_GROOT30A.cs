using UnityEngine;
using System.Collections;

public class Skill_GROOT30A : SkillBase 
{
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Groot_Wildwood_1b");
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("GROOT30A");// Hero.castSkill
		
		yield return new WaitForSeconds(1.2f);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT30A");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		int tempRadius = (int)tempNumber["AOERadius"];
		
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
			
		int damage = 0;
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		foreach(Enemy enemy in enemyList)
		{
			Vector2 vc2 = enemy.transform.position - caller.transform.position;
			if(StaticData.isInOval(tempRadius, tempRadius, vc2))
			{
				if(enemy.isDead)
				{
					continue;
				}
				damage = enemy.getSkillDamageValue(heroDoc.realAtk, tempAtkPer);
				enemy.realDamage(damage);
			}
		}
	}
	
	
}
