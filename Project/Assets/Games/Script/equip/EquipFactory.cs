using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;


public class EquipFactory {
//	public static Array storeList = [];
	
	private static int test_id= 0;
	
	public static EquipData create ( int id  ){
		if(id == 0) return null;
		EquipData equipD = EquipManager.Instance.allEquipHashtable[id] as EquipData;
		EquipData equipItem = equipD.clone();
//		if(equipD.equipDef.type == EquipData.CURRENCY)
//		{
//			equipItem.equipDef.state = EquipDef.STATE_ENUM.CURRENCY;
//		}else{
//			equipItem.equipDef.state = EquipDef.STATE_ENUM.INVENTORY;
//		}
		return equipItem;
	}
	
	public static EquipData getItemInInv ( int id  ){
		for(int i=0; i<EquipManager.Instance.inventoryItemList.Count; i++)
		{
			EquipData inventoryItem = EquipManager.Instance.inventoryItemList[i];
			if(inventoryItem.equipDef.id == id)
			{
				return inventoryItem;
			}
		}
		return null;
	}
	
	public static void getSomeEquips (){
//		getRandomItem(0,900);
//		getRandomItem(0,900);
//		getRandomItem(0,900);
//		getRandomItem(0,900);
		EquipData equipD = EquipManager.Instance.allEquipHashtable[9999] as EquipData;
		EquipManager.Instance.inventoryItemList.Insert(0,equipD.clone());
		/*
		equipD = lib[9900];
		list.Add(equipD.clone());
		*/
	}
	
	public static void getSomeKeys (){
		
	}
	

	public static void loadFromJson(ICollection al){
		foreach(Hashtable h in al){
			int uid = int.Parse(h["uid"] as string );
			int showAt =1;
			if(h["showlv"]!=null){
				showAt = int.Parse(h["showlv"] as string );	
			}
			int makeAvailableAt = 1;
			if(h["unlocklv"]!=null){
				makeAvailableAt = int.Parse(h["unlocklv"] as string );	
			}
			int display = 1;
			int dif = 1;
			string _type = h["type"] as string ;
			EquipData.Type type = (EquipData.Type)Enum.Parse(typeof(EquipData.Type),_type);
			
			string equipName = h["name"] as string ; 
			
			int silver = 0;
			if(h["silver"]!=null){
				silver = int.Parse(h["silver"] as string );	
			}
			int gold = 0;
			if(h["gold"]!=null){
				gold = int.Parse(h["gold"] as string );	
			}
			int cp = 0;
			if(h["cp"]!=null){
				cp = int.Parse(h["cp"] as string );	
			}
			string graphicsID = "";
			if(h["graphicsID"]!=null){
				graphicsID = h["graphicsID"] as string ;	
			}			
			string iconID = h["iconID"] as string ;	
		
			bool  isLvUp = false;
			if(h["isLvUp"]!=null){
				isLvUp = "true".Equals(h["isLvUp"] as string);			
			}
			
			
			List<string> fuseCostIDs = new List<string>();
			if(h["fuseCostIDs"]!=null)
			{
				string fuseCostIDStr = h["fuseCostIDs"] as string;	
				if(fuseCostIDStr!="")
				{
					fuseCostIDs.AddRange(fuseCostIDStr.Split(","[0]));
				}
			}
			
			ArrayList specialType = new ArrayList();
			if(h["specialType"]!=null){
				string tempSpecialType = h["specialType"] as string;
				if(tempSpecialType != null && tempSpecialType.Length > 0)
				{
					specialType= new ArrayList(tempSpecialType.Split("|"[0]));
				}
			}
			string description = "";//StaticData.getXmlNode(equipNode, "description").InnerText;//equipNode.Item["description"].InnerText;
			
			List<Effect> equipEftList = getEquipEftList(h);
			
			EquipDef equipDef = new EquipDef(
				uid,
				dif,
				type,
				showAt,
				makeAvailableAt,
				display,
				equipName,
				silver,
				gold,
				cp,
				graphicsID,
				iconID,
				isLvUp,
				specialType,
				description,
//				equipEft.num,
				fuseCostIDs,
				equipEftList);
			
			EquipData equipData = new EquipData(equipDef);
					
			EquipManager.Instance.allEquipHashtable[uid] = equipData;				
		}
	}
	
