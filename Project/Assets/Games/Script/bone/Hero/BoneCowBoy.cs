using UnityEngine;
using System.Collections;

public class BoneCowBoy : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject ass;
	public GameObject sword;
	public GameObject collar;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject bodyUp2;
	public GameObject bodyDown;
	public GameObject eft;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyAdd"] = body;
		partList["bodyUp"] = bodyUp2;
		partList["bodyDown"] = bodyDown;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["Ass"] = ass;
		partList["sash"] = sash;
		partList["gun"] = sword;
		partList["Collar"]  = collar;
		partList["legUpL"]  = legUpL;
		partList["legUpR"]  = legUpR;
		partList["gune1"]  = eft;
	}
}
