using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CAIERA15B : SkillBase {
	
	private Object chain1Prefab;
	private Object chain2Prefab;
	private Object rChainPrefab;
	private Object holoPrefab;
	private Object starPrefab;
	private ArrayList parms;
	
	protected List<GameObject> desGameObjectList = new List<GameObject>();
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		
		
		Character caiera = caller.GetComponent<Character>();
		
		caiera.castSkill("Skill15B");
		
		MusicManager.playEffectMusic("SFX_Caiera_Shield_of_Sakaar_1a");
		
		StartCoroutine(CreateRotationChainAndLight());
//		StartCoroutine(CreateChain1());
//		StartCoroutine(CreateChain2());
		yield return new WaitForSeconds(1.4f);
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CAIERA15B");
		int time = def.buffDurationTime;
		StartCoroutine(CreateHolo(time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		StartCoroutine(CreateStar(Random.Range(0f,1f), time));
		
		float v = ((Effect)def.buffEffectTable["def_PHY"]).num * 0.01f * caiera.realDef.PHY;
		caiera.addBuff("CAIERA15B", time, v, BuffTypes.DEF_PHY, buffFinish);
	}
	
	public void buffFinish(Character character, Buff self)
	{
		foreach(GameObject obj in desGameObjectList)
		{
			Destroy(obj);
		}
	}
	
//	private IEnumerator CreateChain1(){
//		GameObject caller = parms[1] as GameObject;
//		
//		yield return new WaitForSeconds(.6f);
//		
//		if (null == chain1Prefab){
//			chain1Prefab = Resources.Load("eft/Caiera/SkillEft_CAIERA15B_Chain1");
//		}
//		GameObject chain1 = Instantiate(chain1Prefab) as GameObject;
//		chain1.transform.position = caller.transform.position + new Vector3(0f,0f,0f);
//	}
//	private IEnumerator CreateChain2(){
//		GameObject caller = parms[1] as GameObject;
//		
//		yield return new WaitForSeconds(1.25f);
//		
//		if (null == chain2Prefab){
//			chain2Prefab = Resources.Load("eft/Caiera/SkillEft_CAIERA15B_Chain2");
//		}
//		GameObject chain2 = Instantiate(chain1Prefab) as GameObject;
//		chain2.transform.position = caller.transform.position + new Vector3(0f,0f,0f);
//	}
	
	private IEnumerator CreateRotationChainAndLight(){
		GameObject caller = parms[1] as GameObject;
		
		yield return new WaitForSeconds(.7f);
		
		if (null == rChainPrefab){
			rChainPrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA15B_ChainLight");
		}
		GameObject eft = Instantiate(rChainPrefab) as GameObject;
		eft.transform.position = caller.transform.position + new Vector3(-30f, 170f, 0f);
		
		desGameObjectList.Add(eft);
		
		yield return new WaitForSeconds(.7f);
		desGameObjectList.Remove(eft);
		Destroy(eft);
		
	}
	
	private IEnumerator CreateHolo(float time){
		GameObject caller = parms[1] as GameObject;
		
		if (null == holoPrefab){
			holoPrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA15B_Holo");
		}
		GameObject holo = Instantiate(holoPrefab) as GameObject;
		holo.transform.parent = caller.transform;
		holo.transform.localPosition = Vector3.zero;
		
		desGameObjectList.Add(holo);
		yield return new WaitForSeconds(time);
		desGameObjectList.Remove(holo);
		Destroy(holo);
	}
	
	private IEnumerator CreateStar(float delay, int time){
		GameObject caller = parms[1] as GameObject;
		
		yield return new WaitForSeconds(delay);
		
		if (null == starPrefab){
			starPrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA15B_Star");
		}
		GameObject star = Instantiate(starPrefab) as GameObject;
		star.transform.parent = caller.transform;
		star.transform.localPosition = new Vector3(Random.Range(-400f,400f),
													Random.Range(800f,30f),
													-1f);
		desGameObjectList.Add(star);
		yield return new WaitForSeconds(time);
		desGameObjectList.Remove(star);
		Destroy(star);
	}
}
