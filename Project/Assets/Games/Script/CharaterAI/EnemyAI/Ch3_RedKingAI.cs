using UnityEngine;
using System.Collections;

public class Ch3_RedKingAI : EnemyAI {

	private const float skillCastChance = .5f;
	private string[] sk70u = {"REDKING1", "REDKING5A"};
	private string[] sk30u = {"REDKING5A", "REDKING15A"}; 
	private string[] sk30l = {"REDKING15A","REDKING30A"};
	private int hp70;
	private int hp40;
	
	public void Start(){
		hp70 = (int)(this.character.realMaxHp * 0.7f);
		hp40 = (int)(this.character.realMaxHp * 0.3f);
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
			needAttack = !CastSkills(sk30u);
		}
		else{
			needAttack = !CastSkills(sk30l);
		}
		
		if(this.enemy.skContainer.Count >= 1)
		{
			SkillEnemyManager.Instance.CastSkill(this.enemy);
			needAttack = false;
		}
		return needAttack;
	}
	
	private bool CastSkills(string[] skIds){
		int  skIndex = Random.Range(0,skIds.Length);
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
