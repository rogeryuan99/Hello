using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL30A : SkillBase {
	private ArrayList objs;
	private Object bodyLightingPreb;
	private Object weaponLightingPreb;
	
	private GameObject bodyLighting;
	private GameObject weaponLighting;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		BetaRayBill betaRayBill = caller.GetComponent<BetaRayBill>();
		betaRayBill.castSkill("Skill30A");
		betaRayBill.isTrigger30A = true;
		yield return new WaitForSeconds(0.8f);
		showWeaponLighting();
		yield return new WaitForSeconds(.75f);
		StartCoroutine(showBodyLighting());
		addBuff();
	}
	
	private void showWeaponLighting(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();
		if(null == weaponLightingPreb){
			weaponLightingPreb = Resources.Load("eft/BetaRayBill/Skill30ALighting/SkillEft_BETARAYBILL30A_LIGHTING");
		}
		GameObject weaponLighting = Instantiate(weaponLightingPreb) as GameObject;
		bool isLeftSide = betaRayBill.model.transform.localScale.x > 0;
		weaponLighting.transform.position = betaRayBill.transform.position + new Vector3(isLeftSide ? 5:-5, 74,10);
		weaponLighting.transform.localScale = new Vector3(isLeftSide ? 0.18f:-0.18f, 0.18f, 0.18f);
	}
	
	private IEnumerator showBodyLighting(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();
		if(null == bodyLightingPreb){
			bodyLightingPreb = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL30A_Body_Lightning");
		}
		if(null == bodyLighting){
			bodyLighting = Instantiate(bodyLightingPreb) as GameObject;
		}
		bool isLeftSide = betaRayBill.model.transform.localScale.x > 0;
		bodyLighting.transform.position = betaRayBill.transform.position + new Vector3(isLeftSide ? 0:0, 100,10);
		bodyLighting.transform.localScale = new Vector3(isLeftSide ? 0.75f:-0.75f, 0.75f, 0.75f);
		bodyLighting.transform.parent = betaRayBill.transform;
		
		GameObject go = bodyLighting.transform.FindChild("Body").gameObject;
		Debug.LogError(go);
		PackedSprite ps = go.GetComponent<PackedSprite>();
		yield return new WaitForSeconds(0.1f);
		ps.defaultAnim = 1;
		yield return new WaitForSeconds(0.18f);
		ps.transform.position += new Vector3(isLeftSide?10:-10,5,0);

	}
	
	private void addBuff(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();

		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL30A");
		int skillDurationTime = skillDef.buffDurationTime;
		float v = betaRayBill.realAtk.PHY;
		betaRayBill.addBuff("Skill_BETARAYBILL30A", skillDurationTime, v, BuffTypes.ATK_PHY, cancelSkill_BETARAYBILL30A);
	}
		
	private void cancelSkill_BETARAYBILL30A(Character character, Buff self){
		GameObject caller = objs[1] as GameObject;
		BetaRayBill betaRayBill = caller.GetComponent<BetaRayBill>();
		betaRayBill.isTrigger30A = false;
		Destroy(bodyLighting);
	}
}
