using UnityEngine;
using System.Collections;

public class BoneBug : PieceAnimation
{
//public GameObject TINY_Arm_Back_Lower_01;
//public GameObject TINY_Arm_Back_Upper_01;
//public GameObject TINY_Arm_Top_Lower_01;
//public GameObject TINY_Arm_Top_Upper_01;
//public GameObject TINY_Head_01;
//public GameObject TINY_Leg_Back_Lower_01;
//public GameObject TINY_Leg_Back_Upper_01;
//public GameObject TINY_Leg_Top_Lower_01;
//public GameObject TINY_Leg_Top_Upper_01;
//public GameObject TINY_Torso_01;
//public GameObject TINY_Weapon_01;
//public GameObject drop_shadow;
	
    public GameObject Bug_12;
    public GameObject Bug_14;
    public GameObject Special_effects_11c;
    public GameObject Special_effects_19c;
    public GameObject Special_effects_21c;
    public GameObject Special_effects_23c;
    public GameObject TINY_Arm_Back_Lower_01;
    public GameObject TINY_Arm_Back_Upper_01;
    public GameObject TINY_Arm_Top_Lower_01;
    public GameObject TINY_Arm_Top_Upper_01;
    public GameObject TINY_Head_01;
    public GameObject TINY_Leg_Back_Lower_01;
    public GameObject TINY_Leg_Back_Upper_01;
    public GameObject TINY_Leg_Top_Lower_01;
    public GameObject TINY_Leg_Top_Upper_01;
    public GameObject TINY_Torso_01;
    public GameObject TINY_Weapon_01;
    public GameObject TINY_Weapon_01_2b;
    public GameObject TINY_Weapon_01_3b;
    public GameObject TINY_Weapon_01_b;
    public GameObject TINY_Weapon_02c;
    public GameObject TINY_Weapon_10a;
    public GameObject drop_shadow;
    public GameObject effect_20;
    public GameObject effect_7_2;
	
	public override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
		partList["Bug_12"                   ] = Bug_12;
        partList["Bug_14"                   ] = Bug_14;
        partList["Special_effects_11c"      ] = Special_effects_11c;
        partList["Special_effects_19c"      ] = Special_effects_19c;
        partList["Special_effects_21c"      ] = Special_effects_21c;
        partList["Special_effects_23c"      ] = Special_effects_23c;
        partList["TINY_Arm_Back_Lower_01"   ] = TINY_Arm_Back_Lower_01;
        partList["TINY_Arm_Back_Lower_01__5"] = TINY_Arm_Back_Lower_01;
        partList["TINY_Arm_Back_Lower_01__6"] = TINY_Arm_Back_Lower_01;
        partList["TINY_Arm_Back_Upper_01"   ] = TINY_Arm_Back_Upper_01;
        partList["TINY_Arm_Back_Upper_01__7"] = TINY_Arm_Back_Upper_01;
        partList["TINY_Arm_Back_Upper_01__8"] = TINY_Arm_Back_Upper_01;
        partList["TINY_Arm_Top_Lower_01"    ] = TINY_Arm_Top_Lower_01;
        partList["TINY_Arm_Top_Lower_01__1" ] = TINY_Arm_Top_Lower_01;
        partList["TINY_Arm_Top_Upper_01"    ] = TINY_Arm_Top_Upper_01;
        partList["TINY_Arm_Top_Upper_01__1" ] = TINY_Arm_Top_Upper_01;
        partList["TINY_Arm_Top_Upper_01__2" ] = TINY_Arm_Top_Upper_01;
        partList["TINY_Head_01"             ] = TINY_Head_01;
        partList["TINY_Leg_Back_Lower_01"   ] = TINY_Leg_Back_Lower_01;
        partList["TINY_Leg_Back_Lower_01__3"] = TINY_Leg_Back_Lower_01;
        partList["TINY_Leg_Back_Lower_01__4"] = TINY_Leg_Back_Lower_01;
        partList["TINY_Leg_Back_Upper_01"   ] = TINY_Leg_Back_Upper_01;
        partList["TINY_Leg_Top_Lower_01"    ] = TINY_Leg_Top_Lower_01;
        partList["TINY_Leg_Top_Lower_01__4" ] = TINY_Leg_Top_Lower_01;
        partList["TINY_Leg_Top_Lower_01__5" ] = TINY_Leg_Top_Lower_01;
        partList["TINY_Leg_Top_Upper_01"    ] = TINY_Leg_Top_Upper_01;
        partList["TINY_Leg_Top_Upper_01__5" ] = TINY_Leg_Top_Upper_01;
        partList["TINY_Leg_Top_Upper_01__6" ] = TINY_Leg_Top_Upper_01;
        partList["TINY_Leg_Top_Upper_01__7" ] = TINY_Leg_Top_Upper_01;
        partList["TINY_Torso_01"            ] = TINY_Torso_01;
        partList["TINY_Weapon_01"           ] = TINY_Weapon_01;
        partList["TINY_Weapon_01_2b"        ] = TINY_Weapon_01_2b;
        partList["TINY_Weapon_01_3b"        ] = TINY_Weapon_01_3b;
        partList["TINY_Weapon_01__2"        ] = TINY_Weapon_01;
        partList["TINY_Weapon_01__3"        ] = TINY_Weapon_01;
        partList["TINY_Weapon_01_b"         ] = TINY_Weapon_01_b;
        partList["TINY_Weapon_02c"          ] = TINY_Weapon_02c;
        partList["TINY_Weapon_10a"          ] = TINY_Weapon_10a;
        partList["drop_shadow"              ] = drop_shadow;
        partList["effect_20"                ] = effect_20;
        partList["effect_20__1"             ] = effect_20;
        partList["effect_7_2"               ] = effect_7_2;
		
//		partList["TINY_Arm_Back_Lower_01"]=TINY_Arm_Back_Lower_01;
//partList["TINY_Arm_Back_Upper_01"]=TINY_Arm_Back_Upper_01;
//partList["TINY_Arm_Top_Lower_01"]=TINY_Arm_Top_Lower_01;
//partList["TINY_Arm_Top_Upper_01"]=TINY_Arm_Top_Upper_01;
//partList["TINY_Head_01"]=TINY_Head_01;
//partList["TINY_Leg_Back_Lower_01"]=TINY_Leg_Back_Lower_01;
//partList["TINY_Leg_Back_Upper_01"]=TINY_Leg_Back_Upper_01;
//partList["TINY_Leg_Top_Lower_01"]=TINY_Leg_Top_Lower_01;
//partList["TINY_Leg_Top_Upper_01"]=TINY_Leg_Top_Upper_01;
//partList["TINY_Torso_01"]=TINY_Torso_01;
//partList["TINY_Weapon_01"]=TINY_Weapon_01;
//partList["drop_shadow"]=drop_shadow;
	}
}
