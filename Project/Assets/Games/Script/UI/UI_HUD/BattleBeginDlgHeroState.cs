using UnityEngine;
using System.Collections;

public class BattleBeginDlgHeroState : MonoBehaviour {
	
	public enum ButtonType
	{
		GearUpButton,
		ChangeButton,
		TrainButton
	}
	
	public UISprite avatar;
	public ExpBar xpBar;
	public UILabel levelLabel;
	public UILabel staminaLabel;
	public int currentStamina;
	public UILabel consumeStaminaLabelGameLabel;
	private Hero _hero;
	[HideInInspector]
	public Hero hero{
		get{ return _hero; }
		set{
			_hero = value;
			gameObject.name = string.Format("HeroStateDialog_{0}", _hero.name);
			HeroData hd = (HeroData)hero.data;
			
			initXp(hd.exp);
//			initStamina();

			avatar.spriteName = hd.type;
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
	
	public GameObject buyStaminaBtnObj;
	
	public void Start(){
		gearUpButtonObj.SendMessage((TsFtueManager.Instance.IsGearUpCanUse? "Normal": "GrayLock")); 
		changeButtonObj.SendMessage((TsFtueManager.Instance.IsChangeCanUse? "Normal": "GrayLock"));
		trainButtonObj.SendMessage ((TsFtueManager.Instance.IsTrainCanUse?  "Normal": "GrayLock"));
	}
	
	private void initXp(int xp)
	{
		levelLabel.text = string.Format(Localization.instance.Get("UI_BattleBeginDlg_Level"),((HeroData)hero.data).lv);//"Level: [ffff00]" + ((HeroData)hero.data).lv;
		xpBar.initBar(xp);
	}
	
	public void setConsumeStaminaLabelGameLabel(int consumeStamina)
	{
		initStamina();
		consumeStaminaLabelGameLabel.text = "-" + consumeStamina;
		consumeStaminaLabelGameLabel.gameObject.SetActive(true);
		
		iTween.MoveTo
			(
				consumeStaminaLabelGameLabel.gameObject,
				iTween.Hash
				(
					"y",consumeStaminaLabelGameLabel.gameObject.transform.localPosition.y + 30,
					"islocal",true,
					"easetype","linear",
					"time",1.0f
					
				)
			);
	}
	
	private void initStamina()
	{
		HeroData hd = (HeroData)hero.data;
		
		currentStamina = hd.stamina;
		
		buyStaminaBtnObj.SetActive(false);
		
//		staminaLabel.text = string.Format(Localization.instance.Get("UI_BattleBeginDlg_Stamina"),currentStamina);//"Stamina:[ffff00]" + currentStamina;
		
//		if(hd.stamina == hd.staminaMax)
//		{
//			buyStaminaBtnObj.SetActive(false);
//		}
//		else
//		{
//			buyStaminaBtnObj.SetActive(true);
//		}
	}
	
	public int costStamina = 0;
	
	void onChangeTeamBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate
		{
			//this.gameObject.SetActive(true);
			initStamina();
		};
	}
	
	void onGearUpClick()
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
			foreach(Hero heroTemp in HeroMgr.heroHash.Values)
			{
				heroTemp.initData(heroTemp.data);
			}
			initStamina();
		};
	}
	
	void onTrainClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		BattleBg.Instance.heroTeleportToBattleStartPos();
		SkillTreeDlg dlg = DlgManager.instance.showSkillTreeDlg(hero.data);
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate
		{
			Debug.LogError("dlg.onClose = delegate {");
		};
	}
	
	void onRechargeBtnClick()
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		int dStamina = (this.hero.data as HeroData).staminaMax - (this.hero.data as HeroData).stamina;
		int costGold = (this.hero.data as HeroData).getStaminaRechargeCostGold();
		string s;
		if(UserInfo.instance.getGold() < costGold)
		{
			MusicManager.playEffectMusic("SFX_Error_Message_1c");
			//s = "Your Gold is not enough!";
			s = string.Format("{0}",Localization.instance.Get("UI_CommonDlg_NotEnoughGold"));
			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
			dlg.setOKDlg();
		}
		else
		{
			//s = "Are you sure to recharge [00FFFF]" + dStamina + " [FFFFFF][Stamina] with [00FFFF]" + costGold + " [FFFFFF][Gold] ?";
			s = string.Format(Localization.instance.Get("UI_CommonDlg_Recharge"),dStamina,costGold);
			CommonDlg dlg = DlgManager.instance.ShowCommonDlg(s);
			dlg.onYes = delegate
			{
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
				UserInfo.instance.consumeGold(costGold);	
				(this.hero.data as HeroData).addStamina(dStamina);
				this.initStamina();
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
	
	public void Update()
	{
		HeroData hd = (HeroData)hero.data;
		if(hd.stamina == currentStamina)
		{
			return;
		}
		this.initStamina();
	}
}
