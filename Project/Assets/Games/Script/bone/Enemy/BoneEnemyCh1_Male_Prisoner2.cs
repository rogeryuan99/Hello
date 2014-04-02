using UnityEngine;
using System.Collections;

public class BoneEnemyCh1_Male_Prisoner2 : PieceAnimation {

public GameObject MEDIUM_Arm_Back_Lower_01;
public GameObject MEDIUM_Arm_Back_Lower_02;
public GameObject MEDIUM_Arm_Back_Upper_01;
public GameObject MEDIUM_Arm_Top_Lower_01 ;
public GameObject MEDIUM_Arm_Top_Upper_01 ;
public GameObject MEDIUM_Head_01          ;
public GameObject MEDIUM_Head_02          ;
public GameObject MEDIUM_Head_03          ;
public GameObject MEDIUM_Head_06          ;
public GameObject MEDIUM_Head_07          ;
public GameObject MEDIUM_Leg_Back_Lower_01;
public GameObject MEDIUM_Leg_Back_Upper_01;
public GameObject MEDIUM_Leg_Top_Lower_01 ;
public GameObject MEDIUM_Leg_Top_Upper_01 ;
public GameObject MEDIUM_Torso_01         ;
public GameObject MEDIUM_Weapon_01        ;
public GameObject drop_shadow             ;


	
	public override void Awake (){
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
partList["MEDIUM_Arm_Back_Lower_01"]=MEDIUM_Arm_Back_Lower_01;   
partList["MEDIUM_Arm_Back_Lower_02"]=MEDIUM_Arm_Back_Lower_02;   
partList["MEDIUM_Arm_Back_Upper_01"]=MEDIUM_Arm_Back_Upper_01;   
partList["MEDIUM_Arm_Top_Lower_01"]=MEDIUM_Arm_Top_Lower_01 ;   
partList["MEDIUM_Arm_Top_Upper_01"]=MEDIUM_Arm_Top_Upper_01 ;   
partList["MEDIUM_Head_01"]=MEDIUM_Head_01          ;   
partList["MEDIUM_Head_02"]=MEDIUM_Head_02          ;   
partList["MEDIUM_Head_03"]=MEDIUM_Head_03          ;   
partList["MEDIUM_Head_06"]=MEDIUM_Head_06          ;   
partList["MEDIUM_Head_07"]=MEDIUM_Head_07          ;   
partList["MEDIUM_Leg_Back_Lower_01"]=MEDIUM_Leg_Back_Lower_01;   
partList["MEDIUM_Leg_Back_Upper_01"]=MEDIUM_Leg_Back_Upper_01;   
partList["MEDIUM_Leg_Top_Lower_01"]=MEDIUM_Leg_Top_Lower_01 ;   
partList["MEDIUM_Leg_Top_Upper_01"]=MEDIUM_Leg_Top_Upper_01 ;   
partList["MEDIUM_Torso_01"]=MEDIUM_Torso_01         ;   
partList["MEDIUM_Weapon_01"]=MEDIUM_Weapon_01        ;   
partList["drop_shadow"]=drop_shadow             ;   

	}
}
