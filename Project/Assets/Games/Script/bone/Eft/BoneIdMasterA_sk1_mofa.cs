using UnityEngine;
using System.Collections;

public class BoneIdMasterA_sk1_mofa : EffectAnimation {
	public GameObject mofa;	
	//public GameObject Shadow;		
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();

		partList["mofa"] = mofa; 
		
		//partList["Shadow"] = Shadow;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
