using UnityEngine;
using System.Collections;

public class BoneEnemyCh2Nebula : PieceAnimation {
//public GameObject FEMALE_Arm_Back_Lower_01;
//public GameObject FEMALE_Arm_Back_Upper_01;
//public GameObject FEMALE_Arm_Top_Lower_01;
//public GameObject FEMALE_Arm_Top_Upper_01;
//public GameObject FEMALE_Head_01;
//public GameObject FEMALE_Head_01_left;
//public GameObject FEMALE_Head_02;
//public GameObject FEMALE_Head_02_left;
//public GameObject FEMALE_Head_04;
//public GameObject FEMALE_Head_04_left;
//public GameObject FEMALE_Head_05;
//public GameObject FEMALE_Head_05_left;
//public GameObject FEMALE_Leg_Back_Lower_01;
//public GameObject FEMALE_Leg_Back_Upper_01;
//public GameObject FEMALE_Leg_Top_Lower_01;
//public GameObject FEMALE_Leg_Top_Upper01;
//public GameObject FEMALE_Torso_01;
//public GameObject FEMALE_Weapon_01;
//public GameObject FEMALE_Weapon_02;
//public GameObject drop_shadow;	
	
    public GameObject FEMALE_Arm_Back_Lower_01  ;                      
    public GameObject FEMALE_Arm_Back_Upper_01  ;                      
    public GameObject FEMALE_Arm_Top_Lower_01   ;             
    public GameObject FEMALE_Arm_Top_Lower_01n  ;       // -               
    public GameObject FEMALE_Arm_Top_Lower_02   ;       // -              
    public GameObject FEMALE_Arm_Top_Upper_01   ;                     
    public GameObject FEMALE_Head_01_left       ;                 
    public GameObject FEMALE_Head_02_left       ;                 
    public GameObject FEMALE_Head_04_left       ;                 
    public GameObject FEMALE_Head_05_left       ;                 
    public GameObject FEMALE_Leg_Back_Lower_01  ;                      
    public GameObject FEMALE_Leg_Back_Upper_01  ;                      
    public GameObject FEMALE_Leg_Top_Lower_01   ;                     
    public GameObject FEMALE_Leg_Top_Upper01    ;                    
    public GameObject FEMALE_Torso_01           ;             
    public GameObject FEMALE_Weapon_01          ;              
    public GameObject FEMALE_Weapon_02          ;              
    public GameObject FEMALE_Weapon_03          ;       // -       
    public GameObject FEMALE_Weapon_04          ;       // -       
    public GameObject drop_shadow               ;         
	public GameObject nebula10                  ;              
	public GameObject nebula_21                 ;
    public GameObject nebula_34                 ;       // *
	public GameObject nebula_55                 ;               	
	
//	public GameObject FEMALE_Arm_Back_Lower_01                                
//	public GameObject FEMALE_Arm_Back_Upper_01                                
//	public GameObject FEMALE_Arm_Top_Lower_01                                
//	public GameObject FEMALE_Arm_Top_Lower_01__2                                
//	public GameObject FEMALE_Arm_Top_Upper_01                                
//	public GameObject FEMALE_Head_01_left                                
//	public GameObject FEMALE_Head_02_left                                
//	public GameObject FEMALE_Leg_Back_Lower_01                                
//	public GameObject FEMALE_Leg_Back_Upper_01                                
//	public GameObject FEMALE_Leg_Top_Lower_01                                
//	public GameObject FEMALE_Leg_Top_Upper01                                
//	public GameObject FEMALE_Torso_01                                
//	public GameObject FEMALE_Weapon_01                                
//	public GameObject drop_shadow                                
//	public GameObject nebula10                                
//	public GameObject nebula10__1                                	
	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
				
