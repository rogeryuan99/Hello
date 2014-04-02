using UnityEngine;
using System.Collections;

public class CannonDef : HazardDef
{
		
	protected float attackSpeed = 0f;
	protected float range = 1f;
	protected float attack = 0f;
	
	// 1 toward left 0 toward right
	protected int direction = 1;
	
	public float AttackSpeed
	{
		get
		{ 
			return attackSpeed;
		}
	}
	public float Range
	{
		get
		{ 
			return range; 
		}
	}
	public float Attack
	{
		get
		{ 
			return attack; 
		}
	}
	
	public int Direction
	{
		get
		{ 
			return direction; 
		}
	}
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		attackSpeed = float.Parse(attributesTable["aspd"] as string);
		range = float.Parse(attributesTable["range"] as string);
		attack = float.Parse(attributesTable["atk"] as string);
		direction = int.Parse(attributesTable["dir"] as string);
		
		base.parserAttributes(attributesTable, hazardType);
	}
}
