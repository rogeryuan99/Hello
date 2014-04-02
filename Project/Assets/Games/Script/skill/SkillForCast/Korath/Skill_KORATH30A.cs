using UnityEngine;
using System.Collections;

public class Skill_KORATH30A : SkillBase {
	protected GameObject caller;
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		this.caller = caller;
		
		Character character = caller.GetComponent<Character>(); 
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH5A");
		int aoeRadius= (int)skillDef.activeEffectTable["AOERadius"];
		foreach(Enemy enemy in EnemyMgr.enemyHash.Values)
		{
			if(enemy.targetObj == caller && enemy.GetComponent<EnemyRemote>() == null)
			{
				Vector2 vc2 = caller.transform.position - enemy.transform.position;
				//if(StaticData.isInOval(aoeRadius,aoeRadius,vc2)){
					if(!enemy.isDead){
						character.toward(enemy.transform.position);
						break;
					}
				//}
			}
		}	
		 
		Korath korath = null;
		Ch1_Korath ch1_Korath = null;
		
		if(character is Korath)
		{
			korath = character as Korath;
			korath.addStunBuffCallBack += addStunBuff;
		}
		else
		{
			ch1_Korath = character as Ch1_Korath;
			ch1_Korath.addStunBuffCallBack += addStunBuff;
		}
		
		
		character.castSkill("Skill30A");
		
		yield return new WaitForSeconds(0.6f);
		
		MusicManager.playEffectMusic("SFX_Korath_Baton_Fury_1a");
	}
	
	protected void addStunBuff()
	{
		Character character = caller.GetComponent<Character>();
		Korath korath = null;
		Ch1_Korath ch1_Korath = null;
		
		if(character is Korath)
		{
			korath = character as Korath;
			korath.addStunBuffCallBack -= addStunBuff;
		}
		else
		{
			ch1_Korath = character as Ch1_Korath;
			ch1_Korath.addStunBuffCallBack -= addStunBuff;
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH30A");
		int stateTime = skillDef.buffDurationTime;
		int aoeRadius= (int)skillDef.activeEffectTable["AOERadius"];
		//bool isToward = true;
		if(character is Korath)
		{
			foreach(Enemy enemy in EnemyMgr.enemyHash.Values)
			{
				if(enemy.targetObj == caller && enemy.GetComponent<EnemyRemote>() == null)
				{
					Vector2 vc2 = caller.transform.position - enemy.transform.position;
					if(StaticData.isInOval(aoeRadius,aoeRadius,vc2)){
						if(!enemy.isDead)
						{
							State s = new State(stateTime, null);
							enemy.addAbnormalState(s, Character.ABNORMAL_NUM.STUN);
//							enemy.stunWithSeconds();
						}
					}
				}
			}	
		}
		else
		{
			foreach(Hero hero in HeroMgr.heroHash.Values)
			{
				Vector2 vc2 = caller.transform.position - hero.transform.position;
				if(hero is StarLord || hero is Rocket)
				{
					continue;
				}
				if(StaticData.isInOval(aoeRadius,aoeRadius,vc2))
				{
					if(!hero.isDead)
					{
						State s = new State(stateTime, null);
							hero.addAbnormalState(s, Character.ABNORMAL_NUM.STUN);
//						hero.stunWithSeconds();
					}
				}
			}
		}
	}
	
}
