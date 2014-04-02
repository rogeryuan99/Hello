using UnityEngine;
using System.Collections;

public class BoneEnemyRemoteEft : EffectAnimation {
	
	public GameObject GZ4;
	public GameObject GZ4d;
	public GameObject GZ4b;
	public GameObject GZ4b2;
		
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["GZ4"] = GZ4;
		partList["GZ4d"] = GZ4d;
		partList["GZ4b"] = GZ4b;
		partList["GZ4b2"] = GZ4b2;
	}
	
	protected void destroySelf (string s){
		pauseAnima();
		transform.position = new Vector3(1000,1000,1000);
//		Destroy(this.gameObject);
	}

}
