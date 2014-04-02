using UnityEngine;
using System;
using System.Collections;

public class SkillTreeCell : MonoBehaviour {
	
//	private const string SpId_Passive_Lock 		= "ui_lock_p";
//	private const string SpId_Passive_Unlearn 	= "ui_unlock_p";		
//	private const string SpId_Passive_Learn   	= "ui_learned_p";		
//	private const string SpId_Passive_Selected  = "ui_selected_p";	
//	private const string SpId_Active_Lock 		= "ui_lock_a";
//	private const string SpId_Active_Unlearn 	= "ui_unlock_a";
//	private const string SpId_Active_Learn   	= "ui_learned_a";
//	private const string SpId_Active_Selected   = "ui_selected_a";
			
//	public UILabel textName;
	public UILabel textTime;
	public UISprite frame;
	public UISprite icon;
	public UISprite highLight;
	public UISprite trainSprite;
	public UIButton button;
	public string test_id;
	public GameObject timingGroup;
	public UISprite timingMaskSprite;
	public GameObject lockGroup;
	public UILabel unlockLvLabel;
	public SkillDef skillDef;
	public SkillLearnedData learnedData{
		get; set;
	}
	public bool IsSelected{
		get; set;
	}
	
	public int bortherID;
	
	public delegate void OnFinishedDelegate(SkillTreeCell cell);
	// public event OnFinishedDelegate OnFinished; 
	public OnFinishedDelegate OnFinished;
	
//	public State state;
//	
//	public enum State{
//		Lock,
//		Unlock,
//		Chosen,
//		Abandon,
//		NotLearn
//	}
//	
//	public void setState(State state){
//	 	this.state = state;
//		bg.spriteName = (State.Chosen == state)
//							?(skillDef.isPassive? SpId_Passive_Learn: SpId_Active_Learn)
//							:(skillDef.isPassive? SpId_Passive_Unlearn: SpId_Active_Unlearn);
//		updateTextName();
//		icon.color = (State.Lock == state)? Color.gray: Color.white;
//	}
	
	public void updateOutlook(){
		frame.spriteName = skillDef.isPassive? "skill_mask_p":"skill_mask_a"; 
		frame.MakePixelPerfect();
		timingMaskSprite.fillAmount = 0;
		switch(learnedData.State){
		case SkillLearnedData.LearnedState.LOCKED:
			lockGroup.SetActive(true);
			trainSprite.gameObject.SetActive(false);
			highLight.gameObject.SetActive(false);
			timingGroup.SetActive(false);
			unlockLvLabel.text = "Lv "+skillDef.unlockLv;
			break;
		case SkillLearnedData.LearnedState.UNLEARNED:
			lockGroup.SetActive(false);
			trainSprite.gameObject.SetActive(true);
			highLight.gameObject.SetActive(false);
			timingGroup.SetActive(false);
			break;
		case SkillLearnedData.LearnedState.LEARNING:
			lockGroup.SetActive(false);
			trainSprite.gameObject.SetActive(false);
			highLight.gameObject.SetActive(false);
			timingGroup.SetActive(true);
			timingMaskSprite.fillAmount = 1f;
			textTime.text = learnedData.TimeStringShort;
			break;
		case SkillLearnedData.LearnedState.LEARNED:
			lockGroup.SetActive(false);
			trainSprite.gameObject.SetActive(false);
			highLight.gameObject.SetActive(true);
			timingGroup.SetActive(false);

			if(IsSelected){
				highLight.color = new Color32(7,211,219,255);//on hero
			}else{
				highLight.color = new Color32(89,242,0,255);//learned
			}
//			if(skillDef.isPassive){
//				highLight.transform.localScale = new Vector3(165,165,1);	
//			}else{
//				highLight.transform.localScale = new Vector3(173,173,1);	
//			}
			//textName.text = string.Empty;
			break;
		}
//		// icon.color = (LearnedState.LOCKED == LearnedData.State)? Color.gray: Color.white;
//		icon.color = (SkillLearnedData.LearnedState.LEARNED != learnedData.State)? new Color(0.216f,0.216f,0.216f): Color.white;
		if (SkillLearnedData.LearnedState.LEARNING == learnedData.State && false == IsInvoking("UpdateTrainingTime")){
			learn();
		}
	}
//	private string getIconSpriteName(){
//		string s = "";
//		int n = UnityEngine.Random.Range(0,1000) % 5;
//		switch(n){
//		case 0:
//			s = "commonSkillIcon_Active";
//			break;
//		case 1:
//			s = "commonSkillIcon_Passive";
//			break;
//		case 2:
//			s = "skill_sample_1";
//			break;
//		case 3:
//			s = "skill_sample_2";
//			break;
//		default:
//			s = "skill_sample_3";
//			break;
//		}
//		return s;
//	}
//	private string getBgSpriteName(){
//		if(IsSelected) return (skillDef.isPassive? SpId_Passive_Selected  : SpId_Active_Selected);
//		
//		if(learnedData == null) return (skillDef.isPassive? SpId_Passive_Lock  : SpId_Active_Lock);
//		
//		switch(learnedData.State){
//		case SkillLearnedData.LearnedState.LOCKED:
//			return (skillDef.isPassive? SpId_Passive_Lock  : SpId_Active_Lock);
//			break;
//		case SkillLearnedData.LearnedState.UNLEARNED:
//		case SkillLearnedData.LearnedState.LEARNING:
//			return (skillDef.isPassive? SpId_Passive_Unlearn  : SpId_Active_Unlearn);
//			break;
//		case SkillLearnedData.LearnedState.LEARNED:
//			return (skillDef.isPassive? SpId_Passive_Learn  : SpId_Active_Learn);
//			break;
//		}
//		return null;
//	}
	public void learn(){
		if (SkillLearnedData.LearnedState.UNLEARNED == learnedData.State)
			learnedData.Learn(skillDef.learnTime);
		timingGroup.SetActive(true);
		InvokeRepeating("UpdateTrainingTime", 1f, 1f);
	}
	
