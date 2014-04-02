using UnityEngine;
using System.Collections;

public class BoneEnemyTontonRider : PieceAnimation {
	public GameObject armUpR;
	public GameObject armL;
	public GameObject armDownR;
	public GameObject legUpL;
	public GameObject legUpR;
	
	public GameObject TSarmUpR;
	public GameObject TSarmL;
	public GameObject TSbodyDown;
	public GameObject TSbodyUp; 
	public GameObject TSheadUp;
	public GameObject TSlegDownR;
	public GameObject TSlegL;
	public GameObject TSlegUpL;
	public GameObject TSlegUpR;
	 
	public GameObject sword;
	public GameObject atkEft1;
	public GameObject atkEft2;
	public GameObject atkEft3;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable(); 
		
		partList["SRhead"] = head;
		partList["SRbodyUP"] = body;
		
		partList["SRarmUpR"] = armUpR;
		partList["SRarmDOWNR"] = armDownR;
		partList["SRarmL"] = armL;
		partList["SRlegUPL"]  = legUpL;
		partList["SRlegUPR"]  = legUpR;
		
		partList["headUP"] = TSheadUp;
		partList["bodyUp"] = TSbodyUp; 
		partList["bodyDown"] = TSbodyDown; 
		partList["armL"] = TSarmL; 
		partList["armUpR"] = TSarmUpR;
		partList["legDownR"] = TSlegDownR;
		partList["legL"]  = TSlegL;
		partList["legLUPL"]  = TSlegUpL;
		partList["legLUPR"]  = TSlegUpR;
		
		partList["weapon"] = sword;	
		partList["eft1"] = atkEft1;
		partList["eft2"]  = atkEft2;
		partList["eft3"]  = atkEft3;
	}
}