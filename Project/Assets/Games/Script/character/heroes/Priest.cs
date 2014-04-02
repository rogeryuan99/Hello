using UnityEngine;
using System.Collections;

public class Priest : Hero {
	bool  isRebound = false;//gwp 
	
	public override void Awake (){
		base.Awake();
		isInvincible = false;
		atkAnimKeyFrame = 12;
	}
	// weapon change test
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("weapon",weaponID);
	}
	
	//override
	public void playAtkEft ()
	{
		Vector3 pos;
		int scaleX;
		if(!targetObj)return;
		if(model.transform.localScale.x > 0)
		{
			pos = targetObj.transform.position + new Vector3(0, 40, -2);
			scaleX = 1;
		}else{
			pos = targetObj.transform.position + new Vector3(0, 40, -2);
			scaleX = -1;
		}
		GameObject atkEft = Instantiate(slashEft, pos, transform.rotation) as GameObject;
		Vector3 v = atkEft.transform.localScale;
		v.x = scaleX;
		atkEft.transform.localScale = v;
	}
	
	//gwp
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		Enemy enemy = atkerObj.GetComponent<Enemy>();
		if(isInvincible){
			if(isRebound){
				enemy.defenseAtk(damage,this.gameObject);
			}
			return 0;
		}
		HeroData heroData = data as HeroData;
		if(heroData.getPSkillByID("PRIEST4") != null){	
			if(StaticData.computeChance(30,100)){
				enemy.defenseAtk(damage,this.gameObject);
			}
		}
		
		return base.defenseAtk(damage, atkerObj);
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("atk_priest");
		base.atkAnimaScript("");
	}
	
	
	public override void realDamage ( int dam  ){
		if(isInvincible){
			return;
		}
		base.realDamage(dam);
	}
	//die jia 
	public override void criticalHandler (){
		HeroData heroData = data as HeroData;
		if(heroData.getPSkillByID("PRIEST9") != null){
			string tempSkillName = SkillLib.instance.getSkillNameByID("PRIEST9");
			foreach(DictionaryEntry tempHero in HeroMgr.heroHash){
				Hero hero = tempHero.Value as Hero;
//				int atk = hero.realAtk * 10 / 100;
//				hero.addBuff(tempSkillName,5,atk,BuffTypes.ATK);
				hero.unFlash();
				hero.flash(0.8f,0.2f,0);
			}
		}
	}
	//end
	
	//
	protected override void initSkill (){
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
		isRebound = (heroData.getPSkillByID("PRIEST10") != null);
		
//		if(heroData.getPSkillByID("PRIEST12") != null){
//			for( int i=0; i<heroData.skillListBattle.Count; i++){
//				SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//				int cd = (int)tempSKD.CDTime;
//				tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//			}
//		}
		//end
	}
	
}
