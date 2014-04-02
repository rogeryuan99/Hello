using UnityEngine;
using System.Collections;

public class TsDialog : DlgBase {
	
	public UILabel NpcName;
	//public UILabel Description;
	public UILabel CosmoDescription;
	public UILabel NarratorDescription;
	public GameObject nextBtn;
	public GameObject npcBody;
	public GameObject cosmoGroup;
	public GameObject narratorGroup;
	public GameObject cosmoTalkingBubble;
	
	// {0}Title{1}Description{2}headIcon
	public void FillData(string[] parms){
		NpcName.text = parms[0];
		//Description.text = parms[1];
		switch(parms[0].ToLower()){
			case "cosmo":
				if (false == cosmoGroup.activeInHierarchy){
					cosmoTalkingBubble.transform.localScale = new Vector3(0.01f,0.01f,1f);
					cosmoGroup.SetActive(true);
					narratorGroup.SetActive(false);
					NarratorDescription.gameObject.SetActive(false);
					CosmoDescription.gameObject.SetActive(true);
					iTween.ScaleTo(cosmoTalkingBubble, Vector3.one, .5f);
				}
				CosmoDescription.text = string.Format("{0}",Localization.instance.Get(parms[1]));
			break;
			case "narrator":
				cosmoGroup.SetActive(false);
				narratorGroup.SetActive(true);
				NarratorDescription.gameObject.SetActive(true);
				CosmoDescription.gameObject.SetActive(false);
				NarratorDescription.text = string.Format("{0}",Localization.instance.Get(parms[1]));
			break;
		}
	}
	public void OnNextBtnClick(){
	}
}
