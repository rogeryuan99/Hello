using UnityEngine;
using System.Collections;

public class Skill_BUG30B : SkillBase {
	protected Enemy targetEnemy;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		Bug bug = caller.GetComponent<Bug>();
		bug.Skill15AHitEftCallback += addHitEft;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BUG30B");
		int time = (int)skillDef.buffDurationTime;
		int aoeRadius= (int)skillDef.activeEffectTable["AOERadius"];
		
		foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
			Vector2 vc2 = bug.transform.position - enemy.transform.position;
			if(StaticData.isInOval(aoeRadius,aoeRadius,vc2)){
				if(!enemy.isDead){
					this.targetEnemy = enemy;
					bug.toward(enemy.transform.position);
					bug.castSkill("Skill30B");
					break;
				}	
			}
		}
		
		yield return new WaitForSeconds(0.5f);
	}
	
	protected void addHitEft(Character c){
		Bug bug = c.GetComponent<Bug>();
		bug.Skill15AHitEftCallback -= addHitEft;
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BUG30B");
		int time = (int)skillDef.buffDurationTime;
		int skDamage = (int)((Effect)skillDef.activeEffectTable["atk_PHY"]).num;
		float per = ((Effect)skillDef.buffEffectTable["hp"]).num;
		int hp = (int)(targetEnemy.realMaxHp * (per / 100.0f));
		int tempAtk = targetEnemy.getSkillDamageValue(c.realAtk, skDamage);
		targetEnemy.realDamage(tempAtk);
		
		targetEnemy.addBuff("Skill_BUG30B", time, hp/time, BuffTypes.DE_HP, buffFinish);
		
		StartCoroutine(delayHitEft());
	}
	
	protected IEnumerator delayHitEft(){
		targetEnemy.changeStateColor(new Color(1f, 1f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);
		
		GameObject hitEftPrefab = Resources.Load("eft/Bug/Skill_BUG30B_HitEft") as GameObject;
		GameObject hitEft = Instantiate(hitEftPrefab) as GameObject;
		hitEft.transform.parent = targetEnemy.transform;
		hitEft.transform.localPosition = new Vector3(0,200,0);
		hitEft.transform.localScale = new Vector3(5,5,1);
		
		yield return new WaitForSeconds(0.2f);
		
		GameObject hitEft2 = Instantiate(hitEftPrefab) as GameObject;
		hitEft2.transform.parent = targetEnemy.transform;
		hitEft2.transform.localPosition = new Vector3(0,180,0);
		hitEft2.transform.localScale = new Vector3(4,4,1);
	}
	
	protected void buffFinish(Character character, Buff self){
		if(!targetEnemy.isDead){
			targetEnemy.model.renderer.material.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