	//for 20121107 bug fix   start---->
	public static void loadStaticFromXml (string itemsXml){
		XmlDocument xmlDataRoot = new XmlDocument();
		xmlDataRoot.LoadXml(itemsXml);
		XmlNodeList xmlData = StaticData.getXmlNode(xmlDataRoot, "root").FirstChild.ChildNodes;
		Debug.Log(xmlData.Count);
		XmlNodeList equipList = xmlData;
//		Debug.Log("---------------------------->"+equipList.Count);
		for(int i=0; i<equipList.Count; i++)
		{
			XmlNode equipNode = equipList[i];
//			Debug.Log("---------------------------->"+i);
//			Debug.Log("---------------------------->"+equipNode.Name);
			if("#comment".Equals(equipNode.Name)){
				continue;
			}
			int id = int.Parse(equipNode.Attributes["id"].Value);
			
			
			//add by gwp of level display
			int showAt =1;
			int makeAvailableAt = 1;
			int display = 1;
			if(equipNode.Attributes["showAtLevel"] != null)
			{
				showAt = int.Parse(equipNode.Attributes["showAtLevel"].Value); 
			}
			if(equipNode.Attributes["makeAvailableAtLevel"] != null)
			{
				makeAvailableAt = int.Parse(equipNode.Attributes["makeAvailableAtLevel"].Value); 
			}
			if(equipNode.Attributes["display"] != null)
			{
				display = int.Parse(equipNode.Attributes["display"].Value); 
			}

			int dif = 1;
			if(equipNode.Attributes["dif"]!=null){
				dif= int.Parse(equipNode.Attributes["dif"].Value);
			}
			string _type = equipNode.Attributes["type"].Value;
			EquipData.Type type = (EquipData.Type)Enum.Parse(typeof(EquipData.Type),_type);
			string equipName = equipNode.Attributes["name"].Value;
			int silver = 0;
			if(equipNode.Attributes["silver"]!=null){
				silver = int.Parse(equipNode.Attributes["silver"].Value);
			}
			int gold = 0;
			if(equipNode.Attributes["gold"]!=null){
				gold = int.Parse(equipNode.Attributes["gold"].Value);
			}
			int cp = 0;
			if(equipNode.Attributes["cp"]!=null){
				cp = int.Parse(equipNode.Attributes["cp"].Value);
			}
			string graphicsID = "";
			if(equipNode.Attributes["graphicsID"]!=null){
				graphicsID = equipNode.Attributes["graphicsID"].Value;
			}
			string iconID = equipNode.Attributes["iconID"].Value;
			bool  isLvUp = false;
			if(equipNode.Attributes["isLvUp"]!=null){
				isLvUp = "true".Equals(equipNode.Attributes["isLvUp"].Value);			
			}
			
			List<string> fuseCostIDs = new List<string>();
			if(equipNode.Attributes["fuseCostIDs"]!=null)
			{
				string fuseCostIDStr = equipNode.Attributes["fuseCostIDs"].Value;	
				if(fuseCostIDStr!="")
				{
					fuseCostIDs.AddRange(fuseCostIDStr.Split(","[0]));
				}
			}
			
			
			
			
//			XmlNode eftNode = StaticData.getXmlNode(equipNode, "eft");// equipNode.Item["eft"];
//			string eftType = eftNode.Attributes["type"].Value;
//			int eftNum = int.Parse(eftNode.Attributes["num"].Value);
//			bool  eftIsPer = "true".Equals(eftNode.Attributes["num"].Value);

			ArrayList specialType = new ArrayList();
			
			string tempSpecialType = equipNode.Attributes["specialType"].Value;
			
			if(tempSpecialType != null && tempSpecialType.Length > 0)
			{
				specialType= new ArrayList(tempSpecialType.Split("|"[0]));
			}
			
			string description = "";//StaticData.getXmlNode(equipNode, "description").InnerText;//equipNode.Item["description"].InnerText;
			
			
			List<Effect> equipEftList = getEquipEftList(equipNode);
			
			
			EquipDef equipDef = new EquipDef(
				id,
				dif,
				type,
				showAt,
				makeAvailableAt,
				display,
				equipName,
				silver,
				gold,
				cp,
				graphicsID,
				iconID,
				isLvUp,
				specialType,
				description,
//				equipEft.num,
				fuseCostIDs,
				equipEftList);
			
			EquipData equipData = new EquipData(equipDef);
					
			EquipManager.Instance.allEquipHashtable[id] = equipData;

		}
	}
	//<--------------------  end
	public static List<Effect> getEquipEftList(Hashtable h)
	{
		List<Effect> equipEftList = new List<Effect>();
		parseEffect(h,"atk_PHY",equipEftList);
		parseEffect(h,"atk_IMP",equipEftList);
		parseEffect(h,"atk_PSY",equipEftList);
		parseEffect(h,"atk_EXP",equipEftList);
		parseEffect(h,"atk_ENG",equipEftList);
		parseEffect(h,"atk_MAG",equipEftList);
		parseEffect(h,"def_PHY",equipEftList);
		parseEffect(h,"def_IMP",equipEftList);
		parseEffect(h,"def_PSY",equipEftList);
		parseEffect(h,"def_EXP",equipEftList);
		parseEffect(h,"def_ENG",equipEftList);
		parseEffect(h,"def_MAG",equipEftList);
		parseEffect(h,"mspd"   ,equipEftList);
		parseEffect(h,"aspd"   ,equipEftList);
		parseEffect(h,"hp"   ,equipEftList);
		parseEffect(h,"regen"   ,equipEftList);
		parseEffect(h,"healout"   ,equipEftList);
		parseEffect(h,"healin"   ,equipEftList);
		return equipEftList;
	}
	protected static void parseEffect(Hashtable sourceHash, string key,List<Effect> equipEftList){
		if(sourceHash[key] != null)
		{
			equipEftList.Add(getEquipEft( sourceHash[key] as string, key));
		}
	}
	
