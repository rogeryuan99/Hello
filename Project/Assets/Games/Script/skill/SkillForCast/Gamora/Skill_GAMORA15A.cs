using UnityEngine;
using System.Collections;

public class Skill_GAMORA15A : SkillBase
{
	protected ArrayList objs = null;
	
	protected GameObject bulletPrb;
	
	protected GameObject damageEftPrb;
	protected GameObject damageEft;
	
	protected int attackCount;
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		
		gamora.toward(target.transform.position);
		gamora.castSkill("GAMORA15A");
		
		attackCount = 0;
		
		gamora.gamora15AAttack1Callback -= attack;
		gamora.gamora15AAttack2Callback -= attack;
		gamora.gamora15AAttack3Callback -= attack;
		
		
		gamora.gamora15AAttack1Callback += attack;
		gamora.gamora15AAttack2Callback += attack;
		gamora.gamora15AAttack3Callback += attack;
		
		yield return new WaitForSeconds(0.3f);
		
		MusicManager.playEffectMusic("SFX_Gamora_PistolShoot_1a");
		
	}
	
	public void attack()
	{
		GameObject scene = this.objs[0] as GameObject;
		GameObject caller = this.objs[1] as GameObject;
		GameObject targetObj = this.objs[2] as GameObject;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		
		if(attackCount == 0)
		{
			gamora.gamora15AAttack1Callback -= attack;
		}
		else if(attackCount == 1)
		{
			gamora.gamora15AAttack2Callback -= attack;
		}
		else if(attackCount == 2)
		{
			gamora.gamora15AAttack3Callback -= attack;
		}
		
		
		attackCount++;
		
		if(targetObj == null)
		{
			gamora.gamora15AAttack1Callback -= attack;
			gamora.gamora15AAttack2Callback -= attack;
			gamora.gamora15AAttack3Callback -= attack;
			return;
		}
		
		Character target = targetObj.GetComponent<Character>();
		
		if(target.getIsDead())
		{
			gamora.gamora15AAttack1Callback -= attack;
			gamora.gamora15AAttack2Callback -= attack;
			gamora.gamora15AAttack3Callback -= attack;
			return;
		}
		
		Vector3 vc3 = targetObj.transform.position + new Vector3(0,70,0);
		
		if(target.model.transform.localScale.x > 0)
		{
			vc3 += new Vector3(50,0,0);
		}
		else
		{
			vc3 -= new Vector3(50,0,0);
//			damageEft.transform.localPosition = 
		}
		Vector3 createPt;
		if(gamora.model.transform.localScale.x > 0)
		{
//			print("right");
			createPt = gamora.transform.position + new Vector3(100, 113, 0);
		}
		else
		{
//			print("left");
			createPt = gamora.transform.position + new Vector3(-100, 113,0);
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
			bulletPrb = Resources.Load("eft/Gamora/SkillEft_GAMORA15A_Bullet") as GameObject;
		}
		
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, caller.transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		bltObj.transform.position += new Vector3(0,0,targetObj.transform.position.z-10f);
		
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
	
	protected void removeBullet (GameObject bltObj)
	{
		if(damageEftPrb == null)
		{
			damageEftPrb = Resources.Load("eft/Gamora/SkillEft_GAMORA15A_DamageEft") as GameObject;
		}
		damageEft = Instantiate(damageEftPrb, bltObj.transform.position, transform.rotation) as GameObject;
		Destroy(bltObj);
		
		GameObject scene = this.objs[0] as GameObject;
		GameObject caller = this.objs[1] as GameObject;
		GameObject targetObj = this.objs[2] as GameObject;
		
		Character target = targetObj.GetComponent<Character>();
		Gamora gamora = caller.GetComponent<Gamora>();
		if(target.model.transform.localScale.x > 0)
		{
			damageEft.transform.localScale = new Vector3(-damageEft.transform.localScale.x, damageEft.transform.localScale.y, damageEft.transform.localScale.z);
		}
		
		Hashtable tempNumber = SkillLib.instance.getSkillDefBySkillID("GAMORA15A").activeEffectTable; // GAMORA5A
		
		float tempAtkPer = ((Effect)tempNumber["atk_PHY"]).num;
		target.realDamage(target.getSkillDamageValue(gamora.realAtk, tempAtkPer) / 3);
	}
}
