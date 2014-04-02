using UnityEngine;
using System.Collections;

public class BoneEGB_dead_ef : EffectAnimation {
	public GameObject flash1;	
	public GameObject flash2;	
	public GameObject flash3;	
	public GameObject flash4;	
	public GameObject flash5;	
	public GameObject flash6;	
	public GameObject flash7;	
	public GameObject flash8;	
	public GameObject flash9;	
	public GameObject flash10;	
	public GameObject flash11;	
	public GameObject flash12;	
	public GameObject flash13;	
	public GameObject flash14;		
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["flash1"] = flash1;
		partList["flash2"] = flash2;
		partList["flash3"] = flash3;
		partList["flash4"] = flash4;
		partList["flash5"] = flash5;
		partList["flash6"] = flash6;
		partList["flash7"] = flash7;
		partList["flash8"] = flash8;
		partList["flash9"] = flash9;
		partList["flash10"] = flash10;
		partList["flash11"] = flash11;
		partList["flash12"] = flash12;
		partList["flash13"] = flash13;
		partList["flash14"] = flash14;
	
			
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}