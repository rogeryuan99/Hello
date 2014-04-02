using UnityEngine;
using System.Collections;

public class Skill_MANTIS5B : SkillBase{
	protected ArrayList objs;
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.SkillKeyFrameEvent += addAttackEft;
		heroDoc.castSkill("Skill5B");
		
		yield return new WaitForSeconds(1f);
		
		showDamage();
		
		yield return new WaitForSeconds(0.2f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Staff_Strike_1a");
	}
	
	protected void addAttackEft(Character c){
		//MusicManager.playEffectMusic("SFX_Mantis_Staff_Strike_1a");
		
		GameObject caller = objs[1] as GameObject;
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.SkillKeyFrameEvent -= addAttackEft;
		heroDoc.SkillKeyFrameEvent += addDamageEft;
		
		GameObject atkEftPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS5B") as GameObject;
		GameObject atkEft = Instantiate(atkEftPrefab) as GameObject;
		atkEft.transform.position = c.transform.position + new Vector3(0,100,0);
	}
	
	protected void addDamageEft(Character c){
		GameObject caller = objs[1] as GameObject;
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.SkillKeyFrameEvent -= addDamageEft;
		
		foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
			GameObject damageEftPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS5B_DamageEft") as GameObject;
			GameObject damageEft = Instantiate(damageEftPrefab) as GameObject;
			damageEft.transform.position = enemy.transform.position + new Vector3(0,100,0);
			damageEft.transform.localScale = new Vector3(.4f,.4f,1f);
		}
	}
	
	protected void showDamage(){
		GameObject caller = objs[1] as GameObject;
		Mantis heroDoc = caller.GetComponent<Mantis>();
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["MANTIS5B"] as SkillDef;
		Hashtable tempNumber = skillDef.activeEffectTable;
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		int aoeRadius = (int)tempNumber["AOERadius"];
		
		ArrayList a = new ArrayList(EnemyMgr.enemyHash.Values);
		foreach(Enemy enemy in a)
		{
			Vector2 vc2 = caller.transform.position - enemy.transform.position;
			if(StaticData.isInOval(aoeRadius,aoeRadius,vc2)){
				if(!enemy.isDead){
					Hashtable psTable = (heroDoc.data as HeroData).passiveHash["MANTIS20A"] as Hashtable;
					float val = (null != psTable) ? 2f: 1f;
					enemy.realDamage(enemy.getSkillDamageValue(heroDoc.realAtk, tempAtkPer*val));
				}
			}
		}		
	}
}
