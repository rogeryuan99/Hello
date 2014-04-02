using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_DRAX15A : SkillBase 
{	
	public GameObject skillEftPrb;
	
	public List<GameObject> skillBuffEftList = new List<GameObject>();
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Drax drax = caller.GetComponent<Drax>();
		drax.castSkill("Skill15A");
		drax.attackAnimaEvent += Skill_DRAX15AEffect;
				
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX15A");
		
		int time = (int)skillDef.skillDurationTime;
		
		Debug.LogError(time);
		
		drax.addBuff("Skill_DRAX15A", time, 0, BuffTypes.ATK_PHY);
		
		
		if(skillEftPrb == null)
		{
			skillEftPrb = Resources.Load("eft/Drax/SkillDrax15A_BuffEft") as GameObject;
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
	
	public void Skill_DRAX15AEffect(Drax drax, Character target)
	{
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX15A");
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		int time = skillDef.skillDurationTime;
		
		int damage = target.getSkillDamageValue(drax.realAtk, ((Effect)tempNumber["atk_PHY"]).num);//(int)((((Effect)tempNumber["atk_PHY"]).num / 100.0f + 1.0f) * drax.realAtk.PHY);
				
		
//		target.layDownWithSeconds();
		
		State s = new State(time, null);
		target.addAbnormalState(s, Character.ABNORMAL_NUM.LAYDOWN);
		DestroyBuffEft(drax, "Skill_DRAX15A");
		
		target.realDamage(damage);
	}
	
	public void DestroyBuffEft(Character character, string buffName)
	{
		if(buffName == "Skill_DRAX15A")
		{		
			
			Debug.LogError("DestroyBuffEft");
			(character as Drax).attackAnimaEvent -= Skill_DRAX15AEffect;
			
			foreach(GameObject buffEft in skillBuffEftList)
			{
				Destroy(buffEft);
			}
		}
	}
}
