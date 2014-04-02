using UnityEngine;
using System.Collections;

public class BoneHeroHealbot : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject bodyDown;
	public GameObject sash;
	public GameObject Shadow;
	public GameObject head2;
	public GameObject gq;
//	public GameObject deadEft;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["gq"] = gq;
		partList["bodyUp"] = body;
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
		partList["bodyDown"] = bodyDown;
		partList["sash"] = sash;
		partList["Shadow"] = Shadow;
		partList["face1"] = head2;
//		partList["bob"] = deadEft;
	}
}