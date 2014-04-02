using UnityEngine;
using System.Collections;

public class BoneSTARLORD5B_ShieldGenerator : PieceAnimation {
	
	
	public GameObject EnergySmall_w;
  	public GameObject Light_g2a_w;
  	public GameObject box_a_w;
  	public GameObject box_b_w;
  	public GameObject box_c_w;
  	public GameObject shieldGenerator_01a_w;
  	public GameObject shieldGenerator_02a_W;
  	public GameObject shieldGenerator_02b_W;
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		
		partList ["EnergySmall_w"]         = EnergySmall_w;
	    partList ["EnergySmall_w__1"]	   = EnergySmall_w;
	    partList ["Light_g2a_w"]		   = Light_g2a_w;
	    partList ["box_a_w"]			   = box_a_w;
	    partList ["box_b_w"]			   = box_b_w;
	    partList ["box_c_w"]			   = box_c_w;
	    partList ["shieldGenerator_01a_w"] = shieldGenerator_01a_w;
	    partList ["shieldGenerator_02a_W"] = shieldGenerator_02a_W;
	    partList ["shieldGenerator_02b_W"] = shieldGenerator_02b_W;
	}
}
