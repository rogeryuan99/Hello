using UnityEngine;
using System.Collections;

public class BoneEnemySnowWizard : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject sword;
	public GameObject Shadow;
	public GameObject bodyDown;
	public GameObject ef;

	public GameObject ef1;
	public GameObject ef2;
	public GameObject ef3;
	public GameObject ef4;
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
			
		partList["sash"] = sash;
		partList["weapon"] = sword;
		partList["Shadow"]  = Shadow;
		partList["bodyDown"]  = bodyDown;
		
		partList["ef"]  = ef;
		partList["ef1"]  = ef1;
		partList["ef2"]  = ef2;
		partList["ef3"]  = ef3;
		partList["ef4"]  = ef4;

	}
}