using UnityEngine;
using System.Collections;

public class BoneSkillEft_MANTIS15B : PieceAnimation
{
	public GameObject effect_01b;
	public GameObject effect_02a;
	public GameObject effect_03b;
	
	//public GameObject Shadow;		
	public override void Awake (){
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList ["effect_01b"]=   effect_01b;    
		partList ["effect_02a"]=    effect_02a;   
		partList ["effect_02a__1"]=    effect_02a;
		partList ["effect_02a__2"]=    effect_02a;
		partList ["effect_02a__3"]=    effect_02a;
		partList ["effect_02a__4"]=    effect_02a;
		partList ["effect_02a__5"]=    effect_02a;
		partList ["effect_02a__6"]=    effect_02a;
		partList ["effect_02a__7"]=    effect_02a;
		partList ["effect_03b"]=       effect_03b;

	
	}
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
