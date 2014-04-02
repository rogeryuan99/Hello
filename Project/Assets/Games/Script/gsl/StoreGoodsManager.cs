using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreGoodsManager {
	public List<StoreGoods> goodsList = new List<StoreGoods>();
	
	private static StoreGoodsManager _Instance;
	
	public static StoreGoodsManager Instance{
		get{
			if(_Instance == null){
				_Instance = new StoreGoodsManager();
			}
			return _Instance;
		}
	}

	public void parseFromJson(string jsontext){
		
		ArrayList allData = MiniJsonExtensions.arrayListFromJson(jsontext);
		foreach(Hashtable t in allData){
//			StoreGoods goods = new StoreGoods();
//			goods.id = t["id"] as string;
//			EquipData qd =  EquipManager.Instance.allEquipHashtable[int.Parse(goods.id)] as EquipData;
//			goods.type = qd.equipDef.type;
//			goods.name = qd.equipDef.equipName;
//			goods.silver = qd.equipDef.silver;
//			if(goods.type == "Weapon" || goods.type == "Armor" || goods.type == "Trinket"){
//				goods.iconSpriteName = qd.equipDef.equipName;//qd.equipDef.iconID;
//			}else{
//				goods.iconSpriteName = qd.equipDef.type;
//			}
//			goods.describe = qd.equipDef.des;
////			goods.type = t["type"] as string;
////			goods.name = t["name"] as string;
////			goods.price = (int)(double)t["price"];
////			goods.iconSpriteName = t["iconSpriteName"] as string;
////			goods.describe = t["describe"] as  string;
//			goodsList.Add(goods);
		}
	}
}
