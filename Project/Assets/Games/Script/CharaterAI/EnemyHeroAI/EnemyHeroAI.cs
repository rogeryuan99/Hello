using UnityEngine;
using System.Collections;

public class EnemyHeroAI : HeroAI 
{
	public override bool checkOpponent()
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
		
		if(opponent != null)
		{
			this.hero.targetObj = opponent.gameObject;
			this.OnGetOpponentLater(opponent);
			
		}
		
		return true;
	}
}
