using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireVent : Hazard
{

	public FireVentDef fireVentDef;
	
	protected int radiusX = 200;
	protected int radiusY = 200;
	protected float inc = 1.0f;
		
	public GameObject smokeEftPrb;
	public GameObject smokeEftGameObject;
	protected PackedSprite smokeEftPackedSprite;
	
	public GameObject attackEftPrb;
	public GameObject attackEftGameObject;
	protected PackedSprite attackEftPackedSprite;
	
	public void Start()
	{	
		smokeEftGameObject = Instantiate(smokeEftPrb) as GameObject;
		
		smokeEftGameObject.transform.parent = transform;
		smokeEftGameObject.transform.localPosition = new Vector3(0, 1.07f, 0);
		
		attackEftGameObject = Instantiate(attackEftPrb) as GameObject;
		
		attackEftGameObject.transform.parent = transform;
		attackEftGameObject.transform.localPosition = new Vector3(0, 0.8f, 0);
		attackEftGameObject.SetActive(false);
		
		smokeEftPackedSprite = smokeEftGameObject.GetComponent<PackedSprite>();
		smokeEftPackedSprite.PlayAnim(0);
				
		attackEftPackedSprite = attackEftGameObject.GetComponent<PackedSprite>();
		
		attackEftPackedSprite.SetAnimCompleteDelegate(atkFinish);
		
	}
	
	public override void calculateAttackRect()
	{
		UISprite sprite = gameObject.GetComponent<UISprite>();
		
		radiusX = (int)sprite.gameObject.transform.localScale.x;
		radiusY = (int)sprite.gameObject.transform.localScale.y;
	}
	
	public void Update()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			cancelAtk();
			return;
		}
		
		startAtk();
		
	}
	
	protected void startAtk()
	{
		if (!IsInvoking ("atk"))
		{
			InvokeRepeating ("atk", fireVentDef.AttackSpeed, fireVentDef.AttackSpeed);
		}
	}
	
	protected void cancelAtk ()
	{
		if (IsInvoking ("atk"))
		{
			CancelInvoke("atk");
		}
	}
	
	protected void atk ()
	{
		MusicManager.playEffectMusic("SFX_Kyln_Fire_Vent_1b");
		
		this.smokeEftPackedSprite.StopAnim();
		this.smokeEftGameObject.SetActive(false);
		this.attackEftGameObject.SetActive(true);
		this.attackEftPackedSprite.PlayAnim(0);
		StartCoroutine(damage());
	}
	
	protected IEnumerator damage()
	{
		yield return new WaitForSeconds(0.3f);
		List<Hero> heroListTemp = new List<Hero>();
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			Vector2 vc2 = hero.transform.position - transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				heroListTemp.Add(hero);
			}
		}
		
		foreach(Hero hero in heroListTemp)
		{
			if(hero != null)
			{
				hero.realDamage((int)this.fireVentDef.Attack);
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
				c.realDamage((int)this.fireVentDef.Attack);
			}
		}
	}
	
	protected void atkFinish(SpriteBase sprite)
	{
		attackEftGameObject.SetActive(false);
		smokeEftPackedSprite.gameObject.SetActive(true);
		smokeEftPackedSprite.PlayAnim(0);
	}
	
	public override HazardDef HazardDef
	{
		get
		{
			return fireVentDef;
		}
		set
		{
			hazardDef = value;
			fireVentDef = hazardDef as FireVentDef;
		}
	}
}
