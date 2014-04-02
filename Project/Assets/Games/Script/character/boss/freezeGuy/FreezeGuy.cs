using UnityEngine;
using System.Collections;

public class FreezeGuy : EnemyRemote {
	public GameObject flame_sk_Explosion;
	public GameObject flame_sk_IceBlock; 
	public GameObject flame_sk_Beam;
	private GameObject tempFlame_sk_IceBlock;
	private Hashtable heroes;
	private int skillNum = 0; 
	private Hero heroIceBlock;
	private bool  isMove = false;
	public override void Awake (){
//		birthPts = [500,50];
		base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 14; 
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
	}
	
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Skill",1,specialAtk); 
		pieceAnima.addFrameScript("Skill",26,specialAtkExplosion);
		pieceAnima.addFrameScript("Move",13,startMoveOut);
	}
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	public override void relive (){
		base.relive(); 
		InvokeRepeating("castingSkill",2,5);
		InvokeRepeating("startMove",3,5);
	}
	public void startMove (){
		if( HeroMgr.heroHash.Count == 0){
			return;
		}
		if(StaticData.isBattleEnd){ 
			cancelAtk();
			CancelInvoke("castingSkill");	
			CancelInvoke("startMove");					
			playAnim("Damage");
			pieceAnima.pauseAnima();
			return;
		}	
		playAnim("Move"); 
	}
		//override
