using UnityEngine;
using System.Collections;

public class Skill_CHARLIE2715A : SkillBase {
	protected Character charlie27;
	protected Character enemy;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.charlie27 = caller.GetComponent<Character>();
		this.enemy = target.GetComponent<Character>();
		
		charlie27.castSkill("Skill15A");
		charlie27.toward(enemy.transform.position);
		
//		if (charlie27 is Charlie27)
//			(charlie27 as Charlie27)
//		else
//			(charlie27 as Ch3_Charlie27)
		if (charlie27 is Charlie27){
			(charlie27 as Charlie27).showSkill15AHitEftCallback += showHitEft;
			(charlie27 as Charlie27).showSkill15ABangEftCallback += showBangEft;
		}
		else{
			(charlie27 as Ch3_Charlie27).showSkill15AHitEftCallback += showHitEft;
			(charlie27 as Ch3_Charlie27).showSkill15ABangEftCallback += showBangEft;
		}
		
		yield return new WaitForSeconds(0f);
	}
	
	protected void showHitEft(Character c){
		if (charlie27 is Charlie27)
			(charlie27 as Charlie27).showSkill15AHitEftCallback -= showHitEft;
		else
			(charlie27 as Ch3_Charlie27).showSkill15AHitEftCallback -= showHitEft;
		
		float distanceX = (c.model.transform.localScale.x > 0)? -150 : 150;
		
		iTween.MoveTo(c.gameObject, new Hashtable(){
			{"x", enemy.transform.position.x + distanceX},
			{"y", enemy.transform.position.y},
			{"time",  0.1f},
			{"easeType", "liner"}
		});
		
		StartCoroutine(delayShowHitEft(c));
	}
	
	protected IEnumerator delayShowHitEft(Character c){
		yield return new WaitForSeconds(0.1f);
		
		GameObject hitEftPrefab = Resources.Load("eft/Charlie27/SkillEft_CHARLIE27_15A_HitEft") as GameObject;
		GameObject hitEft = Instantiate(hitEftPrefab) as GameObject;
		hitEft.transform.parent = c.transform;
		if(charlie27.model.transform.localScale.x > 0){
			hitEft.transform.localPosition = new Vector3(500,750,0);
			hitEft.transform.localScale = new Vector3(4,4,1);
		}else{
			hitEft.transform.localPosition = new Vector3(-500,750,0);
			hitEft.transform.localScale = new Vector3(-4,4,1);	
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("CHARLIE2715A"); 
		int aoeRadius = (int)skillDef.activeEffectTable["AOERadius"];
		
		Vector2 vc2 = charlie27.transform.position - enemy.transform.position;
		if(StaticData.isInOval(aoeRadius,aoeRadius,vc2) && !enemy.isDead){
			enemy.playDamageEffect(charlie27.gameObject,400f);
		}
		
		int tempNum = (int)((Effect)skillDef.buffEffectTable["atk_PHY"]).num;
		int stateTime = skillDef.buffDurationTime;
		int ran = Random.Range(0,100);
		if(!enemy.isDead && ran < tempNum){
			State s= new State(stateTime, null);
			enemy.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
		}	
		
	}
	
	protected void showBangEft(Character c){
		if (charlie27 is Charlie27)
			(charlie27 as Charlie27).showSkill15ABangEftCallback -= showBangEft;
		else
			(charlie27 as Ch3_Charlie27).showSkill15ABangEftCallback -= showBangEft;
		
		GameObject bangEftPrefab = Resources.Load("eft/Charlie27/SkillEft_CHARLIE27_15A_BangEft") as GameObject;
		GameObject bangEft = Instantiate(bangEftPrefab) as GameObject;
		bangEft.transform.parent = c.transform;
		if(charlie27.model.transform.localScale.x > 0){
			bangEft.transform.localPosition = new Vector3(600,600,-100);
			bangEft.transform.localScale = new Vector3(100,100,100);
		}else{
			bangEft.transform.localPosition = new Vector3(-600,600,-100);
			bangEft.transform.localScale = new Vector3(-100,100,100);	
		}
	}
}
