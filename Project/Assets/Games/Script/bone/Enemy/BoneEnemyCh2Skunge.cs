using UnityEngine;
using System.Collections;

public class BoneEnemyCh2Skunge : PieceAnimation {
	
//    public GameObject TINY_Arm_Back_Lower_01  ;              
//    public GameObject TINY_Arm_Back_Lower_02  ;              
//    public GameObject TINY_Arm_Back_Upper_01  ;              
//    public GameObject TINY_Arm_Top_Lower_01   ;             
//    public GameObject TINY_Arm_Top_Lower_02   ;             
//    public GameObject TINY_Arm_Top_Lower_03   ;             
//    public GameObject TINY_Arm_Top_Upper_01   ;             
//    public GameObject TINY_Head_01            ;    
//    public GameObject TINY_Head_01n           ;     
//    public GameObject TINY_Head_02            ;    
//    public GameObject TINY_Head_03            ;    
//    public GameObject TINY_Head_04            ;    
//    public GameObject TINY_Head_05            ;    
//    public GameObject TINY_Leg_Back_Lower_01  ;              
//    public GameObject TINY_Leg_Back_Upper_01  ;              
//    public GameObject TINY_Leg_Top_Lower_01   ;             
//    public GameObject TINY_Leg_Top_Upper_01   ;             
//    public GameObject TINY_Torso_01           ;     
//    public GameObject TINY_Weapon_01          ;      
//    public GameObject TINY_Weapon_02          ;      
//    public GameObject drop_shadow             ;  
	
