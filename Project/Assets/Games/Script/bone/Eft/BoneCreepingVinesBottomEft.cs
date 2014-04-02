using UnityEngine;
using System.Collections;

public class BoneCreepingVinesBottomEft : PieceAnimation {
	
	public GameObject light_3C;
	
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["light_3C"] = light_3C;
 

	}
	
	public void OnDestroy()
	{
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
