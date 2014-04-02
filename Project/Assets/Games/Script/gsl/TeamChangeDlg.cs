using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamChangeDlg : DlgBase {
//	public TeamMiniBar teamMiniBar;
	public DynamicGrid grid;
//	public GameObject blackMask;
//	public UILabel teamMiniBarIsfullStr;
	public UILabel heroLv;
	public UILabel heroName;
	public UISprite heroIcon;
	public GameObject heroInfo;
	public GameObject rightObj;
	public UILabel Desc;
	public UILabel btnLabel;
	public GameObject clock;
	public UILabel price;
	public GameObject locked;
	public UILabel lockedLabel;
	public GameObject stateBtn;
	public GameObject box;
	public UILabel trainingTimeLabel;
	public UILabel levelName;
	public UILabel chapterName;

	public TeamChangeCell lastHighLightCell;
	
	void Start () {
		MusicManager.playBgMusic("MUS_UI_Menus");
		heroInfo.SetActive(false);
		rightObj.SetActive(false);
		locked.SetActive(false);
		Desc.text = Localization.instance.Get("UI_TeamChangeDlg_Select");
		Desc.transform.localPosition = new Vector3(-540,-60,-50);
		Desc.transform.localScale = new Vector3(40,40,1);
		Desc.lineWidth = 700;
		//levelName.text = string.Format(Localization.instance.Get("UI_BattleBeginDlg_Level"),MapMgr.Instance.getCurrentLevel().id);
		chapterName.text = Localization.instance.Get("UI_ChapterName_" + MapMgr.Instance.getCurrentChapter().id) + " " + string.Format(Localization.instance.Get("UI_BattleBeginDlg_Level"),MapMgr.Instance.getCurrentLevel().id);
		
//		teamMiniBar.initTeamList();
//		teamMiniBar.onHeroClicked = onTeamMiniBarClicked;
		List<HeroData> heroDataList = new List<HeroData>();
		foreach(HeroData hd in UserInfo.heroDataList){
			switch(hd.state){
			case HeroData.State.LOCKED:
			case HeroData.State.UNLOCKED_NOT_RECRUITED:
			case HeroData.State.RECRUITED_NOT_SELECTED:
			case HeroData.State.SELECTED:
				heroDataList.Add(hd);
				break;
			}
		}
		grid.setData(heroDataList);
		
		UIPageGrid pg = grid.gameObject.GetComponent<UIPageGrid>();
		if(pg != null)
		{
			int colCount = heroDataList.Count / 2;
			if(colCount % 2 != 0)
			{
				colCount++;
			}
			pg.col = colCount;
			pg.repositionNow = true;
		}
	}
//	private void onTeamMiniBarClicked (HeroData hd){
//		Debug.Log("cell clicked "+ ((hd==null)?"null":hd.type));
//		if(hd != null){
//			bool b = teamMiniBar.removeHeroData(hd);
//			if(b && teamMiniBar.teamMiniBarIsfull){
//				setTeamMiniBarIsfullStr(false);
//				teamMiniBar.addHeroData(teamMiniBar.readyHeroData);
//				teamMiniBar.teamMiniBarIsfull = false;
//				teamMiniBar.readyHeroData = null;
//			}
//		}
//	}
	
	void Update ()
	{

	}
	
	public override void OnBtnBackClicked()
	{
		UserInfo.instance.saveAllheroes();
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		base.OnBtnBackClicked();
	}
	
//	public void OnBattleBtnClick()
//	{
//		BattleBg.Instance.heroOutBattle(false);
//		BattleBg.Instance.heroInBattle();
//		BattleBg.Instance.heroMoveToBattleStartPos();
//		Destroy(gameObject);
//	}
	
//	public void setTeamMiniBarIsfullStr(bool isPress){
//		teamMiniBarIsfullStr.gameObject.SetActive(isPress);
//	}
	
	public void OnCellClick(TeamChangeCell cell){
		if(lastHighLightCell != null && lastHighLightCell != cell){
			lastHighLightCell.Border.enabled = false;
		}
		lastHighLightCell = cell;
		
		UpdateView(cell);
	}
	
	public void UpdateView(TeamChangeCell cell){
		heroInfo.SetActive(true);
		Desc.text = Localization.instance.Get("UI_TeamChangeDlg_Select");
		Desc.transform.localPosition = new Vector3(-500,-60,-50);
		Desc.transform.localScale = new Vector3(30,30,1);
		Desc.lineWidth = 500;
		heroIcon.spriteName = "" + cell.heroData.type;
		heroLv.text = "Lv:" + cell.heroData.lv;
		heroName.text = "" + cell.heroData.type;
		switch(cell.heroData.state){
			case HeroData.State.LOCKED:
				locked.SetActive(true);
				rightObj.SetActive(false);
				break;
			case HeroData.State.UNLOCKED_NOT_RECRUITED:
				stateBtn.transform.localPosition = new Vector3(80,-85,-50);
				locked.SetActive(false);
				rightObj.SetActive(true);
				btnLabel.text = Localization.instance.Get("UI_Button_RECRUIT");//"RECRUITED";
				clock.SetActive(false);
				box.SetActive(true);
				price.enabled = true;
				if(cell.heroData.hireCp > 0){
					price.text = "[Command Points] " + cell.heroData.hireCp;
				}else if(cell.heroData.hireSilver > 0){
					price.text = "[Silver] " + cell.heroData.hireSilver;
				}	
				break;
			case HeroData.State.RECRUITED_NOT_SELECTED:
				stateBtn.transform.localPosition = new Vector3(80,-55,-50);
				clock.SetActive(false);
				box.SetActive(false);
				price.enabled = false;
				locked.SetActive(false);
				rightObj.SetActive(true);
				btnLabel.text = Localization.instance.Get("UI_Button_ADD");//"ADD";
				break;
			case HeroData.State.SELECTED:
				locked.SetActive(false);
				rightObj.SetActive(true);
				if(lastHighLightCell.isSkillLearning){
//					stateBtn.transform.localPosition = new Vector3(80,-85,-50);
//					btnLabel.text = "FINISH";
//					clock.SetActive(true);
//					box.SetActive(true);
//					price.enabled = true;
//					price.text = "[Command Points] 1";
				}else{
					stateBtn.transform.localPosition = new Vector3(80,-55,-50);
					btnLabel.text = Localization.instance.Get("UI_Button_REMOVE");//"REMOVE";
					clock.SetActive(false);
					box.SetActive(false);
					price.enabled = false;
				}
				break;
		}
	}
	
	public void OnBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		switch(lastHighLightCell.heroData.state){
			case HeroData.State.UNLOCKED_NOT_RECRUITED:
				MusicManager.playEffectMusic("SFX_UI_commandpoints_2a");
				lastHighLightCell.heroData.state = HeroData.State.RECRUITED_NOT_SELECTED;
				btnLabel.text = Localization.instance.Get("UI_Button_ADD");//"ADD";
				break;
			case HeroData.State.RECRUITED_NOT_SELECTED:
				if(!getTeamBarState(true)) return;
//				MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
				lastHighLightCell.heroData.state = HeroData.State.SELECTED;
				btnLabel.text = Localization.instance.Get("UI_Button_REMOVE");//"REMOVE";
				break;
			case HeroData.State.SELECTED:
				if(lastHighLightCell.isSkillLearning){
					foreach(SkillLearnedData ld in lastHighLightCell.heroData.learnedSkillIdList){
						ld.updateState();
						if(ld.State == SkillLearnedData.LearnedState.LEARNING){
							if(UserInfo.instance.getCommandPoints() >= Formulas.HowMuchToSkipSkillTraining(ld.TotalSeconds)){
								setToSkipConfirmDlg(ld);
							}else{
								setToSkipWrongDlg(ld);
							}
						}
					}
				}else{
					if(!getTeamBarState(false)) return;
//					MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
					lastHighLightCell.heroData.state = HeroData.State.RECRUITED_NOT_SELECTED;
					btnLabel.text = Localization.instance.Get("UI_Button_ADD");//"ADD";
				}
				break;
		}
		
		lastHighLightCell.updateView();
		UpdateView(lastHighLightCell);
		
		BattleBg.Instance.heroOutBattle(false, 0);
		BattleBg.Instance.heroInBattle();
		BattleBg.Instance.heroMoveToBattleStartPos();	
		
		UserInfo.instance.saveAllheroes();
	}
	
	protected bool getTeamBarState(bool isFull){
		int count = 0;
		foreach(HeroData hd in UserInfo.heroDataList){
			if(hd.state == HeroData.State.SELECTED){
				count++;	
			}
		}
		return (isFull? ((count >= 4)? false : true) : ((count <= 1)? false : true));
	}
	
	protected void setToSkipConfirmDlg(SkillLearnedData ld){
		CommonDlg dlg = DlgManager.instance.ShowCommonDlg("");
		dlg.setNormalDlg();
		dlg.OnUpdateStr = () => {
			if (SkillLearnedData.LearnedState.LEARNING != ld.State){
				dlg.OnUpdateStr = null;
				Destroy(dlg.gameObject);
			}
			else
				dlg.ShowCommonStr(string.Format(Localization.instance.Get("UI_CommonDlg_TrainSkillWithCP"), ld.Id,  Formulas.HowMuchToSkipSkillTraining(ld.TotalSeconds)));
		};
		dlg.onYes = () => {
			MusicManager.playEffectMusic("SFX_Skill_Training_done_2a");
			UserInfo.instance.consumeCommandPoints(Formulas.HowMuchToSkipSkillTraining(ld.TotalSeconds));
			ld.SkipLearningTime();
			dlg.OnUpdateStr = null;
			UpdateView(lastHighLightCell);
			stateBtn.transform.localPosition = new Vector3(80,-55,-50);
			btnLabel.text = Localization.instance.Get("UI_Button_REMOVE");//"REMOVE";
			clock.SetActive(false);
			box.SetActive(false);
			price.enabled = false;
			UserInfo.instance.saveAllheroes();
		};
		dlg.onNo = delegate {
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		};
	}
	
	protected void setToSkipWrongDlg(SkillLearnedData ld){
		CommonDlg dlg = DlgManager.instance.ShowCommonDlg("");
		dlg.setOneBtnDlg("CANCEL");
		
		dlg.ShowCommonStr(string.Format(Localization.instance.Get("UI_CommonDlg_SkipSkillTraining_NotEnoughCP"), ld.Id, Formulas.HowMuchToSkipSkillTraining(ld.TotalSeconds)));
		dlg.playErrorMusic();
		dlg.OnUpdateStr = () => {
			if (SkillLearnedData.LearnedState.LEARNING != ld.State){
				dlg.OnUpdateStr = null;
				Destroy(dlg.gameObject);
			}
			else{
				if (UserInfo.instance.getCommandPoints() >= Formulas.HowMuchToSkipSkillTraining(ld.TotalSeconds)){
					setToSkipConfirmDlg(ld);
				}
			}
		};
	}
	
}
