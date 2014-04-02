using UnityEngine;
using System.Collections;

public class Skill_GAMORA1 : SkillBase
{
	public GameObject enemyDamagePrb;
	
	public ArrayList objs = null;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Gamora_Deadly_Slash_1a");
		this.objs = objs;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA1");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		int tempRadius = (int)tempNumber["AOERadius"];
		int tempAtkPer = (int)((Effect)tempNumber["atk_PHY"]).num;
		
		yield return new WaitForSeconds(0.5f);
		
		gamora.castSkill("GAMORA1");
		
		yield return new WaitForSeconds(0.8f);
		
		if(enemyDamagePrb == null)
		{
			enemyDamagePrb = Resources.Load("eft/Gamora/SkillGamora1EnemyDamageEft") as GameObject;
		}
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		foreach(Enemy enemy in enemyList)
		{
			Vector2 vc2 = enemy.transform.position - caller.transform.position;
			if( StaticData.isInOval(tempRadius, tempRadius, vc2) )
			{
				if(enemy.isDead)
				{
					continue;
				}
				GameObject enemyDamageTemp = Instantiate(enemyDamagePrb) as GameObject;
				int damage = (int)(enemy.getSkillDamageValue(gamora.realAtk, tempAtkPer) * GetPassiveValue(gamora.data as HeroData));
				enemy.realDamage(damage);
				
				enemy.playDamageEffect(caller,0);
				enemy.shakeCharacter();
				enemyDamageTemp.transform.position = enemy.transform.position;
				if(vc2.x >= 0) // enemy at gamora right
				{
					enemyDamageTemp.transform.localScale = new Vector3(-enemyDamageTemp.transform.localScale.x, enemyDamageTemp.transform.localScale.y, enemyDamageTemp.transform.localScale.z);
					enemyDamageTemp.transform.position = new Vector3(
						enemyDamageTemp.transform.position.x - 50,  
						enemyDamageTemp.transform.position.y + 60,
						-100);
				}
				else // enemy at gamora left
				{
					enemyDamageTemp.transform.position = new Vector3(
						enemyDamageTemp.transform.position.x + 50,  
						enemyDamageTemp.transform.position.y + 60,
						-100);
				}
			}
		}
	}
	
	private float GetPassiveValue(HeroData data){
		Hashtable psTable = data.passiveHash["GAMORA10A"] as Hashtable;
		float v = (null != psTable && psTable.ContainsKey("sk_damage"))
					? ((int)psTable["sk_damage"] * 0.01f + 1f)
					: 1f;
		return v;
	}
}
