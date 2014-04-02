using UnityEngine;
using System.Collections;

public class BoneEGB_ska1_ef_b : EffectAnimation {
	public GameObject light1;	
	public GameObject light2;	
	public GameObject light3;	
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);

//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["light1"] = light1;
		partList["light2"] = light2;
		partList["light3"] = light3;
			
	}
	protected void destroySelf (string s){
		//Destroy(this.gameObject);
		this.pauseAnima();
	}
}