using UnityEngine;
using System.Collections;

public class BoneCheapShot_skill_b : EffectAnimation {
	public GameObject E1;
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["d_light1"] = E1;
		partList["d_light2"] = E1;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}