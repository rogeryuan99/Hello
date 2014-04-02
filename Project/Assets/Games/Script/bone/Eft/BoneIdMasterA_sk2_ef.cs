using UnityEngine;
using System.Collections;

public class BoneIdMasterA_sk2_ef : EffectAnimation {
	public GameObject gq;
	
	public GameObject gg1;
	public GameObject gq1;
	public GameObject sj;
	public GameObject sj2;
	public GameObject skib2;
	public GameObject skibqqq;
	//public GameObject Shadow;		
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	public void playShortAnima (){
		addFrameScript("Skill",20, destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		
		partList["gq"] = gq; 
		
		partList["gg1"] = gg1; 
		partList["gg2"] = gg1; 
		partList["gg3"] = gg1; 
		partList["gg4"] = gg1; 
		partList["gg5"] = gg1; 
		partList["gq1"] = gq1; 
		partList["gq2"] = gq1; 
		partList["gq3"] = gq1; 
		partList["skib3"] = skib2; 
		partList["skib2"] = skib2; 
		partList["skib1"] = skib2; 
		partList["sj2"] = sj2;
		partList["skib23"] = skibqqq; 
		partList["skib22"] = skibqqq; 
		partList["skib21"] = skibqqq; 
		partList["sj"] = sj; 
		
		//partList["Shadow"] = Shadow;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}