	public void initBySkillDef(SkillDef sd){
		skillDef = sd;
		test_id = sd.id;
		// icon.spriteName = string.Format("SkillIcon_{0}", sd.id);
		//icon.spriteName = getBgSpriteName();
		// icon.MakePixelPerfect();
		if (null != learnedData){
			CancelInvoke("UpdateTrainingTime");
		}
		icon.spriteName = "si_"+sd.id;//getIconSpriteName();
		if(icon.GetAtlasSprite() == null){
			icon.spriteName = "si_ROCKET5B";	
		}
	}
	
//	private void updateTextName(){
//		textTime.gameObject.SetActive(SkillLearnedData.LearnedState.LEARNING == learnedData.State);
//		textName.gameObject.SetActive(SkillLearnedData.LearnedState.LEARNING != learnedData.State);
//		
//		switch(learnedData.State){
//		case SkillLearnedData.LearnedState.LOCKED:
//			textName.text = string.Format("UNLOCK\nAT LV {0}", skillDef.unlockLv);
//			break;
//		case SkillLearnedData.LearnedState.UNLEARNED:
//			textName.text = "Not Trained";
//			break;
//		case SkillLearnedData.LearnedState.LEARNING:
//			textTime.text = learnedData.Time;
//			break;
//		case SkillLearnedData.LearnedState.LEARNED:
//			textName.text = string.Empty;
//			break;
//		}
		// textName.MakePixelPerfect();
		// textName.gameObject.SetActive(true);
//	}
	
	private void UpdateTrainingTime(){
		if (learnedData.IsLearned){
			MusicManager.playEffectMusic("SFX_Skill_Training_done_1a");
			learnedData.State = SkillLearnedData.LearnedState.LEARNED;
			timingGroup.SetActive(false);
			if (null != OnFinished){
				OnFinished(this);
			}
			CancelInvoke("UpdateTrainingTime");
		}
		//updateTextName();
		float percent = (float)learnedData.TotalSeconds / (float)skillDef.learnTime;
		timingMaskSprite.fillAmount = percent;
		textTime.text = learnedData.TimeStringShort;
	}
}
 