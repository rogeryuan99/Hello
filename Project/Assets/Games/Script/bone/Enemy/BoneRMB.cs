using UnityEngine;
using System.Collections;

public class BoneRMB : PieceAnimation {
	public GameObject armDownL;
	public GameObject armDownR;
	public GameObject armUpL;
	public GameObject armUpR;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject Shadow;
	public GameObject sash;
	public GameObject weapon;
	public GameObject weapon_fi;

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
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["bodyUp"] = body;
		partList["head"] = head;
		partList["legupL"] = legUpL;
		partList["legupR"] = legUpR;
		partList["Shadow"] = Shadow;
		partList["sash"] = sash;
		partList["weapon"] = weapon;
		partList["weaponC_F"] = weapon_fi;

	}
}
