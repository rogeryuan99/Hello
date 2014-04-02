using UnityEngine;
using System.Collections;

public class BoneEnemyCanonRider : PieceAnimation {
	public GameObject armUpR;
	public GameObject armDownR;
	public GameObject legDOWNR;
	public GameObject legUpR;
	public GameObject SRarmL;
	public GameObject bodyUp;
	
	public GameObject bodyDown;
	public GameObject bodyDown2;
	public GameObject atkEft;
	
	public GameObject CRlegUPR;
	public GameObject CRlegDOWNR;
	
	public GameObject smoke1;
	public GameObject smoke2;
	public GameObject smoke3;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["CRhead"] = head;
		partList["CRbody"] = body;
		partList["legL"] = legL;
		
		partList["bodyUp"] = bodyUp;
		partList["bodyDown"] = bodyDown;
		partList["bodyDown2"] = bodyDown2;
		
		partList["SRarmUpR"] = armUpR;
		partList["SRarmDOWNR"] = armDownR;
		partList["SRarmL"] = SRarmL;
		partList["legDOWNR"]  = legDOWNR;
		partList["legUPR"]  = legUpR;
		partList["CR_E_ATTACK"]  = atkEft; 
		
		partList["CRlegUPR"]  = CRlegUPR;
		partList["CRlegDOWNR"]  = CRlegDOWNR;
		
		partList["smoke1"]  = smoke1;
		partList["smoke2"]  = smoke2;
		partList["smoke3"]  = smoke3;
	}

}
