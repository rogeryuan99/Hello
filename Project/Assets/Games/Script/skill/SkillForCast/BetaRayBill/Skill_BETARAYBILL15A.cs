using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL15A : SkillBase {
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
		betaRayBill.castSkill("Skill15A");
		betaRayBill.isTrigger15A = true;
		yield return new WaitForSeconds(0.8f);
		showWeaponLighting();
		yield return new WaitForSeconds(.75f);
		StartCoroutine(showBodyLighting());
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL15A");
		int skillDurationTime = skillDef.buffDurationTime;
		betaRayBill.addBuff("Skill_BETARAYBILL15A", skillDurationTime, 0, BuffTypes.ATK_PHY, cancelSkill_BETARAYBILL15A);
	}
	
	private void showWeaponLighting(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();
		if(null == weaponLightingPreb){
			weaponLightingPreb = Resources.Load("eft/BetaRayBill/Skill15ALighting/SkillEft_BETARAYBILL15A_LIGHTING");
		}
		if(null == weaponLighting){
			weaponLighting = Instantiate(weaponLightingPreb) as GameObject;
		}
		bool isLeftSide = betaRayBill.model.transform.localScale.x > 0;
		weaponLighting.transform.position = betaRayBill.transform.position + new Vector3(isLeftSide ? -11:11, 74,10);
		weaponLighting.transform.localScale = new Vector3(isLeftSide ? 0.18f:-0.18f, 0.18f, 0.18f);
	}
	
	private IEnumerator showBodyLighting(){
		GameObject caller = objs[1] as GameObject;
		Character betaRayBill = caller.GetComponent<Character>();
		if(null == bodyLightingPreb){
			bodyLightingPreb = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL15A_Body_Lightning");
		}
		if(null == bodyLighting){
			bodyLighting = Instantiate(bodyLightingPreb) as GameObject;
		}
		bool isLeftSide = betaRayBill.model.transform.localScale.x > 0;
		bodyLighting.transform.position = betaRayBill.transform.position + new Vector3(isLeftSide ? 0:0, 100,10);
		bodyLighting.transform.localScale = new Vector3(isLeftSide ? 0.75f:-0.75f, 0.75f, 0.75f);
		bodyLighting.transform.parent = betaRayBill.transform;
		
		GameObject go = bodyLighting.transform.FindChild("Body").gameObject;
		PackedSprite ps = go.GetComponent<PackedSprite>();
		yield return new WaitForSeconds(0.1f);
		ps.defaultAnim = 1;
		yield return new WaitForSeconds(0.18f);
		ps.transform.position += new Vector3(isLeftSide?10:-10,5,0);

	}
		
	private void cancelSkill_BETARAYBILL15A(Character character, Buff self){
		GameObject caller = objs[1] as GameObject;
		BetaRayBill betaRayBill = caller.GetComponent<BetaRayBill>();
		betaRayBill.isTrigger15A = false;
		Destroy(bodyLighting);
	}
}
