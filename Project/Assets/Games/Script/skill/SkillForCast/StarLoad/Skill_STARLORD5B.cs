using UnityEngine;
using System.Collections;

public class Skill_STARLORD5B : SkillBase {
	
	private GameObject craftPrb;
	private GameObject generatorPrb;
	private GameObject radiationPrb;
	private GameObject fallDownGeneratorPrb;
	private ArrayList parms;
	
	private GameObject craft;
	private GameObject fallDownGenerator;
	
	protected Vector3 centerPoint = new Vector3(0f, BattleBg.actionBounds.center.y, -56.17027f);
	
	public override IEnumerator Cast (ArrayList objs){
		MusicManager.playEffectMusic("SFX_StarLord_Shield_Generator_1a");
		yield return new WaitForSeconds(0f);
		
		parms = objs;
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.castSkill("Skill5B");
		
		Invoke("CreateAirCraft", 1f);
		Invoke("CreateFallDownGenerator", 1.3f);
		Invoke("CreateRadiation1", .7f);
		Invoke("CreateRadiation2", 1.1f);
		Invoke("CreateGenerator",2.3f);
	}
	
	private void CreateAirCraft(){
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		if (null == craftPrb){
			craftPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD5B_AirCraft") as GameObject;
		}
		craft = Instantiate(craftPrb) as GameObject;
		craft.transform.position = new Vector3(-1700f, centerPoint.y, centerPoint.z);
		iTween.MoveTo(craft, new Hashtable(){
			{"x",0},
			{"time",.5f},
			{"easetype","easeOutQuint"}
		});
		iTween.MoveTo(craft, new Hashtable(){
			{"x",1700},
			{"delay",.5f},
			{"time",.5f},
			{"easetype","easeInQuint"},
			{"onCompleteTarget",gameObject},
			{"onComplete","CreateAirCraft_Clear"}
		});
	}
	private void CreateAirCraft_Clear(){
		Destroy (craft);
	}
	
	private void CreateFallDownGenerator(){
		GameObject caller = parms[1] as GameObject;
		if (null == fallDownGeneratorPrb){
			fallDownGeneratorPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD5B_FallDownGenerator") as GameObject;
		}
		fallDownGenerator = Instantiate(fallDownGeneratorPrb) as GameObject;
		fallDownGenerator.transform.position = new Vector3(0f, 700f, centerPoint.z);
		iTween.MoveTo(fallDownGenerator, new Hashtable(){
			{"y",centerPoint.y}, 
			{"time",.5f}, 
			{"easetype","linear"}
		});
		iTween.ScaleTo(fallDownGenerator, new Hashtable(){
			{"y",.9f},
			{"delay",.5f},
			{"time",.5f},
			{"easetype","easeOutElastic"}, 
			{"onCompleteTarget",gameObject},
			{"onComplete", "CreateFallDownGenerator_Clear"}
		});
	}
	private void CreateFallDownGenerator_Clear(){
		Destroy(fallDownGenerator);
	}

	
	private void CreateRadiation1(){
		if (null == radiationPrb){
			radiationPrb = Resources.Load("eft/StarLord/SkillEft_STARLOAD5B_Radiation") as GameObject;
		}
		GameObject radiation = Instantiate(radiationPrb) as GameObject;
		radiation.transform.parent = GameObject.Find("SMALL_Arm_Top_Walkie_01(Clone)").transform;
		radiation.transform.localPosition = new Vector3(-70f, 200f, -100f);
		radiation.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}
	private void CreateRadiation2(){
		if (null == radiationPrb){
			radiationPrb = Resources.Load("eft/StarLord/SkillEft_STARLOAD5B_Radiation") as GameObject;
		}
		GameObject radiation = Instantiate(radiationPrb) as GameObject;
		radiation.transform.parent = GameObject.Find("SMALL_Arm_Top_Walkie_02(Clone)").transform;
		radiation.transform.localPosition = new Vector3(140f, -65f, -100f);
		radiation.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,226f));
	}
	
	private void CreateGenerator(){
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData heroData = (heroDoc.data as HeroData);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD5B");
		Hashtable tempNumber = skillDef.activeEffectTable;
		int time = (int)skillDef.skillDurationTime;
		
		Hashtable passive1 =  heroData.getPSkillByID("STARLORD10B");
		if(passive1 != null)
		{
			SkillDef skillDef1 = SkillLib.instance.getSkillDefBySkillID("STARLORD10B");
			int per = (int)skillDef1.passiveEffectTable["universal"];
			time += time*(per/100);
		}
		
		if(generatorPrb == null)
		{
			generatorPrb = Resources.Load("eft/StarLord/BoneSTARLORD5B_ShieldGenerator") as GameObject;
		}
		Vector3 pos = new Vector3(0f, centerPoint.y, centerPoint.z);
		GameObject generatorObj = Instantiate(generatorPrb, pos, transform.rotation) as GameObject;
		Generator g = generatorObj.GetComponent<Generator>();
		g.init(tempNumber, Generator.GeneratorType.ShieldGenerator, HeroMgr.heroHash, time);
	}
}
