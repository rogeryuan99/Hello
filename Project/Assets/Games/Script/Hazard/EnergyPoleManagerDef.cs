using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyPoleManagerDef : HazardDef
{
	public List<EnergyPoleDef> energyPoleList = new List<EnergyPoleDef>();
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType)
	{
		type = hazardType;
		
		float attackSpeed = float.Parse(attributesTable["aspd"] as string);
		float attack = float.Parse(attributesTable["atk"] as string);
		
		ArrayList energyPolePosList = attributesTable["polePos"] as ArrayList;
		
		for(int i = 0; i < energyPolePosList.Count; i++)
		{				
			string[] energyPolePosStr = energyPolePosList[i].ToString().Split(',');
			Vector2 p = new Vector2(float.Parse(energyPolePosStr[0]), float.Parse(energyPolePosStr[1]));
			
			EnergyPoleDef ep = new EnergyPoleDef();
			ep.attack = attack;
			ep.attackSpeed = attackSpeed;
			
			ep.localPosition = p;
			
			energyPoleList.Add(ep);
		}
	}
}
