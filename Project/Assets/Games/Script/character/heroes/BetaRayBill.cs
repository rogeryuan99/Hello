using UnityEngine;
using System.Collections;

public class BetaRayBill : Hero
{

	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public ParmsDelegate showSkill15BBlastEftCallback;
	public bool isTrigger15A;
	public bool isTrigger30A;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	
	public override void Start()
	{
		base.Start();
		
		pieceAnima.addFrameScript("Skill15B_a", 15, showSkill15BBlastEft);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill15B_a", 15);
	}
	
	private void showSkill15BBlastEft(string s){
		if(showSkill15BBlastEftCallback != null){
			showSkill15BBlastEftCallback(this);
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
			return;
		} 
		
//		MusicManager.playEffectMusic("SFX_Gamora_Basic_1a");
		Vector3 eft;
		if(model.transform.localScale.x > 0)
		{
			eft = transform.position + new Vector3(200,40,-50);
		}else{
			eft = transform.position + new Vector3(-200,40,-50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		Character character = targetObj.GetComponent<Character>();
		if(isTrigger15A){
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL15A");
			Hashtable tempNumber = skillDef.activeEffectTable;
			int aoeRadius = (int)tempNumber["AOERadius"];
			StaticData.splashDamage(character, this, EnemyMgr.enemyHash.Values, character.realAtk, aoeRadius);
		}
		if(isTrigger30A){
			SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL30A");
			Hashtable tempNumber = skillDef.activeEffectTable;
			int chance = (int)tempNumber["chance"];
			int time = skillDef.skillDurationTime;
			if(StaticData.computeChance(chance,100)){
				character.addAbnormalState(new State(time, null), Character.ABNORMAL_NUM.LAYDOWN);				
			}
		}
		base.atkAnimaScript("");
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
}
