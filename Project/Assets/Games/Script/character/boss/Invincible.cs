using UnityEngine;
using System.Collections;

public class Invincible : Enemy {
/*
	Invincible:
		Immune to player damage; can only be damaged by thunder hits.
*/

	public GameObject thunder;
	public GameObject thunderLocIndicator;
	public GameObject deathEffect;
	public GameObject atkEft;
	
	//public GameObject skEft;
	private Hashtable hitedTargets;
	private Hashtable heroes;
	
	private float thunderRadius = 150.0f;
	private float screenWidth = 200.0f;
	private float screenHeight = 200.0f;
	private int num = 0;
	//private GameObject indicator;
	private bool  shouldCastThunder = true;
	
	private int canEndureThunderTimes;// = 10;//how many hit invincible can take before he dies
	
	private float laserHitDelay = 5.0f;
	private float laserHitInterval = 7.5f;
	
	public override void Awake (){
//		birthPts = [500,50];
		base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 21;
		//indicator = Instantiate(thunderLocIndicator, Vector3(0.0f,0.0f,100.0f), gameObject.transform.rotation);
		MsgCenter.instance.addListener(MsgCenter.LEVEL_DEFEAT, stopCastThunder);
		shouldCastThunder = false;
		
	}
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Attack",20,atkLightEffect); 
		pieceAnima.addFrameScript("Death",11,deadEft); 
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
//	public override void fearWithSeconds ( int seconds  ){}
	
	public void atkLightEffect ( string s){
		Vector3 ve;
		if(model.transform.localScale.x > 0){
			ve = new Vector3(10,170,-1);
		}
		else{
			ve = new Vector3(-10,170,-1);
		}
		Vector3 pt = targetObj.transform.position + ve;
		MusicManager.playEffectMusic("boss_empireGeneral_skill");
		GameObject atkEftObj = Instantiate(atkEft, pt, this.transform.rotation) as GameObject;
	}
	private void stopCastThunder (Message m){
		hideIndicator();
		shouldCastThunder = false;
	}
	
	public IEnumerator thunderGenerator ( float delay ,   float interval  ){
		yield return new WaitForSeconds(delay);
		specialAtk();
		while (!isDead) {
			yield return new WaitForSeconds(interval);
			specialAtk();
		}
	}
	
	public override void relive (){
		base.relive();
		//showHpBar();
		StartCoroutine(thunderGenerator(0,laserHitInterval));
		shouldCastThunder = true;

		pieceAnima.animaPlayEndScript(AnimaPlayEnd);
//		canEndureThunderTimes = realDef;
	}
	
	private Vector3 randomThunderLocation (){
		float hitLocX= Random.Range(-500.0f, 400.0f);
		float hitLocY= Random.Range(-200.0f, 100.0f);
		
		float hitLocZ = -0.5f; //indicator always behind enemy texture
		Vector3 hitLocation = new Vector3(hitLocX, hitLocY, hitLocZ);
		
		return hitLocation;
	}
	
	private void displayThunderEffect ( Vector3 hitLocation  ){
		float yOffset= hitLocation.y - gameObject.transform.position.y;
		Vector3 thunderLocation = new Vector3(hitLocation.x - 10.0f, hitLocation.y + 125.0f, hitLocation.y/10 + StaticData.objLayer); // y&z offset adjusted to make thunder looks natural
		GameObject thunderEffect = Instantiate(thunder, thunderLocation, gameObject.transform.rotation) as GameObject;
	}
	
	/*
		function:checkOvalCollision(ovalAWidth, ovalAHeight, ovalACenter,
						   ovalBWidth, ovalBHeight, ovalBCenter)
		!!!!CORRECT ONLY WHEN:
			1. oval isn't rotated
			2. width/height ratio : A == B
	*/
	private bool checkOvalCollision ( float ovalAWidth ,   float ovalAHeight ,   Vector2 ovalACenter ,   float ovalBWidth ,   float ovalBHeight ,   Vector2 ovalBCenter  ){
		 float deltaX= (ovalACenter.x - ovalBCenter.x);
		float deltaY= (ovalACenter.y - ovalBCenter.y);
		
		float lineRatio= deltaY/deltaX;
		
		float ratioA= ovalAWidth/ovalAHeight;
		float radiusA= (ovalAWidth / 2)*Mathf.Sqrt((1 + lineRatio*lineRatio) / (1 + lineRatio*lineRatio*ratioA*ratioA));
		
		float ratioB= ovalBWidth/ovalBHeight;
		float radiusB= (ovalBWidth / 2)*Mathf.Sqrt((1 + lineRatio*lineRatio) / (1 + lineRatio*lineRatio*ratioB*ratioB));
		
		float sumRadius= radiusA + radiusB;

		if (Vector2.Distance(ovalACenter, ovalBCenter) < sumRadius) {
			return true;
		}
		else {
			return false;
		}
		
	}
	
