using UnityEngine;
using System.Collections;

public class BoneEnemyFlreSalamanderNew : PieceAnimation {
	public GameObject armUpR;
	public GameObject armDownR;
	public GameObject headDOWN;
	public GameObject legDOWNR;
	public GameObject Shadow;
	public GameObject legUPR;
	public GameObject armL;
	public GameObject bodyDown;
	public GameObject savxcv;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["headUP"] = head;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["headDOWN"] = headDOWN;
		partList["armDOWNR"] = armDownR;
		partList["armL"] = armL;
		partList["armUpR"] = armUpR;
		partList["legDOWNR"] = legDOWNR;
		partList["Shadow"]  = Shadow;
		partList["legUPR"]  = legUPR;
		partList["bodyDown"] = bodyDown;
		partList["savxcv"] = savxcv;
	}
}
