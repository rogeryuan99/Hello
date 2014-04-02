using UnityEngine;
using System.Collections;

public class StarLord : Hero {
	
	public GameObject starLord_light;
	public GameObject starLord_shoot;
	
	
	public GameObject bulletPrb;
	public GameObject atkEft;
	public GameObject HitEft;
	
	public GameObject atkGroupEft;
	
	public delegate void SkillAnimaEvent(StarLord starlord);
	public SkillAnimaEvent skillAnimaEventCallback;
	
	private bool isGodMode=false;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 7;
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("SuperAttack", 18, atkAnimaScript);
		pieceAnima.addFrameScript("SuperAttack", 22, atkAnimaScript);
		pieceAnima.addFrameScript("SuperAttack", 26, atkAnimaScript);
		
		pieceAnima.addFrameScript("Skill30B", 27, skillAnimaEvent);
		
		pieceAnima.addFrameScript("Skill15A", 24, skillAnimaEvent);
		pieceAnima.addFrameScript("Skill15A", 25, skillAnimaEvent);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SuperAttack", 18);
		pieceAnima.removeFrameScript("SuperAttack", 22);
		pieceAnima.removeFrameScript("SuperAttack", 26);
		
		pieceAnima.removeFrameScript("Skill30B", 30);
		
		pieceAnima.removeFrameScript("Skill15A", 24);
		pieceAnima.removeFrameScript("Skill15A", 25);
	}
	
	public void skillAnimaEvent(string s)
	{
		if(skillAnimaEventCallback != null)
		{
			skillAnimaEventCallback(this);
		}
	}
	
	// weapon change test
	public override void changeWeapon ( string weaponID  )
	{
		//pieceAnima.showPiece("SMALL_Weapon",weaponID);
		
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
			(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
			))
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
				(tag == "Enemy" && "HERO" == getTarget().tag.ToUpper()) ||
				(tag.ToUpper() == getTarget().tag.ToUpper() && isAtkSameTag)
				))
			{
				startAtk();
				Debug.LogError("Attack!!2");
			}else{
				return base.checkOpponent();
			}
		}
		return false;
	}
	
	public void eft()
	{
		GameObject A_STARLORD_Weapon_01C = GameObject.Find("SMALL_Weapon_01") as GameObject;
		if(A_STARLORD_Weapon_01C == null){
			Debug.LogError("SMALL_Weapon is null");
		}else{
			GameObject starLord_light_obj = Instantiate(starLord_light) as GameObject;
			starLord_light_obj.transform.parent = A_STARLORD_Weapon_01C.transform;
			starLord_light_obj.transform.localPosition = new Vector3(0,0,0);
			starLord_light_obj.transform.localScale =  new Vector3(1,1,1);
			starLord_light_obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
			StartCoroutine(delayedShoot(A_STARLORD_Weapon_01C));
		}
	}
	
	protected IEnumerator delayedShoot( GameObject A_STARLORD_Weapon_01C){
		yield return new WaitForSeconds( 0.1f);
		GameObject starLord_shoot_obj = Instantiate(starLord_shoot) as GameObject;
		starLord_shoot_obj.transform.parent = A_STARLORD_Weapon_01C.transform;
		starLord_shoot_obj.transform.localPosition = new Vector3(250,0,0);
		starLord_shoot_obj.transform.localScale =  new Vector3(1,1,1);
		starLord_shoot_obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
	
	protected override void atkAnimaScript (string s)
	{
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
		}
		else	
		{
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
				createPt = transform.position + new Vector3(80,40,-50);
			}else{
	//			print("left");
				createPt = transform.position + new Vector3(-80,40,-50);
			}
			shootBullet(createPt, vc3);
			eft();
		}
	}
	
	public override int defenseAtk (Vector6 atk, GameObject atkerObj)
	{
		if(isGodMode)atk=new Vector6(0,0,0,0,0,0);
		return base.defenseAtk (atk, atkerObj);
	}
	
	protected override void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  ){
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(data.type == HeroData.STARLORD)
		{
			Vector3 atkEftPos= transform.position + new Vector3(0, 50,-10);
			GameObject atkEftObj = Instantiate(atkEft, atkEftPos, transform.rotation) as GameObject;
			atkEftObj.transform.localScale = this.model.transform.localScale;
		}
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
			
			HeroData heroD = this.data as HeroData;
			Hashtable passive1 =  heroD.getPSkillByID("STARLORD10A");
			if(passive1 != null)
			{
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD10A");
				int chanceValue = (int)skillDef.passiveEffectTable["universal"];
				int timeValue = (int)skillDef.passiveEffectTable["universalTime"];
				if( StaticData.computeChance(chanceValue,100))
				{
					burningEnemy(timeValue, target);
				}
			}
			
			trinketEfts(dmg);
			
		}
		else
		{
//			Debug.LogError("starlord zhe jiu shi jie shu bu zou de yuan yin hahahahahahahahahha!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
//			if(!StaticData.isBattleEnd)
//			{
//				standby();
//			}
		}
	}
	
	public void burningEnemy(int time , Character enemy)
	{
		State state = new State(time, DestroySkillEft);
		
		enemy.addAbnormalState(state, Character.ABNORMAL_NUM.FIRE);
		showEft(enemy);
	}
	
	//on the fire effect
	protected GameObject SkillEft_STARLOAD1_FireBlast_Prb;
	protected GameObject fireEftFront;
	protected GameObject fireEftBehind;
	
	public void showEft(Character enemy)
	{
		if(SkillEft_STARLOAD1_FireBlast_Prb == null)
		{
			SkillEft_STARLOAD1_FireBlast_Prb = Resources.Load("eft/StarLord/SkillEft_STARLOAD1_FireBlast") as GameObject;
		}  
		
		Destroy(fireEftFront);
		
		fireEftFront = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject;
		fireEftFront.transform.parent = enemy.transform;
		// fireEftFront.transform.localPosition = new Vector3(0,555,-15);
		fireEftFront.transform.localPosition = new Vector3(0,500,-15);
		
		Destroy(fireEftBehind);
		
		fireEftBehind = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject; 
		fireEftBehind.transform.parent = enemy.transform;
		// fireEftBehind.transform.localPosition = new Vector3(0,555,100);
		fireEftBehind.transform.localPosition = new Vector3(0,500,100);
		
		PackedSprite ps = fireEftFront.GetComponent<PackedSprite>();
		
		ps.Color = new Color(ps.Color.r, ps.Color.g,ps.Color.b,0.5f);
	}
	
	public void DestroySkillEft(State self, Character charater)
	{
		Debug.LogError("skill_starlod1 DestroySkillEft");
//		charater.characterAI.OnDestroySkillEftObj -= DestroySkillEft;
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		Destroy(fireEftFront);
		Destroy(fireEftBehind);
	}
	
	public void toGodMode()
	{
		isGodMode = true;
	}
	public void toNormalMode()
	{
		isGodMode = false;
	}
}
