using UnityEngine;
using System.Collections;

public class BoneDraxTauntHalo : PieceAnimation {

	public GameObject   B_DRAX_Back_ying2;
	public GameObject   B_DRAX_Back_ying1;
	public GameObject   B_DRAX_Back_ying;
	
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["B_DRAX_Back_ying2"] = B_DRAX_Back_ying2;  
		partList["B_DRAX_Back_ying1"] = B_DRAX_Back_ying1	;   
		partList["B_DRAX_Back_ying"] = B_DRAX_Back_ying	;
	}
	
	public void OnDestroy()
	{
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
