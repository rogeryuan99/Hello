using UnityEngine;
using System.Collections;

public class BoneTrainer_new : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodydown;
	public GameObject shadow;
	public GameObject weapon;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject hair;
	public GameObject gq;
	
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
		partList["Shadow"] = shadow;
		partList["bodydown"] = bodydown;
		partList["weapon"] = weapon;
		partList["legUpL"]  = legUpL;
		partList["legUpR"]  = legUpR;
		partList["hair"]  = hair;
		partList["gq"]  = gq;
	}
}
