using UnityEngine;
using System.Collections;

public class BoneCowBoy_skillA : EffectAnimation {
	public GameObject E1;
	public GameObject E2;
	public GameObject E3;
	public GameObject E4;
	public GameObject E5;
	public GameObject E6;
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["qiangsu4"] = E6;
		partList["qiangsu3"] = E6;
		partList["qiangsu2"] = E6;
		partList["qiangsu"] = E6;
		partList["bgg5"] = E5;
		partList["bgg4"] = E5;
		partList["bgg3"] = E5;
		partList["bgg2"] = E5;
		partList["bgg1"] = E5;
		partList["SKA3_E"] = E4;
		partList["sj2"] = E3;
		partList["SKA2_E"] = E1;
		partList["skib3"] = E2;
		partList["skib2"] = E2;
		partList["skib1"] = E2;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}