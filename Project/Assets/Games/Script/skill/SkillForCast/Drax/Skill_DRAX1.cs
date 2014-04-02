using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_DRAX1 : SkillBase {
	
	private ArrayList objs;
	private bool isUpdateMoving = false;
	private Drax drax = null;
	private float v = 0f;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		drax = caller.GetComponent<Drax>();
		if(Vector3.Distance(caller.transform.position, target.transform.position) > drax.data.attackRange + 10.0f)
		{
			drax.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("DRAX1");// DRAX1
			yield break;
		}
		this.objs = objs;
		drax.toward(target.transform.position);
		drax.damageCallback = DamageEnemy;
		drax.movingUpdateCallback = SwitchMoving;
		drax.AnimationFinishedCallback = (animName) => { 
			bool isFinished = false;
			if (animName != "SkillA_a" && animName != "SkillA_b"){
				isUpdateMoving = false; 
				isFinished = true;
			}
			return isFinished;
		};
		v = caller.transform.position.x <= target.transform.position.x? 1000: -1000f;
		drax.castSkill("SkillA_a");
		yield return new WaitForSeconds(.33f);
		MusicManager.playEffectMusic("SFX_Drax_DualKnifeStrike_1a");
	}
	
	public void DamageEnemy(){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		// drax = caller.GetComponent<Drax>();
		// drax.skillFinishedCallback = FinishedSkill;
		drax.castSkill("SkillA_b");
		v = caller.transform.position.x <= target.transform.position.x? -200: 200f;
		
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("DRAX1").activeEffectTable; // DRAX1
		
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		
		Character e = target.GetComponent<Character>();
		
		int tempAtk = e.getSkillDamageValue(drax.realAtk, tempAtkPer);
		
		e.realDamage(tempAtk);
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.6f, 0f));
	}
	
	public void SwitchMoving()
	{
		isUpdateMoving = !isUpdateMoving;
	}
	
	public void Update()
	{
		if (isUpdateMoving)
		{
			GameObject caller = objs[1] as GameObject;
			GameObject target = objs[2] as GameObject;
			Vector3 tp = drax.transform.position + new Vector3(v * Time.deltaTime, 0f);
			if (false == BattleBg.IsOutOfActionBounce(tp))
				drax.setPosition(tp);
		}
		if (null == drax || drax.getIsDead() || StaticData.isBattleEnd)
		{
			isUpdateMoving = false;
		}
	}
}