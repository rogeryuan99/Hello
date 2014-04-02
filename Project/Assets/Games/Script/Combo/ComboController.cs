using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboController : Singleton<ComboController> {

	public List<ComboIcon> icons = new List<ComboIcon>();
	public List<SkillIconData> comboIconDataList = new List<SkillIconData>();
	
	public void Start()
	{
		for (int i=0; i<icons.Count; i++){
			icons[i].Hide();
		}
	}
	
	public SkillIconData getComboIconData(string skillID)
	{
		foreach(SkillIconData skillIconData in this.comboIconDataList)
		{
			if(skillIconData.id == skillID)
			{
				return skillIconData;
			}
		}
		return null;
	}
	
	public List<SkillIconData> getComboIconDataListByHeroType(string heroType)
	{
		List<SkillIconData> comboIconDataListTemp = new List<SkillIconData>();
		foreach(SkillIconData skillIconData in this.comboIconDataList)
		{
			if(skillIconData.id.Contains(heroType))
			{
				comboIconDataListTemp.Add(skillIconData);
			}
		}
		return comboIconDataListTemp;
	}
	
	public void createComboIconDataSkillByHeroData(HeroData heroData)
	{
		this.destroyComboIconDataList(heroData.type);
		
		foreach(string activeSkillID in heroData.activeSkillIDList)
		{
			SkillDef skillDef = SkillLib.instance.allHeroSkillHash[activeSkillID] as SkillDef;
			if(skillDef.comboPartners != "")
			{
				SkillIconData skillIconData = SkillIconData.create(skillDef.id, skillDef.funcName,skillDef.coolDown);
				this.comboIconDataList.Add(skillIconData);
			}
		}
	}
	
	public void createAllComboIconDataSkill()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			createComboIconDataSkillByHeroData((hero.data as HeroData));
		}
		
		this.comboIconDataList.Clear();
	}
	
	public void destroyAllComboIconDataList()
	{
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			this.destroyComboIconDataList((hero.data as HeroData).type);
		}
	}
	
	public void destroyComboIconDataList(string heroType)
	{
		List<SkillIconData> deleteComboIconData = new List<SkillIconData>();
		foreach(SkillIconData skillIconData in this.comboIconDataList)
		{
			if(skillIconData.id.Contains(heroType))
			{
				deleteComboIconData.Add(skillIconData);
				Destroy(skillIconData.gameObject);
			}
		}
		
		foreach(SkillIconData skillIconData in deleteComboIconData)
		{
			this.comboIconDataList.Remove(skillIconData);
		}
	}
	
	public void ShowComboEffect(){
		GameObject superComboPref = Resources.Load("SuperCombo/SuperCombo") as GameObject;
		GameObject superCombo = Instantiate(superComboPref) as GameObject;
		GameObject uiroot = GameObject.Find("UIRoot");
		
		superCombo.transform.parent = uiroot.transform;
		superCombo.transform.localScale = new Vector3(0.001f,0.001f,1f);
		superCombo.transform.localPosition = new Vector3(0,206.1f,-10f);
		
		SkillManager.Instance.chanageBGTextureColorByCoroutine(0.0f, 0.3f);
		SkillManager.Instance.pauseMotionByCoroutine(0.0f, 0.3f);
		
		iTween.ScaleTo(superCombo,new Hashtable(){{"x",501f},{"y",186f},{"delay",0.01f},{"time",.2f},{"easetype",iTween.EaseType.easeOutBounce},{"oncomplete","destroySelf"},{"oncompletetarget",superCombo}});
		MusicManager.playEffectMusic("SFX_Drax_Basic_1a");
	}
	
	public void ChangeComboDisplay()
	{
		
		if (!UserInfo.instance.getIsUnLockCombo()) 
			return;
		
		int iconIndex = 0;
		HeroData data = (Hero.Selected.data as HeroData);
		
		List<SkillIconData> comboIconDataListTemp = this.getComboIconDataListByHeroType(data.type);

		foreach(SkillIconData comboIconData in comboIconDataListTemp)
		{
				
			SkillDef skillDef = SkillLib.instance.allHeroSkillHash[comboIconData.id] as SkillDef;
			
			
			Hero partners = HeroMgr.getHeroByType(skillDef.comboPartners);
			
			if (null != partners && false == partners.isDead)
			{
				
				SkillDef partnersSkillDef = SkillLib.instance.allHeroSkillHash[skillDef.comboPartnersSkillID] as SkillDef;
				
				SkillIconData selfSkillIconData = this.getComboIconData(skillDef.id);
				SkillIconData partnersSkillIconData = this.getComboIconData(partnersSkillDef.id);
				
				icons[iconIndex].skillIconDataList.Clear();
				icons[iconIndex].skillIconDataList.Add(selfSkillIconData);
				icons[iconIndex].skillIconDataList.Add(partnersSkillIconData);
				
				icons[iconIndex].Show(data.type, partners.data.type);
				icons[iconIndex].OnClickCallback = CastCombo;
				iconIndex++;
			}
		}
		if (0 == iconIndex){
			for (int i=0; i<icons.Count; i++){
				icons[i].Hide();
			}
		}
	}
	
	private bool CastCombo(string[] hTypes, List<SkillIconData> skillIconDataList){
		if (null == BattleObjects.skHero || IsNoneTarget(hTypes))
		{
			return false;
		}
		
		bool result = false;
		List<Hero> combo = GetComboHeros(hTypes);
		
		if (2 == combo.Count){
			ShowComboEffect();
			
			for (int i=0; i<combo.Count; i++){
				GameObject target = BattleObjects.skHero.getTarget();
				if (null == target || target.GetComponent<Enemy>().isDead){
					target = BattleObjects.skHero != combo[i]? 
								combo[i].getTarget(): combo[(i+1)%combo.Count].getTarget();
				}
				combo[i].setTarget(target);
				SkillManager.Instance.callSkill(getSkillId(skillIconDataList, combo[i].data.type),new ArrayList(){BattleBg.Instance.gameObject,combo[i].gameObject,combo[i].getTarget()});
				//combo[i].startGroupAtk();
			}
			result = true;
		}
		
		return result;
	}
	
	protected string getSkillId(List<SkillIconData> skillIconDataList, string heroType)
	{
		foreach(SkillIconData sd in skillIconDataList)
		{
			if(sd.id.Contains(heroType))
			{
				return sd.id;
			}
		}
		return "";
	}
	
	private List<Hero> GetComboHeros(string[] hTypes){
		List<Hero> combo = new List<Hero>();
		
		for (int i=0; i<hTypes.Length; i++){
			Hero hero = HeroMgr.getHeroByType(hTypes[i]);
			if (null != hero && false == hero.isDead){
				combo.Add(hero);
			}
		}
		
		return combo;
	}
	
	private bool IsNoneTarget(string[] hTypes){
		bool isNoneTarget = true;
		
		for (int i=0; i<hTypes.Length; i++){
			Hero hero = HeroMgr.getHeroByType(hTypes[i]);
			if (null != hero.getTarget()
				&& false == hero.getTarget().GetComponent<Enemy>().isDead){
				isNoneTarget = false;
			}
		}
		
		return isNoneTarget;
	}
}
