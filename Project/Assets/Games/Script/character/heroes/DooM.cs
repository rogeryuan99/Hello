using UnityEngine;
using System.Collections;

public class DooM : Hero {
	
	public GameObject attackEft;
	// Use this for initialization
	public override void Awake () {
		base.Awake();
		atkAnimKeyFrame = 12;
		//pieceAnima.animaPlayEndScript(atkend);
	}
	
	public void atkend(string s)
	{
		//Destroy(attackEft);
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		
//			if(StaticData.computeChance(5,100)){
//				HeroData heroData = data as HeroData;
//				for(int i=0; i<heroData.skillListBattle.Count; i++){
//					SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//					tempSKD.reset();
//				}
//			}
		
		return base.defenseAtk(damage, atkerObj);
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
		
		base.atkAnimaScript("");
		//Destroy(eftObj);
	}
}
