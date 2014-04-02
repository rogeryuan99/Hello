using UnityEngine;
using System.Collections;

public class BoneEnemyDoppelgangers_skill : EffectAnimation {
	public GameObject E5;
	public GameObject GZ2;
	public GameObject GZ3;
	public GameObject GZ7;
		
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["E5"] = E5;
		partList["GZ2"] = GZ2;
		partList["GZ3"] = GZ3;
		partList["GZ7"] = GZ7;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
