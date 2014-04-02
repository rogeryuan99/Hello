using UnityEngine;
using System.Collections;

public class BoneEnemyDoppelgangers : PieceAnimation {
	public GameObject armDown;
	public GameObject armup;
	public GameObject behind;

	
	public GameObject leg22;

	public GameObject leg44;

	public GameObject leg11;

	public GameObject leg33;

	public GameObject Shadow;
	public GameObject weapon;
	public GameObject leg4;

	public GameObject leg3;
	public GameObject leg2;
	public GameObject leg1;

	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyup"] = body;

		partList["armDown"] = armDown;
		partList["armup"] = armup;	
		partList["behind"] = behind;
		partList["leg22"] = leg22;
		partList["leg44"] = leg44;
		partList["leg11"] = leg11;
		partList["leg33"] = leg33;
		
		partList["Shadow"] = Shadow;
		partList["weapon"] = weapon;
		
		partList["leg2"] = leg2;
		partList["leg4"] = leg4;
		partList["leg1"] = leg1;
		partList["leg3"] = leg3;
	}
}