using UnityEngine;
using System.Collections;

public class Ch3_Charlie27 : Enemy {

//	public delegate void StunBuff();
//	public StunBuff addStunBuffCallBack;
	
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
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE271");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE275A");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE275B");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE2715A");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE2715B");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE2730A");
		SkillEnemyManager.Instance.createSkillIcon("CHARLIE2730B");
		
		base.Start();
		
		pieceAnima.addFrameScript("SkillA", 8, showSkill1LightCrashEft);
		pieceAnima.addFrameScript("SkillA", 21, showSkill1RushLightEft);
		pieceAnima.addFrameScript("SkillA",28,showSkill1Damage);
		pieceAnima.addFrameScript("Skill15A",26,showSkill15AHitEft);
		pieceAnima.addFrameScript("Skill15A",28,showSkill15ABangEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA", 8);
		pieceAnima.removeFrameScript("SkillA", 21);
		pieceAnima.removeFrameScript("SkillA", 28);
		pieceAnima.removeFrameScript("Skill15A", 26);
		pieceAnima.removeFrameScript("Skill15A", 28);
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
//	public override void blinkInScreen()
//	{
//		gameObject.transform.position = BattleBg.getPointInScreen();
//	}
//	
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.LogError(animaName + " !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		switch(animaName){		
			case "SkillA":
			case "Skill5A":
			case "Skill5B":
			case "Skill15A":
			case "Skill15B":
			case "Skill30A":
			case "Skill30B":
				if(!TsTheater.InTutorial && targetObj != null){
					this.state = Character.STANDBY_STATE;
					// MoveToPoint(BattleBg.getPointInAround(targetObj.transform.position,100,150));					
					moveToTarget(targetObj);
				}
				else{
					standby();
				}
				break;
//			case "SkillA":
//			case "Skill15A":
//				SkillFinish();
//				break;
		}
		base.AnimaPlayEnd(animaName);
	}
}
