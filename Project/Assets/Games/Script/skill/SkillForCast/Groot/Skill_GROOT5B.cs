using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_GROOT5B : SkillBase
{
	
	protected GameObject stonePrb;
	protected GameObject stone;
	
	protected GameObject branchBehindPrb;
	
	protected GameObject branchFrontPrb;
	
	protected Hashtable branchHash = new Hashtable();
		
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Groot_Root_1a");
		GRoot heroDoc = caller.GetComponent<GRoot>();
		
		heroDoc.castSkill("GROOT5B");
		
		yield return new WaitForSeconds(0.8f);
		
		if(stonePrb == null)
		{
			stonePrb = Resources.Load("eft/Groot/SkillEft_GROOT5BStone") as GameObject;
		}
		stone = Instantiate(stonePrb) as GameObject;
		
		float x = caller.transform.position.x;
		
		if(heroDoc.model.transform.localScale.x < 0)
		{
			x -= 150; 
		}
		else
		{
			x += 150; 
		}
		stone.transform.position = new Vector3(x, caller.transform.position.y, caller.transform.position.z);
		
		yield return new WaitForSeconds(0.8f);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT5B");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		int time = skillDef.buffDurationTime;
		float mspdValue = ((Effect)skillDef.buffEffectTable["mspd"]).num;
		
		if(branchBehindPrb == null)
		{
			branchBehindPrb = Resources.Load("eft/Groot/SkillEft_GROOT5B_BranchBehind") as GameObject;
		}
		
		if(branchFrontPrb == null)
		{
			branchFrontPrb = Resources.Load("eft/Groot/SkillEft_GROOT5B_BranchFront") as GameObject;
		}
		
		foreach(Enemy e in EnemyMgr.enemyHash.Values)
		{
			if(e.getIsDead())
			{
				continue;
			}
			e.addBuff("Skill_GROOT5B", time, -(mspdValue / 100.0f), BuffTypes.MSPD);
			showEft(e);
			e.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DesSkillEft);
		}
		
		Invoke ("DesAllSkillEft", time);
	}
	
	public void showEft(Enemy enemy)
	{
		
		GameObject branchFront = Instantiate(branchFrontPrb) as GameObject;
		GameObject branchBehind = Instantiate(branchBehindPrb) as GameObject;
		
		float s = 1.5f;
		
		Vector3 scale = new Vector3(s,s,s);
		
		branchFront.transform.parent = enemy.transform;
		branchFront.transform.localPosition = new Vector3(0, enemy.model.transform.localPosition.y, -1);
		branchFront.transform.localScale = scale;
		
		
		branchBehind.transform.parent = enemy.transform;
		branchBehind.transform.localPosition = new Vector3(0, enemy.model.transform.localPosition.y, 0);;
		branchBehind.transform.localScale = scale;
		
		List<PackedSprite> branchList = new  List<PackedSprite>();
		branchList.Add(branchFront.GetComponent<PackedSprite>());
		branchList.Add(branchBehind.GetComponent<PackedSprite>());
		this.branchHash[enemy.getID()] = branchList;
	}
	
	public void DesSkillEft(Character character)
	{
		if(this.branchHash.Count <= 0)
		{
			return;
		}
		
		List<PackedSprite> branchList = this.branchHash[character.getID()] as List<PackedSprite>;
		
		if(branchList == null || branchList.Count <= 0)
		{
			return;
		}
		
		foreach(PackedSprite branch in branchList)
		{
				branch.animations[branch.defaultAnim].onAnimEnd = UVAnimation.ANIM_END_ACTION.Destroy;
				branch.PlayAnimInReverse(0);
		}
		
		this.branchHash.Remove(character.getID());
	}
	
	public void DesAllSkillEft()
	{
		foreach(List<PackedSprite> branchList in branchHash.Values)
		{
			foreach(PackedSprite branch in branchList)
			{
				branch.animations[branch.defaultAnim].onAnimEnd = UVAnimation.ANIM_END_ACTION.Destroy;
				branch.PlayAnimInReverse(0);
			}
			branchList.Clear();
		}
		branchHash.Clear();
	}
}
