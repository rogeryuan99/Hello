using UnityEngine;
using System.Collections;

public class BoneSkillCaiera1BodyEft : PieceAnimation
{

public GameObject FEMALE_Weapon_03;
public GameObject FEMALE_Weapon_10;
public GameObject FEMALE_Weapon_11;
public GameObject FEMALE_Weapon_14;
public GameObject FEMALE_Weapon_15;
public GameObject FEMALE_Weapon_16;
public GameObject FEMALE_Weapon_17;
public GameObject FEMALE_Weapon_18;
public GameObject FEMALE_Weapon_19;
public GameObject FEMALE_Weapon_20;
public GameObject FEMALE_Weapon_23;
public GameObject FEMALE_Weapon_28;
	
	public override void Awake ()
	{ 		
		base.Awake();
	  			
		animaPlayEndScript(pauseAnima);
	}
	
	public void pauseAnima(string s)
	{
		pauseAnima();
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
	 partList ["FEMALE_Weapon_03"]   = FEMALE_Weapon_03;
partList ["FEMALE_Weapon_03__1"]= FEMALE_Weapon_03;
partList ["FEMALE_Weapon_03__2"]= FEMALE_Weapon_03;
partList ["FEMALE_Weapon_10"]   = FEMALE_Weapon_10;
partList ["FEMALE_Weapon_11"]   = FEMALE_Weapon_11;
partList ["FEMALE_Weapon_14"]   = FEMALE_Weapon_14;
partList ["FEMALE_Weapon_15"]   = FEMALE_Weapon_15;
partList ["FEMALE_Weapon_16"]   = FEMALE_Weapon_16;
partList ["FEMALE_Weapon_17"]   = FEMALE_Weapon_17;
partList ["FEMALE_Weapon_18"]   = FEMALE_Weapon_18;
partList ["FEMALE_Weapon_19"]   = FEMALE_Weapon_19;
partList ["FEMALE_Weapon_20"]   = FEMALE_Weapon_20;
partList ["FEMALE_Weapon_23"]   = FEMALE_Weapon_23;
partList ["FEMALE_Weapon_28"]   = FEMALE_Weapon_28;
	}
	
	public void destroySelf(string s)
	{
		
		Destroy(gameObject);
		
	}
}
