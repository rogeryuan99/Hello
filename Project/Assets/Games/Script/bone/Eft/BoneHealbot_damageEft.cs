using UnityEngine;
using System.Collections;

public class BoneHealbot_damageEft : EffectAnimation {
	public GameObject E1;
	public GameObject E2;
	public GameObject E3;
	public GameObject E4;
	
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["dam1"] = E1;
		partList["dam2"] = E2;
		partList["dam3"] = E3;
		partList["dam4"] = E4;
		partList["dam5"] = E4;
		partList["dam6"] = E4;
		partList["dam7"] = E4;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}

}
