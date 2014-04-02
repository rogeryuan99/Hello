using UnityEngine;
using System.Collections;

public class BoneBUG15BHalo : PieceAnimation
{
	public GameObject D_light_01;
	
	public override void Awake ()
	{ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["D_light_01"] = D_light_01;
		partList["D_light_01__1"] = D_light_01;
		partList["D_light_01__2"] = D_light_01;
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
