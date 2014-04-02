using UnityEngine;
using System.Collections;

public class Skill_REDKING1 : SkillBase
{
	
	protected ArrayList objs;
	
	protected GameObject flashChainPrb;
	protected GameObject flashChain;
	
	protected GameObject flashBodyPrb;
	protected GameObject flashBody;
	
	protected GameObject damageEftPrb;
	protected GameObject damageEft;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		
		character.toward(target.transform.position);
		character.castSkill("SkillA");
		
		if(character is RedKing)
		{
			RedKing redKing = character as RedKing;
			redKing.showSkill1EftCallback += showSkill1Eft;
		}
		else if(character is Ch3_RedKing)
		{
			Ch3_RedKing redKing = character as Ch3_RedKing;
			redKing.showSkill1EftCallback += showSkill1Eft;
		}
		
		this.objs = objs;
		
		yield return new WaitForSeconds(.5f);
		
//		Character targetCharacter = target.GetComponent<Character>();
//		targetCharacter.addAbnormalState(100, null, Character.ABNORMAL_NUM.STUN);
		
	}
	
	public void showSkill1Eft(Character character)
	{
		if(character is RedKing)
		{
			RedKing redKing = character as RedKing;
			redKing.showSkill1EftCallback -= showSkill1Eft;
		}
		else if(character is Ch3_RedKing)
		{
			Ch3_RedKing redKing = character as Ch3_RedKing;
			redKing.showSkill1EftCallback -= showSkill1Eft;
		}
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
	
		Destroy(this.flashChain);
		StaticData.createObjFromPrb(ref this.flashChainPrb, "eft/RedKing/SkillEft_RedKing1_FlashChain", ref this.flashChain, null);
		
		PackedSprite flashChainPs = this.flashChain.GetComponent<PackedSprite>();
		
		
		
		if(character.model.transform.localScale.x > 0)
		{
			this.flashChain.transform.position = caller.transform.position + new Vector3(76 , 32, -100);
		}
		else
		{
			this.flashChain.transform.position = caller.transform.position + new Vector3(-76 , 32, -100);
		}
		
	
		this.flashChain.transform.localScale = new Vector3(0, 1, 1);
			
		
		Vector3 chainEndPos = target.transform.position + new Vector3(0, 28, 0);
		
		
		float dis = Vector3.Distance(this.flashChain.transform.position, chainEndPos);
		
		float dis_y = chainEndPos.y - this.flashChain.transform.position.y;
		float dis_x = chainEndPos.x - this.flashChain.transform.position.x;
		
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		float deg = (angle*360)/(2*Mathf.PI);
		
		this.flashChain.transform.localRotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		
		float scale = dis / flashChainPs.width;
		
		iTween.ScaleTo(this.flashChain, 
				new Hashtable()
				{
					{"x", scale},
					{"time", 0.3f},
					{"islocal", true},
					{"oncomplete","flashChainStretchFinish"},
					{"oncompletetarget",gameObject}
				}
			);
		
		Destroy(this.flashBody);
		StaticData.createObjFromPrb(ref this.flashBodyPrb, "eft/RedKing/SkillEft_RedKing1_FlashBody", ref this.flashBody, null);
		this.flashBody.transform.parent = target.transform;
		this.flashBody.transform.localPosition = new Vector3(0,254,-1);

	}
	
	public void flashChainStretchFinish()
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		
		if(target == null)
		{
			Destroy(this.flashChain);
			Destroy(this.flashBody);
			return;
		}
		
		Character targetCharacter = target.GetComponent<Character>();
		
		if(targetCharacter.getIsDead())
		{
			Destroy(this.flashChain);
			Destroy(this.flashBody);
			return;
		}
		
		
		iTween.ScaleTo(this.flashChain, 
				new Hashtable()
				{
					{"x", 0},
					{"time", 0.3f},
					{"islocal", true},
					{"oncomplete","flashChainShrinkFinish"},
					{"oncompletetarget",gameObject}
				}
			);
		
		Vector3 targetMovePos = Vector3.zero;
		
		Character callerCharcter = caller.GetComponent<Character>();
		if(callerCharcter.model.transform.localScale.x > 0)
		{
			targetMovePos = callerCharcter.transform.position + new Vector3(160, 0, 0);
		}
		else
		{
			targetMovePos = callerCharcter.transform.position + new Vector3(-160, 0, 0);
		}
		
		iTween.MoveTo(target, 
				new Hashtable()
				{
					{"position", targetMovePos},
					{"time", 0.3f},
					{"islocal", false}
				}
			);
	}
	
	public void flashChainShrinkFinish()
	{
		Destroy(this.flashChain);
		Destroy(this.flashBody);
		
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(target == null)
		{
			return;
		}
		
		Character targetCharacter = target.GetComponent<Character>();
		
		if(targetCharacter.getIsDead())
		{
			return;
		}
		
		Character character = caller.GetComponent<Character>();
		
		if(character is RedKing)
		{
			RedKing redKing = character as RedKing;
			redKing.showSkill1DamageEftCallback += showSkill1DamageEft;
		}
		else if(character is Ch3_RedKing)
		{
			Ch3_RedKing redKing = character as Ch3_RedKing;
			redKing.showSkill1DamageEftCallback += showSkill1DamageEft;
		}
	}
	
	public void showSkill1DamageEft(Character character)
	{
		GameObject target = objs[2] as GameObject;
		
		if(character is RedKing)
		{
			RedKing redKing = character as RedKing;
			redKing.showSkill1DamageEftCallback -= showSkill1DamageEft;
		}
		else if(character is Ch3_RedKing)
		{
			Ch3_RedKing redKing = character as Ch3_RedKing;
			redKing.showSkill1DamageEftCallback -= showSkill1DamageEft;
		}
		
		if(target == null)
		{
			return;
		}
		
		Character targetCharacter = target.GetComponent<Character>();
		
		if(targetCharacter.getIsDead())
		{
			return;
		}
		
		float damagePosX = 0;
		float damageLsX = 0;
		
		if(character.model.transform.localScale.x > 0 )
		{
			damagePosX = -277.899f;
			damageLsX = -2;
		}
		else
		{
			damagePosX = 277.899f;
			damageLsX = 2;
		}
		
		StaticData.createObjFromPrb(ref damageEftPrb, 
			"eft/RedKing/SkillEft_RedKing1_DamageEft", 
			ref this.damageEft, 
			target.transform, 
			new Vector3(damagePosX, 272, 0), 
			new Vector3(damageLsX, 2, 1));
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("REDKING1");
		
//		targetCharacter.realDamage(targetCharacter.getSkillDamageValue(character.realAtk, ((Effect)skillDef.activeEffectTable["atk_PHY"]).num));
//		Debug.Break();
	}
}
