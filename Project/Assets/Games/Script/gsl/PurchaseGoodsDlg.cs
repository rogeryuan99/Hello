using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurchaseGoodsDlg : DlgBase {
	public UILabel name;
	public UISprite Icon;
	public UILabel price;
	public UILabel describe;
	
	private StoreGoods storeGoods; 
	
	public void init(StoreGoods goods){
		this.storeGoods = goods;
		name.text = goods.name;
		Icon.spriteName = goods.iconSpriteName;
		Icon.MakePixelPerfect();
		price.text = "" + goods.silver;
		describe.text = "description of " + goods.name;//goods.describe;
	}
	
	public void OnBackBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		Destroy(gameObject);			
	}
	
	public void OnBuyBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(storeGoods.type == "Weapon" || storeGoods.type == "Armor" || storeGoods.type == "Trinket"){
			if(EquipManager.Instance.inventoryItemList.Count >= 9){
				DlgManager.instance.ShowPurchaseResultDlg(null);	
				Destroy(gameObject);
				return;
			}
			EquipData ed = EquipManager.Instance.allEquipHashtable[int.Parse(storeGoods.id)] as EquipData;
			EquipData equipData = ed.clone();
			equipData.initUidTemp();
			EquipManager.Instance.inventoryItemList.Add(equipData);
			UserInfo.instance.savePackage();
			Debug.Log("equip's type:" + equipData.equipDef.type + " , equip's id:" + equipData.equipDef.id + " , equip's name:" + equipData.equipDef.equipName);
		}
		DlgManager.instance.ShowPurchaseResultDlg(this.storeGoods);
		Destroy(gameObject);
	}
}
