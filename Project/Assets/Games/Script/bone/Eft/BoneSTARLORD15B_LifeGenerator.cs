using UnityEngine;
using System.Collections;

public class BoneSTARLORD15B_LifeGenerator : PieceAnimation {

    public GameObject EnergySmall;
    public GameObject box;
    public GameObject light_018;
    public GameObject shieldGenerator_01a;
    public GameObject shieldGenerator_02a;
    public GameObject shieldGenerator_02b;
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		partList ["EnergySmall"] = EnergySmall;
		partList ["box"] = box;
		partList ["light_018"] = light_018;
		partList ["shieldGenerator_01a"] = shieldGenerator_01a;
		partList ["shieldGenerator_02a"] = shieldGenerator_02a;
		partList ["shieldGenerator_02b"] = shieldGenerator_02b;
	}
}
