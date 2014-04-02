using UnityEngine;
using System.Collections;

public class Trainer : Wizard {
	public GameObject petPrb;
	public Pet pet;
	
	public bool  isTigersClaw;
	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 14;
		if (GotoProxy.getSceneName() != GotoProxy.SKILLTREE
			&& GotoProxy.getSceneName() != GotoProxy.YOUR_TEAM
			&& GotoProxy.getSceneName() != GotoProxy.COMBINED_ARMORY
			&& GotoProxy.getSceneName() != GotoProxy.ARMORY )
		{
			GameObject petObj = Instantiate(petPrb, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			pet = petObj.GetComponent<Pet>();
			pet.selectMaster(this);
		}
		isTigersClaw = false;
	}
	
	public override void Start (){
		base.Start();
	}
	
	public override void destroyThis (){
		StartCoroutine( destroyPet());
		Destroy(gameObject);
		Message msg = new Message(MsgCenter.HERO_DEAD, this);
		MsgCenter.instance.dispatch(msg);
	}
	
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("weapon",weaponID);
	}
	
	public override void criticalHandler (){
		HeroData heroData = data as HeroData;
		if( heroData.getPSkillByID("TRAINER9") != null){
			if(StaticData.computeChance(30,100)){
				playAnim("Attack");
			}
		}
	}
	
	public IEnumerator destroyPet (){
		if(pet)
		{
			pet.lostMaster();
			Vector3 vc3 = pet.transform.position;
			vc3.x = -700;
			pet.followMaster(vc3);
			yield return new WaitForSeconds(6);
			Destroy(pet);
		}
	}
	
	public override void move ( Vector3 vc3  ){
		base.move(vc3);
		if(isTigersClaw){
			return;
		}
		if(pet)
		{
			pet.followMaster(vc3);
		}
	}
	
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
		int cd;
//		SkillData tempSKD = null;
//		if(heroData.getPSkillByID("TRAINER3") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("TRAINER1")){
//					cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -5 == 0? 0 : cd -5);
//					break;
//				}
//			}
//		}
//		if(heroData.getPSkillByID("TRAINER10") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				tempSKD = heroData.skillListBattle[i] as SkillData;
//				if(tempSKD.id.Equals("TRAINER6")){
//					cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//					break;
//				}
//			}
//		}
		//end
	}
}
