using UnityEngine;
using System.Collections;

public class BoneHealbot_deadEft : EffectAnimation {
	public GameObject E2;
	public GameObject E3;
	public GameObject E4;
	public GameObject E5;
	public GameObject E7;
	public GameObject E8;
	public GameObject E9;
	public GameObject E10;
	public GameObject E11;
	public GameObject E12;
	public GameObject E13;
	public GameObject E14;
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["flash2"] = E2;
		partList["flash3"] = E3;
		partList["flash4"] = E4;
		partList["flash5"] = E5;
		partList["flash7"] = E7;
		partList["flash8"] = E8;
		partList["flash9"] = E9;
		partList["flash10"] = E10;
		partList["flash11"] = E11;
		partList["flash12"] = E12;
		partList["flash13"] = E13;
		partList["flash14"] = E14;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}

}