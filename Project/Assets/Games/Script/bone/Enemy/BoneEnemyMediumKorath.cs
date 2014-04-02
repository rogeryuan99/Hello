using UnityEngine;
using System.Collections;

public class BoneEnemyMediumKorath : PieceAnimation {

//    public GameObject MEDIUM_Arm_Back_Lower_01;
//    public GameObject MEDIUM_Arm_Back_Lower_02;
//    public GameObject MEDIUM_Arm_Back_Upper_01;
//    public GameObject MEDIUM_Arm_Top_Lower_01 ;
//  	public GameObject MEDIUM_Arm_Top_Upper_01 ;
//    public GameObject MEDIUM_Head_01		  ;
//    public GameObject MEDIUM_Head_02		  ;
//    public GameObject MEDIUM_Head_03		  ;
//    public GameObject MEDIUM_Head_06		  ;
//    public GameObject MEDIUM_Head_07		  ;
//    public GameObject MEDIUM_Leg_Back_Lower_01;
//    public GameObject MEDIUM_Leg_Back_Upper_01;
//    public GameObject MEDIUM_Leg_Top_Lower_01 ;
//    public GameObject MEDIUM_Leg_Top_Upper_01 ;
//	public GameObject MEDIUM_Punch_FX_02	  ;
//	public GameObject MEDIUM_Punch_FX_026	  ;
//	public GameObject MEDIUM_Torso_01	      ;
//	public GameObject MEDIUM_Weapon_01		  ;
//	public GameObject MEDIUM_Weapon_015	      ;
//	public GameObject MEDIUM_Weapon_02	      ;
//	public GameObject MEDIUM_Weapon_023	      ;
//	public GameObject drop_shadow	          ;
	
	public GameObject MEDIUM_Arm_Back_Lower_01  ;
	public GameObject MEDIUM_Arm_Back_Lower_02  ;
	public GameObject MEDIUM_Arm_Back_Lower_03  ;
	public GameObject MEDIUM_Arm_Back_Upper_01  ;
	public GameObject MEDIUM_Arm_Top_Lower_01   ;
	public GameObject MEDIUM_Arm_Top_Upper_01   ;
	public GameObject MEDIUM_Head_01            ;
	public GameObject MEDIUM_Head_02            ;
	public GameObject MEDIUM_Head_03            ;
	public GameObject MEDIUM_Head_06            ;
	public GameObject MEDIUM_Head_07            ;
	public GameObject MEDIUM_Leg_Back_Lower_01  ;
	public GameObject MEDIUM_Leg_Back_Upper_01  ;
	public GameObject MEDIUM_Leg_Top_Lower_01   ;
	public GameObject MEDIUM_Leg_Top_Upper_01   ;
	public GameObject MEDIUM_Punch_FX_02        ;
	public GameObject MEDIUM_Torso_01           ;
	public GameObject MEDIUM_Weapon_01          ;
	public GameObject MEDIUM_Weapon_02          ;
	public GameObject MEDIUM_Weapon_03          ;
	public GameObject MEDIUM_Weapon_04          ;
	public GameObject MEDIUM_Weapon_05          ;
	public GameObject drop_shadow               ;
	public GameObject effect_20                 ;
	public GameObject effect_4                  ;
	public GameObject effect_6                  ;
	public GameObject effect_7                  ;
	public GameObject effect_7_2                ;
	
