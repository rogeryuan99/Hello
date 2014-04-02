using UnityEngine;
using System.Collections;

public class BoneSkillGamora1EnemyDamageEft : PieceAnimation {
	
	public GameObject effect_50_1;
	public GameObject effect_51_1;
	
	//public GameObject Shadow;		
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["effect_50_1"] = effect_50_1; 
		partList["effect_51_1"] = effect_51_1;
	
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
