using UnityEngine;
using System.Collections;

public class LandMineDef : HazardDef
{
	protected float attack;
	
	public float Attack
	{
		get
		{ 
			return attack; 
		}
	}
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		attack = float.Parse(attributesTable["atk"] as string);
		
		base.parserAttributes(attributesTable, hazardType);
	}
}
