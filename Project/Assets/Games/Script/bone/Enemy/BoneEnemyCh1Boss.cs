using UnityEngine;
using System.Collections;

public class BoneEnemyCh1Boss : PieceAnimation {

	public GameObject TINY_Arm_Back_Lower_01;
	public GameObject TINY_Arm_Back_Upper_01;
	public GameObject TINY_Arm_Top_Lower_01;
	public GameObject TINY_Arm_Top_Lower_02;
	public GameObject TINY_Arm_Top_Upper_01;
	public GameObject TINY_Head_01;
	public GameObject TINY_Head_03;
	public GameObject TINY_Head_06;
	public GameObject TINY_Leg_Back_Lower_01;
	public GameObject TINY_Leg_Back_Lower_02;
	public GameObject TINY_Leg_Back_Upper_01;
	public GameObject TINY_Leg_Top_Lower_01;
	public GameObject TINY_Leg_Top_Lower_02;
	public GameObject TINY_Leg_Top_Upper_01;
	public GameObject TINY_Torso_01;
	public GameObject TINY_Weapon_01;
	public GameObject drop_shadow;
	
	public override void Awake (){
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["TINY_Arm_Back_Lower_01"] = TINY_Arm_Back_Lower_01;
		partList["TINY_Arm_Back_Upper_01"] = TINY_Arm_Back_Upper_01;
		partList["TINY_Arm_Top_Lower_01"] = TINY_Arm_Top_Lower_01;
		partList["TINY_Arm_Top_Lower_02"] = TINY_Arm_Top_Lower_02;
		partList["TINY_Arm_Top_Upper_01"] = TINY_Arm_Top_Upper_01;
		partList["TINY_Head_01"] = TINY_Head_01;
		partList["TINY_Head_03"] = TINY_Head_03;
		partList["TINY_Head_06"] = TINY_Head_06;
		partList["TINY_Leg_Back_Lower_01"] = TINY_Leg_Back_Lower_01;
		partList["TINY_Leg_Back_Lower_02"] = TINY_Leg_Back_Lower_02;
		partList["TINY_Leg_Back_Upper_01"] = TINY_Leg_Back_Upper_01;
		partList["TINY_Leg_Top_Lower_01"]  = TINY_Leg_Top_Lower_01;
		partList["TINY_Leg_Top_Lower_02"]  = TINY_Leg_Top_Lower_02;
		partList["TINY_Leg_Top_Upper_01"]  = TINY_Leg_Top_Upper_01;
		partList["TINY_Torso_01"]   = TINY_Torso_01;
		partList["TINY_Weapon_01"]  = TINY_Weapon_01;
		partList["drop_shadow"]=drop_shadow;  
	}
}
