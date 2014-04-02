using UnityEngine;
using System.Collections;

public class Skill_MANTIS30A : SkillBase {
	protected float time;
	protected float subTime;
	protected bool isDamageFlag = false;
	protected bool isOnceFlag = false;
	protected int aoeRadius;
	protected GameObject caller;
	protected Enemy enemy;
	
	public override IEnumerator Cast (ArrayList objs){
		caller = objs[1] as GameObject;
		
		Mantis heroDoc = caller.GetComponent<Mantis>();
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["MANTIS30A"] as SkillDef;
		Hashtable tempNumber = skillDef.activeEffectTable;
		time = skillDef.skillDurationTime;
		aoeRadius = (int)tempNumber["AOERadius"];
		isDamageFlag = true;
		isOnceFlag = true;
		subTime = 0f;
		
		yield return new WaitForSeconds(0f);
	}
	
	protected void playDamageEft(Enemy e){
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.toward(e.transform.position);
		heroDoc.castSkill("Skill30A");
		
		heroDoc.SkillKeyFrameEvent += addDamageEft;
			
		StartCoroutine(delayDamageEft());
	}
	
	protected void addDamageEft(Character c){
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.SkillKeyFrameEvent -= addDamageEft;
		
		GameObject damageEftPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS30A_DamageEft") as GameObject;	
		GameObject damageEft = Instantiate(damageEftPrefab) as GameObject;
		damageEft.transform.position = enemy.transform.position + new Vector3(0,50,0);
	}
	
	protected IEnumerator delayDamageEft(){
		yield return new WaitForSeconds(0.2f);
		MusicManager.playEffectMusic("SFX_Mantis_Grand_Master_1a");
		yield return new WaitForSeconds(0.8f);
		enemy.playDamageEffect(caller,300f);
		yield return new WaitForSeconds(1f);
		isOnceFlag = true;
	}
	
	void Update(){
		if(isDamageFlag){
			subTime += Time.deltaTime;
			if(subTime > time){
				isDamageFlag = false;	
			}else{
				if(!isOnceFlag) return;
				foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
					Vector2 vc2 = caller.transform.position - enemy.transform.position;
					if(StaticData.isInOval(aoeRadius,aoeRadius,vc2)){
						if(!enemy.isDead){
							playDamageEft(enemy);
							this.enemy = enemy;
							isOnceFlag = false;
						}
					}
				}
			}
		}
	}
	
}
