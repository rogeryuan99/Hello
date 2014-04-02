using UnityEngine;
using System.Collections;

public class Skill_CAIERA5A : SkillBase {
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		
		Character character = caller.GetComponent<Character>();
		
		character.castSkill("Skill5A");
		
		MusicManager.playEffectMusic("SFX_Caiera_Shinbreaker_1a");
		
		if(character is Caiera)
		{
			Caiera caiera = character as Caiera;
			caiera.showSkill5AEftCallback += addLayDownEft;
		}
		else if(character is Ch3_Caiera)
		{
			Ch3_Caiera caiera = character as Ch3_Caiera;
			caiera.showSkill5AEftCallback += addLayDownEft;
		}
		
		yield return new WaitForSeconds(0f);
	}
	GameObject attackEftPrefab ;
	protected void addLayDownEft(Character c)
	{
		Hashtable characterTable = null;
		if(c is Caiera)
		{
			Caiera caiera = c as Caiera;
			caiera.showSkill5AEftCallback -= addLayDownEft;
			characterTable = EnemyMgr.enemyHash;
		}
		else if(c is Ch3_Caiera)
		{
			Ch3_Caiera caiera = c as Ch3_Caiera;
			caiera.showSkill5AEftCallback -= addLayDownEft;
			characterTable = HeroMgr.heroHash;
		}
//		if(attackEftPrefab == null)
//		{
//		attackEftPrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA5A_WeaponHolo") as GameObject;
//		}
//		GameObject attackEft = Instantiate(attackEftPrefab) as GameObject;
//		attackEft.transform.parent = c.transform;
//		attackEft.transform.localPosition = new Vector3(0,150,0);
//		attackEft.transform.localScale = new Vector3(3f,3f,1f);
//		
//		attackEft.transform.localScale = new Vector3(c.model.transform.localScale.x > 0? attackEft.transform.localScale.x: -attackEft.transform.localScale.x,
//											attackEft.transform.localScale.y, attackEft.transform.localScale.z);
//		attackEft.transform.position = c.transform.position + new Vector3(0f,50f,0f);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("CAIERA5A");
		int aoeRadius= (int)skillDef.activeEffectTable["AOERadius"];
		int skillDurationTime = skillDef.buffDurationTime;
		
		foreach(Character otherCharacter in characterTable.Values)
		{
			Vector2 vc2 = c.transform.position - otherCharacter.transform.position;
			if(StaticData.isInOval(aoeRadius,aoeRadius,vc2))
			{
				if(!otherCharacter.isDead)
				{
					State s = new State(skillDurationTime, null);
					otherCharacter.addAbnormalState(s, Character.ABNORMAL_NUM.LAYDOWN);
					
					GameObject hitEftPrefab = Resources.Load("eft/Caiera/Skill_CAIERA5A_HitEft") as GameObject;
					GameObject hitEft = Instantiate(hitEftPrefab) as GameObject;
					hitEft.transform.parent = otherCharacter.transform;
					hitEft.transform.localPosition = new Vector3(0,100,0);
				}
			}
		}
	}
}
