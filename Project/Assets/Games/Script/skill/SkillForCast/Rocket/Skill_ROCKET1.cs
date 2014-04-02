using UnityEngine;
using System.Collections;

public class Skill_ROCKET1 : SkillBase {
	
	private GameObject rocketLauncherMissilePrb;
	private ArrayList rocketLauncherObjs = null;
	private GameObject rocketLauncherAttackEftPrb;
	private GameObject rocketLauncherAttackEft;
	private GameObject rocketLauncherExplosionPrb;
	private GameObject rocketLauncherExplosion;
	
	private int imageSequenceIndex = 0;
	private const int IMAGE_SEQUENCE_LENGTH = 2;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		
		rocket.toward(target.transform.position);
//		rocket.imageSequenceCallback = ()=>{
//			string key = imageSequenceIndex % IMAGE_SEQUENCE_LENGTH == 0? 
//							"SpecialEffects_01" : "SpecialEffects_85";
//			playImageSequence(rocket, key);
//		};
		rocket.castSkill("SkillA");
		
		yield return new WaitForSeconds(0.5f);
		
		MusicManager.playEffectMusic("SFX_Rocket_Rocket_Launcher_1a");
		StartCoroutine(RocketLauncherShootMissile(objs));
	}
	
//	private void playImageSequence(Rocket rocket, string key){
//		BoneRocket bone = rocket.model.GetComponent<BoneRocket>();
//		ArrayList ary = bone.partHash [key] as ArrayList;
//		GameObject entry = (GameObject)ary [1];
//		PackedSprite sp = entry.GetComponent<PackedSprite>();
//		sp.animations[0].Reset();
//		sp.PlayAnim(0);
//		++imageSequenceIndex;
//	}
	
	private IEnumerator RocketLauncherShootMissile(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		rocketLauncherObjs = objs;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		Vector3 endVc3 = (heroDoc.model.transform.localScale.x > 0)? 
								target.transform.position+ new Vector3(-80,70,0)
								:target.transform.position+ new Vector3(80,70,0);
		;
		Vector3 creatVc3 = (heroDoc.model.transform.localScale.x > 0)? 
								caller.transform.position + new Vector3(80,70,-50)
								:caller.transform.position + new Vector3(-80,70,-50);
		if(rocketLauncherMissilePrb == null){
			rocketLauncherMissilePrb = Resources.Load("eft/Rocket/SpecialEffects_35") as GameObject;
		}
		yield return new WaitForSeconds(.68f);
		
		GameObject rocketLauncherMissile = Instantiate(rocketLauncherMissilePrb,creatVc3, transform.rotation) as GameObject;
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		float deg = (angle*360)/(2*Mathf.PI);
		
		rocketLauncherMissile.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(rocketLauncherMissile,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","RocketLauncherShowExplosion"},{ "oncompletetarget",gameObject},{ "oncompleteparams",rocketLauncherMissile}});
	}
	
	private void RocketLauncherShowExplosion(GameObject rocketLauncherMissile){
		Destroy(rocketLauncherMissile);
		GameObject scene = rocketLauncherObjs[0] as GameObject;
		GameObject target = rocketLauncherObjs[2] as GameObject;

		if(rocketLauncherExplosionPrb == null){
			rocketLauncherExplosionPrb = Resources.Load("eft/Rocket/SpecialEffects_71") as GameObject;
		}
		
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,15,0), 1.5f, 0.0f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.3f));
		
		Destroy(rocketLauncherExplosion);
		rocketLauncherExplosion = Instantiate(rocketLauncherExplosionPrb,target.transform.position ,target.transform.rotation) as GameObject;
		rocketLauncherExplosion.transform.position += new Vector3(0,60, -100);
		DamageEnemy();
	}
	
	private void DamageEnemy(){
		GameObject caller = rocketLauncherObjs[1] as GameObject;
		GameObject target = rocketLauncherObjs[2] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET1");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		float damagePer = ((Effect)tempNumber["atk_PHY"]).num;
		int damage = (int)(heroDoc.realAtk.PHY * (damagePer / 100.0f + 1.0f));
		int tempRadius = (int)tempNumber["AOERadius"];
		
		Character enemy = target.GetComponent<Character>();
		damage = enemy.getSkillDamageValue(heroDoc.realAtk, damagePer);
		enemy.realDamage(damage);
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		//passive skill 20A
		HeroData heroD = heroDoc.data as HeroData;
		Hashtable passive20A =  heroD.getPSkillByID("ROCKET20A");
		Hashtable passive20B =  heroD.getPSkillByID("ROCKET20B");
		bool isP20a = false;
		bool isP20b = false;
		if(passive20A != null)
		{
			skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET20A");
			int chance = (int)skillDef.passiveEffectTable["universal"];
			int time   = (int)skillDef.passiveEffectTable["universalTime"];
			if(StaticData.computeChance(chance,100))
			{
				isP20a = true;
				State s = new State(time, null);
				enemy.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
			}
		}
		if(passive20B != null)
		{
			skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET20B");
			int chance = (int)skillDef.passiveEffectTable["universal"];
			int time   = (int)skillDef.passiveEffectTable["universalTime"];
			if(StaticData.computeChance(chance,100))
			{
				//halving
				isP20b = true;
				enemy.addBuff("ROCKET1",time, -enemy.realDef.PHY/2,BuffTypes.DEF_PHY);
			}	
		}
		
		foreach(Enemy otherEnemy in enemyList)
		{
			if(otherEnemy.getID() != enemy.getID())
			{
				Vector2 vc2 = otherEnemy.transform.position - enemy.transform.position;
				if( StaticData.isInOval(tempRadius,tempRadius , vc2) )
				{
					damage = otherEnemy.getSkillDamageValue(heroDoc.realAtk, damagePer);
					otherEnemy.realDamage(damage / 2);
					if(passive20A != null)
					{
						skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET20A");
						int chance = (int)skillDef.passiveEffectTable["universal"];
						int time   = (int)skillDef.passiveEffectTable["universalTime"];
						if(isP20a)
						{
							isP20a = false;
							State s = new State(time, null);
							enemy.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
						}
					}
					if(passive20B != null)
					{
						skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET20B");
						int chance = (int)skillDef.passiveEffectTable["universal"];
						int time   = (int)skillDef.passiveEffectTable["universalTime"];
						if(isP20b)
						{
							isP20b = false;
							otherEnemy.addBuff("ROCKET1",time, -enemy.realDef.PHY/2,BuffTypes.DEF_PHY);
						}	
					}
				}
			}
		}
	}
}
