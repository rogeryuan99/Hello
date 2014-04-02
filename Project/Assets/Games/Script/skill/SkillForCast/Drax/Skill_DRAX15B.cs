using UnityEngine;
using System.Collections;

public class Skill_DRAX15B : SkillBase {
	
	private Object holoPrefab;
	private GameObject bigHolo;
	private Object stonesPrefab;
	private GameObject stones_a;
	private GameObject stones_b;
	private Object grownLightPrefab;
	private int radius = 0;
	private int time = 0;
	
	private ArrayList parms;
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		Hero drax = caller.GetComponent<Hero>();
		drax.castSkill("Skill15B_a");
		yield return new WaitForSeconds(.34f);
		MusicManager.playEffectMusic("SFX_Drax_Fear_Me_1a");
		
		yield return new WaitForSeconds(.7f);
		StartCoroutine(ShowStones(stones_a));
		yield return new WaitForSeconds(.46f);
		caller.transform.position = new Vector3(0, 0, StaticData.objLayer);
		drax.castSkill("Skill15B_b");
		if(!drax.isDead){
			Debug.LogError("4343");
			StartCoroutine(ShowStones(stones_b));
			StartCoroutine(ShowGrownLight());
			StartCoroutine(ShowBigHolo());
			ReadData();
			FearMe();
		}
	}
	
	private IEnumerator ShowSkillEft_a(){
		yield return new WaitForSeconds(0f);
	}
	private IEnumerator ShowStones(GameObject stones){
		GameObject caller = parms[1] as GameObject;
		if (null == stonesPrefab){
			stonesPrefab = Resources.Load("eft/Drax/SkillEft_Drax15B_Drax11");
		}
		stones = Instantiate(stonesPrefab) as GameObject;
		stones.transform.position = caller.transform.position;
		
		yield return new WaitForSeconds(.5f);
		iTween.ColorTo(stones, new Color(1f,1f,1f,0f), .3f);
		
		yield return new WaitForSeconds(.3f);
		iTween.Stop(stones);
		Destroy(stones);
	}
	private IEnumerator ShowGrownLight(){
		GameObject caller = parms[1] as GameObject;
		if (null == grownLightPrefab){
			grownLightPrefab = Resources.Load("eft/Drax/SkillEft_Drax15B_Drax31");
		}
		GameObject grownLight = Instantiate(grownLightPrefab) as GameObject;
		grownLight.transform.position = caller.transform.position + new Vector3(0f, 150f, 10f);
		grownLight.transform.localScale = Vector3.zero;
		
		iTween.ScaleTo(grownLight, Vector3.one, .8f);
		iTween.ColorTo(grownLight, new Color(1f, 1f, 1f, 0f), .5f);
		
		yield return new WaitForSeconds(.8f);
		iTween.Stop (grownLight);
		Destroy (grownLight);
	}
	
	private IEnumerator ShowBigHolo(){
		GameObject caller = parms[1] as GameObject;
		
		yield return new WaitForSeconds(.3f);
		
		if (null == holoPrefab){
			holoPrefab = Resources.Load("eft/Drax/SkillEft_Drax15B_Drax5");
		}
		bigHolo = Instantiate(holoPrefab) as GameObject;
		bigHolo.transform.parent = caller.transform;
		bigHolo.transform.localScale = new Vector3(2.2f,2.2f,1f);
		bigHolo.transform.position = caller.transform.position + new Vector3(0f, 150f, -10f);
		
		yield return new WaitForSeconds(.5f);
		iTween.ScaleTo(bigHolo, new Vector3(2f,2f,1f), .3f);
		iTween.ColorTo(bigHolo, new Hashtable(){{"a",0f}, {"time",.3f}});
		yield return new WaitForSeconds(.3f);
		Destroy(bigHolo);
	}
	
	private IEnumerator ShowSmallHolo(){
		GameObject caller = parms[1] as GameObject;
		
		yield return new WaitForSeconds(.3f);
		
		if (null == holoPrefab){
			holoPrefab = Resources.Load("eft/Drax/SkillEft_Drax15B_Drax5");
		}
		GameObject smallHolo = Instantiate(holoPrefab) as GameObject;
		smallHolo.transform.parent = caller.transform;
		smallHolo.transform.localScale = new Vector3(2f,2f,1f);
		smallHolo.transform.position = caller.transform.position + new Vector3(0f, 150f, -10f);
		smallHolo.transform.Rotate(new Vector3(0f, 0f, 15f));
		
		yield return new WaitForSeconds(.5f);
		iTween.ScaleTo(smallHolo, new Vector3(2f,2f,1f), .3f);
		iTween.ColorTo(smallHolo, new Hashtable(){{"a",0f}, {"time",.3f}});
		yield return new WaitForSeconds(.3f);
		Destroy(smallHolo);
	}
	
	private void ReadData(){
		GameObject caller = parms[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		HeroData tempHeroData = (heroDoc.data as HeroData);
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX15B");
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		radius = (int)tempNumber["AOERadius"];
		time = skillDef.skillDurationTime;
	}
	
	private void FearMe(){
		GameObject caller = parms[1] as GameObject;
		
		foreach( string key in EnemyMgr.enemyHash.Keys ){
			Enemy otherEnemy = EnemyMgr.enemyHash[key] as Enemy;
			Vector2 vc2 = otherEnemy.transform.position - caller.transform.position;
			if( StaticData.isInOval(radius, radius, vc2) ){
				if(otherEnemy.isDead){
					continue;
				}
				State s = new State(time, null);
				otherEnemy.addAbnormalState(s, Character.ABNORMAL_NUM.FEAR);
//				otherEnemy.fearWithSeconds();
			}
		}
	}
}
