using UnityEngine;
using System.Collections;

public class BoneMarine_skillB_foot : EffectAnimation {
	public GameObject E2;
	public GameObject E3;
	public GameObject E6;
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["light_d"] = E2;
		partList["light4"] = E3;		
		partList["light5"] = E3;
		partList["light6"] = E3;
		partList["xfd1"] = E6;
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}