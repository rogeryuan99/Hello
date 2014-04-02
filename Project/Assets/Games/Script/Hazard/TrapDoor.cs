using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapDoor : Hazard 
{
	public TrapDoorDef trapDoorDef;
	
	protected int radiusX = 100;
	protected int radiusY = 100;
	protected float inc = 0.7f;
	
	protected List<string> suctionHeroTypeList = new List<string>();
		
	public override void calculateAttackRect()
	{
		UISprite sprite = gameObject.GetComponent<UISprite>();
		
		radiusX = (int)sprite.gameObject.transform.localScale.x;
		radiusY = (int)sprite.gameObject.transform.localScale.y;
	}
	
	void Update()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			return;
		}
		
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			Vector2 vc2 = hero.transform.position - transform.position;
			if(StaticData.isInOval(radiusY * inc ,radiusX * inc, vc2))
			{
				if(hero.isDead || suctionHeroTypeList.Contains(hero.data.type))
				{
					continue;
				}
				suctionHeroTypeList.Add(hero.data.type);
				
				hero.standby();
				hero.selecting();
				
				iTween.MoveTo
				(
					hero.gameObject,
					iTween.Hash
					(
						"name","TrapDoor"+hero.data.type,
						"position", transform.position,
						"looptype",iTween.LoopType.none,
						"time", 0.5, 
						"easetype",iTween.EaseType.easeInQuart,
						"onComplete", "finish",
						"oncompletetarget",gameObject,
						"oncompleteparams",hero
					)
				);
			}
		}
	}
	
	void finish(Hero hero)
	{
		
		
		Debug.LogError("finish  " + hero.data.type);
		
		hero.setPosition(new Vector3(-400, 13, StaticData.objLayer));
				
		if(hero.getTarget() == null)
		{
			hero.move(BattleBg.Instance.getHeroStartPosByHeroType((hero.data as HeroData).type));
		}
		else
		{
			hero.moveToTarget(hero.getTarget());
		}
		
		suctionHeroTypeList.Remove(hero.data.type);
	}
	
	public override HazardDef HazardDef
	{
		get
		{
			return trapDoorDef;
		}
		set
		{
			hazardDef = value;
			trapDoorDef = hazardDef as TrapDoorDef;
		}
	}
}
