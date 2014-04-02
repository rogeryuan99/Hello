using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL5A : SkillBase {

	private ArrayList parms;
	private Character bill;
	private Character enemy;
	private Object weaponPrefab;
	private Object bulletPrefab;
	private float damage;
	private bool isTowardRight;
	
	
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		LoadResources();
		
		bill.toward(enemy.transform.position);
		bill.castSkill("Skill5A");
		isTowardRight = bill.model.transform.localScale.x > 0;
		yield return new WaitForSeconds(1.07f);
		CreateWeapon();
		yield return new WaitForSeconds(.47f);
		CreateBullet();
		// Time.timeScale = 0.01f;
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		bill = caller.GetComponent<Character>();
		enemy = target.GetComponent<Character>();
		
		if (null == weaponPrefab){
			weaponPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL5A_RotationWeapon");
		}
		if (null == bulletPrefab){
			bulletPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL5A_Bullet");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL5A");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
	}
	
	private void CreateWeapon(){
		GameObject weapon = Instantiate(weaponPrefab, bill.transform.position, transform.rotation) as GameObject;
		weapon.transform.position += new Vector3((isTowardRight? -65f: 65f), 135f, -1f);
	}
	
	private void CreateBullet(){
		Vector3 starPos = bill.transform.position  + new Vector3((isTowardRight? 170f: -170f), 125f, -1f);
		Vector3 endPos  = enemy.transform.position + new Vector3((isTowardRight? -40f: 40f), Random.Range(30f, 80f), -1f);
		Vector3 difV3   = endPos - starPos;
		float   angle   = Mathf.Atan2(difV3.y, difV3.x);
		float deg = (angle*360)/(2*Mathf.PI) - 90f;
		
		GameObject bullet = Instantiate(bulletPrefab, starPos, transform.rotation) as GameObject;
		bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(bullet,new Hashtable(){{"x",endPos.x},{ "y",endPos.y},{ "speed",2500},{ "easetype","linear"},{ 
								"oncomplete","HitTarget"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bullet}});
	}
	
	private void HitTarget (GameObject bullet)
	{
		Destroy(bullet);
		enemy.realDamage(enemy.getSkillDamageValue(bill.realAtk, damage));
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.3f, 0f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
	}
}
