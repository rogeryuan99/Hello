using UnityEngine;
using System.Collections;

public class Skill_BUG15A : SkillBase {
	
	private Object preparePrefab;
	private Object fireEffectPrefab;
	protected GameObject bulletPrb;
	protected GameObject hitEftPrb;
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character bug   = caller.GetComponent<Character>();
		Character enemy = target.GetComponent<Character>();
		
		bug.toward(target.transform.position);
		bug.castSkill("Skill15A");
		
		yield return new WaitForSeconds(.35f);
		StartCoroutine(CreatePrepareEffect());
		yield return new WaitForSeconds(1f);
		StartCoroutine(CreateFireEffect());
		// create bullet 
		// - hitEffect
	}
	
	private IEnumerator CreatePrepareEffect(){
		GameObject caller = parms[1] as GameObject;
		Character bug = caller.GetComponent<Character>();
		
		if (null == preparePrefab){
			preparePrefab = Resources.Load("eft/Bug/SkillEft_BUG15A_PrepareEffect");
		}
		GameObject eft = Instantiate(preparePrefab) as GameObject;		
		bool isLeftSide = bug.model.transform.localScale.x > 0;
		eft.transform.position = caller.transform.position + new Vector3((isLeftSide?-30f:30f), 125f, -1f);
		
		yield return new WaitForSeconds(.8f);
		Destroy(eft);
	}
	
	private IEnumerator CreateFireEffect(){
		GameObject caller = parms[1] as GameObject;
		GameObject targetObj = parms[2] as GameObject;
		Character bug = caller.GetComponent<Character>();
		
		if (null == fireEffectPrefab){
			fireEffectPrefab = Resources.Load("eft/Bug/SkillEft_BUG15A_FireEffect");
		}
		GameObject eft = Instantiate(fireEffectPrefab) as GameObject;
		bool isLeftSide = bug.model.transform.localScale.x > 0;
		eft.transform.position = caller.transform.position + new Vector3((isLeftSide?50f:-50f), 50f, -1f);
		eft.transform.localScale = new Vector3((isLeftSide?1:-1), 1, 1);
		
		
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(bug.model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = caller.transform.position + new Vector3(80,40,-50);
		}else{
//			print("left");
			createPt = caller.transform.position + new Vector3(-80,40,-50);
		}
		shootBullet(createPt, vc3);
		
		
		yield return new WaitForSeconds(1f);
		Destroy(eft);
		
		
	}
	
	protected void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		GameObject caller = parms[1] as GameObject;
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(bulletPrb == null)
		{
			bulletPrb = Resources.Load("eft/Bug/SkillEft_BUG15A_Bullet") as GameObject;
		}
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, caller.transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected void removeBullet (GameObject bltObj)
	{
		GameObject caller = parms[1] as GameObject;
		GameObject targetObj = parms[2] as GameObject;

		Destroy(bltObj);
		if(targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			GameObject hitEft = null;
		
			if(hitEftPrb == null)
			{
				hitEftPrb = Resources.Load("eft/Bug/SkillEft_BUG15A_HitEffect") as GameObject;
			}
			
			if(hitEft == null)
			{
				hitEft = Instantiate(hitEftPrb, bltObj.transform.position, caller.transform.rotation) as GameObject;
			}
			
			StartCoroutine(desHitEft(hitEft));
			
			if(caller.GetComponent<Character>().model.transform.localScale.x < 0)
			{
				hitEft.transform.localScale = new Vector3
					(
						-hitEft.transform.localScale.x,
						hitEft.transform.localScale.y,
						hitEft.transform.localScale.z
					);
			}
			
			if(StaticData.computeChance(40, 100))
			{
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BUG15A");
				target.addAbnormalState(skillDef.skillDurationTime, null, Character.ABNORMAL_NUM.LAYDOWN);
			}
		}
	}
	
	protected IEnumerator desHitEft(GameObject hitEft)
	{
		yield return new WaitForSeconds(0.8f);
		Destroy(hitEft);
	}
}
