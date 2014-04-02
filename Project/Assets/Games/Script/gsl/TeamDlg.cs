using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamDlg : DlgBase {
	public enum Tag{
		TAG_GEAR,
		TAG_ISO,
		TAG_STORE,
		TAG_SKILL
	}
	[HideInInspector] 
	public Tag currentTag{
		set{
			_currentTag = value;	
			heroBar.transform.localPosition = (currentTag == Tag.TAG_SKILL)? heroBarLeftPos:heroBarRightPos;		
		}
		get{
			return _currentTag;
		}
	}
	private Tag _currentTag = Tag.TAG_GEAR;
	private Tag lastTag;
	public GameObject DescStoragePanelGroup;
	public GameObject IconGearGroup;
	public UILabel infoTitleLabel;
	public UILabel infoDescLabel;
	public UILabel infoPriceLabel;
	public UISprite infoIconBgGear;
	public UISprite infoIconBgISO;
		
	public UISprite info_ATK_Type1;
	public UISprite info_ATK_Type2;
	
	public TeamMiniBar teamMiniBar;
	public HeroData heroData;
	public UILabel heroName;
	public UILabel heroLv;
	public UILabel heroAttribute;
	public ExpBar  heroExpBar;
	public StoragePanel storagePanel;
	
	public UISprite Icon;
	public UILabel time;
	public UIAtlas isoAtlas;
	public UIAtlas equipsAtlas;
	public bool isHighLight = false;
	
	public GameObject checkBox;
	public GameObject heroPanel;
	public UISprite heroIcon;
	
	public UICheckbox checkGear;
	public UICheckbox checkISO;
	public UICheckbox checkSkill;
	public UICheckbox checkStore;
	//public GameObject btnRecharge;
	public GrayScaleTexture btnRecharge;
	
	public GameObject heroBar;
//	public GameObject storeFilterBar;
	
	public UICheckbox check_Filter_HeroSpecific;
	public UICheckbox check_Filter_NoneHeroSpecific;
	
//	public List<StorageCell> storageCellList;
	public List<Hero> heroList = new List<Hero>();
	
	public static TeamDlg instance;
	public FuseISODlg fuseISODlg;
	public SkillTreeDlg skillTreeDlg;

	private Vector3 heroBarOutPos = new Vector3(-11000,15,0);
	private Vector3 heroBarLeftPos = new Vector3(-110,15,0);
	private Vector3 heroBarRightPos = new Vector3(-30,15,0);
	
	void Awake(){	
		instance = this;
		MusicManager.playBgMusic("MUS_UI_Menus");
//		teamBar.init(OnTeamBarCellClicked);
		teamMiniBar.initTeamList();
		HeroData hd = teamMiniBar.highLightFirstHero();
		init(hd);
			
		//createHero();
		teamMiniBar.onHeroClicked = onTeamMiniBarClicked;
		
//		btnStore.SetActive(true);
//		btnInventory.SetActive(false);
//		pageTags.SetActive(true);
//		btnShowAll.SetActive(false);
		
//		storeFilterBar.SetActive(false);
//		btnSkill.SetActive(TsFtueManager.Instance.IsTrainCanUse);
//		btnStore.SetActive(TsFtueManager.Instance.IsGearUpCanUse);
//		btnEdit.SetActive(TsFtueManager.Instance.IsGearUpCanUse);
	}
	
	void Update(){
		if(heroData != null){
			string lefttime = heroData.staminaRegenerate();
			//heroStamina.text = heroData.stamina + "/" + heroData.staminaMax;
			//time.text = lefttime;
			time.text = string.Format("{0}",lefttime);
			int dStamina = this.heroData.staminaMax-this.heroData.stamina;
			if(dStamina > 0) btnRecharge.Disable();
			else btnRecharge.Enable();
			//btnRecharge.GetComponent<BoxCollider>().enabled = (dStamina > 0) ? true : false;
		}
	}
	
	private void onTeamMiniBarClicked (HeroData hd){
		//heroBar.transform.localPosition = Vector3.zero;
//		storeFilterBar.SetActive(false);
		storagePanel.clearHighLight();
		
		switch(storagePanel.currentFilter){
		case StoragePanel.FILTER.STORE_ALL:
		case StoragePanel.FILTER.STORE_All_HERO_SPECIFIC:
		case StoragePanel.FILTER.STORE_All_NONE_HERO_SPECIFIC:
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_HERO_APPLICABLE;
			break;
		}
		
		Debug.Log("cell clicked "+ ((hd==null)?"null":hd.type));
		if(hd != null){
			init (hd);
		}
	}
	public void StorageCellClicked (StorageCell cell){
		if(cell!=null){
			if(fuseISODlg.gameObject.activeInHierarchy){
				//fuseISODlg.initFuseISODlgCell(cell.equipData.equipDef.id);
			}
		}
		
	}
	public void init(HeroData hd){
		this.heroData = hd;
		heroIcon.spriteName = hd.type;
		StartCoroutine(initStep2());
	}
	private IEnumerator initStep2(){
		yield return new WaitForEndOfFrame();
		UpdateView();
		teamMiniBar.highlightHeroData(this.heroData);
		storagePanel.updateStorageCollection(0);
	}
	
	public void UpdateData(){
		//foreach(Hero hero in HeroMgr.heroHash.Values)
		foreach(Hero hero in heroList)
		{
			if(hero.data as HeroData == heroData)
			{
//				hero.initData(heroData);
				//hero.reCalctAtkAndDef();
				//heroAttribute.text = "ATK:" + hero.displayAtk + " DEF:" + hero.realDef + " HP:" +	hero.realHp;
				heroAttribute.text = string.Format("{0}:{1} {2}:{3} {4}:{5}",Localization.instance.Get("UI_Hero_ATK"),hero.displayAtk.toInt().ToStringShorten(),
																	Localization.instance.Get("UI_Hero_DEF"),hero.realDef.toInt().ToStringShorten(),
																	Localization.instance.Get("UI_Hero_HP"),hero.realHp);
			}
		}	
	}
	
	public void updateHeroInfoView(){
		heroName.text = string.Format("{0}",Localization.instance.Get("Hero_Name_"+heroData.type));
		string key_lv = heroLv.GetComponent<UILocalize>().key;
		heroLv.text = string.Format("{0}:{1}",Localization.instance.Get(key_lv),heroData.lv);
		heroExpBar.initBar(heroData.exp);
		List<HeroData.AtkData> atkDatas = heroData.getAtkPowers();
		if(atkDatas[0].v>0){
			info_ATK_Type1.gameObject.SetActive(true);
			info_ATK_Type1.spriteName = atkDatas[0].k;
			info_ATK_Type1.MakePixelPerfect();
		}else{
			info_ATK_Type1.gameObject.SetActive(false);
		}
		if(atkDatas[1].v>0){
			info_ATK_Type2.gameObject.SetActive(true);
			info_ATK_Type2.spriteName = atkDatas[1].k;
			info_ATK_Type2.MakePixelPerfect();
		}else{
			info_ATK_Type2.gameObject.SetActive(false);
		}
	}
	private void UpdateView()
	{
		TeamDlg.instance.IconGearGroup.SetActive(false);
		storagePanel.setHighlightOnSlot(null);
		UpdateData();
		updateHeroInfoView();
		//heroAttribute.text = "ATK:" + heroData.attack + " DEF:" + heroData.defense + " HP:" +heroData.maxHp;
		//heroStamina.text = heroData.stamina + "/" + heroData.staminaMax;
		
		this.storagePanel.slot_weapon.setEquipData(heroData.equipHash[EquipData.Slots.WEAPON] as EquipData);
	 	this.storagePanel.slot_armor.setEquipData(heroData.equipHash[EquipData.Slots.ARMOR] as EquipData);
		this.storagePanel.slot_Trinket1.setEquipData(heroData.equipHash[EquipData.Slots.TRINKET1] as EquipData);
		this.storagePanel.slot_Trinket2.setEquipData(heroData.equipHash[EquipData.Slots.TRINKET2] as EquipData);
		this.storagePanel.slot_iso1.setEquipData(heroData.equipHash[EquipData.Slots.ISO1] as EquipData);
		this.storagePanel.slot_iso2.setEquipData(heroData.equipHash[EquipData.Slots.ISO2] as EquipData);
		this.storagePanel.slot_iso3.setEquipData(heroData.equipHash[EquipData.Slots.ISO3] as EquipData);
		
//		foreach(Hero hero in HeroMgr.heroHash.Values)
//		{
//			this.hideHero(hero.gameObject);
//			if((hero.data as HeroData).type == heroData.type)
//			{
//				this.showHero(hero.gameObject);
//			}
//		}
		foreach(Hero hero in heroList){
			this.hideHero(hero.gameObject);
			if((hero.data as HeroData).type == heroData.type)
			{
				this.showHero(hero.gameObject);
			}
		}
	}
	
	public void OnEditBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();	
		dlg.transform.localPosition = new Vector3(0,0,-800);
		//this.gameObject.SetActive(false);
		dlg.onClose = delegate {
			//this.gameObject.SetActive(true);
		};
	}
	
	public void OnRechargeBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		int dStamina = this.heroData.staminaMax-this.heroData.stamina;
		int costGold = this.heroData.getStaminaRechargeCostGold();
		string s;
		if(UserInfo.instance.getGold() < costGold){
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
			//s = "Your Gold is not enough!"; 	
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughGold"));
			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
			dlg.setOKDlg();
		}else{
			//s = "Are you sure to recharge [00FFFF]" + dStamina + " [FFFFFF][Stamina] with [00FFFF]" + costGold + " [FFFFFF][Gold] ?";
			s = string.Format(Localization.instance.Get("UI_CommonDlg_Recharge"),dStamina,costGold);
			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
			dlg.onYes = delegate {
				MusicManager.playEffectMusic("SFX_Skill_Training_done_2a");
				UserInfo.instance.consumeGold(costGold);	
				this.heroData.addStamina(dStamina);
			};
			dlg.onNo = delegate {
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			};
		}
	}
	
	public override void OnBtnBackClicked()
	{
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		if(this.currentTag == Tag.TAG_SKILL){
			this.currentTag = lastTag;	
			UpdateLeftSlotsWithCurrentTag();
//			switch(lastTag){
//			case Tag.TAG_GEAR:
//				//this.storagePanel.OnGearBtnClick(true);
//				UpdateLeftSlotsWithCurrentTag();
//				break;
//			case Tag.TAG_ISO:
//				//this.storagePanel.OnISO8BtnClick(true);
//				UpdateLeftSlotsWithCurrentTag();
//				break;
//			case Tag.TAG_STORE:
//				UpdateLeftSlotsWithCurrentTag();
//				//this.OnStoreBtnClick(true);
//				break;
//			default:
//				this.storagePanel.OnGearBtnClick(true);
//				break;
//			}
//			UpdateLeftSlotsWithCurrentTag();
		}else{
			deleteHero();
			base.OnBtnBackClicked();
		}
//		base.OnBtnBackClicked();
	}
	
	public void OnSkillTreeBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//		SkillTreeDlg dlg = DlgManager.instance.showSkillTreeDlg(heroData);
