using UnityEngine;
using System.Collections;

public class BoneCowboy_Beam : EffectAnimation {
	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	
	//public GameObject Shadow;		
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["SKA1_E"] = light1; 
		partList["SKA2_E"] = light2; 
		partList["SKA3_E"] = light3; 
	
		
		//partList["Shadow"] = Shadow;
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}