        partList["FEMALE_Arm_Back_Lower_01"  ] = FEMALE_Arm_Back_Lower_01;                
        partList["FEMALE_Arm_Back_Upper_01"  ] = FEMALE_Arm_Back_Upper_01;                
        partList["FEMALE_Arm_Top_Lower_01"   ] = FEMALE_Arm_Top_Lower_01 ;               
        partList["FEMALE_Arm_Top_Lower_01__2"] = FEMALE_Arm_Top_Lower_01 ;               
        partList["FEMALE_Arm_Top_Lower_01__1"] = FEMALE_Arm_Top_Lower_01 ;                  
        partList["FEMALE_Arm_Top_Lower_01__3"] = FEMALE_Arm_Top_Lower_01 ;                  
        partList["FEMALE_Arm_Top_Lower_01n"  ] = FEMALE_Arm_Top_Lower_01n;                
        partList["FEMALE_Arm_Top_Lower_02"   ] = FEMALE_Arm_Top_Lower_02 ;               
        partList["FEMALE_Arm_Top_Upper_01"   ] = FEMALE_Arm_Top_Upper_01 ;               
        partList["FEMALE_Head_01_left"       ] = FEMALE_Head_01_left     ;           
        partList["FEMALE_Head_02_left"       ] = FEMALE_Head_02_left     ;           
        partList["FEMALE_Head_04_left"       ] = FEMALE_Head_04_left     ;           
        partList["FEMALE_Head_05_left"       ] = FEMALE_Head_05_left     ;           
        partList["FEMALE_Leg_Back_Lower_01"  ] = FEMALE_Leg_Back_Lower_01;                
        partList["FEMALE_Leg_Back_Upper_01"  ] = FEMALE_Leg_Back_Upper_01;                
        partList["FEMALE_Leg_Top_Lower_01"   ] = FEMALE_Leg_Top_Lower_01 ;               
        partList["FEMALE_Leg_Top_Upper01"    ] = FEMALE_Leg_Top_Upper01  ;              
        partList["FEMALE_Torso_01"           ] = FEMALE_Torso_01         ;       
        partList["FEMALE_Weapon_01"          ] = FEMALE_Weapon_01        ;        
        partList["FEMALE_Weapon_02"          ] = FEMALE_Weapon_02        ;        
        partList["FEMALE_Weapon_03"          ] = FEMALE_Weapon_03        ;        
        partList["FEMALE_Weapon_04"          ] = FEMALE_Weapon_04        ;        
        partList["drop_shadow"               ] = drop_shadow             ;   
        partList["nebula10"                  ] = nebula10               ; 
        partList["nebula10__1"               ] = nebula10               ; 
        partList["nebula_34"                 ] = nebula_34               ; 
        partList["nebula_34__1"              ] = nebula_34               ;   
		partList["nebula_21"                 ] = nebula_21               ; 
		partList["nebula_55"                 ] = nebula_55               ; 
//		
//partList["FEMALE_Arm_Back_Lower_01"]=FEMALE_Arm_Back_Lower_01;
//partList["FEMALE_Arm_Back_Upper_01"]=FEMALE_Arm_Back_Upper_01;
//partList["FEMALE_Arm_Top_Lower_01"]=FEMALE_Arm_Top_Lower_01;
//partList["FEMALE_Arm_Top_Upper_01"]=FEMALE_Arm_Top_Upper_01;
//partList["FEMALE_Head_01"]=FEMALE_Head_01;
//partList["FEMALE_Head_01_left"]=FEMALE_Head_01_left;
//partList["FEMALE_Head_02"]=FEMALE_Head_02;
//partList["FEMALE_Head_02_left"]=FEMALE_Head_02_left;
//partList["FEMALE_Head_04"]=FEMALE_Head_04;
//partList["FEMALE_Head_04_left"]=FEMALE_Head_04_left;
//partList["FEMALE_Head_05"]=FEMALE_Head_05;
//partList["FEMALE_Head_05_left"]=FEMALE_Head_05_left;
//partList["FEMALE_Leg_Back_Lower_01"]=FEMALE_Leg_Back_Lower_01;
//partList["FEMALE_Leg_Back_Upper_01"]=FEMALE_Leg_Back_Upper_01;
//partList["FEMALE_Leg_Top_Lower_01"]=FEMALE_Leg_Top_Lower_01;
//partList["FEMALE_Leg_Top_Upper01"]=FEMALE_Leg_Top_Upper01;
//partList["FEMALE_Torso_01"]=FEMALE_Torso_01;
//partList["FEMALE_Weapon_01"]=FEMALE_Weapon_01;
//partList["FEMALE_Weapon_02"]=FEMALE_Weapon_02;
//partList["drop_shadow"]=drop_shadow;
	}
}
