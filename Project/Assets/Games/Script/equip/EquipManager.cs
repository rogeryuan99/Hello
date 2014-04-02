using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipManager
{
	private static EquipManager instance;
		
	public List<EquipData> inventoryItemList = new List<EquipData>();
	
	public Hashtable allEquipHashtable = new Hashtable();  //key = id, value=EquipData
	
	// Key hero's Type(GROOT), Value(Hashtable Key (Weapon) Value InventoryItem)
	// public Hashtable heroItemHashtable = new Hashtable();
	
	public static EquipManager Instance {
		get{
			if (instance==null)
			{
				instance = new EquipManager();
			
			}
			return instance;
		}
	}

	public ArrayList dumpDynamicData(){
		ArrayList result = new ArrayList();
		
		for( int i=0; i<EquipManager.Instance.inventoryItemList.Count; i++)
		{
			EquipData ed = EquipManager.Instance.inventoryItemList[i];
			object o = ed.dumpDynamicData();
			if(o!=null){
				result.Add(o);
			}
		}
		return result;
	}
	
	public void loadDynamicData(object o){
		ArrayList a = o as ArrayList;
		if(a !=null){
			foreach(Hashtable h in a){
				EquipData equipD = EquipFactory.create((int)(double)h["id"]);
				equipD.loadDynamicData(h);
				EquipManager.Instance.inventoryItemList.Add(equipD);	
			}
		}else{
			Debug.LogError("Empty Backpack Data");	
		}
		
		bool isHasInventoryItemListISO = false;
		foreach(EquipData equipData in EquipManager.Instance.allEquipHashtable.Values)
		{
			if(equipData.equipDef.type != EquipData.Type.ISO)
			{
				continue;
			}
			isHasInventoryItemListISO = false;
			foreach(EquipData inventoryItemData in EquipManager.Instance.inventoryItemList)
			{
				if(inventoryItemData.equipDef.id == equipData.equipDef.id)
				{
					isHasInventoryItemListISO = true;
					break;
				}
			}
			if(!isHasInventoryItemListISO)
			{
				EquipData equipD = equipData.clone();
				
				//fake data
				switch(equipD.equipDef.id){
				case 4011:
					equipD.count = 5;
					break;
				case 4012:
					equipD.count = 5;
					break;
				case 4013:
					equipD.count = 5;
					break;
				}
				//end fake data
				EquipManager.Instance.inventoryItemList.Add(equipD);
			}		
		}
		EquipManager.Instance.inventoryItemList.Sort(delegate(EquipData x, EquipData y) {
			return (x.equipDef.id < y.equipDef.id)?-1:1;
		});
	}
	
	public EquipData getEquipDataByType(int typeid){
		foreach(EquipData ed in inventoryItemList){
			if(ed.equipDef.id == typeid) return ed;
		}
		return null;
	}
	public void testAddAEquip()
	{
		EquipData equipD = EquipFactory.create(2003);
		equipD.initLv(1);
		equipD.uid = "a01";
		EquipManager.Instance.inventoryItemList.Add(equipD);	
	}
}
