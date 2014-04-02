using UnityEngine;
using System.Collections;

public class BoneDrax : PieceAnimation
{

//    public GameObject Drax_1                     ;       
//    public GameObject Drax_48                    ;        
//    public GameObject MEDIUM_Arm_Back_Lower_01   ;                         
//    public GameObject MEDIUM_Arm_Back_Lower_03   ;                         
//    public GameObject MEDIUM_Arm_Back_Lower_04   ;                         
//    public GameObject MEDIUM_Arm_Back_Upper_01   ;                         
//    public GameObject MEDIUM_Arm_Top_Lower_01    ;                        
//    public GameObject MEDIUM_Arm_Top_Lower_03    ;                        
//    public GameObject MEDIUM_Arm_Top_Lower_04    ;                        
//    public GameObject MEDIUM_Arm_Top_Upper_01    ;                        
//    public GameObject MEDIUM_Arm_Top_Upper_01_1  ;                          
//    public GameObject MEDIUM_Arm_Top_Upper_01_2  ;                          
//    public GameObject MEDIUM_Arm_Top_Upper_02    ;                        
//    public GameObject MEDIUM_Head_01             ;               
//    public GameObject MEDIUM_Head_02             ;               
//    public GameObject MEDIUM_Head_03             ;               
//    public GameObject MEDIUM_Head_05             ;               
//    public GameObject MEDIUM_Head_06             ;               
//    public GameObject MEDIUM_Head_07             ;               
//    public GameObject MEDIUM_Head_08             ;               
//    public GameObject MEDIUM_Leg_Back_Lower_01   ;                         
//    public GameObject MEDIUM_Leg_Back_Upper_01   ;                         
//    public GameObject MEDIUM_Leg_Top_Lower_01    ;                        
//    public GameObject MEDIUM_Leg_Top_Upper_01    ;                        
//    public GameObject MEDIUM_Punch_FX_02         ;                   
//    public GameObject MEDIUM_Torso_01            ;                
//    public GameObject MEDIUM_Torso_01_1          ;                  
//    public GameObject MEDIUM_Torso_01_2          ;                  
//    public GameObject MEDIUM_Torso_03            ;                
//    public GameObject MEDIUM_Weapon_01           ;                 
//    public GameObject MEDIUM_Weapon_02           ;                 
//    public GameObject drop_shadow                ;            
//    public GameObject effect_1                   ;         
//    public GameObject effect_10                  ;          
//    public GameObject effect_11                  ;          
//    public GameObject effect_12                  ;          
//    public GameObject effect_2                   ;         
//    public GameObject effect_3                   ;         
//    public GameObject effect_4                   ;         
//    public GameObject effect_5                   ;         
//    public GameObject effect_6                   ;         
//    public GameObject effect_7                   ;         
//    public GameObject effect_8                   ;         
//    public GameObject effect_9                   ;     
	
