using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_MANTIS30B : SkillBase{
	
	private Object holoPrefab;
	private Object bubblePrefab;
	private List<GameObject> enemyBubbles = new List<GameObject>();
	protected ArrayList parms;
	private int time;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		
		Mantis heroDoc = caller.GetComponent<Mantis>();
		heroDoc.castSkill("Skill30B");
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["MANTIS30B"] as SkillDef;
		time = skillDef.buffDurationTime;
		
		StartCoroutine(CreateHolo());
		//StartCoroutine(CreateBubble(new Vector3(0f, 600f, 0f), new Vector3(3f, 3f, 1f)));
		StartCoroutine(CreateBubble(new Vector3(0f, 600f, -10f), new Vector3(3f, 3f, 1f)));
		StartCoroutine(CreateBubble(new Vector3(-200f, 300f, -10f), new Vector3(2.5f, 2.5f, 1f)));
		StartCoroutine(CreateBubble(new Vector3(200f, 300f, -10f), new Vector3(2.5f, 2.5f, 1f)));
		
		StartCoroutine(CreateBubbleOnTarget());
		
		yield return new WaitForSeconds(0.4f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Psychic_Mastery_1a");
	}
	
	private IEnumerator CreateHolo(){
		GameObject caller = parms[1] as GameObject;
		
		if (null == holoPrefab){
			holoPrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS30B_Holo");
		}
		yield return new WaitForSeconds(1.58f);
		GameObject holo = Instantiate(holoPrefab) as GameObject;
		holo.transform.parent = caller.transform;
		holo.transform.localPosition = new Vector3(-10f, 342f, 0f);
		holo.transform.localScale = new Vector3(3.22f, 3.22f, 1f);
		
		yield return new WaitForSeconds(time);
		
		Destroy(holo);
	}
	
	private IEnumerator CreateBubble(Vector3 pos, Vector3 scale){
		GameObject caller = parms[1] as GameObject;
		
		if (null == bubblePrefab){
			bubblePrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS30B_Bubble");
		}
		yield return new WaitForSeconds(.7f);
		GameObject bubble = Instantiate(bubblePrefab) as GameObject;
		bubble.transform.parent = caller.transform;
		bubble.transform.localPosition = pos;
		bubble.transform.localScale = scale;
		
		yield return new WaitForSeconds(time);
		
		Destroy(bubble);
	}
	
	private IEnumerator CreateBubbleOnTarget(){
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		foreach(Enemy enemy in enemyList)
		{
			enemy.currentColor =  (Color)new Color32(255,128,128,255);
			enemy.model.renderer.material.color = enemy.currentColor;
			enemy.realDamage(0);
			
			if (null == bubblePrefab){
				bubblePrefab = Resources.Load("eft/Mantis/SkillEft_MANTIS30B_Bubble");
			}
			GameObject bubble = Instantiate(bubblePrefab) as GameObject;
			bubble.transform.parent = enemy.transform;
			bubble.transform.localPosition = new Vector3(0f, 300f, 0f);
			bubble.transform.localScale = new Vector3(3f, 3f, 1f);
			enemyBubbles.Add(bubble);
			
			enemy.isAtkSameTag = true;
			enemy.setAbnormalState(Character.ABNORMAL_NUM.CREAZY);
			enemy.setTarget(null);
			enemy.checkOpponent();
			
			enemy.addBuff("MANTIS30B", time, 0f, string.Empty, BuffFinish);
		}
		yield return new WaitForSeconds(0f);
	}
	
	private void BuffFinish(Character ch, Buff buf){
		ch.currentColor =  (Color)new Color32(128,128,128,255);
		ch.model.renderer.material.color = ch.currentColor;
		ch.isAtkSameTag = false;
		ch.setAbnormalState(Character.ABNORMAL_NUM.NORMAL);
		ch.checkOpponent();
		
		Destroy(enemyBubbles[0]);
		enemyBubbles.RemoveAt(0);
	}
}
