using UnityEngine;
using System.Collections;

public abstract class SkillBase : MonoBehaviour {
	
	GameObject skinitPrb;
	public virtual void Prepare(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		StartCoroutine(autoSkillActForTarget(scene, caller));
	}
	
	public abstract IEnumerator Cast(ArrayList objs);
	
	protected IEnumerator autoSkillActForTarget (GameObject scene, GameObject caller){
		
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0, 50, 0), 0.3f, 0.0f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.3f));
		
		Vector3 pos = caller.transform.position + new Vector3(0,140,-5); 
	
		if(skinitPrb == null)
		{
			skinitPrb = Resources.Load("eft/eft") as GameObject;
		}
		GameObject skinit = Instantiate(skinitPrb, pos, caller.transform.rotation) as GameObject;
		skinit.transform.parent = caller.transform;
		
		//MusicManager.playEffectMusic("skill_release");
		yield return new WaitForSeconds(0.5f);
		BattleBg.canUseSkill = true;
	}
}
