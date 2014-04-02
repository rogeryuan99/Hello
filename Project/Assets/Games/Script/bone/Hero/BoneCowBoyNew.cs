using UnityEngine;
using System.Collections;

public class BoneCowBoyNew : CharacterAnima {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject sash;
	public GameObject ass;
	public GameObject sword;
	public GameObject collar;
	public GameObject legUpL;
	public GameObject legUpR;
	public GameObject bodyUp2;
	public GameObject bodyDown; 
	public GameObject Shadow;
	public GameObject eft;
	public GameObject attackEft;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["SC_headC"] = head;
		partList["SC_bodyAddC"] = body;
		partList["SC_bodyUpC"] = bodyUp2;
		partList["SC_bodyDownC"] = bodyDown;
		partList["SC_armUpRC"] = armUpR;
		partList["SC_armUpLC"] = armUpL;
		partList["SC_AssC"] = ass;
		partList["SC_sashC"] = sash;
		partList["SC_Gun"] = sword;
		partList["SC_CollarC"]  = collar;
		partList["SC_legUpLC"]  = legUpL;
		partList["SC_legUpRC"]  = legUpR;
		partList["SC_GunE"]  = eft; 
		partList["Shadow"]  = Shadow;  
		partList["SC_E_ATTACE"]  = attackEft;
	}
//  <part name="Shadow" pos="-3|4" roration="0" epos="0|0"/>
//  <part name="SC_armUpLC" pos="25|78" roration="-4" epos="-1|-25"/>
//  <part name="SC_Gun" pos="25|36" roration="-78" epos="16|6"/>
//  <part name="SC_GunE" pos="23|33" roration="-79" epos="0|0"/>
//  <part name="SC_legUpLC" pos="15|23" roration="-6" epos="-4|-5"/>
//  <part name="SC_AssC" pos="5|27" roration="0" epos="2|6"/>
//  <part name="SC_bodyAddC" pos="3|48" roration="-1" epos="-1|17"/>
//  <part name="SC_legUpRC" pos="-9|29" roration="-15" epos="-1|-6"/>
//  <part name="SC_sashC" pos="4|39" roration="0" epos="-3|-1"/>
//  <part name="SC_bodyDownC" pos="21|77" roration="-1" epos="3|-24"/>
//  <part name="SC_headC" pos="-8|75" roration="0" epos="3|50"/>
//  <part name="SC_bodyUpC" pos="-18|83" roration="1" epos="-2|-24"/>
//  <part name="SC_armUpRC" pos="-28|74" roration="-13" epos="1|-21"/>
//  <part name="SC_CollarC" pos="-22|83" roration="60" epos="0|0"/>
//  <part name="SC_E_ATTACE" pos="91|93" roration="0" epos="0|0"/>
}
