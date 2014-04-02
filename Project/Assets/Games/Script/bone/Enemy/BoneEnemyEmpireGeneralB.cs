using UnityEngine;
using System.Collections;

public class BoneEnemyEmpireGeneralB : PieceAnimation {
	
	public GameObject armDownL;	
	public GameObject armDownR;	
	public GameObject armUpL;	
	public GameObject armUpR;	
	public GameObject Shadow;	
	
	public GameObject ADD_1;
	public GameObject ADD_2;
	public GameObject ADD_3;
	public GameObject E_ADD_1;
	public GameObject E_ADD_2;
	public GameObject E_ADD_3;
	public GameObject E_FLASH_1;
	public GameObject E_FLASH_2;
	public GameObject E_FLASH_3; 
	public GameObject E_FLASH_5;
	//public GameObject weapon_eft;
	public GameObject smoke;			
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armDownL"] = armDownL;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armUpR"] = armUpR;
		partList["bodyDown"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["shadow"] = Shadow;
			
		partList["ADD_1"] = ADD_1;
		partList["ADD_2"] = ADD_2;
		partList["ADD_3"] = ADD_3;
		partList["E_ADD_1"] = E_ADD_1;
		partList["E_ADD_2"] = E_ADD_2; 
		partList["E_ADD_3"] = E_ADD_3;
		partList["E_FLASH_1"] = E_FLASH_1;
		partList["E_FLASH_2"] = E_FLASH_2;
		partList["E_FLASH_3"] = E_FLASH_3; 
		partList["E_FLASH_5"] = E_FLASH_5;
		partList["E_FLASH_6"] = E_FLASH_5;
		partList["E_FLASH_7"] = E_FLASH_5; 
		partList["E_SMOKE_1"] = smoke;
		partList["E_SMOKE_2"] = smoke;
		//partList["weapon_eft"] = weapon_eft;		
	}
}