//		dlg.transform.localPosition += new Vector3(0,0, gameObject.transform.localPosition.z - 300);
		lastTag = this.currentTag;
		
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		this.infoDescLabel.text = "";
		this.infoPriceLabel.text = "";
		this.infoTitleLabel.text = "";
		skillTreeDlg.descDlg.Hide();		
		
		this.currentTag = Tag.TAG_SKILL;
		UpdateLeftSlotsWithCurrentTag();

		this.skillTreeDlg.init(this.heroData);
	}
	
	public void OnStoreBtnClick(bool b){
		if(b){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
	 		currentTag = Tag.TAG_STORE;		
			UpdateLeftSlotsWithCurrentTag( );
	//		btnStore.SetActive(false);
	//		btnInventory.SetActive(true);
	//		pageTags.SetActive(false);
	//		btnShowAll.SetActive(true);
			
			Debug.Log("OnStoreBtnClick");	
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_HERO_APPLICABLE;
			storagePanel.updateStorageCollection(0);		
			storagePanel.clearHighLight();
			storagePanel.setPageToZero();
		}else{
			
		}
	}
	public void onFilterClicked_HeroSpecific(bool isChecked){
//		if(isChecked) MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.LogWarning("onFilterClicked_HeroSpecific "+isChecked);	
		showAllRefresh();
	}
	public void onFilterClicked_NoneHeroSpecific(bool isChecked){
//		if(isChecked) MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.LogWarning("onFilterClicked_NoneHeroSpecific "+isChecked);	
		showAllRefresh();
	}

