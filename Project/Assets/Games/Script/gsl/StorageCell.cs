using UnityEngine;
using System.Collections;

public class StorageCell : MonoBehaviour
{
	private static string Gear_Prime_Checked = "0306-0_40";
	private static string Gear_Prime_UnChecked = "0306-0_40";
	private static string Gear_Checked = "0306-0_65";
	public static string Gear_UnChecked = "0306-0_60";
	private static string ISO_Checked = "0306-0_37";
	public static string ISO_UnChecked = "0306-0_42";
	
	public UISprite Bg;
	public UISprite Icon_Gear;
	public UILabel labelInfo;
	public UILabel labelCount;
	public UIAtlas equipsAtlas;
	public UIAtlas isoAtlas;
	[HideInInspector]public EquipData equipData;
	public Camera uiCamera;
	public StoragePanel storagePanel;
	[HideInInspector]public bool isHighLight = false;
	
	private bool isExist = false;
	private bool isPressed = false;
	private Vector3 startPos = Vector3.zero;
	private Vector3 endPos = Vector3.zero;
	private Vector3 resultPos = Vector3.zero;
	private const float DragTrashhold = 5f;
	[HideInInspector] public Mode mode = Mode.Drag;
	
	[HideInInspector] protected EquipSlotCell currentHighlightEquipSlotCell;
	
	public delegate void StorageCellClicked (StorageCell cell);

	public StorageCellClicked storageCellClicked;
	public delegate void StorageCellDragged (StorageCell cell);
	
	public enum Mode
	{
		Click,
		Drag
	}
	
	void Awake(){
		if (uiCamera == null) {
			uiCamera = NGUITools.FindCameraForLayer (gameObject.layer);
		}
//		storagePanel = NGUITools.FindInParents<StoragePanel> (gameObject);	
		storagePanel = GameObject.Find("5.Storage").GetComponent<StoragePanel>();
	}

	void Start ()
	{
//		if (uiCamera == null) {
//			uiCamera = NGUITools.FindCameraForLayer (gameObject.layer);
//		}
//		storagePanel = NGUITools.FindInParents<StoragePanel> (gameObject);
	}
//	
//	public void init (EquipData ed)
//	{
//		setEquipData (ed);
//	}
	
	public void setEquipData (EquipData ed)
	{		//Gear
		this.equipData = ed;
		this.Bg.spriteName = (storagePanel.currentFilter == StoragePanel.FILTER.OWN_ISO)? ISO_UnChecked:Gear_UnChecked;
		if (ed != null) {
			gameObject.name = ed.equipDef.equipName;
			if(storagePanel != null) storagePanel.isHighLight = true;
			switch(ed.equipDef.type){
			case EquipData.Type.ISO:
				
				Icon_Gear.enabled = true;
				labelInfo.enabled = true;
				labelCount.enabled = true;
				this.Icon_Gear.atlas = this.isoAtlas;
				this.Icon_Gear.spriteName = this.equipData.equipDef.iconID;
				this.Bg.depth = 13;
				if (this.Icon_Gear.GetAtlasSprite () == null) {
					this.Icon_Gear.spriteName = "ISO_default";
				}
				Icon_Gear.MakePixelPerfect ();
				//labelInfo.text = ed.equipDef.equipName ;
				labelInfo.text = string.Format("{0}",Localization.instance.Get("ISO_Name_"+ed.equipDef.id));
				labelCount.text = ed.count.ToString();
				break;

			default:
				Icon_Gear.enabled = true;
				labelInfo.enabled = true;
				labelCount.enabled = false;
				this.Icon_Gear.atlas = this.equipsAtlas;
				this.Icon_Gear.spriteName = this.equipData.equipDef.iconID;
				if (this.Icon_Gear.GetAtlasSprite () == null) {
					this.Icon_Gear.spriteName = "GearDefault";
				}
				Icon_Gear.MakePixelPerfect ();
				//labelInfo.text = ed.equipDef.equipName + "\n<" + ed.uid + ">";
				labelInfo.text = string.Format("{0}\n<{1}{2}>",Localization.instance.Get("Gear_Name_"+ed.equipDef.id),Localization.instance.Get("Gear_Uid"),ed.uid);
				break;
			}
		} else {
			if(storagePanel != null) storagePanel.isHighLight = false;
			Icon_Gear.enabled = false;
			labelInfo.enabled = false;	
			labelCount.enabled = false;	
			gameObject.name = "StorageCell(Clone)";
		}
	}
	
//	public void showIcon(){		//ISO8
//		Icon_ISO8.enabled = true;
//		Icon_Gear.enabled = false;
//		Info.enabled = false;
//		ShowHighLight(this,false);//isHighLight = false;
//	}
	
