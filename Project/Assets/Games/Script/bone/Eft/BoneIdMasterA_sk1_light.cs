using UnityEngine;
using System.Collections;

public class BoneIdMasterA_sk1_light : EffectAnimation 
{
	public GameObject light2;
	public GameObject light3;
	public GameObject light4;
	public GameObject light5;
	public GameObject light6;	
	//public GameObject Shadow;		
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["light2"] = light2; 
		partList["light3"] = light3; 
		partList["light4"] = light4; 
		partList["light5"] = light5; 
		partList["light6"] = light6; 
		
		
		//partList["Shadow"] = Shadow;
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
	
}