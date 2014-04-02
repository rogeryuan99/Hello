using UnityEngine;
using System.Collections;

public class BoneEnemyMedic : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject Shadow;
	public GameObject bodyDown;
	public GameObject sword;
	public GameObject eft;
	public GameObject eft2;
	
	public GameObject ME_clothL;
	public GameObject ME_clothR;
	public GameObject E1; 
	public GameObject E2;
	public GameObject E3;
	public GameObject E4;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;
		
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["bodydown"] = bodyDown; 
		
		partList["Shadow"] = Shadow;
		partList["weapon"] = sword; 
		partList["ef"]  = eft;
		partList["light"]  = eft2;
		 
		partList["ME_clothL"]  = ME_clothL;
		partList["ME_clothR"]  = ME_clothR;  
		
		partList["ef1"]  = E1;
		partList["ef2"]  = E2; 
		partList["ef3"]  = E3;
		partList["ef4"]  = E4; 
	}

}
