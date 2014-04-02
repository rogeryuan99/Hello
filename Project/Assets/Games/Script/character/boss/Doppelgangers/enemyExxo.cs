using UnityEngine;
using System.Collections;

public class enemyExxo : Enemy {
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
	//override
//	protected function drawAvatar()
//	{
////		EquipData eD = EquipFactory.lib[1];
////		HeroData heroD = data;
////		heroD.equipObj(EquipData.WEAPON, eD);  //WEAPON=weapons
//		super.drawAvatar();
//	}

	
//	protected function initSkill()
//	{
////		SkillData skD = SkillData.create("TANK1","EnergizedAlloy",30);
////		SkillData skD2 = SkillData.create("TANK6", "HeavyMetalAttraction", 30);
////		HeroData heroD = SceneInit.heroDataList[6];
//
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Energized Alloy","Heavy Metal Attraction"];
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
////		initData(SceneInit.heroDataList[6]);
//	}
	
}
