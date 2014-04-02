using UnityEngine;
using System.Collections;

public class TeamChangeCell : DetailCell {
	public UISprite bg_Checked;
	public UISprite bg_Unchecked;
	public UISprite bg_Locked;
	public UISprite Icon;
	public UISprite LockIcon;
	public UISprite New;
	public UISprite Border;
	public GrayScaleTexture grayTexture;
	public UILabel name;
	public UILabel time;
	public HeroData heroData;
	public GameObject skillTraining;
	public TeamChangeDlg teamChangeDlg;
	
	public bool isSkillLearning = false;
	
	private int index;

	void Start () {
		teamChangeDlg = NGUITools.FindInParents<TeamChangeDlg>(this.gameObject);
//		skillTraining.SetActive(false);
//		Border.enabled = false;
	}

	void Update () {
//		if(heroData != null){
//			string lefttime = heroData.staminaRegenerate();
//			time.text = string.Format("{0}",lefttime);	
//		}
//		if(!teamChangeDlg.teamMiniBar.teamMiniBarIsfull){
//			this.transform.parent.localScale = Vector3.one;
//			teamChangeDlg.blackMask.SetActive(false);
//			Transform parent = this.transform.parent;
//			UIPanel up = parent.GetComponent<UIPanel>();
//			if(up != null) Destroy(up);
//			parent.localPosition = new Vector3(parent.localPosition.x,parent.localPosition.y,0f);
//		}
	}
	
	public override void OnIn(object data){
		teamChangeDlg = NGUITools.FindInParents<TeamChangeDlg>(this.gameObject);
		HeroData hd = (HeroData)data;
		this.heroData = hd;
		
		skillTraining.SetActive(false);
		Border.enabled = false;
		
		updateView();
	}
	
	public override void OnOut(){
		
	}
	
	public void updateView(){
		this.transform.localScale = Vector3.one;
//		name.text = "" + heroData.type;
		name.text = string.Format("{0}",Localization.instance.Get("Hero_Name_"+heroData.type));
		Icon.spriteName = "" + heroData.type;
		Icon.MakePixelPerfect();
		switch(heroData.state){
		case HeroData.State.LOCKED:	
			bg_Checked.enabled = false;
			bg_Unchecked.enabled = false;
			bg_Locked.enabled = true;
			grayTexture.Enable();
			LockIcon.enabled = true;
			New.enabled = false;
			break;
		case HeroData.State.UNLOCKED_NOT_RECRUITED:	
			bg_Checked.enabled = false;
			bg_Unchecked.enabled = true;
			bg_Locked.enabled = false;
			grayTexture.Disable();
			Icon.color = new Color(1f,1f,1f,1f);	
			LockIcon.enabled = false;
			New.enabled = true;
			if (heroData.hireCp>0){
				//price.text = string.Format("{0} {1}",heroData.hireCp,Localization.instance.Get("UI_TeamChangeCell_CP"));
			}
			break;
		case HeroData.State.RECRUITED_NOT_SELECTED:	
			grayTexture.Disable();
			Icon.color = new Color(1f,1f,1f,1f);	
			bg_Checked.enabled = false;
			bg_Unchecked.enabled = true;
			bg_Locked.enabled = false;
			LockIcon.enabled = false;
			New.enabled = false;
			break;
		case HeroData.State.SELECTED:	
			grayTexture.Disable();
			Icon.color = new Color(1f,1f,1f,1f);
			name.color = new Color(0f,1f,1f,1f);
			name.text = "[Dot]" + name.text + "[Dot]  ";
			bg_Checked.enabled = true;
			bg_Unchecked.enabled = false;	
			bg_Locked.enabled = false;
			LockIcon.enabled = false;
			New.enabled = false;
			break;
		}
	}
	
