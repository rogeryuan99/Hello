using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_BETARAYBILL5B: SkillBase {
	private ArrayList objs;
	
	private Object weaponLightingPreb;
	
	
	private List<GameObject> skillBuffEftList = new List<GameObject>();

	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character betArayBill   = caller.GetComponent<Character>();
		betArayBill.castSkill("Skill5B");
		yield return new WaitForSeconds(0.75f);
		showWeaponLighting();
		yield return new WaitForSeconds(1.3f);
		addBuff();
	}
	
	private void showWeaponLighting(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();
		if(null == weaponLightingPreb){
			weaponLightingPreb = Resources.Load("eft/BetaRayBill/Skill5BLighting/SkillEft_BETARAYBILL5B_LIGHTING");
		}
		GameObject weaponLighting = Instantiate(weaponLightingPreb) as GameObject;
		bool isLeftSide = betaRayBill.model.transform.localScale.x > 0;
		weaponLighting.transform.position = betaRayBill.transform.position + new Vector3(isLeftSide ? 5:-5, 74,10);
		weaponLighting.transform.localScale = new Vector3(isLeftSide ? 0.18f:-0.18f, 0.18f, 0.18f);
	}
	
	private void addBuff(){
		GameObject caller = objs[1] as GameObject;
		Character betArayBill   = caller.GetComponent<Character>();
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL5B");
		int time = def.buffDurationTime;
		float v = ((Effect)def.buffEffectTable["atk_PHY"]).num * 0.01f * betArayBill.realAtk.PHY;
		betArayBill.addBuff("Skill_BETARAYBILL5B", time, v, BuffTypes.ATK_PHY, buffFinish);
		InvokeRepeating("buffEft", 0f, 0.2f);	
	}
	
	public void buffFinish(Character character, Buff self)
	{
		CancelInvoke("buffEft");
	}
	
	private void buffEft(){
		GameObject caller = objs[1] as GameObject;
		Character betArayBill   = caller.GetComponent<Character>();
		betArayBill.flash(0.75f,0.9f,0.24f);
	}
}
