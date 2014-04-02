using UnityEngine;
using System.Collections;

public class EnemyAI : CharacterAI
{
	public Enemy enemy;
	
	public override void getCharacter()
	{
		this.character = gameObject.GetComponent<Enemy>();
		this.enemy = this.character as Enemy;
	}
	
	public override void setCharacter(Character character)
	{
		base.setCharacter(character);
	}
	
	public override void OnAtkAnimaScriptTarget(Character targetCharacter)
	{
		this.enemy.playAtkEft();
		targetCharacter.defenseAtk(this.enemy.realAtk, this.enemy.gameObject);
	}
	
	public override void OnAtkAnimaScriptTargetIsNull()
	{
		this.enemy.startCheckOpponent();
	}
	
	public override void OnAtkAnimaScriptTargetIsDeath()
	{
		this.enemy.startCheckOpponent();
	}
	
	public override void OnAttackTargetInvokTargetIsNull()
	{
		this.enemy.startCheckOpponent();
	}
	
	public override void OnAttackTargetInvokTargetIsDeath()
	{
		this.enemy.targetObj = null;
		this.enemy.startCheckOpponent();
	}
	
	public override void OnCheckAtkerDefense(GameObject atker)
	{
		if(atker.tag != this.character.targetObj.tag || this.character.isAtkSameTag)
		{
			this.enemy.moveToTarget(atker);	
		}
	}
		
	public override void OnMoveWhenTargetDead()
	{
		this.enemy.startCheckOpponent();
	}
	
	public override Character getOpponent(Character primaryOpponent = null)
	{
		Character opponent = null;
		if(StaticData.computeChance(20, 100))
		{
			opponent = HeroMgr.getDefMaxHero(true);
		}
		else
		{
			opponent = HeroMgr.getRandomHero(true);
		}
		
		if(opponent == null)
		{
			if(StaticData.computeChance(20, 100))
			{
				opponent = HeroMgr.getDefMaxHero(false);
			}
			else
			{
				opponent = HeroMgr.getRandomHero(false);
			}
		}
		return opponent;		
	
	}
		
	public override void OnGetOpponentLater(Character opponent)
	{
		this.enemy.moveToTarget(opponent.gameObject);
	}
}
