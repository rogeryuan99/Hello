using UnityEngine;
using System.Collections;

public class BoneEft : EffectAnimation {
	
	public GameObject E1;
	public GameObject E2;
	public GameObject E7;
	public GameObject E8;
	public GameObject E9;
		
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["E1"] = E1;
		partList["E2"] = E2;
		partList["E3"] = E1;
		partList["E4"] = E1;
		partList["E5"] = E1;
		partList["E6"] = E1;
		partList["E7"] = E7;
		partList["E8"] = E8;
		partList["E9"] = E9;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}