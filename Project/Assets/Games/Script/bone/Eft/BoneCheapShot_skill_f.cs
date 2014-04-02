using UnityEngine;
using System.Collections;

public class BoneCheapShot_skill_f : EffectAnimation {
	public GameObject E1;
	public GameObject E3;
	
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["guang1"] = E1;
		partList["guang3"] = E3;
		partList["guang4"] = E3;
		partList["guang5"] = E3;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}