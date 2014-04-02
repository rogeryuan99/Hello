using UnityEngine;
using System.Collections;

public class BonePetEft : EffectAnimation {
	
	public GameObject guangbo;
	public GameObject guangbo2;
	public GameObject guangying;
	public GameObject head3;
		
	public override void Awake (){ 		
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["guangbo"] = guangbo;
		partList["guangbo2"] = guangbo2;
		partList["head3"] = head3;
		partList["guangying"] = guangying;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
