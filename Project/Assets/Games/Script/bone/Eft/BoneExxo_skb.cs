using UnityEngine;
using System.Collections;

public class BoneExxo_skb : EffectAnimation {
	public GameObject gg1;
	public GameObject gqi;
	public GameObject sj2;
	public GameObject skia;
	public GameObject skib2;
		
	public override void Awake (){ 		
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["skia"] = skia;
		partList["gg1"] = gg1;
		partList["gg2"] = gg1;
		partList["gg3"] = gg1;
		partList["gg4"] = gg1;
		partList["gg5"] = gg1;
		
		partList["gq1"] = gqi;
		partList["gq2"] = gqi;
		partList["gq3"] = gqi;
		partList["skib3"] = skib2;
		partList["skib2"] = skib2;
		partList["skib1"] = skib2;
		partList["sj2"] = sj2;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
