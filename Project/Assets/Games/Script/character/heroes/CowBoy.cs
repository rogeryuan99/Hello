using UnityEngine;
using System.Collections;

public class CowBoy : Wizard {
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
	}

	
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("SC_Gun",weaponID);
	}
	
	public override void criticalHandler (){
//		HeroData heroData = data as HeroData;
//		if( heroData.getPSkillByID("COWBOY5") != null){
//			heroData.critValue += heroData.critValue*20/100;
//		}
//		if( heroData.getPSkillByID("COWBOY10") != null){
//			Enemy enemy = targetObj.GetComponent<Enemy>();
//			enemy.setAbnormalState(ABNORMAL_NUM.TAUNT);
//		}
	}
	
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
//		if(heroData.getPSkillByID("COWBOY4") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("COWBOY1")){
//					int cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -3 == 0? 0 : cd -3);
//					break;
//				}
//			}
//		}
		//end
	}
}
