using UnityEngine;
using System.Collections;

public class BoneBetaRayBill : PieceAnimation
{
//    public GameObject MEDIUM_Arm_Back_Lower_01   ;           
//    public GameObject MEDIUM_Arm_Back_Lower_02   ; 
//    public GameObject MEDIUM_Arm_Back_Upper_01   ; 
//    public GameObject MEDIUM_Arm_Top_Lower_01    ;
//    public GameObject MEDIUM_Arm_Top_Upper_01    ;
//    public GameObject MEDIUM_Head_01             ; 
//    public GameObject MEDIUM_Head_07             ; 
//    public GameObject MEDIUM_Leg_Back_Lower_01   ; 
//    public GameObject MEDIUM_Leg_Back_Upper_01   ; 
//    public GameObject MEDIUM_Leg_Top_Lower_01    ;
//    public GameObject MEDIUM_Leg_Top_Upper_01    ;
//    public GameObject MEDIUM_Torso_01            ;  
//    public GameObject MEDIUM_Weapon_01           ;   
//    public GameObject MEDIUM_Weapon_04           ;   
//    public GameObject MEDIUM_Weapon_05           ;   
//    public GameObject MEDIUM_Weapon_06           ;   
//    public GameObject MEDIUM_Weapon_09           ;   
//    public GameObject Medium_Accessory_Back_01   ;           
//    public GameObject drop_shadow                ;
	
