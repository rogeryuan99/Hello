using UnityEngine;
using System.Collections;

public class Skill_CAIERA15A : SkillBase
{
	
	protected ArrayList objs;
	
	protected GameObject flashChainPrb;
	protected GameObject flashChain;
	
	protected GameObject bodyEftPrb;
	protected GameObject bodyEft;
	
	protected GameObject FEMALE_Weapon_03Prb;
	protected GameObject FEMALE_Weapon_03;
	
	protected GameObject lightPrb;
	protected GameObject light;
	
	protected GameObject fracturingEftPrb;
	protected GameObject fracturingEft;
	
	protected GameObject damageEftPrb;
	protected GameObject damageEft;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		
		character.toward(target.transform.position);
		character.castSkill("Skill15A");
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.stretchSkill15AChainCallback += stretchChain;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.stretchSkill15AChainCallback += stretchChain;
		}
		
		this.objs = objs;
		
		yield return new WaitForSeconds(0f);
		
		MusicManager.playEffectMusic("SFX_Caiera_Chain_Slam_1a");
	}
	
	protected State stunState = new State(10, null);
	
	public void stretchChain(Character character)
	{
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.stretchSkill15AChainCallback -= stretchChain;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.stretchSkill15AChainCallback -= stretchChain;
		}
		
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character targetCharacter = target.GetComponent<Character>();
		
		stunState.endTime = 10;
		targetCharacter.standby();
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
					{"time", 0.1f},
					{"islocal", true},
					{"oncomplete","flashChainStretchFinish"},
					{"oncompletetarget",gameObject}
				}
			);
		
		
		
		iTween.MoveTo(this.FEMALE_Weapon_03, 
				new Hashtable(){
					{"x",chainEndPos.x},
					{"y",chainEndPos.y},
					{"time", 0.1f}
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
		
		yield return new WaitForSeconds(0.3f);
		iTween.ScaleTo(this.flashChain, 
				new Hashtable()
				{
					{"x", 0},
					{"time", 0.1f},
					{"islocal", true},
					{"oncomplete","flashChainShrinkFinish"},
					{"oncompletetarget",gameObject}
				}
			);
		
		Vector3 targetMovePos = Vector3.zero;
		
		Character callerCharcter = caller.GetComponent<Character>();
		if(callerCharcter.model.transform.localScale.x > 0)
		{
			targetMovePos = callerCharcter.transform.position + new Vector3(300, 0, 0);
		}
		else
		{
			targetMovePos = callerCharcter.transform.position + new Vector3(-300, 0, 0);
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
//		stunState.endTime = 0;
		
		Destroy(this.bodyEft);
		Destroy(this.flashChain);
		
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
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.skill15AFirstRotateTargetCallback += firstRotateTarget;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.skill15AFirstRotateTargetCallback += firstRotateTarget;
		}
	}
	
	public void firstRotateTarget(Character character)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.skill15AFirstRotateTargetCallback -= firstRotateTarget;
			caiera.showSkill15ADamageEftCallback += showDamageEft;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.skill15AFirstRotateTargetCallback -= firstRotateTarget;
			caiera.showSkill15ADamageEftCallback += showDamageEft;
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
		
		target.transform.position = character.transform.position + new Vector3(0, 300, 0);
		
		Destroy(this.light);
		StaticData.createObjFromPrb(ref this.lightPrb, "eft/Caiera/SkillEft_CAIERA15A_Light", ref this.light, target.transform, new Vector3(0,0,0));
		
		if(character.model.transform.localScale.x > 0)
		{
			this.light.transform.localPosition = new Vector3(496.9783f, -38.2303f, 1);
			this.light.transform.localScale = new Vector3(4, 4, 1);
		}
		else
		{
			this.light.transform.localPosition = new Vector3(-581.0858f, -11.50698f, 1);
			this.light.transform.localScale = new Vector3(-4, 4, 1);
		}
		
//		Debug.Break();

	}
	
	public void showDamageEft(Character character)
	{
		
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.skill15ASecondRotateTargetCallback += secondRotateTarget;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.skill15ASecondRotateTargetCallback += secondRotateTarget;
		}
		
		float x = 0;
		if(character.model.transform.localScale.x > 0)
		{
			x = -250;
		}
		else
		{
			x = 250;
		}
		target.transform.position = caller.transform.position + new Vector3(x, 0, 0);
