using UnityEngine;
using System.Collections;

public class Cannon : Hazard
{
	public CannonDef cannonDef;
	
	public Bounds attackBouds;
	
	public GameObject cannonBulletPrb;
	
	public override void calculateAttackRect()
	{
		UISprite sprite = gameObject.GetComponent<UISprite>();
		
		if(cannonDef.Direction == 0) // toward right
		{
			
			Vector3 pos = new Vector3(
				gameObject.transform.position.x + Screen.width * cannonDef.Range / 2, 
				gameObject.transform.position.y - sprite.gameObject.transform.localScale.y / 2,
				0);
			
			attackBouds = new Bounds(pos, new Vector3(Screen.width * cannonDef.Range, sprite.gameObject.transform.localScale.y/2, 1));
		}
		else if(cannonDef.Direction == 1) // toward left
		{
			Vector3 pos = new Vector3(
				-Screen.width * cannonDef.Range + gameObject.transform.position.x + Screen.width * cannonDef.Range / 2,
				gameObject.transform.position.y - sprite.gameObject.transform.localScale.y / 2, 
				0);
			
			attackBouds = new Bounds(pos, new Vector3(Screen.width * cannonDef.Range, sprite.gameObject.transform.localScale.y/2, 1));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			cancelAtk();
			return;
		}
		bool isHeroInAttackRect = false;
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			attackBouds.center = new Vector3(attackBouds.center.x, attackBouds.center.y, hero.transform.position.z);
			if(attackBouds.Intersects(hero.collider.bounds))
			{
				isHeroInAttackRect = true;
				startAtk();
				break;
			}
		}
		if(!isHeroInAttackRect)
		{
			cancelAtk();
		}
	}
	
	protected void startAtk()
	{
		if (!IsInvoking ("atk"))
		{
			atk ();
			InvokeRepeating ("atk", cannonDef.AttackSpeed, cannonDef.AttackSpeed);
		}
	}
	
	protected void cancelAtk ()
	{
		if (IsInvoking ("atk"))
		{
			CancelInvoke ("atk");
		}
	}
#if UNITY_EDITOR	
	public void OnGUI()
	{
		if(isEnabled)
		{
			NgGUIDraw.DrawBox(ToScreenSpace(attackBouds), Color.blue,3,false);
		}
	}
	
	public Rect ToScreenSpace(Bounds bounds)
    {
        var origin = Camera.mainCamera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, 0.0f));
        var extents = Camera.mainCamera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y, 0.0f));
 
        return new Rect(origin.x, Screen.height - origin.y, extents.x - origin.x, origin.y - extents.y);
    }
#endif
	
	protected void atk ()
	{
		MusicManager.playEffectMusic("SFX_enemy_range_attack_singleshot_1a");
		
		
		Vector3 vc3 = Vector3.zero;
		Vector3 createPt = Vector3.zero;
		if(cannonDef.Direction == 0) // toward right
		{
			createPt = transform.position + new Vector3(100,0,-50);
			vc3 =  transform.position + new Vector3(Screen.width,0,0);
			
		}
		else if(cannonDef.Direction == 1) // toward left
		{
			createPt = transform.position + new Vector3(-100,0,-50);
			vc3 =  transform.position + new Vector3(-Screen.width,0,0);
			
		}
			
		GameObject bulletObj = Instantiate(cannonBulletPrb,createPt, transform.rotation) as GameObject;
		
		Bullet b = bulletObj.GetComponent<Bullet>();
		b.targetType = Bullet.TARGETTYPE.HERO;
		b.attack = (int)cannonDef.Attack;
		b.shootBullet(createPt, vc3);
	}
	
	public override HazardDef HazardDef
	{
		get
		{
			return cannonDef;
		}
		set
		{
			hazardDef = value;
			cannonDef = hazardDef as CannonDef;
	
			transform.localRotation = Quaternion.Euler(0f,180f * cannonDef.Direction , 0f);
		}
	}
}
