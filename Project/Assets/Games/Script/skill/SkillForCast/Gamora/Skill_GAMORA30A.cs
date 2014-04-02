using UnityEngine;
using System.Collections;

public class Skill_GAMORA30A : SkillBase {
	
	private ArrayList gameObjects = null;
	private const float DEATH_STRIKE_TIME = 1f;
	private const int DEATH_STRIKE_POINT_NUM = 10;
	
//	public override IEnumerator Cast (ArrayList objs){
//		GameObject caller = objs[1] as GameObject;
//		GameObject target = objs[2] as GameObject;
//		// Lock Everybody
//		Hero heroDoc = caller.GetComponent<Hero>();
//		heroDoc.castSkill("SkillA");
//		
//		yield return new WaitForSeconds(0.5f);
//			
//		deathstrikeObjs = objs;
////		heroDoc.heroAI.OnMoveToTargetDirectlyFinished += Deathstrike_Punch;
//		heroDoc.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, Deathstrike_Punch);
//		heroDoc.moveToTargetDirectly(target);
//	}
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Gamora_Deathstrike_1a");
		gameObjects = objs;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("Skill30A_a");
		yield return new WaitForSeconds(.8f);
		heroDoc.addHandlerToParmlessHandlerByParam(
			Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, 
			Skill30AcTrigger	
		);
		heroDoc.moveToTargetDirectly(target);
	}
	
	public void Skill30AcTrigger(Character c){
		GameObject caller = gameObjects[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		StartSkill30A_c(heroDoc);
	}
	
	public void StartSkill30A_c(Character character)
	{
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		ScreenController.Instance.EnableBlackScreen();
		gamora.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, Skill30AcTrigger);
		gamora.castSkill("Skill30A_c");
		gamora.transform.position = new Vector3(gamora.transform.position.x, gamora.transform.position.y, -400f);
		gamora.skillFinishedCallback = StartSkill30A_b;
//		target.GetComponent<Character>().layDownWithSeconds();
		
		State s = new State((int)(DEATH_STRIKE_TIME + 1f), null);
		target.GetComponent<Character>().addAbnormalState(s, Character.ABNORMAL_NUM.LAYDOWN);
		gamora.hideHpBar();
	}
	
	public void StartSkill30A_b()
	{
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		ScreenController.Instance.DisableBlackScreen();
		
		gamora.castSkill("Skill30A_b");
		gamora.showHpBar();
		DamageEnemy();
	}
	
	private void DamageEnemy(){
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		Character c = target.GetComponent<Character>();
		Gamora gamora = caller.GetComponent<Gamora>();
		
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("GAMORA30A").activeEffectTable; // GAMORA5A
		
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		c.realDamage(c.getSkillDamageValue(gamora.realAtk, tempAtkPer));
	}
	
	
	
	
	
	
	
	
	
	
	
	
//	private void Deathstrike_Punch(Character c = null){
//		GameObject caller = deathstrikeObjs[1] as GameObject;
//		GameObject target = deathstrikeObjs[2] as GameObject;
//		
//		ScreenController.Instance.EnableBlackScreen();
//		// StartCoroutine("PlayDeathstrikeEft");
//		PlayDeathstrikeEft();
//		Invoke("Deathstrike_Finish", DEATH_STRIKE_TIME);
//		target.GetComponent<Enemy>().layDownWithSeconds(DEATH_STRIKE_TIME + 1f);
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
////		heroDoc.heroAI.OnMoveToTargetDirectlyFinished -= Deathstrike_Punch;
//		heroDoc.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished, Deathstrike_Punch);
//	}
	private void Deathstrike_Finish()
	{
//		GameObject scene  = deathstrikeObjs[0] as GameObject;
//		GameObject caller = deathstrikeObjs[1] as GameObject;
//		GameObject target = deathstrikeObjs[2] as GameObject;
//		
//		ScreenController.Instance.DisableBlackScreen();
//		Enemy enemy = target.GetComponent<Enemy>();
//		
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
//		HeroData heroData = (heroDoc.data as HeroData);
//		
//		Hashtable tempNumber = (heroData.getASkillByID("GAMORA30A") ).number;
//		enemy.realDamage((int)tempNumber["AOENum"]);
		// Unlock Everybody
	}
	
//	private void PlayDeathstrikeEft(){
//		GameObject target = deathstrikeObjs[2] as GameObject;
//		GameObject particle = null;
//		Vector3 []points = GenerateDeathstrikePoints(target, DEATH_STRIKE_POINT_NUM);
//		
//		particle = LoadDeathstrikeParticle();
//		particle.transform.position = points[0];
//		float delay = 0f;
//		float time = 0f;
//		for (int i=1; i<DEATH_STRIKE_POINT_NUM; i++){
//			time = Random.Range(.05f, .1f);
//			iTween.MoveTo(particle, new Hashtable(){{"x",points[i].x}, {"y",points[i].y}, {"time",time}, {"delay",delay}});
//			delay += time;
//		}
//		// Destroy(particle);
//	}
//	private GameObject LoadDeathstrikeParticle(){
//		Object prefab = Resources.Load("eft/DeathstrikeEft");
//		GameObject particle = Instantiate(prefab) as GameObject;
//		// particle.transform.parent = GameObject.Find("UIRoot/GamePanel").transform;
//		
//		return particle;
//	}
//	private Vector3[] GenerateDeathstrikePoints(GameObject target, int num){
//		Vector3 []points = new Vector3[num];
//		Rect rect = new Rect(-480,-320,960,640 );
//		
//		for (int i=0; i<num; i++){
//			points[i] = new Vector3(Random.Range(rect.xMin, rect.xMax),
//										Random.Range(rect.yMin, rect.yMax),
//										-100f);
//			if (i>0 && 0==i%2){
//				if (Vector3.Distance(points[i-1], points[i]) < 500){
//					i--;
//					continue;
//				}
//			}
//		}
//		
//		return points;
//	}
//	
}
