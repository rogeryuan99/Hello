using UnityEngine;
using System.Collections;

public class Skill_GROOT15B : SkillBase {

	private GameObject skillEft_GROOT15B_Light4Prb;
	private GameObject skillEft_GROOT15B_Light4;
	
	protected Vector3 heroOldPosition;
	protected ArrayList gameObjs;
	
	
//	public override void Prepare (ArrayList objs)
//	{
//		base.Prepare (objs);
//		StartCoroutine(creepingVinesShowEftOnBody(objs[1] as GameObject));
//	}
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Groot_Creeping_Vines_1a");
		GRoot heroDoc = caller.GetComponent<GRoot>();
		gameObjs = objs;
		if(Vector3.Distance(caller.transform.position, target.transform.position) > 300.0f)
		{
			heroDoc.PushSkillIdToContainer("GROOT15B");
			heroDoc.moveToTarget(target);
			yield break;
		}
		
		heroOldPosition = caller.transform.position;
		
		heroDoc.SkillKeyFrameEvent += moveToEnemyPosition;
		
		heroDoc.toward(target.transform.position);
		
		heroDoc.castSkill("GROOT15B");// Hero.castSkill
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT15B");
		
		int time = (int)skillDef.skillDurationTime;
	
		Character enemy = target.GetComponent<Character>();
//		enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroyEft);
		State s = new State(time, DestroyEft);
		enemy.addAbnormalState(s, Character.ABNORMAL_NUM.TWINE);
	}

	public void moveToEnemyPosition(Character character)
	{
		GameObject target = gameObjs[2] as GameObject;
		(character as GRoot).SkillKeyFrameEvent -= moveToEnemyPosition;
		iTween.MoveTo
		(	
			character.gameObject,
			new Hashtable()
			{
				{"position",target.transform.position + new Vector3(0, 0, -10)},
				{"speed",1500},
				{"easetype","linear"},
				{ "oncompletetarget",gameObject}
			}
		);
		(character as GRoot).SkillKeyFrameEvent += moveToOldPosition;
	}
	
	public void moveToOldPosition(Character character)
	{
		showEft();
		(character as GRoot).SkillKeyFrameEvent -= moveToOldPosition;
		iTween.MoveTo
		(	
			character.gameObject,
			new Hashtable()
			{
				{"position", this.heroOldPosition},
				{"speed",1500},
				{"easetype","linear"},
				{ "oncompletetarget",gameObject}
			}
		);
	}
	
	
	public void showEft()
	{
		GameObject target = gameObjs[2] as GameObject;
		Character e = target.GetComponent<Character>();
		if(e.getIsDead())
		{
			return;
		}
		if(skillEft_GROOT15B_Light4Prb == null)
		{
			skillEft_GROOT15B_Light4Prb = Resources.Load("eft/Groot/SkillEft_GROOT15B_Light4") as GameObject;
		}
		
//		Destroy(skillEft_GROOT15B_Light4);
		
//		e.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroyEft);
		skillEft_GROOT15B_Light4 = Instantiate(skillEft_GROOT15B_Light4Prb) as GameObject;
		
		skillEft_GROOT15B_Light4.transform.parent = target.transform;
		skillEft_GROOT15B_Light4.transform.localPosition = new Vector3(0, e.model.transform.localPosition.y * e.model.transform.localScale.y, target.transform.position.z - 10);
		skillEft_GROOT15B_Light4.transform.localScale = Vector3.Scale(skillEft_GROOT15B_Light4.transform.localScale, e.model.transform.localScale);
		
	}	

	
	public void DestroyEft(State state, Character charater)
	{	
		Debug.LogError("Skill_GROOT15B DestroyEft");
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroyEft);
		Destroy(skillEft_GROOT15B_Light4);
	}
}
