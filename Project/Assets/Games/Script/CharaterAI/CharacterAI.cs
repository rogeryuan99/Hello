using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAI : MonoBehaviour
{
	public Character character;
	
	public virtual void getCharacter()
	{
		this.character = gameObject.GetComponent<Character>();
	}
	
	public virtual void setCharacter(Character character)
	{
		this.character = character;
	}
	
	public virtual bool OnAtkAnimaScriptTargetBefore()
	{
		return true;
	}
	
	public virtual void OnAtkAnimaScriptTarget(Character targetCharacter)
	{
		
	}
	
	public virtual bool OnAtkAnimaScriptTargetAfter()
	{
		return true;
	}
	
	public virtual void OnAtkAnimaScriptTargetIsNull()
	{
		
	}
	
	public virtual void OnAtkAnimaScriptTargetIsDeath ()
	{
		
	}
	
	
	public virtual bool OnAttackTargetInvokTargetBefore()
	{
		return true;
	}
	
	public virtual void OnAttackTargetInvokTargetIsNull()
	{
	}
	
	public virtual void OnAttackTargetInvokTargetIsDeath()
	{
		
	}
	
	public virtual bool OnAttackTargetInvokAttackTargetBefore()
	{
		return true;
	}
	
	public virtual void OnAttackTargetInvokAttackTarget()
	{
		this.character.toward(this.character.targetObj.transform.position);
		this.character.isPlayAtkAnim = true;
		this.character.playAnim(this.character.attackAnimaName);
	}
	
	public virtual bool OnAttackTargetInvokAttackTargetAfter()
	{
		return true;
	}
	
	
	
	public virtual void OnCheckAtkerDefense(GameObject atker)
	{
	}
		
	public virtual void OnDefenseAtkHurtBeforeByDamageValue(int dam)
	{
	}
	
	public virtual void OnDefenseAtkHurtAfterByRemainsCurrentHP(int currentHP)
	{
		
	}
	
	
	public virtual void OnRealDamageHurtBeforeByDamageValue(int dam)
	{

	}
	
	public virtual void OnRealDamageHurtAfterByRemainsCurrentHP(int currentHP)
	{
		
	}
	
	
	
	public virtual void OnMoveWhenTargetDead()
	{
	}
	
	
	// bool true: have opponent. false:  have not opponent 
	public virtual bool checkOpponent()
	{
		return true;
	}
	
	public virtual Character getOpponent(Character primaryOpponent = null)
	{
		return null;
	}
	
	public virtual void OnGetOpponentLater (Character opponent = null)
	{
	}
	
	public virtual void OnMoveToTargetDistance(float dis)
	{
		
	}
	
	public virtual void OnReachTarget(Vector2 vc)
	{
		if (BarrierMapData.Enable && this.character.movePath.Count > 1)
		{
			this.character.movePath.RemoveAt(0);
		}
		else if(this.character.isAbnormalStateActive(Character.ABNORMAL_NUM.TWINE))
		{
			this.character.startAtk();
		}
		else if(this.character.isAbnormalStateActive(Character.ABNORMAL_NUM.FREEZE))
		{
			return;
		}
		else if(this.character.isAbnormalStateActive(Character.ABNORMAL_NUM.STUN))
		{
			return;	
		}
		else
		{
			//MusicManager.stopMoveMusic();
			if(this.character.state == Character.CAST_STATE)
			{
				return;
			}
			transform.position = new Vector3(vc.x, vc.y, transform.position.z);
			if (gameObject.tag != this.character.targetObj.tag || this.character.isAtkSameTag)
			{
				
				this.character.startAtk();
			}
			else
			{
				this.character.standby();
			}
		}
	}
}
