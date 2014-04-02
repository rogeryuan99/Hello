using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTreeDlg : DlgBase
{ 
//	public static SkillTreeDlg Instance;
	public TeamMiniBar teamMiniBar;
	public HeroData heroData;
	public GameObject cellTemplate;
	public GameObject[] cellPlaceHolders;
	private List<SkillTreeCell> cells = new List<SkillTreeCell>();
	
	public GameObject hotInspector;
	public SkillDescDlg descDlg;
	// public HeroData heroData;
	public GameObject isLearnSkillAlertObj;
	public UILabel heroNameLabel;
	public UILabel heroLvLabel;
	public UISprite learnSkillButtonBG;
	public GameObject learnSkillButtonObj;
	
	public UISprite heroIcon;
	public UILabel heroName;
	
	protected List<SkillDef> skillDefList = new List<SkillDef>();
	
	public List<string> changSkillHeroTypeList = new List<string>();
	private GameObject selectedCell;
	
	private void onTeamMiniBarClicked (HeroData hd){
		Debug.Log("cell clicked "+ ((hd==null)?"null":hd.type));
		descDlg.Hide();
		if(hd != null){
			this.heroData = hd;
			refresh();
		}
	}
	public void init(HeroData heroData)
	{
		Debug.Log("init");
		this.heroData = heroData; 
	}
	public void Start(){
		Debug.Log("init");
//		Instance = this;
		for(int kk = 0; kk<cellPlaceHolders.Length; kk++){
			GameObject cellGo = Instantiate(cellTemplate) as GameObject;
			cellGo.transform.parent = cellPlaceHolders[kk].transform;
			cellGo.transform.localScale = Vector3.one;
			cellGo.transform.localPosition = Vector3.zero;
			SkillTreeCell stc = cellGo.GetComponent<SkillTreeCell>();
			cells.Add(stc);
		}
		
		cells[0].bortherID = -1;
		cells[1].bortherID = -1;
		cells[2].bortherID = 3;
		cells[3].bortherID = 2;
		cells[4].bortherID = 5;
		cells[5].bortherID = 4;
		cells[6].bortherID = -1;
		cells[7].bortherID = 8;
		cells[8].bortherID = 7;
		cells[9].bortherID = 10;
		cells[10].bortherID = 9;
		cells[11].bortherID = -1;
		cells[12].bortherID = 13;
		cells[13].bortherID = 12;
		teamMiniBar.onHeroClicked +=onTeamMiniBarClicked;

		refresh ();
	}
	private void refresh()
	{		
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}_{2}", TsFtueManager.DIALOG_OPENED, gameObject.name, heroData.type));
		
//		heroNameLabel.text = heroData.nickName;
//		heroLvLabel.text = "LV:"+heroData.lv;
		heroNameLabel.text = string.Format("{0}",Localization.instance.Get("Hero_Name_"+heroData.nickName.ToUpper()));
		heroLvLabel.text = string.Format("{0}:{1}",Localization.instance.Get("UI_Hero_Lv"),heroData.lv);
		
		heroName.text = string.Format("{0}",Localization.instance.Get("Hero_Name_"+heroData.nickName.ToUpper()));
		heroIcon.spriteName = heroData.type;
		
		resetSkillDefList(heroData.type);
		

		int index = 0;
		foreach(SkillDef sd in skillDefList)
		{
			Debug.Log("index="+index);
			cells[index].IsSelected = false;
			cells[index].initBySkillDef(sd);
			cells[index].learnedData = heroData.getLearnedData(sd.id);
			if (null == cells[index].learnedData){
				cells[index].learnedData = new SkillLearnedData(sd);
				cells[index].learnedData.State = (heroData.lv >= sd.unlockLv)? 
													SkillLearnedData.LearnedState.UNLEARNED:
													SkillLearnedData.LearnedState.LOCKED;
			}
			else{
				if (heroData.activeSkillIDList.Contains(sd.id)
					|| heroData.passiveSkillIDList.Contains(sd.id)){
					cells[index].IsSelected = true;
				}
				else if (SkillLearnedData.LearnedState.LEARNING == cells[index].learnedData.State){
					cells[index].OnFinished = OnFinishedLearning;
				}
			}
			cells[index].updateOutlook();
			index++;
		}
	}
	
	public void cellClicked(GameObject go)
	{
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		// if(selectedCell != go)
		// {
		selectedCell = go;
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		
		if (SkillLearnedData.LearnedState.LEARNED == cell.learnedData.State)
		{
//			cell.IsSelected = true;
//			cell.updateOutlook();
//	 		changeHeroSkillList(cell);
//			changeSkillTreeCellBortherState(cell);
//			UserInfo.instance.saveAllheroes();
			selectSkill(cell);
		}
		descDlg.DescIcon.spriteName = cell.icon.spriteName;
		descDlg.DescIcon.gameObject.SetActive(true);
		descDlg.DescIconBg.spriteName = cell.frame.spriteName;
		descDlg.DescIconBg.MakePixelPerfect();
		descDlg.DescIconBg.gameObject.SetActive(true);
		descDlg.Show(cell.skillDef);
		descDlg.SetTrainingBtnVisible(SkillLearnedData.LearnedState.UNLEARNED == cell.learnedData.State);
		descDlg.SetSkippingBtnVisible(SkillLearnedData.LearnedState.LEARNING  == cell.learnedData.State);
		descDlg.skillTrainTimeMask.fillAmount = 0;
		cell.OnFinished = OnFinishedLearning;
	}
	
	public void OnFinishedLearning(SkillTreeCell cell_){
		if (selectedCell == cell_.gameObject){
			descDlg.SetSkippingBtnVisible(false);
		}
		selectSkill(cell_);
		cell_.OnFinished = null;
	}

	public void selectSkill(SkillTreeCell cell){
		cell.IsSelected = true;
		cell.updateOutlook();
 		changeHeroSkillList(cell);
		changeSkillTreeCellBortherState(cell);
		UserInfo.instance.saveAllheroes();
	}
	
	public void changeSkillTreeCellBortherState(SkillTreeCell cell)
	{
		if(cell.IsSelected 
			&& cell.bortherID != -1
			&& SkillLearnedData.LearnedState.LEARNED == cell.learnedData.State)
		{
			cells[cell.bortherID].IsSelected = false;
			cells[cell.bortherID].updateOutlook();
		}
	}
	
	public void changeHeroSkillList(SkillTreeCell cell)
	{
		if (SkillLearnedData.LearnedState.LEARNED == cell.learnedData.State)
		{
			if (cell.skillDef.isPassive 
				&& !heroData.passiveSkillIDList.Contains(cell.skillDef.id))
			{
				heroData.passiveSkillIDList.Add(cell.skillDef.id);
			}
			else 
			if (!cell.skillDef.isPassive
				&& !heroData.activeSkillIDList.Contains(cell.skillDef.id))
			{
				heroData.activeSkillIDList.Add(cell.skillDef.id);
			}
			
			if(cell.IsSelected && cell.bortherID != -1)
			{
				if(cell.skillDef.isPassive)
				{
					heroData.passiveSkillIDList.Remove(cells[cell.bortherID].skillDef.id);
				}
				else
				{
					heroData.activeSkillIDList.Remove(cells[cell.bortherID].skillDef.id);
				}
			}
		}
	}
	
	public override void OnBtnBackClicked()
	{
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		base.OnBtnBackClicked();
//		if(HeroMgr.heroHash.Count != 0)
//		{
//			foreach(string heroType in changSkillHeroTypeList)
//			{
//				Hero hero = HeroMgr.getHeroByType(heroType);
//				hero.initData(hero.data as HeroData);
//				
//				SkillIconManager.Instance.createSkillIconDataSkillByHeroData(hero.data);
//				ComboController.Instance.createComboIconDataSkillByHeroData(hero.data);
//			}
//		}
	}      
	
	public void OnTrainBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if (null == selectedCell) return;
		
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		CommonDlg dlg = null;
		if (UserInfo.instance.getCommandPoints() >= cell.skillDef.commandPoints){
			dlg = DlgManager.instance.ShowCommonDlg(string.Format(Localization.instance.Get("UI_CommonDlg_TrainSkillWithCP"), cell.skillDef.name, cell.skillDef.commandPoints));
			dlg.transform.localPosition -= new Vector3(0, 0, -this.transform.localPosition.z);
			dlg.onYes = () => {
				learnSkill(cell);
				UserInfo.instance.consumeCommandPoints(cell.skillDef.commandPoints);
				foreach(AudioSource audio in MusicManager.Instance.effectMusicObjs){
					if(audio.isPlaying && audio.clip.name == "SFX_Skill_Training_2a"){
						return;	
					}
				}
				MusicManager.playEffectMusic("SFX_Skill_Training_2a");
			};
			dlg.onNo = () => {
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
			};
		}
		else{
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
			dlg = DlgManager.instance.ShowCommonDlg(string.Format(Localization.instance.Get("UI_CommonDlg_TrainSkillWithCP_NotEnoughCP"), cell.skillDef.name, cell.skillDef.commandPoints));
			dlg.transform.localPosition -= new Vector3(0, 0, -this.transform.localPosition.z);
			dlg.setOneBtnDlg("CANCEL");
		}
	}
	
	void FixedUpdate(){
		updateSkipBtnText();
	}
	private void updateSkipBtnText(){
		if (null == selectedCell) return;
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		int g = Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds);
		descDlg.buttonText.text = g.ToString();//string.Format(Localization.instance.Get("UI_Button_SkipSkillTraining"),g);//"Finish now\nfor "+g+" [Gold]";
