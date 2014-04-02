using UnityEngine;
using System.Collections;

public class BoneIronMan : PieceAnimation
{
public GameObject SMALL_Arm_Back_Lower_01;
public GameObject SMALL_Arm_Back_Lower_02;
public GameObject SMALL_Arm_Back_Upper_01;
public GameObject SMALL_Arm_Top_Lower_01;
public GameObject SMALL_Arm_Top_Lower_02;
public GameObject SMALL_Arm_Top_Lower_03;
public GameObject SMALL_Arm_Top_Upper_01;
public GameObject SMALL_Arm_Top_Upper_02;
public GameObject SMALL_Head_01;
public GameObject SMALL_Head_02;
public GameObject SMALL_Leg_Back_Lower_01;
public GameObject SMALL_Leg_Back_Upper_01;
public GameObject SMALL_Leg_Top_Lower_01;
public GameObject SMALL_Leg_Top_Upper_01;
public GameObject SMALL_Torso_01;
public GameObject SMALL_Torso_02;
public GameObject drop_shadow;
	
public GameObject SMALL_boost01;
public GameObject SMALL_boost02;
public GameObject SMALL_boost03;
public GameObject SMALL_Dust_FX;
	
	public override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
partList["SMALL_Arm_Back_Lower_01"]=SMALL_Arm_Back_Lower_01;
partList["SMALL_Arm_Back_Lower_02"]=SMALL_Arm_Back_Lower_02;
partList["SMALL_Arm_Back_Upper_01"]=SMALL_Arm_Back_Upper_01;
partList["SMALL_Arm_Top_Lower_01"]=SMALL_Arm_Top_Lower_01;
partList["SMALL_Arm_Top_Lower_02"]=SMALL_Arm_Top_Lower_02;
partList["SMALL_Arm_Top_Lower_03"]=SMALL_Arm_Top_Lower_03;
partList["SMALL_Arm_Top_Upper_01"]=SMALL_Arm_Top_Upper_01;
partList["SMALL_Arm_Top_Upper_02"]=SMALL_Arm_Top_Upper_02;
partList["SMALL_Head_01"]=SMALL_Head_01;
partList["SMALL_Head_02"]=SMALL_Head_02;
partList["SMALL_Leg_Back_Lower_01"]=SMALL_Leg_Back_Lower_01;
partList["SMALL_Leg_Back_Upper_01"]=SMALL_Leg_Back_Upper_01;
partList["SMALL_Leg_Top_Lower_01"]=SMALL_Leg_Top_Lower_01;
partList["SMALL_Leg_Top_Upper_01"]=SMALL_Leg_Top_Upper_01;
partList["SMALL_Torso_01"]=SMALL_Torso_01;
partList["SMALL_Torso_02"]=SMALL_Torso_02;
partList["drop_shadow"]=drop_shadow;
		

		partList["SMALL_boost01"]=SMALL_boost01;
		partList["SMALL_boost01__1"]=SMALL_boost01;
partList["SMALL_boost02"]=SMALL_boost02;
		partList["SMALL_boost02__2"]=SMALL_boost02;
partList["SMALL_boost03"]=SMALL_boost03;
		partList["SMALL_boost03__3"]=SMALL_boost03;
partList["SMALL_Dust_FX"]=SMALL_Dust_FX;
	}
}
