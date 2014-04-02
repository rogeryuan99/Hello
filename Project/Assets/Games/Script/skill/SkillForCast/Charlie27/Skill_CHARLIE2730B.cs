using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CHARLIE2730B : SkillBase {
	private ArrayList objs;
	
	private Object redCirclePreb;
	private Object redLightingPreb;
	private Object crackPreb;
	private Object handLightingPrefab;
	
	private List<GameObject> skillBuffEftList = new List<GameObject>();

	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		charlie27.castSkill("Skill30B");
		yield return new WaitForSeconds(1.3f);
		StartCoroutine(showRedCircleEft());
		StartCoroutine(showCrackEft());
		StartCoroutine(showRedLightingEft());
		yield return new WaitForSeconds(0.8f);
		StartCoroutine(showHandLighting());
		addBuff();
	}
	
	private IEnumerator showRedCircleEft(){
		GameObject caller = objs[1] as GameObject;
		if(null == redCirclePreb){
			redCirclePreb = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30B_Red_Circle");
		}
		GameObject redCircle = Instantiate(redCirclePreb) as GameObject;
		redCircle.transform.localScale = new Vector3(1,1,1); 
		redCircle.transform.position = caller.transform.position;
		yield return new WaitForSeconds(1f);
		Destroy(redCircle);
	}
	
	private IEnumerator showCrackEft(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		if(null == crackPreb){
			crackPreb = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30B_Crack");
		}
		GameObject crack = Instantiate(crackPreb) as GameObject;
		bool isLeftSide = charlie27.model.transform.localScale.x > 0;
		crack.transform.localScale = new Vector3(isLeftSide?1:-1,1,1); 
		crack.transform.position = caller.transform.position + new Vector3(isLeftSide?-28:28,-25,5);
		yield return new WaitForSeconds(1f);
		Destroy(crack);
	}
	
	private IEnumerator showRedLightingEft(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		if(null == redLightingPreb){
			redLightingPreb = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30B_Red_Lighting");
		}
		GameObject redLighting = Instantiate(redLightingPreb) as GameObject;
		bool isLeftSide = charlie27.model.transform.localScale.x > 0;
		redLighting.transform.localScale = new Vector3(isLeftSide?1:-1,1,1); 
		redLighting.transform.position = caller.transform.position;
		redLighting.transform.parent = charlie27.transform;
		yield return new WaitForSeconds(.5f);
		
		Destroy(redLighting);
	}
	
	private IEnumerator showHandLighting(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		if (null == handLightingPrefab){
			handLightingPrefab = Resources.Load("eft/Charlie27/SkillEft_Charlie27_30B_Hand_lighting");
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
				handLighting.transform.localScale = new Vector3(5f,5f,5f);
				skillBuffEftList.Add(handLighting);
			}else if(weaponObj.name.Contains("LARGE_Arm_Top_Lower_01")){
				GameObject handLighting = Instantiate(handLightingPrefab) as GameObject;
				SkillEftShowByDrawLayer se = handLighting.GetComponent<SkillEftShowByDrawLayer>();
				se.flag = weaponObj.GetComponent<PackedSprite>();
				handLighting.transform.parent = weaponObj.transform;
				handLighting.transform.localPosition = new Vector3(225f,-50f,0);
				handLighting.transform.localScale = new Vector3(5f,5f,5f);
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
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CHARLIE2730B");
		int time = def.buffDurationTime;
		float v = charlie27.realAtk.PHY;
		charlie27.addBuff("Skill_CHARLIE2730B", time, v, BuffTypes.ATK_PHY, buffFinish);
		InvokeRepeating("buffEft", 0f, 0.2f);	
	}
	
	public void buffFinish(Character character, Buff self)
	{
		CancelInvoke("buffEft");
	}
	
	private void buffEft(){
		GameObject caller = objs[1] as GameObject;
		Character charlie27   = caller.GetComponent<Character>();
		charlie27.flash(1,0.5f,0.24f);
	}
}
