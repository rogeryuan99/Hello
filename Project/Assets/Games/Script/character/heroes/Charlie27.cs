using UnityEngine;
using System.Collections;

public class Charlie27 : Hero
{
	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public ParmsDelegate showSkill1LightCrashEftCallback;
	public ParmsDelegate showSkill1RushLightEftCallback;
	public ParmsDelegate showSkill1DamageCallback;
	public ParmsDelegate showSkill15AHitEftCallback;
	public ParmsDelegate showSkill15ABangEftCallback;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	
	public override void Start()
	{
		base.Start();
		
		pieceAnima.addFrameScript("SkillA", 8, showSkill1LightCrashEft);
		pieceAnima.addFrameScript("SkillA", 21, showSkill1RushLightEft);
		pieceAnima.addFrameScript("SkillA",28,showSkill1Damage);
		pieceAnima.addFrameScript("Skill15A",25,showSkill15AHitEft);
		pieceAnima.addFrameScript("Skill15A",30,showSkill15ABangEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA", 8);
		pieceAnima.removeFrameScript("SkillA", 21);
		pieceAnima.removeFrameScript("SkillA", 28);
		pieceAnima.removeFrameScript("Skill15A", 25);
		pieceAnima.removeFrameScript("Skill15A", 30);
	}
	
	private void showSkill1LightCrashEft(string s){
		if(showSkill1LightCrashEftCallback != null){
			showSkill1LightCrashEftCallback(this);	
		}
	}
	
	private void showSkill1RushLightEft(string s){
		if(showSkill1RushLightEftCallback != null){
			showSkill1RushLightEftCallback(this);
		}
	}
	
	private void showSkill1Damage(string s){
		if(showSkill1DamageCallback != null){
			showSkill1DamageCallback(this);
		}
	}
	
	private void showSkill15AHitEft(string s){
		if(showSkill15AHitEftCallback != null){
			showSkill15AHitEftCallback(this);
		}
	}
	
	private void showSkill15ABangEft(string s){
		if(showSkill15ABangEftCallback != null){
			showSkill15ABangEftCallback(this);
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
			eft = transform.position + new Vector3(170, 80, -50);
		}else{
			eft = transform.position + new Vector3(-170, 80, -50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		base.atkAnimaScript("");
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
}
