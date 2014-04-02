using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_CAIERA5B : SkillBase {
	
	private Object firePrefab;
	private Object gapPrefab;
	private Object groundFirePrefab;
	private List<GameObject> fires = new List<GameObject>();
	
	private float delayTime = .50f;
	private ArrayList parms;
	public override IEnumerator Cast (ArrayList objs)
	{
		yield return new WaitForSeconds(1f);
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Character caiera = caller.GetComponent<Character>();
		
		caiera.castSkill("Skill5B");
		// yield return new WaitForSeconds(1.35f);
		// yield return new WaitForSeconds(.88f);
		yield return new WaitForSeconds(.88f - delayTime);
		StartCoroutine(CreateSkillEft());
		yield return new WaitForSeconds(delayTime);
		StartCoroutine(CreateGap());
		StartCoroutine(CreateGroundFire());
		AddBuf();
	}
	
	private IEnumerator CreateSkillEft(){
		CreateFire(new Vector3(  0f,150f, -1f), new Vector3(.6f,-.6f,1f), new Color( 1f, 1f, 1f, .5f));
		CreateFire(new Vector3(  0f,150f,  1f), new Vector3(.6f,-.6f,1f), new Color( 1f, 1f, 1f,  1f));
		CreateFire(new Vector3( 50f,150f,  1f), new Vector3(.4f,-.4f,1f), new Color( 1f, 1f, 1f,  1f));
		CreateFire(new Vector3(-30f,150f,  1f), new Vector3(.5f,-.5f,1f), new Color( 1f, 1f, 1f,  1f));
		StartCoroutine(MoveFires());
		
		yield return new WaitForSeconds(delayTime);
		ClearFires();
		CreateFire(new Vector3(-50f,0f, 1f), new Vector3(.6f,.6f,1f), new Color( 1f, 1f, 1f, 1f));
		CreateFire(new Vector3( 70f,0f, 1f), new Vector3(.8f,.8f,1f), new Color( 1f, 1f, 1f, 1f));
		CreateFire(new Vector3(  0f,0f, 1f), new Vector3( 1f, 1f,1f), new Color( 1f, 1f, 1f, 1f));
		CreateFire(new Vector3(  0f,0f,-1f), new Vector3( 1f, 1f,1f), new Color( 1f, 1f, 1f,.5f));
	}
	
	private void CreateFire(Vector3 pos, Vector3 scale, Color color){
		GameObject caller = parms[1] as GameObject;
		
		if (null == firePrefab){
			firePrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA5B_Fire");
		}
		GameObject fire = Instantiate(firePrefab) as GameObject;
		fire.transform.localScale = scale;
		fire.transform.parent = caller.transform;
		fire.transform.localPosition = pos;
		PackedSprite ps = fire.GetComponentInChildren<PackedSprite>();
		ps.Color = color;
			
		fires.Add(fire);
	}
	
	private IEnumerator CreateGap(){
		GameObject caller = parms[1] as GameObject;
		
		if (null == gapPrefab){
			gapPrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA5B_Gap");
		}
		GameObject gap = Instantiate(gapPrefab) as GameObject;
		gap.transform.position = caller.transform.position + new Vector3(0f,-22f,1.5f);
		
		yield return new WaitForSeconds(.1f);
		iTween.ColorTo(gap, new Color(.5f, .5f, .5f, 0f), .15f);
		yield return new WaitForSeconds(.15f);
		Destroy(gap);
	}
	
	private IEnumerator CreateGroundFire(){
		GameObject caller = parms[1] as GameObject;
		
		if (null == groundFirePrefab){
			groundFirePrefab = Resources.Load("eft/Caiera/SkillEft_CAIERA5B_GroundFire");
		}
		GameObject groundFire = Instantiate(groundFirePrefab) as GameObject;
		groundFire.transform.position = caller.transform.position + new Vector3(0f,0f,1f);
		
		iTween.ScaleTo(groundFire, new Vector3(2f, 2f, 2f), .25f);
		iTween.ColorTo(groundFire, new Color(.5f, .5f, .5f, 0f), .25f);
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
		
		yield return new WaitForSeconds(1f);
		Destroy(groundFire);
	}
	
	private void AddBuf(){
		GameObject caller = parms[1] as GameObject;
		Character caiera = caller.GetComponent<Character>();
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("CAIERA5B");
		float atk_phy = ((Effect)def.buffEffectTable["atk_PHY"]).num * .01f * caiera.realAtk.PHY;
		int time = def.buffDurationTime;
		
		caiera.addBuff("CAIERA5B", time, atk_phy, BuffTypes.ATK_PHY, 
						(character, self) => {
							ClearFires();
						}
		);
	}
	// FEMALE_Torso_01
	private IEnumerator MoveFires(){
		GameObject prt = new GameObject("Skill_Caiera5B_FireMotion");
		
		for (int i=fires.Count-1; i>=0; i--){
			fires[i].transform.parent = prt.transform;
		}
		
		iTween.MoveBy(prt, new Hashtable(){{"y", 80},{"time",.09f},{"easeType","easeInOutQuad"}});
		yield return new WaitForSeconds(delayTime);
		iTween.Stop(prt);
		Destroy(prt);
	}
	
	private void ClearFires(){
		for (int i=fires.Count-1; i>=0; i--){
			Destroy(fires[i]);
		}
		fires.Clear();
	}
}
