using UnityEngine;
using System.Collections;

public class Skill_MANTIS5A : SkillBase {
	
	private Object bulletPrefab;
	private Object effectPrefab;
	
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[1] as GameObject;
		parms = objs;
		
		Mantis mantis = caller.GetComponent<Mantis>();
		mantis.toward(target.transform.position);
		mantis.castSkill("Skill5A");
		
		yield return new WaitForSeconds(.2f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Psychic_Shield_1a");
		
		yield return new WaitForSeconds(.5f);
		
		CreateBullet();
		AddBuf();
	}
	
	private void CreateBullet(){
		GameObject caller     = parms[1] as GameObject;
		GameObject target     = parms[2] as GameObject;
		
		Mantis mantis    = caller.GetComponent<Mantis>();
		Vector3 endPos   = target.transform.position + new Vector3(0,70,0);
		Vector3 startPos = (mantis.model.transform.localScale.x > 0)
							? caller.transform.position + new Vector3(80,80,-50)
							: caller.transform.position + new Vector3(-80,80,-50);
				
		if (null == bulletPrefab){
			bulletPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS5A_Bullet");
		}
		GameObject bullet = Instantiate(bulletPrefab, startPos, transform.rotation) as GameObject;
		iTween.MoveTo(bullet,new Hashtable(){{"x",endPos.x},{ "y",endPos.y},{ "speed",2000},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bullet}});
	}
	
	private void removeBullet (GameObject bullet)
	{
		Destroy(bullet);
		StartCoroutine(ShowEffect());
	}
	
	private IEnumerator ShowEffect(){
		GameObject target   = parms[2] as GameObject;
		Character character = target.GetComponent<Character>();
		
		if (null == effectPrefab){
			effectPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS5A_Effect");
		}
		GameObject effect = Instantiate(effectPrefab, target.transform.position, transform.rotation) as GameObject;
		effect.transform.parent = target.transform;
		
		SkillDef   skillDef   = SkillLib.instance.getSkillDefBySkillID("MANTIS5A");
		int        time       = skillDef.buffDurationTime;
		
		yield return new WaitForSeconds(time);
		Destroy(effect);
	}
	
	
	
	
	private void AddBuf(){
		GameObject caller     = parms[1] as GameObject;
		GameObject target     = parms[2] as GameObject;
		Mantis     mantis     = caller.GetComponent<Mantis>();
		Character  targetCharacter = target.GetComponent<Character>();
		SkillDef   skillDef   = SkillLib.instance.getSkillDefBySkillID("MANTIS5A");
		int        time       = skillDef.buffDurationTime;
		Hashtable  tempNumber = skillDef.buffEffectTable;
		
		float def = ((Effect)tempNumber["def_PHY"]).num;
		targetCharacter.addBuff("MANTIS5A", time, def, BuffTypes.DEF_PHY, (ch, buf)=>{});
	}
}
