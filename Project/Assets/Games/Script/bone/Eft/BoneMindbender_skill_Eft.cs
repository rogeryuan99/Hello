using UnityEngine;
using System.Collections;

public class BoneMindbender_skill_Eft : EffectAnimation {
	public GameObject E1;
	public GameObject E2;
	public GameObject E4;
	public GameObject E7;
	public GameObject J1;
    public GameObject J2;
    public GameObject J3;
    public GameObject J4;
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["GZ2"] = E2;
		partList["GZ3"] = E1;
		partList["GZ4"] = E4;
		partList["GZ7"] = E7;
		partList["jg"] = J1;
		partList["jg2"] = J2;
		partList["jg3"] = J3;
		partList["jg4"] = J4;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
