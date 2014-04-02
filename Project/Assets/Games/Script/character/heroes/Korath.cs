using UnityEngine;
using System.Collections;

public class Korath: Hero
{
	
	// public GameObject starLord_light;
	// public GameObject starLord_shoot;
	
	public GameObject bulletPrb;
	public GameObject atkEft;
	public GameObject HitEft;
	
	public GameObject atkGroupEft;
	
	public delegate void StunBuff();
	public StunBuff addStunBuffCallBack;
	
	private int autoRegenHpValue;
	
//	public delegate void SkillAnimaEvent(Korath korath);
//	public SkillAnimaEvent skillAnimaEventCallback;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 7;
	}
	
	public override void initData (CharacterData characterD)
	{
		base.initData (characterD);
		HeroData heroD = this.data as HeroData;
		Hashtable passive1 =  heroD.getPSkillByID("KORATH20");
		if(passive1 != null)
		{
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH20");
			int uValue = (int)skillDef.passiveEffectTable["universal"];
			autoRegenHpValue = uValue;
			if(!IsInvoking("autoRegenHp"))InvokeRepeating("autoRegenHp", 0, 1.0f);
		}
	}
	
	public override void Start()
	{
		base.Start();
		
		pieceAnima.addFrameScript("Skill30A",39,addStunBuff);
//		pieceAnima.addFrameScript("SuperAttack", 18, atkAnimaScript);
//		pieceAnima.addFrameScript("SuperAttack", 22, atkAnimaScript);
//		pieceAnima.addFrameScript("SuperAttack", 26, atkAnimaScript);
//		
//		pieceAnima.addFrameScript("Skill30B", 27, skillAnimaEvent);
//		
//		pieceAnima.addFrameScript("Skill15A", 24, skillAnimaEvent);
//		pieceAnima.addFrameScript("Skill15A", 25, skillAnimaEvent);
	}
	
//	public void OnDestroy()
//	{
//		pieceAnima.removeFrameScript("SuperAttack", 18);
//		pieceAnima.removeFrameScript("SuperAttack", 22);
//		pieceAnima.removeFrameScript("SuperAttack", 26);
//		
//		pieceAnima.removeFrameScript("Skill30B", 30);
//		
//		pieceAnima.removeFrameScript("Skill15A", 24);
//		pieceAnima.removeFrameScript("Skill15A", 25);
//	}
	
//	public void skillAnimaEvent(string s)
//	{
//		if(skillAnimaEventCallback != null)
//		{
//			skillAnimaEventCallback(this);
//		}
//	}
	
	// weapon change test
	public override void changeWeapon ( string weaponID  )
	{
		//pieceAnima.showPiece("SMALL_Weapon",weaponID);
		
	}

	public override void moveToTarget (GameObject obj)
	{
		if( targetObj ){
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		
		if (!IsInGroup &&
			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper())))
		{
			startAtk();
		}
		else
		{
			base.moveToTarget(obj);
		}
	}
	
	public override bool checkOpponent ()
	{
		if(isDead || StaticData.isBattleEnd)
		{
			this.cancelCheckOpponent();
			return false;
		}
		if(state != MOVE_STATE  && state != CAST_STATE)
		{
			if(targetObj != null && 
				!isDead	&&
				!IsInGroup &&
				((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
				(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper())))
			{
				startAtk();
				Debug.LogError("Attack!!2");
			}
			else
			{
				return base.checkOpponent();
			}
		}
		return false;
	}
	
//	public void eft()
//	{
//		GameObject A_STARLORD_Weapon_01C = GameObject.Find("SMALL_Weapon_01") as GameObject;
//		if(A_STARLORD_Weapon_01C == null){
//			Debug.LogError("SMALL_Weapon is null");
//		}else{
//			GameObject starLord_light_obj = Instantiate(starLord_light) as GameObject;
//			starLord_light_obj.transform.parent = A_STARLORD_Weapon_01C.transform;
//			starLord_light_obj.transform.localPosition = new Vector3(0,0,0);
//			starLord_light_obj.transform.localScale =  new Vector3(1,1,1);
//			starLord_light_obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
//			StartCoroutine(delayedShoot(A_STARLORD_Weapon_01C));
//		}
//	}
	
//	protected IEnumerator delayedShoot( GameObject A_STARLORD_Weapon_01C){
//		yield return new WaitForSeconds( 0.1f);
//		GameObject starLord_shoot_obj = Instantiate(starLord_shoot) as GameObject;
//		starLord_shoot_obj.transform.parent = A_STARLORD_Weapon_01C.transform;
//		starLord_shoot_obj.transform.localPosition = new Vector3(250,0,0);
//		starLord_shoot_obj.transform.localScale =  new Vector3(1,1,1);
//		starLord_shoot_obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
//	}
	
	public void addStunBuff(string s)
	{
		if(null != addStunBuffCallBack)
		{
			addStunBuffCallBack();
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
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
//			print("right");
			createPt = transform.position + new Vector3(140,50,-50);
		}else{
//			print("left");
			createPt = transform.position + new Vector3(-140,50,-50);
		}
		
		// shootBullet(createPt, vc3);
		// eft();
		StartCoroutine(DelayShoot(createPt, vc3));
	}
	
	protected IEnumerator DelayShoot(Vector3 creatVc3 ,   Vector3 endVc3){
		yield return new WaitForSeconds(1.2f);
		shootBullet(creatVc3, endVc3);
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
	
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected virtual void removeBullet (GameObject bltObj)
	{
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
			
			dmg = target.defenseAtk(realAtk, this.gameObject);
			
			trinketEfts(dmg);
			
		}
		else
		{
			Debug.LogError("starlord zhe jiu shi jie shu bu zou de yuan yin hahahahahahahahahha!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			if(!StaticData.isBattleEnd)
			{
				standby();
			}
		}
	}
	
	public override void dead(string s)
	{
		this.attackAnimaName = "Attack";
		CancelInvoke("autoRegenHp");
		base.dead();	
	}
	
	private void autoRegenHp()
	{
		int regenValue = (int)((autoRegenHpValue/100.0f)*this.realMaxHp);
		this.addHp(regenValue);
	}
}
