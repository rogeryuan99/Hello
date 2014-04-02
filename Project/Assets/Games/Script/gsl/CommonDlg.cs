using UnityEngine;
using System.Collections;

public class CommonDlg : DlgBase {
	public UILabel commonLabel;
	
	public delegate void OnYesDelegate ();
	public OnYesDelegate onYes;
	
	public delegate void OnNoDelegate ();
	public OnNoDelegate onNo;
	
	public delegate void OnOkDelegate ();
	public OnOkDelegate onOk;
	
	public OnYesDelegate OnUpdateStr;
	
	public GameObject yesBtnObj;
	public GameObject noBtnObj;
	public GameObject okBtnObj;
	
	public UILabel labelYes;
	public UILabel labelOk;
	public UILabel labelNo;
	public UILabel labelNo2;
	
	void Start () {
	
	}
	
	public void setYesText(string s){
		labelYes.text = Localization.instance.Get("UI_CommonDlg_Button_"+s);
	}
	public void setNoText(string s){
		labelNo.text = Localization.instance.Get("UI_CommonDlg_Button_"+s);
		labelNo2.text = Localization.instance.Get("UI_CommonDlg_Button_"+s);
	}
	public void setOkText(string s){
		labelOk.text = Localization.instance.Get("UI_CommonDlg_Button_"+s);
	}
	public void FixedUpdate () {
		if (null != OnUpdateStr){
			OnUpdateStr();
		}
	}
	
	public void OnOkBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if (null != onOk)
			onOk();
		Destroy(gameObject);
	}
	
	public void OnYesBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if (null != onYes)
			onYes();
		Destroy(gameObject);
	}
	
	public void OnNoBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if (null != onNo)
			onNo();
		Destroy(gameObject);	
	}
	
	public void ShowCommonStr(string s){
		commonLabel.text = s;
	}
	
	public void setOneBtnDlg(){
		okBtnObj.SetActive(true);
		yesBtnObj.SetActive(false);
		noBtnObj.SetActive(false);
	}
	
	public void setOKDlg()
	{
		setOneBtnDlg("OK");
	}
	
	public void setOneBtnDlg(string btnString)
	{
		setOneBtnDlg();
		okBtnObj.GetComponentInChildren<UILabel>().text = Localization.instance.Get("UI_CommonDlg_Button_"+btnString);
	}
	
	public void setNormalDlg()
	{
		okBtnObj.SetActive(false);
		yesBtnObj.SetActive(true);
		noBtnObj.SetActive(true);
	}
	
	public void playErrorMusic(){
		MusicManager.playEffectMusic("SFX_Error_Message_1c");	
	}
}
