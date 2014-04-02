using UnityEngine;
using System.Collections;

public class BoneIdMasterB : CharacterAnima {
	public GameObject armDownL;	
	public GameObject armDownR;	
	public GameObject armUpL;	
	public GameObject armUpR;	
	public GameObject bodyDown;	
	public GameObject Shadow;	
	public GameObject Fire;	
	public GameObject E_att1;
		
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
		partList["bodydown"] = bodyDown;
		partList["bodyUp"] = body;
		partList["head"] = head; 
		partList["legL"] = legL; 
		partList["legR"] = legR; 
		partList["Shadow"] = Shadow;
		partList["Fire"] = Fire; 	
		partList["E_att1"] = E_att1; 	
	}

}
