using UnityEngine;
using System.Collections;

public class BoneEnemyCh3Caiera : PieceAnimation {
	
//public GameObject FEMALE_Accessory_Back_01;
//public GameObject FEMALE_Arm_Back_Lower_01;
//public GameObject FEMALE_Arm_Back_Lower_02;
//public GameObject FEMALE_Arm_Back_Upper_01;
//public GameObject FEMALE_Arm_Top_Lower_01;
//public GameObject FEMALE_Arm_Top_Upper_01;
//public GameObject FEMALE_Head_01;
//public GameObject FEMALE_Head_02;
//public GameObject FEMALE_Head_03;
//public GameObject FEMALE_Head_04;
//public GameObject FEMALE_Leg_Back_Lower_01;
//public GameObject FEMALE_Leg_Back_Upper_01;
//public GameObject FEMALE_Leg_Top_Lower_01;
//public GameObject FEMALE_Leg_Top_Upper01;
//public GameObject FEMALE_Torso_01;
//public GameObject FEMALE_Weapon_01;
//public GameObject drop_shadow;
	
    public GameObject FEMALE_Accessory_Back_01;
    public GameObject FEMALE_Arm_Back_Lower_01;
    public GameObject FEMALE_Arm_Back_Lower_02;
    public GameObject FEMALE_Arm_Back_Upper_01;
    public GameObject FEMALE_Arm_Top_Lower_01 ;
    public GameObject FEMALE_Arm_Top_Upper_01 ;
    public GameObject FEMALE_Head_01          ;
    public GameObject FEMALE_Head_02          ;
    public GameObject FEMALE_Head_03          ;
    public GameObject FEMALE_Head_04          ;
    public GameObject FEMALE_Leg_Back_Lower_01;
    public GameObject FEMALE_Leg_Back_Upper_01;
    public GameObject FEMALE_Leg_Top_Lower_01 ;
    public GameObject FEMALE_Leg_Top_Upper01  ;
    public GameObject FEMALE_Torso_01         ;
    public GameObject FEMALE_Weapon_01        ;
    public GameObject FEMALE_Weapon_02        ;
    public GameObject FEMALE_Weapon_03        ;
    public GameObject FEMALE_Weapon_044       ;
    public GameObject FEMALE_Weapon_12        ;
    public GameObject FEMALE_Weapon_31        ;
    public GameObject FEMALE_Weapon_32        ;
    public GameObject FEMALE_Weapon_33        ;
    public GameObject FEMALE_Weapon_34        ;
    public GameObject FEMALE_Weapon_35        ;
    public GameObject drop_shadow             ;	
	
	public GameObject FEMALE_Weapon_a_0001;
	public GameObject FEMALE_Weapon_a_0002;
	public GameObject FEMALE_Weapon_a_0003;
	public GameObject FEMALE_Weapon_a_0004;
	public GameObject FEMALE_Weapon_a_0005;
	public GameObject FEMALE_Weapon_a_0006;
	public GameObject FEMALE_Weapon_a_0007;
	public GameObject FEMALE_Weapon_a_0008;
	public GameObject FEMALE_Weapon_b_0001;
	public GameObject FEMALE_Weapon_b_0002;
	public GameObject FEMALE_Weapon_b_0003;
	public GameObject FEMALE_Weapon_b_0004;
	public GameObject FEMALE_Weapon_b_0005;
	public GameObject FEMALE_Weapon_b_0006;
	
