using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipSlotCell : MonoBehaviour {
	private static string Gear_Prime_Checked = "0306-0_40";
	private static string Gear_Prime_UnChecked = "0306-0_40";
	private static string Gear_Checked = "0306-0_65";
	private static string Gear_UnChecked = "0306-0_60";
	private static string ISO_Checked = "0306-0_37";
	private static string ISO_UnChecked = "0306-0_42";
	
	public static EquipSlotCell instance;
	//public HeroData.Slot slot;
	public EquipData.Type slotType;
	public EquipData.Slots slot;
	public UISprite Bg;
	public UISprite Icon_Gear;
	public UILabel Info;
	//public UILabel ISOCount;
	[HideInInspector] public EquipData equipData;
	public StoragePanel storagePanel;
	[HideInInspector] public Camera uiCamera;
	
	public FuseISODlg fuseISODlg;
	
	public enum Mode
	{
		Click,
		Drag
	}
	public Mode mode = Mode.Drag;
	public bool isHighLight = false;
	
	private bool isPressed = false;
	private bool isExist = false;
	private Vector3 startPos = Vector3.zero;
	private Vector3 endPos = Vector3.zero;
	private Vector3 resultPos = Vector3.zero;
	private const float DragTrashhold = 5f;
	
	void Start () {
		instance = this;
		if(uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
		}
	}
	
	void Update(){
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
	
	private void OnPress (bool isPressed){
		if (isPressed) {
			if (mode == Mode.Click) {
				OnCellClicked ();	
			} else {
				startPos = Utils.toLogicPosition (Input.mousePosition);
				this.isPressed = true;
			}
		} else {
			this.isPressed = false;
			SlotCursor.instance.SetDrag (false);	
			StorageCell storageCell = raycastSlotCell();
			storagePanel.clearHighLight();;
			if (storageCell == null) {
				SlotCursor.instance.SetDrag (false);
				this.setEquipData (this.equipData);
				storagePanel.setHighlightOnSlot(this);
				showInfo (true);
				OnCellClicked();
				storagePanel.SetInfoBar(this.equipData, this.Icon_Gear.atlas, this.Icon_Gear.spriteName);
			} else {
				if(slotType == EquipData.Type.ISO){
					EquipData storageEquipData = storageCell.equipData;
					storageCell.saveDataAndUpdate_ForISO(this.slot,this.equipData,storageEquipData);
					//this.setEquipData(storageEquipData);
				}else{
					EquipData storageEquipData = storageCell.equipData;
					storageCell.setEquipData (this.equipData);
					EquipManager.Instance.inventoryItemList.Add(this.equipData);
					storageCell.SetHighLight(true);
					this.setEquipData(storageEquipData);
					TeamDlg.instance.heroData.equipHash[this.slot] = storageEquipData;
					TeamDlg.instance.UpdateData();
					UserInfo.instance.saveAll();
				}
				if(this.equipData == null) {
					storagePanel.setHighlightOnSlot(null);
					if(!TeamDlg.instance.isHighLight) TeamDlg.instance.IconGearGroup.SetActive(false);;
				}
				else{
					storagePanel.setHighlightOnSlot(this);				
				}
			}
		}
	}
	
	public void showInfo (bool b)
	{
		if (this.equipData != null) {
			Icon_Gear.enabled = b;
			Info.enabled = b;			
		}		
	}

	public void SetHighLight (bool b)
	{
		this.isHighLight = b;
		if (b) {
			switch(slotType){
			case EquipData.Type.WEAPON:
				this.Bg.spriteName = Gear_Prime_Checked;
				break;
			case EquipData.Type.ISO:
				this.Bg.spriteName = ISO_Checked;
				break;
			case EquipData.Type.ARMOR:
			case EquipData.Type.TRINKET:
				this.Bg.spriteName = Gear_Checked;
				break;
			}
			this.Bg.MakePixelPerfect ();		
//			TeamDlg.instance.SetInfoBar (this.equipData);
		} else {
			switch(slotType){
			case EquipData.Type.WEAPON:
				this.Bg.spriteName = Gear_Prime_Checked;
				break;
			case EquipData.Type.ISO:
				this.Bg.spriteName = ISO_UnChecked;
				break;
			case EquipData.Type.ARMOR:
			case EquipData.Type.TRINKET:
				this.Bg.spriteName = Gear_UnChecked;
				break;
			}
			this.Bg.MakePixelPerfect ();		
		}
	}
	
	public void SetSlotCursor ()
	{
		if (this.equipData != null) {
			SlotCursor.instance.SetDrag (true);	
			SlotCursor.instance.SetEquipData (this.equipData,Icon_Gear.atlas,Icon_Gear.spriteName);	
			Icon_Gear.enabled = false;
			Info.enabled = false;
		} else {
			SlotCursor.instance.SetDrag (false);
			SlotCursor.instance.SetEquipData (null,null,null);
		}
	}
	
	private void OnCellClicked(){
		
	}
	
	public EquipData.Type getEquipSlot(){
		return slotType;
	}
	
	public void setEquipData(EquipData ed){
		Debug.Log("slotcell. setEquipData "+ ed);
		this.equipData = ed;
		if(ed == null){
			TeamDlg.instance.isHighLight = false;
			Icon_Gear.enabled = false;
			Info.enabled = false;
		}else{
			TeamDlg.instance.isHighLight = true;
			Icon_Gear.enabled = true;
			Info.enabled = true;
			
			this.Icon_Gear.spriteName = this.equipData.equipDef.iconID;
			if(this.Icon_Gear.GetAtlasSprite()==null){
				if(ed.equipDef.type == EquipData.Type.ISO){
					this.Icon_Gear.spriteName = "ISO_default";
				}else{
					this.Icon_Gear.spriteName = "GearDefault";
				}
			}
			if(ed.equipDef.type == EquipData.Type.ISO){
				Info.text = string.Format("{0}",Localization.instance.Get("ISO_Name_"+ed.equipDef.id));	
			}else{
				Info.text = string.Format("{0}\n<{1}{2}>",Localization.instance.Get("Gear_Name_"+ed.equipDef.id),Localization.instance.Get("Gear_Uid"),ed.uid);
			}
			Icon_Gear.MakePixelPerfect();
			//Info.text = ed.equipDef.equipName + "\n<" + ed.uid + ">";
		}
		
	}
	
	public void OnSlotClick(){
		//DlgManager.instance.ShowEquipUpgradeDlg(equipDataInSlot);	
	}
	
	private StorageCell raycastSlotCell(){
		foreach(StorageCell cell in storagePanel.storageCellList){
			if(hitSlot(cell)) return cell;
		}
		return null;
	}
	
	private bool hitSlot(StorageCell cell){
		Ray ray = this.uiCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if(cell.collider.Raycast(ray,out hit,10000)){
			Debug.Log("hit "+cell.gameObject.name);
			return true;	
		}else{
			return false;
		}
	}
}
