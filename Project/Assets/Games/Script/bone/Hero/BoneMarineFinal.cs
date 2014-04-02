using UnityEngine;
using System.Collections;

public class BoneMarineFinal : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject sash;
	public GameObject weapon;
	public GameObject Shadow;
	public GameObject head2;
	public GameObject head3;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["head2"]  = head2;
		partList["head3"]  = head3;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["armUpR"] = armUpR;
		partList["armUpL"] = armUpL;
		partList["sash"] = sash;
		partList["weapon"] = weapon;
		partList["Shadow"] = Shadow;
	}
}