//	public void OnShowAllClicked(){
//		//MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_simple_1b");
//		StartCoroutine(delayNotSingleMusic());
//		Debug.Log("OnShowAllClicked");
//		this.teamMiniBar.highlightHeroData(null);//clear select
//		heroBar.transform.localPosition = new Vector3(-10000,0,0);
////		storeFilterBar.SetActive(true);
//		//storagePanel.currentFilter = StoragePanel.FILTER.STORE_ALL;
//		this.check_Filter_NoneHeroSpecific.isChecked = true;
//		
//		showAllRefresh();
//	}
	
	private IEnumerator delayNotSingleMusic(){
		yield return new WaitForSeconds(0.7f);
		MusicManager.Instance.isSingleMusic = false;
	}
	
	private void showAllRefresh(){
		if(this.check_Filter_HeroSpecific.isChecked && this.check_Filter_NoneHeroSpecific.isChecked){
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_ALL;
		}else if(this.check_Filter_HeroSpecific.isChecked) {
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_All_HERO_SPECIFIC;
		}else if(this.check_Filter_NoneHeroSpecific.isChecked) {
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_All_NONE_HERO_SPECIFIC;
		}else{
			storagePanel.currentFilter = StoragePanel.FILTER.STORE_NONE;
		}
		storagePanel.updateStorageCollection(0);	
	}
