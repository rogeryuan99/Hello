using UnityEngine;
using System.Collections;

public class Skill_STARLORD15B : SkillBase {

	private int damage;
	protected int time;
	protected GameObject generatorPrb;
	protected GameObject craftPrb;
	protected GameObject jarPrb;
	protected GameObject blastGeneratorPrb;
	
	protected GameObject craft;
	protected GameObject jar;
	protected ArrayList parms;
	
	protected Vector3 centerPoint = new Vector3(0f, BattleBg.actionBounds.center.y, -56.17027f);
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		MusicManager.playEffectMusic("SFX_StarLord_Life_Generator_1a");
		parms = objs;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.toward(Vector3.zero);
		heroDoc.castSkill("Skill15B");
		yield return new WaitForSeconds(1f);
			
		// 
		// SkillEft_STARLORD15B_LifeGenerator
		CreateAirCraft();
		CreateFallDownGenerator();
		Invoke("CreateBlastGenerator", .8f);
		Invoke("CreateLifeGenerator",  1f);
		// CreateLifeGenerator();
	}
	
	private void CreateAirCraft(){
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		if (null == craftPrb){
			craftPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD15B_AirCraft") as GameObject;
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
		if (null == jarPrb){
			jarPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD15B_Jar") as GameObject;
		}
		jar = Instantiate(jarPrb) as GameObject;
		jar.transform.position = new Vector3(0f, 700f, centerPoint.z);
		iTween.MoveTo(jar, new Hashtable(){
			{"y",centerPoint.y}, 
			{"time",.5f}, 
			{"delay",.3f}, 
			{"easetype","linear"}, 
			{"onCompleteTarget",gameObject},
			{"onComplete", "CreateFallDownGenerator_Clear"}
		});
	}
	private void CreateFallDownGenerator_Clear(){
		Destroy(jar);
	}
	
	private void CreateBlastGenerator(){
		GameObject caller = parms[1] as GameObject;
		if (null == blastGeneratorPrb){
			blastGeneratorPrb = Resources.Load("eft/StarLord/BoneSTARLORD15A_BlastGenerator") as GameObject;
		}
		GameObject blastGenerator = Instantiate(blastGeneratorPrb) as GameObject;
		blastGenerator.transform.position = new Vector3(0f, centerPoint.y, centerPoint.z);
	}
	
	private void CreateLifeGenerator(){
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData heroData = (heroDoc.data as HeroData);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD15B");
		Hashtable tempNumber = skillDef.activeEffectTable;
		time = (int)skillDef.skillDurationTime;
		
		if(generatorPrb == null)
		{
			generatorPrb = Resources.Load("eft/StarLord/BoneSTARLORD15A_LifeGenerator") as GameObject;
		}
		Vector3 pos = new Vector3(0f, centerPoint.y, centerPoint.z);
		GameObject generatorObj = Instantiate(generatorPrb, pos, transform.rotation) as GameObject;
		Vector3 scale = generatorObj.transform.localScale;
		generatorObj.transform.localScale = Vector3.zero;
		iTween.ScaleTo(generatorObj, scale, .2f);
		
		if( (heroDoc.data as HeroData).passiveSkillIDList.Contains("STARLORD10B")){
			time = time*2;	
		}
		Generator g = generatorObj.GetComponent<Generator>();
		g.init(tempNumber, Generator.GeneratorType.LifeGenerator, HeroMgr.heroHash, time);
	}
}