//		if(descDlg.skillTrainTimeMask.gameObject.activeInHierarchy != cell.timingMaskSprite.gameObject.activeInHierarchy){
//			descDlg.skillTrainTimeMask.gameObject.SetActive(cell.timingMaskSprite.gameObject.activeInHierarchy);
//		}
		descDlg.skillTrainTimeMask.fillAmount = cell.timingMaskSprite.fillAmount;
		descDlg.skillTrainTimeLeftText.text = cell.textTime.text;
	}
	void OnSkipBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if (null == selectedCell) return;
		
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		CommonDlg dlg = DlgManager.instance.ShowCommonDlg("");
		dlg.transform.localPosition -= new Vector3(0, 0, -this.transform.localPosition.z);
		if (UserInfo.instance.getCommandPoints() >= Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds)){
			setToSkipConfirmDlg(dlg);
		}
		else{
			setToSkipWrongDlg(dlg);
		}
	}
	
	protected void setToSkipConfirmDlg(CommonDlg dlg){
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		
		dlg.setNormalDlg();
		dlg.OnUpdateStr = () => {
			if (SkillLearnedData.LearnedState.LEARNING != cell.learnedData.State){
				dlg.OnUpdateStr = null;
				// descDlg.SetSkippingBtnVisible(false);
				Destroy(dlg.gameObject);
			}
			else
				dlg.ShowCommonStr(string.Format(Localization.instance.Get("UI_CommonDlg_TrainSkillWithCP"), cell.skillDef.name,  Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds)));
		};
		dlg.onYes = () => {
			UserInfo.instance.consumeCommandPoints(Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds));
			cell.learnedData.SkipLearningTime();
			descDlg.SetSkippingBtnVisible(false);
			dlg.OnUpdateStr = null;
			StartCoroutine(delayPlayRechargedMusic());
		};
		dlg.onNo = () => {
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
		};
	}
	
	protected IEnumerator delayPlayRechargedMusic(){
		yield return new WaitForSeconds(0.7f);
		MusicManager.playEffectMusic("SFX_Skill_Training_done_2a");	
	}
	
	protected void setToSkipWrongDlg(CommonDlg dlg){
		SkillTreeCell cell = selectedCell.GetComponent<SkillTreeCell>();
		
		dlg.setOneBtnDlg("CANCEL");
		
		dlg.ShowCommonStr(string.Format(Localization.instance.Get("UI_CommonDlg_SkipSkillTraining_NotEnoughCP"), cell.skillDef.name, Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds)));
		dlg.playErrorMusic();
		dlg.OnUpdateStr = () => {
			if (SkillLearnedData.LearnedState.LEARNING != cell.learnedData.State){
				dlg.OnUpdateStr = null;
				// descDlg.SetSkippingBtnVisible(false);
				Destroy(dlg.gameObject);
			}
			else{
				if (UserInfo.instance.getCommandPoints() >= Formulas.HowMuchToSkipSkillTraining(cell.learnedData.TotalSeconds)){
					setToSkipConfirmDlg(dlg);
				}
			}
		};
	}
	
	protected void learnSkill(SkillTreeCell cell)
	{
//		cell.setState(SkillTreeCell.State.Chosen);
//		changeSkillTreeCellBortherState(cell);
//		changeHeroSkillList(cell);
//		cell.LearnedData = heroData.learnSkill(cell.skillDef);
//		
//		UserInfo.instance.saveAllheroes();
//		
//		changSkillHeroTypeList.Remove(heroData.type);
//		changSkillHeroTypeList.Add(heroData.type);
		
		// cell.setState(SkillTreeCell.State.Chosen);
		unlockComboSkill();
		cell.learn();
		cell.updateOutlook();
		descDlg.SetTrainingBtnVisible(SkillLearnedData.LearnedState.UNLEARNED == cell.learnedData.State);
		descDlg.SetSkippingBtnVisible(SkillLearnedData.LearnedState.LEARNING  == cell.learnedData.State);
		heroData.learnedSkillIdList.Add(cell.learnedData);
		// changeSkillTreeCellBortherState(cell);
		// changeHeroSkillList(cell);
		// cell.LearnedData = heroData.learnSkill(cell.skillDef);
		
		UserInfo.instance.saveAllheroes();
		
		changSkillHeroTypeList.Remove(heroData.type);
		changSkillHeroTypeList.Add(heroData.type);
	}
	
	
	public void unlockComboSkill(){
		SkillTreeCell skillTreeCell = selectedCell.GetComponent<SkillTreeCell>();
		
		if (skillTreeCell.icon.spriteName == "SkillIcon_DRAX1"
			&& HeroData.DRAX == heroData.type){
			UserInfo.instance.saveIsUnLockCombo(true);
		}
	}
	
	// Privates
	private void resetSkillDefList(string type){
		skillDefList.Clear();
		foreach(SkillDef sd in SkillLib.instance.allHeroSkillHash.Values)
		{
			if(sd.id.Contains(type))
			{
				skillDefList.Add(sd);
			}
		}
		skillDefList.Sort(sortByLevel);
	}
	
	private int sortByLevel(SkillDef sd1, SkillDef sd2) 
	{ 
		if(sd1.unlockLv > sd2.unlockLv)
		{
			return 1;
		}
		else if(sd1.unlockLv == sd2.unlockLv)
		{
			string s1 = sd1.id.Substring(sd1.id.Length - 1);
			
			string s2 = sd2.id.Substring(sd2.id.Length - 1);
			
			return string.Compare(s1, s2);
		}
		else if(sd1.unlockLv < sd2.unlockLv)
		{
			return -1;
		}
		return 0;
	}
	
//	private SkillTreeCell.State getSkillState(SkillDef def){
//		SkillTreeCell.State state = SkillTreeCell.State.Lock;
//	
//		if(this.heroData.lv >= def.unlockLv){
//			if (SkillLearnedData.LearnedState.UNLEARNED == heroData.getLearnedState(def.id)){
//				state = SkillTreeCell.State.NotLearn;
//			}
//			else{
//				state = def.isPassive
//							? this.heroData.passiveSkillIDList.Contains(def.id)? SkillTreeCell.State.Chosen: SkillTreeCell.State.Abandon
//							: this.heroData.activeSkillIDList.Contains(def.id)? SkillTreeCell.State.Chosen: SkillTreeCell.State.Abandon;
//			}
//		}
//		return state;
//	}
//	
//	public SkillTreeCell.State getSkillState(List<string> skillList, string skillDefID)
//	{
//		foreach(string skillID in skillList)
//		{
//			if(skillID == skillDefID)
//			{
//				return SkillTreeCell.State.Chosen;
//			}
//		}	
//		return SkillTreeCell.State.Abandon;
//	}
}
 