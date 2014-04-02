using UnityEngine;
using System.Collections;

public class BoneEnemyTrooperB : PieceAnimation {
	public GameObject shadow;
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject weapon;
	public GameObject weaponEft;
	public GameObject legUpL;
	public GameObject legUpR;
	
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
		partList["Shadow"] = shadow;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["sash"] = sash;
		partList["weapon"] = weapon;
		partList["weaponC_F"]  = weaponEft;
		partList["legupL"]  = legUpL;
		partList["legupR"]  = legUpR;
	}
}
