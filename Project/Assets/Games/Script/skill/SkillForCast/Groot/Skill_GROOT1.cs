using UnityEngine;
using System.Collections;

public class Skill_GROOT1 : SkillBase {

	private GameObject creepingVinesEftOnBodyPrb;
	private GameObject creepingVinesEftOnBody;
	private GameObject creepingVinesBulletPrb;
	private ArrayList creepingVinesObjs;
	
	
	public override void Prepare (ArrayList objs){
		base.Prepare (objs);
//		StartCoroutine(creepingVinesShowEftOnBody(objs[1] as GameObject));
	}
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Groot_Regrowth_1a");
		GRoot heroDoc = caller.GetComponent<GRoot>();
		//heroDoc.toward(target.transform.position);
		heroDoc.castSkill("GROOT1");// Hero.castSkill
		//heroDoc.toward(target.transform.position);
		
		yield return new WaitForSeconds(0.8f);

		GameObject prefab = Resources.Load("gsl_dlg/Groot_Particle") as GameObject;
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = heroDoc.transform;
		go.transform.localPosition = new Vector3(0,1200,-500);
		StartCoroutine(destoryEffectObject(go));

		restoreSelfHp(heroDoc);
//		
//		Vector3 vc3 = target.transform.position+ new Vector3(0,30,0);
//		Vector3 createPt;
//		
//		if(heroDoc.model.transform.localScale.x > 0){
//			createPt = caller.transform.position + new Vector3(80,0,-50);
//		}else{
//			createPt = caller.transform.position + new Vector3(-80,0,-50);
//		}
//		
//		creepingVinesObjs = objs;
//		creepingVinesShootBullet(createPt, vc3, objs);
	}
	
//	private IEnumerator creepingVinesShowEftOnBody(GameObject caller)
//	{
//		yield return new WaitForSeconds(0.5f);
//		if(creepingVinesEftOnBodyPrb == null){
//			creepingVinesEftOnBodyPrb = Resources.Load("eft/Groot/SkillEft_GROOT1_Body") as GameObject;;
//		}
//		
//		Destroy(creepingVinesEftOnBody);
//		
//		creepingVinesEftOnBody = Instantiate(creepingVinesEftOnBodyPrb, caller.transform.position + new Vector3(0, 80, 0), caller.transform.localRotation) as GameObject;
//	}
	
//	private void creepingVinesShootBullet ( Vector3 creatVc3, Vector3 endVc3, ArrayList objs){
//		
//		GameObject caller = objs[1] as GameObject;
//		GameObject target = objs[2] as GameObject;
//		
//		float dis_y = endVc3.y - creatVc3.y;
//		float dis_x = endVc3.x - creatVc3.x;
//		float angle = Mathf.Atan2(dis_y, dis_x);
//		
//		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
//		if(creepingVinesBulletPrb == null){
//			creepingVinesBulletPrb = Resources.Load("eft/Groot/SkillEft_GROOT1_Bullet") as GameObject;
//		}
//		GameObject creepingVinesBullet = Instantiate(creepingVinesBulletPrb,creatVc3, caller.transform.rotation) as GameObject;
//		
//		GRoot heroDoc = caller.GetComponent<GRoot>();
//		if(heroDoc.model.transform.localScale.x > 0){
//			creepingVinesBullet.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
//		}
//		else{
//			creepingVinesBullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
//		}
//		
//		iTween.MoveTo(creepingVinesBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
//								"oncomplete","removeCreepingVinesShootBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",creepingVinesBullet}});
//	}
	
//	private void removeCreepingVinesShootBullet (GameObject creepingVinesBullet){
//		
//		Destroy(creepingVinesBullet);
//		
//		GameObject scene = creepingVinesObjs[0] as GameObject;
//		GameObject caller = creepingVinesObjs[1] as GameObject;
//		GameObject target = creepingVinesObjs[2] as GameObject;
//		
//		GRoot heroDoc = caller.GetComponent<GRoot>();
//		Enemy e = target.GetComponent<Enemy>();
//		HeroData tempHeroData = (heroDoc.data as HeroData);
//		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("GROOT1").number; //GROOT15B
//	
//		int continueTime = (int)tempNumber["time"];
//		int tempAtkPer = (int)tempNumber["AOENum"];
//		int tempAtk = (int)(heroDoc.realAtk*tempAtkPer/100);
//		
//		e.realDamage(tempAtk);
//		e.twineWithSeconds((float)continueTime);
//	}
//	
	private IEnumerator destoryEffectObject(GameObject go)
	{
		yield return new WaitForSeconds(2f);
		Destroy(go);
	}
	
	private void restoreSelfHp(GRoot heroDoc)
	{
		HeroData tempHeroData = heroDoc.data as HeroData;
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("GROOT1").activeEffectTable;	//GROOT1
		float tempHp = ((Effect)tempNumber["hp"]).num;
		int tempSelfHp = (int)(heroDoc.realMaxHp * (tempHp / 100f));
		
		heroDoc.addHp(tempSelfHp);
		
//		heroDoc.realHp = (tempSelfHp > heroDoc.realMaxHp) ? heroDoc.realMaxHp : tempSelfHp;
	}
}
