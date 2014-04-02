using UnityEngine;
using System.Collections;

public class Exxo : Hero {
	//gwp
	bool  isReducedEnemyDef;
	bool  isResetSkill;
	bool  isCritEft;
	public GameObject attackEft;
	//gwp end

	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
		isReducedEnemyDef = false;
		isResetSkill = false;
		isCritEft = false;
	}
	// weapon change test
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("weapon",weaponID);
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		if(isResetSkill){
//			if(StaticData.computeChance(5,100)){
//				HeroData heroData = data as HeroData;
//				for(int i=0; i<heroData.skillListBattle.Count; i++){
//					SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//					tempSKD.reset();
//				}
//			}
		}
		return base.defenseAtk(damage, atkerObj);
	}
	
	public override void criticalHandler (){
//		if(isCritEft){
//			addHp( (data as HeroData).critValue/2);
//		}
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("atk_tank");
		Vector3 eft;
		if(model.transform.localScale.x > 0)
		{
			eft = transform.position + new Vector3(70,80,-50);
		}else{
			eft = transform.position + new Vector3(-70,80,-50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(isReducedEnemyDef){
			Enemy enemy = targetObj.GetComponent<Enemy>();
//			enemy.addBuff(SkillLib.instance.getSkillNameByID("TANK7"),8,enemy.realDef/10,BuffTypes.DE_DEF);
		}
		base.atkAnimaScript("");
	}
	
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
//		if(heroData.getPSkillByID("TANK7") != null){
//			isReducedEnemyDef = true;
//		}
//		if(heroData.getPSkillByID("TANK8") != null){
//			isResetSkill = true;
//		}
//		if(heroData.getPSkillByID("TANK12") != null){
//			isCritEft = true;
//		}
//		if(heroData.getPSkillByID("TANK9") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("TANK6")){
//					int cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//					break;
//				}
//			}
//		}
		//end
	}
}
