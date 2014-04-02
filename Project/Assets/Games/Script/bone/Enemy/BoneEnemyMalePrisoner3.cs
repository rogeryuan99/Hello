using UnityEngine;
using System.Collections;

public class BoneEnemyMalePrisoner3 : PieceAnimation {

public GameObject MEDIUM_Arm_Back_Lower_01		;
public GameObject MEDIUM_Arm_Back_Upper_01		;
public GameObject MEDIUM_Arm_Top_Lower_01		;
public GameObject MEDIUM_Arm_Top_Upper_01		;
public GameObject MEDIUM_Head_01				;
public GameObject MEDIUM_Head_02				;
public GameObject MEDIUM_Head_03				;
public GameObject MEDIUM_Head_06				;
public GameObject MEDIUM_Head_07				;
public GameObject MEDIUM_Leg_Back_Lower_01		;
public GameObject MEDIUM_Leg_Back_Upper_01		;
public GameObject MEDIUM_Leg_Top_Lower_01		;
public GameObject MEDIUM_Leg_Top_Upper_01		;
public GameObject MEDIUM_Punch_FX_02			;
public GameObject MEDIUM_Punch_FX_027			;
public GameObject MEDIUM_Torso_01				;
public GameObject MEDIUM_Weapon_01				;
public GameObject drop_shadow             		;
                                     
	public override void Awake (){
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();      
  partList["MEDIUM_Arm_Back_Lower_01"] = MEDIUM_Arm_Back_Lower_01	;  
  partList["MEDIUM_Arm_Back_Upper_01"] = MEDIUM_Arm_Back_Upper_01	;  
  partList["MEDIUM_Arm_Top_Lower_01"] = MEDIUM_Arm_Top_Lower_01	;  
  partList["MEDIUM_Arm_Top_Lower_011"] = MEDIUM_Arm_Top_Lower_01	;  
  partList["MEDIUM_Arm_Top_Lower_012"] = MEDIUM_Arm_Top_Lower_01	;  
  partList["MEDIUM_Arm_Top_Lower_014"] = MEDIUM_Arm_Top_Lower_01	;  
  partList["MEDIUM_Arm_Top_Upper_01"] = MEDIUM_Arm_Top_Upper_01	;  
  partList["MEDIUM_Head_01"] = MEDIUM_Head_01				;  
  partList["MEDIUM_Head_02"] = MEDIUM_Head_02				;  
  partList["MEDIUM_Head_03"] = MEDIUM_Head_03				;  
  partList["MEDIUM_Head_06"] = MEDIUM_Head_06				;  
  partList["MEDIUM_Head_07"] = MEDIUM_Head_07				;  
  partList["MEDIUM_Leg_Back_Lower_01"] = MEDIUM_Leg_Back_Lower_01	;  
  partList["MEDIUM_Leg_Back_Upper_01"] = MEDIUM_Leg_Back_Upper_01	;  
  partList["MEDIUM_Leg_Top_Lower_01"] = MEDIUM_Leg_Top_Lower_01	;  
  partList["MEDIUM_Leg_Top_Upper_01"] = MEDIUM_Leg_Top_Upper_01	;  
  partList["MEDIUM_Punch_FX_02"] = MEDIUM_Punch_FX_02			;  
  partList["MEDIUM_Punch_FX_027"] = MEDIUM_Punch_FX_027		;  
  partList["MEDIUM_Torso_01"] = MEDIUM_Torso_01			;   
  partList["MEDIUM_Weapon_01"] = MEDIUM_Weapon_01			;  
  partList["MEDIUM_Weapon_015"] = MEDIUM_Weapon_01			; 
  partList["MEDIUM_Weapon_016"] = MEDIUM_Weapon_01			; 
  partList["drop_shadow"] = drop_shadow                ;  
	}
}
