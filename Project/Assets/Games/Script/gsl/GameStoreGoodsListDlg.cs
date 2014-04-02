using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStoreGoodsListDlg : DlgBase {
	public DynamicGrid grid;
	public UILabel title;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void init(List<StoreGoods> storeGoodsList){
		title.text = storeGoodsList[0].type;
		grid.setData(storeGoodsList);
	}
	
	public void OnBackBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		Destroy(gameObject);
	}
}
