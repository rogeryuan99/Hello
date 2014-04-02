using UnityEngine;
using System.Collections;

public class enemyTrainer : enemyWizard {
	public GameObject petPrb;
	private Pet pet;
	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 14;
		
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	
//	function Start()
//	{
//		super.Start();
//		GameObject petObj = Instantiate(petPrb, gameObject.transform.position, gameObject.transform.rotation);
//		pet = petObj.GetComponent<Pet>();
//	//	pet.selectMaster(this);
//	}
//	
//	public function move( Vector3 vc3  )
//	{
//		super.move(vc3);
//		if(pet)
//		{
//			pet.followMaster(vc3);
//		}
//	}
//	
//	protected function initSkill()
//	{
////		SkillData skD = SkillData.create("TRAINER1","TigersClaw",25);
////		SkillData skD2 = SkillData.create("TRAINER6", "Protect", 35);
////		HeroData heroD = SceneInit.heroDataList[3];
//
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Tiger’s Claw","Protect"];
//		heroData.passiveList = [];
//		
//		if(!isPuppet)
//		{
////			(data as HeroData).skillListBattle = [skD,skD2];
//			heroData.buildActiveSkill();
//			heroData.buildPassiveSkill();
//		}
//		EquipCalculator.calculate(heroData);
//		
////		initData(SceneInit.heroDataList[3]);
////		(data as HeroData).isSelect = true;
//	}
}
