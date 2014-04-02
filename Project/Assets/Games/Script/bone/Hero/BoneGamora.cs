using UnityEngine;
using System.Collections;

public class BoneGamora : PieceAnimation
{              
	
	public GameObject FEMALE_Arm_Back_Lower_01;             
	public GameObject FEMALE_Arm_Back_Lower_02;             
	public GameObject FEMALE_Arm_Back_Lower_04;             
	public GameObject FEMALE_Arm_Back_Upper_01;             
	public GameObject FEMALE_Arm_Top_Lower_01;              
	public GameObject FEMALE_Arm_Top_Lower_04;       // --Lose       
	public GameObject FEMALE_Arm_Top_Upper_01;              
	public GameObject FEMALE_Head_01;                       
	public GameObject FEMALE_Head_02;                       
	public GameObject FEMALE_Head_03;                       
	public GameObject FEMALE_Head_04;                       
	public GameObject FEMALE_Leg_Back_Lower_01;             
	public GameObject FEMALE_Leg_Back_Upper_01;             
	public GameObject FEMALE_Leg_Top_Lower_01;              
	public GameObject FEMALE_Leg_Top_Upper01;               
	public GameObject FEMALE_Torso_01;                      
	public GameObject FEMALE_Weapon_01;                     
	public GameObject FEMALE_Weapon_03;                     
	public GameObject FEMALE_Weapon_04;                     
	public GameObject drop_shadow;                          
	public GameObject effect_10;                            
	public GameObject effect_10_1;                          
	public GameObject effect_11;                            
	public GameObject effect_12;                            
	public GameObject effect_13;                            
	public GameObject effect_14;                            
	public GameObject effect_15;                            
	public GameObject effect_20;                            
	public GameObject effect_21;                            
	public GameObject effect_25;                            
	public GameObject effect_28;                            
	public GameObject effect_29;                            
	public GameObject effect_3;                             
	public GameObject effect_33;                            
	public GameObject effect_34;                            
	public GameObject effect_36;                            
	public GameObject effect_38;                            
	public GameObject effect_3_1;                           
	public GameObject effect_3_2;                           
	public GameObject effect_3_3;                           
	public GameObject effect_3_4;  
	public GameObject effect_3_3_1;
	public GameObject effect_3_3_2;
	public GameObject effect_3_3_3;
	public GameObject effect_3_5;   
	public GameObject effect_4;                             
	public GameObject effect_40;                            
	public GameObject effect_4_1;                           
	public GameObject effect_4_2;     // --Lose                      
	public GameObject effect_50_2;                          
	public GameObject effect_51_2;                          
	public GameObject effect_56;                            
	public GameObject effect_6;                             
	public GameObject effect_6_0;                           
	public GameObject effect_6_1;                           
	public GameObject effect_7;                             
	public GameObject effect_7_1;                           
	public GameObject effect_7_2;     
	public GameObject effect_86;
	public GameObject effect_68;
	
	public GameObject FEMALE_Arm_Back_Lower_03;
	                          
