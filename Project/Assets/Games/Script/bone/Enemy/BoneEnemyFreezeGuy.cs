using UnityEngine;
using System.Collections;

public class BoneEnemyFreezeGuy : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodyDown;
	public GameObject sword;
	public GameObject eft;
	public GameObject sash;
	
	public GameObject Shadow;
	public GameObject dp;
	public GameObject dp2;
	public GameObject dp3;

	public GameObject g10;
	public GameObject g1;
	
	public GameObject g2;
	public GameObject g3;
	
	public GameObject g4;
	public GameObject g5;
	
	public GameObject g6;
	public GameObject g7;
	public GameObject g8;
	public GameObject gj;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;
	
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["bodyDown"] = bodyDown;
		partList["weapon"] = sword;
		partList["gy"]  = eft;
		partList["sash"]  = sash;
		
		partList["Shadow"]  = Shadow;
		partList["dp"]  = dp;
		partList["dp2"]  = dp2;
		partList["dp3"]  = dp3;
		partList["g10"]  = g10;
		partList["g1"]  = g1;
		partList["g2"]  = g2;
		partList["g3"]  = g3;
		partList["g4"]  = g4;
		partList["g5"]  = g5;
		partList["g6"]  = g6;
		partList["g7"]  = g7;
		partList["g8"]  = g8; 
		partList["gj"]  = gj;
	}
}