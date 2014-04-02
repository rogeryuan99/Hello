using UnityEngine;
using System.Collections;

public class enemyPriest : Enemy {
	bool  isInvincible = false;//gwp   wu di 
	bool  isRebound = false;//gwp 
	
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 12;
	}
	public override void dead (string s=null){
		if(isDead)return;
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		
		state = DEAD_STATE;
		isDead = true;
		data.isDead = true;
		playAnim("Death");
		iTween.Stop(gameObject);
		this.gameObject.collider.enabled = false;
		characterDeadMsg();
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		base.atkAnimaScript("");
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	
	public override void characterDeadMsg (){
		Message msg = new Message(MsgCenter.COPY_HERO_DEAD, this);
		msg.data = this.data.type;
		MsgCenter.instance.dispatch(msg);
	}
	// weapon change test
//	public function changeWeapon( string weaponID  ):void
//	{
//		pieceAnima.showPiece("weapon",weaponID);
//	}
//	
//	//override
//	protected function drawAvatar()
//	{
////		EquipData eD = EquipFactory.lib[1];
////		HeroData heroD = data;
////		heroD.equipObj(EquipData.WEAPON, eD);  //WEAPON=weapons
//		//super.drawAvatar();
//	}
//	//gwp
//	public function defenseAtk( int damage ,   GameObject atkerObj  ):int
//	{
////		Enemy enemy = atkerObj.GetComponent<Enemy>();
////		if(isInvincible){
////			if(isRebound){
////				enemy.defenseAtk(damage,this.gameObject);
////			}
////			return 0;
////		}
////		string tempSkillName = SkillLib.instance.nameLib["PRIEST4"];
////		if(GData.arrayIsContainString(tempSkillName,heroData.passiveList)){	
////			if(GData.computeChance(30,100)){
////				enemy.defenseAtk(damage,this.gameObject);
////			}
////		}
//		
//		return super.defenseAtk(damage, atkerObj);
//	}
//	
//	public function realDamage( int dam  )
//	{
//		if(isInvincible){
//			return;
//		}
//		super.realDamage(dam);
//	}
//	function criticalHandler()
//	{
//		foreach(DictionaryEntry tempHero in HeroMgr.heroHash){
//			Hero hero = tempHero.Value;
//			int atk = hero.realAtk * 10 / 100;
//			hero.addAtk(atk);
//		}
//	}
	//end
	
	//
//	protected function initSkill()
//	{
////		SkillData skD = SkillData.create("PRIEST1","HappyThoughts",45);
////		SkillData skD2 = SkillData.create("PRIEST6", "MindShield", 45);
////		HeroData heroD = SceneInit.heroDataList[6];
//		
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Happy Thoughts","Mind Shield"];
//		heroData.passiveList = [];
//		
//		if(!isPuppet)
//		{
////			heroD.skillListBattle = [skD,skD2];
//			heroData.buildActiveSkill();
//			heroData.buildPassiveSkill();
//		}
//		
//		EquipCalculator.calculate(heroData);
//		
//		//gwp passiveLis
//		string tempSkillName = SkillLib.instance.nameLib["PRIEST10"];
//		isRebound = GData.arrayIsContainString(tempSkillName,heroData.passiveList);
//		
//		tempSkillName = SkillLib.instance.nameLib["PRIEST12"];
//		if(GData.arrayIsContainString(tempSkillName,heroData.passiveList)){
//			foreach(SkillData tempSKD in heroData.skillListBattle){
//				int cd = tempSKD.CDTime;
//				tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//			}
//		}
		//end
//		
//		initData(SceneInit.heroDataList[6]);
//	}
	
}
