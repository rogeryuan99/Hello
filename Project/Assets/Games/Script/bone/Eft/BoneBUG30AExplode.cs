using UnityEngine;
using System.Collections;

public class BoneBUG30AExplode : PieceAnimation
{
	public GameObject D_light_aa01;
	public GameObject E_light_02c;
	public GameObject E_light_03c;
	
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		  partList["D_light_aa01"] = D_light_aa01;
		  partList["D_light_aa01__1"] = D_light_aa01;
		  partList["D_light_aa01__2"] = D_light_aa01;
		  partList["D_light_aa01__3"] = D_light_aa01;
		  partList["E_light_02c"] = E_light_02c;
		  partList["E_light_03c"] = E_light_03c;
	}
	
	public void OnDestroy()
	{
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
