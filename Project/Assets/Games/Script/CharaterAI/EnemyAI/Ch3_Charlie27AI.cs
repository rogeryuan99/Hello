using UnityEngine;
using System.Collections;

public class Ch3_Charlie27AI : EnemyAI {
	
	private const float skillCastChance = .5f;
	private string[] sk70u = {"CHARLIE271",  "CHARLIE275A",  "CHARLIE275B"};
	private string[] sk40u = {"CHARLIE275B", "CHARLIE2715A", "CHARLIE2715B"}; 
	private string[] sk40l = {"CHARLIE275A", "CHARLIE2730A", "CHARLIE2730B"};
	private int hp70;
	private int hp40;
	
	public void Start(){
		hp70 = (int)(this.character.realMaxHp * 0.7f);
		hp40 = (int)(this.character.realMaxHp * 0.4f);
	}
	
	public override bool OnAtkAnimaScriptTargetBefore()
	{
		bool needAttack = true;
		if (Random.value > skillCastChance) {
			needAttack = true;
		}
		else if (this.character.realHp >= hp70){
			needAttack = !CastSkills(sk70u);
		}
		else if (this.character.realHp >= hp40){
			needAttack = !CastSkills(sk40u);
		}
		else{
			needAttack = !CastSkills(sk40l);
		}

		if(this.enemy.skContainer.Count >= 1)
		{
			SkillEnemyManager.Instance.CastSkill(this.enemy);
			needAttack = false;
		}
		return needAttack;
	}
	
	private bool CastSkills(string[] skIds){
		int  skIndex = Random.Range(0,3);
		bool canCast = false;
		
		for (int i=0; i<skIds.Length; i++){
			SkillIconData skillIconData = SkillEnemyManager.Instance.getSkillIconData(skIds[skIndex]);
		
			if(skillIconData != null && !skillIconData.isCoolDown)
			{
				if(this.enemy.targetObj == null)
				{
					this.enemy.targetObj = base.getOpponent().gameObject;
				}
				this.enemy.PushSkillIdToContainer(skIds[skIndex]);	
				canCast = false;
				i = skIds.Length;
			}
			else{
				skIndex = (skIndex + 1) % skIds.Length;
			}
		}
		
		return canCast;
	}
}

