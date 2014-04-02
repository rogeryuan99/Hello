using UnityEngine;
using System.Collections;

public class Skill_ROCKET5A : SkillBase {

private GameObject rocketLauncherMissilePrb;
	private ArrayList rocketLauncherObjs = null;
	private GameObject grenadeEft;
	private GameObject rocketLauncherAttackEftPrb;
	private GameObject rocketLauncherAttackEft;
	private GameObject rocketLauncherExplosionPrb;
	private GameObject rocketLauncherExplosion;
	private float cumulateTime = 0;
	private bool isGrenadeAppear = false;
	private Character enemy;
	
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		enemy = target.GetComponent<Character>();
		
		MusicManager.playEffectMusic("SFX_Rocket_Rocket_Shot_First_1a");
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("Skill5A");// Hero.castSkill
		
		yield return new WaitForSeconds(1.2f);
		
		StartCoroutine(RocketLauncherShootMissile(objs));
	}
	
	private IEnumerator RocketLauncherShootMissile(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		rocketLauncherObjs = objs;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		GameObject bigBangEftPrefab = Resources.Load("gsl_dlg/Rocket5A") as GameObject;
		GameObject bigBangEft = Instantiate(bigBangEftPrefab) as GameObject;
		bigBangEft.transform.parent = target.transform;
		bigBangEft.transform.position = target.transform.position+ new Vector3(0,70,-200);
		
		GameObject grenadePrefab = Resources.Load("gsl_dlg/Rocket_Antitank_grenade") as GameObject;
		grenadeEft = Instantiate(grenadePrefab) as GameObject;
		isGrenadeAppear = true;

		RocketLauncherShowExplosion();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET5A");
		float tempTime = skillDef.buffDurationTime;
		
		yield return new WaitForSeconds(tempTime);
		isGrenadeAppear = false;
		Destroy(grenadeEft);
	}
	
	private void RocketLauncherShowExplosion(){
		GameObject scene = rocketLauncherObjs[0] as GameObject;
		GameObject caller = rocketLauncherObjs[1] as GameObject;
		GameObject target = rocketLauncherObjs[2] as GameObject;

		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET5A");
		Hashtable tempNumber = skillDef.buffEffectTable;	
		float tempTime = skillDef.buffDurationTime;
		float defendPer = ((Effect)tempNumber["def_PHY"]).num;
		float tempAct = enemy.realDef.PHY*(-defendPer/100);
		enemy.addBuff("Skill_ROCKET5A",(int)tempTime,tempAct,BuffTypes.DEF_PHY);
	}
	
	void Update(){
		if(enemy==null || enemy.isDead){
			isGrenadeAppear = false;
			Destroy(grenadeEft);	
			return;
		}
		if(isGrenadeAppear){
			cumulateTime += Time.deltaTime;
			float angle = Mathf.Lerp(0,720, .5f +.5f*Mathf.Sin(cumulateTime));
			float a = 50f;
			float speed = 5f;
			
			GameObject target = rocketLauncherObjs[2] as GameObject;
			grenadeEft.transform.position = new Vector3(target.transform.position.x+a*Mathf.Sin(cumulateTime*speed),
																				target.transform.position.y+a*Mathf.Cos(cumulateTime*speed)+140f,
																				target.transform.position.z);
		}
	}
}
