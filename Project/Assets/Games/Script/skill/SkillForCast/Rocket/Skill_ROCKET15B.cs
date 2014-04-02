using UnityEngine;
using System.Collections;

public class Skill_ROCKET15B : SkillBase 
{
	protected GameObject jarEftPrb;
	
	protected ArrayList objs;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		MusicManager.playEffectMusic("SFX_Rocket_Battle_Tactics_1a");
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.addHerosBuffCallback += addbuff;
		rocket.castSkill("Skill15B");
		yield return new WaitForSeconds(.55f);
		
		StartCoroutine(showJarEft(caller));
	}
	
	public IEnumerator showJarEft(GameObject caller)
	{
		yield return new WaitForSeconds(.55f);
		if(jarEftPrb == null)
		{
			jarEftPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET15A_Jar") as GameObject;
		}
		
		GameObject jarEft = Instantiate(jarEftPrb) as GameObject;
		
		jarEft.transform.position = new Vector3(caller.transform.position.x, caller.transform.position.y + 272 , 0);
		
	}
	
	public void addbuff()
	{
		GameObject caller = objs[1] as GameObject;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.addHerosBuffCallback -= addbuff;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET15B");
		
		int time = (int)skillDef.buffDurationTime;
		
		float atkPer = ((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		
		foreach(Hero hero in HeroMgr.heroHash.Values)
		{
			int tempAct = (int)(hero.realAtk.PHY * (atkPer / 100.0f + 1.0f));
			hero.currentColor =  (Color)new Color32(255,230,80,255);
			hero.model.renderer.material.color = hero.currentColor;
			hero.addBuff("ROCKET15B" + "_" + hero.data.type, time, tempAct, BuffTypes.ATK_PHY, buffFinish);
	
		}
	}
	
	public void buffFinish(Character character, Buff self)
	{
		character.currentColor = Character.normalColor;
		character.model.renderer.material.color = character.currentColor;
	}
}
