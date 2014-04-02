using UnityEngine;
using System.Collections;

public class BoneWizard_sk2 : EffectAnimation {

	public GameObject huo;
	public GameObject miao;
	public GameObject qiangbaoguang;
	public GameObject qiangshangguang;
	
	public override void Awake (){ 		
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["qiangshanguang4"] = qiangshangguang;
		partList["qiangshanguang3"] = qiangshangguang;
		partList["qiangshanguang2"] = qiangshangguang;
		partList["qiangshanguang"] = qiangshangguang;
		partList["m3"] = miao;
		partList["m2"] = miao;
		partList["m1"] = miao;
		partList["huo"] = huo;
		partList["qiangbaoguang"] = qiangbaoguang;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