    public GameObject Drax_1                     ;            
    public GameObject Drax_3                     ;            
    public GameObject Drax_32                    ;             
    public GameObject Drax_4                     ;            
    public GameObject Drax_42                    ;             
    public GameObject Drax_48                    ;             
    public GameObject MEDIUM_Arm_Back_Lower_01   ;                              
    public GameObject MEDIUM_Arm_Back_Lower_03   ;                              
    public GameObject MEDIUM_Arm_Back_Lower_04   ;                              
    public GameObject MEDIUM_Arm_Back_Upper_01   ;                              
    public GameObject MEDIUM_Arm_Top_Lower_01    ;                             
    public GameObject MEDIUM_Arm_Top_Lower_03    ;                             
    public GameObject MEDIUM_Arm_Top_Lower_04    ;                             
    public GameObject MEDIUM_Arm_Top_Upper_01    ;                             
    public GameObject MEDIUM_Arm_Top_Upper_01_1  ;                               
    public GameObject MEDIUM_Arm_Top_Upper_01_2  ;                               
    public GameObject MEDIUM_Arm_Top_Upper_02    ;                             
    public GameObject MEDIUM_Head_01             ;                    
    public GameObject MEDIUM_Head_02             ;                    
    public GameObject MEDIUM_Head_03             ;                    
    public GameObject MEDIUM_Head_05             ;                    
    public GameObject MEDIUM_Head_06             ;                    
    public GameObject MEDIUM_Head_07             ;                    
    public GameObject MEDIUM_Head_08             ;                    
    public GameObject MEDIUM_Leg_Back_Lower_01   ;                              
    public GameObject MEDIUM_Leg_Back_Upper_01   ;                              
    public GameObject MEDIUM_Leg_Top_Lower_01    ;                             
    public GameObject MEDIUM_Leg_Top_Upper_01    ;                             
    public GameObject MEDIUM_Punch_FX_02         ;                        
    public GameObject MEDIUM_Torso_01            ;                     
    public GameObject MEDIUM_Torso_01_1          ;                       
    public GameObject MEDIUM_Torso_01_2          ;                       
    public GameObject MEDIUM_Torso_03            ;                     
    public GameObject MEDIUM_Weapon_01           ;                      
    public GameObject MEDIUM_Weapon_02           ;                      
    public GameObject drop_shadow                ;                 
    public GameObject effect_1                   ;              
    public GameObject effect_10                  ;               
    public GameObject effect_11                  ;               
    public GameObject effect_12                  ;               
    public GameObject effect_2                   ;              
    public GameObject effect_3                   ;              
    public GameObject effect_4                   ;              
    public GameObject effect_5                   ;              
    public GameObject effect_6                   ;              
    public GameObject effect_7                   ;              
    public GameObject effect_8                   ;              
    public GameObject effect_9                   ;              
	
	
	// Use this for initialization
	public override void Awake ()
	{
		base.Awake ();
	}
	
	
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
		partList ["Drax_1"                     ] = Drax_1                     ;
		partList ["Drax_3"                     ] = Drax_3                     ;
		partList ["Drax_32"                    ] = Drax_32                    ;
		partList ["Drax_4"                     ] = Drax_4                     ;
		partList ["Drax_42"                    ] = Drax_42                    ;
		partList ["Drax_48"                    ] = Drax_48                    ;
		partList ["MEDIUM_Arm_Back_Lower_01"   ] = MEDIUM_Arm_Back_Lower_01   ;
		partList ["MEDIUM_Arm_Back_Lower_01__4"] = MEDIUM_Arm_Back_Lower_01;
		partList ["MEDIUM_Arm_Back_Lower_01__8"] = MEDIUM_Arm_Back_Lower_01;
		partList ["MEDIUM_Arm_Back_Lower_03"   ] = MEDIUM_Arm_Back_Lower_03   ;
		partList ["MEDIUM_Arm_Back_Lower_04"   ] = MEDIUM_Arm_Back_Lower_04   ;
		partList ["MEDIUM_Arm_Back_Upper_01"   ] = MEDIUM_Arm_Back_Upper_01   ;
		partList ["MEDIUM_Arm_Top_Lower_01"    ] = MEDIUM_Arm_Top_Lower_01    ;
		partList ["MEDIUM_Arm_Top_Lower_01__1" ] = MEDIUM_Arm_Top_Lower_01 ;
		partList ["MEDIUM_Arm_Top_Lower_01__2" ] = MEDIUM_Arm_Top_Lower_01 ;
		partList ["MEDIUM_Arm_Top_Lower_01__3" ] = MEDIUM_Arm_Top_Lower_01 ;
		partList ["MEDIUM_Arm_Top_Lower_01__4" ] = MEDIUM_Arm_Top_Lower_01 ;
		partList ["MEDIUM_Arm_Top_Lower_03"    ] = MEDIUM_Arm_Top_Lower_03    ;
		partList ["MEDIUM_Arm_Top_Lower_04"    ] = MEDIUM_Arm_Top_Lower_04    ;
		partList ["MEDIUM_Arm_Top_Upper_01"    ] = MEDIUM_Arm_Top_Upper_01    ;
		partList ["MEDIUM_Arm_Top_Upper_01_1"  ] = MEDIUM_Arm_Top_Upper_01_1  ;
		partList ["MEDIUM_Arm_Top_Upper_01_2"  ] = MEDIUM_Arm_Top_Upper_01_2  ;
		partList ["MEDIUM_Arm_Top_Upper_01__1" ] = MEDIUM_Arm_Top_Upper_01 ;
		partList ["MEDIUM_Arm_Top_Upper_02"    ] = MEDIUM_Arm_Top_Upper_02    ;
		partList ["MEDIUM_Head_01"             ] = MEDIUM_Head_01             ;
		partList ["MEDIUM_Head_01__2"          ] = MEDIUM_Head_01          ;
		partList ["MEDIUM_Head_02"             ] = MEDIUM_Head_02             ;
		partList ["MEDIUM_Head_03"             ] = MEDIUM_Head_03             ;
		partList ["MEDIUM_Head_05"             ] = MEDIUM_Head_05             ;
		partList ["MEDIUM_Head_06"             ] = MEDIUM_Head_06             ;
		partList ["MEDIUM_Head_07"             ] = MEDIUM_Head_07             ;
		partList ["MEDIUM_Head_08"             ] = MEDIUM_Head_08             ;
		partList ["MEDIUM_Leg_Back_Lower_01"   ] = MEDIUM_Leg_Back_Lower_01   ;
		partList ["MEDIUM_Leg_Back_Upper_01"   ] = MEDIUM_Leg_Back_Upper_01   ;
		partList ["MEDIUM_Leg_Top_Lower_01"    ] = MEDIUM_Leg_Top_Lower_01    ;
		partList ["MEDIUM_Leg_Top_Upper_01"    ] = MEDIUM_Leg_Top_Upper_01    ;
		partList ["MEDIUM_Punch_FX_02"         ] = MEDIUM_Punch_FX_02         ;
		partList ["MEDIUM_Punch_FX_02__7"      ] = MEDIUM_Punch_FX_02      ;
		partList ["MEDIUM_Torso_01"            ] = MEDIUM_Torso_01            ;
		partList ["MEDIUM_Torso_01_1"          ] = MEDIUM_Torso_01_1          ;
		partList ["MEDIUM_Torso_01_2"          ] = MEDIUM_Torso_01_2          ;
		partList ["MEDIUM_Torso_03"            ] = MEDIUM_Torso_03            ;
		partList ["MEDIUM_Weapon_01"           ] = MEDIUM_Weapon_01           ;
		partList ["MEDIUM_Weapon_01__1"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__2"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__3"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__4"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__5"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__6"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_01__9"        ] = MEDIUM_Weapon_01        ;
		partList ["MEDIUM_Weapon_02"           ] = MEDIUM_Weapon_02           ;
		partList ["MEDIUM_Weapon_02__2"        ] = MEDIUM_Weapon_02        ;
		partList ["MEDIUM_Weapon_02__3"        ] = MEDIUM_Weapon_02        ;
		partList ["MEDIUM_Weapon_02__7"        ] = MEDIUM_Weapon_02        ;
		partList ["drop_shadow"                ] = drop_shadow                ;
		partList ["effect_1"                   ] = effect_1                   ;
		partList ["effect_10"                  ] = effect_10                  ;
		partList ["effect_10__7"               ] = effect_10               ;
		partList ["effect_11"                  ] = effect_11                  ;
		partList ["effect_11__2"               ] = effect_11               ;
		partList ["effect_11__3"               ] = effect_11               ;
		partList ["effect_11__4"               ] = effect_11               ;
		partList ["effect_12"                  ] = effect_12                  ;
		partList ["effect_12__5"               ] = effect_12               ;
		partList ["effect_12__6"               ] = effect_12               ;
		partList ["effect_1__1"                ] = effect_1                ;
		partList ["effect_1__2"                ] = effect_1                ;
		partList ["effect_1__3"                ] = effect_1                ;
		partList ["effect_2"                   ] = effect_2                   ;
		partList ["effect_3"                   ] = effect_3                   ;
		partList ["effect_3__3"                ] = effect_3                ;
		partList ["effect_4"                   ] = effect_4                   ;
		partList ["effect_4__5"                ] = effect_4                ;
		partList ["effect_5"                   ] = effect_5                   ;
		partList ["effect_5__1"                ] = effect_5                ;
		partList ["effect_5__2"                ] = effect_5                ;
		partList ["effect_6"                   ] = effect_6                   ;
		partList ["effect_6__5"                ] = effect_6                ;
		partList ["effect_6__6"                ] = effect_6                ;
		partList ["effect_7"                   ] = effect_7                   ;
		partList ["effect_7__2"                ] = effect_7                ;
		partList ["effect_7__3"                ] = effect_7                ;
		partList ["effect_7__4"                ] = effect_7                ;
		partList ["effect_8"                   ] = effect_8                   ;
		partList ["effect_8__7"                ] = effect_8                ;
		partList ["effect_9"                   ] = effect_9                   ;
		partList ["effect_9__1"                ] = effect_9                ;
		
//		partList ["Drax_1"                      ] = Drax_1                     ;         
//		partList ["Drax_48"                     ] = Drax_48                    ;          
//		partList ["MEDIUM_Arm_Back_Lower_01"    ] = MEDIUM_Arm_Back_Lower_01   ;                           
//		partList ["MEDIUM_Arm_Back_Lower_01__4" ] = MEDIUM_Arm_Back_Lower_01;                              
//		partList ["MEDIUM_Arm_Back_Lower_01__8" ] = MEDIUM_Arm_Back_Lower_01;                              
//		partList ["MEDIUM_Arm_Back_Lower_03"    ] = MEDIUM_Arm_Back_Lower_03   ;                           
//		partList ["MEDIUM_Arm_Back_Lower_04"    ] = MEDIUM_Arm_Back_Lower_04   ;                           
//		partList ["MEDIUM_Arm_Back_Upper_01"    ] = MEDIUM_Arm_Back_Upper_01   ;                           
//		partList ["MEDIUM_Arm_Top_Lower_01"     ] = MEDIUM_Arm_Top_Lower_01    ;                          
//		partList ["MEDIUM_Arm_Top_Lower_01__1"  ] = MEDIUM_Arm_Top_Lower_01 ;                             
//		partList ["MEDIUM_Arm_Top_Lower_01__2"  ] = MEDIUM_Arm_Top_Lower_01 ;                             
//		partList ["MEDIUM_Arm_Top_Lower_01__3"  ] = MEDIUM_Arm_Top_Lower_01 ;                             
//		partList ["MEDIUM_Arm_Top_Lower_01__4"  ] = MEDIUM_Arm_Top_Lower_01 ;                             
//		partList ["MEDIUM_Arm_Top_Lower_03"     ] = MEDIUM_Arm_Top_Lower_03    ;                          
//		partList ["MEDIUM_Arm_Top_Lower_04"     ] = MEDIUM_Arm_Top_Lower_04    ;                          
//		partList ["MEDIUM_Arm_Top_Upper_01"     ] = MEDIUM_Arm_Top_Upper_01    ;                          
//		partList ["MEDIUM_Arm_Top_Upper_01_1"   ] = MEDIUM_Arm_Top_Upper_01_1  ;                            
//		partList ["MEDIUM_Arm_Top_Upper_01_2"   ] = MEDIUM_Arm_Top_Upper_01_2  ;                            
//		partList ["MEDIUM_Arm_Top_Upper_01__1"  ] = MEDIUM_Arm_Top_Upper_01 ;                             
//		partList ["MEDIUM_Arm_Top_Upper_02"     ] = MEDIUM_Arm_Top_Upper_02    ;                          
//		partList ["MEDIUM_Head_01"              ] = MEDIUM_Head_01             ;                 
//		partList ["MEDIUM_Head_01__2"           ] = MEDIUM_Head_01          ;                    
//		partList ["MEDIUM_Head_02"              ] = MEDIUM_Head_02             ;                 
//		partList ["MEDIUM_Head_03"              ] = MEDIUM_Head_03             ;                 
//		partList ["MEDIUM_Head_05"              ] = MEDIUM_Head_05             ;                 
//		partList ["MEDIUM_Head_06"              ] = MEDIUM_Head_06             ;                 
//		partList ["MEDIUM_Head_07"              ] = MEDIUM_Head_07             ;                 
//		partList ["MEDIUM_Head_08"              ] = MEDIUM_Head_08             ;                 
//		partList ["MEDIUM_Leg_Back_Lower_01"    ] = MEDIUM_Leg_Back_Lower_01   ;                           
//		partList ["MEDIUM_Leg_Back_Upper_01"    ] = MEDIUM_Leg_Back_Upper_01   ;                           
//		partList ["MEDIUM_Leg_Top_Lower_01"     ] = MEDIUM_Leg_Top_Lower_01    ;                          
//		partList ["MEDIUM_Leg_Top_Upper_01"     ] = MEDIUM_Leg_Top_Upper_01    ;                          
//		partList ["MEDIUM_Punch_FX_02"          ] = MEDIUM_Punch_FX_02         ;                     
//		partList ["MEDIUM_Punch_FX_02__7"       ] = MEDIUM_Punch_FX_02      ;                        
//		partList ["MEDIUM_Torso_01"             ] = MEDIUM_Torso_01            ;                  
//		partList ["MEDIUM_Torso_01_1"           ] = MEDIUM_Torso_01_1          ;                    
//		partList ["MEDIUM_Torso_01_2"           ] = MEDIUM_Torso_01_2          ;                    
//		partList ["MEDIUM_Torso_03"             ] = MEDIUM_Torso_03            ;                  
//		partList ["MEDIUM_Weapon_01"            ] = MEDIUM_Weapon_01           ;                   
//		partList ["MEDIUM_Weapon_01__1"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__2"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__3"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__4"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__5"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__6"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_01__9"         ] = MEDIUM_Weapon_01        ;                      
//		partList ["MEDIUM_Weapon_02"            ] = MEDIUM_Weapon_02           ;                   
//		partList ["MEDIUM_Weapon_02__2"         ] = MEDIUM_Weapon_02        ;                      
//		partList ["MEDIUM_Weapon_02__3"         ] = MEDIUM_Weapon_02        ;                      
//		partList ["MEDIUM_Weapon_02__7"         ] = MEDIUM_Weapon_02        ;                      
//		partList ["drop_shadow"                 ] = drop_shadow                ;              
//		partList ["effect_1"                    ] = effect_1                   ;           
//		partList ["effect_10"                   ] = effect_10                  ;            
//		partList ["effect_10__7"                ] = effect_10               ;               
//		partList ["effect_11"                   ] = effect_11                  ;            
//		partList ["effect_11__2"                ] = effect_11               ;               
//		partList ["effect_11__3"                ] = effect_11               ;               
//		partList ["effect_11__4"                ] = effect_11               ;               
//		partList ["effect_12"                   ] = effect_12                  ;            
//		partList ["effect_12__5"                ] = effect_12               ;               
//		partList ["effect_12__6"                ] = effect_12               ;               
//		partList ["effect_1__1"                 ] = effect_1                ;              
//		partList ["effect_1__2"                 ] = effect_1                ;              
//		partList ["effect_1__3"                 ] = effect_1                ;              
//		partList ["effect_2"                    ] = effect_2                   ;           
//		partList ["effect_3"                    ] = effect_3                   ;           
//		partList ["effect_3__3"                 ] = effect_3                ;              
//		partList ["effect_4"                    ] = effect_4                   ;           
//		partList ["effect_4__5"                 ] = effect_4                ;              
//		partList ["effect_5"                    ] = effect_5                   ;           
//		partList ["effect_5__1"                 ] = effect_5                ;              
//		partList ["effect_5__2"                 ] = effect_5                ;              
//		partList ["effect_6"                    ] = effect_6                   ;           
//		partList ["effect_6__5"                 ] = effect_6                ;              
//		partList ["effect_6__6"                 ] = effect_6                ;              
//		partList ["effect_7"                    ] = effect_7                   ;           
//		partList ["effect_7__2"                 ] = effect_7                ;              
//		partList ["effect_7__3"                 ] = effect_7                ;              
//		partList ["effect_7__4"                 ] = effect_7                ;              
//		partList ["effect_8"                    ] = effect_8                   ;           
//		partList ["effect_8__7"                 ] = effect_8                ;              
//		partList ["effect_9"                    ] = effect_9                   ;           
//		partList ["effect_9__1"                 ] = effect_9                ;              
	}           
}               
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
