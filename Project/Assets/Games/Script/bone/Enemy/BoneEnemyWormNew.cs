using UnityEngine;
using System.Collections;

public class BoneEnemyWormNew : PieceAnimation {
	public GameObject armDOWNR;
	public GameObject armUpR;
	public GameObject armL;
	public GameObject bodyDown;
	public GameObject headDOWN;
	public GameObject legDownR;
	public GameObject legUPR;
	public GameObject Shadow;
	public GameObject eft;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){	partList = new Hashtable();
		partList["armDOWNR"] = armDOWNR;
		partList["armL"] = armL;
		partList["armUpR"] = armUpR;
		partList["headUP"] = head;
		partList["bodyUp"] = body;
		partList["bodyDown"] = bodyDown;
		partList["headDOWN"] = headDOWN;
		partList["legDOWNR"] = legDownR;
		partList["legUPR"]  = legUPR;
		partList["legL"] = legL;
		partList["Shadow"]  = Shadow;
		partList["savxcv"]  = eft;
	}
}