	// Use this for initialization
	public override void Awake ()
	{
		base.Awake ();
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
		partList ["FEMALE_Arm_Back_Lower_01"] = FEMALE_Arm_Back_Lower_01;   
		partList ["FEMALE_Arm_Back_Lower_011"] = FEMALE_Arm_Back_Lower_01;  
		partList ["FEMALE_Arm_Back_Lower_02"] = FEMALE_Arm_Back_Lower_02;   
		partList ["FEMALE_Arm_Back_Lower_04"] = FEMALE_Arm_Back_Lower_04;   
		partList ["FEMALE_Arm_Back_Upper_01"] = FEMALE_Arm_Back_Upper_01;    
		partList ["FEMALE_Arm_Top_Lower_01"] = FEMALE_Arm_Top_Lower_01;     
		partList ["FEMALE_Arm_Top_Lower_04"] = FEMALE_Arm_Top_Lower_04;     
		partList ["FEMALE_Arm_Top_Upper_01"] = FEMALE_Arm_Top_Upper_01;     
		partList ["FEMALE_Head_01"] = FEMALE_Head_01;              
		partList ["FEMALE_Head_02"] = FEMALE_Head_02;              
		partList ["FEMALE_Head_03"] = FEMALE_Head_03;              
		partList ["FEMALE_Head_04"] = FEMALE_Head_04;              
		partList ["FEMALE_Leg_Back_Lower_01"] = FEMALE_Leg_Back_Lower_01;    
		partList ["FEMALE_Leg_Back_Upper_01"] = FEMALE_Leg_Back_Upper_01;    
		partList ["FEMALE_Leg_Top_Lower_01"] = FEMALE_Leg_Top_Lower_01;     
		partList ["FEMALE_Leg_Top_Upper01"] = FEMALE_Leg_Top_Upper01;      
		partList ["FEMALE_Torso_01"] = FEMALE_Torso_01;             
		partList ["FEMALE_Weapon_01"] = FEMALE_Weapon_01;            
		partList ["FEMALE_Weapon_011"] = FEMALE_Weapon_01;           
		partList ["FEMALE_Weapon_03"] = FEMALE_Weapon_03;            
		partList ["FEMALE_Weapon_035"] = FEMALE_Weapon_03;           
		partList ["FEMALE_Weapon_038"] = FEMALE_Weapon_03;           
		partList ["FEMALE_Weapon_04"] = FEMALE_Weapon_04;            
		partList ["drop_shadow"] = drop_shadow;                 
		partList ["effect_10"] = effect_10;                   
		partList ["effect_10_1"] = effect_10_1;                 
		partList ["effect_11"] = effect_11;                   
		partList ["effect_111"] = effect_11;                  
		partList ["effect_112"] = effect_11;                  
		partList ["effect_113"] = effect_11;                  
		partList ["effect_114"] = effect_11;                  
		partList ["effect_115"] = effect_11;                  
		partList ["effect_12"] = effect_12;                   
		partList ["effect_13"] = effect_13;                   
		partList ["effect_14"] = effect_14;                   
		partList ["effect_15"] = effect_15;                   
		partList ["effect_20"] = effect_20;                   
		partList ["effect_203"] = effect_20;                  
		partList ["effect_21"] = effect_21;                   
		partList ["effect_25"] = effect_25;                   
		partList ["effect_2510"] = effect_25;                 
		partList ["effect_2511"] = effect_25;                 
		partList ["effect_252"] = effect_25;                  
		partList ["effect_253"] = effect_25;                  
		partList ["effect_254"] = effect_25;                  
		partList ["effect_255"] = effect_25;                  
		partList ["effect_256"] = effect_25;                  
		partList ["effect_257"] = effect_25;                  
		partList ["effect_258"] = effect_25;                  
		partList ["effect_259"] = effect_25;                  
		partList ["effect_28"] = effect_28;                   
		partList ["effect_29"] = effect_29;                   
		partList ["effect_3"] = effect_3;                    
		partList ["effect_33"] = effect_33;                   
		partList ["effect_34"] = effect_34;                   
		partList ["effect_36"] = effect_36;                   
		partList ["effect_38"] = effect_38;                   
		partList ["effect_3812"] = effect_38;                 
		partList ["effect_3_1"] = effect_3_1;                  
		partList ["effect_3_2"] = effect_3_2;                  
		partList ["effect_3_3"] = effect_3_3;                  
		partList ["effect_3_34"] = effect_3_3;                 
		partList ["effect_3_37"] = effect_3_3;                 
		partList ["effect_3_3_1"] = effect_3_3_1;                
		partList ["effect_3_3_2"] = effect_3_3_2;                
		partList ["effect_3_3_3"] = effect_3_3_3;                
		partList ["effect_3_4"] = effect_3_4;                  
		partList ["effect_3_5"] = effect_3_5;                  
		partList ["effect_3_51"] = effect_3_5;                 
		partList ["effect_3_52"] = effect_3_5;                 
		partList ["effect_3_53"] = effect_3_5;                 
		partList ["effect_3_56"] = effect_3_5;                 
		partList ["effect_4"] = effect_4;                    
		partList ["effect_40"] = effect_40;                   
		partList ["effect_4_1"] = effect_4_1;                  
		partList ["effect_4_2"] = effect_4_2;                  
		partList ["effect_50_2"] = effect_50_2;                 
		partList ["effect_51_2"] = effect_51_2;                 
		partList ["effect_56"] = effect_56;                   
		partList ["effect_5610"] = effect_56;                 
		partList ["effect_569"] = effect_56;                  
		partList ["effect_6"] = effect_6;                    
		partList ["effect_6_0"] = effect_6_0;                  
		partList ["effect_6_1"] = effect_6_1;                  
		partList ["effect_6_12"] = effect_6_1;                 
		partList ["effect_7"] = effect_7;                    
		partList ["effect_7_1"] = effect_7_1;                  
		partList ["effect_7_2"] = effect_7_2;  
		partList ["FEMALE_Arm_Top_Lower_01__1"] = FEMALE_Arm_Top_Lower_01; 
		partList ["FEMALE_Arm_Back_Lower_03"] = FEMALE_Arm_Back_Lower_03; 
		partList ["effect_86"] = effect_86; 
		partList ["effect_68"] = effect_68;
	}
}
