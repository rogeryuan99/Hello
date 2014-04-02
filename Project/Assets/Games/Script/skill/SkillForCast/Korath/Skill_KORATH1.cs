using UnityEngine;
using System.Collections;

public class Skill_KORATH1 : SkillBase {

private GameObject fireBulletPrb;
	private GameObject gunFirePrefab;
	private GameObject bulletPrefab;
	private GameObject hitEffectPrefab;
	private int damage;
	protected int time;
	private GameObject weapon;
	
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		
		Character character = caller.GetComponent<Character>();
		character.toward(target.transform.position);
		character.castSkill("SkillA");
		
		if(character is Korath)
		{
			weapon = GameObject.Find("Korath/MEDIUM_Weapon_01");
		}
		else
		{
			weapon = GameObject.Find("enemyMediumKorathBase/MEDIUM_Weapon_01");
		}
							
		SkillDef skillDef =  SkillLib.instance.getSkillDefBySkillID("KORATH1");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		
		HeroData heroData = character.data as HeroData;
		Hashtable passiveTable = heroData.getPSkillByID("KORATH10");
		if(null != passiveTable){
			SkillDef passiveSkillDef = SkillLib.instance.getSkillDefBySkillID("KORATH10");
			int passiveDamage = (int)passiveSkillDef.passiveEffectTable["atk_damage"];
			tempAtkPer *= (1+passiveDamage/100f);
		}
		
		damage = target.GetComponent<Character>().getSkillDamageValue(character.realAtk, tempAtkPer);
		time = (int)skillDef.skillDurationTime;
		
		yield return new WaitForSeconds(.565f);
		MusicManager.playEffectMusic("SFX_Korath_Burst_Fire_1a");
		Attack(character, target.GetComponent<Character>());
		yield return new WaitForSeconds(.3f);
		Attack(character, target.GetComponent<Character>());
		yield return new WaitForSeconds(.2f);
		Attack(character, target.GetComponent<Character>());
	}
	
	private void Attack(Character call, Character target)
	{
		if(call.getIsDead())
		{
			return;
		}
	
		if (false == target.getIsDead()){
			//MusicManager.playEffectMusic("SFX_enemy_range_attack_1a");
			createBullets();
			createGunFire();
		}
	}
	
	private void createGunFire(){
		GameObject caller = parms[1] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		bool isRightSide = character.model.transform.localScale.x > 0;
		Vector3 createPt = weapon.transform.position + (isRightSide? new Vector3(138f,5f,0f): new Vector3(-138f,5f,0f));
		
		if(gunFirePrefab == null){
			gunFirePrefab = Resources.Load("eft/Korath/SkillEft_KORATH1_GunFire") as GameObject;
		}
		GameObject gunFire = Instantiate(gunFirePrefab, 
											createPt, 
											Quaternion.Euler(new Vector3(0,0,character.model.transform.localScale.x > 0? 0:180))
										) as GameObject;
	}
	
	private void createBullets(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		if(character == null)
		{
			return;
		}
		bool isRightSide = character.model.transform.localScale.x > 0;
		Vector3 endPos = target.transform.position+ new Vector3(Random.Range(-30f,30f),Random.Range(40f, 100f),0);
		Vector3 createPos = weapon.transform.position + (isRightSide? new Vector3(138f,5f,-50f): new Vector3(-138f,5f,-50f));
		
		shootFireBullet(createPos, endPos, "removeBullet");
	}
	
	private void shootFireBullet (Vector3 creatVc3, Vector3 endVc3, string oncompleteFunStr){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		// StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		if(bulletPrefab == null){
			bulletPrefab = Resources.Load("eft/Korath/SkillEft_KORATH1_Bullet") as GameObject;
		}
		GameObject bullet = Instantiate(bulletPrefab,creatVc3, transform.rotation) as GameObject;
		float deg = (angle*360)/(2*Mathf.PI);
		bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(bullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",2000},{ "easetype","linear"},{ 
								"oncomplete",oncompleteFunStr},{ "oncompletetarget",gameObject},{ "oncompleteparams",bullet}});
	}
	
	private void removeBullet (GameObject bullet){
		GameObject target = parms[2] as GameObject;
		
		Character character = target.GetComponent<Character>();
		character.realDamage(damage);
		
		createHitEffect(bullet);
		Destroy(bullet);
	}
	
	private void createHitEffect(GameObject bullet){
		GameObject caller = parms[1] as GameObject;
		
		if(hitEffectPrefab == null){
			hitEffectPrefab = Resources.Load("eft/Korath/SkillEft_Korath1_HitEft") as GameObject;
		}
		GameObject hitEffect = Instantiate(hitEffectPrefab, 
											bullet.transform.position, 
											transform.rotation
										) as GameObject;
		hitEffect.transform.localScale = new Vector3(Random.Range(.3f, .6f), Random.Range(.7f, .9f), 1f);
		hitEffect.transform.Rotate(new Vector3(Random.Range(0f, 45f), Random.Range(0f, 45f), Random.Range(0f, 45f)));
	}
	
//	
//	private void removeBulletAndShowEft (GameObject fireBullet)
//	{
//		Destroy(fireBullet);
//		if(fireBulletTarget == null)
//		{
//			return;
//		}
//		Character enemy = fireBulletTarget.GetComponent<Character>();
//		enemy.realDamage(damage);
//		
//		enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//		enemy.fireWithSeconds(time);
//		
//		showEft(enemy);
//	}
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
//	public void DestroySkillEft(Character charater)
//	{
//		Debug.LogError("skill_starlod1 DestroySkillEft");
////		charater.characterAI.OnDestroySkillEftObj -= DestroySkillEft;
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//		Destroy(fireEftFront);
//		Destroy(fireEftBehind);
//	}
}
