using UnityEngine;
using System.Collections;

public class LargeMech : Enemy {
	public GameObject atkEft;
	public GameObject flame_sk_warning;
	public GameObject flame_sk_burning;
	public GameObject explosion;
	private GameObject tempFlame_sk_burning;
	private ArrayList objArray = new ArrayList();
	private Hashtable heroes;
	private int skillNum = 0;
	private Hero hero;
	
	//add by gwp for fire music
	private ArrayList indexAry;
	
	public override  void Awake (){
//		birthPts = [500,50];
		base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 14; 
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		indexAry = new ArrayList();
	}
	
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Skill",15,specialAtk);
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	public override void relive (){
		base.relive(); 
		InvokeRepeating("castingSkill",1,10);
	}
	
	public void castingSkill (){ 
		if(targetObj != null){
			Hero hitHero = targetObj.GetComponent<Hero>();
			if(this.isDead == false && hitHero.isDead==false){ 
				state = CAST_STATE;
				playAnim("Skill"); 
				MusicManager.playEffectMusic("boss_largeMech_smoke");
			}
		}
	}
	public void specialAtk (string s ){
		//standby();
		if(this.isDead){
			return;
		}
		hero = HeroMgr.getRandomHero(); 
		Debug.Log(hero+"<-----------xingyihua burning go to hero>");
		if(hero == null || hero.isDead){
			return;
		}
		GameObject tempFlame_sk_warning;
		PackedSprite tempFlame_sk_warningInfo;

		float z = hero.gameObject.transform.position.z;
		if(model.transform.localScale.x > 0){
				tempFlame_sk_warning= Instantiate(flame_sk_warning,new Vector3(gameObject.transform.position.x+200,gameObject.transform.position.y+80,gameObject.transform.position.z),transform.rotation) as GameObject;
				tempFlame_sk_warningInfo= tempFlame_sk_warning.GetComponent<PackedSprite>();
				tempFlame_sk_warningInfo.PlayAnim("eft");
//				tempFlame_sk_burning = Instantiate(flame_sk_burning,Vector3(hero.gameObject.transform.position.x,hero.gameObject.transform.position.y,z),transform.rotation);
		}
		else{
				tempFlame_sk_warning = Instantiate(flame_sk_warning,new Vector3(gameObject.transform.position.x-150,gameObject.transform.position.y+80,gameObject.transform.position.z),transform.rotation) as GameObject;
				tempFlame_sk_warningInfo = tempFlame_sk_warning.GetComponent<PackedSprite>();
			tempFlame_sk_warningInfo.transform.localScale = new Vector3(-1, tempFlame_sk_warningInfo.transform.localScale.y, tempFlame_sk_warningInfo.transform.localScale.z);
//				tempFlame_sk_warningInfo.transform.localScale.x = -1;
				tempFlame_sk_warningInfo.PlayAnim("eft");
			
//				tempFlame_sk_burning = Instantiate(flame_sk_burning,Vector3(hero.gameObject.transform.position.x,hero.gameObject.transform.position.y,z),transform.rotation);
//				tempFlame_sk_burning.transform.localScale.x = -1;
		}
		Instantiate(explosion,new Vector3(hero.gameObject.transform.position.x,hero.gameObject.transform.position.y,z),transform.rotation);
		Invoke("explosionEnd", 0.1f);
	}
	
