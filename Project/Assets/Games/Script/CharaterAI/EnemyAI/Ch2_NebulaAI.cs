using UnityEngine;
using System.Collections;

public class Ch2_NebulaAI : EnemyAI {

	private const float skillCastChance = .5f;
	private string[] sk50u = {"NEBULA1", "NEBULA5A"};
	private string[] sk50l = {"NEBULA15A","NEBULA30A"};
	private int hp50;
	
	public void Start(){
		hp50 = (int)(this.character.realMaxHp * 0.5f * LevelMgr.Instance.curLevelDifficulty);
	}
	
	public override bool OnAtkAnimaScriptTargetBefore()
	{
		bool needAttack = true;
		if (Random.value > skillCastChance) {
			needAttack = true;
		}
		else if (this.character.realHp >= hp50){
			needAttack = !CastSkills(sk50u);
		}
		else{
			needAttack = !CastSkills(sk50l);
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
			
			if(skIds[skIndex] == "NEBULA30A" &&
				this.character.attackAnimaName == "Skill30A_b" &&
				this.character.realHp < hp50){
				return true;
			}
			
			if(skIds[skIndex] == "NEBULA5A" &&
				GameObject.Find("SkillEft_NEBULA5A_DeckGun(Clone)") != null &&
				this.character.realHp >= hp50){
				return true	;
			}
		
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
