using UnityEngine;
using System.Collections;

public class BoneHulk : PieceAnimation
{
	public GameObject LARGE_Arm_Back_Lower_01;
public GameObject LARGE_Arm_Back_Lower_02;
public GameObject LARGE_Arm_Back_Lower_03;
public GameObject LARGE_Arm_Back_Upper_01;
public GameObject LARGE_Arm_Top_Lower_01;
public GameObject LARGE_Arm_Top_Lower_02;
public GameObject LARGE_Arm_Top_Lower_03;
public GameObject LARGE_Arm_Top_Upper_01;
public GameObject LARGE_Arm_Top_Upper_02;
public GameObject LARGE_Arm_Top_Upper_03;
public GameObject LARGE_Head_01;
public GameObject LARGE_Head_02;
public GameObject LARGE_Head_03;
public GameObject LARGE_Head_04;
public GameObject LARGE_Leg_Back_Lower_01;
public GameObject LARGE_Leg_Back_Upper_01;
public GameObject LARGE_Leg_Top_Lower_01;
public GameObject LARGE_Leg_Top_Upper_01;
public GameObject LARGE_Torso_01;
public GameObject LARGE_Torso_02;
public GameObject drop_shadow;
	public override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		partList["LARGE_Arm_Back_Lower_01"]=LARGE_Arm_Back_Lower_01;
partList["LARGE_Arm_Back_Lower_02"]=LARGE_Arm_Back_Lower_02;
partList["LARGE_Arm_Back_Lower_03"]=LARGE_Arm_Back_Lower_03;
partList["LARGE_Arm_Back_Upper_01"]=LARGE_Arm_Back_Upper_01;
partList["LARGE_Arm_Top_Lower_01"]=LARGE_Arm_Top_Lower_01;
partList["LARGE_Arm_Top_Lower_02"]=LARGE_Arm_Top_Lower_02;
partList["LARGE_Arm_Top_Lower_03"]=LARGE_Arm_Top_Lower_03;
partList["LARGE_Arm_Top_Upper_01"]=LARGE_Arm_Top_Upper_01;
partList["LARGE_Arm_Top_Upper_02"]=LARGE_Arm_Top_Upper_02;
partList["LARGE_Arm_Top_Upper_03"]=LARGE_Arm_Top_Upper_03;
partList["LARGE_Head_01"]=LARGE_Head_01;
partList["LARGE_Head_02"]=LARGE_Head_02;
partList["LARGE_Head_03"]=LARGE_Head_03;
partList["LARGE_Head_04"]=LARGE_Head_04;
partList["LARGE_Leg_Back_Lower_01"]=LARGE_Leg_Back_Lower_01;
partList["LARGE_Leg_Back_Upper_01"]=LARGE_Leg_Back_Upper_01;
partList["LARGE_Leg_Top_Lower_01"]=LARGE_Leg_Top_Lower_01;
partList["LARGE_Leg_Top_Upper_01"]=LARGE_Leg_Top_Upper_01;
partList["LARGE_Torso_01"]=LARGE_Torso_01;
partList["LARGE_Torso_02"]=LARGE_Torso_02;
partList["drop_shadow"]=drop_shadow;
	}
}
