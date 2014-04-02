using UnityEngine;
using System.Collections;

public class Skill_LEVAN1 : SkillBase {

	private Object effectPrefab;
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		Character levan  = caller.GetComponent<Character>();
		if(Vector3.Distance(caller.transform.position, target.transform.position) > levan.data.attackRange + 10.0f)
		{
			levan.moveToTarget(target);
			BattleObjects.skHero.PushSkillIdToContainer("LEVAN1");// DRAX1
			yield break;
		}
		levan.toward(target.transform.position);
		levan.castSkill("SkillA");
		MusicManager.playEffectMusic("SFX_Levan_Skullduggery_1a");
		yield return new WaitForSeconds(.85f);
		
		CreateHitEffect();
		FlashTarget();
		Hit();
	}
	
	private void CreateHitEffect(){
		GameObject caller = parms[1] as GameObject;
		Character levan  = caller.GetComponent<Character>();
		
		if (null == effectPrefab){
			effectPrefab = Resources.Load("eft/Levan/SkillEft_Levan1_HitEffect");
		}
		GameObject effect = Instantiate(effectPrefab) as GameObject;
		effect.transform.parent = GameObject.Find(string.Format("{0}/SMALL_Weapon_01", (levan as Levan?"Levan":"enemyCh2LevanBase"))).transform;
		effect.transform.position = effect.transform.parent.position;
		effect.transform.position += (levan.model.transform.localScale.x > 0)? new Vector3(60f, 8f, 0f): new Vector3(-60f, 8f, 0f);
		
		FlashTarget();
		Invoke("FlashTarget", .1f);
		Invoke("FlashTarget", .2f);
		Invoke("FlashTarget", .3f);
		Invoke("FlashTarget", .4f);
	}
	
	private void FlashTarget(){
		GameObject target = parms[2] as GameObject;
		Character character  = target.GetComponent<Character>();
		// character.changeStateColor(new Color(.5f, .5f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);
		character.changeStateColor(new Color(1f, 1f, 1f, 1f), new Color(.5f, .5f, .5f, 1f), .05f);
	}
	
	private IEnumerator Hit(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character character  = target.GetComponent<Character>();
		
		SkillDef skillDef =  SkillLib.instance.getSkillDefBySkillID("LEVAN1");
		Hashtable tempNumber = skillDef.buffEffectTable;
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		
		character.realDamage(0);
		
		yield return new WaitForSeconds(.5f);
		character.addBuff("LEVAN1", skillDef.buffDurationTime, tempAtkPer, BuffTypes.DE_ATK_PHY, (ch, buf)=>{});
		character.changeStateColor(new Color(.5f,.5f,1f,1f), new Color(.5f,.5f,.5f,1f), skillDef.buffDurationTime);
	}
}
