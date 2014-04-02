using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SentryGun : Hazard
{
	public SentryGunDef sentryGunDef;
	
	protected int radiusX = 200;
	protected int radiusY = 200;
	protected float inc = 1.0f;
	
	public bool isAttack = false;
	
	public GameObject frontSightPrefab;
	protected GameObject frontSight;
					
	public GameObject sentryGunLinePrefab;
	protected GameObject sentryGunLine;
	
	public GameObject sentryGunAttarckPrefab;
	protected GameObject sentryGunAttarck;
	protected PackedSprite sentryGunAttarckPackedSprite;
	
	public GameObject sentryGunBulletHolePrefab;
	protected GameObject sentryGunBulletHole;
	protected PackedSprite sentryGunBulletHolePackedSprite;
	
	private List<string> heroList = new List<string>();
	private List<Character> enimiesList = new List<Character>();
	public void Awake()
	{
		
	}
	
	public override void calculateAttackRect()
	{
		UISprite sprite = frontSight.GetComponent<UISprite>();
		
		radiusX = (int)sprite.gameObject.transform.localScale.x;
		radiusY = (int)sprite.gameObject.transform.localScale.y;
	}
	
	void FixedUpdate()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			this.isAttack = false;
			heroList.Clear();
			cancelAtk();
			hideEft();
			return;
		}
		
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			Vector2 vc2 = hero.transform.position - frontSight.transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				if(hero.isDead)
				{
					if(heroList.Contains(hero.data.type))
					{
						heroList.Remove(hero.data.type);
					}
					continue;
				}
				if(!heroList.Contains(hero.data.type))
				{
					heroList.Add(hero.data.type);
				}
			}
			else
			{
				if(heroList.Contains(hero.data.type))
				{
					heroList.Remove(hero.data.type);
				}
			}
		}
		enimiesList.Clear();
		foreach(GameObject enemyGo in LevelMgr.Instance.allEnemies)
		{
			if(enemyGo == null) continue;
			Character c = enemyGo.GetComponent<Character>();
			if(c == null) continue;
			if(c.isDead) continue;
			Vector2 vc2 = c.transform.position - frontSight.transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				enimiesList.Add(c);
			}
		}
		
		if(heroList.Count<=0 && enimiesList.Count <=0)
		{
			this.isAttack = false;
			heroList.Clear();
			cancelAtk();
			hideEft();
		}
		else
		{
			if (!isAttack)
			{
				startAtk();
			}
		}
		
	}
	
	protected void hideEft()
	{
		sentryGunAttarckPackedSprite.StopAnim();
		sentryGunAttarckPackedSprite.Hide(true);
		sentryGunAttarck.SetActive(false);
		
		sentryGunBulletHolePackedSprite.StopAnim();
		sentryGunBulletHolePackedSprite.Hide(true);
		sentryGunBulletHole.SetActive(false);
	}
	
	protected void showEft()
	{
		sentryGunAttarck.SetActive(true);
		sentryGunAttarckPackedSprite.Hide(false);
		sentryGunAttarckPackedSprite.PlayAnim(0);
		
		sentryGunBulletHole.SetActive(true);
		sentryGunBulletHolePackedSprite.Hide(false);
		sentryGunBulletHolePackedSprite.PlayAnim(0);
	}
	
	protected void startAtk()
	{
		if (!IsInvoking ("atk"))
		{
			InvokeRepeating ("atk", 0.5f, sentryGunDef.AttackSpeed);
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
		MusicManager.playEffectMusic("SFX_Kyln_Sentry_Gun_Loop_1b");
		this.isAttack = true;
		if(sentryGunAttarckPackedSprite.IsHidden())
		{
			showEft();
		}
		foreach(string heroType in heroList)
		{
			Hero h = HeroMgr.getHeroByType(heroType);
			h.realDamage((int)this.sentryGunDef.Attack);
		}
		
		foreach(Character c in enimiesList){
			c.realDamage((int)this.sentryGunDef.Attack);	
		}
	}
	
	protected void atkFinish(SpriteBase sprite)
	{
		this.isAttack = false;
		hideEft();
	}
	
	public override HazardDef HazardDef
	{
		get
		{
			return this.sentryGunDef;
		}
		set
		{
			hazardDef = value;
			sentryGunDef = hazardDef as SentryGunDef;
			
			transform.localPosition += new Vector3(0,0,1);
			
			frontSight = Instantiate(frontSightPrefab) as GameObject;
		
			frontSight.transform.parent = transform.parent;
			//POS
			float xx = Mathf.Lerp(-Utils.getScreenLogicWidth()/2f,Utils.getScreenLogicWidth()/2f,(1+sentryGunDef.FrontSightPos.x)/2f);
			float yy = Mathf.Lerp(-Utils.getScreenLogicHeight()/2f,Utils.getScreenLogicHeight()/2f,(1+sentryGunDef.FrontSightPos.y)/2f);
			frontSight.transform.localPosition = new Vector3(xx, yy, 0);
			
			frontSight.GetComponent<UISprite>().MakePixelPerfect();
			
			gameObject.GetComponent<UISprite>().MakePixelPerfect();
			
			//POS
			yy = Mathf.Lerp(-Utils.getScreenLogicHeight()/2f,Utils.getScreenLogicHeight()/2f,(1+sentryGunDef.SentryGunY)/2f);
			gameObject.transform.localPosition = new Vector3(xx, yy, 0);
			
			sentryGunLine = Instantiate(sentryGunLinePrefab) as GameObject;
			
			sentryGunLine.transform.parent = transform.parent;
			sentryGunLine.GetComponent<UISprite>().MakePixelPerfect();
			sentryGunLine.GetComponent<UISprite>().depth = gameObject.GetComponent<UISprite>().depth - 1;
			sentryGunLine.transform.localPosition = transform.localPosition + new Vector3(sentryGunLine.transform.localScale.x / 2, -sentryGunLine.transform.localScale.y / 2, 0); // + new Vector3(8, -sentryGunLine.transform.localScale.y / 2, 0)
			
			
			sentryGunAttarck = Instantiate(sentryGunAttarckPrefab) as GameObject;
			
			sentryGunAttarckPackedSprite = sentryGunAttarck.GetComponent<PackedSprite>();
			sentryGunAttarckPackedSprite.SetAnimCompleteDelegate(atkFinish);
			
			sentryGunAttarck.transform.parent = transform.parent;
			sentryGunAttarck.transform.localPosition = transform.localPosition - new Vector3(-4, transform.localScale.y + 30 , 0);
			
			
			sentryGunAttarck.SetActive(false);
			
			sentryGunBulletHole = Instantiate(sentryGunBulletHolePrefab) as GameObject;
			sentryGunBulletHole.transform.parent = transform.parent;
			
			sentryGunBulletHole.transform.localPosition = frontSight.transform.localPosition;// + new Vector3(0, frontSight.transform.localScale.y / 2, 0);
			
			sentryGunBulletHolePackedSprite = sentryGunBulletHole.GetComponent<PackedSprite>();
			sentryGunBulletHole.SetActive(false);
			
			
		}
	}
}