    public GameObject TINY_Arm_Back_Lower_01   ;             
    public GameObject TINY_Arm_Back_Lower_02   ;             
    public GameObject TINY_Arm_Back_Lower_05   ;             
    public GameObject TINY_Arm_Back_Upper_01   ;             
    public GameObject TINY_Arm_Top_Lower_01    ;            
    public GameObject TINY_Arm_Top_Lower_02    ;            
    public GameObject TINY_Arm_Top_Lower_03    ;            
    public GameObject TINY_Arm_Top_Lower_08    ;            
    public GameObject TINY_Arm_Top_Upper_01    ;            
    public GameObject TINY_Head_01             ;   
    public GameObject TINY_Head_01n            ;    
    public GameObject TINY_Head_02             ;   
    public GameObject TINY_Head_03             ;   
    public GameObject TINY_Head_04             ;   
    public GameObject TINY_Head_05             ;   
    public GameObject TINY_Head_06             ;   
    public GameObject TINY_Leg_Back_Lower_01   ;             
    public GameObject TINY_Leg_Back_Upper_01   ;             
    public GameObject TINY_Leg_Top_Lower_01    ;            
    public GameObject TINY_Leg_Top_Upper_01    ;            
    public GameObject TINY_Torso_01            ;    
    public GameObject TINY_Weapon_01           ;     
    public GameObject TINY_Weapon_02           ;     
    public GameObject TINY_Weapon_06           ;     
    public GameObject drop_shadow              ;  
    public GameObject flash_c                  ;
    public GameObject light_c01                ;
    public GameObject light_c04                ;
	
	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
        partList["TINY_Arm_Back_Lower_01"   ] = TINY_Arm_Back_Lower_01   ;              
        partList["TINY_Arm_Back_Lower_01__3"] = TINY_Arm_Back_Lower_01   ;                 
        partList["TINY_Arm_Back_Lower_01__4"] = TINY_Arm_Back_Lower_01   ;                 
        partList["TINY_Arm_Back_Lower_02"   ] = TINY_Arm_Back_Lower_02   ;              
        partList["TINY_Arm_Back_Lower_05"   ] = TINY_Arm_Back_Lower_05   ;              
        partList["TINY_Arm_Back_Upper_01"   ] = TINY_Arm_Back_Upper_01   ;              
        partList["TINY_Arm_Back_Upper_01__5"] = TINY_Arm_Back_Upper_01   ;                 
        partList["TINY_Arm_Top_Lower_01"    ] = TINY_Arm_Top_Lower_01    ;             
        partList["TINY_Arm_Top_Lower_01__1" ] = TINY_Arm_Top_Lower_01    ;                
        partList["TINY_Arm_Top_Lower_01__3" ] = TINY_Arm_Top_Lower_01    ;                
        partList["TINY_Arm_Top_Lower_02"    ] = TINY_Arm_Top_Lower_02    ;             
        partList["TINY_Arm_Top_Lower_03"    ] = TINY_Arm_Top_Lower_03    ;             
        partList["TINY_Arm_Top_Lower_08"    ] = TINY_Arm_Top_Lower_08    ;             
        partList["TINY_Arm_Top_Upper_01"    ] = TINY_Arm_Top_Upper_01    ;             
        partList["TINY_Head_01"             ] = TINY_Head_01             ;    
        partList["TINY_Head_01n"            ] = TINY_Head_01n            ;     
        partList["TINY_Head_02"             ] = TINY_Head_02             ;    
        partList["TINY_Head_03"             ] = TINY_Head_03             ;    
        partList["TINY_Head_04"             ] = TINY_Head_04             ;    
        partList["TINY_Head_05"             ] = TINY_Head_05             ;    
        partList["TINY_Head_06"             ] = TINY_Head_06             ;    
        partList["TINY_Leg_Back_Lower_01"   ] = TINY_Leg_Back_Lower_01   ;              
        partList["TINY_Leg_Back_Upper_01"   ] = TINY_Leg_Back_Upper_01   ;              
        partList["TINY_Leg_Top_Lower_01"    ] = TINY_Leg_Top_Lower_01    ;             
        partList["TINY_Leg_Top_Lower_01__1" ] = TINY_Leg_Top_Lower_01    ;                
        partList["TINY_Leg_Top_Upper_01"    ] = TINY_Leg_Top_Upper_01    ;             
        partList["TINY_Torso_01"            ] = TINY_Torso_01            ;     
        partList["TINY_Weapon_01"           ] = TINY_Weapon_01           ;      
        partList["TINY_Weapon_01__1"        ] = TINY_Weapon_01           ;         
        partList["TINY_Weapon_01__4"        ] = TINY_Weapon_01           ;         
        partList["TINY_Weapon_02"           ] = TINY_Weapon_02           ;      
        partList["TINY_Weapon_02__2"        ] = TINY_Weapon_02           ;         
        partList["TINY_Weapon_06"           ] = TINY_Weapon_06           ;      
        partList["TINY_Weapon_06__2"        ] = TINY_Weapon_06           ;         
        partList["drop_shadow"              ] = drop_shadow              ;   
        partList["flash_c"                  ] = flash_c                  ;  
        partList["light_c01"                ] = light_c01                ; 
        partList["light_c01__1"             ] = light_c01                ;    
        partList["light_c04"                ] = light_c04                ; 
        partList["light_c04__1"             ] = light_c04                ;    
        partList["light_c04__2"             ] = light_c04                ;    
        partList["light_c04__5"             ] = light_c04                ;    		
		
//        partList["TINY_Arm_Back_Lower_01"  ] = TINY_Arm_Back_Lower_01  ;                
//        partList["TINY_Arm_Back_Lower_02"  ] = TINY_Arm_Back_Lower_02  ;                
//        partList["TINY_Arm_Back_Upper_01"  ] = TINY_Arm_Back_Upper_01  ;                
//        partList["TINY_Arm_Top_Lower_01"   ] = TINY_Arm_Top_Lower_01   ;               
//        partList["TINY_Arm_Top_Lower_01__1"] = TINY_Arm_Top_Lower_01   ;                  
//        partList["TINY_Arm_Top_Lower_02"   ] = TINY_Arm_Top_Lower_02   ;               
//        partList["TINY_Arm_Top_Lower_03"   ] = TINY_Arm_Top_Lower_03   ;               
//        partList["TINY_Arm_Top_Upper_01"   ] = TINY_Arm_Top_Upper_01   ;               
//        partList["TINY_Head_01"            ] = TINY_Head_01            ;      
//        partList["TINY_Head_01n"           ] = TINY_Head_01n           ;       
//        partList["TINY_Head_02"            ] = TINY_Head_02            ;      
//        partList["TINY_Head_03"            ] = TINY_Head_03            ;      
//        partList["TINY_Head_04"            ] = TINY_Head_04            ;      
//        partList["TINY_Head_05"            ] = TINY_Head_05            ;      
//        partList["TINY_Leg_Back_Lower_01"  ] = TINY_Leg_Back_Lower_01  ;                
//        partList["TINY_Leg_Back_Upper_01"  ] = TINY_Leg_Back_Upper_01  ;                
//        partList["TINY_Leg_Top_Lower_01"   ] = TINY_Leg_Top_Lower_01   ;               
//        partList["TINY_Leg_Top_Lower_01__1"] = TINY_Leg_Top_Lower_01   ;                  
//        partList["TINY_Leg_Top_Upper_01"   ] = TINY_Leg_Top_Upper_01   ;               
//        partList["TINY_Torso_01"           ] = TINY_Torso_01           ;       
//        partList["TINY_Weapon_01"          ] = TINY_Weapon_01          ;        
//        partList["TINY_Weapon_02"          ] = TINY_Weapon_02          ;        
//        partList["TINY_Weapon_02__2"       ] = TINY_Weapon_02          ;           
//        partList["drop_shadow"             ] = drop_shadow             ;     		
	}
}
