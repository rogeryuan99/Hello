using UnityEngine;
using System.Collections;

public class Skill_BUG1 : SkillBase {
	private Object bulletPrefab;
	private Object blastPrefab;
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		
		Character bug = caller.GetComponent<Character>();
		bug.toward(target.transform.position);
		bug.castSkill("SkillA");
		yield return new WaitForSeconds(.95f);
		
		CreateBullet();
	}
	private void CreateBullet(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		
		Character bug = caller.GetComponent<Character>();
		Vector3 endPos = target.transform.position+ new Vector3(0,70,0);
		Vector3 createPt = caller.transform.position
							+ (bug.model.transform.localScale.x > 0
									? new Vector3(80,80,-50)
									: new Vector3(-80,80,-50));
		ShootBullet(createPt, endPos, "RemoveBulletAndShowEft");
	}

	private void ShootBullet (Vector3 creatVc3, Vector3 endVc3, string oncompleteFunStr){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		if(bulletPrefab == null){
			bulletPrefab = Resources.Load("eft/Bug/SkillEft_BUG1_Bullet");
		}
		GameObject bullet = Instantiate(bulletPrefab, creatVc3, transform.rotation) as GameObject;
		GameObject target = parms[2] as GameObject;
		Character enemy = target.GetComponent<Character>();
		float deg = (angle*360)/(2*Mathf.PI) + (enemy.model.transform.localScale.x > 0? 90f: -90f);
		bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(bullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",2000},{ "easetype","linear"},{ 
								"oncomplete",oncompleteFunStr},{ "oncompletetarget",gameObject},{ "oncompleteparams",bullet}});
	}

	private void RemoveBulletAndShowEft(GameObject bullet){
		Destroy(bullet);
		
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character bug = caller.GetComponent<Character>();
		Character enemy = target.GetComponent<Character>();
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BUG1");
		int damage = enemy.getSkillDamageValue(bug.realAtk, ((Effect)def.activeEffectTable["atk_PHY"]).num);
		enemy.realDamage(damage);
		
		if (null == blastPrefab){
			blastPrefab = Resources.Load("eft/Bug/SkillEft_BUG1_Blast");
		}
		GameObject blast = Instantiate(blastPrefab) as GameObject;
		blast.transform.position = target.transform.position + new Vector3(0f, 80f, 1f);
	}
//	
//	protected GameObject SkillEft_STARLOAD1_FireBlast_Prb;
//	protected GameObject fireEftFront;
//	protected GameObject fireEftBehind;
//	
//	public void showEft(Character enemy)
//	{
//		if(SkillEft_STARLOAD1_FireBlast_Prb == null)
//		{
//			SkillEft_STARLOAD1_FireBlast_Prb = Resources.Load("eft/StarLord/SkillEft_STARLOAD1_FireBlast") as GameObject;
//		}  
//		
//		Destroy(fireEftFront);
//		
//		fireEftFront = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject;
//		fireEftFront.transform.parent = enemy.transform;
//		// fireEftFront.transform.localPosition = new Vector3(0,555,-15);
//		fireEftFront.transform.localPosition = new Vector3(0,500,-15);
//		
//		Destroy(fireEftBehind);
//		
//		fireEftBehind = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject; 
//		fireEftBehind.transform.parent = enemy.transform;
//		// fireEftBehind.transform.localPosition = new Vector3(0,555,100);
//		fireEftBehind.transform.localPosition = new Vector3(0,500,100);
//		
//		PackedSprite ps = fireEftFront.GetComponent<PackedSprite>();
//		
//		ps.Color = new Color(ps.Color.r, ps.Color.g,ps.Color.b,0.5f);
//	}
//	
//	public void DestroySkillEft(State self, Character charater)
//	{
//		Debug.LogError("skill_starlod1 DestroySkillEft");
////		charater.characterAI.OnDestroySkillEftObj -= DestroySkillEft;
////		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//		Destroy(fireEftFront);
//		Destroy(fireEftBehind);
//	}
//	
}
