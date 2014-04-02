using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ch1_Korath : EnemyRemote
{
	public delegate void StunBuff();
	public StunBuff addStunBuffCallBack;
	
	public override void blinkInScreen()
	{
		gameObject.transform.position = BattleBg.getPointInScreen();
	}
	
	protected override void AnimaPlayEnd ( string animaName  )
	{
//		Debug.Log(">>>>>>animaName:"+animaName);
//		Debug.LogError(animaName + " !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		switch(animaName)
		{			
			case "Skill5A":
			case "Skill30A":
				if(!TsTheater.InTutorial)
				{
					if(targetObj != null)
					{
						this.state = Character.STANDBY_STATE;
						MoveToPoint(BattleBg.getPointInAround(targetObj.transform.position,100,150));					
					}
					else
					{
						standby();
					}
				}
				else
				{
					standby();
				}
				
				break;
			case "Skill1":
			case "SkillA":
			case "Skill15A":
				SkillFinish();
				break;
		}
		base.AnimaPlayEnd(animaName);
	}
	
	public void moveToPosFinished(Character c = null)
	{
		isMove = false;
		startAtk();
		this.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished, moveToPosFinished);
	}
	
	public void addStunBuff(string s)
	{
		if(null != addStunBuffCallBack)
		{
			addStunBuffCallBack();
		}
	}
	
	protected void initSkill()
	{
		SkillEnemyManager.Instance.createSkillIcon("KORATH1");
		SkillEnemyManager.Instance.createSkillIcon("KORATH5A");
		SkillEnemyManager.Instance.createSkillIcon("KORATH15A");
		SkillEnemyManager.Instance.createSkillIcon("KORATH30A");
	}
	
	public override void Start ()
	{
		initSkill();
		base.Start ();
		
		pieceAnima.addFrameScript("Skill30A",39,addStunBuff);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill30A",39);
	}
}
