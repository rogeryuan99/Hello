using UnityEngine;
using System.Collections;

public class Skill_BETARAYBILL1 : SkillBase {
	
	private ArrayList parms;
	private Object gapPrefab;
	private Object hitEffectPrefab;
	private Object weaponHaloPrefab;
	private Character bill;
	private Character enemy;
	private float damage;
	private bool isTowardRight;
	
	
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		LoadResources();
		
		if(Vector3.Distance(bill.transform.position, enemy.transform.position) > bill.data.attackRange + 10.0f)
		{
			bill.moveToTarget(enemy.gameObject);
			BattleObjects.skHero.PushSkillIdToContainer("BETARAYBILL1");// BETARAYBILL1
			yield break;
		}
		
		bill.toward(enemy.transform.position);
		bill.castSkill("SkillA");
		isTowardRight = bill.model.transform.localScale.x > 0;
		yield return new WaitForSeconds(.47f);
		CreateHalo();
		yield return new WaitForSeconds(0.88f);
		
		ShakeCamera();
		CreateGap();
		CreateHitEffect();
		DamageEnemy();
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		bill  = caller.GetComponent<Character>();
		enemy = target.GetComponent<Character>();
		
		if (null == gapPrefab){
			gapPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL1_Gap");
		}
		if (null == hitEffectPrefab){
			hitEffectPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL1_HitEffect");
		}
		if (null == weaponHaloPrefab){
			weaponHaloPrefab = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL1_WeaponHalo");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("BETARAYBILL1");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
	}
	
	private void CreateHalo(){
		GameObject halo = Instantiate(weaponHaloPrefab, bill.transform.position, transform.rotation) as GameObject;
		halo.transform.position += new Vector3((isTowardRight? -0f: 0f), 245f, 0f);
		halo.transform.localScale = new Vector3((isTowardRight? 1f: -1f), 1f, 1f);
	}
	
	private void ShakeCamera(){
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,60,0), 0.3f, 0f));
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
	}
	
	private void CreateGap(){
		GameObject gap = Instantiate(gapPrefab, bill.transform.position, transform.rotation) as GameObject;
		gap.transform.position += new Vector3((isTowardRight? 120f: -120f), 0f, 1f);
	}
	
	private void CreateHitEffect(){
		GameObject eft = Instantiate(hitEffectPrefab, bill.transform.position, transform.rotation) as GameObject;
		eft.transform.position += new Vector3((isTowardRight? 140f: -140f), 40f, -1f);
		eft.transform.localScale = new Vector3((isTowardRight? 1f: -1f), 1f, 1f);
	}
	
	private void DamageEnemy(){
		Debug.LogError(string.Format("** DamageEnemy: {0}", enemy.getSkillDamageValue(bill.realAtk, damage)));
		enemy.realDamage(enemy.getSkillDamageValue(bill.realAtk, damage));
	}
}
