using UnityEngine;
using System.Collections;

public class BoneEGB_ska1_ef_f_GZ : EffectAnimation {
	public GameObject GZ;	
	public GameObject GZ2;	
	public GameObject GZ3;	
	public GameObject E5;	
	public GameObject GZ7;	
	public GameObject GZ8;	
	public GameObject GZ9;	
	public GameObject GZ10;	
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["GZ"] = GZ;
		partList["GZ2"] = GZ2;
		partList["GZ3"] = GZ3;
		partList["E5"] = E5;
		partList["GZ7"] = GZ7;
		partList["GZ8"] = GZ8;
		partList["GZ9"] = GZ9;
		partList["GZ10"] = GZ10;
			
	}
		protected void destroySelf (string s){
		Destroy(this.gameObject);
	}

}
