using UnityEngine;
using System.Collections;

public class BoneFlyingDrone : PieceAnimation {
	public GameObject armR;
	public GameObject armL;
	public GameObject Shadow;

	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["body"] = body;
		partList["armR"] = armR;
		partList["armL"] = armL;
		partList["Shadow"] = Shadow;

	}
}