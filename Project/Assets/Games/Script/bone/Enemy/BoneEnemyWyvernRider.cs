using UnityEngine;
using System.Collections;

public class BoneEnemyWyvernRider : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject Shadow;
	public GameObject bodyDown;
	public GameObject sword;
	public GameObject head2;
	public GameObject tail;

	public GameObject wing; 
	public GameObject wing1;
	public GameObject wing2;

	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;
		partList["legR"] = legR;
		
		partList["armUpR"] = armUpR;
		partList["armUpL"] = armUpL;
		partList["bodyDown"] = bodyDown; 
		partList["shadow"] = Shadow;
		partList["weapon"] = sword; 
		
		partList["head2"]  = head2;
		partList["tail"]  = tail;
		partList["wing"]  = wing;	
		partList["wing1"]  = wing1;
		partList["wing2"]  = wing2; 
	
	}
}
