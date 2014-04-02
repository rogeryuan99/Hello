using UnityEngine;
using System.Collections;

public class Druid : Wizard {
	//gwp
	bool  isReducedEnemyDef;
	//gwp end
	public GameObject bearModel;
	public GameObject originalModel;
	
	public bool  isTransfigution;
	public GameObject changeBullet;
	
	public override void Awake (){
		isInvincible = false;
		model = originalModel;
		scaleSize = new Vector3(0.9f, 0.9f, 1);
		base.Awake();
		atkAnimKeyFrame = 14;
	}
	
	public override void Start (){
		base.Start();
		bearModel.SetActiveRecursively(false);
	}
	
	// weapon change test
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("weapon",weaponID);
	}
	
	public void transmutation (){
		Vector3 localScaleX = model.gameObject.transform.localScale;
		model = bearModel;
		originalModel.SetActiveRecursively(false);
		bearModel.SetActiveRecursively(true);
		model.gameObject.transform.localScale = localScaleX;
		setPieceAnima();
		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		hpBar.gameObject.transform.localPosition = new Vector3(hpBar.gameObject.transform.localPosition.x, 269,hpBar.gameObject.transform.localPosition.z);
//		hpBar.gameObject.transform.localPosition.y = 269;
		isTransfigution = true;
	}
	
	public void toHuman (){
		Vector3 localScaleX = model.gameObject.transform.localScale;
		model = originalModel;
		originalModel.SetActiveRecursively(true);
		bearModel.SetActiveRecursively(false);
		model.gameObject.transform.localScale = localScaleX;
		setPieceAnima();
		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		hpBar.gameObject.transform.localPosition = new Vector3(hpBar.gameObject.transform.localPosition.x, 169,hpBar.gameObject.transform.localPosition.z);
//		hpBar.gameObject.transform.localPosition.y = 169;
		if(StaticData.isBattleEnd){
			hideHpBar();
		}
		isTransfigution = false;
		if(!getIsDead()){
			standby();
		}
	}
	
	public override void addHp ( int hpNum  ){
		if(isInvincible && hpNum < 0){
			return;
		}
		realHp+= hpNum;
		realHp = Mathf.Min(realMaxHp,realHp);
		realHp = Mathf.Max(0,realHp);
		showHpBar();
	}
	
	public override void dead (string s=null){
		/*cancelAtk();
		GameObject skmObj = GameObject.Find("skillManager");
		if(skmObj != null && isTransfigution){
			SkillManager skm = skmObj.GetComponent<SkillManager>(); 
			if(skm.druidSK_A != null)
			{
				skm.druidSK_A = Resources.Load("eft/idMasterB_sk2_ef") as GameObject;
			}  
			GameObject eftObj = Instantiate(skm.druidSK_A,this.transform.position+new Vector3(0,80,-5),this.transform.rotation) as GameObject;
			BoneIdMasterA_sk2_ef pieceAnim = eftObj.GetComponent<BoneIdMasterA_sk2_ef>();
			pieceAnim.addFrameScript("Skill",10,deadHandler);
			pieceAnim.playShortAnima();
		}else{
			base.dead();
		}*/
		Debug.LogError("Error Here!!!!");
	}
	
	void deadHandler (string s){
		toHuman();
		base.dead();
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		if(isReducedEnemyDef){
			Enemy enemy = targetObj.GetComponent<Enemy>();
//			enemy.addBuff(SkillLib.instance.getSkillNameByID("DRUID12"),8,enemy.realDef*3/10,BuffTypes.DE_DEF);
		}
		atkAnima();
	}
	
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
//		if(heroData.getPSkillByID("DRUID12") != null){
//			isReducedEnemyDef = true;
//		}
//		if(heroData.getPSkillByID("DRUID7") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("DRUID6")){
//					int cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//					break;
//				}
//			}
//		}
		//end
	}
	
	protected void atkAnima (){
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
			createPt = transform.position + new Vector3(100,140,-50);
		}else{
			createPt = transform.position + new Vector3(-100,140,-50);
		}
		shootBullet(createPt, vc3);
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		if(isTransfigution == false)
		{
			bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		}else
		{
			bltObj = Instantiate(changeBullet,creatVc3, transform.rotation) as GameObject;
		}
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{"z",endVc3.z-10},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject}});
	}
	protected override void removeBullet (){
		Destroy(bltObj);
		if(targetObj != null)
		{
			int dmg;
			Enemy enemy = targetObj.GetComponent<Enemy>();
			
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
	