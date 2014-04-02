using UnityEngine;
using System.Collections;

public class BoneEmpireGeneralB : PieceAnimation {
	public GameObject armDownL;	
	public GameObject armDownR;	
	public GameObject armUpL;	
	public GameObject armUpR;	
	public GameObject bodyDown;	
	public GameObject Shadow;	
	public GameObject weapon_eft;			
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armDownL"] = armDownL;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armUpR"] = armUpR;
		partList["bodyDown"] = bodyDown;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["shadow"] = Shadow;
		partList["weapon_eft"] = weapon_eft;		
	}

}
