using UnityEngine;
using System.Collections;

public class Skill_CHARLIE271 : SkillBase {
	
	private GameObject lightCrashPrefab;
	private GameObject rushHoloPrefab;
	private GameObject rushLightPrefab;
	private Character charlie27;
	private Character enemy;
	
	private ArrayList parms;
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		charlie27 = caller.GetComponent<Character>();
		
		GameObject target = parms[2] as GameObject;
		enemy = target.GetComponent<Character>();
		
		charlie27.castSkill("SkillA");
		charlie27.toward(enemy.transform.position);
		
		LoadResources();
		
		if (charlie27 is Charlie27)
			AddFrameEvent(charlie27 as Charlie27);
		else
			AddFrameEvent(charlie27 as Ch3_Charlie27);
		
		yield return new WaitForSeconds(0f);
	}
	
	private void LoadResources(){
		if (null == lightCrashPrefab){
			lightCrashPrefab = Resources.Load("eft/Charlie27/SkillEft_CHARLIE27_1_LightCrash") as GameObject;
		}
		if (null == rushHoloPrefab){
			rushHoloPrefab = Resources.Load("eft/Charlie27/SkillEft_CHARLIE27_1_RushHolo") as GameObject;
		}
		if (null == rushLightPrefab){
			rushLightPrefab = Resources.Load("eft/Charlie27/SkillEft_CHARLIE27_1_RushLight") as GameObject;
		}
	}
	
	private void AddFrameEvent(Charlie27 c27){
		c27.showSkill1LightCrashEftCallback += CreateLightCrash;
		c27.showSkill1RushLightEftCallback += CreateRushLight;
		c27.showSkill1DamageCallback += showDamage;
	}
	private void AddFrameEvent(Ch3_Charlie27 c27){
		c27.showSkill1LightCrashEftCallback += CreateLightCrash;
		c27.showSkill1RushLightEftCallback += CreateRushLight;
		c27.showSkill1DamageCallback += showDamage;
	}
	
	protected void CreateLightCrash(Character c){
		if (charlie27 is Charlie27)
			(charlie27 as Charlie27).showSkill1LightCrashEftCallback -= CreateLightCrash;
		else
			(charlie27 as Ch3_Charlie27).showSkill1LightCrashEftCallback -= CreateLightCrash;
		
		GameObject lightCrash = Instantiate(lightCrashPrefab) as GameObject;
		lightCrash.transform.parent = charlie27.transform;
		
		if(c.model.transform.localScale.x > 0){
			lightCrash.transform.localPosition = new Vector3(-300,450,-100);	
		}else{
			lightCrash.transform.localPosition = new Vector3(650,500,-100);
		}

		lightCrash.transform.localScale = new Vector3(5,5,1);
	}
	
	protected void CreateRushLight(Character c){
		if (charlie27 is Charlie27)
			(charlie27 as Charlie27).showSkill1RushLightEftCallback -= CreateRushLight;
		else
			(charlie27 as Ch3_Charlie27).showSkill1RushLightEftCallback -= CreateRushLight;
		
		showRushHolo();
		
		GameObject rushLight = Instantiate(rushLightPrefab) as GameObject;
		rushLight.transform.parent = charlie27.transform;
		
		if(c.model.transform.localScale.x > 0){
			rushLight.transform.localPosition = new Vector3(-600,270,1);
			rushLight.transform.localScale = new Vector3(3,3,1);
		}else{
			rushLight.transform.localPosition = new Vector3(600,270,1);
			rushLight.transform.localScale = new Vector3(-3,3,1);	
		}
		
		float distanceX = (c.model.transform.localScale.x > 0)? -150 : 150;
		
		iTween.MoveTo(c.gameObject, new Hashtable(){
			{"x", enemy.transform.position.x + distanceX},
			{"y", enemy.transform.position.y},
			{"time",  0.2f},
			{"easeType", "liner"}
		});
		
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.6f, 0f));
	}
	
	protected void showRushHolo(){
		GameObject rushHolo = Instantiate(rushHoloPrefab) as GameObject;
		rushHolo.transform.parent = charlie27.transform;
		if(charlie27.model.transform.localScale.x > 0){
			rushHolo.transform.localPosition = new Vector3(350,270,0);
			rushHolo.transform.localScale = new Vector3(4,4,1);
		}else{
			rushHolo.transform.localPosition = new Vector3(-350,270,0);
			rushHolo.transform.localScale = new Vector3(-4,4,1);
		}
	}
	
	protected void showDamage(Character c){
		if (charlie27 is Charlie27)
			(charlie27 as Charlie27).showSkill1DamageCallback -= showDamage;
		else
			(charlie27 as Ch3_Charlie27).showSkill1DamageCallback -= showDamage;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("CHARLIE271");
		float tempHp = ((Effect)skillDef.activeEffectTable["hp"]).num;
		int tempSelfHp = (int)(enemy.realMaxHp * (tempHp / 100f));
		
		if(!enemy.isDead) enemy.realDamage(tempSelfHp);
	}
}
