using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CAIERA30A : SkillBase
{
	
	public GameObject attackEftPrb;
	public GameObject damageEftPrb;
	
	public ArrayList objs;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character character = caller.GetComponent<Character>();
		
		if(Vector3.Distance(caller.transform.position, target.transform.position) > character.data.attackRange + 10.0f)
		{
			character.moveToTarget(target);
			character.PushSkillIdToContainer("CAIERA30A");
			yield break;
		}
		
		yield return new WaitForSeconds(0.5f);
		MusicManager.playEffectMusic("SFX_Caiera_The_Oldstrong_1a");
		
		this.objs = objs;
		
		character.castSkill("Skill30A");

		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.showSkill30AAttackEftCallback += showSkill30AAttackEft;
			caiera.showSkill30ADamageEftCallback += showSkill30ADamageEft;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.showSkill30AAttackEftCallback += showSkill30AAttackEft;
			caiera.showSkill30ADamageEftCallback += showSkill30ADamageEft;
		}
		
		
	}
	
	public void showSkill30AAttackEft(Character c)
	{
		if(c is Caiera)
		{
			Caiera caiera = c as Caiera;
			caiera.showSkill30AAttackEftCallback -= showSkill30AAttackEft;
		}
		else if(c is Ch3_Caiera)
		{
			Ch3_Caiera caiera = c as Ch3_Caiera;
			caiera.showSkill30AAttackEftCallback -= showSkill30AAttackEft;
		}
		
		
		int x = 0;
		int lsx = 0;
		if(c.model.transform.localScale.x > 0)
		{
			x = -54;
			lsx = 4;
		}
		else
		{
			x = -54;
			lsx = -4;
		}
		GameObject attackEft = null;
		
		StaticData.createObjFromPrb(ref attackEftPrb, "eft/Caiera/Skill_CAIERA30A_AttackEft", ref attackEft, c.transform, new Vector3(x, 256, 0), new Vector3(lsx, 4, 1));
	}
	
	protected float targetOldPosY;
	
	public void showSkill30ADamageEft(Character c)
	{
		Hashtable characterTable = null;
		Hashtable dropAtkTable = null;
		
		if(c is Caiera)
		{
			Caiera caiera = c as Caiera;
			caiera.showSkill30ADamageEftCallback -= showSkill30ADamageEft;
			characterTable = EnemyMgr.enemyHash;
			dropAtkTable = HeroMgr.heroHash;
		}
		else if(c is Ch3_Caiera)
		{
			Ch3_Caiera caiera = c as Ch3_Caiera;
			caiera.showSkill30ADamageEftCallback -= showSkill30ADamageEft;
			characterTable = HeroMgr.heroHash;
			dropAtkTable = EnemyMgr.enemyHash;
		}
		
		
		
		int x = 0;
		
		if(c.model.transform.localScale.x > 0)
		{
			x = 530;
		}
		else
		{
			x = -530;
		}
		
		GameObject damageEft = null;
		StaticData.createObjFromPrb(ref damageEftPrb, "eft/Caiera/Skill_CAIERA30A_DamageEft", ref damageEft, c.transform, new Vector3(x, 331, -1), new Vector3(4, 4, 1));

		
		GameObject target = objs[2] as GameObject;
		
		Character targetCharacter = target.GetComponent<Character>();
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["CAIERA30A"] as SkillDef;
		
		targetCharacter.addAbnormalState(skillDef.skillDurationTime, stunStateFinish, Character.ABNORMAL_NUM.STUN) ;
		
		float y = Camera.main.orthographicSize + 100;
		
		targetOldPosY = target.transform.position.y;
		
		targetCharacter.standby();
		
		
		ArrayList otherCharacterList = new ArrayList(characterTable.Values);
		
		int radius = (int)skillDef.activeEffectTable["AOERadius"];
		
		foreach(Character otherCharacter in otherCharacterList)
		{
			if(otherCharacter.getID() != targetCharacter.getID())
			{
				Vector2 vc2 = otherCharacter.transform.position - targetCharacter.transform.position;
				if( StaticData.isInOval(radius ,radius , vc2) )
				{
					if(StaticData.computeChance(80, 100))
					{
						otherCharacter.addAbnormalState(skillDef.skillDurationTime, null, Character.ABNORMAL_NUM.STUN);
					}
				}
			}
		}
		
		
		ArrayList dropAtkCharacterTableList = new ArrayList(dropAtkTable.Values);
		
		iTween.MoveTo(target, 
			iTween.Hash
			(
				"y",y,
				"time",0.5f,
				"easetype","linear",
				"islocal",false,
				"oncomplete","dropAtkPosition",
				"oncompletetarget",gameObject,
				"oncompleteparams",dropAtkCharacterTableList
			)
		);
		
		
		
		
		
	}
	
	public void dropAtkPosition(ArrayList dropAtkCharacterTableList)
	{
		GameObject target = objs[2] as GameObject;
		
		Character targetCharacter = target.GetComponent<Character>();
		
		foreach(Character dropAtkCharacter in dropAtkCharacterTableList)
		{
			if(targetCharacter == null)
			{
				return;
			}
			if(dropAtkCharacter.targetObj == null)
			{
				continue;
			}
			if(dropAtkCharacter.targetObj.GetComponent<Character>().getID() == targetCharacter.getID())
			{
				dropAtkCharacter.targetObj = null;
				dropAtkCharacter.dropAtkPosition(targetCharacter);
				dropAtkCharacter.standby();
			}
		}
	}
			
	public void stunStateFinish(State state, Character character)
	{
		iTween.MoveTo(character.gameObject, 
			iTween.Hash
			(
				"y",targetOldPosY,
				"time",0.5f,
				"easetype","linear",
				"islocal",false,
				"oncomplete","moveFinish",
				"oncompleteparams",character,
				"oncompletetarget",this.gameObject
			)
		);
	}
	
	public void moveFinish(Character character)
	{
		character.startCheckOpponent();
		character.standby();
	}
}
