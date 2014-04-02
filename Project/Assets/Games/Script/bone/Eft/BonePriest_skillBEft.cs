using UnityEngine;
using System.Collections;

public class BonePriest_skillBEft : EffectAnimation {
	public GameObject E1;
	public GameObject E2;
	public GameObject E3;
	public GameObject E4;
	
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["E1"] = E1;
		partList["E2"] = E2;
		partList["E3"] = E3;
		partList["E4"] = E4;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
