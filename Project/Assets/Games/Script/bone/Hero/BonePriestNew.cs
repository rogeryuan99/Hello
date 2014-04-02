using UnityEngine;
using System.Collections;

public class BonePriestNew : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject headB;
	public GameObject bodyDown;
	public GameObject sword;
	public GameObject eft;
	public GameObject shadow;
	
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["headB"] = headB;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["bodydown"] = bodyDown;
		partList["weapon"] = sword;
		partList["weapon2"] = sword;
		partList["Eweapon"]  = eft;
		partList["Eweapon2"]  = eft;
		partList["Shadow"]   = shadow;
	}
}
