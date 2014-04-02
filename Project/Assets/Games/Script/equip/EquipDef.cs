using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EquipDef
{
	public int id;
	public int dif;
	public EquipData.Type type;
	
	// show at XX level
	public int showAtLevel;
	
	// use ai XX level
	public int makeAvailableAtLevel;
	
	public int display;
	
	public string equipName;
	
	public int silver;   
	
	public int gold;
	public int commandPoints;
	
	
	public string graphicsID;
	public string iconID;
	
	public bool  isLvUp= true;
	
	public string des;
	
	public ArrayList specialType = new ArrayList();
	
	public STATE_ENUM state;
	
	
	public int maxLv=10;
	
//	public int baseValue;
	
	// add by why 20130522 start
	//NORMAL=0, GOOD=1, RARE=2, LEGEND=3
	
	public enum STATE_ENUM{STORE, INVENTORY, EQUIPPED, CURRENCY};
	
//	public enum EQUIPRARITY_ENUM{NORMAL, GOOD, RARE, LEGEND};
	
	
//	public EQUIPRARITY_ENUM equipRarity;
	
	public List<Effect> equipEftList;
	
	public List<string> fuseISOCostID = new List<string>();
	
	public EquipDef()
	{
		
	}
	
	public EquipDef(
		int id,  
		int dif, 
		EquipData.Type type,
		int showAtLevel, 
		int makeAvailableAtLevel, 
		int display,
		string equipName,
		int silver,
		int gold,
		int commandPoints,
		string graphicsID,
		string iconID,
		bool isLvUp,
		ArrayList specialType,
		string des,
//		int baseValue,
		List<string> fuseISOCostID,
		List<Effect> equipEftList)
	{
		
		this.id = id;
		this.dif = dif;
		this.type = type;
		this.showAtLevel = showAtLevel;
		this.makeAvailableAtLevel = makeAvailableAtLevel;
		this.display = display;
		this.equipName = equipName;
		this.silver = silver;
		this.gold = gold;
		this.commandPoints = commandPoints;
		this.graphicsID = graphicsID;
		this.iconID = iconID;
		this.isLvUp = isLvUp;
		for(int i = 0; i < specialType.Count; ++i)
		{
			this.specialType.Add(specialType[i]);
		}
		this.des = des;
//		this.baseValue = baseValue;
		this.fuseISOCostID = fuseISOCostID;
		this.equipEftList = equipEftList;
	}
	
	public EquipDef clone ()
	{
		List<Effect> equipEftListTemp = new List<Effect>();
		foreach(Effect eft in this.equipEftList)
		{
			equipEftListTemp.Add(eft.clone());
		}
		return new EquipDef(
				this.id,
				this.dif,
				this.type,
				this.showAtLevel,
				this.makeAvailableAtLevel,
				this.display,
				this.equipName,
				this.silver,
				this.gold,
				this.commandPoints,
				this.graphicsID,
				this.iconID,
				this.isLvUp,
				this.specialType,
				this.des,
//				this.baseValue,
				this.fuseISOCostID,
				equipEftListTemp);
	}

}
