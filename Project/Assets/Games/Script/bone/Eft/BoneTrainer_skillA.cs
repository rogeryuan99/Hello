using UnityEngine;
using System.Collections;

public class BoneTrainer_skillA : EffectAnimation {
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
		partList["xuanzhuan21"] = E2;
		partList["xuanzhuan22"] = E2;
		partList["xuanzhuan23"] = E2;
		partList["xuanzhuan24"] = E2;
		partList["xuanzhuan25"] = E2;
		partList["xuanzhuan26"] = E2;
		partList["xuanzhuan27"] = E2;
		partList["qiangshanguang4"] = E3;
		partList["qiangshanguang3"] = E3;
		partList["qiangshanguang2"] = E3;
		partList["qiangshanguang1"] = E3;
		partList["qiangguangdian2"] = E1;
		partList["qiangbaoguang1"] = E4;
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
