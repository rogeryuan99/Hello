using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {
	
	public static SkillManager Instance{get; set;}
	
	// private GameObject ex_line;
	// private GameObject skinit;
	
	//anim
	//wizard
	/*private GameObject wizardBomb;
	private GameObject wizardSK2_start;
	private GameObject wizardSK2_blt;
	private GameObject wizardSK2_shoot;
	private GameObject wizardSK2_boom;
	//marine
	private GameObject marineSK_A;
	private GameObject marineSK_A2;
	private GameObject marineSK_B; 
	private GameObject marineSK_B_foot; 
	
	//trainer
	private GameObject trainerSK_A;
	//priest
	private GameObject priestSK_A;
	private GameObject priestSK_B;
	private GameObject priestSK_Bp2;
	
	//healer
	private GameObject healerSK_A;
	private GameObject healerSK_B; 
	private GameObject healingEft;
	//cowboy
	private GameObject cowboySK_A;
	private GameObject cowboySK_B;
	//tank
	private GameObject tankSK_A;
	private GameObject tankLightBall;
	private GameObject tankSK_B;
	private GameObject tankAddDef;
	//druid
	[HideInInspector]
	public GameObject druidSK_A;
	private GameObject druidSK_B_1;
	private GameObject druidSK_B_2;
	
	private GameObject starLordBomb;
	
	private GameObject grootSK_A;
	private GameObject grootEft;

	private GameObject petEftPrb;
	
	public static bool  isPop = false;
	*/
	
		
	
	public UISprite screenBalck;
	
	public void callSkillPrepare (string id, ArrayList objs){
		BattleBg.canUseSkill = true;
		id = findeComboID(id);
		GameObject skillObj = GetSkillObject(id);
		SkillBase skill = GetSkillBase(skillObj);

		if(skill != null)
		{
			skill.Prepare(objs);
		}
		
	}
	
	public void callSkill (string id, ArrayList objs)
	{
		BattleBg.canUseSkill = true;
		id = findeComboID(id);
		GameObject skillObj = GetSkillObject(id);
		SkillBase skill = GetSkillBase(skillObj);
		if(skill != null)
		{
			StartCoroutine(skill.Cast(objs));
		}
	}
	
	private string findeComboID(string id){
		string[] lastStrs = new string[3]{"2","15","100"};
		for(int i = 0;i<lastStrs.Length;i++){
			string lastStr = lastStrs[i];
			int index = id.LastIndexOf(lastStrs[i]);
			if(index != -1){
				id = id.Substring(0,id.Length - lastStr.Length) + "_COMBO";
				return id;
			}
		}
		return id;
	}
	
	private GameObject GetSkillObject(string id){
		GameObject skillObj = GameObject.Find(id);
		
		if (null == skillObj){
			skillObj = new GameObject(id);
		}
		
		return skillObj;
	}
	
	private SkillBase GetSkillBase(GameObject obj){
		SkillBase skill = obj.GetComponent(string.Format("Skill_{0}", obj.name)) as SkillBase;
		if (null == skill){
			skill = obj.AddComponent(string.Format("Skill_{0}", obj.name)) as SkillBase;
			if (null == skill){
				//throw new MissingComponentException();
				
			}
		}
		
		return skill;
	}
	
	public void Awake (){
		Instance = this;
		
		screenBalck.color = new Color32(0,0,0,0);
	}
	
	public void chanageBGTextureColorByCoroutine(float delayForSeconds, float waitForSeconds){
		StartCoroutine(chanageBGTextureColor(delayForSeconds, waitForSeconds));
	}
	public IEnumerator chanageBGTextureColor(float delayForSeconds, float waitForSeconds)
	{
		yield return new WaitForSeconds(delayForSeconds);
		screenBalck.color = new Color32(0,0,0,200);
		yield return new WaitForSeconds(waitForSeconds);
		screenBalck.color = new Color32(0,0,0,0);
		
	}
	
	
	
	
	
	public void slowMotionByCoroutine(float delayForSeconds,float waitForSeconds){
		StartCoroutine(slowMotion(delayForSeconds, waitForSeconds));
	}	
	public void pauseMotionByCoroutine(float delayForSeconds,float waitForSeconds){
		StartCoroutine(pauseMotion(delayForSeconds, waitForSeconds));
	}	
	
	public void slipMotionTo(string[] parms){
		slipMotionTo(float.Parse(parms[0]), float.Parse(parms[1]), float.Parse(parms[2]));
	}
	public void slipMotionTo(float from, float to, float time){
		iTween.ValueTo(gameObject, new Hashtable(){{"from",from}, {"to",to}, {"time",time}, {"onUpdate", "updateSlipMotionTo"}});
	}
	public void updateSlipMotionTo(float value){
		Time.timeScale = value;
	}
	public IEnumerator slowMotion(float delayForSeconds,float waitForSeconds)
	{
		yield return new WaitForSeconds(delayForSeconds);
		Time.timeScale = 0.3f;
		yield return new WaitForSeconds(waitForSeconds);
		Time.timeScale = 1.0f;
	}
	public IEnumerator pauseMotion(float delayForSeconds,float waitForSeconds)
	{
		yield return new WaitForSeconds(delayForSeconds);
		Time.timeScale = 0.2f;
		yield return new WaitForSeconds(waitForSeconds);
		Time.timeScale = 1.0f;
	}
	
	
	
	
	public IEnumerator shakeCamera(Vector3 pos, float time, float waitForSeconds)
	{
		yield return new WaitForSeconds(waitForSeconds);
		Camera c = Camera.mainCamera;
		GameObject go = c.gameObject;
		iTween.PunchPosition(go,iTween.Hash("amount",pos,"time",time));
	}
	
	
	
	
	
	
	
	///#########################skill part###################################	
	/*
	protected GameObject fireBulletPrb;
	protected GameObject fireBulletTarget;
	protected int fireBlastDamage;

	public void FireBlastPre(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	
	public IEnumerator FireBlast(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		fireBulletTarget = target;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		if (heroDoc.model.transform.position.x > target.transform.position.x) {
			heroDoc.model.transform.localScale = new Vector3 (-heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1);
		} else {
			heroDoc.model.transform.localScale = new Vector3 (heroDoc.scaleSize.x, heroDoc.scaleSize.y, 1);
		}
		
		heroDoc.castSkill("SkillA");
		yield return new WaitForSeconds(0.5f);
			
		HeroData heroData = (heroDoc.data as HeroData);
		
		Hashtable tempNumber = heroData.getASkillByID("STARLORD1").number;
		int tempAtkPer = (int)tempNumber["AOENum"];
		fireBlastDamage = (int)(heroDoc.realAtk*tempAtkPer/100);
		
		Vector3 vc3 = target.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(heroDoc.model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = caller.transform.position + new Vector3(80,40,-50);
		}else{
//			print("left");
			createPt = caller.transform.position + new Vector3(-80,40,-50);
		}
			
		shootFireBullet(createPt, vc3);
	}
	
	public void shootFireBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		StartCoroutine(chanageBGTextureColor(0.0f, 0.3f));
		if(fireBulletPrb == null)
		{
			fireBulletPrb = Resources.Load("eft/StarLord/FireBlast") as GameObject;
		}
		GameObject fireBullet = Instantiate(fireBulletPrb,creatVc3, transform.rotation) as GameObject;
		fireBullet.transform.localScale = new Vector3(0.7f,1.4f,1);
		float deg = (angle*360)/(2*Mathf.PI) + 90;
		fireBullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(fireBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",fireBullet}});
	}
	
	protected virtual void removeBullet (GameObject fireBullet){
		
		Destroy(fireBullet);
		if(fireBulletTarget == null)
		{
			return;
		}
		Enemy enemy = fireBulletTarget.GetComponent<Enemy>();
		enemy.realDamage(fireBlastDamage);
		enemy.fearWithSeconds(3);
	}
	*/
	
	/*
	private ArrayList deathstrikeObjs = null;
	private const float DEATH_STRIKE_TIME = 1f;
	private const int DEATH_STRIKE_POINT_NUM = 10;
	
	public void DeathstrikePre(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	
	public IEnumerator Deathstrike(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		// Lock Everybody
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("SkillA");
		
		yield return new WaitForSeconds(0.5f);
			
		deathstrikeObjs = objs;
		heroDoc.OnMoveToTargetDirectlyFinished += Deathstrike_Punch;
		heroDoc.moveToTargetDirectly(target);
	}
	
	private void Deathstrike_Punch(){
		GameObject caller = deathstrikeObjs[1] as GameObject;
		GameObject target = deathstrikeObjs[2] as GameObject;
		
		ScreenController.Instance.EnableBlackScreen();
		// StartCoroutine("PlayDeathstrikeEft");
		PlayDeathstrikeEft();
		Invoke("Deathstrike_Finish", DEATH_STRIKE_TIME);
		target.GetComponent<Enemy>().LayDown(DEATH_STRIKE_TIME + 1f);
		
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.OnMoveToTargetDirectlyFinished -= Deathstrike_Punch;
	}
	private void Deathstrike_Finish(){
//		GameObject scene  = deathstrikeObjs[0] as GameObject;
//		GameObject caller = deathstrikeObjs[1] as GameObject;
//		GameObject target = deathstrikeObjs[2] as GameObject;
//		
//		ScreenController.Instance.DisableBlackScreen();
//		Enemy enemy = target.GetComponent<Enemy>();
//		
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
//		HeroData heroData = (heroDoc.data as HeroData);
//		
//		Hashtable tempNumber = (heroData.getASkillByID("GAMORA30A") ).number;
//		enemy.realDamage((int)tempNumber["AOENum"]);
		// Unlock Everybody
	}
	
	private void PlayDeathstrikeEft(){
		GameObject target = deathstrikeObjs[2] as GameObject;
		GameObject particle = null;
		Vector3 []points = GenerateDeathstrikePoints(target, DEATH_STRIKE_POINT_NUM);
		
		particle = LoadDeathstrikeParticle();
		particle.transform.position = points[0];
		float delay = 0f;
		float time = 0f;
		for (int i=1; i<DEATH_STRIKE_POINT_NUM; i++){
			time = Random.Range(.05f, .1f);
			iTween.MoveTo(particle, new Hashtable(){{"x",points[i].x}, {"y",points[i].y}, {"time",time}, {"delay",delay}});
			delay += time;
		}
		// Destroy(particle);
	}
	private GameObject LoadDeathstrikeParticle(){
		Object prefab = Resources.Load("eft/DeathstrikeEft");
		GameObject particle = Instantiate(prefab) as GameObject;
		// particle.transform.parent = GameObject.Find("UIRoot/GamePanel").transform;
		
		return particle;
	}
	private Vector3[] GenerateDeathstrikePoints(GameObject target, int num){
		Vector3 []points = new Vector3[num];
		Rect rect = new Rect(-480,-320,960,640 );
		
		for (int i=0; i<num; i++){
			points[i] = new Vector3(Random.Range(rect.xMin, rect.xMax),
										Random.Range(rect.yMin, rect.yMax),
										-100f);
			if (i>0 && 0==i%2){
				if (Vector3.Distance(points[i-1], points[i]) < 500){
					i--;
					continue;
				}
			}
		}
		
		return points;
	}
	*/
	
	/*
	public void CleavePre(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	public IEnumerator Cleave(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Gamora heroDoc = caller.GetComponent<Gamora>();
		
		
		if(Vector3.Distance(caller.transform.position, target.transform.position) > heroDoc.data.attackRange + 10.0f)
		{
			heroDoc.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("GAMORA1");// GAMORA5A
			yield break;
		}
		heroDoc.cleaveObjs = objs;
		
		heroDoc.castSkill("SkillB");
	}
	*/
	
// Never Used
	/*public void ShockTherapy1Pre(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	public IEnumerator ShockTherapy1(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		// Hero callerDoc = caller.GetComponent<Hero>();
		GRoot heroDoc = caller.GetComponent<GRoot>();
		heroDoc.castSkill("SkillA");// Hero.castSkill
		
		yield return new WaitForSeconds(0.5f);
	
		
		HeroData heroData = (heroDoc.data as HeroData);
		Hashtable tempNumber = heroData.getASkillByID("GROOT1").number; // GROOT15B
		int tempAtkPer = (int)tempNumber["AOENum"];
		int tempAtk = (int)(heroDoc.realAtk*tempAtkPer/100);
		int tempRadius = (int)tempNumber["AOERadius"];
		
		
		
		Hero target_hero = target.GetComponent<Hero>();
		//caller.GetComponent<Hero>().addHp(tempAtk);
		
		if(!grootSK_A)
		{
			grootSK_A = Resources.Load("eft/HB_SK1") as GameObject;
		}  
		 
		Instantiate(grootSK_A,new Vector3(target_hero.transform.position.x,target_hero.transform.position.y,-1),caller.transform.rotation);
		yield return new WaitForSeconds(0.2f);
		MusicManager.playEffectMusic("skill_special_heal");
		
		Hero hero = target.GetComponent<Hero>();
		
		Vector2 vc2 = target.transform.position - target_hero.transform.position;
		if( GData.isInOval(tempRadius*2, tempRadius, vc2) )
		{
			hero.addHp(tempAtk/2); 
	
			if(!grootEft)
			{
				grootEft = Resources.Load("eft/Eft_HealLight") as GameObject;
			}  
	
			Instantiate(grootEft, hero.transform.position+ new Vector3(0,-20,0), hero.transform.rotation);
		}
		
		
		AchievementManager.incrAchievement("SHOCKTHERAPY_50");
	}
*/	
	
	/*	
	public GameObject creepingVinesEftOnBodyPrb;
	public GameObject creepingVinesEftOnBody;
	
	public void CreepingVinesPre(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
		
		StartCoroutine(creepingVinesShowEftOnBody(caller));
	}
	
	public IEnumerator creepingVinesShowEftOnBody(GameObject caller)
	{
		yield return new WaitForSeconds(0.5f);
		if(creepingVinesEftOnBodyPrb == null)
		{
			creepingVinesEftOnBodyPrb = Resources.Load("eft/Groot/CreepingVinesEftOnBody") as GameObject;;
		}
		
		Destroy(creepingVinesEftOnBody);
		
		creepingVinesEftOnBody = Instantiate(creepingVinesEftOnBodyPrb, caller.transform.position + new Vector3(0, 80, 0), caller.transform.localRotation) as GameObject;
	}
	
	public ArrayList creepingVinesObjs;
	
	public IEnumerator CreepingVines(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		GRoot heroDoc = caller.GetComponent<GRoot>();
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("SkillA");// Hero.castSkill
		heroDoc.toward(target.transform.position);
		
		yield return new WaitForSeconds(1.0f);
		
		Vector3 vc3 = target.transform.position+ new Vector3(0,30,0);
		Vector3 createPt;
		
		if(heroDoc.model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = caller.transform.position + new Vector3(80,0,-50);
		}else{
//			print("left");
			createPt = caller.transform.position + new Vector3(-80,0,-50);
		}
		
		creepingVinesObjs = objs;
		creepingVinesShootBullet(createPt, vc3, objs);
	}
	
	public GameObject creepingVinesBulletPrb;
	
	public void creepingVinesShootBullet ( Vector3 creatVc3 ,   Vector3 endVc3, ArrayList objs  ){
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		StartCoroutine(chanageBGTextureColor(0.0f, 0.3f));
		if(creepingVinesBulletPrb == null)
		{
			creepingVinesBulletPrb = Resources.Load("eft/Groot/CreepingVinesBullet") as GameObject;
		}
		GameObject creepingVinesBullet = Instantiate(creepingVinesBulletPrb,creatVc3, caller.transform.rotation) as GameObject;
		
		
		GRoot heroDoc = caller.GetComponent<GRoot>();
		if(heroDoc.model.transform.localScale.x > 0)
		{
			creepingVinesBullet.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
		}
		else
		{
			creepingVinesBullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		}
		
		
		//float deg = (angle*360)/(2*Mathf.PI) - 180;
		//creepingVinesBullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(creepingVinesBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeCreepingVinesShootBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",creepingVinesBullet}});
	}
	
	protected virtual void removeCreepingVinesShootBullet (GameObject creepingVinesBullet){
		
		Destroy(creepingVinesBullet);
		
		GameObject scene = creepingVinesObjs[0] as GameObject;
		GameObject caller = creepingVinesObjs[1] as GameObject;
		GameObject target = creepingVinesObjs[2] as GameObject;
		
		
		GRoot heroDoc = caller.GetComponent<GRoot>();
		Enemy e = target.GetComponent<Enemy>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		Hashtable tempNumber = tempHeroData.getASkillByID("GROOT1").number; //GROOT15B
	
		int continueTime = (int)tempNumber["time"];
		
		int tempAtkPer = (int)tempNumber["AOENum"];
		int tempAtk = (int)(heroDoc.realAtk*tempAtkPer/100);
		
		e.realDamage(tempAtk /2 );
		
		e.twine((float)continueTime);
	}
*/	
	
	/*
	public void RocketLauncherPre(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	
	public IEnumerator RocketLauncher(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("SkillA");// Hero.castSkill
		
		yield return new WaitForSeconds(0.5f);
		
		StartCoroutine(RocketLauncherShootMissile(objs));
	}
	
	public GameObject rocketLauncherMissilePrb;
	public ArrayList rocketLauncherObjs = null;
	
	public GameObject rocketLauncherAttackEftPrb;
	public GameObject rocketLauncherAttackEft;
	
	public IEnumerator RocketLauncherShootMissile(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		rocketLauncherObjs = objs;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		if(rocketLauncherMissilePrb == null)
		{
			rocketLauncherAttackEftPrb = Resources.Load("eft/Rocket/RocketLauncherAttackEft") as GameObject;
		}
		GameObject rocketLauncherAttackEft = Instantiate(rocketLauncherAttackEftPrb, new Vector3(0,0,0), caller.transform.rotation) as GameObject;
		
		Vector3 endVc3 = target.transform.position+ new Vector3(0,70,0);
		Vector3 creatVc3;
		if(heroDoc.model.transform.localScale.x > 0)
		{
//			print("right");
			creatVc3 = caller.transform.position + new Vector3(80,40,-50);
			rocketLauncherAttackEft.transform.localScale = new Vector3(1,1,1);
			rocketLauncherAttackEft.transform.position = caller.transform.position + new Vector3(30,40,0);
		}else{
//			print("left");
			creatVc3 = caller.transform.position + new Vector3(-80,40,-50);
			rocketLauncherAttackEft.transform.localScale = new Vector3(-1,1,1);
			rocketLauncherAttackEft.transform.position = caller.transform.position + new Vector3(-30,40,0);
		}
		
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(rocketLauncherMissilePrb == null)
		{
			rocketLauncherMissilePrb = Resources.Load("eft/Rocket/RocketLauncherMissile") as GameObject;
		}
		GameObject rocketLauncherMissile = Instantiate(rocketLauncherMissilePrb,creatVc3, transform.rotation) as GameObject;
		
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		float deg = (angle*360)/(2*Mathf.PI);
		
		rocketLauncherMissile.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		iTween.MoveTo(rocketLauncherMissile,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","RocketLauncherShowExplosion"},{ "oncompletetarget",gameObject},{ "oncompleteparams",rocketLauncherMissile}});
		
		yield return new WaitForSeconds(0.5f);
	}
	
	
	public GameObject rocketLauncherExplosionPrb;
	public GameObject rocketLauncherExplosion;
	
	public void RocketLauncherShowExplosion(GameObject rocketLauncherMissile)
	{
		Destroy(rocketLauncherMissile);
		GameObject scene = rocketLauncherObjs[0] as GameObject;
		GameObject caller = rocketLauncherObjs[1] as GameObject;
		GameObject target = rocketLauncherObjs[2] as GameObject;
		

		if(rocketLauncherExplosionPrb == null)
		{
			rocketLauncherExplosionPrb = Resources.Load("eft/Rocket/RocketLauncherExplosion") as GameObject;
		}
		
		StartCoroutine(shakeCamera(new Vector3(0,15,0), 1.5f, 0.0f));
		StartCoroutine(chanageBGTextureColor(0.0f, 0.3f));
		StartCoroutine(slowMotion(0.0f, 0.3f));
		
		Destroy(rocketLauncherExplosion);
		
		rocketLauncherExplosion = Instantiate(rocketLauncherExplosionPrb,target.transform.position ,target.transform.rotation) as GameObject;
		
		rocketLauncherExplosion.transform.position += new Vector3(0,60, -100);
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		
		Hashtable tempNumber = tempHeroData.getASkillByID("ROCKET1").number;
		
		int damagePer = (int)tempNumber["AOENum"];
		int damage = heroDoc.realAtk*damagePer/100;
		
		int tempRadius = (int)tempNumber["AOERadius"];
		
		Enemy enemy = target.GetComponent<Enemy>();
		enemy.realDamage(damage /2 );
		
		foreach( string key in EnemyMgr.enemyHash.Keys )
		{
			Enemy otherEnemy = EnemyMgr.enemyHash[key] as Enemy;
			if(otherEnemy.getID() != enemy.getID())
			{
				Vector2 vc2 = otherEnemy.transform.position - enemy.transform.position;
				if( GData.isInOval(tempRadius*2,tempRadius , vc2) )
				{
					otherEnemy.realDamage(damage/2);
				}
			}
		}
		
	}
	
	*/
	
	/*
	private GameObject draxTauntHaloPrb;
	private GameObject draxTauntHalo;
	private GameObject draxTauntBottomPrb;
	private GameObject draxGameObject;
	private List<GameObject> draxTauntBottoms = new List<GameObject>();
	
	
	public void DraxTauntPre(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	
	public IEnumerator DraxTaunt(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.castSkill("SkillA");// Hero.castSkill
		
		yield return new WaitForSeconds(0.5f);
		
		StartCoroutine(draxTauntBottomEft(caller));
		StartCoroutine(draxTauntHaloEft(caller));
		
		HeroData tempHeroData = (heroDoc.data as HeroData);
		Hashtable tempNumber = tempHeroData.getASkillByID("DRAX1").number;
		
		int tempRadius = (int)tempNumber["AOERadius"];
		int time = (int)tempNumber["time"];
		
		foreach( string key in EnemyMgr.enemyHash.Keys )
		{
			Enemy otherEnemy = EnemyMgr.enemyHash[key] as Enemy;
			
			Vector2 vc2 = otherEnemy.transform.position - caller.transform.position;
			if( GData.isInOval(tempRadius*2,tempRadius , vc2) )
			{
				otherEnemy.setAbnormalState(Character.ABNORMAL_NUM.TAUNT);
				otherEnemy.moveToTarget(caller);
				StartCoroutine(draxTauntBottomEft(otherEnemy.gameObject));
			}
		}
		
		draxGameObject = caller;
		
		Invoke ("draxTauntFinish", time);
	}
	
	private IEnumerator draxTauntBottomEft(GameObject caller){
		yield return new WaitForSeconds(0.1f);
		Character characterDoc = caller.GetComponent<Character>();
		
		if (draxTauntBottomPrb == null){
			draxTauntBottomPrb = Resources.Load("eft/Drax/DraxTauntBottom") as GameObject;
		}
		draxTauntBottoms.Add(Instantiate(draxTauntBottomPrb) as GameObject);
		draxTauntBottoms[draxTauntBottoms.Count-1].transform.parent = caller.transform;
		draxTauntBottoms[draxTauntBottoms.Count-1].transform.localPosition = new Vector3(0, 0, 0);
		draxTauntBottoms[draxTauntBottoms.Count-1].transform.localScale = (characterDoc is Hero)? Vector3.one: new Vector3(.5f,.5f,1f);
	}
	
	private IEnumerator draxTauntHaloEft(GameObject caller){
		yield return new WaitForSeconds(0.1f);
		Hero heroDoc = caller.GetComponent<Hero>();
		if(draxTauntHaloPrb == null){
			draxTauntHaloPrb = Resources.Load("eft/Drax/DraxTauntHalo") as GameObject;
		}
		
		Destroy(draxTauntHalo);
		
		draxTauntHalo = Instantiate(draxTauntHaloPrb) as GameObject;
		draxTauntHalo.transform.parent = caller.transform;
		draxTauntHalo.transform.localPosition = new Vector3(-585, 253, 0);
		
		HeroData heroData = heroDoc.data as HeroData;
		Hashtable tempNumber = heroData.getASkillByID("DRAX1").number;
		int tempRadius = (int)tempNumber["AOERadius"];
		draxTauntHalo.transform.localScale = Vector3.one * (tempRadius/100);
	}
	
	private void draxTauntFinish()
	{
		foreach( string key in EnemyMgr.enemyHash.Keys )
		{
			Enemy otherEnemy = EnemyMgr.enemyHash[key] as Enemy;
			
			if(otherEnemy.getAbnormalState() == Character.ABNORMAL_NUM.TAUNT)
			{
				otherEnemy.setAbnormalState(Character.ABNORMAL_NUM.NORMAL);
			}
		}
		for (int i=draxTauntBottoms.Count-1; i>=0; i--){
			Destroy(draxTauntBottoms[i]);
		}
		Destroy(draxTauntHalo);
		
		Drax drax = draxGameObject.GetComponent<Drax>();
		drax.pieceAnima.restart();
	}
	*/
	
	/*public IEnumerator autoSkillActForTarget ( GameObject scene ,   GameObject caller){
		
		StartCoroutine(shakeCamera(new Vector3(0, 50, 0), 0.3f, 0.0f));
		StartCoroutine(chanageBGTextureColor(0.0f, 0.3f));
		StartCoroutine(slowMotion(0.0f, 0.3f));
		if(!isPop){
			Vector3 pos = caller.transform.position + new Vector3(0,140,-5); 
		
			if(!skinit)
			{
				skinit = Resources.Load("eft/eft") as GameObject;
			} 
		
			Instantiate(skinit, pos, caller.transform.rotation);
			MusicManager.playEffectMusic("skill_release");
			yield return new WaitForSeconds(0.5f);
			BattleBg.canUseSkill = true;
		}
		
	}*/
	
// Never Used 
 	/*public void setSkillEffectTxt ( GameObject scene ,   string txt  ){
 		BattleBg bg = scene.GetComponent<BattleBg>();
 		bg.setSkEffect(txt);
 	}
 	public void autoSkillEnd ( GameObject scene  ){
 		scene.renderer.material.SetColor("_Color",new Color(0.5f, 0.5f, 0.5f) );
 	}
 	*/
}
