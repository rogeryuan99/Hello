using UnityEngine;
using System.Collections;

public class BoneMantis : PieceAnimation
{

//public GameObject FEMALE_Accessory_Top_01;
//public GameObject FEMALE_Arm_Back_Lower_01;
//public GameObject FEMALE_Arm_Back_Upper_01;
//public GameObject FEMALE_Arm_Top_Lower_01;
//public GameObject FEMALE_Arm_Top_Upper_01;
//public GameObject FEMALE_Head_01;
//public GameObject FEMALE_Head_02;
//public GameObject FEMALE_Head_04;
//public GameObject FEMALE_Leg_Back_Upper_01;
//public GameObject FEMALE_Leg_Back_Lower_01;
//public GameObject FEMALE_Leg_Top_Lower_01;
//public GameObject FEMALE_Leg_Top_Upper01;
//public GameObject FEMALE_Torso_01;
//public GameObject drop_shadow;
//	
//
//public GameObject FEMALE_Arm_Top_Lower_02_b; 
//public GameObject FEMALE_Arm_Top_Lower_03_b; 
//public GameObject FEMALE_Head_03_b;      
//public GameObject Mantis_b_14;                  
//public GameObject Mantis_b_4;                  
//public GameObject Mantis_b_5;   
//public GameObject Mantis_b_6;
//public GameObject Stick_04c;
//public GameObject Stick_14c;                   
//public GameObject Stick_17c;                
//public GameObject Stick_19c;                   
//public GameObject effect_01b;                  
//public GameObject effect_8_2;                  
//public GameObject effect_8_3;                  
	
