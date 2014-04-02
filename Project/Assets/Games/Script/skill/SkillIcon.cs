using UnityEngine;
using System.Collections;

public class SkillIcon : MonoBehaviour {
public string methodName;
public SkillIconData skillIconData;
public GameObject maskObj;
//public GameObject skillBg;
//public GameObject skillBgSilver;
public UISprite sprite;
private CDMaskRect mask;

private float scaleValue;

private RaycastHit outHit;
	
	public UILabel skillIdLabel;

//modified by xiaoyong 20130702 for mesh effect was not work at first display
private Vector3 originalPt;

//public static Hashtable skillUserHash = new Hashtable();
public void Awake (){	
	MsgCenter.instance.addListener(MsgCenter.FREEZE_START, freezeStart);
	mask = maskObj.GetComponent<CDMaskRect>();
	maskObj.transform.parent = transform;
	mask.updateMesh(mask.diameter,0);
//	sprite.spriteName = "SkillIcon_"+ skillData.skillName + "_Disable";
//	sprite.MakePixelPerfect();
	//skillBg.transform.localPosition.SetZ(1000f);
	//skillBgSilver.transform.localPosition.SetZ(0f);
}

//xingyihua 8.14f
public void freezeStart ( Message msg  ){
	this.gameObject.SetActive(false);
}
//end

public void Start (){
	originalPt = this.gameObject.transform.localPosition;
	
	this.gameObject.transform.localPosition = new Vector3(-4000f,-4000f,0);
}

public void resetIcon (){
	
}

public void clearData (){
	if(skillIconData != null)
	{
		skillIconData.clearMaskObj();
	}
	skillIconData = null;
	//skillBg.transform.localPosition.SetZ(1000f);
	//skillBgSilver.transform.localPosition.SetZ(1000f);
	//maskObj.transform.localPosition.SetZ(1000f);
	//skillBg.SetActive(false);
	//skillBgSilver.SetActive(false);
	maskObj.SetActive(false);
}

public void buildSkill ( SkillIconData skillD  ){
	if(skillIconData != null)
	{
		skillIconData.clearMaskObj();
	}
	this.skillIconData = skillD;
	this.gameObject.SetActive(true);
	//modified by xiaoyong 20130702 for mesh effect was not work at first display
	this.gameObject.transform.localPosition = originalPt;
	changeSprite(sprite,"si_"+ skillIconData.skillName );
//	sprite.spriteName = "SkillIcon_"+ skillData.skillName;//gwp id-->name
//	sprite.MakePixelPerfect();	
	if(skillIconData.isCoolDown)
	{
		skillIconData.updateMaskObj(mask,this);
//		sprite.spriteName = "SkillIcon_"+ skillData.skillName + "_Disable";
//		sprite.MakePixelPerfect();
		//changeSprite(sprite,"SkillIcon_"+ skillIconData.skillName + "_Disable");
		maskObj.transform.localPosition.SetZ(-1f);
		skillIdLabel.text = skillIconData.skillName;
			
	}else{
//		sprite.spriteName = "SkillIcon_"+ skillData.skillName;
//		sprite.MakePixelPerfect();	
		//changeSprite(sprite,"SkillIcon_"+ skillIconData.skillName);	
		mask.updateMesh(mask.diameter,0);
		maskObj.transform.localPosition.SetZ(-1f);
			skillIdLabel.text = skillIconData.skillName;
	}
	maskObj.SetActive(true);
}
private void changeSprite(UISprite sprite, string targetSpName, string defaultSpName = "si_ROCKET5B"){
	if(sprite.atlas.GetSprite(targetSpName) != null){
		sprite.spriteName = targetSpName;
	}else{
		sprite.spriteName = defaultSpName;
	}
	//sprite.MakePixelPerfect();		
}
public void shock (){
	gameObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
	Hashtable hs = new Hashtable(){{"scale",Vector3.one},{ "time",0.5f},{"easetype","easeOutBounce"}};
	iTween.ScaleTo(gameObject, hs);
//	sprite.spriteName = "SkillIcon_"+ skillData.skillName;
//	sprite.MakePixelPerfect();	
	//changeSprite(sprite,"SkillIcon_"+ skillIconData.skillName);
    //skillBg.transform.localPosition.SetZ(0f);
    //skillBgSilver.transform.localPosition.SetZ(1000f);
}
public void clickAnim (){
	iTween.Stop(gameObject);
	gameObject.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
	iTween.ScaleTo(gameObject, new Hashtable(){{"scale",Vector3.one},{ "time",0.5f}});//,{"easetype","easeInBounce"}});
}
	
// public BattleObjects
 public void clickHandler ( MonoBehaviour mono ,   string methodN  ){
 	methodName    = methodN;
 }
	public delegate void IntParmCallback(SkillIcon icon);
	public IntParmCallback ClickHandler;
	
