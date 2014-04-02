using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CHARLIE275B : SkillBase {
	private ArrayList objs;
	
	private Object handLightingPrefab;
	private Object headLightingPrefab;
	private Object bodyLightingPrefab;
	
	
	private List<GameObject> skillBuffEftList = new List<GameObject>();

	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		charlie27.castSkill("Skill5B");
		yield return new WaitForSeconds(0.79f);
		StartCoroutine(showHeadLighting());
		StartCoroutine(showBodyLighting());
		yield return new WaitForSeconds(0.6f);
		StartCoroutine(showHandLighting());
		addBuff();
	}
	
	private IEnumerator showHeadLighting(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		if (null == headLightingPrefab){
			headLightingPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5B_Head_Lighting");
		}
		GameObject headLighting = Instantiate(headLightingPrefab) as GameObject;
		bool isLeftSide = charlie27.model.transform.localScale.x > 0;
		headLighting.transform.position = caller.transform.position + new Vector3(isLeftSide? -12: 12, 148, 0);
		yield return new WaitForSeconds(1f);
		Destroy(headLighting);
		
	}
	
	private IEnumerator showBodyLighting(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		bool isLeftSide = charlie27.model.transform.localScale.x > 0;
		
		if(isLeftSide){
			bodyLightingPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5B_Body_Lighting_Left");
		}else{
			bodyLightingPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5B_Body_Lighting_Right");
		}
		GameObject bodyLighting = Instantiate(bodyLightingPrefab) as GameObject;
		bodyLighting.transform.position = caller.transform.position + new Vector3(isLeftSide? -3: 3, 90, 5);
		bodyLighting.transform.localScale = new Vector3(isLeftSide?0.8f:-0.8f,0.8f,0.8f);
		yield return new WaitForSeconds(1f);
		Destroy(bodyLighting);
		
	}
	
	private IEnumerator showHandLighting(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		if (null == handLightingPrefab){
			handLightingPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_5B_Hand_Lighting");
		}
			
		PackedSprite[] weaponPackedSprites = charlie27.model.GetComponentsInChildren<PackedSprite>();
		for(int i = 0; i < weaponPackedSprites.Length; i++)
		{
			GameObject weaponObj = weaponPackedSprites[i].gameObject;
			if(weaponObj.name.Contains("LARGE_Arm_Back_Lower_01"))
			{
				GameObject handLighting = Instantiate(handLightingPrefab) as GameObject;
				SkillEftShowByDrawLayer se = handLighting.GetComponent<SkillEftShowByDrawLayer>();
				se.flag = weaponObj.GetComponent<PackedSprite>();
				handLighting.transform.parent = weaponObj.transform;
				handLighting.transform.localPosition = new Vector3(257f,86f,0);
				handLighting.transform.localScale = new Vector3(20f,20f,20f);
				skillBuffEftList.Add(handLighting);
			}else if(weaponObj.name.Contains("LARGE_Arm_Top_Lower_01")){
				GameObject handLighting = Instantiate(handLightingPrefab) as GameObject;
				SkillEftShowByDrawLayer se = handLighting.GetComponent<SkillEftShowByDrawLayer>();
				se.flag = weaponObj.GetComponent<PackedSprite>();
				handLighting.transform.parent = weaponObj.transform;
				handLighting.transform.localPosition = new Vector3(225f,-50f,0);
				handLighting.transform.localScale = new Vector3(20f,20f,20f);
				skillBuffEftList.Add(handLighting);
			}
		}
		yield return new WaitForSeconds(0.3f);
		foreach(GameObject buffEft in skillBuffEftList)
		{
			Destroy(buffEft);
		}
	}
	
	private void addBuff(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CHARLIE275B");
		int time = def.buffDurationTime;
		float v = ((Effect)def.buffEffectTable["atk_PHY"]).num * 0.01f * charlie27.realAtk.PHY;
		charlie27.addBuff("Skill_CHARLIE275B", time, v, BuffTypes.ATK_PHY, buffFinish);
		InvokeRepeating("buffEft", 0f, 0.2f);	
	}
	
	public void buffFinish(Character character, Buff self)
	{
		CancelInvoke("buffEft");
	}
	
	private void buffEft(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		charlie27.flash(0.75f,0.9f,0.24f);
	}
}
