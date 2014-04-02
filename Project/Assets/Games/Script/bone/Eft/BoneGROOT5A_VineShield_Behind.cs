using UnityEngine;
using System.Collections;

public class BoneGROOT5A_VineShield_Behind : PieceAnimation
{

	public GameObject branch_06;
	public GameObject branch_07;
	
	public VineShield vs;
	
	public override void Awake ()
	{ 		
		base.Awake();
		addFrameScript("SkillB",9 , PauseAnima);
		animaPlayEndScript(animaPlayEnd);
		
	}
	
	public void PauseAnima(string s)
	{
		this.pauseAnima();
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		partList ["branch_06"]=     branch_06;
		partList ["branch_061"]=   branch_06;
		partList ["branch_07"]=     branch_07;
		partList ["branch_072"]=   branch_07;
		
	}
	
	protected void animaPlayEnd (string s)
	{
		vs.isVineShieldBehindAnimaPlayEnd = true;
		vs.destroySelf();
	}
	
	public override void OnDestroy()
	{
		removeFrameScript("SkillB", 9);
		base.OnDestroy();
	}
}
