using UnityEngine;
using System.Collections;

public class Ch1_KorathAI : EnemyAI 
{
//	public override void OnGetOpponentLater(Character opponent)
//	{
//		base.OnGetOpponentLater(opponent);
//	}
	
	public override bool OnAtkAnimaScriptTargetBefore()
	{
		int per50HP = (int)(this.character.realMaxHp * 0.5f);
		if(this.character.realHp >= per50HP)
		{
			string skillID = "KORATH15A";
			SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
		
			if(skillIconData != null && !skillIconData.isCoolDown)
			{
				
				if(this.enemy.targetObj == null)
				{
					this.enemy.targetObj = base.getOpponent().gameObject;
				}
				this.enemy.PushSkillIdToContainer(skillID);	
				
			}
			else
			{
				skillID = "KORATH5A";
				skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
				if(skillIconData != null && !skillIconData.isCoolDown)
				{
					foreach(Hero hero in HeroMgr.heroHash.Values)
					{
						if(hero is StarLord || hero is Rocket)
						{
							continue;
						}
						float dis = Vector3.Distance(hero.transform.position, this.character.transform.position);
						
						if((int)dis <= (int)(this.character.data.attackRange + 15))
						{
							this.enemy.PushSkillIdToContainer(skillID);
							this.enemy.targetObj = hero.gameObject;
							break;
						}
					}
				}
			}
		}
		else if(this.character.realHp < per50HP)
		{
			string skillID = "KORATH1";
			SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
			
			
			if(skillIconData != null && !skillIconData.isCoolDown)
			{
				this.enemy.PushSkillIdToContainer(skillID);
				this.enemy.targetObj = HeroMgr.getLowestHealthHero().gameObject;
			}
			else 
			{
				skillID = "KORATH30A";
				skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID); 
				if(skillIconData != null && !skillIconData.isCoolDown)
				{
					SkillDef skillDef = SkillLib.instance.allHeroSkillHash[skillID] as SkillDef;
					int radius = (int)skillDef.activeEffectTable["AOERadius"];
					foreach(Hero hero in HeroMgr.heroHash.Values)
					{
						Vector2 vc2 = hero.transform.position - this.character.transform.position;
						if( StaticData.isInOval(radius, radius, vc2) )
						{
							if(hero.isDead)
							{
								continue;
							}
							this.enemy.PushSkillIdToContainer(skillID);
							this.enemy.targetObj = hero.gameObject;
							break;
						}
					}
				}
			}
		}
		if(this.enemy.skContainer.Count >= 1)
		{
			SkillEnemyManager.Instance.CastSkill(this.enemy);
			return false;
		}
		return true;
	}
}
