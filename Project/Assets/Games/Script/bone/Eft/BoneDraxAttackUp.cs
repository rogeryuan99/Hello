using UnityEngine;
using System.Collections;

public class BoneDraxAttackUp : PieceAnimation {

	public GameObject   MEDIUM_Punch_FX_02;
	
	public override void Awake ()
	{ 		
		base.Awake();
		addFrameScript("SkillA", 2, show);
		animaPlayEndScript(destroySelf);
	}
	
	protected void show (string s)
	{
		gameObject.transform.localScale = Vector3.one;
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		partList["MEDIUM_Punch_FX_02"] = MEDIUM_Punch_FX_02;  
	}
	
	public void OnDestroy()
	{
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
