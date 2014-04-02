using UnityEngine;
using System.Collections;

public class enemyMarine : enemyWizard {
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	void enemyDeadHandler (){
//		//waitHandler();
//	}
	
//	void waitHandler (){
//		string tempSkillName = SkillLib.instance.nameLib["MARINE11"];
//		if( GData.arrayIsContainString(tempSkillName,heroData.skillList)){
//			
//			foreach(DictionaryEntry tempHero in HeroMgr.heroHash){
//				Hero hero = tempHero.Value;
//				int atk = hero.realAtk * 10 / 100;
//				hero.addAtk(atk);
//			}
//		
//			yield return new WaitForSeconds(5);
//		
//			foreach(DictionaryEntry tempHero in HeroMgr.heroHash){
//				hero = tempHero.Value;
//				atk = hero.realAtk * 10 / 100;
//				hero.addAtk(-atk);
//			}
//		}
//	}
//	
//	public function defenseAtk( int damage ,   GameObject atkerObj  ):int
//	{
////		string tempSkillName = SkillLib.instance.nameLib["MARINE5"];
////		if( GData.arrayIsContainString(tempSkillName,heroData.skillList)){
////			if(GData.computeChance(10,100)){
////				//Enemy enemy = targetObj.GetComponent<Enemy>();
////			}
////		}
//		return super.defenseAtk(damage, atkerObj);
//	}
	
//	void criticalHandler (){
//		string tempSkillName = SkillLib.instance.nameLib["MARINE3"];
//		if( GData.arrayIsContainString(tempSkillName,heroData.skillList)){
//			heroData.critValue += heroData.critValue*15/100;
//		}
//	}
	
//	protected function initSkill()
//	{
////		SkillData skD = SkillData.create("MARINE1","SpaceCadetsMarch",45);
////		SkillData skD2 = SkillData.create("MARINE6", "Berserk", 30);
////		HeroData heroD = SceneInit.heroDataList[2];
//
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Space Cadet’s March","Berserk"];
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
////		
////		initData(SceneInit.heroDataList[2]);
////		(data as HeroData).isSelect = true;
//	}
}
