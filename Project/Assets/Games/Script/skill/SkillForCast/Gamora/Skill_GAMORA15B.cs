using UnityEngine;
using System.Collections;

public class Skill_GAMORA15B : SkillBase {
	
	protected ArrayList gameObjects;
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character enemy = target.GetComponent<Character>();
		MusicManager.playEffectMusic("SFX_Gamora_Dashing_Strike_1a");
		gameObjects = objs;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		if(Vector3.Distance(caller.transform.position, target.transform.position) > heroDoc.data.attackRange + 10.0f)
		{
			heroDoc.toward(target.transform.position);
			heroDoc.castSkill("Skill15B_a");
			yield return new WaitForSeconds(.1f);
			heroDoc.addHandlerToParmlessHandlerByParam(
				Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, 
				Skill15BbTrigger
			);
			heroDoc.moveToTargetDirectly(target);
			yield break;
		}
		State s = new State(2, null);
		enemy.addAbnormalState(s, Character.ABNORMAL_NUM.STUN);
		heroDoc.toward(target.transform.position);
		StartCoroutine(StartSkill15B_b());
	}
	
	public void Skill15BbTrigger(Character c){
		StartCoroutine(StartSkill15B_b());
	}
	
	public IEnumerator StartSkill15B_b()
	{
		GameObject scene = gameObjects[0] as GameObject;
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		gamora.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, Skill15BbTrigger);
		gamora.castSkill("Skill15B_b");
		gamora.damageCallback = DamageEnemy;
		gamora.slowdownCallback = SlowDown;
		
		yield return new WaitForSeconds(0.0f);
	}
	
	private int count = 0;
	public void DamageEnemy(){
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		Character e = target.GetComponent<Character>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA15B");
		
		Hashtable tempNumber = skillDef.activeEffectTable; // GAMORA5A
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		int tempAtk = e.getSkillDamageValue(gamora.realAtk, tempAtkPer);
		
		
		e.realDamage(tempAtk + (count++%3 == 2? tempAtk: 0));
		MusicManager.playEffectMusic("Skill_GAMORA5A");
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.3f, 0f));
	}
	
	public void SlowDown(){
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.15f));
	}
}
