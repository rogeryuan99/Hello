using UnityEngine;
using System.Collections;

public class Healer : Hero {
	public GameObject healPrb;
	public GameObject deadEft;
	public GameObject damageEft;
	public bool  isControlled;
	public Hashtable heroes;
	
	//gwp
	public bool  isSputtering;
	public bool  isAddAtk;
	public bool  isAddDef;
	
	public int addAtkValue;
	public int addDefValue;
	public bool  isDown = false;
	//gwp end
	
	public GameObject controlledAnimation;
	public GameObject controlledIndicator;
	public GameObject indicator;
	
	public override void Awake (){
		base.Awake();
		scaleSize = new Vector3(0.65f, 0.65f, 1);
		isInvincible=false;
		isSputtering=false;
		isAddAtk=false;
		isAddDef=false;
	}
	
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Death",22,deadLightEffect); 
		pieceAnima.addFrameScript("Damage",3,damageLightEffect); 
	}
	
	
	public override void relive (){
		base.relive();
		releaseFromControl();
	}
	public void damageLightEffect (string s){
		Vector3 pt = this.gameObject.transform.position + new Vector3(10,70,-1);
		GameObject damageEftObj = Instantiate(damageEft, pt, this.transform.rotation) as GameObject;
	}
	public void deadLightEffect (string s){
		Vector3 pt = this.gameObject.transform.position + new Vector3(-40,70,0);
		GameObject deadEftObj = Instantiate(deadEft, pt, this.transform.rotation) as GameObject;
	}
	public void startHeal ( GameObject gameObj  ){
		Hero hero = gameObj.GetComponent<Hero>();
		startHeal(hero);
	}
	
	public void startHeal ( Hero hero  )
	{
		if(this.isDead){
			return;	
		}
		//gwp 
		GameObject tempTarget = targetObj;
		Hero tempTargetData = null;
		//gwp end
		
		state = ATK_STATE;
		targetObj = hero.gameObject;
		
		//gwp 
		if(tempTarget != null){
			tempTargetData = tempTarget.GetComponent<Hero>();
			if(!tempTargetData.getIsDead()){
				if(isAddAtk){
//					tempTargetData.addAtk(-addAtkValue);	
				}
				if(isAddDef){
//					tempTargetData.addDef(-addDefValue);
				}
			}
		}
		//gwp end
		
		if(!hero.getIsDead())
		{
			if(isAddAtk){
//				addAtkValue = hero.realAtk*25/100;
//				hero.addAtk(addAtkValue);	
			}
			if(isAddDef){
//				addDefValue = hero.realDef*25/100;
//				hero.addDef(addDefValue);
			}
			
			if( !IsInvoking("healTarget") )
			{
				healTarget();
				CancelInvoke("healTarget");
				InvokeRepeating("healTarget", realAspd, realAspd);
			}
		}
	}
	
	//gwp override
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		if(isInvincible){
			return 0;
		}
		
		 int dam = base.defenseAtk(damage, atkerObj);
		 if(isDown){
		 	hpBar.hideHpBar();
//		 	if(TutorialBattleBg.getIsTutorial() && TutorialBattleBg.taskState == "ATTACK_ATTACK_ENEMY_B")
//		 	{
//		 		hpBar.gameObject.SetActiveRecursively(true);
//		 		hpBar.ChangeHp(realHp);
//		 	}
		 }
		 return dam;
	}
	public override void realDamage ( int dam  ){
		if(isInvincible){
			return;
		}
		base.realDamage(dam);
	}
	//gwp end
	
	public void releaseFromControl (){
		isControlled = false;
		gameObject.transform.tag = "Player";
		if (indicator) {
			Destroy(indicator);
		} 
		foreach(Transform child in transform)
		{
			if (child.name == "healbot") {
				child.renderer.material.mainTexture = Resources.Load("HealbotMaterials/m_heroHealbotNormal") as Texture;
			}
		}
		standby();
	}
	
	public void becomeControlled (){
		isControlled = true;
		gameObject.transform.tag = "ChestBox";
		standby();
		foreach(Transform child in transform)
		{
			if (child.name == "healbot") {
				
				child.renderer.material.mainTexture = Resources.Load("HealbotMaterials/m_heroHealbotRed") as Texture;
				//("_MainTex", Texture2D.)
			}
		}
//		this.renderer.material.SetColor
		
		GameObject animation= Instantiate(controlledAnimation, this.transform.position, this.transform.rotation) as GameObject;
		animation.transform.parent = this.gameObject.transform;
		animation.transform.localPosition = new Vector3(8, 150, -10);
		
		indicator = Instantiate(controlledIndicator, this.transform.position, this.transform.rotation) as GameObject;
		indicator.transform.parent = this.gameObject.transform;
		indicator.transform.localPosition = new Vector3(8, 150, -10);
		healRandomHero();
		Invoke("releaseFromControl", 20);
	}
	
	public void healRandomHero (){
		heroes = HeroMgr.heroHash.Clone() as Hashtable;
		Debug.Log("heroes:" + heroes.Count);
		if (heroes.Count > 1) {
			Debug.Log("more than 1 heroes");
			Hero hero = HeroMgr.getRandomHero();
			while (hero.gameObject.Equals(gameObject)) {
				hero = HeroMgr.getRandomHero();
			}
			
			startHeal(hero);
			//targetObj = hero.gameObject;
		}
	}
	
	//override
	public override string getTargetTagType (){
		return "Player";
	}
	//override
	public override void move ( Vector3 vc3  ){
		targetPt = vc3;
		state = MOVE_STATE;
		playAnim("Move");
		setDirection(vc3);
		toward(vc3);
	}
	
	//override
	public override void setTarget ( GameObject gObj  ){
//		if(TutorialBattleBg.getIsTutorial())
//		{
//			base.setTarget(gObj);
//		}
	}
	
	public void stopHeal ()
	{
		targetObj = null;
	}
	
	public void stopMoving ()
	{
		if(state != ATK_STATE)
		{
			standby();
		}
	}
	
	protected override void initRealAtk ( HeroData heroD  )
	{
//		realAtk =(int)( ( heroD.attack+heroD.getWeaponValue() )*(heroD.itemDM.atk/100.0f +1)*(heroD.skillDM.atk/100.0f +1));
	}
	
	// weapon change test
	
	public override void changeWeapon ( string weaponID  ){
		pieceAnima.showPiece("sash",weaponID);
	}
	
	public override void startCheckOpponent (){}
	protected override void moveTargetInAtkUpdate (){}
	
	protected override void autoSelectTarget(GameObject atkerObj){}

	public override void standby (){
		if(StaticData.isBattleEnd){
			float dis = Vector2.Distance(transform.position,targetPt);
			float spd = Time.deltaTime*realMspd;
			if(Mathf.Abs(dis)>spd){
				move(targetPt);
				return;
			}
		}
		if(state == ATK_STATE)
		{
			CancelInvoke("healTarget");
		}
		state = STANDBY_STATE;
		doAnim("Stand");
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("atk_healer");
		Debug.Log("targetObj="+targetObj.name);
		Hero hero = targetObj.GetComponent<Hero>();
		if (isControlled == true) {
//			hero.realDamage(realAtk);
			Debug.Log("hero lost HP:" + realAtk);
		}
		else {
//			hero.addHp(realAtk);
			Debug.Log("hero add HP :" + realAtk);
		}
		if(isSputtering){
			sputteringEft();
		}
		Vector3 pt = hero.transform.position + new Vector3(0,-20,-5);
		GameObject healingObj = Instantiate(healPrb, pt, this.transform.rotation) as GameObject;
		if (isControlled == true) {
			healingObj.renderer.material.SetColor("_Color",new Color(0.5f, 0.1f, 0.1f, 1.0f));
		}
	}
	//gwp sputtering eft
	private void sputteringEft (){
		Hero tempHero = null;
		Vector3 vc3;
		foreach( DictionaryEntry tempEntry in HeroMgr.heroHash)
		{
			tempHero = tempEntry.Value as Hero;
			vc3 = tempHero.transform.position - transform.position;
			if(StaticData.isInOval(300,150,vc3)){
				if (isControlled == true) {
//					tempHero.realDamage(realAtk/5);
				}
				else {
//					tempHero.addHp(realAtk/5);
				}
			}
		}
	}
	//gwp end
	
	protected override void initSkill (){
	
		HeroData heroData = (data as HeroData);
		
		//gwp passiveLis
//		if(heroData.getPSkillByID("HEALER4") != null){
//			isSputtering = true;
//		}
//		if(heroData.getPSkillByID("HEALER7") != null){
//			isAddAtk = true;
//		}
//		if(heroData.getPSkillByID("HEALER8") != null){
//			isAddDef = true;
//		}
//		if(heroData.getPSkillByID("HEALER11") != null){
//			for(int i=0; i<heroData.skillListBattle.Count; i++){
//				SkillData tempSKD = heroData.skillListBattle[i] as SkillData;
//					int cd = (int)tempSKD.CDTime;
//					tempSKD.CDTime = (cd -10 == 0? 0 : cd -10);
//			}
//		}
		//end
	}
	
	private void healTarget ()
	{
		
		if( state != MOVE_STATE && state != CAST_STATE )
		{
			
			if(targetObj != null)
			{
				Hero hero = targetObj.GetComponent<Hero>();
				if(!hero.getIsDead())
				{
					toward(targetObj.transform.position);
					isPlayAtkAnim = true;
					playAnim("Attack");
				}
				else {
					healRandomHero();
				}
			}
		}
	}
	
	public override void dead (string s=null){
		base.dead();
		CancelInvoke("healTarget");
		CancelInvoke("releaseFromControl");
		if (indicator) {
			Destroy(indicator);
		}
	}
}
