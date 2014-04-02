using UnityEngine;
using System.Collections;

public class Skill_KORATH15A : SkillBase {
	
	private Object holo1Prefab, holo2Prefab, tiePrefab;
	private GameObject tie;
	private int time;
	protected ArrayList parms;
	public override IEnumerator Cast(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		Character character = caller.GetComponent<Character>();
		character.toward(target.transform.position);
		character.castSkill("Skill15A");
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH15A");
		time = skillDef.skillDurationTime;
		
		yield return new WaitForSeconds(1.1f);
		MusicManager.playEffectMusic("SFX_Korath_Psionic_Pursuit_1a");
		CreateHolo1();
		yield return new WaitForSeconds(.5f);
		CreateHolo2();
		CreateTie();
		TieEnemy();
	}
	
	private void CreateHolo1(){
		GameObject caller = parms[1] as GameObject;
		Character character = caller.GetComponent<Character>();
		
		if (null == holo1Prefab){
			holo1Prefab = Resources.Load("eft/Korath/SkillEft_Korath15A_Holo_a");
		}
		GameObject holo1 = Instantiate(holo1Prefab) as GameObject;
		holo1.transform.position = caller.transform.position + new Vector3(character.model.transform.localScale.x > 0? 230: -230,
																			130f, -5f);
		holo1.transform.localScale = new Vector3((character.model.transform.localScale.x > 0? holo1.transform.localScale.x: -holo1.transform.localScale.x),
													holo1.transform.localScale.y, holo1.transform.localScale.z);
	}
	private void CreateHolo2(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character character = caller.GetComponent<Character>();
		
		if (null == holo2Prefab){
			holo2Prefab = Resources.Load("eft/Korath/SkillEft_Korath15A_Holo_b");
		}
		GameObject holo2 = Instantiate(holo2Prefab) as GameObject;
		holo2.transform.position = target.transform.position + new Vector3(character.model.transform.localScale.x > 0? -100: 100,
																			100f, -5f);
		holo2.transform.localScale = new Vector3(character.model.transform.localScale.x > 0? holo2.transform.localScale.x: -holo2.transform.localScale.x,
													holo2.transform.localScale.y, holo2.transform.localScale.z);
	}
	private void CreateTie(){
		GameObject target = parms[2] as GameObject;
		
		if (null == tiePrefab){
			tiePrefab = Resources.Load("eft/Korath/SkillEft_Korath15A_Tie");
		}
		tie = Instantiate(tiePrefab) as GameObject;
		tie.transform.parent = target.transform;
		tie.transform.localScale = new Vector3(2f, 2f, 2f);
		tie.transform.localPosition = new Vector3(0f, 250f, 0f);
		Invoke("DestroyTie", time);
	}
	private void DestroyTie(){
		Destroy(tie);
	}
	
	private void TieEnemy(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character callerCharacter = caller.GetComponent<Character>();
		Character targetCharacter = target.GetComponent<Character>();
		
		targetCharacter.model.renderer.material.color = (Color)new Color32(128,128,80,255);
		
		ArrayList characterList = null;
		
		if(callerCharacter is Korath)
		{
			characterList = new ArrayList(EnemyMgr.enemyHash.Values);
		}
		else if(callerCharacter is Ch1_Korath)
		{
			characterList = new ArrayList(HeroMgr.heroHash.Values);
		}
		
		foreach(Character character in characterList)
		{
			if (character.getID() != callerCharacter.getID() && 
				character.getID() != targetCharacter.getID())
			{
				character.isAtkSameTag = true;
				if(character.state == Character.CAST_STATE || character.currentActionStates != Character.DefaultActionStates)
				{
					character.targetObj = targetCharacter.gameObject;
				}
				else
				{
					character.moveToTarget(targetCharacter.gameObject);
				}
			}
		}
		
		Invoke("NormalTargetColor", time);
	}
	private void NormalTargetColor(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character callerCharacter = caller.GetComponent<Character>();
		
		Character targetCharacter = target.GetComponent<Character>();
		
		targetCharacter.model.renderer.material.color = (Color)new Color32(128,128,128,255);
		
		ArrayList characterList = null;
		
		if(callerCharacter is Korath)
		{
			characterList = new ArrayList(EnemyMgr.enemyHash.Values);
		}
		else if(callerCharacter is Ch1_Korath)
		{
			characterList = new ArrayList(HeroMgr.heroHash.Values);
		}
		
		foreach(Character character in characterList)
		{
			if (character.getID() != callerCharacter.getID() && 
				character.getID() != targetCharacter.getID())
			{
				character.isAtkSameTag = false;
				character.standby();
				character.targetObj = null;
				
				character.startCheckOpponent();
			}
		}
	}
	
//	protected void addStunBuff(){
//		GameObject caller = objs[1] as GameObject;
//		GameObject target = objs[2] as GameObject;	
//		
//		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("KORATH5A");
//		float stateTime = skillDef.buffDurationTime;
//		Character c = target.GetComponent<Character>();
//		c.stunWithSeconds(stateTime);
//	}
}