	public static List<Effect> getEquipEftList(XmlNode equipNode)
	{
		List<Effect> equipEftList = new List<Effect>();
	
		if(equipNode.Attributes["mspd"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["mspd"].Value, "mspd"));
		}
		if(equipNode.Attributes["aspd"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["aspd"].Value, "aspd"));
		}
		if(equipNode.Attributes["hp"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["hp"].Value, "hp"));
		}
		if(equipNode.Attributes["regen"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["regen"].Value, "regen"));
		}
		if(equipNode.Attributes["healout"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["healout"].Value, "healout"));
		}
		if(equipNode.Attributes["healin"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["healin"].Value, "healin"));
		}
		if(equipNode.Attributes["atk_PHY"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_PHY"].Value, "atk_PHY"));
		}
		if(equipNode.Attributes["atk_IMP"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_IMP"].Value, "atk_IMP"));
		}
		if(equipNode.Attributes["atk_PSY"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_PSY"].Value, "atk_PSY"));
		}
		if(equipNode.Attributes["atk_EXP"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_EXP"].Value, "atk_EXP"));
		}
		if(equipNode.Attributes["atk_ENG"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_ENG"].Value, "atk_ENG"));
		}
		if(equipNode.Attributes["atk_MAG"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["atk_MAG"].Value, "atk_MAG"));
		}
		if(equipNode.Attributes["def_PHY"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_PHY"].Value, "def_PHY"));
		}
		if(equipNode.Attributes["def_IMP"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_IMP"].Value, "def_IMP"));
		}
		if(equipNode.Attributes["def_PSY"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_PSY"].Value, "def_PSY"));
		}
		if(equipNode.Attributes["def_EXP"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_EXP"].Value, "def_EXP"));
		}
		if(equipNode.Attributes["def_ENG"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_ENG"].Value, "def_ENG"));
		}
		if(equipNode.Attributes["def_MAG"] != null)
		{
			equipEftList.Add(getEquipEft(equipNode.Attributes["def_MAG"].Value, "def_MAG"));
		}
		return equipEftList;
	}
	
	protected static Effect getEquipEft(string v, string name)
	{
		Effect eft = new Effect();
		eft.eName = name.ToUpper();
		if(v.Contains("%"))
		{
			eft.isPer = true;
			string num = v.Remove(v.IndexOf('%'));
			
			eft.num = int.Parse(num);
		}
		else
		{
			eft.isPer = false;
			eft.num = int.Parse(v);
		}
		return eft;
	}
	
	public static long dateToSeconds ( string date  ){
		long second = 0;
		if(date.Length > 1){
			int year = int.Parse(date.Substring(0,4));
			int month = int.Parse(date.Substring(4,2));
			int day = int.Parse(date.Substring(6,2));
			Debug.Log(">>>>year:"+year +" month:" + month + "day:" + day);
			System.DateTime dateTime = new System.DateTime(year,month,day);
			second = dateTime.Ticks/10000/1000;
			//print(second); 
		}
		return second;
	}
	

}
