using UnityEngine;
using System.Collections;

public class BoneDraxTauntBottom : PieceAnimation {
	
	public GameObject   B_DRAX_Back_cf2;
	public GameObject   B_DRAX_Back_cf1	;
	public GameObject   B_DRAX_Back_cf	;
	
	public override void Awake (){ 		
		base.Awake();
	}
	
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["B_DRAX_Back_cf2"] = B_DRAX_Back_cf2;  
		partList["B_DRAX_Back_cf1"] = B_DRAX_Back_cf1	;   
		partList["B_DRAX_Back_cf"] = B_DRAX_Back_cf	;
	}
	
	public void OnDestroy()
	{
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
