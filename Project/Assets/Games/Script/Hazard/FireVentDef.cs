using UnityEngine;
using System.Collections;

public class FireVentDef : HazardDef
{
	protected float attackSpeed = 0f;
	protected float attack = 0f;
	
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
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		attackSpeed = float.Parse(attributesTable["aspd"] as string);
		attack = float.Parse(attributesTable["atk"] as string);
		
		base.parserAttributes(attributesTable, hazardType);
	}
}