	public void OnCellClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.Log("OnCellClick");
		if(heroData.state != HeroData.State.LOCKED){
			Border.enabled = true;
			teamChangeDlg.OnCellClick(this);
		}
		
	}
	
	public void FixedUpdate(){
		if(heroData == null) return;
		SkillLearnedData maxLd = null;
		foreach(SkillLearnedData ld in heroData.learnedSkillIdList){
			ld.updateState();
			if(ld.State == SkillLearnedData.LearnedState.LEARNING){
				if(maxLd == null){
					maxLd = ld;	
				}else{
					if(ld.TotalSeconds>maxLd.TotalSeconds){
						maxLd = ld;
					}
				}
			}
		}
		if(maxLd != null){
			skillTraining.SetActive(true);
			time.text = maxLd.TimeStringShort;
		}else{
			skillTraining.SetActive(false);
		}
		if(teamChangeDlg.lastHighLightCell == null || teamChangeDlg.lastHighLightCell != this) return;
		if (maxLd != null){
			isSkillLearning = true;
			teamChangeDlg.stateBtn.transform.localPosition = new Vector3(80,-85,-50);
			teamChangeDlg.btnLabel.text = Localization.instance.Get("UI_Button_FINISH");//"FINISH";
			teamChangeDlg.clock.SetActive(true);
			teamChangeDlg.box.SetActive(true);
			teamChangeDlg.price.enabled = true;
			teamChangeDlg.price.text = "[Command Points] " + Formulas.HowMuchToSkipSkillTraining(maxLd.TotalSeconds);
			teamChangeDlg.trainingTimeLabel.text = maxLd.TimeStringShort;
		}else{
			isSkillLearning = false;
			teamChangeDlg.UpdateView(this);
//			teamChangeDlg.stateBtn.transform.localPosition = new Vector3(80,-55,-50);
//			teamChangeDlg.clock.SetActive(false);
//			teamChangeDlg.box.SetActive(false);
//			teamChangeDlg.price.enabled = false; 
		}
	}
	
//	public void OnCellBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		Debug.Log("OnCellBtnClick");
//		teamChangeDlg.setTeamMiniBarIsfullStr(false);
//		switch(heroData.state){
//		case HeroData.State.LOCKED:	
//			break;
//		case HeroData.State.UNLOCKED_NOT_RECRUITED:	
//			string s = "";
//			if (UserInfo.instance.getCommandPoints() >= heroData.hireCp){
//				s = string.Format(Localization.instance.Get("UI_CommonDlg_Recruit"),heroData.type,heroData.hireCp);
//				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//				dlg.onYes = delegate {
//					UserInfo.instance.consumeCommandPoints(heroData.hireCp);
//					heroData.state = HeroData.State.RECRUITED_NOT_SELECTED;
//					updateView();
//					UserInfo.instance.saveAllheroes();
//				};
//			}else{
//				s = string.Format(Localization.instance.Get("UI_CommonDlg_Recruit_NotEnough"),heroData.type,heroData.hireCp);
//				CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
//				dlg.setOneBtnDlg();
//			}
//			break;
//		case HeroData.State.RECRUITED_NOT_SELECTED:	
//			{
//				int teamHeros = 0;
//				foreach(HeroData h in UserInfo.heroDataList){
//					if(h.state == HeroData.State.SELECTED) teamHeros++;
//				}
//				if(teamHeros >= teamChangeDlg.teamMiniBar.teamCellList.Count){		// >= 4
//					if(teamChangeDlg.teamMiniBar.teamMiniBarIsfull){
//						teamChangeDlg.teamMiniBar.teamMiniBarIsfull = false;
//						teamChangeDlg.teamMiniBar.readyHeroData = null;
//					}else{
//						teamChangeDlg.setTeamMiniBarIsfullStr(true);
//						teamChangeDlg.teamMiniBar.teamMiniBarIsfull = true;
//						this.transform.parent.localScale = 1.2f*Vector3.one;
//						teamChangeDlg.teamMiniBar.readyHeroData = this.heroData;
//						teamChangeDlg.blackMask.SetActive(true);
//						if(this.GetComponent<UIPanel>() == null){
//							Transform parent = this.transform.parent;
//							parent.gameObject.AddComponent<UIPanel>();
//							parent.localPosition = new Vector3(parent.localPosition.x,parent.localPosition.y,-100f);
//						}
//						this.gameObject.SetActive(false);
//						this.gameObject.SetActive(true);
//						
//						return;
//					}
//				}
//				bool b = teamChangeDlg.teamMiniBar.addHeroData(this.heroData);	
//				Debug.Log("added,should update here,successed ="+b);	
//			}
//			break;
//		case HeroData.State.SELECTED:	
//			bool b = teamChangeDlg.teamMiniBar.removeHeroData(this.heroData);	
//			Debug.Log("removed,should update here,successed ="+b);	
//			break;
//		}
//	}
}
