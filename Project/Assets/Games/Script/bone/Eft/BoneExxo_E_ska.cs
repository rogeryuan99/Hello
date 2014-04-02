using UnityEngine;
using System.Collections;

public class BoneExxo_E_ska : EffectAnimation {
	public GameObject EX_E_ska1;	
	public GameObject EX_E_ska2;	
	public GameObject EX_E_ska3;	
	public GameObject EX_E_ska4;	
	public GameObject EX_E_ska5;	
	public GameObject EX_E_ska6;
	public GameObject EX_E_gg4;	
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["EX_E_ska1"] = EX_E_ska1;
		partList["EX_E_ska2"] = EX_E_ska2;
		partList["EX_E_ska3"] = EX_E_ska3;
		partList["EX_E_ska4"] = EX_E_ska4;
		partList["EX_E_ska5"] = EX_E_ska5;
		partList["EX_E_ska6"] = EX_E_ska6;
		
		partList["gg1"] = EX_E_gg4;
		partList["gg2"] = EX_E_gg4;
		partList["gg3"] = EX_E_gg4;
		partList["gg4"] = EX_E_gg4;
		partList["gg5"] = EX_E_gg4;
			
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}

}