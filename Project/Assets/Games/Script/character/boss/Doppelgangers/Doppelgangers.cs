using UnityEngine;
using System.Collections;

public class Doppelgangers : Enemy {
	GameObject cowboy;
	GameObject healer;
	GameObject marine;
	GameObject tank;
	GameObject trainer;
	GameObject priest;
	GameObject wizard;
	GameObject druid;
	
	public GameObject atkEft;
	private int num;
	public static Hashtable copyHero;
	private Hashtable copyObj; 
	private Hashtable copyHeroManager;
	public GameObject skillEft;
	
	private int copyNum = 2;
	public bool  isCopy = true;
	
	public override void Awake (){
		scaleSize = new Vector3(2.0f, 2.0f, 1);
		base.Awake();
		copyHero = new Hashtable();
		copyObj =new Hashtable(); 
		copyHeroManager =new Hashtable();
		initCopyObj();
		MsgCenter.instance.addListener(MsgCenter.COPY_HERO_DEAD, copyHeroDead);
		this.gameObject.collider.enabled = false; 
	}
	public override void Start (){
		base.Start();
		createEnemy();
		pieceAnima.addFrameScript("Skill",14,startAgainCopyHero);
		pieceAnima.addFrameScript("Attack",30,atkEftStart);
	}
	
	public void atkEftStart (string s){
		GameObject atkObj = null;
		if(model.transform.localScale.x > 0){
			atkObj = Instantiate(atkEft,new Vector3(this.gameObject.transform.position.x+300,this.gameObject.transform.position.y+225,this.gameObject.transform.position.z-100),transform.rotation) as GameObject;			
		}else{
			atkObj = Instantiate(atkEft,new Vector3(this.gameObject.transform.position.x-300,this.gameObject.transform.position.y+225,this.gameObject.transform.position.z-100),transform.rotation) as GameObject;						
			atkObj.transform.localScale -= new Vector3(2,0,0);
		}
	}
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	private void initCopyObj (){
		copyObj.Add("COWBOY",cowboy);
		copyObj.Add("HEALER",healer);
		copyObj.Add("MARINE",marine);
		copyObj.Add("TANK",tank);
		copyObj.Add("TRAINER",trainer);
		copyObj.Add("PRIEST",priest);
		copyObj.Add("WIZARD",wizard);	
		copyObj.Add("DRUID",druid);
	}
	public static Enemy getAddHpObj (){
		if(copyHero.Count > 0){ 
			int hp;
			hp = 1000;
			Enemy addHpObj = null;
			foreach( string key in copyHero.Keys)
			{ 
			        GameObject enemyObject = copyHero[key] as GameObject;
					Enemy enemy = enemyObject.GetComponent<Enemy>(); 
					if(enemy.data.type == HeroData.HEALER){
						continue;
					}
					if(! enemy.getIsDead()){
//						print("enemy.realDef:"+enemy.realDef+"  enemy.data.type:"+ enemy.data.type+"   def:"+hp);
						if(enemy.realHp < hp){
							hp=enemy.realHp;
							addHpObj=enemy;
						} 
					}
					
			}
			return addHpObj;
		}
		return null;
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		base.atkAnimaScript("");
	}
	
	private void startCopyHero (){
	    standby(); 
		Hashtable heroMgr = HeroMgr.heroHash;
		ArrayList heroName = new ArrayList();
		if(heroMgr.Count > 0){    
			if(copyHeroManager.Count > 0){
				foreach( DictionaryEntry tempHero in copyHeroManager)
				{ 
					 Hero hero = HeroMgr.getHeroByType(tempHero.Key as string);
					 if(hero == null){
//					 	copyHeroManager.Remove(tempHero.Key);
					 	heroName.Add(tempHero.Key);
					 }
				}
			}
		}
		if(heroName.Count > 0){
			for(int k = 0;k < heroName.Count;k++){
				string str = heroName[k] as string;
				copyHeroManager.Remove(str);
			}
		}
		if(copyHeroManager.Count > 0){ 
				for(int i = 0; i < copyHeroManager.Count;){
					 if(num == 2){
					 	break;
					 }
					 if(copyHeroManager.Count == 0){
					 	break;
					 }
					 Hero heroObj = null;  
					 GameObject copyObject = null;		
					 int length = copyHeroManager.Count;
					 int randomID = int.Parse((Random.value*length).ToString());
					 int index = 0;
					 foreach( DictionaryEntry tempHero in copyHeroManager)
					 {
						if(index == randomID)
						{
							copyObject = tempHero.Value as GameObject;  
							heroObj = HeroMgr.getHeroByType(tempHero.Key as string); 
							break;
						}
						index++;
	   				 }
	   				 HeroData heroD = (heroObj.data as HeroData); 			
				 	 Enemy enemy = copyObject.GetComponent<Enemy>();
				 	 enemy.model.renderer.material.SetColor("_Color",new Color(0.3f, 0.3f, 0.3f, 1.0f));
				 	 CharacterData cloneHeroD = (heroD.clone() as CharacterData);
					 enemy.initData(cloneHeroD);
					 enemy.enemyType = "enemy"+heroObj.data.type;
					 enemy.relive();
//					 enemy.realAtk = (int)(heroObj.realAtk * 1.5f);
//					 enemy.realDef = (int)(heroObj.realDef * 0.8f);
					 copyHero.Add(heroObj.data.type,copyObject);
					 copyHeroManager.Remove(heroObj.data.type);
					 num++; 		
				}  	
		}else{
//			copyNum ++;
//			 if(this.isDead){
//			 	return;
//			 }
//			 int hpNum = realMaxHp/3;
//			 addHp(-hpNum);
//			 if(realHp<= 0 || copyNum == 3)
//			 {
//				dead();
//				return;
//			 }
			 isCopy = false;
			 Vector2 v2 = getBirthPt(); 
			 Vector3 ve = new Vector3(v2.x, v2.y,-80);
			 this.gameObject.transform.position = ve;
			 this.gameObject.collider.enabled = true; 
			 playAnim("Move");
			 startAtkHero(); 
			 createEnemy();
//			 Invoke("skillStart", 5);
		}
	}
	
