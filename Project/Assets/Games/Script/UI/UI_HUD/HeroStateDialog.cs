using UnityEngine;
using System.Collections;

public class HeroStateDialog : MonoBehaviour {
	
	public enum ButtonType
	{
		GearUpButton,
		ChangeButton,
		TrainButton
	}
	
	public HPBar staminaBar;
	public UILabel levelLabel;
	public UILabel staminaLabel;
	public UILabel consumeStaminaLabelGameLabel;
	public Hero _hero;
	public Hero hero{
		get{ return _hero; }
		set{
			_hero = value;
			gameObject.name = string.Format("HeroStateDialog_{0}", _hero.name);
		}
	}
	
	public GameObject gearUpButtonObj;
	public UISprite gearUpButtonSprite;
	public UILabel gearUpButtonText;
	
	public GameObject changeButtonObj;
	public UISprite changeButtonSprite;
	public UILabel changeButtonText;
	
	public GameObject trainButtonObj;
	public UISprite trainButtonSprite;
	public UILabel trainButtonText;
	
	public UISprite bg;
	
	public GameObject buyStaminaBtnObj;
	
	public void Start(){
		gearUpButtonObj.SendMessage((TsFtueManager.Instance.IsGearUpCanUse? "Normal": "GrayLock")); 
		changeButtonObj.SendMessage((TsFtueManager.Instance.IsChangeCanUse? "Normal": "GrayLock"));
		trainButtonObj.SendMessage ((TsFtueManager.Instance.IsTrainCanUse?  "Normal": "GrayLock"));
	}
	
	public void initStaminaBar(int currentStamina, int maxStamina)
	{
		staminaBar.initBar(maxStamina);
		staminaBar.ChangeHpTo(currentStamina);
		
		staminaLabel.text = "Stamina : " + currentStamina;
		
		if(currentStamina == maxStamina)
		{
			buyStaminaBtnObj.SetActive(false);
			bg.transform.localScale = new Vector3(bg.transform.localScale.x, 250, bg.transform.localScale.z);
		}
		else
		{
			buyStaminaBtnObj.SetActive(true);
			bg.transform.localScale = new Vector3(bg.transform.localScale.x, 280, bg.transform.localScale.z);
		}
	}
	
	public int costStamina = 0;
	
	public void changeStaminaBar()
	{
		consumeStaminaLabelGameLabel.text = "-"+costStamina.ToString();
		
		staminaBar.ChangeHpTo((hero.data as HeroData).stamina);
		
		staminaLabel.text = "Stamina : " + (hero.data as HeroData).stamina;
	}
	
	public void initLevelLabel(int currentLevel)
	{
		levelLabel.text = "Level : " + currentLevel;
	}
	
	public void onChangeTeamBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate {
			//this.gameObject.SetActive(true);
		};
	}
	
	public void onGearUpClick()
	{
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		MusicManager.Instance.playSingleMusic("SFX_UI_button_tap_simple_1b");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamDlg dlg = DlgManager.instance.ShowTeamDlg();
		dlg.delayMusic();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.init(hero.data as HeroData);
		dlg.onClose = delegate 
		{
			initStaminaBar(dlg.heroData.stamina, dlg.heroData.staminaMax);
			foreach(Hero heroTemp in HeroMgr.heroHash.Values)
			{
				heroTemp.initData(heroTemp.data);
			}
		};
	}
	
	public void onTrainClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		SkillTreeDlg dlg = DlgManager.instance.showSkillTreeDlg(hero.data);
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate {
			Debug.LogError("dlg.onClose = delegate {");
		};
	}
	
	public void onRechargeBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		int dStamina = (this.hero.data as HeroData).staminaMax - (this.hero.data as HeroData).stamina;
		int costGold = (this.hero.data as HeroData).getStaminaRechargeCostGold();
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
				UserInfo.instance.consumeGold(costGold);	
				(this.hero.data as HeroData).addStamina(dStamina);
				this.initStaminaBar((this.hero.data as HeroData).stamina, (this.hero.data as HeroData).staminaMax);
			};
			dlg.onNo = delegate {
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			};
		}
	}
	
	public void enableAllButton(bool isButtonEnabled)
	{
		this.enabledGearUpButton(isButtonEnabled);
		this.enabledChangeButton(isButtonEnabled);
		this.enabledTrainButton(isButtonEnabled);
	}
	
	public void enabledGearUpButton(bool isButtonEnabled)
	{
		this.enabledButton(gearUpButtonObj, gearUpButtonSprite, gearUpButtonText, isButtonEnabled);
	}
	
	public void enabledChangeButton(bool isButtonEnabled)
	{
		this.enabledButton(changeButtonObj, changeButtonSprite, changeButtonText, isButtonEnabled);
	}
	
	public void enabledTrainButton(bool isButtonEnabled)
	{
		this.enabledButton(trainButtonObj, trainButtonSprite, trainButtonText,isButtonEnabled);
	}
	
	public void enabledButton(GameObject buttonObj, UISprite buttonSprite, UILabel label, bool isButtonEnabled)
	{
		buttonObj.transform.collider.enabled = isButtonEnabled;
		if(isButtonEnabled){
			
			buttonSprite.color = new Color32(8, 225, 215, 229);
			label.color = Color.white;
			
		}else{
			buttonSprite.color = Color.gray;
			label.color = Color.gray;
		}
	}
	
	public void highlightGearUpButton(bool isEnabled)
	{
		highlightButton(gearUpButtonSprite, isEnabled);
	}
	
	public void highlightChangeButton(bool isEnabled)
	{
		highlightButton(changeButtonSprite, isEnabled);
	}
	
	public void highlightTrainButton(bool isEnabled)
	{
		highlightButton(trainButtonSprite, isEnabled);
	}
	
	protected void highlightButton(UISprite sprite, bool isEnabled)
	{
		sprite.gameObject.GetComponent<TweenColor>().enabled = isEnabled;
		if(!isEnabled)
		{
			sprite.gameObject.GetComponent<UISprite>().color = new Color32(8,225,215,229);	
		}
	}
	
	public void highlightAllButtons(bool isEnabled)
	{
		highlightGearUpButton(isEnabled);
		highlightChangeButton(isEnabled);
		highlightTrainButton(isEnabled);
	}
	
	public void highlightButtonByButtonType(ButtonType buttonType,  bool isEnabled)
	{
		if(buttonType == ButtonType.ChangeButton)
		{
			highlightChangeButton(isEnabled);
		}
		else if(buttonType == ButtonType.TrainButton)
		{
			highlightTrainButton(isEnabled);
		}
	}
}
