using UnityEngine;
using System.Collections;

public class BoneEnemyCh3BossRedKing : PieceAnimation {
	
//public GameObject MEDIUM_Arm_Back_Lower_01;
//public GameObject MEDIUM_Arm_Back_Lower_02;
//public GameObject MEDIUM_Arm_Back_Upper_01;
//public GameObject MEDIUM_Arm_Top_Lower_01;
//public GameObject MEDIUM_Arm_Top_Upper_01;
//public GameObject MEDIUM_Head_01;
//public GameObject MEDIUM_Head_02;
//public GameObject MEDIUM_Head_03;
//public GameObject MEDIUM_Head_06;
//public GameObject MEDIUM_Head_07;
//public GameObject MEDIUM_Leg_Back_Lower_01;
//public GameObject MEDIUM_Leg_Back_Upper_01;
//public GameObject MEDIUM_Leg_Top_Lower_01;
//public GameObject MEDIUM_Leg_Top_Upper_01;
//public GameObject MEDIUM_Shoot_FX_02;
//public GameObject MEDIUM_Torso_01;
//public GameObject MEDIUM_Weapon_01;	
//public GameObject drop_shadow;
	
    public GameObject MEDIUM_Arm_Back_Lower_01;                             
    public GameObject MEDIUM_Arm_Back_Lower_02;                             
    public GameObject MEDIUM_Arm_Back_Upper_01;                             
    public GameObject MEDIUM_Arm_Top_Lower_01 ;                            
    public GameObject MEDIUM_Arm_Top_Lower_02 ;                            
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
    public GameObject MEDIUM_Shoot_FX_02      ;                            
    public GameObject MEDIUM_Shoot_FX_04      ;                       
    public GameObject MEDIUM_Torso_01         ;                    
    public GameObject MEDIUM_Weapon_01        ;                     
    public GameObject Special_effects_01c     ;                        
    public GameObject drop_shadow             ;                
    public GameObject effect_02a              ;               
	
	

	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
				
        partList["MEDIUM_Arm_Back_Lower_01"] = MEDIUM_Arm_Back_Lower_01;                           
        partList["MEDIUM_Arm_Back_Lower_02"] = MEDIUM_Arm_Back_Lower_02;                           
        partList["MEDIUM_Arm_Back_Upper_01"] = MEDIUM_Arm_Back_Upper_01;                           
        partList["MEDIUM_Arm_Top_Lower_01" ] = MEDIUM_Arm_Top_Lower_01 ;                          
        partList["MEDIUM_Arm_Top_Lower_02" ] = MEDIUM_Arm_Top_Lower_02 ;                          
        partList["MEDIUM_Arm_Top_Upper_01" ] = MEDIUM_Arm_Top_Upper_01 ;                          
        partList["MEDIUM_Head_01"          ] = MEDIUM_Head_01          ;                 
        partList["MEDIUM_Head_02"          ] = MEDIUM_Head_02          ;                 
        partList["MEDIUM_Head_03"          ] = MEDIUM_Head_03          ;                 
        partList["MEDIUM_Head_06"          ] = MEDIUM_Head_06          ;                 
        partList["MEDIUM_Head_07"          ] = MEDIUM_Head_07          ;                 
        partList["MEDIUM_Leg_Back_Lower_01"] = MEDIUM_Leg_Back_Lower_01;                           
        partList["MEDIUM_Leg_Back_Upper_01"] = MEDIUM_Leg_Back_Upper_01;                           
        partList["MEDIUM_Leg_Top_Lower_01" ] = MEDIUM_Leg_Top_Lower_01 ;                          
        partList["MEDIUM_Leg_Top_Upper_01" ] = MEDIUM_Leg_Top_Upper_01 ;                          
        partList["MEDIUM_Shoot_FX_02"      ] = MEDIUM_Shoot_FX_02      ;                          
        partList["MEDIUM_Shoot_FX_04"      ] = MEDIUM_Shoot_FX_04      ;                     
        partList["MEDIUM_Torso_01"         ] = MEDIUM_Torso_01         ;                  
        partList["MEDIUM_Weapon_01"        ] = MEDIUM_Weapon_01        ;                   
        partList["Special_effects_01c"     ] = Special_effects_01c     ;                      
        partList["drop_shadow"             ] = drop_shadow             ;              
        partList["effect_02a"              ] = effect_02a              ;             
        partList["effect_02a__1"           ] = effect_02a              ;                
        partList["effect_02a__2"           ] = effect_02a              ;                
        partList["effect_02a__3"           ] = effect_02a              ;                
        partList["effect_02a__4"           ] = effect_02a              ;                
        partList["effect_02a__5"           ] = effect_02a              ;                
        partList["effect_02a__6"           ] = effect_02a              ;                
        partList["effect_02a__7"           ] = effect_02a              ;                		
		
		
		
//partList["MEDIUM_Arm_Back_Lower_01"]=MEDIUM_Arm_Back_Lower_01;
//partList["MEDIUM_Arm_Back_Lower_02"]=MEDIUM_Arm_Back_Lower_02;
//partList["MEDIUM_Arm_Back_Upper_01"]=MEDIUM_Arm_Back_Upper_01;
//partList["MEDIUM_Arm_Top_Lower_01"]=MEDIUM_Arm_Top_Lower_01;
//partList["MEDIUM_Arm_Top_Upper_01"]=MEDIUM_Arm_Top_Upper_01;
//partList["MEDIUM_Head_01"]=MEDIUM_Head_01;
//partList["MEDIUM_Head_02"]=MEDIUM_Head_02;
//partList["MEDIUM_Head_03"]=MEDIUM_Head_03;
//partList["MEDIUM_Head_06"]=MEDIUM_Head_06;
//partList["MEDIUM_Head_07"]=MEDIUM_Head_07;
//partList["MEDIUM_Leg_Back_Lower_01"]=MEDIUM_Leg_Back_Lower_01;
//partList["MEDIUM_Leg_Back_Upper_01"]=MEDIUM_Leg_Back_Upper_01;
//partList["MEDIUM_Leg_Top_Lower_01"]=MEDIUM_Leg_Top_Lower_01;
//partList["MEDIUM_Leg_Top_Upper_01"]=MEDIUM_Leg_Top_Upper_01;
//partList["MEDIUM_Shoot_FX_02"]=MEDIUM_Shoot_FX_02;
//partList["MEDIUM_Torso_01"]=MEDIUM_Torso_01;
//partList["MEDIUM_Weapon_01"]=MEDIUM_Weapon_01;
//partList["drop_shadow"]=drop_shadow;
		
	}
}
