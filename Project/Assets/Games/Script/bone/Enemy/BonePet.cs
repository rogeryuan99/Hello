using UnityEngine;
using System.Collections;

public class BonePet : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodyDown;
	public GameObject weaponc_FI;
	public GameObject Shadow;
	public GameObject legupL;
	public GameObject legupR;
	public GameObject TailC;
	public GameObject head2;

	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["head2"]  = head2;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
			
		partList["bodyDown"] = bodyDown;
		partList["weaponc_FI"] = weaponc_FI;
		partList["Shadow"]  = Shadow;
		partList["legupL"]  = legupL;
		partList["legupR"]  = legupR;
		partList["TailC"]  = TailC;
		
	}
}