	private bool thunderHitSelf ( Vector3 thunderLocation  ){
		Vector2 selfLocation = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
		Vector2 hitLocation = new Vector2(thunderLocation.x, thunderLocation.y);

		float bossWidth = 250.0f;
		return checkOvalCollision(bossWidth, bossWidth/2, selfLocation, thunderRadius*2, thunderRadius, hitLocation);
	}
	
	private void handleThunderHit ( Vector2 hitLocation  ){
		if (thunderHitSelf(new Vector3(hitLocation.x, hitLocation.y, 0))) {
			thunderDamage((data.maxHp/canEndureThunderTimes) + 1);
		}
		
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		
		foreach( string key in heroes.Keys)
		{
			Hero hero = heroes[key] as Hero;

			float heroWidth = hero.gameObject.collider.bounds.size.x;
			Vector2 heroLocation = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y);
			
			if(checkOvalCollision(heroWidth, heroWidth/2, heroLocation, thunderRadius*2, thunderRadius, hitLocation) )
			{
				hero.realDamage(50);//realDamage: prevent hero from losing current target or other unintended behaviours.			
			}
		}
		
	}
	
	private void hideIndicator (){
		//indicator.transform.position = Vector3(0.0f,0.0f,100.0f);
		
	}
	
	private void destroyIndicator ( GameObject indicator  ){
		Destroy(indicator);
	}
	
	private IEnumerator generateThunder (){
		
		Vector3 hitLocation = randomThunderLocation();
		
		if ((!isDead) && (shouldCastThunder)){
			GameObject indicator = Instantiate(thunderLocIndicator, hitLocation, gameObject.transform.rotation) as GameObject;
			iTween.ScaleTo(indicator, new Hashtable(){{"scale", new Vector3(0.1f,0.1f,0.1f)},{ "time",laserHitDelay},{"easetype","linear"},{ "oncomplete","destroyIndicator"},{ "oncompleteparams",indicator},{ "oncompletetarget",gameObject}});
			//indicator.transform.position = hitLocation;
			yield return new WaitForSeconds(0.8f);
			MusicManager.playEffectMusic("boss_empireGeneral_eft");
		}
		
		yield return new WaitForSeconds(laserHitDelay-0.8f);
		hideIndicator();
		
		if ((!isDead) && (shouldCastThunder)){
			displayThunderEffect(hitLocation);
			MusicManager.playEffectMusic("boss_empireGeneral_dead");
			handleThunderHit(hitLocation);	
		}
	}
		
	public void specialAtk (){
		StartCoroutine(generateThunder());
	}
	
	public override void dead (string s=null){
		base.dead();
		hideIndicator();
		finalBossAchievement();
		//AchievementManager.updateAchievement("BEAT_MINI_3", 1);
	
	}
	public void deadEft (string s){
		if(num == 2){
			MusicManager.playEffectMusic("boss_empireGeneral_dead");
			Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
		}else{
			num++;
			playAnim("Death");
		}
	}
	
	public override int defenseAtk( Vector6 damage ,   GameObject atkerObj  )//override
	{
		int dmg =0;
//		dmg = base.defenseAtk(0, atkerObj);
		base.checkAtkerDefense(atkerObj);
		return dmg;
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
				playAnim("Move");
				break;
			case "Death":
				//Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
				//destroyThis();
				pieceAnima.pauseAnima();
				Invoke("destroyThis",0.2f);
				break;
			default:
				break;
		}
	}
	
	
	void OnDestroy (){
		//Destroy(indicator);
		MsgCenter.instance.removeListener(MsgCenter.LEVEL_DEFEAT, stopCastThunder);
	}
	
	public override void realDamage ( int dam  ){
		Debug.Log("Allahu Akbar! Allahu Akbar!");
	}
	
	public void thunderDamage ( int dam  ){
		base.realDamage(dam);
	}
}
