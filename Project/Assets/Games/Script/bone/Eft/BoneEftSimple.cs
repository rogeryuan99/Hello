using UnityEngine;
using System.Collections;

public class BoneEftSimple : EffectAnimation {
	public GameObject g2;
	public GameObject g6;
	public GameObject sj;
	public GameObject skibqqq;
	public GameObject x2;
	public GameObject xx;
	
	
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["skib3"] = skibqqq;
		partList["skib2"] = skibqqq;
		partList["skib1"] = skibqqq;
		partList["sj"] = sj;
		partList["x2"] = x2;
		partList["g5"] = g2;
		partList["g6"] = g2;
		partList["g7"] = g2;
		partList["g8"] = g6;
		partList["g3"] = xx;
		partList["g4"] = xx;
		partList["g9"] = xx;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}