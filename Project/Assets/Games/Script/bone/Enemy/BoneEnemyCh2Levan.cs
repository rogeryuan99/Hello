using UnityEngine;
using System.Collections;

public class BoneEnemyCh2Levan : PieceAnimation {
	
//	public GameObject SMALL_Arm_Back_Lower_01  ;                   
//	public GameObject SMALL_Arm_Back_Upper_01  ;                     
//	public GameObject SMALL_Arm_Top_Lower_01   ;                    
//	public GameObject SMALL_Arm_Top_Lower_03   ;                    
//	public GameObject SMALL_Arm_Top_Upper_01   ;                    
//	public GameObject SMALL_Head_01            ;           
//	public GameObject SMALL_Head_02            ;           
//	public GameObject SMALL_Head_03            ;           
//	public GameObject SMALL_Head_04            ;           
//	public GameObject SMALL_Leg_Back_Lower_01  ;                     
//	public GameObject SMALL_Leg_Back_Upper_01  ;                     
//	public GameObject SMALL_Leg_Top_Lower_01   ;                    
//	public GameObject SMALL_Leg_Top_Upper_01   ;                    
//	public GameObject SMALL_Torso_01           ;            
//	public GameObject SMALL_Torso_02           ;            
//	public GameObject SMALL_Weapon_01          ;             
//	public GameObject SMALL_Weapon_02c         ;              
//	public GameObject SMALL_Weapon_03c         ;              
//	public GameObject SMALL_Weapon_04c         ;              
//	public GameObject Special_effects_01c      ;                 
//	public GameObject Special_effects_18c      ;                 
//	public GameObject Special_effects_27c      ;                 
//	public GameObject Special_effects_28c      ;                 
//	public GameObject drop_shadow              ; 
	
  	public GameObject SMALL_Arm_Back_Lower_01   ;                     
  	public GameObject SMALL_Arm_Back_Upper_01   ;                     
  	public GameObject SMALL_Arm_Top_Lower_01    ;                    
  	public GameObject SMALL_Arm_Top_Lower_03    ;                    
  	public GameObject SMALL_Arm_Top_Upper_01    ;                    
  	public GameObject SMALL_Head_01             ;           
  	public GameObject SMALL_Head_02             ;           
  	public GameObject SMALL_Head_03             ;           
  	public GameObject SMALL_Head_04             ;           
  	public GameObject SMALL_Leg_Back_Lower_01   ;                     
  	public GameObject SMALL_Leg_Back_Upper_01   ;                     
  	public GameObject SMALL_Leg_Top_Lower_01    ;                    
  	public GameObject SMALL_Leg_Top_Upper_01    ;                    
  	public GameObject SMALL_Torso_01            ;            
  	public GameObject SMALL_Torso_02            ;            
  	public GameObject SMALL_Weapon_01           ;             
  	public GameObject SMALL_Weapon_02b          ;              
  	public GameObject SMALL_Weapon_02c          ;              
  	public GameObject SMALL_Weapon_03c          ;              
  	public GameObject SMALL_Weapon_04c          ;              
  	public GameObject Special_effects_01c       ;                 
  	public GameObject Special_effects_18c       ;                 
  	public GameObject Special_effects_27c       ;                 
  	public GameObject Special_effects_28c       ;                 
  	public GameObject drop_shadow               ;         	
	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
			