	public void CallSkill(){
		if(skillIconData == null) return;
		
		if(!skillIconData.isCoolDown)
		{
			if(!BattleBg.canUseSkill)
			{
				return;
			}
			MusicManager.playEffectMusic("SFX_skill_icon_1b");
			
			//sprite.spriteName = "SkillIcon_"+ skillData.skillName + "_Disable";
			//sprite.MakePixelPerfect();
			//changeSprite(sprite,"SkillIcon_"+ skillIconData.skillName + "_Disable");
			//skillBg.transform.localPosition.SetZ(1000f);
			//skillBgSilver.transform.localPosition.SetZ(0f);
			// monoBehaviour.Invoke(methodName,0);
			if (null != ClickHandler)
			{
				ClickHandler(this);
			}
			clickAnim();
			skillIconData.skillCast(mask,this);
				
			//xingyihua
//			setSkillUser();
			// CallComboSkill(skillIconData.id);
		}
	}
	
//	public void CallComboSkill(string skillID)
//	{
//		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID(skillID);
//		if(skillDef.comboPartners != null && skillDef.comboPartners != string.Empty)
//		{			
//			Character character = HeroMgr.getHeroByType(skillDef.comboPartners);
//			
//			if(!character.isActionStateActive(Character.ActionStateIndex.CallSkillState))
//			{
//				return;
//			}
//			
//			character.setTarget(BattleObjects.skHero.getTarget());
//			
////			SkillIconData sid = SkillIconManager.Instance.getSkillIconData(skillDef.comboPartners+"2");
////			SkillIconData skillIconData = SkillIconData.create(skillDef.id, skillDef.funcName,skillDef.coolDown);
//			SkillDef comboSkillDef = SkillLib.instance.allHeroSkillHash[skillDef.comboPartners+"2"] as SkillDef;
//			SkillIconData sid = SkillIconData.create(comboSkillDef.id, comboSkillDef.funcName,comboSkillDef.coolDown);
//			sid.skillCast(null,null);
//			character.PushSkillIdToContainer(sid.id);
//		
//			// SkillManager.Instance.callSkill(skilldata.skillName + "Pre",new ArrayList(){BattleBg.Instance.gameObject,BattleObjects.skHero.gameObject, BattleObjects.skHero.getTarget()});
//			SkillManager.Instance.callSkillPrepare
//			(
//				sid.id, 
//				new ArrayList()
//				{
//					BattleBg.Instance.gameObject,
//					character.gameObject, 
//					character.getTarget()
//				}
//			);
//			
//		}
//	}
	
	public void CancelCallSkill(){
		//sprite.spriteName = "SkillIcon_"+ skillData.skillName;
		//sprite.MakePixelPerfect();
		//changeSprite(sprite,"si_"+ skillIconData.skillName);
		//skillBg.transform.localPosition.SetZ(0f);
		//skillBgSilver.transform.localPosition.SetZ(1000f);
	}
	
//	public void setSkillUser ()
//	{
//		if(skillUserHash.ContainsKey(skillIconData.skillName))
//		{
//			int num = (int)skillUserHash[skillIconData.skillName];
//			num++;
//			skillUserHash[skillIconData.skillName] = num;
//		}
//		else
//		{
//			skillUserHash[skillIconData.skillName] = 1;
//		}
//	}

//public static ArrayList getSkillUser (){
//	ArrayList skillUser = new ArrayList();
//	foreach( DictionaryEntry skillUserData in skillUserHash)
//	{
//		int num = (int)skillUserData.Value;
//		string skillName = skillUserData.Key.ToString();
//		
//		skillUser.Add(skillName+"_"+num);
//	}
//	return skillUser;
//}

void OnDestroy (){
//	skillUserHash.Clear();
	MsgCenter.instance.removeListener(MsgCenter.FREEZE_START, freezeStart);
}
}
