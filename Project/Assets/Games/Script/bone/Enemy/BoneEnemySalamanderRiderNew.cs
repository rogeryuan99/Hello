using UnityEngine;
using System.Collections;

public class BoneEnemySalamanderRiderNew : PieceAnimation {
	public GameObject armUpR;
	public GameObject armDownR;
	public GameObject armL;
	public GameObject bodyDown;
	public GameObject headDOWN;
	public GameObject legDOWNR;
	public GameObject legUPR;
	
	public GameObject SRarmUpR;
	public GameObject SRarmDownR;
	public GameObject SRarmL;
	public GameObject SRbodyDOWN;
	public GameObject SRbody;
	public GameObject SRhead;
	public GameObject SRlegDOWNR;
	public GameObject SRlegUPR;
	public GameObject ef;
	public GameObject ef3; 
	public GameObject ef4; 
	public GameObject ef5; 
	public GameObject ef6; 
	
	public GameObject Shadow;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["headUP"] = head;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
	
		partList["armDOWNR"] = armDownR;
		partList["armL"] = armL;
		partList["armUpR"] = armUpR;
		partList["bodyDown"] = bodyDown;
		partList["headDOWN"] = headDOWN;
		partList["legDOWNR"] = legDOWNR;
		partList["legUPR"]  = legUPR;
		
		partList["SRarmDOWNR"] = SRarmDownR;
		partList["SRarmL"] = SRarmL;
		partList["SRarmUpR"] = SRarmUpR;
		partList["SRbodyDOWN"] = SRbodyDOWN;
		partList["SRbodyUP"] = SRbody;
		partList["SRhead"] = SRhead;
		partList["SRlegDOWNR"] = SRlegDOWNR;
		partList["SRlegUPR"]  = SRlegUPR;
		
		partList["Shadow"]  = Shadow; 
		partList["ef"]  = ef;
		partList["ef3"]  = ef3;
		partList["ef4"]  = ef4;
		partList["ef5"]  = ef5;
		partList["ef6"]  = ef6;
	}

}
