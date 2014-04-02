using UnityEngine;
using System.Collections;

public class Skill_STARLORD15A : SkillBase
{
	public ArrayList objs;
	
	public GameObject smokeEftPrb;
	
	public GameObject bulletPrb;
	
	public GameObject icePrb;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		
		this.objs = objs;
		
		StarLord heroDoc = caller.GetComponent<StarLord>();
		
		heroDoc.toward(Vector3.zero);
		heroDoc.castSkill("Skill15A");
		heroDoc.skillAnimaEventCallback += attack;
		
		yield return new WaitForSeconds(0.45f);
		
		MusicManager.playEffectMusic("SFX_StarLord_IceStorm_1a");
	}
	
	public void attack(StarLord starLord)
	{
		
		GameObject scene = this.objs[0] as GameObject;
		GameObject caller = this.objs[1] as GameObject;
		GameObject targetObj = this.objs[2] as GameObject;
		
		starLord.skillAnimaEventCallback -= attack;
		
		Vector3 createPt;
		if(starLord.model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = starLord.transform.position + new Vector3(100, 113, 0);
		}
		else
		{
//			print("left");
			createPt = starLord.transform.position + new Vector3(-100, 113,0);
		}
		createPt = new Vector3(createPt.x, createPt.y, 0);
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		Vector3 vc3 = Vector3.one;
		foreach(Enemy enemy in enemyList)
		{
			if(enemy.isDead)
			{
				continue;
			}
			vc3 = enemy.transform.position + new Vector3(0,70,0);
		
		
			shootBullet(createPt, vc3, enemy.gameObject);
		}
		
		
		
		starLord.skillAnimaEventCallback += showSmokeEft;
	}
	
	protected void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3, GameObject targetObj)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		if(bulletPrb == null)
		{
			bulletPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD15A_Bullet") as GameObject;
		}
		
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, caller.transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		ArrayList prams = new ArrayList();
		
		prams.Add(bltObj);
		prams.Add(targetObj);
		prams.Add(caller);
		
		iTween.MoveTo(bltObj,new Hashtable()
		{
			{"x",endVc3.x},
			{"y",endVc3.y},
			{"speed",1500},
			{"easetype","linear"},
			{"oncomplete","removeBullet"},
			{"oncompletetarget",gameObject},
			{"oncompleteparams",prams}
		});
	}
	
	public void removeBullet(ArrayList prams)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		GameObject bltObj = prams[0] as GameObject;
		GameObject targetObj = prams[1] as GameObject;
		
		Destroy(bltObj);
		if(targetObj == null)
		{
			return;
		}
		if(icePrb == null)
		{
			icePrb = Resources.Load("eft/StarLord/SkillEft_STARLORD15A_Ice") as GameObject;
		}
		
		GameObject ice = Instantiate(icePrb) as GameObject;
		ice.transform.position = targetObj.transform.position + new Vector3(0, 80, targetObj.transform.position.z - (targetObj.transform.position.z + 100));
		
		StarLord heroDoc = (prams[2] as GameObject).GetComponent<StarLord>();
		
		Character c = targetObj.GetComponent<Character>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD15A");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		int tempAtkPer = (int)((Effect)tempNumber["atk_PHY"]).num;
		
		c.realDamage(c.getSkillDamageValue(heroDoc.realAtk, tempAtkPer));
	}
	
	public void showSmokeEft(StarLord starLord)
	{
		starLord.skillAnimaEventCallback -= showSmokeEft;
		
		if(smokeEftPrb == null)
		{
			smokeEftPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD15A_Smoke") as GameObject;
			
		}
		
		GameObject smokeEft1 = Instantiate(smokeEftPrb) as GameObject;
		GameObject smokeEft2 = Instantiate(smokeEftPrb) as GameObject;
		
		if(starLord.model.transform.localScale.x > 0)
		{
			smokeEft1.transform.position = starLord.transform.position + new Vector3(50, 131, 0);
			smokeEft2.transform.position = starLord.transform.position + new Vector3(0, 131, 0);
		}
		else
		{
			smokeEft1.transform.localScale = new Vector3(-1, 1, 0);
			smokeEft2.transform.localScale = new Vector3(-1, 1, 0);
			smokeEft1.transform.position = starLord.transform.position + new Vector3(-50, 131, 0);
			smokeEft2.transform.position = starLord.transform.position + new Vector3(0, 131, 0);
		}
		
		
	}
}
