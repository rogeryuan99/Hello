using UnityEngine;
using System.Collections;

public class enemyCowBoy : enemyWizard {

	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	
//	void enemyDeadHandler (){
//		//Debug.Log("++++++++++enemyDeadHandler");
//		foreach(SkillData skData in heroData.skillListBattle){
//			skData.reset();
//			//Debug.Log("++++++++++enemyDeadHandler000000000");
//		}
//	}
	
//	void criticalHandler (){
//		string tempSkillName = SkillLib.instance.nameLib["COWBOY5"];
//		if( GData.arrayIsContainString(tempSkillName,heroData.skillList)){
//			heroData.critValue += heroData.critValue*20/100;
//		}
//		tempSkillName = SkillLib.instance.nameLib["COWBOY10"];
//		if( GData.arrayIsContainString(tempSkillName,heroData.skillList)){
//			Enemy enemy = targetObj.GetComponent<Enemy>();
//			enemy.playAnim("Damage");
//			// playAnim
//		}
//	}
	
//	protected function initSkill()
//	{
//	
////		SkillData skD = SkillData.create("COWBOY1","Quickdraw",25);
////		SkillData skD2 = SkillData.create("COWBOY6", "FrontierSpirit", 30);
////		HeroData heroD = SceneInit.heroDataList[1];
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Quickdraw","Frontier Spirit"];
//		heroData.passiveList = [];
//		
//		if(!isPuppet)
//		{
//			//heroData.skillListBattle = [skD,skD2];
//			heroData.buildActiveSkill();
//			heroData.buildPassiveSkill();
//		}
//		EquipCalculator.calculate(heroData);
//		
//		//gwp passiveLis
//		string tempSkillName = SkillLib.instance.nameLib["COWBOY4"];
//		if(GData.arrayIsContainString(tempSkillName,heroData.passiveList)){
//			foreach(SkillData tempSKD in heroData.skillListBattle){
//				if(tempSKD.skillName.Equals(SkillLib.instance.nameLib["COWBOY1"])){
//					int cd = tempSKD.CDTime;
//					tempSKD.CDTime = (cd -3 == 0? 0 : cd -3);
//					break;
//				}
//			}
//		}
//		//end
//		
//		
////		initData(SceneInit.heroDataList[1]);
////		(data as HeroData).isSelect = true;
//	}
}
