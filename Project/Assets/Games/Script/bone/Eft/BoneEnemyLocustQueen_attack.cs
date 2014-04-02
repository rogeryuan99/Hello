using UnityEngine;
using System.Collections;

public class BoneEnemyLocustQueen_attack : EffectAnimation {
	public GameObject E_T_att1;	
	//public GameObject Shadow;		
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();

		partList["E_T_att1"] = E_T_att1; 
		
		//partList["Shadow"] = Shadow;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
	
}