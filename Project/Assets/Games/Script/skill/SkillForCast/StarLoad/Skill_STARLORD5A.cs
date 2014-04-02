using UnityEngine;
using System.Collections;

public class Skill_STARLORD5A : SkillBase {

	private GameObject windBulletPrb;
	private GameObject target;
	// private int fireBlastDamage;
	protected int time;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_StarLord_Wind_Blast_1a");
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.model.transform.localScale = heroDoc.model.transform.position.x > target.transform.position.x?
													new Vector3 (-heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1):
													new Vector3 (heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1);
		
		heroDoc.castSkill("Skill5A");
		yield return new WaitForSeconds(1.3f);
			
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD5A");
		
		time = skillDef.skillDurationTime;
		
		Vector3 vc3 = target.transform.position+ new Vector3(0,70,0);
		Vector3 createPt = heroDoc.model.transform.localScale.x > 0?
								caller.transform.position + new Vector3(80,40,-50):
								caller.transform.position + new Vector3(-80,40,-50);
			
		shootFireBullet(createPt, vc3);
	}
	
	private void shootFireBullet ( Vector3 creatVc3, Vector3 endVc3){
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		if(windBulletPrb == null){
			windBulletPrb = Resources.Load("eft/StarLord/SkillEft_STARLOAD1_WindBlast") as GameObject;
		}
		GameObject windBullet = Instantiate(windBulletPrb,creatVc3, transform.rotation) as GameObject;
		iTween.MoveTo(windBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1000},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",windBullet}});
		
	}
	
	public GameObject windBulletObj;
	
	private void removeBullet (GameObject windBullet)
	{
		
		if(target == null)
		{
			Destroy(windBullet);
			return;
		}
		this.windBulletObj = windBullet;
		Character enemy = target.GetComponent<Character>();
		
//		enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		// enemy.realDamage(fireBlastDamage);
//		enemy.twistWithSeconds();
		
		State state = new State(time, DestroySkillEft);
		
		enemy.addAbnormalState(state, Character.ABNORMAL_NUM.TWIST);
	}
	
	protected void DestroySkillEft(State s, Character charater)
	{
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		Destroy(this.windBulletObj);
	}
}
