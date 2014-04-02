using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandMine : Hazard 
{

	public LandMineDef landMineDef;
	
	protected int radiusX = 200;
	protected int radiusY = 200;
	protected float inc = 2.0f;
	
	public bool isAttack = false;
	
	public GameObject explodeEftPrb;
	protected GameObject explodeEft;
	protected PackedSprite explodeEftPackedSprite;
	
	public GameObject landMineRotationPrb;
	protected GameObject landMineRotation;
	protected PackedSprite landMineRotationPackedSprite;
	
	protected UISprite landMineSprite;
	
	public void Start()
	{
		isAttack = false;
		
		
	}
	
	public override void calculateAttackRect()
	{
		radiusX = (int)landMineSprite.gameObject.transform.localScale.x;
		radiusY = (int)landMineSprite.gameObject.transform.localScale.y;
	}
	
	void FixedUpdate()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			return;
		}
		if(isAttack){
			return;
		}
		
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			if(hero.isDead)continue;
			Vector2 vc2 = hero.transform.position - gameObject.transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				attack();
				return;
			}
		}
		foreach(GameObject enemyGo in LevelMgr.Instance.allEnemies)
		{
			if(enemyGo == null) continue;
			Character c = enemyGo.GetComponent<Character>();
			if(c == null) continue;
			if(c.isDead) continue;
			Vector2 vc2 = c.transform.position - transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				attack();
				return;
			}
		}		
	}
	
	protected void attack()
	{
		MusicManager.playEffectMusic("SFX_Kyln_Land_Mine_1a");
		isAttack = true;
		
		landMineSprite.spriteName = "LandMine_2";
		landMineSprite.MakePixelPerfect();
		
		landMineRotationPackedSprite.gameObject.SetActive(true);
		landMineRotationPackedSprite.PlayAnim(0);
	}
	
	protected void landMineRotationFinish(SpriteBase sprite)
	{
		this.isEnabled = false;
		landMineRotationPackedSprite.gameObject.SetActive(false);
		explodeEftPackedSprite.gameObject.SetActive(true);
		explodeEftPackedSprite.PlayAnim(0);
		StartCoroutine(damage());
	}
	
	protected IEnumerator damage()
	{
		yield return new WaitForSeconds(0.3f);
		
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			if(hero.isDead)continue;
			Vector2 vc2 = hero.transform.position - gameObject.transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				hero.realDamage((int)this.landMineDef.Attack);
			}
		}
		foreach(GameObject enemyGo in LevelMgr.Instance.allEnemies)
		{
			if(enemyGo == null) continue;
			Character c = enemyGo.GetComponent<Character>();
			if(c == null) continue;
			if(c.isDead) continue;
			Vector2 vc2 = c.transform.position - transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				c.realDamage((int)this.landMineDef.Attack);
			}
		}	
	}
	
	public override HazardDef HazardDef
	{
		get
		{
			return landMineDef;
		}
		set
		{
			hazardDef = value;
			landMineDef = hazardDef as LandMineDef;
			
			explodeEft = Instantiate(explodeEftPrb) as GameObject;
		
			explodeEft.transform.parent = transform;
			explodeEft.transform.localPosition = new Vector3(0, 1.07f, -100);
		
			explodeEftPackedSprite = explodeEft.GetComponent<PackedSprite>();
			
			explodeEft.SetActive(false);
			
			
			landMineRotation = Instantiate(landMineRotationPrb) as GameObject;
			
			landMineRotation.transform.parent = transform;
			landMineRotation.transform.localPosition = new Vector3(0, 0.8f, 0);	
			
			landMineRotationPackedSprite = landMineRotation.GetComponent<PackedSprite>();
			landMineRotationPackedSprite.SetAnimCompleteDelegate(landMineRotationFinish);
			
			landMineRotation.SetActive(false);
			
			landMineSprite = gameObject.GetComponent<UISprite>();
		}
	}
}
