using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillIconManager : Singleton<SkillIconManager> 
{
	// key herotype value List<SkillIconData>
	public List<SkillIconData> skillIconDataList = new List<SkillIconData>();
	
	public enum TargetType{
		PLAYER, ENEMY, NONE
	}
	
	private string skillTarget;
	private int skillCastCount = 0;
	public GameObject []skIcons = new GameObject[6];
	
	
	public SkillIconData getSkillIconData(string skillID)
	{
		foreach(SkillIconData skillIconData in this.skillIconDataList)
		{
			if(skillIconData.id == skillID)
			{
				return skillIconData;
			}
		}
		return null;
	}
	
	public List<SkillIconData> getSkillIconDataListByHeroType(string heroType)
	{
		List<SkillIconData> skillIconDataListTemp = new List<SkillIconData>();
		foreach(SkillIconData skillIconData in this.skillIconDataList)
		{
			if(skillIconData.id.Contains(heroType))
			{
				skillIconDataListTemp.Add(skillIconData);
			}
		}
		return skillIconDataListTemp;
	}
	
	public void createSkillIconDataSkillByHeroData(HeroData heroData)
	{
		this.destroySkillIconDataList(heroData.type);
		heroData.activeSkillIDList.Sort(delegate (string x,string y){
			x=x.Replace("27","");
			y=y.Replace("27","");
			while(x.Length>0){
				if(x[0].CompareTo("9"[0])>0){
					x = x.Substring(1);
				}else{
					break;	
				}
			}
			while(x.Length>0){
				if(x[x.Length-1].CompareTo("9"[0])>0){
					x = x.Remove(x.Length-1);
				}else{
					break;	
				}
			}
			while(y.Length>0){
				if(y[0].CompareTo("9"[0])>0){
					y = y.Substring(1);
				}else{
					break;	
				}
			}
			while(y.Length>0){
				if(y[y.Length-1].CompareTo("9"[0])>0){
					y = y.Remove(y.Length-1);
				}else{
					break;	
				}
			}
//			Debug.Log("x="+x+" y="+y);
			int xx = 0;
			int.TryParse(x,out xx);
			int yy = 0;
			int.TryParse(y,out yy);
			return (xx<yy)?-1:1;
			
			//maybe too many loops, roger
		});
		foreach(string activeSkillID in heroData.activeSkillIDList)
		{
			SkillDef skillDef = SkillLib.instance.allHeroSkillHash[activeSkillID] as SkillDef;
			
			SkillIconData skillIconData = SkillIconData.create(skillDef.id, skillDef.funcName,skillDef.coolDown);
			this.skillIconDataList.Add(skillIconData);
			
		}
	}
	
	public void createAllSkillIconDataSkill()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			createSkillIconDataSkillByHeroData((hero.data as HeroData));
		}
	}
	
	public void destroyAllSkillIconDataList()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			this.destroySkillIconDataList((hero.data as HeroData).type);
		}
	}
	
	public void destroySkillIconDataList(string heroType)
	{
		List<SkillIconData> deleteSkillIconData = new List<SkillIconData>();
		
		foreach(SkillIconData skillIconData in this.skillIconDataList)
		{
			if(skillIconData.id.Contains(heroType))
			{
				deleteSkillIconData.Add(skillIconData);
				Destroy(skillIconData.gameObject);
			}
		}
		foreach(SkillIconData skillIconData in deleteSkillIconData)
		{
			this.skillIconDataList.Remove(skillIconData);
		}
	}
	
	
	public void Update()
	{
		if(StaticData.isBattleEnd && !TsTheater.InTutorial)
		{
			HideSkillIcon();
		}
	}
	
	public void clearAllData()
	{
		for(int i = 0; i < skIcons.Length; ++i)
		{
			SkillIcon skillSlot = skIcons[i].GetComponent<SkillIcon>();
			skillSlot.clearData();
		}
	}
	
	public void ChangeSkillDisplay ()
	{
		HideSkillIcon();
		
		if(BattleObjects.skHero != null)
		{
			int skIconsIndex = 0;
			List<SkillIconData> skillIconDataListTemp = this.getSkillIconDataListByHeroType((BattleObjects.skHero.data as HeroData).type);			
			
			for(int i = 0; i < skillIconDataListTemp.Count; ++i)
			{
				SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID(skillIconDataListTemp[i].id);
				if(skillDef.comboPartners != null && skillDef.comboPartners != string.Empty)
				{
					if(HeroMgr.getHeroByType(skillDef.comboPartners) == null)
					{
						continue;
					}
					
					Hero comboPartners = HeroMgr.getHeroByType(skillDef.comboPartners);
					HeroData comboPartnersData = (comboPartners.data as HeroData);
//					if(!comboPartnersData.activeSkillIDList.Contains(skillDef.comboPartnersSkillID))
//					{
//						continue;
//					}
				}
				
				SkillIcon skillSlot = skIcons[skIconsIndex].GetComponent<SkillIcon>();
				skillSlot.buildSkill(skillIconDataListTemp[i]);
				skillSlot.ClickHandler = SkillSlotHandler;
				ChangeEventTypeOfSkillIconBtn(skillSlot);
				skIconsIndex++;
			}
			
		}
	}
	
	public void HideSkillIcon()
	{
		for (int i=0; i<skIcons.Length; i++)
		{
			skIcons[i].SetActive(false);
		}
	}
	
	private void ChangeEventTypeOfSkillIconBtn(SkillIcon icon)
	{
		string target = (SkillLib.instance.allHeroSkillHash[icon.skillIconData.id] as SkillDef).target;
		UIButtonMessage message = icon.gameObject.GetComponent<UIButtonMessage>();
		
		if (TargetType.NONE.ToString() == target)
		{
			message.trigger = UIButtonMessage.Trigger.OnClick;
		}
		else
		{
			message.trigger = UIButtonMessage.Trigger.OnPress;
		}
	}
	
	private void SkillSlotHandler(SkillIcon icon)
	{		
		if(!BattleObjects.skHero.isActionStateActive(Character.ActionStateIndex.CallSkillState))
		{
			return;
		}
		string skId = icon.GetComponent<SkillIcon>().skillIconData.id;
		
		skillTarget = (SkillLib.instance.allHeroSkillHash[skId] as SkillDef).target;
	
		// SkillManager.Instance.callSkill(skilldata.skillName + "Pre",new ArrayList(){BattleBg.Instance.gameObject,BattleObjects.skHero.gameObject, BattleObjects.skHero.getTarget()});
		SkillManager.Instance.callSkillPrepare(skId,new ArrayList(){BattleBg.Instance.gameObject,BattleObjects.skHero.gameObject, BattleObjects.skHero.getTarget()});
		BattleObjects.skHero.PushSkillIdToContainer(skId);
		
		if (TargetType.NONE.ToString() == skillTarget ||
			(null != BattleObjects.skHero.getTarget() &&
			!BattleObjects.skHero.getTarget().GetComponent<Character>().getIsDead()	&& 
			skillTarget == BattleObjects.skHero.getTarget().tag.ToUpper()))
		{
			CastSkill(BattleObjects.skHero);
			
		}
	}
	
	public void CastSkill(Hero skHero)
	{
		if(!BattleBg.canUseSkill && !skHero.isActionStateActive(Character.ActionStateIndex.CallSkillState))
		{
			return;
		}
		
		Character e = null;
		
		string skId = skHero.getSkIdCanCastFromContainer();
		string skTarget = (SkillLib.instance.allHeroSkillHash[skId] as SkillDef).target;
		
		if(skHero.getTarget() != null)
		{
			e = skHero.getTarget().GetComponent<Character>();
		}
		
		// if(null == e || !e.getIsDead() || skTarget == TargetType.NONE.ToString()
		if(skTarget == TargetType.NONE.ToString()
			|| null != e && !e.getIsDead() && skTarget == e.tag.ToUpper())
		{
			BattleBg.canUseSkill = false;
			
			SkillIconData skillIconData = skHero.pickASkillDataFromContainer(skId);
			// SkillManager.Instance.callSkill(skilldata.skillName,new ArrayList(){BattleBg.Instance.gameObject,skHero.gameObject,skHero.getTarget()});
//			SkillManager.Instance.callSkill(skillIconData.id,new ArrayList(){BattleBg.Instance.gameObject,skHero.gameObject,skHero.getTarget()});
			SkillManager.Instance.callSkill(skId,new ArrayList(){BattleBg.Instance.gameObject,skHero.gameObject,skHero.getTarget()});
			skillCastCount++;
			CastSkillAchievements();
			skillTarget = string.Empty;
			
//			CallComboSkill(skillIconData.id);
			CallComboSkill(skId);
		}
	}
	
	public void CallComboSkill(string skillID)
	{
		SkillDef cDef = SkillLib.instance.getSkillDefBySkillID(skillID);
		if (!string.IsNullOrEmpty(cDef.comboPartners))
		{			
			Character character = HeroMgr.getHeroByType(cDef.comboPartners);
			if(character == null
				|| !character.isActionStateActive(Character.ActionStateIndex.CallSkillState))
			{
				return;
			}
			
			character.setTarget(BattleObjects.skHero.getTarget());
			SkillDef cDefPt = SkillLib.instance.getSkillDefBySkillID(string.Format("{0}2", cDef.comboPartners));
			
			Character e = null;
			if(character.getTarget() != null)
			{
				e = character.getTarget().GetComponent<Character>();
			}
			
			if (cDefPt.target == TargetType.NONE.ToString() || 
				null != e && !e.getIsDead() && 
				cDefPt.target == e.tag.ToUpper())
			{
				character.pickASkillDataFromContainer(cDefPt.id);
				SkillManager.Instance.callSkill(cDefPt.id,new ArrayList(){BattleBg.Instance.gameObject,character.gameObject,character.getTarget()});
			}else{
				character.PushSkillIdToContainer(cDefPt.id);
			}
		}
	}
	
	public void CastSkillAchievements (){
		if (skillCastCount == 20) {
			AchievementManager.updateAchievement("20SKILL_1BATTLE", 1);
		}
	}
}
