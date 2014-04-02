using UnityEngine;
using System.Collections;

public class BoneEft2 : BoneEft {

	protected override void initPartData (){
		partList = new Hashtable();
		partList["E1"] = E1;
		partList["E2"] = E2;
		partList["E3"] = E7;
		partList["E4"] = E8;
		partList["E5"] = E2;
	}
}