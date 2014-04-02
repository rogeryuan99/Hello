using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleBeginDlg : MonoBehaviour
{
	public UIImageButton battleButton;
	public GameObject heroStateTemplate;
	public GameObject heroEmptySlotTemplate;
	public UILabel labelStaminaCost;
	private Hashtable colums = new Hashtable();
	
	List<BattleBeginDlgHeroState> battleBeginDlgHeroStateList = new List<BattleBeginDlgHeroState>();
	
	void Start()
	{
		int n = 0;
		foreach(Hero h in HeroMgr.heroHash.Values)
		{
			GameObject colum = heroStateTemplate;
			if(n>0){
				colum = Instantiate(heroStateTemplate) as GameObject;	
				colum.transform.parent = heroStateTemplate.transform.parent;
				colum.transform.localScale = heroStateTemplate.transform.localScale;
				colum.transform.localPosition = heroStateTemplate.transform.localPosition + new Vector3(160*n,0,0); 
				
			}
			BattleBeginDlgHeroState bbhs = colum.GetComponent<BattleBeginDlgHeroState>();
			bbhs.hero = h;
			battleBeginDlgHeroStateList.Add(bbhs);
			colums.Add(h.data.type,colum.GetComponent<BattleBeginDlgHeroState>());
			n ++;
		}
		for(int k = n;k<4;k++){
			GameObject slot = Instantiate(heroEmptySlotTemplate) as GameObject;	
			slot.transform.parent = heroStateTemplate.transform.parent;
			slot.transform.localScale = heroStateTemplate.transform.localScale;
			slot.transform.localPosition = heroStateTemplate.transform.localPosition + new Vector3(160*k,0,0); 
		}
		heroEmptySlotTemplate.SetActive(false);
		//this.labelStaminaCost.text = string.Format("Cost [ffffff]{0}[-] Stamina for each hero",Formulas.getCostStaminaByLevel(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
		this.labelStaminaCost.text = "";//string.Format(Localization.instance.Get("UI_BattleBeginDlg_StaminaCost"),Formulas.getCostStaminaByLevel(MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex));
	}
	void OnBtnBackClicked(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		Debug.LogError("OnBtnBackClicked");
		Application.LoadLevel("UIMain");
	}
	
	public void setConsumeStaminaLabelGameLabel(int consumeStamina)
	{
		if(battleBeginDlgHeroStateList.Count <= 0)
		{
			return;
		}
		foreach(BattleBeginDlgHeroState bbdhs in battleBeginDlgHeroStateList)
		{
			bbdhs.setConsumeStaminaLabelGameLabel(consumeStamina);
		}
	}
	
	void OnBattleBtnClick()
	{
		enableBattleButton(false);
		Debug.LogError("OnBattleBtnClick");
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		LevelMgr.Instance.OnBattleBtnClickForMusic();
		LevelMgr.Instance.OnBattleBtnClick();
//		Destroy(this.gameObject);
	}
	
	public void enableBattleButton(bool b)
	{
		battleButton.collider.enabled = b;
	}
	
	void OnEmptySlotClicked(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		dlg.transform.localPosition += new Vector3(0, 0, -600);
		dlg.onClose = delegate {
			//this.gameObject.SetActive(true);
		};
	}
}