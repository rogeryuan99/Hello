using UnityEngine;
using System.Collections;

public class BoneEnemyMindbender : PieceAnimation {
	public GameObject armUpR;
	public GameObject bodyUp1;
	public GameObject armDownR;
	public GameObject bodyUp2;
	public GameObject armDownL;
	public GameObject Shadow;
	public GameObject bodyDown;
	public GameObject at1;
	public GameObject at2;
	public GameObject legUpL;
	public GameObject legUpR;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["bodyUp"] = body; 
		partList["legDownL"] = legL;
		partList["legDownR"] = legR; 
		
		partList["bodyUp1"] = bodyUp1;
		partList["bodyUp2"] = bodyUp2; 
		partList["armDownR"] = armDownR;
		partList["armUpR"] = armUpR;
		
		partList["armDownL"] = armDownL;
		partList["bodyDown"] = bodyDown;
		partList["Shadow"] = Shadow;
		partList["at2"] = at2;
		partList["at1"]  = at1;	
		partList["legupL"]  = legUpL;
		partList["legupR"]  = legUpR;
	}
}
