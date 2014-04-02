using UnityEngine;
using System.Collections;

public class Rocket : Hero 
{
	public GameObject bulletPrb;
	public GameObject atkEft;
	//protected GameObject bltObj;
	public GameObject HitEft;
	public delegate void noParmsDelegate();
	public noParmsDelegate imageSequenceCallback;
	public noParmsDelegate damageEnemyCallback;
	public noParmsDelegate showAttackEftCallback;
	public noParmsDelegate shootBulletCallback;
	public noParmsDelegate addHerosBuffCallback;
	
	//value for passive 10A----->
	private bool rocket_10a  = false;
	private bool isSplashAtk = false;
	private int  chance10a;
	private int  aoe_10a;
	//<----------
	
	//value for passive 10B
	private int defPer;
	private int distance;
	
	//value for passive 25
	public GameObject drone;
	public int atkInterval;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 7;
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("SuperAttack", 18, groupAttack);
		pieceAnima.addFrameScript("SuperAttack", 22, groupAttack);
		pieceAnima.addFrameScript("SuperAttack", 26, groupAttack);
		pieceAnima.addFrameScript("SkillA",      17, playImageSequence);
		pieceAnima.addFrameScript("SkillA",      25, playImageSequence);
		pieceAnima.addFrameScript("Skill5B",     21, damageEnemy);
		pieceAnima.addFrameScript("Skill15A",     17, showAttackEft);
		pieceAnima.addFrameScript("Skill15A",     31, shootBullet);
		