//	protected function startAtk()
//	{
//		if(! GData.isBattleEnd){
////			state = ATK_STATE;
//			toward(targetObj.transform.position);
//			isPlayAtkAnim = true;
//			playAnim("Attack");
//		}
//	}
	protected override void moveTargetInAtkUpdate (){
		
		if(targetObj == null && state != STANDBY_STATE )
		{
			standby();
			return;
		}
		
		int xDis = (int)Mathf.Abs(targetObj.transform.position.x - transform.position.x);
		int yDis = (int)Mathf.Abs(targetObj.transform.position.y - transform.position.y);
		float spd2 = Time.deltaTime*realMspd;
		bool  isInRange = xDis<=(data.attackRange+spd2) && yDis<data.attackRange/5;
		if(!isInRange && !isPlayAtkAnim)
		{	
			attackTargetInvok();
		}
	}
	
	public void castingSkill (){ 
		if(targetObj != null){
			heroIceBlock = HeroMgr.getRandomHero();
			if(heroIceBlock == null){
				return;
			} 
			Hero hitHero = heroIceBlock.GetComponent<Hero>();
			if(tempFlame_sk_IceBlock == null && this.isDead == false && hitHero.isDead==false){ 
				state = CAST_STATE;
				playAnim("Skill"); 
				MusicManager.playEffectMusic("boss_freezeGuy_skill");  
			}else{
				if(tempFlame_sk_IceBlock.transform.position.x == -1000){
					Destroy(tempFlame_sk_IceBlock);
					state = CAST_STATE;
					playAnim("Skill"); 
					MusicManager.playEffectMusic("boss_freezeGuy_skill");  
				}
			}
		}
	} 

	public void specialAtkExplosion (string s){ 
		if(heroIceBlock == null || heroIceBlock.isDead){
			if(HeroMgr.heroHash.Count == 0){	
				return;
			}else{
				heroIceBlock = HeroMgr.getRandomHero(); 
			}
		}
		GameObject tempFlame_sk_Explosion; 
		GameObject tempFlame_sk_Explosion2;
		PackedSprite tempFlame_sk_ExplosionInfo;  
		PackedSprite tempFlame_sk_ExplosionInfo2;  
		Vector3 vec = Vector3.zero; 
		Vector3 vec2 = Vector3.zero;
		int local;
		if(model.transform.localScale.x > 0){ 
			vec = new Vector3(heroIceBlock.gameObject.transform.position.x+60,heroIceBlock.gameObject.transform.position.y+10,heroIceBlock.gameObject.transform.position.z); 
			vec2 = new Vector3(heroIceBlock.gameObject.transform.position.x-60,heroIceBlock.gameObject.transform.position.y+10,heroIceBlock.gameObject.transform.position.z); 
			local=1;
		}
		else{
			vec = new Vector3(heroIceBlock.gameObject.transform.position.x-60,heroIceBlock.gameObject.transform.position.y+10,heroIceBlock.gameObject.transform.position.z );
			vec2 = new Vector3(heroIceBlock.gameObject.transform.position.x+60,heroIceBlock.gameObject.transform.position.y+10,heroIceBlock.gameObject.transform.position.z); 
			local=-1;
		} 
		tempFlame_sk_Explosion = Instantiate(flame_sk_Explosion,vec,transform.rotation) as GameObject; 
		tempFlame_sk_Explosion2 = Instantiate(flame_sk_Explosion,vec2,transform.rotation) as  GameObject;
		tempFlame_sk_ExplosionInfo = tempFlame_sk_Explosion.GetComponent<PackedSprite>(); 
		tempFlame_sk_ExplosionInfo2 = tempFlame_sk_Explosion2.GetComponent<PackedSprite>(); 
		tempFlame_sk_ExplosionInfo.transform.localScale = new Vector3(local, tempFlame_sk_ExplosionInfo.transform.localScale.y, tempFlame_sk_ExplosionInfo.transform.localScale.z);
//		tempFlame_sk_ExplosionInfo.transform.localScale.x = local; 
		tempFlame_sk_ExplosionInfo2.transform.localScale = new Vector3(-local, tempFlame_sk_ExplosionInfo2.transform.localScale.y, tempFlame_sk_ExplosionInfo2.transform.localScale.z);
//		tempFlame_sk_ExplosionInfo2.transform.localScale.x = (-local);
		tempFlame_sk_ExplosionInfo.PlayAnim("Explosion");   
		tempFlame_sk_ExplosionInfo2.PlayAnim("Explosion");
		
		
		tempFlame_sk_IceBlock = Instantiate(flame_sk_IceBlock,new Vector3(heroIceBlock.gameObject.transform.position.x-16,heroIceBlock.gameObject.transform.position.y+60,heroIceBlock.gameObject.transform.position.z-1),transform.rotation) as GameObject;
		if(heroIceBlock.data.type != HeroData.DRUID){
				
			tempFlame_sk_IceBlock.transform.localScale = new Vector3(0.7f, 0.8f, tempFlame_sk_IceBlock.transform.localScale.z);
//				tempFlame_sk_IceBlock.transform.localScale.x = 0.7f;
//				tempFlame_sk_IceBlock.transform.localScale.y = 0.8f;
		}else{
//				if(! (heroIceBlock as Druid).isTransfigution){
//					tempFlame_sk_IceBlock.transform.localScale.x = 0.7f;
//					tempFlame_sk_IceBlock.transform.localScale.y = 0.8f;
//				}else{
//					Debug.Log(model.transform.localScale.x+"<---------------xingyihua>");
//					if(model.transform.localScale.x > 0){
//						tempFlame_sk_IceBlock.transform.position.x = tempFlame_sk_IceBlock.transform.position.x + 16;
//					}else{
//						tempFlame_sk_IceBlock.transform.position.x = tempFlame_sk_IceBlock.transform.position.x + 38;
//					}
//				}
		}
		EnemyIceBlock freeze = tempFlame_sk_IceBlock.GetComponent<Enemy>() as EnemyIceBlock; 
		freeze.relive();
		freeze.setHero(heroIceBlock);
	}
	public void specialAtk (string s){	
		//standby();   
//		heroIceBlock = HeroMgr.getRandomHero(); 
		GameObject tempFlame_sk_Beam; 
		tempFlame_sk_Beam = Instantiate(flame_sk_Beam,new Vector3(heroIceBlock.gameObject.transform.position.x,heroIceBlock.gameObject.transform.position.y,heroIceBlock.gameObject.transform.position.z+50),transform.rotation) as GameObject;	 	
//		print("x:"+heroIceBlock.gameObject.transform.position.x+"   y:"+heroIceBlock.gameObject.transform.position.y+"  z:"+heroIceBlock.gameObject.transform.position.z);

		//Invoke("specialAtkExplosion", 0.5f);			
	}
	public void deleteEft (){
		if(tempFlame_sk_IceBlock){
			EnemyIceBlock enemyIceBlock = tempFlame_sk_IceBlock.GetComponent<EnemyIceBlock>();
			enemyIceBlock.startDead("");
	//		GameObject.DestroyObject(tempFlame_sk_IceBlock); 
			skillNum=0;
		}
	}

	public void startMoveOut (string s){
				if(StaticData.isBattleEnd){ 
					cancelAtk();
					CancelInvoke("castingSkill");	
					CancelInvoke("startMove");
					playAnim("Damage");
					pieceAnima.pauseAnima();
					return;
				}	
				standby();
				if(! isMove){
					isMove = true;
				}else{
					cancelAtk();
					MusicManager.playEffectMusic("boss_freezeGuy_move");  
					this.gameObject.collider.enabled = true;
				 	this.gameObject.transform.position = new Vector3(-1000, 0, 0);
					Invoke("changePosition", 0.5f);
					isMove = false;
				}
	}
	private void changePosition (){ 
		Vector2 vc2 = BattleBg.getPointInScreen();
		Vector3 ve = new Vector3(vc2.x, vc2.y, vc2.y/10 + StaticData.objLayer);
		this.gameObject.transform.position=ve;
		if(targetObj != null){
			startAtk();
		}
		CancelInvoke("changePosition");	
	}
	public override void moveToTarget ( GameObject obj  ){
		Debug.Log("freezeGuy moveToTarget----=>");
	}
	protected override void atkAnimaScript (string s){	
		
//		GameObject tempAtkEft;
//		PackedSprite tempAtkEftInfo;
		base.atkAnimaScript("");
//		Vector3 vc3;
//		int direction;
//		if(model.transform.localScale.x > 0){
//			vc3 = Vector3(targetObj.transform.position.x+20,targetObj.transform.position.y+60,-96); 
//			direction = 1;
//		}else{
//			vc3 = Vector3(targetObj.transform.position.x,targetObj.transform.position.y+60,-96);
//			direction = -1;
//		}
//		tempAtkEft = Instantiate(atkEft,vc3,transform.rotation);
//		tempAtkEftInfo = tempAtkEft.GetComponent<PackedSprite>();
//		tempAtkEftInfo.transform.localScale.x = direction;
//		tempAtkEftInfo.PlayAnim("eft");
	
	}
	
