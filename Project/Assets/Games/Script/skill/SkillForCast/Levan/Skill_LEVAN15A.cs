using UnityEngine;
using System.Collections;

public class Skill_LEVAN15A : SkillBase {
	protected GameObject caller;
	protected GameObject topEft;
	protected GameObject bottomEft;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.caller = caller;
		
		Character character = caller.GetComponent<Character>();
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN15A");
		int skillDurationTime = skillDef.buffDurationTime;
		
		character.castSkill("Skill15A_a");
//		character.toward(target.transform.position);
		character.attackAnimaName = "Skill15A_b";
		if(character is Levan)
		{
			Levan levan = character as Levan;
			
			levan.showSkill15EftCallback += showEffect;
			levan.SkillKeyFrameEvent += addSkillBuff;
		}
		else
		{
			Ch2_Levan levan = character as Ch2_Levan;
			levan.showSkill15EftCallback += showEffect;
			levan.SkillKeyFrameEvent += addSkillBuff;	
		}
		
		character.addBuff("Skill_LEVAN15A", skillDurationTime , 0, BuffTypes.ATK_PHY, cancelSkillBuff);
		
		yield return new WaitForSeconds(0f);
		
		MusicManager.playEffectMusic("SFX_Levan_Gunfighter_1a");
	}
	
	protected void showEffect()
	{
		Character character = caller.GetComponent<Character>();
		
		if(character is Levan)
		{
			Levan levan = character as Levan;
			levan.showSkill15EftCallback -= showEffect;	
		}
		else
		{
			Ch2_Levan levan = character as Ch2_Levan;
			levan.showSkill15EftCallback -= showEffect;	
		}
		
		GameObject topPrefab = Resources.Load("eft/Levan/SkillEft_Levan15A_FireEffect_Top") as GameObject;
		topEft = Instantiate(topPrefab) as GameObject;
		topEft.transform.parent = caller.transform;
		topEft.transform.localPosition = new Vector3(0,0,40f);
		topEft.transform.localScale = new Vector3(3f,3f,1f);
		
		GameObject bottomPrefab = Resources.Load("eft/Levan/SkillEft_Levan15A_FireEffect_Bottom") as GameObject;
		bottomEft = Instantiate(bottomPrefab) as GameObject;
		bottomEft.transform.parent = caller.transform;
		bottomEft.transform.localPosition = new Vector3(0,1f,0);
		bottomEft.transform.localScale = new Vector3(3f,3f,1f);
	}
	
	protected void addSkillBuff(Character c)
	{
		
	}
	
	protected void cancelSkillBuff(Character character, Buff self){
		Destroy(topEft);
		Destroy(bottomEft);
		if(character is Levan){
			Levan levan = character as Levan;
			levan.attackAnimaName = "Attack";
			levan.SkillKeyFrameEvent -= addSkillBuff;
		}
		else
		{
			Ch2_Levan levan = character as Ch2_Levan;
			levan.attackAnimaName = "Attack";
			levan.SkillKeyFrameEvent -= addSkillBuff;
		}
	}
}
