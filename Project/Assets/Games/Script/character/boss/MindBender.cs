using UnityEngine;
using System.Collections;

public class MindBender : Enemy {
/*
	MindBender:
		Controls healer so healer deals damage to player team members.
*/

	private Hashtable heroes;
	public GameObject controllEffect;
	public GameObject bomb;
	
	public GameObject explosion;
	
	public override void Awake (){
//		birthPts = [500,50];
		base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 13;
		
		//indicator = Instantiate(thunderLocIndicator, Vector3(0.0f,0.0f,100.0f), gameObject.transform.rotation);
		//MsgCenter.instance.addListener(MsgCenter.LEVEL_DEFEAT, stopCastThunder);
		//shouldCastThunder = false;
	}
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Skill",23,specialAtk);
	}
	
	public override void relive (){
		base.relive();
		//showHpBar();
		InvokeRepeating("castSkillAnimation", 10, 30);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
	}
	
	public void castSkillAnimation (){
		standby();
		state = CAST_STATE;
		playAnim("Skill");
		MusicManager.playEffectMusic("boss_mindbender_skill");
	}
	
	public override void moveToTarget ( GameObject obj  ){	
		Character character = obj.GetComponent<Character>();
		if (character is Healer) {
			Healer healer = (Healer)character;
			if (healer.isControlled) {
				return;
			}
		}
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		base.moveToTarget(obj);
	}
	
	protected void heroRelive (){
		if(targetObj == null && enemyType != "" && isDead == false ){
				attackSomeoneElse();
		}
	}
	
	private void enemiesChangeTarget (){
		Hashtable enemyHash = EnemyMgr.enemyHash.Clone() as Hashtable;
		if(enemyHash.Count>0){
			foreach(string key in enemyHash.Keys){
				Enemy en = enemyHash[key] as Enemy;
				if (en.getTarget() != null) {
					Character character = en.getTarget().gameObject.GetComponent<Character>();
					if (character.GetType() == typeof(Healer)) {
						//attackSomeoneElse();
						en.checkOpponent();
					}		
				}else {
					en.checkOpponent();
				}
			}
		}
	}

	public void specialAtk (string s){
//		playAnim("Skill");
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		
		foreach( string key in heroes.Keys)
		{
			Hero hero = heroes[key] as Hero;

			if (hero.GetType() == typeof(Healer)) {
				Healer healer = (Healer)hero;
				Instantiate(bomb,new Vector3(targetObj.transform.position.x,targetObj.transform.position.y+80,targetObj.transform.position.z-50),transform.rotation);
				Vector3 effectPos = gameObject.transform.position;
				effectPos.z -= 0.1f;
				Instantiate(controllEffect, effectPos, gameObject.transform.rotation);
				healer.becomeControlled();
				Character character = targetObj.GetComponent<Character>();
				if (character.GetType() == typeof(Healer)) {
					attackSomeoneElse();
				}
				enemiesChangeTarget();
				Invoke("attackIfOnlyHealerLeft", 21);//
				
				break;
			}
		}
	}
	
	private void attackIfOnlyHealerLeft (){
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		if (heroes.Count == 1) {
			Hero hero = HeroMgr.getRandomHero();
			if (hero){
				if (hero.GetType() == typeof(Healer)) {
					//Healer healer = hero;
					//if (!healer.isControlled) {
						moveToTarget(hero.gameObject);
					//}
				}
			}
		}
	}
	
	private void attackSomeoneElse (){
		Hero hero = HeroMgr.getRandomHero();
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		
		if (hero)
		{
			if (hero.GetType() == typeof(Healer)) {
				Healer healer = (Healer)hero;
				if (!healer.isControlled) {
					moveToTarget(hero.gameObject);
				}
				else if (heroes.Count > 1){
					attackSomeoneElse();
				}
				/*else {
					standBy();
				}
				*/
			} else {
				moveToTarget(hero.gameObject);
			}
		}
		//else 
		/*
		else {
			standBy();
		}
		*/
	}
	
	public override int defenseAtk( Vector6 damage ,   GameObject atkerObj  )//override
	{
		int dmg;
		dmg = base.defenseAtk(damage, atkerObj);
//		super.checkAtkerDefense(atkerObj);
		return dmg;
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("boss_mindbender_atk");
		base.atkAnimaScript("");
	}
	
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Move");
				break;
			case "Damage":
				playAnim("Move");
				break;
			case "Skill":
				attackSomeoneElse();
				playAnim("Move");
				break;
			case "Death":
				Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
				//destroyThis();
				pieceAnima.pauseAnima();
				Invoke("destroyThis",0.2f);
				break;
			default:
				break;
		}
	}
	
	public override void dead (string s=null){
		base.dead();
		CancelInvoke("castSkillAnimation");
		finalBossAchievement();
	}

}
