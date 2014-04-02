using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoragePanel : MonoBehaviour
{
	public Camera uiCamera;
	public GameObject ListOffset;
	public GameObject CheckBox_Page;
	public GameObject btnBuy;
	public GameObject btnSell;
	public GameObject btnUpgrade;
	public UILabel upgradeFrom;
	public UILabel upgradeTo;
	public UILabel upgradeSilver;
	
	public GameObject btnFuse;
	public UISprite CheckBox_Gear;
	public UISprite CheckBox_ISO8;
	public List<UISprite> sprite_PageList = new List<UISprite> ();
	public List<UILabel> label_PageList = new List<UILabel> ();
	public Vector2 startPos = Vector2.zero;
	public Vector2 spacing = Vector2.zero;
	public List<StorageCell> storageCellList = new List<StorageCell> ();
	public bool isHighLight = false;
	public EquipSlotCell slot_weapon;
	public EquipSlotCell slot_armor;
	public EquipSlotCell slot_Trinket1;
	public EquipSlotCell slot_Trinket2;
	public EquipSlotCell slot_iso1;
	public EquipSlotCell slot_iso2;
	public EquipSlotCell slot_iso3;
	
	public EquipSlotCell slot_isoSource1;
	public EquipSlotCell slot_isoSource2;
	public EquipSlotCell slot_isoResult;
	public EquipSlotCell slot_noFuse;
	
	public UISprite infoBg;
	public UISprite infoToggle;
	public UISprite infoLine;
	
	public List<UISprite> pageIndexList = new List<UISprite>();
	
	private int curPageIndex;
	private List<GameObject> pageList = new List<GameObject>();
	
//	private string selectISOID;
	protected EquipData selectEquipData;
	private const int MAX_STORAGE_SLOT = 15;
	private const int MAX_PAGE_COUNT = 3;
	private const int MAX_ROW_COUNT_PER_PAGE = 3;
	private const int MAX_COL_COUNT_PER_PAGE = 5;
	public enum FILTER
	{
		OWN_GEAR,
		OWN_ISO,
		STORE_HERO_APPLICABLE,
		STORE_All_NONE_HERO_SPECIFIC,
		STORE_All_HERO_SPECIFIC,
		STORE_ALL,
		STORE_NONE
	}
	
	public FILTER currentFilter = FILTER.OWN_GEAR;
		
	void Start ()
	{
		if (uiCamera == null) {
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);	
		}
//		initStorageCellSlotList();
		initPageList();
		
		curPageIndex = 0;
		setHighLightOnIcon();
	}
	
	void Update(){
		if(pageList.Count <= 0) return;
		for(int n = 0;n < pageList.Count;n++){
			GameObject go = ListOffset.GetComponent<UICenterOnChild>().hotCellGo;
			if(pageList[n] == go){
				curPageIndex = n;
				setHighLightOnIcon();	
			}
		}
	}
	
	public void setPageToZero(){
		curPageIndex = 0;
		UICenterOnChild centerChild = ListOffset.GetComponent<UICenterOnChild>();
		if(ListOffset.transform.childCount > 0) centerChild.jumpTo(0);
		setHighLightOnIcon();
	}
	
	void setHighLightOnIcon(){
		foreach(UISprite us in pageIndexList){
			string s = "pageIndex_" + curPageIndex;
			if(us.name == s){
				us.color = new Color(1,1,1,1);	
			}else{
				us.color = new Color(1,1,1,0.3f);	
			}
		}
	}
	
	private void initPageList(){
		for(int count = 0; count < MAX_PAGE_COUNT;count++){
			GameObject go = new GameObject();
			go.name = "Page" + count;
			go.transform.parent = ListOffset.transform;
			go.transform.localPosition = new Vector3(spacing.x * MAX_COL_COUNT_PER_PAGE*count + 20*count,0,0);
			go.transform.localScale = Vector3.one;
			pageList.Add(go);
			initStorageCellSlotList(go);
		}
	}
	
	private void initStorageCellSlotList (GameObject go)
	{
		for (int row = 0; row < MAX_ROW_COUNT_PER_PAGE; row++) {
			for (int col = 0; col < MAX_COL_COUNT_PER_PAGE; col++) {
				Vector2 pos = new Vector2 (startPos.x + spacing.x * col, startPos.y + spacing.y * row);
				initStorageCellSlot(pos,go);
			}
		}
	}
	
	private void initStorageCellSlot (Vector2 pos,GameObject page)
	{
		GameObject prefab = Resources.Load("gsl_cell/StorageCell") as GameObject;
		
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = page.transform;
		go.transform.localPosition = new Vector3 (pos.x, pos.y, 0);
		go.transform.localScale = Vector3.one;
		StorageCell cell = go.GetComponent<StorageCell>();
		cell.storageCellClicked = this.StorageCellClicked;
		if (cell != null)
			storageCellList.Add(cell);
	}
	
	public void updateStorageCollection (int pageIndex)
	{
		// determin input set
		ICollection inputCollection = null;
		switch (currentFilter) {
		case FILTER.STORE_ALL:
		case FILTER.STORE_All_HERO_SPECIFIC:
		case FILTER.STORE_All_NONE_HERO_SPECIFIC:
		case FILTER.STORE_HERO_APPLICABLE:
		case FILTER.STORE_NONE:
			inputCollection = EquipManager.Instance.allEquipHashtable.Values;
			break;
		case FILTER.OWN_GEAR:
		case FILTER.OWN_ISO:
			inputCollection = EquipManager.Instance.inventoryItemList;
			break;
		}
		
		//determin output set
		List<EquipData> outputCollection = new List<EquipData> ();
		HeroData heroData = TeamDlg.instance.heroData;
		foreach (EquipData ed in inputCollection) {
			switch (currentFilter) {
			case FILTER.STORE_HERO_APPLICABLE:
				if (ed.equipDef.type != EquipData.Type.ISO && (ed.equipDef.specialType.Count == 0 || ed.equipDef.specialType.Contains(heroData.type))) {
					outputCollection.Add(ed);	
				}	
				break;
			case FILTER.STORE_All_NONE_HERO_SPECIFIC:
				if (ed.equipDef.type != EquipData.Type.ISO && ed.equipDef.specialType.Count == 0) {
					outputCollection.Add(ed);	
				}
				break;
			case FILTER.STORE_All_HERO_SPECIFIC:
				if (ed.equipDef.type != EquipData.Type.ISO && ed.equipDef.specialType.Count > 0) {
					outputCollection.Add(ed);	
				}
				break;
			case FILTER.STORE_ALL:
				if (ed.equipDef.type != EquipData.Type.ISO) {
					outputCollection.Add(ed);
				}
				break;
			case FILTER.STORE_NONE:
				
				break;
			case FILTER.OWN_GEAR:
				if (ed.equipDef.type != EquipData.Type.ISO && (ed.equipDef.specialType.Count == 0 || ed.equipDef.specialType.Contains(heroData.type))) {
					outputCollection.Add(ed);
				}
				break;
			case FILTER.OWN_ISO:
				if (ed.equipDef.type == EquipData.Type.ISO) {
					outputCollection.Add(ed);
				}
				break;
			}
		}
		
		outputCollection.Sort(delegate(EquipData x, EquipData y) {
			return (x.equipDef.id < y.equipDef.id) ? -1 : 1;
		});

		for (int n = 0; n < storageCellList.Count; n++) {
			StorageCell cell = storageCellList [n];
			int index = n + pageIndex * MAX_STORAGE_SLOT;
			cell.name = string.Format("Storage{0}", n+1);
			cell.setEquipData(index >= outputCollection.Count
							? null : outputCollection [index]);
		}	
		Debug.Log("updateStorageCollection ,currentFIlter=" + currentFilter + " count ="+outputCollection.Count);
	}
	
	public void OnGearBtnClick (bool isChecked)
	{
		if (isChecked) {
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			TeamDlg.instance.currentTag = TeamDlg.Tag.TAG_GEAR;
//			CheckBox_Gear.spriteName = "GEAR_Checked";
//			CheckBox_Gear.depth = 3;
//			CheckBox_Page.SetActive(true);
			OnPage2BtnClick(false);
			OnPage3BtnClick(false);
			OnPage4BtnClick(false);
			OnPage1BtnClick(true);
			clearHighLight();
			currentFilter = FILTER.OWN_GEAR;
			this.updateStorageCollection(0);
			setPageToZero();
		}
//		} else {
//			CheckBox_Gear.spriteName = "GEAR_Unchecked";
//			CheckBox_Gear.depth = 2;	
//		}
	}
	
	public void OnISO8BtnClick (bool isChecked)
	{
		if (isChecked) {
			TeamDlg.instance.currentTag = TeamDlg.Tag.TAG_ISO;
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//			CheckBox_ISO8.spriteName = "ISO8_Checked";
//			CheckBox_ISO8.depth = 3;
			updateISO();
			setPageToZero();
		}else{
			OnToggleBtnClick();
		}
//		} else {
//			CheckBox_ISO8.spriteName = "ISO8_Unchecked";
//			CheckBox_ISO8.depth = 2;	
//		}
	}
	
	public void updateISO()
	{
//		CheckBox_Page.SetActive(false);
		clearHighLight();
		currentFilter = FILTER.OWN_ISO;
		this.updateStorageCollection(0);	
	}
	
	public void OnPage1BtnClick (bool isChecked)
	{
		clearHighLight();
		updatePageBtn(0, isChecked);
		if (isChecked) {
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			updateStorageCollection(0);
		}
	}
	
	public void OnPage2BtnClick (bool isChecked)
	{
		clearHighLight();
		updatePageBtn(1, isChecked);
		if (isChecked){
			updateStorageCollection(1);
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
	}
	
	public void OnPage3BtnClick (bool isChecked)
	{
		clearHighLight();
		updatePageBtn(2, isChecked);
		if (isChecked){
			updateStorageCollection(2);
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
	}
	
	public void OnPage4BtnClick (bool isChecked)
	{
		clearHighLight();
		updatePageBtn(3, isChecked);
		if (isChecked){
			updateStorageCollection(3);
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
	}
	
	private void updatePageBtn (int index, bool isChecked)
	{
//		if (isChecked) {
////			sprite_PageList [index].spriteName = "StoragePage_Checked";
////			sprite_PageList [index].depth = 1;
//			label_PageList [index].color = new Color (1f, 1f, 1f, 1f);
//		} else {
////			sprite_PageList [index].spriteName = "StoragePage_Unchecked";
////			sprite_PageList [index].depth = 0;
//			label_PageList [index].color = new Color (0.35f, 1f, 1f, 1f);
//		}
	}
	
	public void SetInfoBar (EquipData ed, UIAtlas atlas , string spriteName )
	{
		if(ed == null) {
			TeamDlg.instance.infoPriceLabel.text = "";
			TeamDlg.instance.infoDescLabel.text = "";
			TeamDlg.instance.infoTitleLabel.text = "";
			btnBuy.SetActive(false);
			btnFuse.SetActive(false);	
			btnSell.SetActive(false);	
			btnUpgrade.SetActive(false);	
			TeamDlg.instance.IconGearGroup.SetActive(false);
			return;
		}
		TeamDlg.instance.IconGearGroup.SetActive(true);
		if(atlas != null) TeamDlg.instance.Icon.atlas = atlas;
		if(spriteName != null) TeamDlg.instance.Icon.spriteName = spriteName;
		TeamDlg.instance.Icon.MakePixelPerfect();
		string eqname = Localization.instance.Get((ed.equipDef.type==EquipData.Type.ISO?"ISO_Name_":"Gear_Name_")+ed.equipDef.id);
		TeamDlg.instance.infoTitleLabel.text = eqname;
		TeamDlg.instance.infoDescLabel.text = ed.getDescString();
		
//		if(ed.equipDef.equipEft != null)
//		{
//			infoLabel.text += "\n"+ed.equipDef.equipEft.eName + ": +"+ed.eftNum;	
//		}
		TeamDlg.instance.infoDescLabel.text = ed.getDescString();
		
		string price = "";
		if(ed.equipDef.gold>0){
			price += ((price == "")?"":"\n") + ed.equipDef.gold + " GOLD";
		}
		if(ed.equipDef.silver>0){
			price += ((price == "")?"":"\n") + ed.equipDef.silver + " SILVER";
		}
		if(ed.equipDef.commandPoints>0){
			price += ((price == "")?"":"\n") + ed.equipDef.commandPoints + " CP";
		}
		Debug.Log("price = "+price + string.Format("  {0},{1},{2},{3}",ed.equipDef.gold,ed.equipDef.silver,ed.equipDef.commandPoints,ed.equipDef.id));
		
		selectEquipData = ed;
		
		switch(currentFilter){
			case FILTER.OWN_GEAR:
				btnBuy.SetActive(false);
				btnFuse.SetActive(false);	
				btnSell.SetActive(true);	
				if(ed.equipDef.type != EquipData.Type.TRINKET && ed.canUpgrade())
				{
					upgradeFrom.text = "LV "+ed.getCurrentLv();
					upgradeTo.text = "LV "+ (ed.getCurrentLv()+1);
					upgradeSilver.text = ed.getLvUpMoney().ToString();;
				
					btnUpgrade.SetActive(true);	
				}
				else
				{
					btnUpgrade.SetActive(false);	
				}
				TeamDlg.instance.infoIconBgGear.gameObject.SetActive(true);
				TeamDlg.instance.infoIconBgISO.gameObject.SetActive(false);

				TeamDlg.instance.infoPriceLabel.text = "";
				break;
			case FILTER.OWN_ISO:
				btnBuy.SetActive(false);
				//btnFuse.SetActive(true);	
				btnSell.SetActive(false);	
				btnUpgrade.SetActive(false);	
				TeamDlg.instance.infoPriceLabel.text = "";
				TeamDlg.instance.infoIconBgGear.gameObject.SetActive(false);
				TeamDlg.instance.infoIconBgISO.gameObject.SetActive(true);
				break;
			case FILTER.STORE_HERO_APPLICABLE:
			case FILTER.STORE_All_NONE_HERO_SPECIFIC:
			case FILTER.STORE_All_HERO_SPECIFIC:
			case FILTER.STORE_ALL:	
				btnBuy.SetActive(true);
				btnFuse.SetActive(false);	
				btnSell.SetActive(false);	
				btnUpgrade.SetActive(false);	
				TeamDlg.instance.infoPriceLabel.text = price;
				TeamDlg.instance.infoIconBgGear.gameObject.SetActive(true);
				TeamDlg.instance.infoIconBgISO.gameObject.SetActive(false);
			break;
		}
	}
	
	public void StorageCellClicked (StorageCell cell)
	{
//		Debug.LogError("StorageCellClicked");
		TeamDlg.instance.StorageCellClicked (cell);
	}

	public EquipSlotCell raycastSlotCell ()
	{
		if (hitSlot(slot_weapon))
			return slot_weapon;
		if (hitSlot(slot_armor))
			return slot_armor;
		if (hitSlot(slot_Trinket1))
			return slot_Trinket1;
		if (hitSlot(slot_Trinket2))
			return slot_Trinket2;
		if (hitSlot(slot_iso1))
			return slot_iso1;
		if (hitSlot(slot_iso2))
			return slot_iso2; 
		if (hitSlot(slot_iso3))
			return slot_iso3; 
		if(hitSlot(slot_isoSource1))
			return slot_isoSource1;
		if(hitSlot(slot_isoSource2))
			return slot_isoSource2;
		return null;
	}

	private bool hitSlot (EquipSlotCell cell)
	{
		Ray ray = this.uiCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		if (cell != null && cell.collider.Raycast(ray, out hit, 10000)) {
			return true;	
		} else {
			return false;
		}
//		Vector3 logicMouseV3 = new Vector3(logicMouse.x,logicMouse.y,cell.transform.position.z);
//		if(cell.collider.
//			return true;	
//		}else{
//			return false;
//		}
	}
	
	public EquipSlotCell getHighLightEquipSlotCell()
	{
		if(this.slot_weapon.isHighLight)
		{
			return this.slot_weapon;
		}
		else if(this.slot_armor.isHighLight)
		{
			return this.slot_armor;
		}
		else if(this.slot_Trinket1.isHighLight)
		{
			return this.slot_Trinket1;
		}
		else if(this.slot_Trinket2.isHighLight)
		{
			return this.slot_Trinket2;
		}
		else if(this.slot_iso1.isHighLight)
		{
			return this.slot_iso1;
		}
		else if(this.slot_iso2.isHighLight)
		{
			return this.slot_iso2;
		}
		else if(this.slot_iso3.isHighLight)
		{
			return this.slot_iso3;
		}
		else if(this.slot_isoSource1.isHighLight)
		{
			return this.slot_isoSource1;
		}
		else if(this.slot_isoSource2.isHighLight)
		{
			return this.slot_isoSource2;
		}
		return null;
	}
	
	

	public void setHighlightOnSlot (EquipSlotCell cell)
	{
		if(null == cell){
			Debug.Log("clear all hight");
		}	
		this.slot_weapon.SetHighLight(this.slot_weapon == cell);
		this.slot_armor.SetHighLight(this.slot_armor == cell);
		this.slot_Trinket1.SetHighLight(this.slot_Trinket1 == cell);
		this.slot_Trinket2.SetHighLight(this.slot_Trinket2 == cell);
		this.slot_iso1.SetHighLight(this.slot_iso1 == cell);
		this.slot_iso2.SetHighLight(this.slot_iso2 == cell);
		this.slot_iso3.SetHighLight(this.slot_iso3 == cell);
		this.slot_isoSource1.SetHighLight(this.slot_isoSource1 == cell);
		this.slot_isoSource2.SetHighLight(this.slot_isoSource2 == cell);
	}
	
	public void clearSlotHighLight()
	{
		this.slot_weapon.SetHighLight(false);
		this.slot_armor.SetHighLight(false);
		this.slot_Trinket1.SetHighLight(false);
		this.slot_Trinket2.SetHighLight(false);
		this.slot_iso1.SetHighLight(false);
		this.slot_iso2.SetHighLight(false);
		this.slot_iso3.SetHighLight(false);
		this.slot_isoSource1.SetHighLight(false);
		this.slot_isoSource2.SetHighLight(false);
	}
	
	public void updateWithEquipData(){
		foreach (StorageCell cell in storageCellList) {
			if (cell != null)
				cell.setEquipData(cell.equipData);
		}
	}
	public void clearHighLight ()
	{
		foreach (StorageCell cell in storageCellList) {
			if (cell != null)
				cell.SetHighLight(false);
		}
		SetInfoBar(null,null,null);
	}
	
	public void OnFuseISOBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		List<EquipData> templist = new List<EquipData>();
//		foreach(EquipData ed in EquipManager.Instance.inventoryItemList)
//		{
//			if(ed.equipDef.type == EquipData.Type.ISO)
//			{
//				templist.Add(ed);
//			}
//		}
		
		iTween.ScaleTo(infoBg.gameObject,new Hashtable(){{"x",842f},{"delay",0f},{"time",.2f},{"easetype",iTween.EaseType.linear}});
		StartCoroutine(delayShowInfoToggle());
		
		TeamDlg.instance.showFuseISODlg( selectEquipData.equipDef.id);
	}
	
	private IEnumerator delayShowInfoToggle(){
		yield return new WaitForSeconds(0.2f);
		infoToggle.gameObject.SetActive(true);	
		infoLine.gameObject.SetActive(true);
	}
	
	public void OnToggleBtnClick(){
		infoToggle.gameObject.SetActive(false);
		infoLine.gameObject.SetActive(false);
		TeamDlg.instance.cancelFuseISODlg();
		iTween.ScaleTo(infoBg.gameObject,new Hashtable(){{"x",620},{"delay",0f},{"time",.1f},{"easetype",iTween.EaseType.linear}});	
	}
	
	public void OnBuyGearBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		//string s = "Are you sure to buy [00FFFF]" + selectEquipData.equipDef.equipName + " [FFFFFF]with [00FFFF]";
		string s = string.Format(Localization.instance.Get("UI_CommonDlg_BuyGear"),selectEquipData.equipDef.equipName);
		if(selectEquipData.equipDef.silver != 0)
		{
			if(selectEquipData.equipDef.silver > UserInfo.instance.getSilver()){
				MusicManager.playEffectMusic("SFX_Error_Message_1c");
				//s = "You don't have enough silver.";
				s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughSilver"));
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
				dlg.setOKDlg();
				return;
			}else
//				s += selectEquipData.equipDef.silver + " [FFFFFF][Silver] ";
				s += string.Format("{0} [FFFFFF][Silver] ",selectEquipData.equipDef.silver);
		}
		else if(selectEquipData.equipDef.gold != 0)
		{
			if(selectEquipData.equipDef.gold > UserInfo.instance.getGold()){
				MusicManager.playEffectMusic("SFX_Error_Message_1c");
				//s = "You don't have enough gold.";
				s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughGold"));
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
				dlg.setOKDlg();
				return;
			}else
				//s += selectEquipData.equipDef.gold + " [FFFFFF][Gold] "; 
				s += string.Format("{0} [FFFFFF][Gold]",selectEquipData.equipDef.gold); 
		}
		else if(selectEquipData.equipDef.commandPoints != 0)
		{
			if(selectEquipData.equipDef.commandPoints > UserInfo.instance.getCommandPoints()){
				MusicManager.playEffectMusic("SFX_Error_Message_1c");
				//s = "You don't have enough CommandPoints.";
				s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughCommandPoints"));
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
				dlg.setOKDlg();
				return;
			}else
				//s += selectEquipData.equipDef.commandPoints + " [FFFFFF][CPoint] "; 
				s += string.Format("{0} [FFFFFF][Command Points]",selectEquipData.equipDef.commandPoints);
		}
		s += "?";
		CommonDlg cdlg = DlgManager.instance.ShowCommonDlg(s);
		cdlg.onYes = buyEquip;
		cdlg.onNo = delegate {
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		};
	}
	
	public void buyEquip()
	{
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(selectEquipData.equipDef.silver > 0) UserInfo.instance.consumeSilver(selectEquipData.equipDef.silver);
		if(selectEquipData.equipDef.gold > 0) UserInfo.instance.consumeGold(selectEquipData.equipDef.gold);
		if(selectEquipData.equipDef.commandPoints > 0) UserInfo.instance.consumeCommandPoints(selectEquipData.equipDef.commandPoints);
		EquipManager.Instance.inventoryItemList.Add(EquipFactory.create(selectEquipData.equipDef.id));
		UserInfo.instance.saveAll();
	}
	
	int levelUpMoney = 0;
	
	public void OnUpgradeGearBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		string s = "";
				
		CommonDlg cdlg = DlgManager.instance.ShowCommonDlg(s);
		
		levelUpMoney = selectEquipData.getLvUpMoney();
		
		if(selectEquipData.equipDef.silver != 0 && levelUpMoney > UserInfo.instance.getSilver())
		{
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
			//s = "You don't have enough silver.";
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughSilver"));
			cdlg.setOKDlg();
		}
		else if(selectEquipData.equipDef.gold != 0 && levelUpMoney > UserInfo.instance.getGold())
		{
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
			//s = "You don't have enough gold.";
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughGold"));
			cdlg.setOKDlg();
		}
		else if(selectEquipData.equipDef.commandPoints != 0 && levelUpMoney > UserInfo.instance.getCommandPoints())
		{
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
//			s = "You don't have enough CommandPoints.";
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughCommandPoints"));
			cdlg.setOKDlg();
		}
		else if(!selectEquipData.canUpgrade())
		{
			//s = "It's already in the highest level.";
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_UpgradeEquip_HighestLevel"));
			cdlg.setOKDlg();
		}
		else
		{
			
			//s = "Are you sure to upgrade it to Lv " +  selectEquipData.getNextLv() + "\n";
			if(selectEquipData.equipDef.silver != 0)
			{
				//s += "with " + levelUpMoney + " [Silver]?\n";
				s = string.Format(Localization.instance.Get("UI_CommonDlg_UpgradeEquip"),selectEquipData.getNextLv(),levelUpMoney);
				s += " [Silver]?\n";
			}
			else if(selectEquipData.equipDef.gold != 0)
			{
//				s += "with " + levelUpMoney + " [Gold]?\n";
				s = string.Format(Localization.instance.Get("UI_CommonDlg_UpgradeEquip"),selectEquipData.getNextLv(),levelUpMoney);
				s += " [Gold]?\n";
			}
			else if(selectEquipData.equipDef.commandPoints != 0)
			{
//				s += "with " + levelUpMoney + " [CPoint]?\n";
				s = string.Format(Localization.instance.Get("UI_CommonDlg_UpgradeEquip"),selectEquipData.getNextLv(),levelUpMoney);
				s += " [Command Points]?\n";
			}
			
			foreach(Effect eft in selectEquipData.equipEftList)
			{
				s += "\n"+ eft.eName + ": " + eft.num  + " >> ";
				s += selectEquipData.getNextLvData((int)eft.num);
			}
			
			cdlg.onYes = upgradeGear;
			cdlg.onNo = delegate {
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			};
		}
		
		
		cdlg.commonLabel.text = s;
	}
	
	protected void upgradeGear()
	{
		MusicManager.playEffectMusic("SFX_Item_Upgrade_1c");
		selectEquipData.levelUp();
		
		if(selectEquipData.equipDef.silver != 0)
		{
			UserInfo.instance.consumeSilver(levelUpMoney);
		}
		else if(selectEquipData.equipDef.gold != 0)
		{
			UserInfo.instance.consumeGold(levelUpMoney);
		}
		else if(selectEquipData.equipDef.commandPoints != 0)
		{
			UserInfo.instance.consumeCommandPoints(levelUpMoney);
		}
		
		UserInfo.instance.saveAll();
		
		SetInfoBar(selectEquipData,null,null);
	}
	
	public void OnSellGearBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.Log("OnSellGearBtnClick");	
		
		//string s = "Are you sure to sell [00FFFF]" + selectEquipData.equipDef.equipName + " [FFFFFF]for [00FFFF]";
		string s = string.Format(Localization.instance.Get("UI_CommonDlg_Sell"),selectEquipData.equipDef.equipName);
		if(selectEquipData.equipDef.silver != 0)
		{
//			s += selectEquipData.equipDef.silver + " [FFFFFF][Silver] "; 
			s += string.Format("{0} [FFFFFF][Silver] ",selectEquipData.equipDef.silver);
		}
		else if(selectEquipData.equipDef.gold != 0)
		{
//			s += selectEquipData.equipDef.gold + " [FFFFFF][Gold] "; 
			s += string.Format("{0} [FFFFFF][Gold] ",selectEquipData.equipDef.gold);
		}
		else if(selectEquipData.equipDef.commandPoints != 0)
		{
//			s += selectEquipData.equipDef.commandPoints + " [FFFFFF][CPoint] "; 
			s += string.Format("{0} [FFFFFF][Command Points] ",selectEquipData.equipDef.commandPoints);
		}
		s += "?";
		CommonDlg cdlg = DlgManager.instance.ShowCommonDlg(s);
		cdlg.onYes = sellEquip;
		cdlg.onNo = delegate {
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		};
	}
	
	public void sellEquip()
	{
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		UserInfo.instance.addSilver(selectEquipData.equipDef.silver);
		UserInfo.instance.addGold(selectEquipData.equipDef.gold);
		UserInfo.instance.addCommandPoints(selectEquipData.equipDef.commandPoints);
		foreach(StorageCell cell in storageCellList){
			if(cell != null && cell.equipData == selectEquipData){
				cell.setEquipData(null);	
				cell.SetHighLight(false);
				SetInfoBar(null,null,null);
				break;
			}
		}
		
		if(slot_weapon.equipData == selectEquipData){
			slot_weapon.setEquipData(null);
			SetInfoBar(null,null,null);
			TeamDlg.instance.heroData.equipHash.Remove(EquipData.Slots.WEAPON);
		}
		if(this.slot_armor.equipData == selectEquipData){
			slot_armor.setEquipData(null);
			SetInfoBar(null,null,null);
			TeamDlg.instance.heroData.equipHash.Remove(EquipData.Slots.ARMOR);
		}
		if(this.slot_Trinket1.equipData == selectEquipData){
			slot_Trinket1.setEquipData(null);
			SetInfoBar(null,null,null);
			TeamDlg.instance.heroData.equipHash.Remove(EquipData.Slots.TRINKET1);
		}
		if(this.slot_Trinket2.equipData == selectEquipData){
			slot_Trinket2.setEquipData(null);
			SetInfoBar(null,null,null);
			TeamDlg.instance.heroData.equipHash.Remove(EquipData.Slots.TRINKET2);
		}
		
		EquipManager.Instance.inventoryItemList.Remove(selectEquipData);
		UserInfo.instance.saveAll();
	}
	
	public void OnGUI ()
	{
		GUILayout.Space(350);
		if (GUILayout.Button("AddRandom")) {
			int i = Random.Range(0, 100);
			int id = 1002;
		
			if (i < 25) {
				id = Random.Range(1000, 1010);
			} else if (i <50) {
				id = Random.Range(2000, 2010);
			} else if (i < 100) {
				id = Random.Range(3006, 3010);
			}
		
			Debug.LogError("reward items " + id);
			EquipData rewardEquip;
			rewardEquip = EquipManager.Instance.allEquipHashtable [id] as EquipData;
			EquipData equipData = rewardEquip.clone();
			equipData.initUidTemp();
			EquipManager.Instance.inventoryItemList.Add(equipData);
			UserInfo.instance.savePackage();			
			this.updateStorageCollection(0);
		}
	}
	
}
