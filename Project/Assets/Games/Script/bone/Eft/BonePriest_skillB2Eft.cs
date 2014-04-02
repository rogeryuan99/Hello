using UnityEngine;
using System.Collections;

public class BonePriest_skillB2Eft : EffectAnimation {
	public GameObject gg;
	public GameObject gq;
	
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["gg5"] = gg;
		partList["gg4"] = gg;
		partList["gg3"] = gg;
		partList["gg2"] = gg;
		partList["gg1"] = gg;
		partList["gq1"] = gq;
		partList["gq2"] = gq;
		partList["gq3"] = gq;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