	public override void Awake (){
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["MEDIUM_Arm_Back_Lower_01"    ] = MEDIUM_Arm_Back_Lower_01  ;                  
		partList["MEDIUM_Arm_Back_Lower_02"    ] = MEDIUM_Arm_Back_Lower_02  ;                  
		partList["MEDIUM_Arm_Back_Lower_03"    ] = MEDIUM_Arm_Back_Lower_03  ;                  
		partList["MEDIUM_Arm_Back_Upper_01"    ] = MEDIUM_Arm_Back_Upper_01  ;                  
		partList["MEDIUM_Arm_Top_Lower_01"     ] = MEDIUM_Arm_Top_Lower_01   ;                 
		partList["MEDIUM_Arm_Top_Lower_01__1"  ] = MEDIUM_Arm_Top_Lower_01;                    
		partList["MEDIUM_Arm_Top_Lower_01__2"  ] = MEDIUM_Arm_Top_Lower_01;                    
		partList["MEDIUM_Arm_Top_Lower_01__4"  ] = MEDIUM_Arm_Top_Lower_01;                    
		partList["MEDIUM_Arm_Top_Upper_01"     ] = MEDIUM_Arm_Top_Upper_01   ;                 
		partList["MEDIUM_Head_01"              ] = MEDIUM_Head_01            ;        
		partList["MEDIUM_Head_02"              ] = MEDIUM_Head_02            ;        
		partList["MEDIUM_Head_03"              ] = MEDIUM_Head_03            ;        
		partList["MEDIUM_Head_06"              ] = MEDIUM_Head_06            ;        
		partList["MEDIUM_Head_07"              ] = MEDIUM_Head_07            ;        
		partList["MEDIUM_Leg_Back_Lower_01"    ] = MEDIUM_Leg_Back_Lower_01  ;                  
		partList["MEDIUM_Leg_Back_Upper_01"    ] = MEDIUM_Leg_Back_Upper_01  ;                  
		partList["MEDIUM_Leg_Top_Lower_01"     ] = MEDIUM_Leg_Top_Lower_01   ;                 
		partList["MEDIUM_Leg_Top_Upper_01"     ] = MEDIUM_Leg_Top_Upper_01   ;                 
		partList["MEDIUM_Punch_FX_02"          ] = MEDIUM_Punch_FX_02        ;            
		partList["MEDIUM_Punch_FX_02__6"       ] = MEDIUM_Punch_FX_02     ;               
		partList["MEDIUM_Torso_01"             ] = MEDIUM_Torso_01           ;         
		partList["MEDIUM_Weapon_01"            ] = MEDIUM_Weapon_01          ;          
		partList["MEDIUM_Weapon_01__5"         ] = MEDIUM_Weapon_01       ;             
		partList["MEDIUM_Weapon_02"            ] = MEDIUM_Weapon_02          ;          
		partList["MEDIUM_Weapon_02__2"         ] = MEDIUM_Weapon_02       ;             
		partList["MEDIUM_Weapon_02__3"         ] = MEDIUM_Weapon_02       ;             
		partList["MEDIUM_Weapon_03"            ] = MEDIUM_Weapon_03          ;          
		partList["MEDIUM_Weapon_04"            ] = MEDIUM_Weapon_04          ;          
		partList["MEDIUM_Weapon_05"            ] = MEDIUM_Weapon_05          ;          
		partList["drop_shadow"                 ] = drop_shadow               ;     
		partList["effect_20"                   ] = effect_20                 ;   
		partList["effect_4"                    ] = effect_4                  ;  
		partList["effect_6"                    ] = effect_6                  ;  
		partList["effect_7"                    ] = effect_7                  ;  
		partList["effect_7_2"                  ] = effect_7_2                ;    
		
//		partList["MEDIUM_Arm_Back_Lower_01"] =   	MEDIUM_Arm_Back_Lower_01  ;
//		partList["MEDIUM_Arm_Back_Lower_02"] =   	MEDIUM_Arm_Back_Lower_02  ;
//		partList["MEDIUM_Arm_Back_Upper_01"] =   	MEDIUM_Arm_Back_Upper_01  ;
//		partList["MEDIUM_Arm_Top_Lower_01"] =   	MEDIUM_Arm_Top_Lower_01   ;
//		partList["MEDIUM_Arm_Top_Lower_011"] =   	MEDIUM_Arm_Top_Lower_01   ;
//		partList["MEDIUM_Arm_Top_Lower_012"] =   	MEDIUM_Arm_Top_Lower_01   ;
//		partList["MEDIUM_Arm_Top_Lower_014"] =   	MEDIUM_Arm_Top_Lower_01   ;
//		partList["MEDIUM_Arm_Top_Upper_01"] =   	MEDIUM_Arm_Top_Upper_01   ;
//		partList["MEDIUM_Head_01"		] =   	MEDIUM_Head_01            ;
//		partList["MEDIUM_Head_02"		] =   	MEDIUM_Head_02            ;
//		partList["MEDIUM_Head_03"		] =   	MEDIUM_Head_03            ;
//		partList["MEDIUM_Head_06"		] =   	MEDIUM_Head_06            ;
//		partList["MEDIUM_Head_07"		] =   	MEDIUM_Head_07            ;
//		partList["MEDIUM_Leg_Back_Lower_01"] =   	MEDIUM_Leg_Back_Lower_01  ;
//		partList["MEDIUM_Leg_Back_Upper_01"] =   	MEDIUM_Leg_Back_Upper_01  ;
//		partList["MEDIUM_Leg_Top_Lower_01"] =   	MEDIUM_Leg_Top_Lower_01   ;
//		partList["MEDIUM_Leg_Top_Upper_01"] =   	MEDIUM_Leg_Top_Upper_01   ;
//		partList["MEDIUM_Punch_FX_02"	] =   	MEDIUM_Punch_FX_02           ;
//		partList["MEDIUM_Punch_FX_026"	] =   	MEDIUM_Punch_FX_02          ;
//		partList["MEDIUM_Torso_01"		] =   	MEDIUM_Torso_01           ;
//		partList["MEDIUM_Weapon_01"		] =   	MEDIUM_Weapon_01          ;
//		partList["MEDIUM_Weapon_015"	] =   	MEDIUM_Weapon_01          ;
//		partList["MEDIUM_Weapon_02"		] =   	MEDIUM_Weapon_02          ;
//		partList["MEDIUM_Weapon_023"	] =   	MEDIUM_Weapon_02          ;
//		partList["drop_shadow"	     	] =   	drop_shadow               ;
	}
}
