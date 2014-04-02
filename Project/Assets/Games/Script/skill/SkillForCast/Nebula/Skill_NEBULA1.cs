using UnityEngine;
using System.Collections;

public class Skill_NEBULA1 : SkillBase {
	private Character caller;
	private GameObject fireEft;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		this.caller = caller.GetComponent<Character>();
		
		this.caller.castSkill("SkillA");
		this.caller.toward(this.caller.targetObj.transform.position);
		if(this.caller is Nebula){
			Nebula nebula = this.caller as Nebula;
			nebula.showSkill1BulletEftCallBack += showBulletEft;
		}else if(this.caller is Ch2_Nebula){
			Ch2_Nebula nebula = this.caller as Ch2_Nebula;
			nebula.showSkill1BulletEftCallBack += showBulletEft;
		}
		
		yield return new WaitForSeconds(0f);
	}
	
	private void showBulletEft(Character c){
		if(caller is Nebula){
			Nebula nebula = caller as Nebula;
			nebula.showSkill1BulletEftCallBack -= showBulletEft;
			nebula.showSkill1FireEftCallBack += showFireEft;
		}else if(caller is Ch2_Nebula){
			Ch2_Nebula nebula = caller as Ch2_Nebula;
			nebula.showSkill1BulletEftCallBack -= showBulletEft;
			nebula.showSkill1FireEftCallBack += showFireEft;
		}
		
		GameObject bulletEftPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA1_BulletEft") as GameObject;
		GameObject bulletEft = Instantiate(bulletEftPrefab) as GameObject;
		bulletEft.transform.parent = c.transform;
		if(c.model.transform.localScale.x > 0){
			bulletEft.transform.localPosition = new Vector3(850,470,0);
			bulletEft.transform.localScale = new Vector3(3,3,1);
		}else{
			bulletEft.transform.localPosition = new Vector3(-850,470,0);
			bulletEft.transform.localScale = new Vector3(-3,3,1);
		}
	}
	
	private void showFireEft(Character c){
		if(caller is Nebula){
			Nebula nebula = caller as Nebula;
			nebula.showSkill1FireEftCallBack -= showFireEft;
		}else if(caller is Ch2_Nebula){
			Ch2_Nebula nebula = caller as Ch2_Nebula;
			nebula.showSkill1FireEftCallBack -= showFireEft;
		}
		
		GameObject fireEftPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA1_FireEft") as GameObject;
		if(fireEft == null) fireEft = Instantiate(fireEftPrefab) as GameObject;
		Character target = c.targetObj.GetComponent<Character>();
		fireEft.transform.parent = target.transform;
		fireEft.transform.localPosition = new Vector3(0,300,0);
		if(target.model.transform.localScale.x > 0){
			fireEft.transform.localScale = new Vector3(6,6,1);
		}else{
			fireEft.transform.localScale = new Vector3(-6,6,1);
		}
		
		SkillDef skillDef =  SkillLib.instance.getSkillDefBySkillID("NEBULA1");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		int fireBlastDamage = c.getSkillDamageValue(c.realAtk, tempAtkPer);
		int time = (int)skillDef.buffDurationTime;
		
		State state = new State(time, DestroyFireEft);
		target.addAbnormalState(state, Character.ABNORMAL_NUM.FIRE);
		target.realDamage(fireBlastDamage);
	}
	
	private void DestroyFireEft(State self, Character charater){
		Destroy(fireEft);
	}
}
