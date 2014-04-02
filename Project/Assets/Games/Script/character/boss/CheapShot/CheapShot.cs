using UnityEngine;
using System.Collections;

public class CheapShot : EnemyRemote {
	public static string MSG_MINEBOMB = "MSG_MINEBOMB";
	public Hashtable heroes;
	public GameObject minePrb;
	private GameObject instanceMine;
	private ArrayList funcs=new ArrayList() {"Skill","Attack"}; 
	public bool  isStop;
	private Hero atkHero;
	private Vector3 bulletPosition;
	private Vector3 endVc3; 
	public GameObject atkHitEftPrb;
	
	// skill attack flash
	public GameObject atkEft;
	public GameObject skillF;
	public GameObject skillLight;
	private GameObject lightGameObject;
	private ArrayList mineAry;
	
	public override void Awake (){
base.Awake();
		mineAry = new ArrayList();
		MsgCenter.instance.addListener(MSG_MINEBOMB,mineBomb);  
		MsgCenter.instance.addListener(MsgCenter.LEVEL_DEFEAT,LevelDefeat); 
	}
	
	public override void Start (){
		birthPt = getBirthPt();
		pieceAnima.addFrameScript("Attack",28,getAtkLight);  
//		pieceAnima.addFrameScript("Attack",28,atkAnimaScript);
		pieceAnima.addFrameScript("Skill",13,putMine);
		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
		CharacterData characterD = new CharacterData();
//		pieceAnima.pauseAnima();
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	public override void relive (){	
base.relive();
		randomRun(); 
	}
	
	public void changeDirection (){
		if(targetObj == null)
		{
			getNewTarget();
		}
		if(targetObj == null)
		{
			return;
		}
		atkHero = targetObj.GetComponent<Hero>();
		if(atkHero.getIsDead())
		{
			getNewTarget();
		}
		if(atkHero)
		{
			if(model.transform.localScale.x > 0 && (model.transform.position.x > atkHero.transform.position.x))
			{
				model.transform.localScale += new Vector3(-0.8f,0,0);
//				model.transform.localScale.x = -0.8f;
			}
			if(model.transform.localScale.x < 0 && (model.transform.position.x < atkHero.transform.position.x))
			{
				model.transform.localScale += new Vector3(0.8f,0,0);
//				model.transform.localScale.x = 0.8f;
			}
		}
	}
	
	protected override void atkAnimaScript (string s){ 
		CancelInvoke("atkAnimaScript");
		Destroy(lightGameObject);
		pieceAnima.restart();
//		if(targetObj == null)
//		{
//			getNewTarget();
//		}
//		atkHero = targetObj.GetComponent<Hero>();
//		if(atkHero.getIsDead())
//		{
//			getNewTarget();
//		}
//		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		if(atkHero != null)
		{
//			if(model.transform.localScale.x > 0 && (model.transform.position.x > atkHero.transform.position.x))
//			{
//				model.transform.localScale.x = -0.8f;
//			}
//			if(model.transform.localScale.x < 0 && (model.transform.position.x < atkHero.transform.position.x))
//			{
//				model.transform.localScale.x = 0.8f;
//			}
			getEft();
			MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
			endVc3 = targetObj.transform.position+ new Vector3(0,70,0);
			shootBullet(bulletPosition, endVc3);
		}
	} 
	
	public void getAtkLight (string s){
		pieceAnima.pauseAnima();
		if(model.transform.localScale.x > 0)
    	{
    		bulletPosition = new Vector3(transform.position.x+150,transform.position.y+70,transform.position.z-50);
    	}else{
    		bulletPosition = new Vector3(transform.position.x-150,transform.position.y+70,transform.position.z-50);
    	}
		lightGameObject = Instantiate(skillLight,bulletPosition,transform.rotation) as GameObject;
		Invoke("atkAnimaScript",1);
	}
	
    public void getEft (){
//    	Destroy(lightGameObject);
    	GameObject tempAtkEft = null;
		PackedSprite tempAtkEftInfo;
    	if(model.transform.localScale.x > 0)
    	{
    		bulletPosition = new Vector3(transform.position.x+150,transform.position.y+70,transform.position.z-50);
    		tempAtkEft = Instantiate(atkEft,bulletPosition,transform.rotation) as  GameObject;
			tempAtkEftInfo = tempAtkEft.GetComponent<PackedSprite>();
			tempAtkEftInfo.PlayAnim("eft"); 
    	}else{
    		bulletPosition = new Vector3(transform.position.x-150,transform.position.y+70,transform.position.z-50);
    		tempAtkEft = Instantiate(atkEft,bulletPosition,transform.rotation) as  GameObject;
			tempAtkEftInfo = tempAtkEft.GetComponent<PackedSprite>();
			tempAtkEftInfo.transform.localScale = new Vector3(-1, tempAtkEftInfo.transform.localScale.y, tempAtkEftInfo.transform.localScale.z);
//			tempAtkEftInfo.transform.localScale.x = -1;
			tempAtkEftInfo.PlayAnim("eft"); 
    	}
    }

	public void getNewTarget (){
		Hero hero = HeroMgr.getRandomHero();
		if(hero != null)
		{
			targetObj = hero.gameObject;
		}
	}
	
	public override void moveToTarget ( GameObject obj  ){	
		if(state == ATK_STATE)
		{
			cancelAtk();
		}
		if(isDead)return;//add by xiaoyong 20120729  for enemy relive
		//xingyh 4.9f enemy out of the screen
		if( targetObj != null)
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		playAnim("Move");
		state = MOVE_TARGET_STATE;
		bool  b = isCollider();
		if(!b){
			targetObj = null;
			standby();
			return;
		}
	}
	
	protected override void removeBullet (GameObject bltObjTemp){
base.removeBullet(bltObjTemp);
		Destroy(bltObjTemp);
		atkHitEftPrb.gameObject.transform.localScale = new Vector3(4.5f,4.5f,1);
		GameObject hitEft = Instantiate(atkHitEftPrb,new Vector3(targetObj.transform.position.x,targetObj.transform.position.y,targetObj.transform.position.z-10),targetObj.transform.rotation) as GameObject;
		bulletScope();
	}
	
	public void putMine (string s){
//		state = CAST_STATE;
		GameObject tempSkillF= Instantiate(skillF, new Vector3(transform.position.x,transform.position.y,transform.position.z-10),transform.rotation) as GameObject;
		MusicManager.playEffectMusic("boss_cheapShoot_skill");
		instanceMine=Instantiate(minePrb,transform.position,transform.rotation) as GameObject;
		mineAry.Add(instanceMine);
	}
	
	public void mineBomb ( Message msg  ){ 
		Mine mineTemp = msg.sender as Mine;
		mineAry.Remove( mineTemp.gameObject);
		mineScope(mineTemp.gameObject);
	} 
	
	public void LevelDefeat ( Message msg  ){
		isStop = true;
	}
	
	public void destoryMine (){
//		GameObject[] Mines = GameObject.FindGameObjectsWithTag ("Mine");
		for (int i=0; i< mineAry.Count; i++)
		{
//			Destroy(Mines[i]); 
			GameObject mineObj =  mineAry[i] as GameObject;
			Destroy(mineObj); 
//			Mine mine = mineAry[i].transform.GetComponent<Mine>();
//			mine.killThis();
		}
	} 
	
	public void mineScope ( GameObject mine  ){	 
		Vector2 scope=new Vector2(200,200); 
		foreach(string key in heroes.Keys)
		{
			Hero hero = heroes[key] as  Hero; 
			if(hero != null){
				if(Mathf.Abs(mine.transform.position.x-hero.transform.position.x)<scope.x && Mathf.Abs(mine.transform.position.y-hero.transform.position.y)<scope.y)
				{ 
//					hero.defenseAtk(realAtk*5,this.gameObject);
				}
			}
		}
	}
	
		
	public void bulletScope (){
		Vector2 scope = new Vector2(200,200); 
		foreach(string key in heroes.Keys)
		{
			Hero hero = heroes[key] as Hero; 
			if(hero != null)
			{
				if(Mathf.Abs(atkHero.transform.position.x-hero.transform.position.x)<scope.x && Mathf.Abs(atkHero.transform.position.y-hero.transform.position.y)<scope.y)
				{
					hero.defenseAtk(realAtk,this.gameObject);
				}
			}
		}
	}

	public void randomRun (){  
//		int maxX = screen.x/2-screen.x/6;
//		int minX = -screen.x/2+screen.x/6;
//		int maxY = screen.y/2-screen.y/6;
//		int minY = -screen.y/2+screen.y/6; 
//		Vector3 randomPosition = Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),transform.position.y/10 + GData.objLayer);
//		Debug.Log("getMaxPointInScreen:"+BattleBg.getMaxPointInScreen());
		Vector3 randomPosition = BattleBg.getPointInScreen();
		move(randomPosition);
	} 
	
	protected override void moveStateUpdate (){
		float dis = Vector2.Distance(transform.position,targetPt);
		float spd = Time.deltaTime*realMspd;
		if(Mathf.Abs(dis)>spd){
			if(dirVc3.y != 0){
				setDepth();
			}
			transform.Translate(dirVc3*spd);
		}else{ 
			standby();
			if(!isStop)
			{
				changeDirection();
				isPlayAtkAnim = true;
				playAnim(funcs[Random.Range(0,funcs.Count)].ToString());
//				playAnim("Skill");
			}  
		}
	}
	
//checkOvalCollision (heroWidth, heroWidth/2,heroLocation,thunderRadius*2, thunderRadius,bltObj)
//	function checkOvalCollision ( float ovalAWidth ,   float ovalAHeight ,   Vector2 ovalACenter ,   float ovalBWidth ,   float ovalBHeight ,   Vector2 ovalBCenter  ):bool 
 //	{
//		FIXME_VAR_TYPE deltaX= (ovalACenter.x - ovalBCenter.x);
//		FIXME_VAR_TYPE deltaY= (ovalACenter.y - ovalBCenter.y);
//		FIXME_VAR_TYPE lineRatio= deltaY/deltaX;
//		FIXME_VAR_TYPE ratioA= ovalAWidth/ovalAHeight;
//		FIXME_VAR_TYPE radiusA= (ovalAWidth / 2)*Mathf.Sqrt((1 + lineRatio*lineRatio) / (1 + lineRatio*lineRatio*ratioA*ratioA));
//		FIXME_VAR_TYPE ratioB= ovalBWidth/ovalBHeight;
//		FIXME_VAR_TYPE radiusB= (ovalBWidth / 2)*Mathf.Sqrt((1 + lineRatio*lineRatio) / (1 + lineRatio*lineRatio*ratioB*ratioB));
//		FIXME_VAR_TYPE sumRadius= radiusA + radiusB;
//		if (Vector2.Distance(ovalACenter, ovalBCenter) < sumRadius) {
//			return true;
//		}
//		else {
//			return false;
//		}
//	}

	public override void Update (){
base.Update();
	}
	
 	protected override void AnimaPlayEnd ( string animaName  ){
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Move");
				randomRun();
				break;
			case "Damage":
				playAnim("Move");
				break;
			case "Skill":
				playAnim("Move");
				randomRun();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				Invoke("destroyThis",3);
				break;
			default:
				break;
		}
	}
	
	 public void OnDestroy (){ 
		heroes.Clear();
		MsgCenter.instance.removeListener(MsgCenter.LEVEL_DEFEAT,LevelDefeat); 
		MsgCenter.instance.removeListener(MSG_MINEBOMB,mineBomb);
	}
	
	public override void dead (string s=null){
		if(IsInvoking())
		{
			CancelInvoke("atkAnimaScript");
		}
base.dead();
		if(lightGameObject)
		{
			Destroy(lightGameObject);
		}
		destoryMine();
		miniBossAchievement();
	}
}
