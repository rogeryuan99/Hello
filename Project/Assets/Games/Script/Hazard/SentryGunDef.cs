using UnityEngine;
using System;
using System.Collections;

public class SentryGunDef : HazardDef
{

	protected float attackSpeed = 0f;
	protected float attack = 0f;
	protected Vector2 frontSightPos;
	public float SentryGunY;
	
	public float AttackSpeed
	{
		get
		{ 
			return attackSpeed;
		}
	}
	
	public float Attack
	{
		get
		{ 
			return attack; 
		}
	}
	
	public Vector2 FrontSightPos
	{
		get
		{ 
			return frontSightPos; 
		}
	}
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		
		type = hazardType;
		
		attackSpeed = float.Parse(attributesTable["aspd"] as string);
		attack = float.Parse(attributesTable["atk"] as string);
		
		SentryGunY = float.Parse(attributesTable["SentryGunY"] as string);
		
		string[] strPos = (attributesTable["FrontSightPos"] as string).Split(',');
		frontSightPos = new Vector2(float.Parse(strPos[0]), float.Parse(strPos[1]));
		
//		base.parserAttributes(attributesTable);
	}
}
