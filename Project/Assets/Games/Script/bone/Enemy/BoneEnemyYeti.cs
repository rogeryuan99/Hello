using UnityEngine;
using System.Collections;

public class BoneEnemyYeti : PieceAnimation {
	public GameObject armUpR;
	public GameObject armUpL;
	public GameObject armDownR;
	public GameObject armDownL;
	public GameObject sash;
	public GameObject sword;
	public GameObject Shadow;
	public GameObject legupL;
	public GameObject legupR;
	public GameObject shldrL;
	public GameObject shldrR;
	public GameObject weapon2;
	public GameObject weapon3;
	
	public GameObject instance36;
	public GameObject ef1;
	public GameObject ef2;
	public GameObject ef3;
	public GameObject ef4;
	public GameObject ef5;
	public GameObject ef6;
	public GameObject ef7;
	public GameObject ef8;
	public GameObject ef1c;
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["head"] = head;
		partList["bodyUp"] = body;
		partList["legL"] = legL;
		partList["legR"] = legR;
		
		partList["armUpR"] = armUpR;
		partList["armDownR"] = armDownR;
		partList["armUpL"] = armUpL;
		partList["armDownL"] = armDownL;
			
		partList["sash"] = sash;
		partList["weapon"] = sword;
		partList["Shadow"]  = Shadow;
		partList["legUpL"]  = legupL;
		
		partList["legUpR"]  = legupR;
		partList["shldrL"]  = shldrL;
		partList["shldrR"]  = shldrR;
		partList["weapon2"]  = weapon2;
		partList["weapon3"]  = weapon3;
		
		partList["instance36"]  = instance36;
		partList["ef1"]  = ef1;
		partList["ef2"]  = ef2;
		partList["ef3"]  = ef3;
		partList["ef4"]  = ef4;
		partList["ef5"]  = ef5;
		partList["ef6"]  = ef6;
		partList["ef7"]  = ef7;
		partList["ef8"]  = ef8;
		partList["ef1c"]  = ef1c;
	}
}