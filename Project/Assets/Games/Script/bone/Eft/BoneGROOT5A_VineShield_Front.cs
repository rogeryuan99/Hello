using UnityEngine;
using System.Collections;

public class BoneGROOT5A_VineShield_Front : PieceAnimation
{

	public GameObject branch_05;
	
	public VineShield vs;
	
	public override void Awake ()
	{ 		
		base.Awake();
		addFrameScript("SkillA",9 , PauseAnima);
		animaPlayEndScript(animaPlayEnd);
	}
	
	public void PauseAnima(string s)
	{
		this.pauseAnima();
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		partList ["branch_05"]=     branch_05;
		partList ["branch_051"]=   branch_05;
	}
	
	protected void animaPlayEnd (string s)
	{
		vs.isVineShieldFrontAnimaPlayEnd = true;
		vs.destroySelf();
	}
	
	public override void OnDestroy()
	{
		removeFrameScript("SkillA", 9);
		base.OnDestroy();
	}
}
