using UnityEngine;
using System.Collections;

public class Coward : Enemy {
	private Hashtable heroes;
	
	public Vector2 runawayTarget;
	public bool  isRunaway = false;
	public bool  isATK = true;
	
	public override void Awake (){
base.Awake();
		atkAnimKeyFrame = 27; 
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
	}
	
//	function Start()
//	{
//		super.Start();	
//		specialAtk();
//	}
	public override void relive (){
base.relive(); 
//		print("relive-------->"); 
		specialAtk();
	}
	public void specialAtk()//ATK
	{
		Hero hero = HeroMgr.getDefMinHero();
		InvokeRepeating("specialAtkComplete", 5, 10);
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	public void specialAtkComplete()//Find Target
	{
		if(isATK)
		{
			Debug.Log("specialAtkComplete()");
			Hero hero = HeroMgr.getDefMinHero();
			if(hero)
			{
				moveToTarget(hero.gameObject);
			}
		}
	}
	
	protected override void atkAnimaScript (string s){
//		print("attck AnimaScript----------->");
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
base.atkAnimaScript("");
		isRunaway = false;
	}
	
	public override int defenseAtk( Vector6 damage ,   GameObject atkerObj  )//get attacked
	{
		int dmg;
		Run();
		
		dmg = base.defenseAtk(damage, atkerObj);
		return dmg;
	}
	
	protected override void checkAtkerDefense ( GameObject atker  ){}
	
	public void BackATK (){
		isATK = true;
		if(! isDead){
			specialAtkComplete();
		}
//		print("Back!!!!!!!!");
	}
	
	public void Run (){
		if(!isRunaway && isATK)
		{
			isRunaway = true;
			isATK = false;
			randomMoving();
			
			Invoke("BackATK", 10);
//			print("Runnnnnnn!!!!!!!!");
		}
	}
	
	//override
	protected override void moveStateUpdate (){
		float dis = Vector2.Distance(transform.position,targetPt);
		float spd = Time.deltaTime*realMspd;
		if(Mathf.Abs(dis)>spd){
			if(dirVc3.y != 0){
				setDepth();
			}
			transform.Translate(dirVc3*spd);
		}else{
			randomMoving();
		}
	}
	
	private void randomMoving (){
//			BoxCollider bgBoxCollider = BattleBg.bgCollider.GetComponent<BoxCollider>();	
			Vector3 minVc3 = BattleBg.actionBounds.min;
			Vector3 maxVc3 = BattleBg.actionBounds.max;
			runawayTarget.x = Random.Range(minVc3.x+80,maxVc3.x-80);
			runawayTarget.y = Random.Range(minVc3.y+80,maxVc3.y-80);
			move(new Vector3(runawayTarget.x,runawayTarget.y,0));
	}
	
	void OnDestroy (){
		heroes.Clear();
	}
	
	public override void dead (string s=null){
		CancelInvoke("specialAtkComplete");
		base.dead();
		miniBossAchievement();
		//AchievementManager.updateAchievement("BEAT_MINI_2", 1);
	}
	
	
}
