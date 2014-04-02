using UnityEngine;
using System.Collections;

public class BoneReliveEft : EffectAnimation {
	
	public GameObject SKA2_E;
	public GameObject GZ4;
	public GameObject SKA1_E;
		
	public override void Awake (){ 		
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["SKA2_E"] = SKA2_E;
		partList["GZ4"] = GZ4;
		partList["SKA1_E"] = SKA1_E;
	}

	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}