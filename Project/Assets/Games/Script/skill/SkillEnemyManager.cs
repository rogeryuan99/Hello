using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillEnemyManager : Singleton<SkillEnemyManager>
{
	public List<SkillIconData> skillIconDataList = new List<SkillIconData>();

	public void destroyAllSkillIconDataList()
	{
		List<SkillIconData> deleteSkillIconData = new List<SkillIconData>();
		foreach(SkillIconData skillIconData in this.skillIconDataList)
		{
			deleteSkillIconData.Add(skillIconData);
			Destroy(skillIconData.gameObject);
		}
		
		foreach(SkillIconData skillIconData in deleteSkillIconData)
		{
			this.skillIconDataList.Remove(skillIconData);
		}
	}
	
	public void createSkillIcon(string id)
	{
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash[id] as SkillDef;
		SkillIconData sid = SkillIconData.create(skillDef.id, "Enemy_" + skillDef.funcName, skillDef.coolDown);
		this.skillIconDataList.Add(sid);
	}
	
	public SkillIconData getSkillIconData(string id)
	{
		foreach(SkillIconData skillIconData in this.skillIconDataList)
		{
			if(skillIconData.id == id)
			{
				return skillIconData;
			}
		}
		Debug.LogError(string.Format("SkillEnemyManager getSkillIconData '{0}' is NULL", id));
		return null;
	}
	
	public void callSkill (string id, ArrayList objs)
	{
		
		GameObject skillObj = GetSkillObject(id);
		
		
		SkillBase skill = GetSkillBase(skillObj, id);
//		skill.Prepare(objs);
		
		if(skill != null)
		{
			StartCoroutine(skill.Cast(objs));
		}
	}
	
	private GameObject GetSkillObject(string id)
	{
		GameObject skillObj = GameObject.Find("Enemy_" + id);
		
		if (null == skillObj)
		{
			skillObj = new GameObject("Enemy_" + id);
		}
		
		return skillObj;
	}
	
	private SkillBase GetSkillBase(GameObject obj, string id)
	{
		string name = string.Format("Skill_{0}", id);
		
		SkillBase skill = obj.GetComponent(name) as SkillBase;
		if (null == skill)
		{
			skill = obj.AddComponent(name) as SkillBase;
			if (null == skill)
			{
				//throw new MissingComponentException();
				
			}
		}
		
		return skill;
	}
	
	public void CastSkill(Enemy enemy)
	{
		if(!enemy.isActionStateActive(Character.ActionStateIndex.CallSkillState))
		{
			return;	
		}
		
		Character e = null;
		
		string skId = enemy.getSkIdCanCastFromContainer();
		string skTarget = (SkillLib.instance.allHeroSkillHash[skId] as SkillDef).target;
		
		if(skTarget.ToUpper() == SkillIconManager.TargetType.ENEMY.ToString().ToUpper())
		{
			skTarget = SkillIconManager.TargetType.PLAYER.ToString().ToUpper();
		}
		
		if(enemy.getTarget() != null)
		{
			e = enemy.getTarget().GetComponent<Character>();
		}
		
		// if(null == e || !e.getIsDead() || skTarget == TargetType.NONE.ToString()
		if(skTarget == SkillIconManager.TargetType.NONE.ToString() ||
			null != e && 
			!e.getIsDead() && 
			skTarget.ToUpper() == e.tag.ToUpper()
			)
		{
			
			SkillIconData skillIconData = enemy.pickASkillDataFromContainer(skId);
			skillIconData.skillCast(null,null);
			
			callSkill(skillIconData.id,new ArrayList(){BattleBg.Instance.gameObject, enemy.gameObject, enemy.getTarget()});
		}
	}
}
