using UnityEngine;
using System.Collections;

public class Skill_GROOT5A : SkillBase
{
	
	protected GameObject stonePrb;
	protected GameObject stone;
	
	protected GameObject vineShieldPrb;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Groot_Vine_Shield_1a");
		GRoot heroDoc = caller.GetComponent<GRoot>();
		
		
		
		heroDoc.castSkill("GROOT5B"); // groot5A anima same as groot5b
		
		yield return new WaitForSeconds(0.8f);
		
		if(stonePrb == null)
		{
			stonePrb = Resources.Load("eft/Groot/SkillEft_GROOT5BStone") as GameObject;
		}
		stone = Instantiate(stonePrb) as GameObject;
		
		float x = caller.transform.position.x;
		
		if(heroDoc.model.transform.localScale.x < 0)
		{
			x -= 150; 
		}
		else
		{
			x += 150; 
		}
		stone.transform.position = new Vector3(x, caller.transform.position.y, caller.transform.position.z);
		
		yield return new WaitForSeconds(0.8f);
		
		Hero hero = target.GetComponent<Hero>();
		
		if(hero.getIsDead())
		{
			yield break;
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT5A");
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		float maxHPPer = ((Effect)tempNumber["hp"]).num;
		int maxHP =  (int)(heroDoc.realMaxHp * (maxHPPer / 100.0f));
		
		if(vineShieldPrb == null)
		{
			vineShieldPrb = Resources.Load("eft/Groot/SkillEft_GROOT5A_VineShield") as GameObject;
		}
		GameObject vineShieldObj = Instantiate(vineShieldPrb) as GameObject;
		
		
		
		
		if(hero.vineShield != null)
		{
			Destroy(hero.vineShield.hpBar.gameObject);
			Destroy(hero.vineShield.gameObject);
		}
		
		vineShieldObj.transform.parent = target.transform;
		vineShieldObj.transform.localPosition = new Vector3(0, hero.model.transform.localPosition.y, 0);
		VineShield vs = vineShieldObj.GetComponent<VineShield>();
		
		vs.init(maxHP, hero);
	}
}
