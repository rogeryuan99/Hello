using UnityEngine;
using System;
using System.Collections;

public class HazardDef
{
	
	protected HazardType type = HazardType.Cannon;
	protected Vector2 position = Vector2.zero;
	
	public enum HazardType
	{
		Cannon,
		TrapDoor,
		FireVent,
		SentryGun,
		LandMine,
		EnergyPole,
		PowerUp
	}
	
	public HazardType Type
	{
		get
		{
			return type;
		}
	}
	
	public Vector2 Position
	{
		get
		{ 
			return position; 
		}
	}
	
	public virtual void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		
		type = hazardType;
		
		string[] strPos = (attributesTable["pos"] as string).Split(',');
		position = new Vector2(float.Parse(strPos[0]), float.Parse(strPos[1]));
	}	
}
