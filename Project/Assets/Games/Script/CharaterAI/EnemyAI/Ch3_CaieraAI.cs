using UnityEngine;
using System.Collections;

public class Ch3_CaieraAI : EnemyAI
{

	public override bool OnAtkAnimaScriptTargetBefore()
	{
		int per50HP = (int)(this.character.realMaxHp * 0.5f);
		
		string[] skillIDs = null;
			
		if(this.character.realHp >= per50HP)
		{
			skillIDs = new string[]
			{
				"CAIERA1"
				,
				"CAIERA5A"
				,
				"CAIERA5B"
			};
			
			
		}
		else
		{
			skillIDs = new string[]
			{
				"CAIERA15A"
				,
				"CAIERA15B"
				,
				"CAIERA30A"
				,
				"CAIERA30B"
			};
		}
		
		string skillID = skillIDs[Random.Range(0, skillIDs.Length)];
		
		SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
		
		if(skillIconData != null && !skillIconData.isCoolDown && character.skContainer.Count == 0 && enemy.state != Character.CAST_STATE)
		{
			
			if(this.enemy.targetObj == null)
			{
				this.enemy.targetObj = base.getOpponent().gameObject;
			}
			this.enemy.PushSkillIdToContainer(skillID);	
		}
		
		if(this.enemy.skContainer.Count >= 1 && enemy.state != Character.CAST_STATE)
		{
			SkillEnemyManager.Instance.CastSkill(this.enemy);
			return false;
		}
		return true;
	}
}
