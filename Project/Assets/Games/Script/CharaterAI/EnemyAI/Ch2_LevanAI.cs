using UnityEngine;
using System.Collections;

public class Ch2_LevanAI : EnemyAI
{
	public override bool OnAtkAnimaScriptTargetBefore()
	{
		int per50HP = (int)(this.character.realMaxHp * 0.5f);
		
		string[] skillIDs = null;
			
		if(this.character.realHp >= per50HP)
		{
			skillIDs = new string[]
			{
				"LEVAN1",
				"LEVAN5A"
			};
			
			
		}
		else
		{
			skillIDs = new string[]
			{
				"LEVAN15A",
				"LEVAN30A"
			};
		}
		
		string skillID = skillIDs[Random.Range(0,2)];
		
		if(skillID == "LEVAN15A" && 
			this.character.attackAnimaName == "Skill15A_b" && 
			this.character.realHp < per50HP)
		{
			return true;
		}
		
		SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
		if(skillIconData != null && !skillIconData.isCoolDown && character.skContainer.Count == 0)
		{
			
			if(this.enemy.targetObj == null)
			{
				this.enemy.targetObj = base.getOpponent().gameObject;
			}
			this.enemy.PushSkillIdToContainer(skillID);	
		}
		
		if(this.enemy.skContainer.Count >= 1)
		{
			SkillEnemyManager.Instance.CastSkill(this.enemy);
			return false;
		}
		return true;
	}
	
	public override void OnGetOpponentLater(Character opponent)
	{
		string skillID = ""; 
		
		int per50HP = (int)(this.character.realMaxHp * 0.5f);
		
		if(this.character.realHp >= per50HP)
		{
			skillID = "LEVAN5A";
		}
		else
		{
			skillID = "LEVAN15A";
		}
		
		if(skillID == "LEVAN15A" && 
			this.character.attackAnimaName == "Skill15A_b" && 
			this.character.realHp < per50HP)
		{
			base.OnGetOpponentLater(opponent);
			return;
		}
			
		SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skillID);
		if(skillIconData != null && !skillIconData.isCoolDown && this.character.skContainer.Count == 0)
		{
			if(this.enemy.targetObj == null)
			{
				this.enemy.targetObj = base.getOpponent().gameObject;
			}
			this.enemy.PushSkillIdToContainer(skillID);
			SkillEnemyManager.Instance.CastSkill(this.enemy);
		}
		else
		{
			base.OnGetOpponentLater(opponent);
		}
	}
}
