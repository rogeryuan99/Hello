using UnityEngine;
using System.Collections;

public class Skill_LEVAN5A : SkillBase {

	private Object effectPrefab;
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		Character levan  = caller.GetComponent<Character>();
		levan.toward(target.transform.position);
		levan.castSkill("Skill5A");
		MusicManager.playEffectMusic("SFX_Levan_Blaster_Pistol_1a");
		yield return new WaitForSeconds(1.2f);
		
		CreateHitEffect();
		Hit();
	}
	
	private void CreateHitEffect(){
		GameObject target = parms[2] as GameObject;
		
		if (null == effectPrefab){
			effectPrefab = Resources.Load("eft/Levan/SkillEft_Levan5A_HitEffect");
		}
		Vector3 hitPos = new Vector3(Random.Range(-40f, 40f),
										Random.Range(50f, 100f),
										-1f);
		float scale = Random.Range(.5f, 1f);
		GameObject hitEft = Instantiate(effectPrefab, hitPos + target.transform.position, transform.rotation) as GameObject;
		hitEft.transform.localScale *= scale;
	}
	
	private void Hit(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character levan = caller.GetComponent<Character>();
		Character character = target.GetComponent<Character>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("LEVAN5A");
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		float per = ((Effect)tempNumber["atk_PHY"]).num;
		int damage = character.getSkillDamageValue(levan.realAtk, per);
		character.realDamage(damage);
	}
}
