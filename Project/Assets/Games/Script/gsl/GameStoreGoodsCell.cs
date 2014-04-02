using UnityEngine;
using System.Collections;

public class GameStoreGoodsCell : DetailCell {
	public UILabel goodsName;
	public UISprite Icon;
	public UILabel price;
	
	public StoreGoods goods;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnIn(object data){
		goods = (StoreGoods)data;
		goodsName.text = goods.name;
		Icon.spriteName = goods.iconSpriteName;
		Icon.MakePixelPerfect();
		price.text = "" + goods.silver;
	}
	
	public override void OnOut(){
		
	}
	
	public void OnInfoBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		DlgManager.instance.ShowPurchaseGoodsDlg(goods);	
	}
}
