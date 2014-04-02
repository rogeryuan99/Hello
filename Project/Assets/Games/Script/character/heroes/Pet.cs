using UnityEngine;
using System.Collections;

public class Pet : Enemy {
	private Hero master;
	private bool  isLostMaster=false;
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 14;
		CharacterData tempData = new CharacterData(new Hashtable(){{"type","pet"},{"hp",1000},{"mspd",170},{"aspd",0},{"atk",0},{"def",30},{"rewardCoins",0},{"rewardExp",0},{"cstk",0},{"evd",0},{"stk",0}});
		initData(tempData);
		hideHpBar();
	}
	
	public override void dead (string s=null){
		Trainer tempHero = (master as Trainer);
//		tempHero.isProtect = false;
		if(isDead)return;
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		state = DEAD_STATE;
		isDead = true;
		data.isDead = true;
//		Message msg = new Message(MsgCenter.FALL_DOWN, this);
//		MsgCenter.instance.dispatch(msg);
//		playAnim("Death");
		iTween.Stop(gameObject);
		this.gameObject.collider.enabled = false;
		hideHpBar();
	}
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	public override void showHpBar (){
		hpBar.gameObject.SetActiveRecursively(false);
		hpBar.gameObject.transform.localScale = new Vector3(0,0,0);
	}
	
	public void castSkillA (){
		standby();
		state = CAST_STATE;
		playAnim("SkillA");
	}
	public void castSkillB (){
		standby();
		state = CAST_STATE;
//		pieceAnima.addFrameScript("SkillB", 21, skillBPause);
		playAnim("SkillB");
	}
	
//	private function skillBPause()
//	{
//		pieceAnima.pauseAnima();
//	}
	
	public void castSkillBComplete (){
		standby();
	}
	
	protected override void AnimaPlayEnd ( string animaName  ){
		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Stand");
				break;
			case "Damage":
				playAnim("Stand");
				break;
			case "SkillA":
				standby();
				break;
			case "SkillB":
				standby();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				Invoke("destroyThis",3);
				break;
			default:
				break;
		}
	}
	
	public override void Start (){
//		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
//		pieceAnima.pauseAnima();
//		birthPt = getBirthPt();
	}
	
	public override void toggleEnable (){
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
//		
//		Hero hero = HeroMgr.getRandomHero();
//		if(hero)
//		{
//			moveToTarget(hero.gameObject);
//		}
//		isEnabled = true;
		hideHpBar();
	}
	public override void relive (){
		
//		gameObject.transform.position = birthPt;
//		
//		hpBar.resetView();
//		isDead = false;
//		data.isDead = false;
//		initData(data);
//		playAnim("Move");
//		this.gameObject.collider.enabled = true;
		toggleEnable();
	}
	
	public void selectMaster ( Hero hero  ){
		master = hero;
		gameObject.transform.position = master.gameObject.transform.position - new Vector3(140,0,0);
		hideHpBar();
		setDepth();
	}
	
	public void updateDepth (){
		setDepth();
	}
	
	public void skillRushAttack ( Vector2 vc2  ){
		
	}
	public void skillProtect (){
		
	}
	public void lostMaster (){
		isLostMaster = true;
	}
	public override void selecting (){
		if(isLostMaster)
		{
			Destroy(gameObject);
		}
	}
	public void followMaster ( Vector3 vc3  ){
			if(vc3.x > BattleBg.actionBounds.max.x){
				vc3.x = BattleBg.actionBounds.max.x -2;
			}
			if(vc3.x < BattleBg.actionBounds.min.x){
				vc3.x = BattleBg.actionBounds.min.x +2;
			}
		targetPt = vc3;
		CancelInvoke("petMove");
		Invoke("petMove", 1);
	}
	
	private void petMove (){
		if(state == CAST_STATE)
		{
			return;
		}
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		state = MOVE_STATE;
		if(master)
		{
			if(master.model.transform.localScale.x>0)
			{
				targetPt -= new Vector3(80,0,0);
			}else{
				targetPt += new Vector3(80,0,0);
			}
		}
		playAnim("Move");
		setDirection(targetPt);
		toward(targetPt);
	}
	
	//override
	public override void toward ( Vector3 vc3  ){
		if(model.transform.position.x<vc3.x)// this animation x direction is reverse
		{
			model.transform.localScale = new Vector3(-scaleSize.x,scaleSize.y,1);
		}else{
			model.transform.localScale = new Vector3(scaleSize.x,scaleSize.y,1);
		}
	}
	
}
