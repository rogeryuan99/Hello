using UnityEngine;
using System.Collections;

public class HeroAI : CharacterAI
{
	public Hero hero;
	
	public override void getCharacter()
	{
		this.character = gameObject.GetComponent<Hero>();
		this.hero = this.character as Hero;
	}
	
	public override void setCharacter(Character character)
	{
		base.setCharacter(character);
	}
	
	public override void OnAtkAnimaScriptTarget(Character targetCharacter)
	{
		int dmg;
		
		dmg = targetCharacter.defenseAtk(this.hero.realAtk, this.hero.gameObject);
		
		this.hero.trinketEfts(dmg);
	}
	
	public override void OnAttackTargetInvokTargetIsNull()
	{
		this.hero.standby();
	}
	
	public override void OnAttackTargetInvokTargetIsDeath()
	{
		this.hero.targetObj = null;
		Debug.Log("attackTargetInvok 2  ???????????????????????");
		this.hero.standby();
	}
	
	public override bool OnAttackTargetInvokAttackTargetBefore()
	{
		if(this.hero.skContainer.Count >= 1)
		{
			if(SkillIconManager.TargetType.PLAYER.ToString() == hero.SkillTargetType)
			{
				return true;
			}
			SkillIconManager.Instance.CastSkill(this.hero);
			return false;
		}
		return true;
	}
	
	public override bool checkOpponent()
	{
		foreach(string key in EnemyMgr.enemyHash.Keys)
		{
			Character enemy = EnemyMgr.enemyHash[key] as Character;
			if( enemy.getIsDead())
			{
				continue;
			}
			
			Character opponent = this.hero.getOpponent(enemy);
			
			if(opponent != null)
			{
				this.hero.targetObj = opponent.gameObject;
				this.OnGetOpponentLater(opponent);
				return true;
			}
		}
		return false;
	}
	
	public override Character getOpponent(Character primaryOpponent)
	{
		int xDis = (int)Mathf.Abs(primaryOpponent.transform.position.x - this.hero.transform.position.x);
		int yDis = (int)Mathf.Abs(primaryOpponent.transform.position.y - this.hero.transform.position.y);
		if(xDis <= 200 && yDis < 200)
		{
			return primaryOpponent;
		}
		return null;
	}
	
	public override void OnGetOpponentLater (Character opponent)
	{
		this.hero.moveToTarget(opponent.gameObject);
	}
	
	public override void OnMoveWhenTargetDead()
	{
		Debug.Log("moveWhenTargetDead ling yi ge yuan yin ???????????????");
		this.hero.standby();
	}
	
	public override void OnMoveToTargetDistance(float dis)
	{
		if(this.hero.data.type == HeroData.GROOT && dis <= 300.0f)
		{
			if(this.hero.skContainer.Count >= 1)
			{
				SkillIconManager.Instance.CastSkill(this.hero);
			}
		}
	}
	
	public override void OnDefenseAtkHurtBeforeByDamageValue(int dam)
	{
		if(this.hero.isVineShield)
		{
			this.hero.vineShield.realDamage(dam);
		}
	}
}
