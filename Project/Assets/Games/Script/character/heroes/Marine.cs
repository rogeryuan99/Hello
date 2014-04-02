using UnityEngine;
using System.Collections;

public class Marine : Wizard {
	Enemy[] shotgunTargets;
	GameObject[] bulletObjects;
	int targetCount = 0;
	int maxBullets = 60;
	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 10;
		usingShotgun = false;
		shotgunTargets = new Enemy[maxBullets];
		bulletObjects = new GameObject[maxBullets];
	}
	// weapon change test
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("weapon",weaponID);
	}
	
	
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		if(targetObj == null)
		{
			return;
		}
		Enemy enemy = targetObj.GetComponent<Enemy>();
		if(enemy.getIsDead())
		{
			return;
		}
		Vector3 createPt = Vector3.zero;
		if (usingShotgun == false) {
			
			Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
			
			if(model.transform.localScale.x > 0)
			{
				createPt = transform.position + new Vector3(20,45,-50);
			}else{
				createPt = transform.position + new Vector3(-20,45,-50);
			}
			shootBullet(createPt, vc3);
		}
		else {
			int range = 800;
			targetCount = 0;
			if(model.transform.localScale.x > 0)
			{
				createPt = transform.position + new Vector3(20,45,-50);
			}else{
				createPt = transform.position + new Vector3(-20,45,-50);
			}
			
			foreach(string key in EnemyMgr.enemyHash.Keys){
				Enemy en = EnemyMgr.enemyHash[key] as Enemy;
				
				if(Vector3.Distance(en.gameObject.transform.position, targetObj.transform.position) <= range){
					if ((!en.isDead) && (isInShotgunRange(en.gameObject))) {
						shootShell(createPt, en.gameObject.transform.position + new Vector3(0, 70, 0), targetCount);
						shotgunTargets[targetCount] = en;
						targetCount++;
					}
				}
			}
		}
	}
	
	private bool isInShotgunRange ( GameObject enemyObject  ){
		 return true;
		
		Vector3 targetDir= enemyObject.transform.position - transform.position;
	    Vector3 forward= targetObj.transform.position - transform.position;
	    float angle= Vector3.Angle(targetDir, forward);
	    return (angle < 20.0f);
	}
	
	protected void shootShell ( Vector3 creatVc3 ,   Vector3 endVc3 ,   int targetOffset  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		Vector3 atkEftPos;
		GameObject atkEftObj;
		if(model.transform.localScale.x > 0)
		{
			atkEftPos = transform.position + new Vector3(130, 55,-10);
			atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
		}else{
			atkEftPos = transform.position + new Vector3(-130, 55,-10);
			atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale += new Vector3(-1.3f, 0,0);
//			atkEftObj.transform.localScale.x = -1.3f;
		}
		
		bulletObjects[targetOffset] = Instantiate(bulletPrb,creatVc3, transform.rotation) as  GameObject;
		bulletObjects[targetOffset].transform.localScale = new Vector3(2,2,0);
		
		float deg = (angle*360)/(2*Mathf.PI);
		bulletObjects[targetOffset].transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bulletObjects[targetOffset].transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bulletObjects[targetOffset],new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeShell"},{ "oncompletetarget",gameObject},{ "oncompleteparams",targetOffset}});
	}
	
	protected void removeShell ( int shellOffset  ){
		GameObject HitEftObj = Instantiate(HitEft, bulletObjects[shellOffset].transform.position, transform.rotation) as GameObject;//+Vector3(0,100,-50),
		Destroy(bulletObjects[shellOffset]);
		bulletObjects[shellOffset] = null;
		Enemy enemy = null;
		if(shotgunTargets[shellOffset] != null)
		{
			enemy = shotgunTargets[shellOffset];
			if (enemy.isDead) {
				shotgunTargets[shellOffset] = null;
				return;
			}
			HitEftObj.transform.parent = shotgunTargets[shellOffset].gameObject.transform;
			//add by xiaoyong for critical strike
			int dmg;
			// delete by why 2014.2.7
//			if( StaticData.computeChance((int)realCStk*100, 100) )
//			{
//				(data as HeroData).critValue = realAtk*2;
//				criticalHandler();
//				dmg = enemy.defenseAtk((data as HeroData).critValue, this.gameObject);
//				if(!cstkAnimPrb)
//				{
//					cstkAnimPrb = Resources.Load("eft/CritEft") as GameObject;
//				}
//				GameObject cstkAnimObj = Instantiate(cstkAnimPrb, targetObj.transform.position+new Vector3(0,55,-1),this.gameObject.transform.rotation) as GameObject;
//			}
//			else
//			{
				dmg = enemy.defenseAtk(realAtk, this.gameObject);
//			}
			trinketEfts(dmg);
			
			shotgunTargets[shellOffset] = null;
		}else{
			if(enemy.getIsDead()){
				standby();
			}
		}
		
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		Vector3 atkEftPos;
		GameObject atkEftObj;
		if(model.transform.localScale.x > 0)
		{
			atkEftPos = transform.position + new Vector3(130, 55,-10);
			atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
		}else{
			atkEftPos = transform.position + new Vector3(-130, 55,-10);
			atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale += new Vector3(-1.3f, 0,0);
//			atkEftObj.transform.localScale.x = -1.3f;
		}
		
		bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject}});
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		HeroData heroData = data as HeroData;
		if( heroData.getPSkillByID("MARINE5") != null){
			if(StaticData.computeChance(10,100)){
				Enemy enemy = atkerObj.GetComponent<Enemy>();
				enemy.fearWithSeconds();
			}
		}
		return base.defenseAtk(damage, atkerObj);
	}
	
	public override void criticalHandler ()
	{
//		HeroData heroData = data as HeroData;
//		if( heroData.getPSkillByID("MARINE3") != null){
//			heroData.critValue += heroData.critValue*15/100;
//		}
	}
}
