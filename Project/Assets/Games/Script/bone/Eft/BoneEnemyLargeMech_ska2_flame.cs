using UnityEngine;
using System.Collections;

public class BoneEnemyLargeMech_ska2_flame : EffectAnimation {
	public GameObject flame1;	
	public GameObject flame2;	
	public GameObject flame3;	
	public GameObject flame4;	
	public GameObject flame5;	
	public GameObject flame6;	
	public GameObject flame7;	
	public GameObject flame8;	
	public GameObject flame9;	
	public GameObject flame10;	
	public GameObject flame11;	
	public GameObject flame12;	
	public GameObject flame13;	
	public GameObject flame14;	
	public GameObject flame15;	
	public GameObject flame17;	
	public GameObject flame18;	
	public GameObject flame19;		
	public override void Awake (){
		base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["flame1"] = flame1;
		partList["flame2"] = flame2;
		partList["flame3"] = flame3;
		partList["flame4"] = flame4;
		partList["flame5"] = flame5;
		partList["flame6"] = flame6;
		partList["flame7"] = flame7;
		partList["flame8"] = flame8;
		partList["flame9"] = flame9;
		partList["flame10"] = flame10;
		partList["flame11"] = flame11;
		partList["flame12"] = flame12;
		partList["flame13"] = flame13;
		partList["flame14"] = flame14;
		partList["flame15"] = flame15;
		partList["flame17"] = flame17;
		partList["flame18"] = flame18;
		partList["flame19"] = flame19;
			
	}
}