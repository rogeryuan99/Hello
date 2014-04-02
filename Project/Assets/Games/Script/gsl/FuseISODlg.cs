using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FuseISODlg : DlgBase {
	private int selectISOID;
	public EquipSlotCell fuseElement1;
	public EquipSlotCell fuseElement2;
	public EquipSlotCell fuseResult;
	
	public EquipSlotCell noFuseElement;
	
	public GameObject fusePanelObj;
	public GameObject noFusePanelObj;
	
	public UIImageButton fuseBtn;
	
	protected EquipData sourceEd1;
	protected EquipData sourceEd2;
	protected EquipData targetEd;
	
	public UILabel selectISOInfo;
	public UILabel costInfo;
	public UILabel costPrice;
	public UILabel fuseResultISOAttribute;
	
	//		List<EquipData> templist = new List<EquipData>();
//		foreach(EquipData ed in EquipManager.Instance.inventoryItemList)
//		{
//			if(ed.equipDef.type == EquipData.Type.ISO)
//			{
//				templist.Add(ed);
//			}
//		}
	
	void Start(){
		sourceEd1 = null;
		sourceEd2 = null;
		targetEd = null;
		
		fuseElement1.setEquipData(sourceEd1);
		fuseElement2.setEquipData(sourceEd2);	
		fuseResult.setEquipData(targetEd);
		
		costPrice.text = "0";
		fuseResultISOAttribute.text = "";	
	}
	
	public void initFuseISODlgCell( int selectISOID)
	{
		this.selectISOID = selectISOID;	
		fusePanelObj.SetActive(true);
		noFusePanelObj.SetActive(false);
		
		targetEd = EquipManager.Instance.getEquipDataByType(selectISOID);
		if(targetEd.equipDef.fuseISOCostID.Count==0){
			Debug.LogError("NOFUSE");	
			sourceEd1 = null;
			sourceEd2 = null;
		}else{
			sourceEd1 = EquipManager.Instance.getEquipDataByType(int.Parse(targetEd.equipDef.fuseISOCostID[0]));
			sourceEd2 = EquipManager.Instance.getEquipDataByType(int.Parse(targetEd.equipDef.fuseISOCostID[1]));
		}
		Debug.Log("targetEd="+targetEd);
		Debug.Log("sourceEd1="+sourceEd1);
		Debug.Log("sourceEd2="+sourceEd2);
		
		fuseResult.storagePanel = TeamDlg.instance.storagePanel;
		fuseElement1.storagePanel = TeamDlg.instance.storagePanel;
		fuseElement2.storagePanel = TeamDlg.instance.storagePanel;
		
		fuseResult.setEquipData(targetEd);
		fuseElement1.setEquipData(sourceEd1);
		fuseElement2.setEquipData(sourceEd2);
		
		if(targetEd.equipDef.fuseISOCostID.Count == 0)
		{
			fusePanelObj.SetActive(false);
			noFusePanelObj.SetActive(true);
			noFuseElement.setEquipData(targetEd);
		}else
		{
			costInfo.text =""+targetEd.equipDef.silver;// string.Format(Localization.instance.Get("UI_FuseISODlg_Cost"),targetEd.equipDef.silver);
			fusePanelObj.SetActive(true);
			noFusePanelObj.SetActive(false);
			
			bool canfuse = true;
			
			if(fuseElement1.equipData.count<=0 || fuseElement2.equipData.count<=0){
				canfuse = false;
			}
			if(fuseElement1.equipData.equipDef.id == fuseElement1.equipData.equipDef.id && fuseElement1.equipData.count<2){
				canfuse = false;
			}
			Debug.Log("enough to fuse: "+canfuse);
			fuseBtn.isEnabled = canfuse;
			fuseBtn.collider.enabled = canfuse;
		}
		setISOInfoBar(targetEd);
	}
	
	
	protected void setISOInfoBar(EquipData equipData)
	{
//		// delete by why 2014.2.10
//		string numStr = "";
//		if(equipData.equipDef.equipEft.isPer)
//		{
//			numStr =equipData.eftNum.ToString() + "%";
//		}
//		else
//		{
//			numStr =equipData.eftNum.ToString();
//		}
//		//selectISOInfo.text = equipData.equipDef.equipName + "\n" + equipData.equipDef.equipEft.eName + " : " + numStr;
//		selectISOInfo.text = string.Format("{0}\n{1} : {2}",Localization.instance.Get("ISO_Name_"+equipData.equipDef.id),
//															Localization.instance.Get("UI_Hero_"+equipData.equipDef.equipEft.eName),numStr);
		selectISOInfo.text = equipData.getDescString();
	}
	
	public void onFuseBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		
		if(targetEd == null) return;
		
		fuseElement1.equipData.count--;
		fuseElement2.equipData.count--;
		fuseResult.equipData.count++;
		
		UserInfo.instance.savePackage();
		TeamDlg.instance.storagePanel.updateWithEquipData();
		UpdateFuseView();
//		initFuseISODlgCell(this.selectISOID);
//		refreshStorageCell();
	}
	
	public void onCloseBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		TeamDlg.instance.storagePanel.updateStorageCollection(0);
		this.gameObject.SetActive(false);
	}
	
	public void UpdateFuseView(){
		sourceEd1 = fuseElement1.equipData;
		sourceEd2 = fuseElement2.equipData;
		
		bool canfuse = true;
		if(sourceEd1 == null || sourceEd2 == null){
			canfuse = false;
		}else if(sourceEd1.count<=0 || sourceEd2.count<=0){
			canfuse = false;
		}else if(sourceEd1.equipDef.id == sourceEd2.equipDef.id && sourceEd1.count<2){
			canfuse = false;
		}
		Debug.Log("enough to fuse: "+canfuse);

		if(canfuse){
			targetEd = getResultISO(sourceEd1.equipDef.id,sourceEd2.equipDef.id);	
		}else{
			targetEd = null;	
		}
		if(sourceEd1 != null && sourceEd1.count <= 0) fuseElement1.setEquipData(null);
		else fuseElement1.setEquipData(sourceEd1);
		if(sourceEd2 != null && sourceEd2.count <= 0) fuseElement2.setEquipData(null);	
		else fuseElement2.setEquipData(sourceEd2);
		fuseResult.setEquipData(targetEd);	
		if(targetEd != null){
			costPrice.text = "" + targetEd.equipDef.silver;
			string s = "";
			fuseResultISOAttribute.text = "" + targetEd.getDescString();
		}else{
			costPrice.text = "0";
			fuseResultISOAttribute.text = "";	
		}
	}
	
	public EquipData getResultISO(int source1,int source2){
		foreach(EquipData ed in EquipManager.Instance.inventoryItemList){
			if(ed.equipDef.type == EquipData.Type.ISO && ed.equipDef.fuseISOCostID.Count > 1){
				int s1 = int.Parse(ed.equipDef.fuseISOCostID[0]);
				int s2 = int.Parse(ed.equipDef.fuseISOCostID[1]);
				if((s1 == source1 && s2 == source2) || (s2 == source1 && s1 == source2)){
					return ed;	
				}
			}
		}
		return null;
	}
}
