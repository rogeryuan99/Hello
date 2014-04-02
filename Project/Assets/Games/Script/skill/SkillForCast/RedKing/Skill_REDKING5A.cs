using UnityEngine;
using System.Collections;

public class Skill_REDKING5A : SkillBase {
	private Object shootEftPrefab;
	private Object shootBulletPrefab;
	private ArrayList objs;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		redking.toward(target.transform.position);
		redking.castSkill("Skill5A");
		yield return new WaitForSeconds(1.15f);
		createShootEft();
		createShootBullet();
	}
	
	private void createShootEft(){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		if (null == shootEftPrefab){
			shootEftPrefab = Resources.Load("eft/RedKing/SkillEft_Redking5A_Shoot_Eft");
		}
		GameObject shootEft = Instantiate(shootEftPrefab) as GameObject;
		bool isLeftSide = redking.model.transform.localScale.x > 0;
		shootEft.transform.localScale = new Vector3(isLeftSide? 1f:-1f, 1f, 1f);
		shootEft.transform.position = caller.transform.position + new Vector3(isLeftSide? 143: -143f, isLeftSide? 56f: 56f, 0f);
	}
	
	private void createShootBullet(){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character redking   = caller.GetComponent<Character>();
		bool isLeftSide = redking.model.transform.localScale.x > 0;
		
		Vector3 vc3 = target.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;

		createPt = caller.transform.position + new Vector3(isLeftSide?180:-180,52,1);
			
		shootFireBullet(createPt, vc3, "removeBullet");
	}
	
	private void shootFireBullet (Vector3 creatVc3, Vector3 endVc3, string oncompleteFunStr){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		if (null == shootBulletPrefab){
			shootBulletPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing5A_Shoot_Bullet");
		}
		GameObject fireBullet = Instantiate(shootBulletPrefab,creatVc3, transform.rotation) as GameObject;
		fireBullet.transform.localScale = new Vector3(.75f, .75f, .75f);
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
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		Character enemy   = target.GetComponent<Character>();
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("REDKING5A");
		int damage = enemy.getSkillDamageValue(redking.realAtk, ((Effect)skillDef.activeEffectTable["atk_PHY"]).num);
		enemy.realDamage(damage);	
	}
}
