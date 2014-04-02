using UnityEngine;
using System.Collections;

public class BonePriest_skillAEft : EffectAnimation {
	public GameObject E1;
	public GameObject E2;
	
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["PR_E_SKA1"] = E1;
		partList["PR_E_SKA2"] = E2;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
