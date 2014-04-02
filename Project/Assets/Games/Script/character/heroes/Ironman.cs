using UnityEngine;
using System.Collections;

public class Ironman : Hero {

	public GameObject bulletPrb;
	
	public GameObject bltObj;
	
	public GameObject atkEft;
	public GameObject HitEft;
	
	public GameObject celebrate_Dust_FXPrb;
	
	public GameObject celebrate_BoostPrb;
	
	public GameObject celebrate_Boost_Left;
	public GameObject celebrate_Boost_Right;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 5;
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("Attack", 7, moveBullet1);
		pieceAnima.addFrameScript("Attack", 8, moveBullet2);
		
//		pieceAnima.addFrameScript("Celebrate", 10, showCelebrateEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Attack", 7);	
		pieceAnima.removeFrameScript("Attack", 8);	
	}
	
	
	
	// weapon change test
	public override void changeWeapon ( string weaponID  )
	{
		//pieceAnima.showPiece("SMALL_Weapon",weaponID);
		
	}
	
	public void showCelebrateEft(string s)
	{
		GameObject celebrate_Dust_FX = Instantiate(celebrate_Dust_FXPrb) as GameObject;
		celebrate_Dust_FX.transform.position = transform.position;
		celebrate_Dust_FX.transform.localScale = Vector3.one;
		StartCoroutine(changeCelebrate_Dust_FX(celebrate_Dust_FX));
		
		celebrate_Boost_Left = Instantiate(celebrate_BoostPrb) as GameObject;
		celebrate_Boost_Right = Instantiate(celebrate_BoostPrb) as GameObject;
		celebrate_Boost_Left.transform.position = transform.position;
		celebrate_Boost_Right.transform.position = transform.position;
	}
	
	public IEnumerator changeCelebrate_Dust_FX(GameObject celebrate_Dust_FX)
	{
		int count = 4;
		int currentCount = 1;
		while(currentCount != count)
		{
			celebrate_Dust_FX.transform.localScale += new Vector3(0.1f, 0.1f, 0);
			celebrate_Dust_FX.renderer.material.color -= new Color32(0,0,0,200 / 4);
			yield return new WaitForSeconds(0.01f);
		}
		Destroy(celebrate_Dust_FX);
	}
	
	public void showCelebrate_Boost(string s)
	{
		
	}
	
	public void moveBullet1(string s)
	{
		
	}
	
	public void moveBullet2(string s)
	{
		if(bltObj != null)
		{	
			bltObj.transform.localScale = new Vector3(0.23f, 0.05f, 1);
		}
	}

	public override void moveToTarget ( GameObject obj  )
	{
		if( targetObj )
		{
			Character targetDoc =  targetObj.GetComponent<Character>(); 
			targetDoc.dropAtkPosition(this);
		}
		targetObj = obj;
		
		// if (!IsInGroup){
		if (!IsInGroup &&
			((tag != "Enemy" && "ENEMY" == getTarget().tag.ToUpper()) ||
			(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper())))
		{
			startAtk();
		}
		else{
			base.moveToTarget(obj);
		}
	}
	
	
	
	public override bool checkOpponent ()
	{
		if(isDead || StaticData.isBattleEnd){
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
			}else{
				return base.checkOpponent();
			}
		}
		return false;
	}
	
	
	
//	protected IEnumerator delayedShoot( GameObject A_STARLORD_Weapon_01C){
//		yield return new WaitForSeconds( 0.1f);
//		GameObject starLord_shoot_obj = Instantiate(starLord_shoot) as GameObject;
//		starLord_shoot_obj.transform.parent = A_STARLORD_Weapon_01C.transform;
//		starLord_shoot_obj.transform.localPosition = new Vector3(250,0,0);
//		starLord_shoot_obj.transform.localScale =  new Vector3(1,1,1);
//		starLord_shoot_obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
//	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_StarLord_Basic_1a");
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
			createPt = transform.position + new Vector3(80,80,-50);
		}else{
//			print("left");
			createPt = transform.position + new Vector3(-80,80,-50);
		}
		
		bltObj = Instantiate(bulletPrb, vc3, transform.rotation) as GameObject;
		bltObj.transform.position = createPt;
		bltObj.transform.localScale = new Vector3(0.064f, 0.064f, 1);
		StartCoroutine(showBullet(bltObj, createPt, vc3));
	}
	
	protected IEnumerator showBullet(GameObject bltObj, Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		int count = 3;
		int current = 1;
		while(current != count)
		{
			
			bltObj.transform.localScale += new Vector3(0.02f, 0.02f, 0);
			
			current ++;
			
			yield return new WaitForSeconds(0.01f);
		}
		
		bltObj.transform.localScale = new Vector3(0.164f, 0.164f, 1);
		
		shootBullet(creatVc3, endVc3);
	}
	
	
	
	protected override void shootBullet (Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		
//		Debug.Break();
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
		bltObj = null;
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
			
		}else{
			
			if(!StaticData.isBattleEnd)
			{
				standby();
			}
		}
	}
}
