using UnityEngine;
using System.Collections;

public class BoneEnemyFemalePrisoner2 : PieceAnimation {
	
	
    public GameObject FEMALE_Arm_Back_Lower_01;          
    public GameObject FEMALE_Arm_Back_Upper_01;
    public GameObject FEMALE_Arm_Top_Lower_01;
    public GameObject FEMALE_Arm_Top_Upper_01;
    public GameObject FEMALE_Head_01;
    public GameObject FEMALE_Head_02;
    public GameObject FEMALE_Head_04;
    public GameObject FEMALE_Leg_Back_Lower_01;
    public GameObject FEMALE_Leg_Back_Upper_01;
    public GameObject FEMALE_Leg_Top_Lower_01;
    public GameObject FEMALE_Leg_Top_Upper01;
    public GameObject FEMALE_Torso_01;
	public GameObject FEMALE_Weapon_01;
    public GameObject drop_shadow;

	
	public override void Awake (){
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();                                                     
		  partList["FEMALE_Arm_Back_Lower_01"] =  FEMALE_Arm_Back_Lower_01  ;     
		  partList["FEMALE_Arm_Back_Upper_01"] =  FEMALE_Arm_Back_Upper_01  ;     
		  partList["FEMALE_Arm_Top_Lower_01"] =   FEMALE_Arm_Top_Lower_01   ;     
		  partList["FEMALE_Arm_Top_Upper_01"] =   FEMALE_Arm_Top_Upper_01   ;     
		  partList["FEMALE_Head_01"] =            FEMALE_Head_01            ;     
		  partList["FEMALE_Head_02"] =            FEMALE_Head_02            ;     
		  partList["FEMALE_Head_04"] =            FEMALE_Head_04            ;     
		  partList["FEMALE_Leg_Back_Lower_01"] =  FEMALE_Leg_Back_Lower_01  ;     
		  partList["FEMALE_Leg_Back_Upper_01"] =  FEMALE_Leg_Back_Upper_01  ;     
		  partList["FEMALE_Leg_Top_Lower_01"] =   FEMALE_Leg_Top_Lower_01   ;     
		  partList["FEMALE_Leg_Top_Upper01"] =    FEMALE_Leg_Top_Upper01    ;     
		  partList["FEMALE_Torso_01"] =           FEMALE_Torso_01           ;
		  partList["FEMALE_Weapon_01"] =           FEMALE_Weapon_01           ;
		  partList["drop_shadow"] =               drop_shadow               ;     


	}
}
