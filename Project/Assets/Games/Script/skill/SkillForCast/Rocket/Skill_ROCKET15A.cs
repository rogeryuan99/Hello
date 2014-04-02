using UnityEngine;
using System.Collections;

public class Skill_ROCKET15A : SkillBase
{
	protected ArrayList objs;
	
	protected GameObject attackEftPrb;
	
	protected GameObject bulletPrb;
	
	protected GameObject damageEftPrb;
	
	public override IEnumerator Cast(ArrayList objs)
	{	
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		
		rocket.showAttackEftCallback += showAttackEft;
		rocket.shootBulletCallback += shootBullet;
		
		rocket.toward(target.transform.position);
		
		rocket.castSkill("Skill15A");
		
		yield return new WaitForSeconds(0.2f);
		
		MusicManager.playEffectMusic("SFX_Rocket_SuppressingFire_1a");
	}
	
	public void showAttackEft()
	{
		
		GameObject caller = objs[1] as GameObject;
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.showAttackEftCallback -= showAttackEft;
		
		if(attackEftPrb == null)
		{
			attackEftPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET15A_AttackEft") as GameObject;
		}
		
		GameObject attackEft = Instantiate(attackEftPrb) as GameObject;
		
		attackEft.transform.position = rocket.transform.position;
		
		if(rocket.model.transform.localScale.x > 0)
		{
			attackEft.transform.position += new Vector3(92,82,0);
			attackEft.transform.localScale = new Vector3(0.5f,0.5f,1);
		}
		else
		{
			attackEft.transform.position += new Vector3(-92,82,0);
			attackEft.transform.localScale = new Vector3(-0.5f,0.5f,1);
		}
	}
	
	public void shootBullet()
	{
		GameObject caller = objs[1] as GameObject;
		GameObject targetObj = objs[2] as GameObject;
		
		Rocket rocket = caller.GetComponent<Rocket>();
		rocket.shootBulletCallback -= shootBullet;
		
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
		Vector3 createPt;
		
		if(bulletPrb == null)
		{
			bulletPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET15A_Bullet") as GameObject;
		}
		
		GameObject bltObj = Instantiate(bulletPrb) as GameObject;
		bltObj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		
		if(rocket.model.transform.localScale.x > 0)
		{
			// right
			createPt = rocket.transform.position + new Vector3(100,60,-50);
			
		}
		else
		{
			// left
			createPt = rocket.transform.position + new Vector3(-100,60,-50);

			
		}
		shootBullet(bltObj, createPt, vc3);
	}
	
	protected void shootBullet (GameObject bltObj, Vector3 creatVc3 ,   Vector3 endVc3  )
	{
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		
		
		float deg = (angle*360)/(2*Mathf.PI);
		
		bltObj.transform.position = creatVc3;
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));

		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",bltObj}});
	}
	
	protected void removeBullet (GameObject bltObj)
	{
		GameObject target = objs[2] as GameObject;
		
		if(damageEftPrb == null)
		{
			damageEftPrb = Resources.Load("eft/Rocket/SkillEft_ROCKET15A_DamageEft") as GameObject;
		}
		
		
		GameObject damageEft = Instantiate(damageEftPrb) as GameObject;
		damageEft.transform.position = bltObj.transform.position;
		Destroy(bltObj);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("ROCKET15A");
		
		int stateTime = skillDef.skillDurationTime;
		int aoeRadius= (int)skillDef.activeEffectTable["AOERadius"]; 
		
		Enemy enemy = target.GetComponent<Enemy>();
		
		if(!enemy.isDead)
		{
			State s = new State(stateTime, null);
			enemy.addAbnormalState(s, Character.ABNORMAL_NUM.FREEZE);
//			enemy.freezeWithSeconds();
		}		
		
		foreach(Enemy otherEnemy in EnemyMgr.enemyHash.Values)
		{
			if(otherEnemy.getID() != enemy.getID())
			{
				Vector2 vc2 = otherEnemy.transform.position - enemy.transform.position;
				if(StaticData.isInOval(aoeRadius , aoeRadius, vc2) )
				{
					if(otherEnemy.isDead)
					{
						continue;
					}
//					otherEnemy.freezeWithSeconds();
					State s = new State(stateTime, null);
					otherEnemy.addAbnormalState(s, Character.ABNORMAL_NUM.FREEZE);
				}
			}
		}
	}
}
