using UnityEngine;
using System.Collections;

public class Skill_GROOT15A : SkillBase {
	
	public GameObject callerEft;
	public GameObject targetEft;
	
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.standby();
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("GROOT15A");
		//heroDoc.playAnim("GROOT15A");

		StartCoroutine(showTargetEft(objs));
		
		yield return new WaitForSeconds(0.2f);
		
		MusicManager.playEffectMusic("SFX_Groot_Floral_Bloom");
	}
	
	public IEnumerator showTargetEft(ArrayList objs)
	{
		yield return new WaitForSeconds( 0.3f);
		
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		Character e = target.GetComponent<Character>();
		
		//e.realDamage((int)(((float)heroDoc.realAtk.PHY * 2.2f)));
		if(callerEft == null)
		{
			callerEft = Resources.Load("gsl_dlg/GROOT15A_1") as GameObject;
		}
		Vector3 pos;
		if(heroDoc.model.transform.localScale.x < 0){
			pos = caller.gameObject.transform.position + new Vector3(-60,70,0);	
		}else{
			pos = caller.gameObject.transform.position + new Vector3(60,70,0);	
		}
		GameObject o = Instantiate(callerEft, pos , caller.gameObject.transform.localRotation) as GameObject;	
		NcCurveAnimation nc = o.transform.GetChild(0).GetComponent<NcCurveAnimation>();
		if(heroDoc.model.transform.localScale.x < 0){
			nc.GetCurveInfo(0).m_fValueScale = -6;
		}else{
			nc.GetCurveInfo(0).m_fValueScale = 6;		
		}
		yield return new WaitForSeconds(1f);
		Destroy(o);
		
		if(targetEft == null)
		{
			targetEft = Resources.Load("gsl_dlg/GROOT15A_2") as GameObject;
		}
		GameObject o1 = Instantiate(targetEft) as GameObject;	
		o1.transform.parent = target.transform;
		o1.transform.localPosition = new Vector3(0,300,-100);
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT15A");
		Hashtable tempNumber = skillDef.buffEffectTable;	//GROOT15A
		float tempTime = skillDef.buffDurationTime;
		float tempHp = ((Effect)tempNumber["hp"]).num;
		e.addBuff("Skill_GROOT15A", (int)tempTime, tempHp/tempTime, BuffTypes.HP);
		
		yield return new WaitForSeconds(tempTime);
		Destroy(o1);
	}
}
