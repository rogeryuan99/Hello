using UnityEngine;
using System.Collections;

public class Skill_SKUNGE15A : SkillBase {

	private ArrayList parms;
	private Character character;
	private Object eftPrefab;
	
	public override IEnumerator Cast (ArrayList objs){
		parms = objs;
		LoadResources();
		
		character.castSkill("Skill15A");
		if(character is Skunge){
			Skunge skunge = character as Skunge;
			skunge.showSkill15AMusicHaloEftCallBack += showMusicHalo;
		}else if(character is Ch2_Skunge){
			Ch2_Skunge skunge = character as Ch2_Skunge;
			skunge.showSkill15AMusicHaloEftCallBack += showMusicHalo;
		}
		
		yield return new WaitForSeconds(0f);
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		character = caller.GetComponent<Character>();
		
		if (null == eftPrefab){
			eftPrefab = Resources.Load("eft/Skunge/SkillEft_SKUNGE15A_MusicHalo");
		}
	}
	
	private void showMusicHalo(Character c){
		if(character is Skunge){
			Skunge skunge = character as Skunge;
			skunge.showSkill15AMusicHaloEftCallBack -= showMusicHalo;
		}else if(character is Ch2_Skunge){
			Ch2_Skunge skunge = character as Ch2_Skunge;
			skunge.showSkill15AMusicHaloEftCallBack -= showMusicHalo;
		}
		
		StartCoroutine(delayCreateAnotherMusicHalo());
	}
	
	private IEnumerator delayCreateAnotherMusicHalo(){
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);		
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);		
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);
		createMusicHalo();
		yield return new WaitForSeconds(1f);
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);		
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);		
		createMusicHalo();		
		yield return new WaitForSeconds(0.2f);
		createMusicHalo();
	}
	
	private void createMusicHalo(){
		if(character.getIsDead()) return;
		showStunEft();
		
		GameObject haloEft = Instantiate(eftPrefab) as GameObject;
		haloEft.transform.parent = character.transform;
		haloEft.transform.localPosition = new Vector3(0,200,0);
		haloEft.transform.localScale = new Vector3(4,4,1);
	}
	
	private void showStunEft(){
		if(character.getIsDead()) return;
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("SKUNGE15A");
		int buffTime = (int)def.buffDurationTime;
		State s = new State(buffTime, null);
		
		if(character is Skunge){
			foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
				if(!enemy.getIsDead() && !enemy.isAbnormalStateActive(Character.ABNORMAL_NUM.STUN)){
					enemy.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
				}
			}
		}else if(character is Ch2_Skunge){
			foreach(Hero hero in HeroMgr.heroHash.Values){
				if(!hero.getIsDead() && !hero.isAbnormalStateActive(Character.ABNORMAL_NUM.STUN)){
					hero.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
				}
			}
		}
	}
	
}