//	public void OnInventoryClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//		Debug.Log(storagePanel.currentFilter);
//		
//		heroBar.transform.localPosition = (currentTag == Tag.TAG_SKILL)? heroBarLeftPos:heroBarRightPos;
//		
////		storeFilterBar.SetActive(false);
//		init (this.heroData);
//		
//		btnStore.SetActive(true);
//		btnInventory.SetActive(false);
//		pageTags.SetActive(true);
//		
//		Debug.Log("OnStoreBtnClick");	
//		storagePanel.currentFilter = StoragePanel.FILTER.OWN_GEAR;
//		storagePanel.updateStorageCollection(0);	
//		storagePanel.clearHighLight();
//	}
	
	public void OnManageBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.LogError("OnManageBtnClick");
		ShowTeamManagerDlg();	
	}
	
	public void OnBackTeamBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.LogError("OnBackTeamBtnClick");
		ShowTeamManagerDlg();
	}
	
	public delegate void NoneParmsDelegate();
	public NoneParmsDelegate OnEquipArmor;
	public void suitEquipUp(EquipData ed){
		Debug.LogError(ed.equipDef.equipName + " were put on~" + ed.equipDef.type);	
		if(EquipData.Type.ARMOR == ed.equipDef.type && null != OnEquipArmor){
			OnEquipArmor();
		}
	}
	
	private void ShowTeamEditorDlg(){
	}
	
	private void ShowTeamManagerDlg(){
	}
//	void OnSkillBtnClick(bool isChecked){
//		this.infoDescLabel.text = "";
//		this.infoPriceLabel.text = "";
//		this.infoTitleLabel.text = "";
//		skillTreeDlg.descDlg.Hide();		
//		
//		if(isChecked){
//			this.currentTag = Tag.TAG_SKILL;
//			UpdateLeftSlotsWithCurrentTag();
//			//DescStoragePanelGroup.SetActive(!isChecked);
////			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
//			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		}
//		DescStoragePanelGroup.SetActive(!isChecked);
//		IconGearGroup.SetActive(!isChecked);
//		
//		this.storagePanel.gameObject.SetActive(!isChecked);
//		this.skillTreeDlg.gameObject.SetActive(isChecked);
//		this.skillTreeDlg.init(this.heroData);
//	}
	
	public void showFuseISODlg( int selectISOID){
		this.storagePanel.slot_weapon.gameObject.SetActive(false);
		this.storagePanel.slot_armor.gameObject.SetActive(false);
		this.storagePanel.slot_Trinket1.gameObject.SetActive(false);
		this.storagePanel.slot_Trinket2.gameObject.SetActive(false);
		this.storagePanel.slot_iso1.gameObject.SetActive(false);
		this.storagePanel.slot_iso2.gameObject.SetActive(false);
		this.storagePanel.slot_iso3.gameObject.SetActive(false);		
		heroBar.transform.localPosition = heroBarOutPos;		
		fuseISODlg.gameObject.SetActive(true);
		//fuseISODlg.transform.localPosition = new Vector3(0,0,-800);
		//fuseISODlg.initFuseISODlgCell( selectISOID);

	}
	
	public void cancelFuseISODlg(){
		this.storagePanel.slot_weapon.gameObject.SetActive(true);
		this.storagePanel.slot_armor.gameObject.SetActive(true);
		this.storagePanel.slot_Trinket1.gameObject.SetActive(true);
		this.storagePanel.slot_Trinket2.gameObject.SetActive(true);
		this.storagePanel.slot_iso1.gameObject.SetActive(true);
		this.storagePanel.slot_iso2.gameObject.SetActive(true);
		this.storagePanel.slot_iso3.gameObject.SetActive(true);		
		heroBar.transform.localPosition = heroBarRightPos;		
		fuseISODlg.gameObject.SetActive(false);	
	}
	