	public void explosionEnd (){
		if(this.isDead){
			return;
		}
		float z = hero.gameObject.transform.position.z+50;
		if(model.transform.localScale.x > 0){	
				tempFlame_sk_burning = Instantiate(flame_sk_burning,new Vector3(hero.gameObject.transform.position.x,hero.gameObject.transform.position.y,z),transform.rotation) as GameObject;
		}else{		
				tempFlame_sk_burning = Instantiate(flame_sk_burning,new Vector3(hero.gameObject.transform.position.x,hero.gameObject.transform.position.y,z),transform.rotation) as GameObject;
			tempFlame_sk_burning.transform.localScale = new Vector3(-1, tempFlame_sk_burning.transform.localScale.y, tempFlame_sk_burning.transform.localScale.z);	
//			tempFlame_sk_burning.transform.localScale.x = -1;
		}
		int index = 0;//MusicManager.playEffectMusicForLoop("boss_largeMech_fire");
		indexAry.Add(index);
		if(z > 0){
			tempFlame_sk_burning.transform.position = new Vector3(tempFlame_sk_burning.transform.position.x, tempFlame_sk_burning.transform.position.y, 0);
//			tempFlame_sk_burning.transform.position.z = 0;
			float sizeZ = Mathf.Abs(hero.gameObject.transform.position.z*2);
			Debug.Log("hero.gameObject.transform.position.z*2: "+sizeZ);
			
			BoxCollider boxC = tempFlame_sk_burning.collider as BoxCollider;
			boxC.size = new Vector3(boxC.size.x, boxC.size.y, sizeZ);
//			boxC.size.z = sizeZ;
		}
		objArray.Add(tempFlame_sk_burning);
		InvokeRepeating("specialAtkComplete", 1, 1);		
		skillNum = skillNum+1; 
		if(skillNum > 2){	
			deleteEft();
//			CancelInvoke("specialAtkComplete");			
		} 
	}
	public void deleteEft (){
		GameObject obj = objArray[0] as GameObject;
		objArray.RemoveAt(0);
		int index = int.Parse(indexAry[0].ToString());
		MusicManager.cancleLoop(index);
		indexAry.RemoveAt(0);
		GameObject.DestroyObject(obj); 
		skillNum = skillNum-1;
	}
//	public function hasHitHero() 
//	{ 		
//			print("hesHitHero------->");
////			GameObject tempAtkEft = Instantiate(atkEft,Vector3(hitHero.gameObject.transform.position.x,hitHero.gameObject.transform.position.y+60,hitHero.gameObject.transform.position.z),transform.rotation);
////			PackedSprite tempAtkEftInfo = tempAtkEft.GetComponent<PackedSprite>();
////			tempAtkEftInfo.PlayAnim("eft");  
//		//	InvokeRepeating("specialAtk", 1, 2);	
//	}

	public void specialAtkComplete (){
		foreach( string key in heroes.Keys)
		{
			Hero hero = heroes[key] as Hero; 
			if( ! hero.isDead ){
					Bounds heroBounds = hero.gameObject.collider.bounds;
					for(int i = 0 ; i< objArray.Count ; i ++ ){
						GameObject obj = objArray[i] as GameObject;
						if( obj.collider.bounds.Intersects(heroBounds) )
						{
//							hero.defenseAtk(200, this.gameObject);
						}
					}
				}
		}  
	}
	
	protected override void atkAnimaScript (string s){	
		GameObject tempAtkEft;
		PackedSprite tempAtkEftInfo;
		base.atkAnimaScript("");
		Vector3 vc3;
		int direction;
		
		if(targetObj){
			Debug.Log(targetObj+"<-----------xingyihua burning go to hero>");
			if(model.transform.localScale.x > 0){
				vc3 = new Vector3(targetObj.transform.position.x+20,targetObj.transform.position.y+60,-96); 
				direction = 1;
			}else{
				vc3 = new Vector3(targetObj.transform.position.x,targetObj.transform.position.y+60,-96);
				direction = -1;
			}
			tempAtkEft = Instantiate(atkEft,vc3,transform.rotation) as GameObject;
			tempAtkEftInfo = tempAtkEft.GetComponent<PackedSprite>();
			tempAtkEftInfo.transform.localScale = new Vector3(direction, tempAtkEftInfo.transform.localScale.y, tempAtkEftInfo.transform.localScale.z);
//			tempAtkEftInfo.transform.localScale.x = direction;
			tempAtkEftInfo.PlayAnim("eft");
			MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		}
	}
	
	public override void dead (string s=null){
		for(int i = objArray.Count-1;i >= 0 ; i--){
			GameObject obj = objArray[i] as GameObject;
			objArray.RemoveAt(i);
			int index = int.Parse(indexAry[0].ToString());
			MusicManager.cancleLoop(index);
			indexAry.RemoveAt(0);
			GameObject.DestroyObject(obj); 
		}
		CancelInvoke("castingSkill");
		CancelInvoke("specialAtkComplete");
		CancelInvoke("explosionEnd");
		cancelAtk();	
		base.dead();
		miniBossAchievement();
		//AchievementManager.updateAchievement("BEAT_MINI_4", 1);
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
				isPlayAtkAnim = false; 	
				playAnim("Move"); 
				base.checkOpponent();
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
