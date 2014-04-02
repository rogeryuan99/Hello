using UnityEngine;
using System.Collections;

public class BoneIdMasterA : CharacterAnima {
	public GameObject armDownL;	
	public GameObject armUpL;	
	public GameObject armUpR;	
	public GameObject bodyDown;	
	public GameObject Shadow;	
	public GameObject weapon;	
	
	public GameObject weapon_eft;	
	public GameObject weapon_eft2;	
	public GameObject head2;	
		
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armDownL"] = armDownL;
		partList["armUpL"] = armUpL;
		partList["armUpR"] = armUpR;
		partList["bodyDown"] = bodyDown;
		partList["bodyUp"] = body;
		partList["head"] = head; 
		partList["legL"] = legL; 
		partList["legR"] = legR; 
		partList["Shadow"] = Shadow;
		partList["weapon"] = weapon; 
		partList["head2"] = head2;
		
		partList["light"] = weapon_eft;
		partList["light2"] = weapon_eft2;
	}
}