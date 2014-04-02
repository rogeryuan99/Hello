using UnityEngine;
using System.Collections;

public class BoneFreezeGuyEft : BoneEft {
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	protected override void initPartData (){
		partList = new Hashtable();
		partList["sg"] = E1;
		partList["gs"] = E2;
		partList["xh"] = E7;
		partList["GZ7"] = E8;
	}
}