//
//	public function moveToTarget( GameObject obj  )
//	{	
//		if(state == ATK_STATE)
//		{
//			cancelAtk();
//		}
//		
//		//xingyh 4.9f enemy out of the screen
//		targetObj = obj;
//		state = MOVE_TARGET_STATE;
//		
//		bool  b = isCollider();
//		if(!b){
//			targetObj = null;
//			standby();
//			return;
//		}
//	}
	public override void dead (string s=null){
		if(isDead)return;
		cancelAtk();
	    CancelInvoke("castingSkill");	
		CancelInvoke("startMove");
		CancelInvoke("changePosition");
		base.dead();
		deleteEft();
		//delete by xiaoyong .20120716  It's seem to doesn't make sense.
//		removeInList();
//		characterDeadMsg();
	
		finalBossAchievement();
	}
	public override void attackTargetInvok (){
		if(StaticData.isBattleEnd){ 
			cancelAtk();
			CancelInvoke("castingSkill");	
			CancelInvoke("startMove");
//			playAnim("Attack");
//			pieceAnima.pauseAnima();
			return;
		}
		Hero hero = null;
		if(targetObj){
			hero = targetObj.GetComponent<Hero>();
		}
		if(! hero.isDead)
		{
			toward(targetObj.transform.position);
			isPlayAtkAnim = true;
			playAnim("Attack");
		}else{
			standby();
			hero = HeroMgr.getRandomHero();
			if(hero)
			{
				targetObj = hero.gameObject;
				startAtk();
			}
		}
	}
	protected override void AnimaPlayEnd ( string animaName  ){
		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
//				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Damage":
//				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Skill":
//				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				Invoke("destroyThis",3);
				break;
			default:
				break;
		}
	}

	void OnDestroy (){
		heroes.Clear();
	}
}
