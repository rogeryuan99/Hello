using UnityEngine;
using System.Collections;

public class BoneDooM : CharacterAnima {
	
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject bodyDown;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject legDownR;
	public GameObject legDownL;
	public GameObject Shadow;
	public GameObject weapon;
	public GameObject weaponC_FI;
	
	// Use this for initialization
	public override void Awake () {
	base.Awake();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armDownR"] = armDownR;
		partList["armDownL"] = armDownL;
		partList["armUpR"] = armUpR;
		partList["armUpL"] = armUpL;
		partList["sash"] = bodyDown;
		partList["bodyUp"] = body;
		partList["head"] = head;
		partList["legdownR"] = legDownR;
		partList["legdownL"] = legDownL;
		partList["legupL"] = legUpL;
		partList["legupR"] = legUpR;
		partList["Shadow"] = Shadow;
		
		partList["weapon"] = weapon;
		partList["weaponC_FI"] = weaponC_FI;
		partList ["head"] = head;
		partList ["body"] = body;
		partList ["legL"] = legL;
		partList ["legR"] = legR;
		partList ["handL"] = handL;
		partList ["handR"] = handR;
	}

}
