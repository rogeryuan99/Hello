using UnityEngine;
using System.Collections;

public class BoneExxoNew : CharacterAnima {
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject bodyDown;
	public GameObject head2;
	public GameObject head3;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject legDownR;
	public GameObject legDownL;
	public GameObject Shadow;
	public GameObject weapon;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armDownR"] = armDownR;
		partList["armDownL"] = armDownL;
		partList["armUpR"] = armUpR;
		partList["armUpL"] = armUpL;
		
		partList["bodydown"] = bodyDown;
		partList["bodyup"] = body;
		partList["head"] = head;
		partList["head2"] = head2;
		partList["head3"] = head3;
		partList["legdownR"] = legDownR;
		partList["legdownL"] = legDownL;
		partList["legupL"] = legUpL;
		partList["legupR"] = legUpR;
		partList["Shadow"] = Shadow;
		
		partList["weapon"] = weapon;
	}

}
