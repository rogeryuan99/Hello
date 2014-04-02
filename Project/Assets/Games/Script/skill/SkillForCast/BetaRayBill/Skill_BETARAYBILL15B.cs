using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL15B : SkillBase {
	private BetaRayBill bill;
	private Character enemy;

	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		bill = caller.GetComponent<BetaRayBill>();
		enemy = target.GetComponent<Character>();
		
		bill.toward(enemy.transform.position);
		bill.castSkill("Skill15B_a");
		bill.showSkill15BBlastEftCallback += showBlastEft;
		
		yield return new WaitForSeconds(0f);	
	}
	
	private void showBlastEft(Character c){
		PieceAnimation anim = bill.model.GetComponent<PieceAnimation>();
		anim.pauseAnima();
		
		GameObject blastEftPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL15B_BlastEft") as GameObject;
		GameObject blastEft = Instantiate(blastEftPrefab) as GameObject;
		blastEft.transform.parent = bill.transform;
		if(bill.model.transform.localScale.x > 0){
			blastEft.transform.localPosition = new Vector3(-315,350,1);
			blastEft.transform.localScale = new Vector3(3,3,1);
		}else{
			blastEft.transform.localPosition = new Vector3(315,350,1);
			blastEft.transform.localScale = new Vector3(-3,3,1);	
		}
		
		float distanceX = (bill.model.transform.localScale.x > 0)? -150 : 150;
		iTween.MoveTo(bill.gameObject, new Hashtable(){
			{"x", enemy.transform.position.x + distanceX},
			{"y", enemy.transform.position.y},
			{"time",  0.2f},
			{"easeType", "liner"}
		});
		StartCoroutine(delayBlastFinish(blastEft));
	}
	
	private IEnumerator delayBlastFinish(GameObject blastEft){
		yield return new WaitForSeconds(0.2f);
		Destroy(blastEft);
		bill.castSkill("Skill15B_b");
		
		GameObject hitEftPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL15B_HitEft") as GameObject;
		GameObject hitEft = Instantiate(hitEftPrefab) as GameObject;
		hitEft.transform.parent = enemy.transform;
		hitEft.transform.localPosition = new Vector3(0,400,0);
		if(enemy.model.transform.localScale.x > 0){
			hitEft.transform.localScale = new Vector3(-3,3,1);	
		}else{
			hitEft.transform.localScale = new Vector3(3,3,1);	
		}
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL15B");
		int tempNum = (int)((Effect)skillDef.activeEffectTable["atk_PHY"]).num;
		int buffTime = skillDef.buffDurationTime;
		int chanceValue = skillDef.skillDurationTime;
		int damage = enemy.getSkillDamageValue(bill.realAtk,tempNum);
		enemy.realDamage(damage);
		if(StaticData.computeChance(chanceValue,100)){
			State s = new State(buffTime, null);
			enemy.addAbnormalState(s,Character.ABNORMAL_NUM.STUN);
		}
	}
}
