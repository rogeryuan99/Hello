using UnityEngine;
using System.Collections;

public class BoneSM_SkillAEft : EffectAnimation {
	public GameObject sj;
	public GameObject sj2;
	public GameObject skia;
	public GameObject skib2;
	public GameObject skibqqq;
	public GameObject xf2;
		
	public override void Awake (){ 		
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["xf5"] = xf2;
		partList["xf4"] = xf2;
		partList["xf3"] = xf2;
		partList["xf1q"] = xf2;
		partList["xf"] = xf2;
		partList["skia"] = skia;
		
		partList["skib23"] = skibqqq;
		partList["skib22"] = skibqqq;
		partList["skib21"] = skibqqq;
		partList["sj"] = sj;
		partList["skib3"] = skib2;
		partList["skib2"] = skib2;
		partList["skib1"] = skib2;	
		partList["sj2"] = sj2;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
