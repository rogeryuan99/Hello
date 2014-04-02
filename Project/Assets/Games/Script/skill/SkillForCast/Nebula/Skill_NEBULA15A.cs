using UnityEngine;
using System.Collections;

public class Skill_NEBULA15A : SkillBase {
	private ArrayList objs;
	private Object shootEftPreb;
	private Object netEftPreb;
	
	public override IEnumerator Cast (ArrayList objs){
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Character character = caller.GetComponent<Character>();
		character.castSkill("Skill15A");
		yield return new WaitForSeconds(1.39f);
//		Time.timeScale = 0.002f;
//		int i =0;
//		while(1>0){
//			yield return new WaitForSeconds(0.002f);
//			Debug.LogError(++i);
//		}
		showShootEft();
		yield return new WaitForSeconds(0.12f);
		showNetEft();
		yield return new WaitForSeconds(0.5f);
		addBuff(character);
//		show

	}
	
	private void showShootEft(){
		GameObject caller = objs[1] as GameObject;
		Character nebula = caller.GetComponent<Character>();
		if(null == shootEftPreb){
			shootEftPreb = Resources.Load("eft/Nebula/SkillEft_NEBULA15A_ShootEft");
		}
		GameObject shootEft = Instantiate(shootEftPreb) as GameObject;
		bool isLeftSide = nebula.model.transform.localScale.x > 0;
		shootEft.transform.position = nebula.transform.position + new Vector3(isLeftSide ? -22:22, 328,0);
		shootEft.transform.localScale = new Vector3(isLeftSide ? 0.75f:0.75f, 0.75f, 0.75f);
	}
	
	private void showNetEft(){
		GameObject caller = objs[1] as GameObject;
		Character nebula = caller.GetComponent<Character>();
		if(null == netEftPreb){
			netEftPreb = Resources.Load("eft/Nebula/SkillEft_NEBULA15A_NetEft");
		}
		GameObject netEft = Instantiate(netEftPreb) as GameObject;
		bool isLeftSide = nebula.model.transform.localScale.x > 0;
		netEft.transform.position = nebula.transform.position + new Vector3(isLeftSide ? -2:2, 187,0);
		netEft.transform.localScale = new Vector3(isLeftSide ? 0.75f:-0.75f, 0.75f, 0.75f);
	}
	
	private void addBuff(Character character){
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("NEBULA15A");
		int time = skillDef.buffDurationTime;
		float mspdValue = ((Effect)(skillDef.buffEffectTable["mspd"])).num;
		if(character is Nebula){
			foreach(Hero hero in HeroMgr.heroHash.Values){
				hero.addBuff("SKILL_NEBULA15A",time,mspdValue/100f,BuffTypes.MSPD);
			}
		}else if(character is Ch2_Nebula){
			foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
				enemy.addBuff("SKILL_NEBULA15A",time,mspdValue/100f,BuffTypes.MSPD);
			}
		}
	}

}
