using UnityEngine;
using System.Collections;

public class BoneHealing : EffectAnimation {
	public GameObject g2;
	public GameObject g6;
	public GameObject xx;
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
	}
	protected override void initPartData (){
		partList = new Hashtable();
		
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
