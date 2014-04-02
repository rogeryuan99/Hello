using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_ROCKET30B : SkillBase
{
	protected ArrayList objs;
	protected List<GameObject> buffEftList = new List<GameObject>();
	private int rewardHarm;
//	protected List<Enemy>  commonTargetList = new List<GameObject>();

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		this.objs = objs;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.addHerosBuffCallback += addBuffEft;
		
		MusicManager.playEffectMusic("SFX_Rocket_Concentrate_Fire_1a");
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("Skill30B");// Hero.castSkill
		
		
		yield return new WaitForSeconds(0.7f);
		GameObject prefab = Resources.Load("eft/Rocket/SkillEft_ROCKET30B") as GameObject;
		GameObject eft = Instantiate(prefab) as GameObject;
		eft.transform.localScale = new Vector3(heroDoc.model.transform.localScale.x > 0? eft.transform.localScale.x: -eft.transform.localScale.x,
													eft.transform.localScale.y, eft.transform.localScale.z);
		eft.transform.position = caller.transform.position + new Vector3(0,100,0);
		//eft.transform.parent = heroDoc.transform;
		//eft.transform.localPosition = Vector3.zero;
		foreach(Enemy e in EnemyMgr.enemyHash.Values){
			e.addConcentrateFireDelegate += addConcentrateFireDelegate;
		}
	}
	
	public void addBuffEft(){
		GameObject caller = objs[1] as GameObject;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.addHerosBuffCallback -= addBuffEft;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET30B");
		int time = (int)skillDef.buffDurationTime;
		float atkPer = ((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		rewardHarm = (int)((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		
		GameObject buffEftPrefab = Resources.Load("eft/Rocket/SkillEft_ROCKET30B_BuffEft") as GameObject;
		foreach(Hero hero in HeroMgr.heroHash.Values){
			GameObject buffEft = Instantiate(buffEftPrefab) as GameObject;
			buffEft.transform.parent = hero.transform;
			buffEft.transform.localPosition = new Vector3(0f,0f,10f);
			buffEft.transform.localScale = new Vector3(2.5f,2.5f,1f);
			buffEftList.Add(buffEft);
			int tempAct = (int)(hero.realAtk.PHY * (atkPer / 100.0f + 1.0f));
			hero.addBuff("ROCKET30B" + "_" + hero.data.type, time, tempAct, BuffTypes.ATK_PHY, buffFinish);
		}
	}
	
	public void buffFinish(Character character, Buff self){
		foreach(GameObject buffEft in buffEftList){
			Destroy(buffEft);
		}
		buffEftList.Clear();
		
		foreach(Enemy e in EnemyMgr.enemyHash.Values){
			e.addConcentrateFireDelegate -= addConcentrateFireDelegate;
		}
	}
	
	public int addConcentrateFireDelegate(Character c){
		int n = 0;
		foreach(Hero h in HeroMgr.heroHash.Values)
		{
			if(h.targetObj == c.gameObject) n++;
			if(n > 1){
				return rewardHarm;	
			}
		}
		return 0;
	}
}
