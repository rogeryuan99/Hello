using UnityEngine;
using System.Collections;

public class Bug : Hero
{
	public GameObject attackEft;
	
	public delegate void AttackAnimaEvent(Character character, Character target);
	public event AttackAnimaEvent attackAnimaEvent;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate Skill15AHitEftCallback;
	
	public event ParmsDelegate showSkill30AEftCallback;
	
	public event ParmsDelegate showSkill15BEftCallback;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 18;
	}
	
	public override void Start()
	{
		base.Start();
		
		pieceAnima.addFrameScript("Skill30B",33, addSkill15AHitEft);
		
		pieceAnima.addFrameScript("Skill30A",26, showSkill30AEft);
		
		pieceAnima.addFrameScript("Skill15B",26, showSkill15BEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill30A",26);
		pieceAnima.removeFrameScript("Skill30B",33);
		pieceAnima.removeFrameScript("Skill15B",26);
	}
	
	public void showSkill15BEft(string s)
	{
		if(showSkill15BEftCallback != null){
			showSkill15BEftCallback(this);
		}
	}
	
	public void showSkill30AEft(string s)
	{
		if(showSkill30AEftCallback != null){
			showSkill30AEftCallback(this);
		}
	}
	
	public void addSkill15AHitEft(string s)
	{
		if(Skill15AHitEftCallback != null){
			Skill15AHitEftCallback(this);
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
			return;
		} 
		
//		MusicManager.playEffectMusic("SFX_Gamora_Basic_1a");
		Vector3 eft;
		if(model.transform.localScale.x > 0)
		{
			eft = transform.position + new Vector3(100,40,-50);
		}else{
			eft = transform.position + new Vector3(-100,40,-50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		if(attackAnimaEvent != null && targetObj != null)
		{
			Character target = targetObj.GetComponent<Character>();
			if(!target.isDead)
			{
				attackAnimaEvent(this, target);
				return;
			}
		}
		
		base.atkAnimaScript("");
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
}
