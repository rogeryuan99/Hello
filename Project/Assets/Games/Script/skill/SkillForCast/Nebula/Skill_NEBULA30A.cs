using UnityEngine;
using System.Collections;

public class Skill_NEBULA30A : SkillBase {
	private Character character;
	private Character enemy;
	private Object bgFirePrefab;

	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		character = caller.GetComponent<Character>();
		enemy = target.GetComponent<Character>();
		
		character.castSkill("Skill30A_a");
		character.toward(enemy.transform.position);
		character.attackAnimaName = "Skill30A_b";
		
		if(character is Nebula){
			Nebula nebula = character as Nebula;
			nebula.showSkill30AHaloEftCallBack += showHaloEft;	
			nebula.showSkill30ABulletEftCallBack += showBulletEft;
		}else if(character is Ch2_Nebula){
			Ch2_Nebula nebula = character as Ch2_Nebula;
			nebula.showSkill30AHaloEftCallBack += showHaloEft;
			nebula.showSkill30ABulletEftCallBack += showBulletEft;
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("NEBULA30A");
		int buffTime = (int)skillDef.buffDurationTime;
		int tempAtk = (int)((Effect)skillDef.activeEffectTable["atk_PHY"]).num;
		int tempAspd = (int)((Effect)skillDef.activeEffectTable["aspd"]).num;
		
		character.addBuff("Skill_NEBULA30A",buffTime,tempAtk,BuffTypes.ATK_PHY,buffFinish);
		character.addBuff("Skill_NEBULA30A",buffTime,tempAspd,BuffTypes.ASPD);
		
		yield return new WaitForSeconds(1f);
		
		createBgFire();
	}
	
	private void showHaloEft(Character c){
		if(c is Nebula){
			Nebula nebula = c as Nebula;
			nebula.showSkill30AHaloEftCallBack -= showHaloEft;
			nebula.showSkill30AStarEftCallBack += showStarEft;
		}else if(c is Ch2_Nebula){
			Ch2_Nebula nebula = c as Ch2_Nebula;
			nebula.showSkill30AHaloEftCallBack -= showHaloEft;
			nebula.showSkill30AStarEftCallBack += showStarEft;
		}
		GameObject haloEftPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA30A_HaloEft") as GameObject;
		GameObject haloEft = Instantiate(haloEftPrefab) as GameObject;
		haloEft.transform.parent = c.transform;
		if(c.model.transform.localScale.x > 0){
			haloEft.transform.localPosition = new Vector3(280,380,-100);
			haloEft.transform.localRotation = Quaternion.Euler(new Vector3(345,168,60));
			haloEft.transform.localScale = new Vector3(100,100,100);
		}else{
			haloEft.transform.localPosition = new Vector3(-280,380,-100);
			haloEft.transform.localRotation = Quaternion.Euler(new Vector3(345,348,60));
			haloEft.transform.localScale = new Vector3(100,100,100);	
		}
	}
	
	private void showStarEft(Character c){
		if(c is Nebula){
			Nebula nebula = c as Nebula;
			nebula.showSkill30AStarEftCallBack -= showStarEft;
		}else if(c is Ch2_Nebula){
			Ch2_Nebula nebula = c as Ch2_Nebula;
			nebula.showSkill30AStarEftCallBack -= showStarEft;
		}
		GameObject starEftPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA30A_StarEft") as GameObject;
		GameObject starEft = Instantiate(starEftPrefab) as GameObject;
		starEft.transform.parent = c.transform;
		starEft.transform.localPosition = new Vector3(0,200,0);
		starEft.transform.localScale = new Vector3(100,100,100);
		
		StartCoroutine(delayDestoryStarEft(starEft));
	}
	
	private IEnumerator delayDestoryStarEft(GameObject starEft){
		yield return new WaitForSeconds(2f);
		Destroy(starEft);
	}
	
	private void buffFinish(Character character, Buff self){
		//character.attackAnimaName = "Attack";
		if(character is Nebula){
			Nebula nebula = character as Nebula;
			nebula.attackAnimaName = "Attack";
		}else if(character is Ch2_Nebula){
			Ch2_Nebula nebula = character as Ch2_Nebula;
			nebula.attackAnimaName = "Attack";
		}
	}
	
	private void showBulletEft(Character c){
		Attack(character,enemy);
	}
	
	private void Attack(Character caller,Character target){
		if(caller.getIsDead()) return;
		if(!target.getIsDead()){
			createBullets();
			createGunFire();	
		}
	}
	
	private void createBullets(){
		if(character == null) return;
		bool isRightSide = character.model.transform.localScale.x > 0;
		Vector3 startPos = character.transform.position + (isRightSide? new Vector3(138f,5f,-50f): new Vector3(-138f,5f,-50f));
		Vector3 endPos = enemy.transform.position+ new Vector3(Random.Range(-30f,30f),Random.Range(40f, 100f),0);
		
		shootFireBullet(startPos, endPos, "removeBullet");
	}
	
	private void shootFireBullet (Vector3 creatVc3, Vector3 endVc3, string oncompleteFunStr){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		GameObject bulletPrefab = Resources.Load("eft/Nebula/Iromman_Attack_Bullet") as GameObject;
		GameObject bullet = Instantiate(bulletPrefab,creatVc3, transform.rotation) as GameObject;
		float deg = (angle*360)/(2*Mathf.PI);
		bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		bullet.transform.localScale = new Vector3(0.064f,0.064f,1f);
		iTween.MoveTo(bullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",2000},{ "easetype","linear"},{ 
								"oncomplete",oncompleteFunStr},{ "oncompletetarget",gameObject},{ "oncompleteparams",bullet}});
	}
	
	private void removeBullet (GameObject bullet){
		Destroy(bullet);
	}
	
	private void createGunFire(){
		bool isRightSide = character.model.transform.localScale.x > 0;
		Vector3 createPt = character.transform.position + (isRightSide? new Vector3(138f,5f,0f): new Vector3(-138f,5f,0f));

		GameObject gunFirePrefab = Resources.Load("eft/Nebula/gunfire") as GameObject;
		GameObject gunFire = Instantiate(gunFirePrefab, 
											createPt, 
											Quaternion.Euler(new Vector3(0,0,character.model.transform.localScale.x > 0? 0:180))
										) as GameObject;
	}
	
	private void createBgFire(){
		if (null == bgFirePrefab){
			bgFirePrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA30A_BgFire");
		}
		GameObject fire = Instantiate(bgFirePrefab) as GameObject;
		fire.transform.position = character.transform.position;
	}
}
