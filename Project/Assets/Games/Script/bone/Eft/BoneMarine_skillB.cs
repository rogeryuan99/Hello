using UnityEngine;
using System.Collections;

public class BoneMarine_skillB : EffectAnimation {
	public GameObject E1;
	public GameObject E4;
	public GameObject E5;
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["ggg"] = E1;
		partList["xf2"] = E4;		
		partList["xf5"] = E5;
		partList["xf4"] = E5;
		partList["xf3"] = E5;
		partList["xf1q"] = E5;
		partList["xf"] = E5;
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
