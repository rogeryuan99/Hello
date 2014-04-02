using UnityEngine;
using System.Collections;

public class HeroStateDialogNew : MonoBehaviour {
	
//	public enum ButtonType
//	{
//		ChangeButton,
//		TrainButton
//	}
//	
	public GameObject groupTraining;
	public GameObject groupNotTraining;
	public GameObject groupAdd;
	public GameObject button;
	public UILabel expLvLabel;
	public ExpBar expBar;
	public UILabel trainingTimeLabel;
	public Hero hero;
	//private static int _index4TextADD = 0;
	private HeroData hd;
	public void init(Hero hero,int _index4TextADD){
		this.hero = hero;
		if(hero == null){
			groupTraining.SetActive(false);
			groupNotTraining.SetActive(false);
			groupAdd.SetActive(true);	
			gameObject.name = string.Format("HeroStateDialog_{0}{1}", "ADD", _index4TextADD);
		}else{
			hd = hero.data as HeroData;
			gameObject.name = string.Format("HeroStateDialog_{0}", hero.name);
			expLvLabel.text = "Level " + hd.lv;
			expBar.initBar(hd.exp);
			groupTraining.SetActive(false);
			groupNotTraining.SetActive(false);
			groupAdd.SetActive(false);	
		}
		//_index4TextADD = (++_index4TextADD > 3)? 0: _index4TextADD;
	}
	
	public GameObject changeButtonObj;
	public UISprite changeButtonSprite;
	public UILabel changeButtonText;
	
	public GameObject trainButtonObj;
	public UISprite trainButtonSprite;
	public UILabel trainButtonText;
	
	public void Start(){
		//changeButtonObj.SendMessage((TsFtueManager.Instance.IsChangeCanUse? "Normal": "GrayLock"));
		//trainButtonObj.SendMessage ((TsFtueManager.Instance.IsTrainCanUse?  "Normal": "GrayLock"));
	}

	public void onRemoveClicked(){
		if(HeroMgr.heroHash.Count == 1){
			Debug.Log("Can not remove the last hero");
			return;
		}
		hd.state = HeroData.State.RECRUITED_NOT_SELECTED;
		BattleBg.Instance.heroOutBattle(false, 0);	
	}

	public void onChangeTeamBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate {
			Debug.Log("...... on close");
			//this.gameObject.SetActive(true);
			MusicManager.playBgMusic(MapMgr.Instance.getCurrentLevel().bgMusic);
		};
	}
	
	
	public void onTrainClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.Log("FinishTrain");
		MusicManager.playBgMusic(MapMgr.Instance.getCurrentLevel().bgMusic);
	}
	
	public void FixedUpdate(){
		if(hero == null) return;
		SkillLearnedData maxLd = null;
		foreach(SkillLearnedData ld in hd.learnedSkillIdList){
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
		if (maxLd != null){
			groupTraining.SetActive(true);
			groupNotTraining.SetActive(false);
			trainingTimeLabel.text = maxLd.TimeStringShort;
		}else{
			groupTraining.SetActive(false);
			groupNotTraining.SetActive(true);
		}
		
		if( tempOldHeorsCount != HeroMgr.heroHash.Count){
			tempOldHeorsCount = HeroMgr.heroHash.Count;
			if(tempOldHeorsCount == 1){
				button.gameObject.SendMessage("GrayLock");	
			}else{
				button.gameObject.SendMessage("Normal");	
			}
		}
	}
	private int tempOldHeorsCount = -1;
	
	
	
	
//	public void enableAllButton(bool isButtonEnabled)
//	{
//		this.enabledGearUpButton(isButtonEnabled);
//		this.enabledChangeButton(isButtonEnabled);
//		this.enabledTrainButton(isButtonEnabled);
//	}
//	
//	public void enabledGearUpButton(bool isButtonEnabled)
//	{
//		this.enabledButton(gearUpButtonObj, gearUpButtonSprite, gearUpButtonText, isButtonEnabled);
//	}
//	
//	public void enabledChangeButton(bool isButtonEnabled)
//	{
//		this.enabledButton(changeButtonObj, changeButtonSprite, changeButtonText, isButtonEnabled);
//	}
//	
//	public void enabledTrainButton(bool isButtonEnabled)
//	{
//		this.enabledButton(trainButtonObj, trainButtonSprite, trainButtonText,isButtonEnabled);
//	}
//	
//	public void enabledButton(GameObject buttonObj, UISprite buttonSprite, UILabel label, bool isButtonEnabled)
//	{
//		buttonObj.transform.collider.enabled = isButtonEnabled;
//		if(isButtonEnabled){
//			
//			buttonSprite.color = new Color32(8, 225, 215, 229);
//			label.color = Color.white;
//			
//		}else{
//			buttonSprite.color = Color.gray;
//			label.color = Color.gray;
//		}
//	}
//	
//	public void highlightGearUpButton(bool isEnabled)
//	{
//		highlightButton(gearUpButtonSprite, isEnabled);
//	}
//	
//	public void highlightChangeButton(bool isEnabled)
//	{
//		highlightButton(changeButtonSprite, isEnabled);
//	}
//	
//	public void highlightTrainButton(bool isEnabled)
//	{
//		highlightButton(trainButtonSprite, isEnabled);
//	}
//	
//	protected void highlightButton(UISprite sprite, bool isEnabled)
//	{
//		sprite.gameObject.GetComponent<TweenColor>().enabled = isEnabled;
//		if(!isEnabled)
//		{
//			sprite.gameObject.GetComponent<UISprite>().color = new Color32(8,225,215,229);	
//		}
//	}
//	
//	public void highlightAllButtons(bool isEnabled)
//	{
//		highlightGearUpButton(isEnabled);
//		highlightChangeButton(isEnabled);
//		highlightTrainButton(isEnabled);
//	}
//	
//	public void highlightButtonByButtonType(ButtonType buttonType,  bool isEnabled)
//	{
//		if(buttonType == ButtonType.ChangeButton)
//		{
//			highlightChangeButton(isEnabled);
//		}
//		else if(buttonType == ButtonType.TrainButton)
//		{
//			highlightTrainButton(isEnabled);
//		}
//	}
	public void onGearUpClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamDlg dlg = DlgManager.instance.ShowTeamDlg();
		dlg.delayMusic();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.init(hero.data as HeroData);
		dlg.onClose = delegate 
		{
			foreach(Hero heroTemp in HeroMgr.heroHash.Values)
			{
				heroTemp.initData(heroTemp.data);
			}
			MusicManager.playBgMusic(MapMgr.Instance.getCurrentLevel().bgMusic);
		};
	}

}