	public GameObject Special_effects_01;
	public GameObject Special_effects_03;
	public GameObject Special_effects_05;
	public GameObject Special_effects_07;
	public GameObject Special_effects_09;
	public GameObject Special_effects_11;

public GameObject FEMALE_Weapon_06;
public GameObject FEMALE_Weapon_09;
public GameObject FEMALE_Weapon_10;
public GameObject FEMALE_Weapon_11;
public GameObject FEMALE_Weapon_13;
public GameObject FEMALE_Weapon_14;
public GameObject FEMALE_Weapon_15;
public GameObject FEMALE_Weapon_16;
public GameObject FEMALE_Weapon_28;
	
	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
				
        
        partList["FEMALE_Accessory_Back_01"   ] = FEMALE_Accessory_Back_01;
        partList["FEMALE_Arm_Back_Lower_01"   ] = FEMALE_Arm_Back_Lower_01;
        partList["FEMALE_Arm_Back_Lower_01__1"] = FEMALE_Arm_Back_Lower_01;
        partList["FEMALE_Arm_Back_Lower_02"   ] = FEMALE_Arm_Back_Lower_02;
        partList["FEMALE_Arm_Back_Upper_01"   ] = FEMALE_Arm_Back_Upper_01;
        partList["FEMALE_Arm_Top_Lower_01"    ] = FEMALE_Arm_Top_Lower_01 ;
        partList["FEMALE_Arm_Top_Upper_01"    ] = FEMALE_Arm_Top_Upper_01 ;
        partList["FEMALE_Head_01"             ] = FEMALE_Head_01          ;
        partList["FEMALE_Head_02"             ] = FEMALE_Head_02          ;
        partList["FEMALE_Head_03"             ] = FEMALE_Head_03          ;
        partList["FEMALE_Head_04"             ] = FEMALE_Head_04          ;
        partList["FEMALE_Leg_Back_Lower_01"   ] = FEMALE_Leg_Back_Lower_01;
        partList["FEMALE_Leg_Back_Upper_01"   ] = FEMALE_Leg_Back_Upper_01;
        partList["FEMALE_Leg_Top_Lower_01"    ] = FEMALE_Leg_Top_Lower_01 ;
        partList["FEMALE_Leg_Top_Upper01"     ] = FEMALE_Leg_Top_Upper01  ;
        partList["FEMALE_Torso_01"            ] = FEMALE_Torso_01         ;
        partList["FEMALE_Weapon_01"           ] = FEMALE_Weapon_01        ;
        partList["FEMALE_Weapon_02"           ] = FEMALE_Weapon_02        ;
        partList["FEMALE_Weapon_03"           ] = FEMALE_Weapon_03        ;
//        partList["FEMALE_Weapon_03__1"        ] = FEMALE_Weapon_03        ;
        partList["FEMALE_Weapon_03__2"        ] = FEMALE_Weapon_03        ;
//        partList["FEMALE_Weapon_03__3"        ] = FEMALE_Weapon_03        ;
        partList["FEMALE_Weapon_03__4"          ] = FEMALE_Weapon_03;
        partList["FEMALE_Weapon_044"          ] = FEMALE_Weapon_044       ;
        partList["FEMALE_Weapon_06"          ] = FEMALE_Weapon_06;
        partList["FEMALE_Weapon_09"          ] = FEMALE_Weapon_09;
        partList["FEMALE_Weapon_10"          ] = FEMALE_Weapon_10;
        partList["FEMALE_Weapon_11"          ] = FEMALE_Weapon_11;
        partList["FEMALE_Weapon_12"          ] = FEMALE_Weapon_12;
        partList["FEMALE_Weapon_12__3"          ] = FEMALE_Weapon_12;
        partList["FEMALE_Weapon_13"          ] = FEMALE_Weapon_13;
        partList["FEMALE_Weapon_14"          ] = FEMALE_Weapon_14;
        partList["FEMALE_Weapon_15"          ] = FEMALE_Weapon_15;
        partList["FEMALE_Weapon_16"          ] = FEMALE_Weapon_16;
        partList["FEMALE_Weapon_28"          ] = FEMALE_Weapon_28;
        partList["FEMALE_Weapon_28__1"          ] = FEMALE_Weapon_28;
        partList["FEMALE_Weapon_31"           ] = FEMALE_Weapon_31        ;
        partList["FEMALE_Weapon_32"           ] = FEMALE_Weapon_32        ;
        partList["FEMALE_Weapon_33"           ] = FEMALE_Weapon_33        ;
        partList["FEMALE_Weapon_34"           ] = FEMALE_Weapon_34        ;
        partList["FEMALE_Weapon_35"           ] = FEMALE_Weapon_35        ;
        partList["drop_shadow"                ] = drop_shadow             ;
		partList["FEMALE_Weapon_a_0001"]=FEMALE_Weapon_a_0001;
		partList["FEMALE_Weapon_a_0002"]=FEMALE_Weapon_a_0002;
		partList["FEMALE_Weapon_a_0003"]=FEMALE_Weapon_a_0003;
		partList["FEMALE_Weapon_a_0004"]=FEMALE_Weapon_a_0004;
		partList["FEMALE_Weapon_a_0005"]=FEMALE_Weapon_a_0005;
		partList["FEMALE_Weapon_a_0006"]=FEMALE_Weapon_a_0006;
		partList["FEMALE_Weapon_a_0007"]=FEMALE_Weapon_a_0007;
		partList["FEMALE_Weapon_a_0008"]=FEMALE_Weapon_a_0008;
		partList["FEMALE_Weapon_b_0001"]=FEMALE_Weapon_b_0001;
		partList["FEMALE_Weapon_b_0002"]=FEMALE_Weapon_b_0002;
		partList["FEMALE_Weapon_b_0003"]=FEMALE_Weapon_b_0003;
		partList["FEMALE_Weapon_b_0004"]=FEMALE_Weapon_b_0004;
		partList["FEMALE_Weapon_b_0005"]=FEMALE_Weapon_b_0005;
		partList["FEMALE_Weapon_b_0006"]=FEMALE_Weapon_b_0006;

		partList["Special_effects_01"] = Special_effects_01;
		partList["Special_effects_03"] = Special_effects_03;
		partList["Special_effects_05"] = Special_effects_05;
		partList["Special_effects_07"] = Special_effects_07;
		partList["Special_effects_09"] = Special_effects_09;
		partList["Special_effects_11"] = Special_effects_11;		

		
//partList["FEMALE_Accessory_Back_01"]=FEMALE_Accessory_Back_01;
//partList["FEMALE_Arm_Back_Lower_01"]=FEMALE_Arm_Back_Lower_01;
//partList["FEMALE_Arm_Back_Lower_02"]=FEMALE_Arm_Back_Lower_02;
//partList["FEMALE_Arm_Back_Upper_01"]=FEMALE_Arm_Back_Upper_01;
//partList["FEMALE_Arm_Top_Lower_01"]=FEMALE_Arm_Top_Lower_01;
//partList["FEMALE_Arm_Top_Upper_01"]=FEMALE_Arm_Top_Upper_01;
//partList["FEMALE_Head_01"]=FEMALE_Head_01;
//partList["FEMALE_Head_02"]=FEMALE_Head_02;
//partList["FEMALE_Head_03"]=FEMALE_Head_03;
//partList["FEMALE_Head_04"]=FEMALE_Head_04;
//partList["FEMALE_Leg_Back_Lower_01"]=FEMALE_Leg_Back_Lower_01;
//partList["FEMALE_Leg_Back_Upper_01"]=FEMALE_Leg_Back_Upper_01;
//partList["FEMALE_Leg_Top_Lower_01"]=FEMALE_Leg_Top_Lower_01;
//partList["FEMALE_Leg_Top_Upper01"]=FEMALE_Leg_Top_Upper01;
//partList["FEMALE_Torso_01"]=FEMALE_Torso_01;
//partList["FEMALE_Weapon_01"]=FEMALE_Weapon_01;
//partList["drop_shadow"]=drop_shadow;
		
	}
}
