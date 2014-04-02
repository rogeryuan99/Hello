using UnityEngine;
using System.Collections;

public class Skill_STARLORD30B : SkillBase
{
	
	protected GameObject bulletPrb;
	
	
	protected GameObject explodePrb;
	
	protected ArrayList objs;
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		MusicManager.playEffectMusic("SFX_StarLord_Orbital_Bombardment_1a");
		
		StarLord heroDoc = caller.GetComponent<StarLord>();
		
		heroDoc.skillAnimaEventCallback += attack;
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("Skill30B");
		
		yield return new WaitForSeconds(0.5f);
		
		
	}
	
	public void attack(StarLord starLord)
	{
		GameObject scene = this.objs[0] as GameObject;
		GameObject caller = this.objs[1] as GameObject;
		GameObject targetObj = this.objs[2] as GameObject;
		
		starLord.skillAnimaEventCallback -= attack;
		
		if(targetObj == null)
		{
			
			return;
		}
		
		Character target = targetObj.GetComponent<Character>();
		
		if(target.getIsDead())
		{
			
			return;
		}
		
		Vector3 vc3 = targetObj.transform.position + new Vector3(0,70,0);
		
//		if(target.model.transform.localScale.x > 0)
//		{
//			vc3 += new Vector3(50,0,0);
//		}
//		else
//		{
//			vc3 -= new Vector3(50,0,0);
////			damageEft.transform.localPosition = 
//		}
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
		shootBullet(createPt, vc3);
	}
	
	protected void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject targetObj = objs[2] as GameObject;
		
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		if(bulletPrb == null)
		{
			bulletPrb = Resources.Load("eft/StarLord/SkillEft_STARLORD30B_Bullet") as GameObject;
		}
		
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, caller.transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		iTween.MoveTo(bltObj,new Hashtable()
		{
			{"x",endVc3.x},
			{"y",endVc3.y},
			{"speed",1500},
			{"easetype","linear"},
			{"oncomplete","removeBullet"},
			{"oncompletetarget",gameObject},
			{"oncompleteparams",bltObj}
		});
	}
	
	public void removeBullet(GameObject bltObj)
	{
		Destroy(bltObj);
		
		if(explodePrb == null)
		{
			explodePrb = Resources.Load("eft/StarLord/SkillEft_STARLORD30B_Explode") as GameObject;
		}
		
		GameObject caller = this.objs[1] as GameObject;
		GameObject targetObj = objs[2] as GameObject;
		
		GameObject explode = Instantiate(explodePrb) as GameObject;
		explode.transform.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y + 135, -100);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("STARLORD30B");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		StarLord heroDoc = caller.GetComponent<StarLord>();
		
		int tempRadius = (int)tempNumber["AOERadius"];
		int tempAtkPer = (int)((Effect)tempNumber["atk_PHY"]).num;
		
		Character enemy = targetObj.GetComponent<Character>();
		
		enemy.realDamage(enemy.getSkillDamageValue(heroDoc.realAtk, tempAtkPer));
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		foreach(Enemy otherEnemy in enemyList)
		{
			if(otherEnemy.getID() != enemy.getID())
			{
				Vector2 vc2 = otherEnemy.transform.position - enemy.transform.position;
				if( StaticData.isInOval(tempRadius, tempRadius, vc2) )
				{
					if(otherEnemy.isDead)
					{
						continue;
					}
					otherEnemy.realDamage(otherEnemy.getSkillDamageValue(heroDoc.realAtk, tempAtkPer));						
				}
			}
		}
	}
}
