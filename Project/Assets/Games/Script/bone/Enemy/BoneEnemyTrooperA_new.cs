using UnityEngine;
using System.Collections;

public class BoneEnemyTrooperA_new : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject bodyDown;
	public GameObject weapon;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject Shadow;
	public GameObject eft;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["Shadow"] = Shadow;
		partList["armDownL"] = armDownL;
		partList["armUpL"] = armUpL;
		partList["weapon"] = weapon;
		partList["legupL"]  = legUpL;
		partList["legL"] = legL;
		partList["legupR"]  = legUpR;
		partList["bodyUp"] = body;
		partList["sash"] = sash;
		partList["legR"] = legR;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["weaponC_F"] = eft;
	}
}
