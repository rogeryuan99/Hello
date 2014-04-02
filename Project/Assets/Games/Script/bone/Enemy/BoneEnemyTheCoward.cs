using UnityEngine;
using System.Collections;

public class BoneEnemyTheCoward : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject Shadow;
	public GameObject sword; 
	public GameObject legDownL;
	public GameObject legDownR;
	public GameObject legUpL;
	public GameObject legUpR;
	
	public GameObject atkEft;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;  
		
		partList["legDownL"] = legDownL;
		partList["legDownR"] = legDownR; 
		partList["armUpR"] = armUpR;
		partList["armDownr"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDowNL"] = armDownL;
		partList["Shadow"] = Shadow;
		partList["weapon"] = sword; 	
		partList["legUpL"]  = legUpL;
		partList["legUpR"]  = legUpR;
		
		partList["hh"]  = atkEft;
	}

  
}