//	public void SetInfoBar(EquipData ed){
//		Debug.Log("TeamDlg.SetInfoBar "+ed);
//		if(ed == null){
//			TeamDlg.instance.IconGearGroup.SetActive(false);
//		}
//		else{
//			TeamDlg.instance.IconGearGroup.SetActive(true);
//			//info.text = "[00FFFF]" + ed.equipDef.equipName + "   Lv" + ed.getCurrentLv() + "\nATK: [FFFF00]+99\n[00FFFF]DEF: [FFFF00]+99";
//			if(ed.equipDef.type == EquipData.Type.ISO){
//				Icon.atlas = this.isoAtlas;
//				Icon.spriteName = ed.equipDef.iconID;
//				if(Icon.GetAtlasSprite()==null){
//					Icon.spriteName = "ISO_default";
//				}
//				infoTitleLabel.text = Localization.instance.Get("ISO_Name_"+ed.equipDef.id);
//				infoDescLabel.text =  ed.getDescString();
//				infoIconBgGear.gameObject.SetActive(false);
//				infoIconBgISO.gameObject.SetActive(true);
//				//info.text = "[00FFFF]" + ed.equipDef.equipName + "\n[FFFF00]" +ed.equipDef.equipEft.eName + ": [00FFFF]" + numStr;
////				Debug.LogError(ed.equipDef.equipEft.eName);
////				info.text = string.Format("[00FFFF]{0}\n[FFFF00]{1}: [00FFFF]{2}",Localization.instance.Get("ISO_Name_"+ed.equipDef.id),
////													Localization.instance.Get("UI_Hero_"+ed.equipDef.equipEft.eName),numStr);
//				// delete by why 2014.2.10 end 
//			}else{//GEAR
//				Icon.atlas = this.equipsAtlas;
//				Icon.spriteName = ed.equipDef.iconID;
//				if(Icon.GetAtlasSprite()==null){
//					Icon.spriteName = "GearDefault";
//				}
//				infoTitleLabel.text = Localization.instance.Get("Gear_Name_"+ed.equipDef.id);
//				infoDescLabel.text = ed.getDescString();
//				infoIconBgGear.gameObject.SetActive(true);
//				infoIconBgISO.gameObject.SetActive(false);
//				// delete by why 2014.2.10
//				
//				//info.text = "[00FFFF]" + ed.equipDef.equipName + "   Lv" + ed.getCurrentLv() + "\n[FFFF00]" +ed.equipDef.equipEft.eName + ": [00FFFF]" + numStr;
////				info.text = string.Format("[00FFFF]{0}   {1}{2}\n[FFFF00]{3}: [00FFFF]{4}",Localization.instance.Get("Gear_Name_"+ed.equipDef.id),
////													Localization.instance.Get("UI_Hero_Lv"),ed.getCurrentLv(),
////													Localization.instance.Get("UI_Hero_"+ed.equipDef.equipEft.eName),numStr);
//				// delete by why 2014.2.10 end
//			}
//			Icon.MakePixelPerfect();
//			TeamDlg.instance.updateHeroInfoView();
//		}
//	}
	
	public GameObject heroRoot;
	
	public void createHero()
	{

//		if(GotoProxy.getSceneName() == "UIMain")
//		{
			for(int i=0; i<UserInfo.heroDataList.Count; i++)
			{
				HeroData heroData = UserInfo.heroDataList[i] as HeroData;
				
				if(heroData.state == HeroData.State.SELECTED)
				{
					GameObject heroObj = Instantiate(CacheMgr.getHeroPrb(heroData.type)) as GameObject;
					
					Hero hero = heroObj.GetComponent<Hero>();
					heroObj.transform.parent = heroRoot.transform;
				
					heroObj.transform.localScale = new Vector3(0.2f, 0.2f,1);
					
					hero.initData(heroData);
					hideHero(hero.gameObject);
					
					heroList.Add(hero);
				}
			}
//		}
//		else if(GotoProxy.getSceneName() == "NormalLevel")
//		{
//			foreach(Hero hero in HeroMgr.heroHash.Values)
//			{
//				heroList.Add(hero);
//				hero.gameObject.transform.parent = heroRoot.transform;
//				hero.gameObject.transform.localScale = new Vector3(0.3f, 0.3f,1);
//				hideHero(hero.gameObject);
//			}
//		}
	}
	
	public void deleteHero()
	{
//		if(GotoProxy.getSceneName() == "UIMain")
//		{
			foreach(Hero hero in heroList){
				//HeroMgr.delHero(hero.id);
				Destroy(hero.gameObject);
			}
			heroList.Clear();
//			foreach(Hero hero in HeroMgr.heroHash.Values)
//			{
//				Destroy(hero.gameObject);
//			}
//			HeroMgr.clear();
//		}
//		else if(GotoProxy.getSceneName() == "NormalLevel")
//		{
//			foreach(Hero hero in HeroMgr.heroHash.Values)
//			//foreach(Hero hero in heroList)
//			{
//				hero.gameObject.transform.parent = null;
//				hero.gameObject.transform.localScale = new Vector3(Utils.characterSize,Utils.characterSize,1);
//				//hero.gameObject.transform.position = BattleBg.Instance.getHeroStartPosByHeroType((hero.data as HeroData).type);
//				hero.setPosition(BattleBg.Instance.getHeroStartPosByHeroType((hero.data as HeroData).type));
//			}
//		}
	}
	
	public void delayMusic(){
		StartCoroutine(delayIsNotSingleMusic());
	}
	
	private IEnumerator delayIsNotSingleMusic(){
		yield return new WaitForSeconds(0.7f);	
		MusicManager.Instance.isSingleMusic = false;
	}
	
	protected void hideHero(GameObject heroObj)
	{
		heroObj.transform.localPosition = new Vector3(-10000, -115, -50.0f);
	}
	
	protected void showHero(GameObject heroObj)
	{
		heroObj.transform.localPosition = new Vector3(200, -115, -50.0f);
	}
	protected void UpdateLeftSlotsWithCurrentTag(){
		this.fuseISODlg.gameObject.SetActive(false);
		this.storagePanel.gameObject.SetActive(true);
		this.skillTreeDlg.gameObject.SetActive(false);
		checkBox.SetActive(true);
		heroPanel.SetActive(true);
		switch(this.currentTag){
		case Tag.TAG_SKILL:
			this.storagePanel.slot_weapon.gameObject.SetActive(true);
		 	this.storagePanel.slot_armor.gameObject.SetActive(false);
			this.storagePanel.slot_Trinket1.gameObject.SetActive(false);
			this.storagePanel.slot_Trinket2.gameObject.SetActive(false);
			this.storagePanel.slot_iso1.gameObject.SetActive(false);
			this.storagePanel.slot_iso2.gameObject.SetActive(false);
			this.storagePanel.slot_iso3.gameObject.SetActive(false);
			this.storagePanel.gameObject.SetActive(false);
			DescStoragePanelGroup.SetActive(false);
			IconGearGroup.SetActive(false);
			checkBox.SetActive(false);
			heroPanel.SetActive(false);
			this.skillTreeDlg.gameObject.SetActive(true);
		break;
//		case Tag.TAG_ISO:
//			this.storagePanel.slot_weapon.gameObject.SetActive(true);
//			this.storagePanel.slot_armor.gameObject.SetActive(false);
//			this.storagePanel.slot_Trinket1.gameObject.SetActive(false);
//			this.storagePanel.slot_Trinket2.gameObject.SetActive(false);
//			this.storagePanel.slot_iso1.gameObject.SetActive(false);
//			this.storagePanel.slot_iso2.gameObject.SetActive(false);
//			this.storagePanel.slot_iso3.gameObject.SetActive(false);
//			break;
		case Tag.TAG_ISO:
		case Tag.TAG_GEAR:
		case Tag.TAG_STORE:
			this.storagePanel.slot_weapon.gameObject.SetActive(true);
		 	this.storagePanel.slot_armor.gameObject.SetActive(true);
			this.storagePanel.slot_Trinket1.gameObject.SetActive(true);
			this.storagePanel.slot_Trinket2.gameObject.SetActive(true);
			this.storagePanel.slot_iso1.gameObject.SetActive(true);
			this.storagePanel.slot_iso2.gameObject.SetActive(true);
			this.storagePanel.slot_iso3.gameObject.SetActive(true);
			//this.fuseISODlg.gameObject.SetActive(false);
			break;
		}
	}	
}