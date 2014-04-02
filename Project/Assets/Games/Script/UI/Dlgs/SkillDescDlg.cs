using UnityEngine;
using System.Collections;

public class SkillDescDlg : MonoBehaviour {
	public UILabel Title;
	public UILabel Description;
	public UISprite DescIconBg;
	public UISprite DescIcon;
	
	public GameObject TrainingBtn;
	public GameObject SkippingBtn;
	public UILabel buttonText;
	public UILabel skillTrainTimeLeftText;
	public UISprite skillTrainTimeMask;
	public void Show(SkillDef def){
//		Title.text = def.name;
//		Description.text = def.description;
		Title.text = string.Format("{0}",Localization.instance.Get("Skill_Name_"+def.id));
		Description.text = string.Format("{0}",Localization.instance.Get("Skill_Desc_"+def.id));
		// Icon.spriteName = "???";
//		Tween.Reset();
//		Tween.Play(true);
	}
	public void Hide(){
		TrainingBtn.SetActive(false);
		this.SkippingBtn.SetActive(false);
		DescIconBg.gameObject.SetActive(false);
		DescIcon.gameObject.SetActive(false);
	}
	
	public void SetTrainingBtnVisible(bool visible){
		TrainingBtn.SetActive(visible);
	}
	public void SetSkippingBtnVisible(bool visible){
		SkippingBtn.SetActive(visible);
	}
//	
//	private void SwapTweenRefObject(){
//		Transform temp = Tween.from;
//		Tween.from = Tween.to;
//		Tween.to = temp;
//	}
}
