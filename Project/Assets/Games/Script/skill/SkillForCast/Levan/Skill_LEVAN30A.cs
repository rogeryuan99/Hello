using UnityEngine;
using System.Collections;

public class Skill_LEVAN30A : SkillBase {
	protected ArrayList objs;
	protected int tempDamage;
	protected bool isPlayEft = false;
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		Character character = caller.GetComponent<Character>();
		character.castSkill("Skill30A");
		if(character is Levan){
			Levan leven = character as Levan;
			leven.SkillKeyFrameEvent += addDamageEft;	
		}else{
			Ch2_Levan leven = character as Ch2_Levan;
			leven.SkillKeyFrameEvent += addDamageEft;	
		}

		yield return new WaitForSeconds(0f);
		
		MusicManager.playEffectMusic("SFX_Levan_Cruel_Cuts_1a");
	}
	
	protected void addDamageEft(Character c){
		GameObject caller = objs[1] as GameObject;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN30A");
		int time = (int)skillDef.buffDurationTime;
		tempDamage = (int)((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		
		isPlayEft = true;
		
		Character character = caller.GetComponent<Character>();
		if(character is Levan){
			Levan levan = character as Levan;
			levan.SkillKeyFrameEvent -= addDamageEft;
			levan.addBuff("LEVAN30A" + "_" + levan.data.type, time, 0, BuffTypes.ATK_PHY, buffFinish);
		}else{
			Ch2_Levan levan = character as Ch2_Levan;
			levan.SkillKeyFrameEvent -= addDamageEft;
			levan.addBuff("LEVAN30A" + "_" + levan.data.type, time, 0, BuffTypes.ATK_PHY, buffFinish);
		}
		
		GameObject levanDamageEftPrefab = Resources.Load("eft/Levan/LevanDamageEft") as GameObject;
		GameObject levanDamageEft = Instantiate(levanDamageEftPrefab) as GameObject;
		levanDamageEft.transform.position = caller.transform.position + new Vector3(0,250,0);
	}
	
	protected void buffFinish(Character c, Buff self){
		isPlayEft = false;
	}
	
	public int addHeavyDamage(Character c){
		if(c != null){
			c.addCruelCutsDelegate -= addHeavyDamage;
			return tempDamage;
		}
		return 0;
	}
	
	void Update(){
		if(isPlayEft){
			foreach(Hero hero in HeroMgr.heroHash.Values){
				if(null != hero.targetObj && (hero.data.type == HeroData.LEVAN || hero.data.type == HeroData.SKUNGE)){
					Character character = hero.targetObj.GetComponent<Character>();
					if(null != character && null == character.addCruelCutsDelegate && !character.isDead){
						character.addCruelCutsDelegate += addHeavyDamage;
					}
				}
			}
			
			foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
				if(null != enemy.targetObj && (enemy.data.type == EnemyDataLib.Ch2_Levan || enemy.data.type == EnemyDataLib.Ch2_Skunge)){
					Character character = enemy.targetObj.GetComponent<Character>();
					if(null != character && null == character.addCruelCutsDelegate && !character.isDead){
						character.addCruelCutsDelegate += addHeavyDamage;
					}
				}
			}
		}
	}
	
}