	private void OnPress (bool isPressed)
	{
		if (isPressed) {
			if (mode == Mode.Click) {
				if (storageCellClicked != null) {
					storageCellClicked (this);	
				}
			} else {
				
				startPos = Utils.toLogicPosition (Input.mousePosition);
				this.isPressed = true;
				if(storagePanel == null || equipData == null)
				{
					return;
				}
				
				currentHighlightEquipSlotCell = storagePanel.getHighLightEquipSlotCell();  
				
				
				if(equipData.equipDef.type == EquipData.Type.WEAPON)
				{
					storagePanel.slot_weapon.SetHighLight(true);
				}
				else if(equipData.equipDef.type == EquipData.Type.ARMOR)
				{
					storagePanel.slot_armor.SetHighLight(true);
				}
				else if(equipData.equipDef.type == EquipData.Type.TRINKET)
				{
					storagePanel.slot_Trinket1.SetHighLight(true);
					storagePanel.slot_Trinket2.SetHighLight(true);
				}
				else if(equipData.equipDef.type == EquipData.Type.ISO)
				{
					storagePanel.slot_iso1.SetHighLight(true);
					storagePanel.slot_iso2.SetHighLight(true);
					storagePanel.slot_iso3.SetHighLight(true);
				}
				if(currentHighlightEquipSlotCell != null)
				{
					
				}
			}
		} else {
//			if (mode == Mode.Click) {
//				return;
//			}
			this.isPressed = false;
			SlotCursor.instance.SetDrag (false);
			
			
			EquipSlotCell slotcell = null;
			if(storagePanel != null)
			{
				slotcell = storagePanel.raycastSlotCell ();
				if(currentHighlightEquipSlotCell != null)
				{
					storagePanel.setHighlightOnSlot(currentHighlightEquipSlotCell);
				}
				else
				{
					storagePanel.clearSlotHighLight();
				}
			}
			if (slotcell == null) {
				// back, cancel the drag
				SlotCursor.instance.SetDrag (false);
				this.setEquipData (this.equipData);
				SetHighLight (true);
				showInfo (true);
				if (storageCellClicked != null)
					storageCellClicked (this);	
			} else {
				if (slotcell.slotType == this.equipData.equipDef.type) {
					EquipData oldEquiptingData = slotcell.equipData;
					//slotcell.setEquipData (this.equipData);
					if(slotcell.slotType == EquipData.Type.ISO){
						if(this.equipData.count <= 0){
							return;
						}
						if(slotcell.fuseISODlg != null){
							slotcell.setEquipData (this.equipData);			
							//saveDataAndUpdate_ForISO(slotcell.slot,oldEquiptingData,this.equipData);
							slotcell.fuseISODlg.UpdateFuseView();
						}else{
							if(oldEquiptingData == null){
								slotcell.setEquipData (this.equipData);			
								saveDataAndUpdate_ForISO(slotcell.slot,oldEquiptingData,this.equipData);
							}else{
	//							string s = "Are you sure to deploy [00FFFF]" + this.equipData.equipDef.equipName
	//								+ " [FFFFFF]and destory [00FFFF]" + oldEquiptingData.equipDef.equipName + "[FFFFFF]?";
								string s = string.Format(Localization.instance.Get("UI_CommonDlg_DeployISO"),this.equipData.equipDef.equipName,oldEquiptingData.equipDef.equipName);
								CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
								dlg.onYes = delegate {
									MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
									slotcell.setEquipData (this.equipData);
									saveDataAndUpdate_ForISO(slotcell.slot,oldEquiptingData,this.equipData);
								};
								dlg.onNo = delegate {
									MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
									SlotCursor.instance.SetDrag (false);
									this.setEquipData (this.equipData);
									SetHighLight (true);
									showInfo (true);
									if (storageCellClicked != null)
										storageCellClicked (this);	
								};
							}
						}
						//saveDataAndUpdate_ForISO(slotcell.slot,oldEquiptingData,this.equipData);
					}else{
						TeamDlg.instance.suitEquipUp(this.equipData);
						slotcell.setEquipData (this.equipData);
						saveDataAndUpdate_ForGear(slotcell.slot,oldEquiptingData,this.equipData);
					}
							
					if(storagePanel != null)
					{
						storagePanel.setHighlightOnSlot(slotcell);
					}
					if(oldEquiptingData == null) {
						SetHighLight(false);
						if(storagePanel != null && !storagePanel.isHighLight) 
							storagePanel.SetInfoBar(null,null,null);
					}
					else 
						SetHighLight(true);
				}else {
					//miss type , cancel the drag
					SlotCursor.instance.SetDrag (false);
					this.setEquipData (this.equipData);
					SetHighLight (true);
					showInfo (true);	
				}
			}
		}
	}
	public void saveDataAndUpdate_ForISO(EquipData.Slots slot,EquipData tobeUnequip, EquipData tobeEquip){
		EquipSlotCell slotcell = null;
		if(storagePanel != null){
			slotcell = storagePanel.raycastSlotCell ();	
		}
		if(tobeUnequip!=null){
			EquipData ed = EquipManager.Instance.getEquipDataByType(tobeUnequip.equipDef.id);
			//ed.count ++;
			//if(slotcell.fuseISODlg != null) ed.count++;
			TeamDlg.instance.heroData.unEquipObj(slot);
		}
		if(tobeEquip != null){
			EquipData ed = EquipManager.Instance.getEquipDataByType(tobeEquip.equipDef.id);
			if(slotcell.fuseISODlg == null) ed.count --;
			TeamDlg.instance.heroData.equipObj(slot,tobeEquip);
		}
		
		//this.setEquipData(tobeEquip);
		if(storagePanel != null)
		{
			storagePanel.updateISO();
		}
		MusicManager.playEffectMusic("SFX_Equip_1a");
		UserInfo.instance.saveAll();
		TeamDlg.instance.UpdateData();
	}
	public void saveDataAndUpdate_ForGear(EquipData.Slots slot,EquipData tobeUnequip, EquipData tobeEquip){
		if(tobeUnequip!=null){
			EquipManager.Instance.inventoryItemList.Add(tobeUnequip);
			TeamDlg.instance.heroData.unEquipObj(slot);
		}
		EquipManager.Instance.inventoryItemList.Remove(tobeEquip);
		TeamDlg.instance.heroData.equipObj(slot,tobeEquip);
		
		this.setEquipData(tobeUnequip);
		MusicManager.playEffectMusic("SFX_Equip_1a");		
		UserInfo.instance.saveAll();
		TeamDlg.instance.UpdateData();
	}
	
	
	
