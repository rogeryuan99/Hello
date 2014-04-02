using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_DRAX5B : SkillBase
{
	public GameObject skillEftPrb;
	
	public List<GameObject> skillBuffEftList = new List<GameObject>();
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Drax_Slow_Bleed_1a");
		Drax drax = caller.GetComponent<Drax>();
		drax.castSkill("Skill5B");
		drax.attackAnimaEvent += Skill_DRAX5BEffect;
		
		if(skillEftPrb == null)
		{
			skillEftPrb = Resources.Load("eft/Drax/SkillDrax5B_BuffEft") as GameObject;
		}
				
		PackedSprite[] weaponPackedSprites = drax.model.GetComponentsInChildren<PackedSprite>();
		
		for(int i = 0; i < weaponPackedSprites.Length; i++)
		{
			GameObject weaponObj = weaponPackedSprites[i].gameObject;
			if(weaponObj.name.Contains("MEDIUM_Weapon_01"))
			{
				GameObject buffEft = Instantiate(skillEftPrb) as GameObject;
				
				SkillEftShowByDrawLayer se = buffEft.GetComponent<SkillEftShowByDrawLayer>();
				se.flag = weaponObj.GetComponent<PackedSprite>();
				
				
				buffEft.transform.parent = weaponObj.transform;
				buffEft.transform.localPosition = Vector3.zero;
//				buffEft.transform.localScale = new Vector3(4,4,1);
				skillBuffEftList.Add(buffEft);
			}
		}
		
		yield return new WaitForSeconds(0.5f);
	}
	
	public void Skill_DRAX5BEffect(Drax drax, Character target)
	{
		if(target.getIsDead())
		{
			return;
		}
		drax.attackAnimaEvent -= Skill_DRAX5BEffect;
		
		Debug.LogError("Skill_DRAX5BEffect");
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX5B");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		int time = skillDef.buffDurationTime;
		float per = ((Effect)skillDef.buffEffectTable["hp"]).num;
		int hp = (int)((per / 100.0f + 1.0f) * drax.realMaxHp);
		target.addBuff("Skill_DRAX5B", time, (float)hp/(float)time, BuffTypes.DE_HP);
		
		DestroyBuffEft(drax, "Skill_DRAX5B");
		
	}
	
	public void DestroyBuffEft(Character character, string buffName)
	{
		if(buffName == "Skill_DRAX5B")
		{		
			(character as Drax).attackAnimaEvent -= Skill_DRAX5BEffect;
			
			foreach(GameObject buffEft in skillBuffEftList)
			{
				Destroy(buffEft);
			}
		}
	}
}
