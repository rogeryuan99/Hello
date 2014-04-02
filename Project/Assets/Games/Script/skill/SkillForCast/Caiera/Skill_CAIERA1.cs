using UnityEngine;
using System.Collections;

public class Skill_CAIERA1 : SkillBase 
{
	protected ArrayList objs;
	
	protected GameObject flashChainPrb;
	protected GameObject flashChain;
	
	protected GameObject bodyEftPrb;
	protected GameObject bodyEft;
	
	protected GameObject FEMALE_Weapon_03Prb;
	protected GameObject FEMALE_Weapon_03;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		
		character.toward(target.transform.position);
		character.castSkill("SkillA");
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.stretchSkill1ChainCallback += stretchSkill1Chain;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.stretchSkill1ChainCallback += stretchSkill1Chain;
		}
		
		this.objs = objs;
		
		yield return new WaitForSeconds(0f);
		
		MusicManager.playEffectMusic("SFX_Caiera_Come_Here_1a");
	}
	
	protected State stunState = new State(10, null);
	
	public void stretchSkill1Chain(Character character)
	{
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.stretchSkill1ChainCallback -= stretchSkill1Chain;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.stretchSkill1ChainCallback -= stretchSkill1Chain;
		}
		
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character targetCharacter = target.GetComponent<Character>();
		
		targetCharacter.addAbnormalState(stunState,Character.ABNORMAL_NUM.STUN);
	
		Destroy(this.flashChain);
		StaticData.createObjFromPrb(ref this.flashChainPrb, "eft/Caiera/SkillEft_CAIERA1_Chain", ref this.flashChain, null);
		
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
		
		Vector3 chainEndPos = target.transform.position + new Vector3(0, 32, 0);
		
		float dis = Vector3.Distance(this.flashChain.transform.position, chainEndPos);
		
		float dis_y = chainEndPos.y - this.flashChain.transform.position.y;
		float dis_x = chainEndPos.x - this.flashChain.transform.position.x;
		
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		float deg = (angle*360)/(2*Mathf.PI);
		
		this.flashChain.transform.localRotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		float scale = dis / flashChainPs.width;
		
		Destroy(this.FEMALE_Weapon_03);
		StaticData.createObjFromPrb(ref this.FEMALE_Weapon_03Prb, "eft/Caiera/SkillEft_CAIERA1_FEMALE_Weapon_03", ref this.FEMALE_Weapon_03, null);
		
		this.FEMALE_Weapon_03.transform.position = this.flashChain.transform.position;
		this.FEMALE_Weapon_03.transform.localRotation = Quaternion.Euler(new Vector3(0,0,deg - 90));
		
		iTween.ScaleTo(this.flashChain, 
				new Hashtable()
				{
					{"x", scale},
					{"time", 0.2f},
					{"islocal", true},
					{"oncomplete","flashChainStretchFinish"},
					{"oncompletetarget",gameObject}
				}
			);
		
		
		
		iTween.MoveTo(this.FEMALE_Weapon_03, 
				new Hashtable(){
					{"x",chainEndPos.x},
					{"y",chainEndPos.y},
					{"time", 0.2f}
				}
			);
	}
	
	public void flashChainStretchFinish()
	{
		Destroy(this.FEMALE_Weapon_03);
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(target == null)
		{
			Destroy(this.flashChain);
			return;
		}
		
		Character targetCharacter = target.GetComponent<Character>();
		
		if(targetCharacter.getIsDead())
		{
			Destroy(this.flashChain);
			return;
		}
		
		
		Destroy(this.bodyEft);
		
		StaticData.createObjFromPrb(ref this.bodyEftPrb, "eft/Caiera/SkillEft_CAIERA1_BodyEft", ref this.bodyEft, target.transform, new Vector3(0,354.7676f,0));
		
		StartCoroutine(chainShrink());
		
	}
	
	public IEnumerator chainShrink()
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		yield return new WaitForSeconds(0.5f);
		iTween.ScaleTo(this.flashChain, 
				new Hashtable()
				{
					{"x", 0},
					{"time", 0.2f},
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
		stunState.endTime = 0;
		Destroy(this.bodyEft);
		Destroy(this.flashChain);
	}
}
