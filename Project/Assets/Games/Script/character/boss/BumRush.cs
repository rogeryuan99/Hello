using UnityEngine;
using System.Collections;

public class BumRush : Enemy {
	public GameObject skEft;
	private Hashtable hitedTargets;
	private Hashtable heroes; 
	private string damage;
	//add by gwp for skil music
	private int index;
	
	public override void Awake (){
//		print("BumRush-----Awake");
//		birthPts = [500,50];
base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
//		atkAnimKeyFrame = 13;
		hitedTargets = new Hashtable();
	}
	public override void Start (){
base.Start();
		pieceAnima.addFrameScript("Attack",13,atkEft);
		pieceAnima.addFrameScript("Skill",20,specialAtk);
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	public override void relive (){	
//		print("BumRush-----relive");
base.relive();
		InvokeRepeating("specialAtkFront", 6, 10);	
	}
	
	public void specialAtkFront (){
		standby();
		if(!StaticData.isBattleEnd){
			state = CAST_STATE;
			damage = "damage";
			playAnim("Skill"); 
			MusicManager.playEffectMusic("boss_locustQueen_skillEft");
		}else{
			CancelInvoke("specialAtkFront");
		}
	}
	 
	public void specialAtk (string s){	
		heroes = HeroMgr.heroHash.Clone() as Hashtable;   //get heroHash---has all heros
		   //if gameObject attack hero, gameObject stop attack and start hit hero
		if(model.transform.localScale.x > 0)
		{
			//"x" is move to position.x, onupdatetarget invoke hasHitHero method, oncompletetarget invoke specialAtkComplete method
			iTween.MoveTo(gameObject,new Hashtable(){{"x",600},{  "speed",1200},{ "easetype","linear"},{ "onupdate","hasHitHero"},{ "onupdatetarget",gameObject},{ 
									"oncomplete","specialAtkComplete"},{ "oncompletetarget",gameObject}});
		}else
		{
			iTween.MoveTo(gameObject,new Hashtable(){{"x",-600},{  "speed",1200},{ "easetype","linear"},{ "onupdate","hasHitHero"},{ "onupdatetarget",gameObject},{ 
									"oncomplete","specialAtkComplete"},{ "oncompletetarget",gameObject}});
		}
		//index = MusicManager.playEffectMusicForLoop("boss_locustQueen_skill");
		
//		playAnim("Skill");
	}
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		
		if(isDead)return 0;
		int dam = base.getDamageValue(damage);
//		ConsoleObj.instance.showInfo(atkerObj.name+"_AtkValue:"+damage+" "+gameObject.name+"_RealDef:"+realDef+" damage:"+damage+"-"+realDef+"="+dam);
		realHp -= dam;
		showHpBar();
		if(realHp<= 0)
		{
			dead();
		}else{
			if (!isPlayAtkAnim 
					&& state != CAST_STATE 
					&& state != MOVE_TARGET_STATE 
					&& state != MOVE_TARGET_DIRECTLY_STATE
					&& state != MOVE_STATE){
				playAnim("Damage");
			}
		}
		checkAtkerDefense(atkerObj);
		return dam;
		
	}
	
	protected void atkEft (string s){	
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
//		print("atk------->>");
		int direction;
		if(model.transform.localScale.x > 0){
			direction = 1;
		}else{
			direction = -1;
		}
		Hero hero = targetObj.GetComponent<Hero>();
		Vector3 pt = new Vector3(targetObj.transform.position.x,targetObj.transform.position.y+20,targetObj.transform.position.z-20 );
		GameObject tempSkEft = Instantiate(skEft,pt,this.transform.rotation) as GameObject;	
		tempSkEft.transform.localScale = new Vector3(direction, tempSkEft.transform.localScale.y, tempSkEft.transform.localScale.z);
//		tempSkEft.transform.localScale.x = direction;
		
		hero.defenseAtk(realAtk,this.gameObject);
	}
	
	public void hasHitHero (){
		foreach( string key in heroes.Keys)  //hero.Keys type is string
		{
			Hero hero = heroes[key] as Hero;
			Bounds heroBounds = hero.gameObject.collider.bounds;
			if( gameObject.collider.bounds.Intersects(heroBounds) )  //Does another bounding box intersect with this bounding box?
			{
				if(hitedTargets[hero.data.type] == null)
				{
					//the boss hit the hero
					hitedTargets[hero.data.type] = 1;  //data classtype is HeroData     type is string  "type1"
					hero.realDamage(200);//realDamage: prevent hero from chasing boss after boss runs out of screen
					Debug.Log("fuck--------------><"+hero.state);
					if(hero.state != MOVE_STATE){
//						BoxCollider bgBoxCollider = BattleBg.bgCollider.GetComponent<BoxCollider>();
						Vector3 minVc3 = BattleBg.actionBounds.min;
						Vector3 maxVc3 = BattleBg.actionBounds.max;
						Rect rect = new Rect(minVc3.x+100, minVc3.y, maxVc3.x-minVc3.x-200, maxVc3.y-minVc3.y);
						Debug.Log(maxVc3.x+" <------xingyihua------>"+minVc3.x);
						
						Vector2 sCircelVc2;
						if(model.transform.localScale.x < 0)
						{
							sCircelVc2 = new Vector2(hero.gameObject.transform.position.x-50,hero.gameObject.transform.position.y);
						}else{
							sCircelVc2 = new Vector2(hero.gameObject.transform.position.x+50,hero.gameObject.transform.position.y);
						}
						if(rect.Contains(sCircelVc2)){	
							Debug.Log(hero.gameObject.transform.position.x+" <------xingyihua------>");
							if(model.transform.localScale.x < 0)
							{
								hero.gameObject.transform.position = new Vector3(hero.gameObject.transform.position.x - 50,hero.gameObject.transform.position.y,hero.gameObject.transform.position.z);
//								hero.gameObject.transform.position.x -= 50;
							}else{
								hero.gameObject.transform.position = new Vector3(hero.gameObject.transform.position.x +  50,hero.gameObject.transform.position.y,hero.gameObject.transform.position.z);
//								hero.gameObject.transform.position.x += 50;
							}
						}
					}
				}
				
			}
		}
	}
	
	//super class Enemy start()-----pieceAnima.animaPlayEndScript(AnimaPlayEnd);
	public void specialAtkComplete (){
		MusicManager.cancleLoop(index);
		hitedTargets.Clear();
		getBirthPt();   // reset Enemy's x value
		Hero hero = HeroMgr.getRandomHero();
		if(hero)
		{
			moveToTarget(hero.gameObject);
		}
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
				playAnim("Rush");
//				super.attackRandomHero();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				CancelInvoke("specialAtkFront");
				Invoke("destroyThis",3);  //Invoke ( string methodName ,   float time  )----invoke method destroyThis()
				//AchievementManager.updateAchievement("BEAT_MINI_1", 1);
				break;
			default:
				break;
		}
	}
	
	void OnDestroy (){
		if(heroes != null){
			heroes.Clear();
		}
	}
	
	public override void dead (string s=null){
base.dead();
		miniBossAchievement();
	}
}
