using UnityEngine;
using System.Collections;

public class Skill_MANTIS15B : SkillBase
{
	protected GameObject skillEft_MANTIS15BPrb;
	protected GameObject skillEft_MANTIS15B;
	
	
	protected GameObject skillEft_MANTIS15B_DamagePrb;
	
	protected GameObject bulletPrb;
	
	int skillDurationTime = 0;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Mantis mantis = caller.GetComponent<Mantis>();
		
		SkillDef skillDef = SkillLib.instance.allHeroSkillHash["MANTIS15B"] as SkillDef;
		skillDurationTime = skillDef.skillDurationTime;
		
		mantis.castSkill("Skill15B");
		mantis.skill15BKeyFrameEventCallback += showEft;
		Debug.LogError("mantis15B");
		
		yield return new WaitForSeconds(0.25f);
		
		MusicManager.playEffectMusic("SFX_Mantis_Force_of_Will_1a");
		
	}
	
	public void showEft(Character character)
	{
		Mantis mantis = character as Mantis;
		
		mantis.skill15BKeyFrameEventCallback -= showEft;
		if(skillEft_MANTIS15BPrb == null)
		{
			skillEft_MANTIS15BPrb = Resources.Load("eft/Mantis/SkillEft_MANTIS15B") as GameObject;
		}
		
		Destroy(skillEft_MANTIS15B);
		
		skillEft_MANTIS15B = Instantiate(skillEft_MANTIS15BPrb) as GameObject;
		
		float x = 0;
		if(mantis.model.transform.localScale.x > 0)
		{
			x = -34;
		}
		else
		{
			x = 34;
		}
		
		skillEft_MANTIS15B.transform.position = character.transform.position + new Vector3(x, 261, 0);
		skillEft_MANTIS15B.transform.localScale = new Vector3(0.164f, 0.164f, 1);

		StartCoroutine(showBullet(skillEft_MANTIS15B.transform.position));
	}
	
	public IEnumerator showBullet(Vector3 creatVc3)
	{
		yield return new WaitForSeconds(0.5f);
		foreach(Enemy enemy in EnemyMgr.enemyHash.Values)
		{
			if(enemy.getIsDead())
			{
				continue;
			}
			shootBullet(creatVc3, enemy.transform.position + new Vector3(0,70,0), enemy as Character);
		}
	}
	
	protected void shootBullet ( Vector3 creatVc3 ,   Vector3 endVc3 , Character charater )
	{
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
		
		if(bulletPrb == null)
		{
			bulletPrb = Resources.Load("eft/Mantis/SkillEft_MANTIS15B_Bullet") as GameObject;
		}
		
		GameObject bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation) as GameObject;
		
		float deg = (angle*360)/(2*Mathf.PI);
		bltObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		ArrayList parameters = new ArrayList();
		parameters.Add(bltObj);
		parameters.Add(charater);
		
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
		iTween.MoveTo(bltObj,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{ "oncompletetarget",gameObject},{ "oncompleteparams",parameters}});
	}
	
	protected Hashtable damageEftHash = new Hashtable(); // key characterID value GameObject skillEft_MANTIS15B_Damage
	
	
	protected void removeBullet (ArrayList parameters)
	{
		GameObject bltObj = parameters[0] as GameObject;
		Character character = parameters[1] as Character;
		
		parameters.Clear();
		
		if(skillEft_MANTIS15B_DamagePrb == null)
		{
			skillEft_MANTIS15B_DamagePrb = Resources.Load("eft/Mantis/skillEft_MANTIS15B_Damage") as GameObject;;
		}
		
		GameObject skillEft_MANTIS15B_Damage = Instantiate(skillEft_MANTIS15B_DamagePrb) as GameObject;
		
		skillEft_MANTIS15B_Damage.transform.parent = character.transform;
		skillEft_MANTIS15B_Damage.transform.localPosition = new Vector3(0, 240, -1);
		skillEft_MANTIS15B_Damage.transform.localScale = new Vector3(8,8,1);
		
		damageEftHash[character.getID()] = skillEft_MANTIS15B_Damage;
		
//		character.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
			
		Destroy(bltObj);
		
//		character.stunWithSeconds();
		
		State s= new State(skillDurationTime, null);
			character.addAbnormalState(s, Character.ABNORMAL_NUM.STUN);
	}
	
	public void DestroySkillEft(State state, Character character)
	{
//		character.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		
		GameObject skillEft_MANTIS15B_Damage = damageEftHash[character.getID()] as GameObject;
		Destroy(skillEft_MANTIS15B_Damage);
		
		damageEftHash.Remove(character.getID());
	}
}
