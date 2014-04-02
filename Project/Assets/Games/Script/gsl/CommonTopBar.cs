using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonTopBar : MonoBehaviour {
	public UILabel GoldLabel;
	public UILabel SilverLabel;
	public UILabel CpLabel;
	void Update(){
		if(UserInfo.instance == null) return;
		SilverLabel.text = UserInfo.instance.getSilver().ToString();	
		CpLabel.text = UserInfo.instance.getCommandPoints().ToString();
	}
	void OnSilverAdd(){
		int count = 1000;
//		int costCP = 5;
//		string s;
//		if(UserInfo.instance.getCommandPoints() < costCP){
//			MusicManager.playEffectMusic("SFX_Error_Message_1c");
//			//s = "Your Gold is not enough!"; 	
//			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughCommandPoints"));
//			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//			dlg.setOKDlg();
//		}else{
//			//s = "Are you sure to buy [00FFFF]" + count + " [FFFFFF][Silver] with [00FFFF]" + costGold + " [FFFFFF][Gold] ?";
//			s = string.Format(Localization.instance.Get("UI_CommonDlg_BuySilver"),count,costCP);
//			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//			dlg.onYes = delegate {
//				UserInfo.instance.consumeCommandPoints(costCP);
//				UserInfo.instance.addSilver(count);
//			};
//		}
		UserInfo.instance.addSilver(count);
		UserInfo.instance.saveAll();
	}
	void OnGoldAdd(){
//		int count = 50;
//		//string s = "Buy [00FFFF]" + count + " [FFFFFF][Gold] from iap store.";
//		string s = string.Format(Localization.instance.Get("UI_CommonDlg_BuyGold"),count);
//		CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//		dlg.onYes = delegate {
//			UserInfo.instance.addGold(count);
//		};
	}
	void OnCpAdd(){
		int count = 10;
//		string s = string.Format(Localization.instance.Get("UI_CommonDlg_BuyCP"),count);
//		CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//		dlg.onYes = delegate {
//			UserInfo.instance.addCommandPoints(count);
//		};
		UserInfo.instance.addCommandPoints(count);
		UserInfo.instance.saveAll();
	}
}
