using UnityEngine;
using System.Collections;

public class BoneGROOT5BStone : PieceAnimation
{
public GameObject Stone_01	;			
public GameObject Stone_0111;			
public GameObject Stone_0116;			
public GameObject Stone_0118;			
public GameObject Stone_0120;			
public GameObject Stone_02	;			
public GameObject Stone_0212;			
public GameObject Stone_0219;			
public GameObject Stone_027	;		
public GameObject Stone_03	;			
public GameObject Stone_04	;			
public GameObject Stone_041	;		
public GameObject Stone_0414;			
public GameObject Stone_0417;			
public GameObject Stone_042	;		
public GameObject Stone_043	;		
public GameObject Stone_044	;		
public GameObject Stone_045	;		
public GameObject Stone_046	;		
public GameObject Stone_05	;			
public GameObject Stone_0510;			
public GameObject Stone_0513;			
public GameObject Stone_0515;			
public GameObject Stone_058	;		
public GameObject Stone_059	;		


	public override void Awake ()
	{ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
		
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
partList ["Stone_01"]=     Stone_01	;
partList ["Stone_0111"]=   Stone_0111;
partList ["Stone_0116"]=   Stone_0116;
partList ["Stone_0118"]=   Stone_0118;
partList ["Stone_0120"]=   Stone_0120;
partList ["Stone_02"]=	   Stone_02	;
partList ["Stone_0212"]=   Stone_0212;
partList ["Stone_0219"]=	Stone_0219;	
partList ["Stone_027"]=		Stone_027	;	
partList ["Stone_03"]=	   Stone_03	;
partList ["Stone_04"]=	   Stone_04	;
partList ["Stone_041"]=	   Stone_041	;
partList ["Stone_0414"]=	Stone_0414;		
partList ["Stone_0417"]=	Stone_0417;		
partList ["Stone_042"]=		Stone_042	;			
partList ["Stone_043"]=		Stone_043	;			
partList ["Stone_044"]=		Stone_044	;			
partList ["Stone_045"]=		Stone_045	;				
partList ["Stone_046"]=		Stone_046	;				
partList ["Stone_05"]=		Stone_05	;				
partList ["Stone_0510"]=	Stone_0510;				
partList ["Stone_0513"]=	Stone_0513;			
partList ["Stone_0515"]=	Stone_0515;				
partList ["Stone_058"]=		Stone_058	;			
partList ["Stone_059"]=  	Stone_059	;				
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
