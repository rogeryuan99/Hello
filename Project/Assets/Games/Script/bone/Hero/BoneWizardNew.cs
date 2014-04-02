using UnityEngine;
using System.Collections;

public class BoneWizardNew : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodyDown;
	public GameObject Shadow;
	public GameObject bodyUpadd;
	public GameObject head2;
	public GameObject eft;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["bodyDown"] = bodyDown;
		partList["Shadow"] = Shadow;
		partList["bodyUpadd"]  = bodyUpadd;
		partList["head2"]  = head2;
		partList["gq"]  = eft;
	}
}