//		this.light.transform.position = target.transform.position;
		
		Destroy(this.damageEft);
		Destroy(this.fracturingEft);
		StaticData.createObjFromPrb(ref this.fracturingEftPrb, "eft/Caiera/SkillEft_CAIERA15A_Fracturing", ref this.fracturingEft, null);
		StaticData.createObjFromPrb(ref this.damageEftPrb, "eft/Caiera/SkillEft_CAIERA15A_Damage", ref this.damageEft, null);
		
		if(character.model.transform.localScale.x > 0)
		{
			this.light.transform.localPosition = new Vector3(382.2884f, 439.65f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,45));
		}
		else
		{
			this.light.transform.localPosition = new Vector3(-396.972f, 517.8419f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-45));
		}
		
		this.damageEft.transform.position = target.transform.position;
		this.fracturingEft.transform.position = target.transform.position + new Vector3(0, -30, -1);
		
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("CAIERA15A");
		
		
		Character targetCharacter = target.GetComponent<Character>();
		int damage = targetCharacter.getSkillDamageValue(character.realAtk, ((Effect)skillDef.activeEffectTable["atk_PHY"]).num);
		targetCharacter.realDamage(damage / 2);
		
//		Debug.Break();
	}
	
	public void secondRotateTarget(Character character)
	{
		Destroy(this.damageEft);
		Destroy(this.fracturingEft);
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.skill15ASecondRotateTargetCallback -= secondRotateTarget;
			caiera.skill15AFinishCallback += skillFinish;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.skill15ASecondRotateTargetCallback -= secondRotateTarget;
			caiera.skill15AFinishCallback += skillFinish;
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
		
		target.transform.position = character.transform.position + new Vector3(0, 300, 0);
		
		if(character.model.transform.localScale.x > 0)
		{
			this.light.transform.localPosition = new Vector3(-350.8037f, 22.19457f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
			this.light.transform.localScale = new Vector3(-4, 4, 1);
		}
		else
		{
			this.light.transform.localPosition = new Vector3(460.3079f, 74.76176f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
			this.light.transform.localScale = new Vector3(4, 4, 1);
		}
//		Debug.Break();
	}
	
	public void skillFinish(Character character)
	{
		Destroy(this.light);
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.skill15AFinishCallback -= skillFinish;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.skill15AFinishCallback -= skillFinish;
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
		
		float x = 0;
		if(character.model.transform.localScale.x > 0)
		{
			x = 300;
		}
		else
		{
			x = -300;
		}
		
		target.transform.position = character.transform.position + new Vector3(x, 0, 0);
		
		Destroy(this.damageEft);
		Destroy(this.fracturingEft);
		StaticData.createObjFromPrb(ref this.fracturingEftPrb, "eft/Caiera/SkillEft_CAIERA15A_Fracturing", ref this.fracturingEft, null);
		StaticData.createObjFromPrb(ref this.damageEftPrb, "eft/Caiera/SkillEft_CAIERA15A_Damage", ref this.damageEft, null);
		
		if(character.model.transform.localScale.x > 0)
		{
			this.light.transform.localPosition = new Vector3(-450.0904f, 564.0514f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-45));
			this.light.transform.localScale = new Vector3(-4, 4, 1);
		}
		else
		{
			this.light.transform.localPosition = new Vector3(466.0642f, 564.0514f, 1);
			this.light.transform.localRotation = Quaternion.Euler(new Vector3(0,0,45));
			this.light.transform.localScale = new Vector3(4, 4, 1);
		}
		
		this.damageEft.transform.position = target.transform.position;
		this.fracturingEft.transform.position = target.transform.position + new Vector3(0, -30, -1);
		
//		Debug.Break();
		
		StartCoroutine(destroyEft());
		
		stunState.endTime = 0;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("CAIERA15A");
		
		int damage = targetCharacter.getSkillDamageValue(character.realAtk, ((Effect)skillDef.activeEffectTable["atk_PHY"]).num);
		targetCharacter.realDamage(damage / 2);
	}
	
	public IEnumerator destroyEft()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.damageEft);
		Destroy(this.fracturingEft);
	}
}
