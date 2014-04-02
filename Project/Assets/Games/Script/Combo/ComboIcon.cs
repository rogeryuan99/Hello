using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboIcon : MonoBehaviour {
	
	public UISprite maskObj;
	public UISprite sprite;
	
	public List<SkillIconData> skillIconDataList = new List<SkillIconData>();
	
	public delegate bool OnClickDelegate(string[] hTypes, List<SkillIconData> skillIconDataList);
	public OnClickDelegate OnClickCallback;
	
	private const float CDTIME = 3f;
	private float cdTime = -.002f;
	
	private string[] heroTypes = new string[2];
	
	public void Update(){
		if(StaticData.isBattleEnd && !TsTheater.InTutorial){
			gameObject.SetActive(false);
		}
		else {
			gameObject.SetActive(true);
			ColdDown();
		}
	}
	
	
	public void OnClick(){
		if (cdTime > 0){
			return;
		}
		
		if (null != OnClickCallback
				&& OnClickCallback(heroTypes, skillIconDataList)){
			cdTime = CDTIME;
		}
	}
	
	public void ColdDown(){
		if (0 != cdTime){
			cdTime = cdTime > 0? cdTime-Time.deltaTime: 0f;
			maskObj.fillAmount = cdTime / CDTIME;
		}
	}
	
	public void Show(string hType, string pType){
		heroTypes[0] = hType.CompareTo(pType) < 0? hType: pType;
		heroTypes[1] = hType.CompareTo(heroTypes[0]) != 0? hType: pType;
		
		sprite.spriteName  = string.Format("ComboIcon_{0}_{1}", heroTypes[0], heroTypes[1]);
		if (null==sprite.GetAtlasSprite()){
			sprite.spriteName = "ComboIcon_DEFAULT";
		}
		maskObj.spriteName = sprite.spriteName;
		
		ColdDown();
		
		gameObject.SetActive(true);
	}
	
	public void Hide(){
		gameObject.SetActive(false);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	/*
	public SkillData skillData;
	private CDMaskRect mask;
	
	private Vector3 originalPt;
	
	public static Hashtable skillUserHash = new Hashtable();
	
	public void Awake (){	
		MsgCenter.instance.addListener(MsgCenter.FREEZE_START, freezeStart);
		mask = maskObj.GetComponent<CDMaskRect>();
		maskObj.transform.parent = transform;
		mask.updateMesh(mask.diameter,0);
	}
	
	public void Start (){
		originalPt = this.gameObject.transform.localPosition;
		
		this.gameObject.transform.position = new Vector3(-4000f,-4000f,-4000f);
	}
	
	public void resetIcon (){
		
	}
	
	public void clearData (){
		if(skillData != null)
		{
			skillData.clearMaskObj();
		}
		skillData = null;
		maskObj.transform.localPosition.SetZ(1000f);
		maskObj.SetActive(false);
	}
	
	public void buildSkill ( SkillData skillD  ){
		if(skillData != null)
		{
			skillData.clearMaskObj();
		}
		skillData = skillD;
		this.gameObject.SetActive(true);
		this.gameObject.transform.localPosition = originalPt;
		
		sprite.spriteName = "SkillIcon_"+ skillData.skillName;//gwp id-->name
		sprite.MakePixelPerfect();	
		if(skillData.isCoolDown)
		{
			HUDItemIconCCD.skillCDNum ++;
			skillData.updateMaskObj(mask,this);
			sprite.spriteName = "SkillIcon_"+ skillData.skillName + "_Disable";
			sprite.MakePixelPerfect();
			maskObj.transform.localPosition.SetZ(-1f);
		}else{
			sprite.spriteName = "SkillIcon_"+ skillData.skillName;
			sprite.MakePixelPerfect();	
			mask.updateMesh(mask.diameter,0);
			maskObj.transform.localPosition.SetZ(-1f);
		}
		maskObj.SetActive(true);
	}
	
	public void shock (){
		gameObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
		Hashtable hs = new Hashtable(){{"scale",Vector3.one},{ "time",0.5f},{"easetype","easeOutBounce"}};
		iTween.ScaleTo(gameObject, hs);
		sprite.spriteName = "SkillIcon_"+ skillData.skillName;
		sprite.MakePixelPerfect();	
	}
	
	public void clickAnim (){
		iTween.Stop(gameObject);
		gameObject.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
		iTween.ScaleTo(gameObject, new Hashtable(){{"scale",Vector3.one},{ "time",0.5f}});//,{"easetype","easeInBounce"}});
	}
	
	public delegate void IntParmCallback(SkillIcon icon);
	public IntParmCallback ClickHandler;
	
	public void CallSkill(){
		if(skillData == null) return;
		
		if(!skillData.isCoolDown){
			if(!BattleBg.canUseSkill){
				return;
			}
			sprite.spriteName = "SkillIcon_"+ skillData.skillName + "_Disable";
			sprite.MakePixelPerfect();
			if (null != ClickHandler){
				ClickHandler(this);
			}
			clickAnim();
			skillData.skillCast(mask,this);
				
			//xingyihua
			setSkillUser();
			
			HUDItemIconCCD.skillCDNum ++;
			if(HUDItemIconCCD.skillCDNum < 2){
				Message msg = new Message(MsgCenter.SKILL_CD_UPDATE, this);
				MsgCenter.instance.dispatch(msg);
			}
		}
	}
	
	public void CancelCallSkill(){
			sprite.spriteName = "SkillIcon_"+ skillData.skillName;
			sprite.MakePixelPerfect();
		}
		
	public void setSkillUser (){
		if(skillUserHash.ContainsKey(skillData.skillName)){
			int num = (int)skillUserHash[skillData.skillName];
			num++;
			skillUserHash[skillData.skillName] = num;
		}else{
			skillUserHash[skillData.skillName] = 1;
		}
	}
	
	public static ArrayList getSkillUser (){
		ArrayList skillUser = new ArrayList();
		foreach( DictionaryEntry skillUserData in skillUserHash)
		{
			int num = (int)skillUserData.Value;
			string skillName = skillUserData.Key.ToString();
			
			skillUser.Add(skillName+"_"+num);
		}
		return skillUser;
	}
	
	void OnDestroy (){
		skillUserHash.Clear();
		MsgCenter.instance.removeListener(MsgCenter.FREEZE_START, freezeStart);
	}
	*/
}