		pieceAnima.addFrameScript("Skill15B", 40, addHerosBuff);
		pieceAnima.addFrameScript("Skill30B", 35, addBuffEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SuperAttack", 18);
		pieceAnima.removeFrameScript("SuperAttack", 22);
		pieceAnima.removeFrameScript("SuperAttack", 26);
		pieceAnima.removeFrameScript("SkillA",      17);
		pieceAnima.removeFrameScript("SkillA",      25);
		pieceAnima.removeFrameScript("Skill5B",     21);
		pieceAnima.removeFrameScript("Skill15A",    17);
		pieceAnima.removeFrameScript("Skill15A",    31);
	}
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		alreadyAddedDef = new Hashtable();
		isSplashAtk = false;
		HeroData heroD = this.data as HeroData;
		Hashtable passive1 =  heroD.getPSkillByID("ROCKET10A");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET10A");
			chance10a = (int)skillDef.passiveEffectTable["chance"];
			aoe_10a   = (int)skillDef.passiveEffectTable["AOERadius"];
			rocket_10a = true;
		}else{
			rocket_10a = false;
		}
		passive1 = heroD.getPSkillByID("ROCKET10B");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET10B");
			int uValue = (int)skillDef.passiveEffectTable["universal"];
			defPer = uValue;
			distance = (int)skillDef.passiveEffectTable["AOERadius"];
			if(!IsInvoking("checkNearAllies"))InvokeRepeating("checkNearAllies", 0, 1.0f);
		}
		passive1 = heroD.getPSkillByID("ROCKET25");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET25");
			atkInterval = (int)skillDef.passiveEffectTable["universalTime"];
			if(drone == null)
			{
				GameObject grenadePrefab = Resources.Load("gsl_dlg/Rocket_Antitank_grenade") as GameObject;
				drone = Instantiate(grenadePrefab) as GameObject;
				Vector3 offsetPos;
				if(this.model.transform.localScale.x < 0)
				{
					offsetPos = new Vector3(50,100,0);
				}else{
					offsetPos = new Vector3(-50,100,0);
				}
				iTween.MoveTo(drone,new Hashtable(){{"position",transform.position+offsetPos},{ "easetype","easeOutSine"},{ "speed",500},{"oncomplete","droneFly"},{ "oncompletetarget",gameObject}});
				InvokeRepeating("droneAttackTarget",0,atkInterval);
			}
		}
	}
	
	public void destroyDrone()
	{
		if(drone != null)
		{
			Destroy(drone);
		}
	}
	
	private void droneFly()
	{
		iTween.MoveTo(drone,new Hashtable(){{"position",drone.transform.position+new Vector3(0,20,0)},{ "easetype","easeInOutSine"},{"looptype","pingPong"},{ "time",1}});
	}
	
	private void droneAttackTarget()
	{
		if(targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(!target.getIsDead())
			{
				Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
				Vector3 createPt = drone.transform.position;
				shootBullet(createPt, vc3);
			}
		}
		
	}
	
	public void groupAttack(string s)
	{
		atkAnimaScript("groupAttack");
	}
	
	public void playImageSequence(string s){
		if (null != imageSequenceCallback){
			imageSequenceCallback();
		}
	}
	public void damageEnemy(string s){
		if (null != damageEnemyCallback){
			damageEnemyCallback();
		}
	}
	
	public void showAttackEft(string s){
		if (null != showAttackEftCallback){
			showAttackEftCallback();
		}
	}
	
	public void shootBullet(string s){
		if (null != shootBulletCallback){
			shootBulletCallback();
		}
	}
	
	public void addHerosBuff(string s)
	{
		if (null != addHerosBuffCallback){
			addHerosBuffCallback();
		}
	}
	
	public void addBuffEft(string s){
		if (null != addHerosBuffCallback){
			addHerosBuffCallback();
		}
	}
	
	public override void moveToTarget ( GameObject obj  )
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		if( targetObj )
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		
		targetObj = obj;
		
		// if (!IsInGroup){
		if (!IsInGroup && 
			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
			(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)))
		{
			startAtk();
		}
		else
		{
			base.moveToTarget(obj);
		}
	}
	
	public override bool checkOpponent (){
		if( isDead || 
			this.isAbnormalStateActive(Character.ABNORMAL_NUM.LAYDOWN) || 
			StaticData.isBattleEnd ||
			state == CAST_STATE)
		{
			this.cancelCheckOpponent();
			return false;
		}
		
		if (state != MOVE_STATE 
				&& state != MOVE_TARGET_STATE 
				&& state != MOVE_TARGET_DIRECTLY_STATE
				&& state != CAST_STATE)
		{
			
			if(this.heroAI.checkOpponent())
			{
				return true;
			}
			else
			{
				standby();
			}
		}
		return false;
	}
	
	protected override void atkAnimaScript (string s)
	{
		MusicManager.playEffectMusic("SFX_Rocket_Basic_1a");
		if(targetObj == null)
		{
			return;
		}
		Character target = targetObj.GetComponent<Character>();
		if(target.getIsDead())
		{
			return;
		}
		Vector3 vc3 = targetObj.transform.position+ new Vector3(0,70,0);
		Vector3 createPt;
		if(model.transform.localScale.x > 0)
		{
			// right
			if(s == "groupAttack")
			{
				createPt = transform.position + new Vector3(100,80,-50);
			}
			else
			{
				createPt = transform.position + new Vector3(100,60,-50);
			}
		}else{
			// left
			if(s == "groupAttack")
			{
				createPt = transform.position + new Vector3(-100,80,-50);
			}
			else
			{
				createPt = transform.position + new Vector3(-100,60,-50);
			}
		}
		shootBullet(createPt, vc3);
	}

	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(data.type == HeroData.ROCKET)
		{
			Vector3 atkEftPos= transform.position + new Vector3(0,60,-10);
			GameObject atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale = this.model.transform.localScale;
		}
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		
		if(rocket_10a && StaticData.computeChance(chance10a,100))
		{
			isSplashAtk = true;
			bltObj.transform.localScale = new Vector3(3,3,0);
		}
		float deg = (angle*360)/(2*Mathf.PI);
		//Debug.Log(" Rocket deg = "+deg);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected virtual void removeBullet (GameObject bltObj){
		GameObject HitEftObj = null;
		if(HitEft)
		{
			HitEftObj = Instantiate(HitEft, bltObj.transform.position, transform.rotation) as GameObject;//+Vector3(0,100,-50),
		}
		
		Destroy(bltObj);
		if(targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(HitEftObj != null)
			{
				HitEftObj.transform.parent = targetObj.transform;
			}
			//add by xiaoyong for critical strike
			int dmg;
			
			// delete by why 2014.2.7
//			if( StaticData.computeChance((int)realCStk*100, 100) )
//			{
//				(data as HeroData).critValue = realAtk*2;
//				criticalHandler();
//				
//				if(!cstkAnimPrb)
//				{
//					cstkAnimPrb = Resources.Load("eft/CritEft") as GameObject;
//				}
//				GameObject cstkAnimObj = Instantiate(cstkAnimPrb, targetObj.transform.position+new Vector3(0,50,-1),this.gameObject.transform.rotation) as GameObject;
//				dmg = target.defenseAtk((data as HeroData).critValue, this.gameObject);
//			}
//			else
//			{
			dmg = target.defenseAtk(realAtk, this.gameObject);
			if(isSplashAtk)
			{
				HitEftObj.transform.localScale = new Vector3(18,18,0);
				StaticData.splashDamage(target, this, EnemyMgr.enemyHash.Values, realAtk, aoe_10a);
				isSplashAtk = false;
			}
			
//			}
			trinketEfts(dmg);
			
		}
		else
		{
//			Debug.LogError("rocket zhe jiu shi jie shu bu zou de yuan yin hahahahahahahahahha!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
//			if(!StaticData.isBattleEnd)
//			{
//				standby();
//			}
		}
	}
	
	//for 10b
	private Hashtable alreadyAddedDef;
	private void checkNearAllies()
	{
		int j = 0;
		ArrayList heroRefClone = new ArrayList(HeroMgr.heroHash.Values);
		for(int i = 0 ; i<heroRefClone.Count; i++)
		{
			Hero hero = heroRefClone[i] as Hero;
			bool isNear = false;
			for(j=0 ; j<heroRefClone.Count; j++)
			{
				Hero otherHero = heroRefClone[j] as Hero;
				if(hero != otherHero)
				{
					float dis = Vector2.Distance(hero.transform.position, otherHero.transform.position);
					if(dis<distance)
					{
						isNear = true;
//						print(hero.name + "--" + otherHero.name +":"+ distance);
					}
					
				}
			}
			if(isNear)
			{
				if(!alreadyAddedDef.Contains(hero.data.type))
				{
					ArrayList list = new ArrayList();
					int plusValue = (int)(hero.realDef.PHY*(defPer/100.0f));
					list.Add(hero);
					list.Add(plusValue);
					alreadyAddedDef[hero.data.type] = list;
					hero.realDef.PHY += plusValue;
				}
			}else{
				if(alreadyAddedDef.Contains(hero.data.type))
				{
					ArrayList tempList = (ArrayList)alreadyAddedDef[hero.data.type];
					hero.realDef.PHY -= (int)(tempList[1]);
					alreadyAddedDef.Remove(hero.data.type);
				}
			}
		}
		
//		foreach(ArrayList ary in alreadyAddedDef.Values)
//		{
//			print(ary[0]+":::"+ary[1]);
//		}
		
	}
	
	public override void dead (string s)
	{
		CancelInvoke("checkNearAllies");
		CancelInvoke("droneAttackTarget");
		foreach(ArrayList ary in alreadyAddedDef.Values)
		{
			Hero hero = (Hero)ary[0];
			hero.realDef.PHY -= (int)ary[1];
		}
		alreadyAddedDef.Clear();
		Destroy(drone);
		base.dead (s);
	}
	
	public override void standby ()
	{
		base.standby ();
		if(drone != null)
		{
			Vector3 offsetPos;
			if(this.model.transform.localScale.x < 0)
			{
				offsetPos = new Vector3(50,100,0);
			}else{
				offsetPos = new Vector3(-50,100,0);
			}
			iTween.MoveTo(drone,new Hashtable(){{"position",transform.position+offsetPos},{ "easetype","easeOutSine"},{ "speed",500},{"oncomplete","droneFly"},{ "oncompletetarget",gameObject}});
		}
	}
	
}
