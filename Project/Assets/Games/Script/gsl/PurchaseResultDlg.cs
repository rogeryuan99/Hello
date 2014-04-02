using UnityEngine;
using System.Collections;

public class PurchaseResultDlg : DlgBase {
	public UILabel title;
	public UILabel description;
	private StoreGoods storeGoods;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public void init(StoreGoods goods){
		if(goods == null){
			title.text = "Sorry";
			description.text = "Your storage is full!";
		}else	{
			title.text = "Purchase Successful";
			description.text = "Room for content about coin can go right here so use it wisely!";
			this.storeGoods = goods;
		}
	}
	
	public void OnBackBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		Destroy(gameObject);	
	}
	
	public void OnGetMoreBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Destroy(gameObject);
//		if(storeGoods.type == "Gear" || storeGoods.type == "Uniforms" || storeGoods.type == "Artifacts"){
//			EquipData ed = EquipManager.Instance.allEquipHashtable[int.Parse(storeGoods.id)] as EquipData;
//			EquipData equipData = ed.clone();
//			EquipManager.Instance.allMyEquips.Add(equipData);
//			Debug.Log("equip's type:" + equipData.equipDef.type + " , equip's id:" + equipData.equipDef.id + " , equip's name:" + equipData.equipDef.equipName);
//		}
	}
}
