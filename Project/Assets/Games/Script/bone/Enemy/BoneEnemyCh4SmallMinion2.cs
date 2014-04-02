using UnityEngine;
using System.Collections;

public class BoneEnemyCh4SmallMinion2 : PieceAnimation {
	
	public GameObject SMALL_Arm_Back_Lower_01;
	public GameObject SMALL_Arm_Back_Upper_01;
	public GameObject SMALL_Arm_Top_Lower_01;
	public GameObject SMALL_Arm_Top_Upper_01;
	public GameObject SMALL_Head_01;
	public GameObject SMALL_Head_02;
	public GameObject SMALL_Leg_Back_Lower_01;
	public GameObject SMALL_Leg_Back_Upper_01;
	public GameObject SMALL_Leg_Top_Lower_01;
	public GameObject SMALL_Leg_Top_Upper_01;
	public GameObject SMALL_Torso_01;
	public GameObject SMALL_Torso_03;
	public GameObject drop_shadow;
	
	public override void Awake ()
	{
		base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
				
		partList["SMALL_Arm_Back_Lower_01"]=SMALL_Arm_Back_Lower_01;
		partList["SMALL_Arm_Back_Upper_01"]=SMALL_Arm_Back_Upper_01;
		partList["SMALL_Arm_Top_Lower_01"]=SMALL_Arm_Top_Lower_01;
		partList["SMALL_Arm_Top_Upper_01"]=SMALL_Arm_Top_Upper_01;
		partList["SMALL_Head_01"]=SMALL_Head_01;
		partList["SMALL_Head_02"]=SMALL_Head_02;
		partList["SMALL_Leg_Back_Lower_01"]=SMALL_Leg_Back_Lower_01;
		partList["SMALL_Leg_Back_Upper_01"]=SMALL_Leg_Back_Upper_01;
		partList["SMALL_Leg_Top_Lower_01"]=SMALL_Leg_Top_Lower_01;
		partList["SMALL_Leg_Top_Upper_01"]=SMALL_Leg_Top_Upper_01;
		partList["SMALL_Torso_01"]=SMALL_Torso_01;
		partList["SMALL_Torso_03"]=SMALL_Torso_03;
		partList["drop_shadow"]=drop_shadow;
	}
}
