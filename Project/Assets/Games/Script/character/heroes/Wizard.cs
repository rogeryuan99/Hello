using UnityEngine;
using System.Collections;

public class Wizard : Hero {

	public GameObject bulletPrb;
	public GameObject atkEft;
	protected GameObject bltObj;
	public GameObject HitEft;
	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
	}
	
	//override
	public override void moveToTarget ( GameObject obj  ){
		if( targetObj )
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		startAtk();
	}
	
	// weapon change test
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("armDownR",weaponID);
	}
	
	//override
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		if(targetObj == null)
		{
			return;
		}
		Enemy enemy = targetObj.GetComponent<Enemy>();
		if(enemy.getIsDead())
		{
			return;
		}
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = transform.position + new Vector3(20,40,-50);
		}else{
//			print("left");
			createPt = transform.position + new Vector3(-20,40,-50);
		}
		shootBullet(createPt, vc3);
	}
	
	public override bool  checkOpponent(){
		if(StaticData.isBattleEnd){
			cancelCheckOpponent();
			return false;
		}
		if(state != MOVE_STATE  && state != CAST_STATE){
			if(targetObj != null && !isDead)
			{
				startAtk();
			}else{
				return base.checkOpponent();
			}
		}
		return false;
	}
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);

//		SkillData tempSKD = null;
//		int cd = 0;
//		//gwp passiveLis
//		if(heroData.getPSkillByID("WIZARD9") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//					tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("WIZARD6")){
//					cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//					break;
//				}
//			}
//		}
//		if(heroData.getPSkillByID("WIZARD12") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//					tempSKD = heroData.skillListBattle[i] as SkillData;
//					cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -5 == 0? 0 : cd -5);
//			}
//		}
		//end
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(data.type == HeroData.WIZARD)
		{
			Vector3 atkEftPos= transform.position + new Vector3(0, 50,-10);
			GameObject atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale = this.model.transform.localScale;
		}
		bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject}});
	}
	
	protected virtual void removeBullet (){
		GameObject HitEftObj = null;
		if(HitEft)
		{
			HitEftObj = Instantiate(HitEft, bltObj.transform.position, transform.rotation) as GameObject;//+Vector3(0,100,-50),
		}
		
		Destroy(bltObj);
		if(targetObj != null)
		{
			Enemy enemy = targetObj.GetComponent<Enemy>();
			if(HitEftObj != null)
			{
				HitEftObj.transform.parent = targetObj.transform;
			}
			//add by xiaoyong for critical strike
			int dmg;
			
			// delete by why 2014.2.7
//			if( StaticData.computeChance((int)realCStk*100, 100) )
//			{
//				(data as HeroData).critValue = realAtk*2;
//				criticalHandler();
//				
//				if(!cstkAnimPrb)
//				{
//					cstkAnimPrb = Resources.Load("eft/CritEft") as GameObject;
//				}
//				GameObject cstkAnimObj = Instantiate(cstkAnimPrb, targetObj.transform.position+new Vector3(0,50,-1),this.gameObject.transform.rotation) as GameObject;
//				dmg = enemy.defenseAtk((data as HeroData).critValue, this.gameObject);
//			}
//			else
//			{
				dmg = enemy.defenseAtk(realAtk, this.gameObject);
//			}
			trinketEfts(dmg);
			
		}else{
			standby();
		}
	}
	
}