    public GameObject MEDIUM_Arm_Back_Lower_01   ;                
    public GameObject MEDIUM_Arm_Back_Lower_02   ;                
    public GameObject MEDIUM_Arm_Back_Lower_06   ;          
    public GameObject MEDIUM_Arm_Back_Upper_01   ;                
    public GameObject MEDIUM_Arm_Top_Lower_01    ;               
    public GameObject MEDIUM_Arm_Top_Upper_01    ;               
    public GameObject MEDIUM_Head_01             ;      
    public GameObject MEDIUM_Head_07             ;      
    public GameObject MEDIUM_Leg_Back_Lower_01   ;                
    public GameObject MEDIUM_Leg_Back_Upper_01   ;                
    public GameObject MEDIUM_Leg_Top_Lower_01    ;               
    public GameObject MEDIUM_Leg_Top_Upper_01    ;               
    public GameObject MEDIUM_Torso_01            ;       
    public GameObject MEDIUM_Weapon_01           ;        
    public GameObject MEDIUM_Weapon_04           ;        
    public GameObject MEDIUM_Weapon_05           ;        
    public GameObject MEDIUM_Weapon_06           ;        
    public GameObject MEDIUM_Weapon_08           ;  
    public GameObject MEDIUM_Weapon_09           ;        
    public GameObject Medium_Accessory_Back_01   ;                
    public GameObject Special_effects_24c        ;    
    public GameObject Special_effects_85c        ;
	public GameObject Special_effects_17c        ;
    public GameObject drop_shadow                ;   
    public GameObject light_a01                  ;  
	
	
	public override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
        partList["MEDIUM_Arm_Back_Lower_01"   ] = MEDIUM_Arm_Back_Lower_01;                         
        partList["MEDIUM_Arm_Back_Lower_01__1"] = MEDIUM_Arm_Back_Lower_01;                            
        partList["MEDIUM_Arm_Back_Lower_01__4"] = MEDIUM_Arm_Back_Lower_01;                            
        partList["MEDIUM_Arm_Back_Lower_02"   ] = MEDIUM_Arm_Back_Lower_02;                         
        partList["MEDIUM_Arm_Back_Lower_06"   ] = MEDIUM_Arm_Back_Lower_06;                         
        partList["MEDIUM_Arm_Back_Upper_01"   ] = MEDIUM_Arm_Back_Upper_01;                         
        partList["MEDIUM_Arm_Top_Lower_01"    ] = MEDIUM_Arm_Top_Lower_01 ;                        
        partList["MEDIUM_Arm_Top_Lower_01__2" ] = MEDIUM_Arm_Top_Lower_01 ;                           
        partList["MEDIUM_Arm_Top_Upper_01"    ] = MEDIUM_Arm_Top_Upper_01 ;                        
        partList["MEDIUM_Arm_Top_Upper_01__3" ] = MEDIUM_Arm_Top_Upper_01 ;                           
        partList["MEDIUM_Head_01"             ] = MEDIUM_Head_01          ;               
        partList["MEDIUM_Head_07"             ] = MEDIUM_Head_07          ;               
        partList["MEDIUM_Head_07__1"          ] = MEDIUM_Head_07          ;                  
        partList["MEDIUM_Leg_Back_Lower_01"   ] = MEDIUM_Leg_Back_Lower_01;                         
        partList["MEDIUM_Leg_Back_Upper_01"   ] = MEDIUM_Leg_Back_Upper_01;                         
        partList["MEDIUM_Leg_Top_Lower_01"    ] = MEDIUM_Leg_Top_Lower_01 ;                        
        partList["MEDIUM_Leg_Top_Upper_01"    ] = MEDIUM_Leg_Top_Upper_01 ;                        
        partList["MEDIUM_Torso_01"            ] = MEDIUM_Torso_01         ;                
        partList["MEDIUM_Weapon_01"           ] = MEDIUM_Weapon_01        ;                 
        partList["MEDIUM_Weapon_01__1"        ] = MEDIUM_Weapon_01        ;                    
        partList["MEDIUM_Weapon_01__5"        ] = MEDIUM_Weapon_01        ;                    
        partList["MEDIUM_Weapon_04"           ] = MEDIUM_Weapon_04        ;                 
        partList["MEDIUM_Weapon_05"           ] = MEDIUM_Weapon_05        ;                 
        partList["MEDIUM_Weapon_06"           ] = MEDIUM_Weapon_06        ;                 
        partList["MEDIUM_Weapon_08"           ] = MEDIUM_Weapon_08        ;                 
        partList["MEDIUM_Weapon_08__1"        ] = MEDIUM_Weapon_08        ;                    
        partList["MEDIUM_Weapon_08__2"        ] = MEDIUM_Weapon_08        ;                    
        partList["MEDIUM_Weapon_09"           ] = MEDIUM_Weapon_09        ;                 
        partList["Medium_Accessory_Back_01"   ] = Medium_Accessory_Back_01;                         
        partList["Special_effects_24c"        ] = Special_effects_24c     ;                    
        partList["Special_effects_85c"        ] = Special_effects_85c     ;
		partList["Special_effects_17c"        ] = Special_effects_17c     ;
        partList["drop_shadow"                ] = drop_shadow             ;            
        partList["light_a01"                  ] = light_a01               ;          
		
		
		
//        partList["MEDIUM_Arm_Back_Lower_01"   ] = MEDIUM_Arm_Back_Lower_01;                 
//        partList["MEDIUM_Arm_Back_Lower_01__1"] = MEDIUM_Arm_Back_Lower_01;               
//        partList["MEDIUM_Arm_Back_Lower_01__4"] = MEDIUM_Arm_Back_Lower_01;                   
//        partList["MEDIUM_Arm_Back_Lower_02"   ] = MEDIUM_Arm_Back_Lower_02;                
//        partList["MEDIUM_Arm_Back_Upper_01"   ] = MEDIUM_Arm_Back_Upper_01;                
//        partList["MEDIUM_Arm_Top_Lower_01"    ] = MEDIUM_Arm_Top_Lower_01 ;               
//        partList["MEDIUM_Arm_Top_Lower_01__2" ] = MEDIUM_Arm_Top_Lower_01 ;                  
//        partList["MEDIUM_Arm_Top_Upper_01"    ] = MEDIUM_Arm_Top_Upper_01 ;               
//        partList["MEDIUM_Arm_Top_Upper_01__3" ] = MEDIUM_Arm_Top_Upper_01 ;                  
//        partList["MEDIUM_Head_01"             ] = MEDIUM_Head_01          ;      
//        partList["MEDIUM_Head_07"             ] = MEDIUM_Head_07          ;      
//        partList["MEDIUM_Head_07__1"          ] = MEDIUM_Head_07          ;         
//        partList["MEDIUM_Leg_Back_Lower_01"   ] = MEDIUM_Leg_Back_Lower_01;                
//        partList["MEDIUM_Leg_Back_Upper_01"   ] = MEDIUM_Leg_Back_Upper_01;                
//        partList["MEDIUM_Leg_Top_Lower_01"    ] = MEDIUM_Leg_Top_Lower_01 ;               
//        partList["MEDIUM_Leg_Top_Upper_01"    ] = MEDIUM_Leg_Top_Upper_01 ;               
//        partList["MEDIUM_Torso_01"            ] = MEDIUM_Torso_01         ;       
//        partList["MEDIUM_Weapon_01"           ] = MEDIUM_Weapon_01        ;        
//        partList["MEDIUM_Weapon_01__1"        ] = MEDIUM_Weapon_01        ;           
//        partList["MEDIUM_Weapon_01__5"        ] = MEDIUM_Weapon_01        ;           
//        partList["MEDIUM_Weapon_04"           ] = MEDIUM_Weapon_04        ;           
//        partList["MEDIUM_Weapon_05"           ] = MEDIUM_Weapon_05        ;            
//        partList["MEDIUM_Weapon_06"           ] = MEDIUM_Weapon_06        ;           
//        partList["MEDIUM_Weapon_09"           ] = MEDIUM_Weapon_09        ;       
//        partList["Medium_Accessory_Back_01"   ] = Medium_Accessory_Back_01;                
//        partList["drop_shadow"                ] = drop_shadow             ;   		
	}
}
