using UnityEngine;
using System.Collections;

public class BoneFreezeGuy_IceBlock : PieceAnimation {
	public GameObject bks1;
	public GameObject bks2;
	public GameObject bks3;
	public GameObject bks4;
	public GameObject bks5;
	public GameObject bks6;
	public GameObject FGB1;
	public GameObject FGB2;
	public GameObject FGB3;
	public GameObject FGB4;
	public GameObject FGB5;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();

		partList["bks1"] = bks1;
		partList["bks2"] = bks2;
		partList["bks3"] = bks3;
		partList["bks4"] = bks4;
		partList["bks5"] = bks5;
		partList["bks6"] = bks6;
		
		partList["FGB1"] = FGB1;
		partList["FGB2"] = FGB2;
		partList["FGB3"] = FGB3;
		partList["FGB4"] = FGB4;
		partList["FGB5"] = FGB5;
	}

}