		partList["SMALL_Arm_Back_Lower_01"   ] = SMALL_Arm_Back_Lower_01   ;                     
		partList["SMALL_Arm_Back_Lower_01__2"] = SMALL_Arm_Back_Lower_01   ;                        
		partList["SMALL_Arm_Back_Upper_01"   ] = SMALL_Arm_Back_Upper_01   ;                     
		partList["SMALL_Arm_Top_Lower_01"    ] = SMALL_Arm_Top_Lower_01    ;                    
		partList["SMALL_Arm_Top_Lower_01__1" ] = SMALL_Arm_Top_Lower_01    ;                       
		partList["SMALL_Arm_Top_Lower_03"    ] = SMALL_Arm_Top_Lower_03    ;                    
		partList["SMALL_Arm_Top_Upper_01"    ] = SMALL_Arm_Top_Upper_01    ;                    
		partList["SMALL_Head_01"             ] = SMALL_Head_01             ;           
		partList["SMALL_Head_02"             ] = SMALL_Head_02             ;           
		partList["SMALL_Head_03"             ] = SMALL_Head_03             ;           
		partList["SMALL_Head_04"             ] = SMALL_Head_04             ;           
		partList["SMALL_Leg_Back_Lower_01"   ] = SMALL_Leg_Back_Lower_01   ;                     
		partList["SMALL_Leg_Back_Upper_01"   ] = SMALL_Leg_Back_Upper_01   ;                     
		partList["SMALL_Leg_Top_Lower_01"    ] = SMALL_Leg_Top_Lower_01    ;                    
		partList["SMALL_Leg_Top_Upper_01"    ] = SMALL_Leg_Top_Upper_01    ;                    
		partList["SMALL_Torso_01"            ] = SMALL_Torso_01            ;            
		partList["SMALL_Torso_02"            ] = SMALL_Torso_02            ;            
		partList["SMALL_Weapon_01"           ] = SMALL_Weapon_01           ;             
		partList["SMALL_Weapon_02b"          ] = SMALL_Weapon_02b          ;              
		partList["SMALL_Weapon_02c"          ] = SMALL_Weapon_02c          ;              
		partList["SMALL_Weapon_03c"          ] = SMALL_Weapon_03c          ;              
		partList["SMALL_Weapon_04c"          ] = SMALL_Weapon_04c          ;              
		partList["Special_effects_01c"       ] = Special_effects_01c       ;                 
		partList["Special_effects_18c"       ] = Special_effects_18c       ;                 
		partList["Special_effects_27c"       ] = Special_effects_27c       ;                 
		partList["Special_effects_28c"       ] = Special_effects_28c       ;                 
		partList["Special_effects_28c__1"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__2"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__3"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__4"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__5"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__6"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__7"    ] = Special_effects_28c       ;                    
		partList["Special_effects_28c__8"    ] = Special_effects_28c       ;                    
		partList["drop_shadow"               ] = drop_shadow               ;          		
		
//		partList["SMALL_Arm_Back_Lower_01"  ] = SMALL_Arm_Back_Lower_01  ;                 
//		partList["SMALL_Arm_Back_Upper_01"  ] = SMALL_Arm_Back_Upper_01  ;                 
//		partList["SMALL_Arm_Top_Lower_01"   ] = SMALL_Arm_Top_Lower_01   ;                
//		partList["SMALL_Arm_Top_Lower_01__1"] = SMALL_Arm_Top_Lower_01   ;                   
//		partList["SMALL_Arm_Top_Lower_03"   ] = SMALL_Arm_Top_Lower_03   ;                
//		partList["SMALL_Arm_Top_Upper_01"   ] = SMALL_Arm_Top_Upper_01   ;                
//		partList["SMALL_Head_01"            ] = SMALL_Head_01            ;       
//		partList["SMALL_Head_02"            ] = SMALL_Head_02            ;       
//		partList["SMALL_Head_03"            ] = SMALL_Head_03            ;       
//		partList["SMALL_Head_04"            ] = SMALL_Head_04            ;       
//		partList["SMALL_Leg_Back_Lower_01"  ] = SMALL_Leg_Back_Lower_01  ;                 
//		partList["SMALL_Leg_Back_Upper_01"  ] = SMALL_Leg_Back_Upper_01  ;                 
//		partList["SMALL_Leg_Top_Lower_01"   ] = SMALL_Leg_Top_Lower_01   ;                
//		partList["SMALL_Leg_Top_Upper_01"   ] = SMALL_Leg_Top_Upper_01   ;                
//		partList["SMALL_Torso_01"           ] = SMALL_Torso_01           ;        
//		partList["SMALL_Torso_02"           ] = SMALL_Torso_02           ;        
//		partList["SMALL_Weapon_01"          ] = SMALL_Weapon_01          ;         
//		partList["SMALL_Weapon_02c"         ] = SMALL_Weapon_02c         ;          
//		partList["SMALL_Weapon_03c"         ] = SMALL_Weapon_03c         ;          
//		partList["SMALL_Weapon_04c"         ] = SMALL_Weapon_04c         ;          
//		partList["Special_effects_01c"      ] = Special_effects_01c      ;             
//		partList["Special_effects_18c"      ] = Special_effects_18c      ;             
//		partList["Special_effects_27c"      ] = Special_effects_27c      ;             
//		partList["Special_effects_28c"      ] = Special_effects_28c      ;             
//		partList["Special_effects_28c__1"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__2"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__3"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__4"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__5"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__6"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__7"   ] = Special_effects_28c      ;                
//		partList["Special_effects_28c__8"   ] = Special_effects_28c      ;                
//		partList["drop_shadow"              ] = drop_shadow              ;  
	}
}
