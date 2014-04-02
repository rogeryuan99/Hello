using UnityEngine;
using System.Collections;

public class SkillIconData : MonoBehaviour {
public Vector2 iconPosition;
public string skillName;
public float CDTime;
public float originalCDTime;
public float currentTime;
public bool  isCoolDown;
public float tempValue;
public string id;
//	public string partners;
public CDMaskRect mask;
public SkillIcon iconView;
//gwp add
//public Hashtable number;
//<==gwp end

public static SkillIconData create ( string id ,   string name ,   float cooldown)
{
	GameObject obj = new GameObject("SkData_"+name);
	
	//	Debug.LogError("GameObject SkillData " + DestroyObjManager.Instance.destroyObjArray.Count);
	SkillIconData skillIconData = obj.AddComponent<SkillIconData>();//new SkillData();
	skillIconData.isCoolDown = false;
	skillIconData.skillName = name;
	skillIconData.id = id;
	skillIconData.CDTime    = cooldown;
	skillIconData.originalCDTime = skillIconData.CDTime;
	skillIconData.currentTime  = skillIconData.CDTime;
//	skillData.number = nums;
//		skillData.partners = partners;
	return skillIconData;
}

public void cooldownModify ( int per  ){
	CDTime = originalCDTime*(1-per/100.0f);
}

public void skillCast ( CDMaskRect pMask ,   SkillIcon icon  ){
	if(!isCoolDown)
	{
		mask = pMask;
		iconView = icon;
		iTween.ValueTo(this.gameObject,new Hashtable(){{"from",CDTime},{"to",0},{ "onupdate","updateCurrentTime"},{ "onupdatetarget",gameObject},{
										"time",CDTime},{ "easetype","linear"},{"onupdateparams",tempValue},{
										"oncomplete","unlockCooldown"},{ "oncompletetarget",gameObject}});
		isCoolDown = true;
	}
}

public void updateMaskObj ( CDMaskRect pMask ,   SkillIcon icon  ){
	mask = pMask;
	iconView = icon;
}
public void clearMaskObj (){
	mask = null;
	iconView = null;
}

public void updateCurrentTime ( float values  ){
	currentTime = values;
	if(mask != null)
	{
		mask.updateMesh(mask.diameter, currentTime/CDTime*mask.diameter);
//		mask.SetSize(80, currentTime/CDTime*80);
	}
}
public void unlockCooldown (){
	isCoolDown = false;
//	print("coolDown is over:"+skillName);
	if(iconView != null)
	{
		iconView.shock(); 
	}
	clearMaskObj();
	
}

public void reset (){
	iTween.Stop(this.gameObject);
	updateCurrentTime(0);
	unlockCooldown();
}

	public override string ToString ()
	{
		return string.Format(" Skill name:{0} id:{1}",name,id);
	}
/*
Vector2 iconPosition;
string skillName;
float CDTime;

static void create ( Vector2 pos ,   string name ,   float cooldown  ){
	GameObject obj = new GameObject();
	SkillData skillData = obj.AddComponent<SkillData>();//new SkillData();
	skillData.skillName = name;
	skillData.iconPosition = pos;
	skillData.CDTime    = cooldown;
	return skillData;
}
*/
}
