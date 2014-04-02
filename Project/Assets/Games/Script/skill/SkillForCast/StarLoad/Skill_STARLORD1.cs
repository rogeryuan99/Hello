using UnityEngine;
using System.Collections;

public class Skill_STARLORD1 : SkillBase {

	private GameObject fireBulletPrb;
	private GameObject smokePrb;
	private GameObject fireBulletTarget;
	private int fireBlastDamage;
	protected int time;
	protected StarLord starlord;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		fireBulletTarget = target;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		starlord = caller.GetComponent<StarLord>();
		
		if (heroDoc.model.transform.position.x > target.transform.position.x)
		{
			heroDoc.model.transform.localScale = new Vector3 (-heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1);
		} 
		else
		{
			heroDoc.model.transform.localScale = new Vector3 (heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1);
		}
		
		heroDoc.castSkill("SkillA");
		yield return new WaitForSeconds(.80f);
		MusicManager.playEffectMusic("SFX_StarLord_Fire_Blast_1a");
//		yield return new WaitForSeconds(.01f);
//		MusicManager.playEffectMusic("SFX_StarLord_Fire_Blast_1a");
		yield return new WaitForSeconds(.3f);
					
		SkillDef skillDef =  SkillLib.instance.getSkillDefBySkillID("STARLORD1");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		fireBlastDamage = target.GetComponent<Character>().getSkillDamageValue(heroDoc.realAtk, tempAtkPer);
		time = (int)skillDef.skillDurationTime;
		
		
		createBullets(objs);
		createSmoke(objs);
	}
	private void createBullets(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		Vector3 vc3 = target.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(heroDoc.model.transform.localScale.x > 0){
			createPt = caller.transform.position + new Vector3(80,80,-50);
		}else{
			createPt = caller.transform.position + new Vector3(-80,80,-50);
		}
			
		shootFireBullet(createPt, vc3, "removeBullet");
		
		
		if(heroDoc.model.transform.localScale.x > 0){
			createPt = caller.transform.position + new Vector3(60,70,-50);
		}else{
			createPt = caller.transform.position + new Vector3(-60,70,-50);
		}
		shootFireBullet(createPt, vc3, "removeBulletAndShowEft");
	}
	
	private void createSmoke(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		Vector3 createPt;
		Vector3 vc3 = target.transform.position+ new Vector3(0,70,0);
		if(heroDoc.model.transform.localScale.x > 0){
			createPt = caller.transform.position + new Vector3(80,80,-50);
		}else{
			createPt = caller.transform.position + new Vector3(-80,80,-50);
		}
		createSmoke(createPt, vc3);
		
		if(heroDoc.model.transform.localScale.x > 0){
			createPt = caller.transform.position + new Vector3(60,70,-50);
		}else{
			createPt = caller.transform.position + new Vector3(-60,70,-50);
		}
		createSmoke(createPt, vc3);
	}
	
	private void createSmoke(Vector3 creatVc3, Vector3 endVc3){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		if(smokePrb == null){
			smokePrb = Resources.Load("eft/StarLord/SkillEft_STARLOAD1_Smoke") as GameObject;
		}
		GameObject smoke = Instantiate(smokePrb,creatVc3, transform.rotation) as GameObject;
		float deg = (angle*360)/(2*Mathf.PI);
		smoke.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
	}
	
	private void shootFireBullet (Vector3 creatVc3, Vector3 endVc3, string oncompleteFunStr){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		if(fireBulletPrb == null)
		{
			fireBulletPrb = Resources.Load("eft/StarLord/SkillEft_STARLOAD1_FireBullet") as GameObject;
		}
		GameObject fireBullet = Instantiate(fireBulletPrb,creatVc3, transform.rotation) as GameObject;
		// fireBullet.transform.localScale = new Vector3(0.7f,1.4f,1);
		// float deg = (angle*360)/(2*Mathf.PI) + 90;
		float deg = (angle*360)/(2*Mathf.PI);
		fireBullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(fireBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",2000},{ "easetype","linear"},{ 
								"oncomplete",oncompleteFunStr},{ "oncompletetarget",gameObject},{ "oncompleteparams",fireBullet}});
	}
	
	
	private void removeBullet (GameObject fireBullet)
	{
		Destroy(fireBullet);
		
	}
	
	private void removeBulletAndShowEft (GameObject fireBullet)
	{
		Destroy(fireBullet);
		if(fireBulletTarget == null)
		{
			return;
		}
		Character enemy = fireBulletTarget.GetComponent<Character>();
		
		
//		enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//		enemy.fireWithSeconds();
		starlord.burningEnemy(time,enemy);
		enemy.realDamage(fireBlastDamage);
		
	}
	
	
}