    public GameObject FEMALE_Accessory_Top_01     ;             
    public GameObject FEMALE_Arm_Back_Lower_01    ;              
    public GameObject FEMALE_Arm_Back_Upper_01    ;              
    public GameObject FEMALE_Arm_Top_Lower_01     ;             
    public GameObject FEMALE_Arm_Top_Lower_02_b   ;               
    public GameObject FEMALE_Arm_Top_Lower_03_b   ;               
    public GameObject FEMALE_Arm_Top_Upper_01     ;             
    public GameObject FEMALE_Head_01              ;    
    public GameObject FEMALE_Head_02              ;    
    public GameObject FEMALE_Head_03_b            ;      
    public GameObject FEMALE_Head_04              ;    
    public GameObject FEMALE_Leg_Back_Lower_01    ;              
    public GameObject FEMALE_Leg_Back_Upper_01    ;              
    public GameObject FEMALE_Leg_Top_Lower_01     ;             
    public GameObject FEMALE_Leg_Top_Upper01      ;            
    public GameObject FEMALE_Torso_01             ;     
    public GameObject Mantis_b_4                  ;
    public GameObject Mantis_b_5                  ;
    public GameObject Mantis_b_6                  ;
    public GameObject Stick_04c                   ;
    public GameObject Stick_14c                   ;
    public GameObject Stick_17c                   ;
    public GameObject Stick_19c                   ;
    public GameObject drop_shadow                 ; 
    public GameObject effect_01a                  ;
    public GameObject effect_01b                  ;
    public GameObject effect_02a                  ;
    public GameObject effect_03a                  ;
	public GameObject effect_04a                  ;
    public GameObject effect_8_2                  ;
    public GameObject effect_8_3                  ;
    public GameObject Mantis_b_14                 ;	
	
	
	public override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
        partList["FEMALE_Accessory_Top_01"      ] = FEMALE_Accessory_Top_01     ;                      
        partList["FEMALE_Arm_Back_Lower_01"     ] = FEMALE_Arm_Back_Lower_01    ;                       
        partList["FEMALE_Arm_Back_Lower_01__1"  ] = FEMALE_Arm_Back_Lower_01 ;                          
        partList["FEMALE_Arm_Back_Upper_01"     ] = FEMALE_Arm_Back_Upper_01    ;                       
        partList["FEMALE_Arm_Top_Lower_01"      ] = FEMALE_Arm_Top_Lower_01     ;                      
        partList["FEMALE_Arm_Top_Lower_01__1"   ] = FEMALE_Arm_Top_Lower_01  ;                         
        partList["FEMALE_Arm_Top_Lower_02_b"    ] = FEMALE_Arm_Top_Lower_02_b   ;                        
        partList["FEMALE_Arm_Top_Lower_02_b__1" ] = FEMALE_Arm_Top_Lower_02_b;                           
        partList["FEMALE_Arm_Top_Lower_02_b__8" ] = FEMALE_Arm_Top_Lower_02_b;                           
        partList["FEMALE_Arm_Top_Lower_03_b"    ] = FEMALE_Arm_Top_Lower_03_b   ;                        
        partList["FEMALE_Arm_Top_Lower_03_b__1" ] = FEMALE_Arm_Top_Lower_03_b;                           
        partList["FEMALE_Arm_Top_Upper_01"      ] = FEMALE_Arm_Top_Upper_01     ;                      
        partList["FEMALE_Arm_Top_Upper_01__1"   ] = FEMALE_Arm_Top_Upper_01  ;                         
        partList["FEMALE_Arm_Top_Upper_01__2"   ] = FEMALE_Arm_Top_Upper_01  ;                         
        partList["FEMALE_Arm_Top_Upper_01__9"   ] = FEMALE_Arm_Top_Upper_01  ;                         
        partList["FEMALE_Head_01"               ] = FEMALE_Head_01              ;             
        partList["FEMALE_Head_02"               ] = FEMALE_Head_02              ;             
        partList["FEMALE_Head_03_b"             ] = FEMALE_Head_03_b            ;               
        partList["FEMALE_Head_04"               ] = FEMALE_Head_04              ;             
        partList["FEMALE_Leg_Back_Lower_01"     ] = FEMALE_Leg_Back_Lower_01    ;                       
        partList["FEMALE_Leg_Back_Upper_01"     ] = FEMALE_Leg_Back_Upper_01    ;                       
        partList["FEMALE_Leg_Top_Lower_01"      ] = FEMALE_Leg_Top_Lower_01     ;                      
        partList["FEMALE_Leg_Top_Upper01"       ] = FEMALE_Leg_Top_Upper01      ;                     
        partList["FEMALE_Torso_01"              ] = FEMALE_Torso_01             ;              
        partList["Mantis_b_4"                   ] = Mantis_b_4                  ;         
        partList["Mantis_b_5"                   ] = Mantis_b_5                  ;         
        partList["Mantis_b_5__1"                ] = Mantis_b_5               ;            
        partList["Mantis_b_5__2"                ] = Mantis_b_5               ;            
        partList["Mantis_b_5__3"                ] = Mantis_b_5               ;            
        partList["Mantis_b_6"                   ] = Mantis_b_6                  ;         
        partList["Mantis_b_6__1"                ] = Mantis_b_6               ;            
        partList["Mantis_b_6__2"                ] = Mantis_b_6               ;            
        partList["Mantis_b_6__3"                ] = Mantis_b_6               ;            
        partList["Stick_04c"                    ] = Stick_04c                   ;        
        partList["Stick_04c__2"                 ] = Stick_04c                ;           
        partList["Stick_14c"                    ] = Stick_14c                   ;        
        partList["Stick_17c"                    ] = Stick_17c                   ;        
        partList["Stick_17c__2"                 ] = Stick_17c                ;           
        partList["Stick_19c"                    ] = Stick_19c                   ;        
        partList["drop_shadow"                  ] = drop_shadow                 ;          
        partList["effect_01a"                   ] = effect_01a                  ;         
        partList["effect_01b"                   ] = effect_01b                  ;         
        partList["effect_02a"                   ] = effect_02a                  ;         
        partList["effect_02a__1"                ] = effect_02a               ;            
        partList["effect_02a__2"                ] = effect_02a               ;            
        partList["effect_02a__3"                ] = effect_02a               ;            
        partList["effect_02a__4"                ] = effect_02a               ;            
        partList["effect_02a__5"                ] = effect_02a               ;            
        partList["effect_02a__6"                ] = effect_02a               ;            
        partList["effect_02a__7"                ] = effect_02a               ;            
        partList["effect_03a"                   ] = effect_03a                  ;         
        partList["effect_04a"                   ] = effect_04a                  ;         
        partList["effect_8_2"                   ] = effect_8_2                  ;         
        partList["effect_8_3"                   ] = effect_8_3                  ;         
        partList["Mantis_b_14"                  ] = Mantis_b_14                 ;         		
		
		
//partList["FEMALE_Accessory_Top_01"]=FEMALE_Accessory_Top_01;
//partList["FEMALE_Arm_Back_Lower_01"]=FEMALE_Arm_Back_Lower_01;
//partList["FEMALE_Arm_Back_Upper_01"]=FEMALE_Arm_Back_Upper_01;
//partList["FEMALE_Arm_Top_Lower_01"]=FEMALE_Arm_Top_Lower_01;
//partList["FEMALE_Arm_Top_Upper_01"]=FEMALE_Arm_Top_Upper_01;
//partList["FEMALE_Head_01"]=FEMALE_Head_01;
//partList["FEMALE_Head_02"]=FEMALE_Head_02;
//partList["FEMALE_Head_04"]=FEMALE_Head_04;
//partList["FEMALE_Leg_Back_Upper_01"]=FEMALE_Leg_Back_Upper_01;
//partList["FEMALE_Leg_Back_Lower_01"]=FEMALE_Leg_Back_Lower_01;
//partList["FEMALE_Leg_Top_Lower_01"]=FEMALE_Leg_Top_Lower_01;
//partList["FEMALE_Leg_Top_Upper01"]=FEMALE_Leg_Top_Upper01;
//partList["FEMALE_Torso_01"]=FEMALE_Torso_01;
//partList["drop_shadow"]=drop_shadow;	
//		partList["FEMALE_Arm_Top_Upper_01__1"]=FEMALE_Arm_Top_Upper_01;
//		
//partList ["FEMALE_Arm_Back_Lower_01__1"]=FEMALE_Arm_Back_Lower_01;
//partList ["FEMALE_Arm_Top_Lower_01__1"]=FEMALE_Arm_Top_Lower_01;
//partList ["FEMALE_Arm_Top_Lower_02_b"]=FEMALE_Arm_Top_Lower_02_b;
//partList ["FEMALE_Arm_Top_Lower_02_b__1"]=FEMALE_Arm_Top_Lower_02_b;
//partList ["FEMALE_Arm_Top_Lower_03_b"]=FEMALE_Arm_Top_Lower_03_b;
//partList ["FEMALE_Arm_Top_Upper_01__2"]=FEMALE_Arm_Top_Upper_01;
//partList ["FEMALE_Head_03_b"]=  FEMALE_Head_03_b;
//partList ["Mantis_b_14"]=Mantis_b_14;
//partList ["Mantis_b_4"]=Mantis_b_4;
//partList ["Mantis_b_5"]=Mantis_b_5;
//partList ["Mantis_b_5__1"]=Mantis_b_5;
//partList ["Mantis_b_5__2"]=Mantis_b_5;
//partList ["Mantis_b_5__3"]=Mantis_b_5;
//		
//partList ["Mantis_b_6"]=Mantis_b_6;
//partList ["Mantis_b_6__1"]=Mantis_b_6;
//partList ["Mantis_b_6__2"]=Mantis_b_6;
//partList ["Mantis_b_6__3"]=Mantis_b_6;
//partList ["Stick_04c"]=Stick_04c;
//partList ["Stick_04c__2"]=Stick_04c;
//partList ["Stick_14c"]=Stick_14c;
//partList ["Stick_17c"]=Stick_17c;
//partList ["Stick_17c__2"]=Stick_17c;
//partList ["Stick_19c"]=Stick_19c;
//partList ["effect_01b"]=effect_01b;
//partList ["effect_8_2"]=effect_8_2;
//partList ["effect_8_3"]=effect_8_3;
	}
}
