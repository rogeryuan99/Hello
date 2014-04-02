using UnityEngine;
using System.Collections;

public class BoneEnemyCheapShot : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodyUp; 
	public GameObject bodyUp2;
	public GameObject bodyDown;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["body"]  = body;
		partList["HandL"] = handL;
		partList["HandR"] = handR;
		
		partList["armUPR"] = armUpR;
		partList["armdownR"] = armDownR;
		partList["armUPL"] = armUpL;
		partList["armdownL"] = armDownL;
		partList["bodyDown"] = bodyDown;
		partList["bodyUP"] = bodyUp;
		partList["bodyUP2"] = bodyUp2;
		
	}

}