	public void startAtkHero (){
		Hero hero;
		if( StaticData.computeChance(50, 100) )
		{
			hero = HeroMgr.getDefMaxHero();
		}else{
			hero = HeroMgr.getRandomHero();
		}
		
		if(hero)
		{
			if (hero.gameObject.tag == "Player") {
				moveToTarget(hero.gameObject);
			}else {
				checkOpponent();
			}
		}
	}
	public override void realDamage ( int dam  ){
		if(isCopy) return;
		base.realDamage(dam);
	}
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		if(isDead)return 0;
		if(isCopy) return 0;
		//int dam = Mathf.Max(1, damage - realDef);
		int dam =  base.getDamageValue(damage);
		
//		ConsoleObj.instance.showInfo(atkerObj.name+"_AtkValue:"+damage+" "+gameObject.name+"_RealDef:"+realDef+" damage:"+damage+"-"+realDef+"="+dam);
		realHp -= dam;
		showHpBar();
		if(realHp<= 0)
		{
			dead();
		}else if(realHp <= realMaxHp/3*copyNum && copyNum > 0){
			copyNum --;
			skillStart();
		}else{
			if (!isPlayAtkAnim 
					&& state != CAST_STATE 
					&& state != MOVE_TARGET_STATE 
					&& state != MOVE_TARGET_DIRECTLY_STATE
					&& state != MOVE_STATE )
			{
				playAnim("Damage");
			}
		}
//		if(abnormalState != ABNORMAL_NUM.FEAR)
//		{
//			checkAtkerDefense(atkerObj);
//		}
		return dam;
	}
	
//	protected function playAnim( string name  )
//	{
////		Debug.Log(name+"<xingyihua>"+isCopy);
//		if(name != "Skill" && isCopy){
//			if(name != "Death" ){
//				return;
//			}
//		}
//		super.playAnim(name);
//	}	
	private void createEnemy (){
		Hashtable heroMgr = HeroMgr.heroHash;
		foreach(string key in heroMgr.Keys){
			Vector2 v2 = getBirthPt(); 
			Vector3 ve = new Vector3(v2.x, v2.y,-80);
			Hero hero = heroMgr[key] as Hero;
			GameObject enemyObj = Instantiate(copyObj[hero.data.type] as Object,ve,transform.rotation) as  GameObject;
			copyHeroManager.Add(hero.data.type,enemyObj);
		}
	}
	private void skillStart (){
		 if(! isDead){
		 	 isCopy = true;
			 standby(); 
			 playAnim("Skill");
			 MusicManager.playEffectMusic("boss_doppelgangers_skill");
			 GameObject skillObj = Instantiate(skillEft,new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z-100),transform.rotation) as GameObject;
		 }
	}
	private void startAgainCopyHero (string s){
		if(! this.getIsDead()){
//			isCopy = true;
			birthPt = getBirthPt();
			this.gameObject.transform.position = birthPt; 
			this.gameObject.collider.enabled = false;	
			Invoke("againCopyHero", 1.5f);
		 }
	}
	private void againCopyHero (){
		 if(! this.getIsDead()){
		    standby(); 
		 	copyHero.Clear();
		 	startCopyHero();
		 }
	} 
	 
	public override void relive (){	
		gameObject.transform.position = birthPt;
		setDepth();
		hpBar.resetView();
		isDead = false;
		data.isDead = false;
		initData(data);
		this.gameObject.collider.enabled = false;
		toggleEnable();
		standby(); 
		startCopyHero() ;
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
				break;
			default:
				break;
		}
	}
	public void copyHeroDead ( Message msg  ){
		num -- ;  
		string key = msg.data as string;
		GameObject gameObj =  copyHero[key] as GameObject;
		Enemy obj = gameObj.GetComponent<Enemy>();  
		obj.isDead = true;
		if(num == 0){
			startCopyHero();
		}
	}
	void OnDestroy (){
		copyHero.Clear(); 
		copyObj.Clear();
		MsgCenter.instance.removeListener(MsgCenter.COPY_HERO_DEAD, copyHeroDead);
	}
	
	public override void dead (string s=null){
		base.dead();
		finalBossAchievement();
	}
}
