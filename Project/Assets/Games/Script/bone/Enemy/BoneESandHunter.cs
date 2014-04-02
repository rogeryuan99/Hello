using UnityEngine;
using System.Collections;

public class BoneESandHunter : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject bodyDown;
	public GameObject sword;
	public GameObject Shadow;
	public GameObject legupL;
	public GameObject legupR;
	public GameObject weaponc_FI;
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
		partList["sash"] = sash;
		partList["weapon"] = sword;
		partList["Shadow"]  = Shadow;
		partList["legupL"]  = legupL;
		partList["legupR"]  = legupR;
		partList["weaponc_FI"]  = weaponc_FI;
	}
}