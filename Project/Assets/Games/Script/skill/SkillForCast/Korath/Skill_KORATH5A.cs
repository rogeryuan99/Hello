using UnityEngine;
using System.Collections;

public class Skill_KORATH5A : SkillBase
{
	protected ArrayList objs;
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
//		Korath korath = caller.GetComponent<Korath>();
//		korath.addStunBuffCallBack += addStunBuff;
				
		Character character = caller.GetComponent<Character>();
		character.toward(target.transform.position);
		character.castSkill("Skill5A");
		yield return new WaitForSeconds(0.5f);
		MusicManager.playEffectMusic("SFX_Korath_Beta_Baton_1a");
		yield return new WaitForSeconds(0.7f);
		
		addStunBuff();

	}
	
	protected void addStunBuff(){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;	
		
		Korath korath = caller.GetComponent<Korath>();
		// Hero enemy = target.GetComponent<Hero>();
		Character enemy = target.GetComponent<Character>();
//		korath.addStunBuffCallBack -= addStunBuff;
		
		HeroData data = korath.data as HeroData;
		Hashtable passiveTable = data.getPSkillByID("KORATH25");
		if(passiveTable != null)
		{
			SkillDef passiveSkillDef = SkillLib.instance.getSkillDefBySkillID("KORATH25");
			int uValue = (int)passiveSkillDef.passiveEffectTable["universal"];
			int time = (int)passiveSkillDef.passiveEffectTable["universalTime"];
			// if(null != enemy && null != enemy.data){
			if (null != enemy && enemy is Hero){
				Hero enemyHero = enemy as Hero;
				HeroData enemyData = enemyHero.data as HeroData;
				float initialDef = enemyHero.getInitRealDef(enemyData);
				float diffDef = initialDef - enemy.realDef.PHY;
				if(diffDef > 0){
					float deBuff = diffDef*(uValue/100f);
					enemy.addBuff("SKLL_KORATH25",time,deBuff,BuffTypes.DE_DEF_PHY);
				}
			}		
		}
		
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH5A");
		int stateTime = skillDef.buffDurationTime;
		Character c = target.GetComponent<Character>();
		if(!c.getIsDead())
		{
			State s= new State(stateTime, null);
			c.addAbnormalState(s, Character.ABNORMAL_NUM.STUN);
//			c.stunWithSeconds();
		}
	}
}
