using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyBase : MonoBehaviour
{
	protected EnergyPoleDef energyPoleDef;
	
	public PackedSprite energyPackedSprite;
	
	private List<string> heroTypeList = new List<string>();
	private List<Character> enimies = new List<Character>();
	
	public virtual EnergyPoleDef EnergyPoleDef
	{
		set
		{
			this.energyPoleDef = value;
		}
		get
		{
			return energyPoleDef;
		}
	}
	
	public void energyHide(bool b)
	{
		if(b == energyPackedSprite.IsHidden())
		{
			return;
		}
		
		energyPackedSprite.Hide(b);
		
		if(!b)
		{
			energyPackedSprite.PlayAnim(0);
			startAttack();
		}
		else
		{
			stopAttack();
		}
		
	}
	
	public void stopAttack()
	{
		energyPackedSprite.StopAnim();
		cancelAtk();
		this.heroTypeList.Clear();
	}
	
	public void FixedUpdate()
	{
		if(energyPackedSprite == null)
		{
			return;
		}
		if(energyPackedSprite.IsAnimating())
		{
			foreach(Character character in HeroMgr.heroHash.Values)
			{
				if(character.isDead)
				{
					if(this.heroTypeList.Contains(character.data.type))
					{
						this.heroTypeList.Remove(character.data.type);
					}
					continue;
				}
				
				Bounds energyBounds = gameObject.collider.bounds;
				Bounds characterBounds = character.collider.bounds;
				characterBounds.center = new Vector3(characterBounds.center.x, characterBounds.center.y, energyBounds.center.z);
			
				
				if(energyBounds.Intersects(characterBounds))
				{
					if(!this.heroTypeList.Contains(character.data.type))
					{
						this.heroTypeList.Add(character.data.type);
					}
				}
				else
				{
					if(this.heroTypeList.Contains(character.data.type))
					{
						this.heroTypeList.Remove(character.data.type);
					}
				}
			}
			////////enemies
			enimies.Clear();
			foreach(GameObject enemyGo in LevelMgr.Instance.allEnemies)
			{
				if(enemyGo == null) continue;
				Character c = enemyGo.GetComponent<Character>();
				if(c == null) continue;
				if(c.isDead) continue;
				
				Bounds energyBounds = gameObject.collider.bounds;
				Bounds characterBounds = c.collider.bounds;
				characterBounds.center = new Vector3(characterBounds.center.x, characterBounds.center.y, energyBounds.center.z);
				
				if(energyBounds.Intersects(characterBounds))
				{
					enimies.Add(c);
				}
			}
		}
	}
	
	public void startAttack()
	{
		if(!IsInvoking("damage"))
		{
			InvokeRepeating("damage", 0.5f, 0.5f);
		}
	}
	
	public void cancelAtk()
	{
		if (IsInvoking ("damage"))
		{
			CancelInvoke ("damage");
		}
	}
	
	public void damage()
	{
		List<Hero> heroListTemp = new List<Hero>();
		foreach(string heroType in this.heroTypeList)
		{
			heroListTemp.Add(HeroMgr.getHeroByType(heroType));	
		}
		
		foreach(Hero hero in heroListTemp)
		{
			if(hero != null)
			{
				hero.realDamage((int)this.energyPoleDef.attack);
			}
		}
		
		foreach(Character c in enimies){
			if(c != null){
				c.realDamage((int)this.energyPoleDef.attack);	
			}
		}
	}
}