	void Update ()
	{
		if(storagePanel != null && (storagePanel.currentFilter == StoragePanel.FILTER.OWN_GEAR ||
			storagePanel.currentFilter == StoragePanel.FILTER.OWN_ISO)){
			mode = Mode.Drag;	
		}else{
			mode = Mode.Click;	
		}
		if (mode != Mode.Drag)
			return;
		if (isPressed) {
			float distance = Vector2.Distance (Utils.toLogicPosition (Input.mousePosition), startPos);
			if (distance > DragTrashhold) {
				SetSlotCursor ();	
			} else {
			}
		}
	}

	private void showInfo (bool b)
	{
		if (this.equipData != null) {
					
		}		
	}

	public void SetHighLight (bool b)
	{
		if(b){
			if(storagePanel != null)
			{
				foreach (StorageCell cell in storagePanel.storageCellList) {
					if (cell != this)
						ShowHighLight (cell, false);
				}
			}
		}
		ShowHighLight (this, b);
	}
	
	public void ShowHighLight (bool highLight)
	{
		this.ShowHighLight(this, highLight);
	}
	
	private void ShowHighLight (StorageCell cell, bool highLight)
	{
		this.isHighLight = false;
		if(cell.equipData == null){
			if (highLight) {
				cell.Bg.spriteName = (storagePanel.currentFilter == StoragePanel.FILTER.OWN_ISO)?ISO_Checked:Gear_Checked;
			}else{
				cell.Bg.spriteName = (storagePanel.currentFilter == StoragePanel.FILTER.OWN_ISO)?ISO_UnChecked:Gear_UnChecked;
			}
			if(storagePanel != null)
			{
				storagePanel.setHighlightOnSlot(null);
				storagePanel.SetInfoBar (null,null,null);
			}
		}else{
			if (highLight) {
				cell.Bg.spriteName = (cell.equipData.equipDef.type == EquipData.Type.ISO)? ISO_Checked:Gear_Checked;
				cell.Bg.MakePixelPerfect ();		
				if(storagePanel != null)
				{
					storagePanel.setHighlightOnSlot(null);
					storagePanel.SetInfoBar (this.equipData,this.Icon_Gear.atlas,this.Icon_Gear.spriteName);
				}
			} else {
				cell.Bg.spriteName = (cell.equipData.equipDef.type == EquipData.Type.ISO)? ISO_UnChecked:Gear_UnChecked;
				cell.Bg.MakePixelPerfect ();		
			}
		}
	}
	
	public void SetSlotCursor ()
	{
		if (this.equipData != null) {
			switch(this.equipData.equipDef.type){
			case EquipData.Type.ISO:
					SlotCursor.instance.SetDrag (true);	
					SlotCursor.instance.SetEquipData (this.equipData,Icon_Gear.atlas,Icon_Gear.spriteName);	
					Icon_Gear.enabled = true;
					labelInfo.enabled = true;				
				break;
			default:
					SlotCursor.instance.SetDrag (true);	
					SlotCursor.instance.SetEquipData (this.equipData,Icon_Gear.atlas,Icon_Gear.spriteName);	
					Icon_Gear.enabled = false;
					labelInfo.enabled = false;
				break;
			}
		} else {
			SlotCursor.instance.SetDrag (false);
			SlotCursor.instance.SetEquipData (null,null,null);
		}
	}
}
