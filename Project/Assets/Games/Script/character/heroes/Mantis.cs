using UnityEngine;
using System.Collections;

public class Mantis : Hero
{

	public GameObject attackEft;
	
	public delegate void ParmsDelegate(Character character);
	public event ParmsDelegate SkillKeyFrameEvent;
	public event ParmsDelegate skill15AKeyFrameEventCallback;
	
	public event ParmsDelegate skill15BKeyFrameEventCallback;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 18;
	}
	
	public override void Start()
	{
		base.Start();
		
		pieceAnima.addFrameScript("Skill5B",28, skillKeyFrameEvent);
		pieceAnima.addFrameScript("Skill5B",33, skillKeyFrameEvent);
		pieceAnima.addFrameScript("Skill15A",23, skill15AKeyFrameEvent);
		pieceAnima.addFrameScript("Skill15B",19, skill15BKeyFrameEvent);
		pieceAnima.addFrameScript("Skill30A",21, skillKeyFrameEvent);
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("Skill5B",28);
		
		pieceAnima.removeFrameScript("Skill5B",33);
		pieceAnima.removeFrameScript("Skill15A",23);
		pieceAnima.removeFrameScript("Skill15B",19);
	}
	
	public void skill15BKeyFrameEvent(string s)
	{
		if(this.skill15BKeyFrameEventCallback != null)
		{
			this.skill15BKeyFrameEventCallback(this);
		}
	}
	
	public override string getTargetTagType ()
	{
		return "Player";
	}
	
	public void stopHeal ()
	{
		targetObj = null;
	}
	
	public void startHeal ( GameObject gameObj  )
	{
		Hero hero = gameObj.GetComponent<Hero>();
		startHeal(hero);
	}
	
	public void startHeal ( Hero hero  )
	{
		if(this.isDead)
		{
			return;	
		}
		
		state = ATK_STATE;
		targetObj = hero.gameObject;
		
		if(!hero.getIsDead())
		{
			if( !IsInvoking("healTarget") )
			{
				healTarget();
				CancelInvoke("healTarget");
				InvokeRepeating("healTarget", realAspd, realAspd);
			}
		}
	}
	
	public void skillKeyFrameEvent(string s)
	{
		if(SkillKeyFrameEvent != null)
		{
			SkillKeyFrameEvent(this);
		}
	}
	
	public void skill15AKeyFrameEvent(string s)
	{
		if(skill15AKeyFrameEventCallback != null)
		{
			skill15AKeyFrameEventCallback(this);
		}
	}

	public override void startCheckOpponent (){}
	protected override void moveTargetInAtkUpdate (){}
	protected override void autoSelectTarget(GameObject atkerObj){}
	
	private void healTarget ()
	{
		if( state != MOVE_STATE && state != CAST_STATE )
		{
			
			if(targetObj != null)
			{
				Hero hero = targetObj.GetComponent<Hero>();
				if(!hero.getIsDead())
				{
					toward(targetObj.transform.position);
					isPlayAtkAnim = true;
					playAnim("Attack");
				}
				else
				{
					healRandomHero();
				}
			}
		}
	}
	
	public override void moveToTarget (GameObject obj)
	{
		standby();
	}
	
	public void healRandomHero ()
	{
		if (HeroMgr.heroHash.Count > 1)
		{
			HeroMgr.getRandomHero();
		}
	}
	
	public override void standby ()
	{
		if(state == ATK_STATE)
		{
			CancelInvoke("healTarget");
		}
		state = STANDBY_STATE;
		doAnim("Stand");
	}

	
	protected override void atkAnimaScript (string s)
	{
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
			return;
		} 
		
		Character character = targetObj.GetComponent<Character>();
		character.changeStateColor(beHealColor);
		HeroData data = this.data as HeroData;
//		character.addHp((int)(realAtk.PHY * GetPassiveValue(data)));
		Hashtable psTable = data.passiveHash["MANTIS10B"] as Hashtable;
		int aoeRadius = (null != psTable && psTable.ContainsKey("AOERadius"))
					? (int)psTable["AOERadius"]
					: 0;
		StaticData.splashHeal(character, this, HeroMgr.heroHash.Values, (int)(realAtk.PHY * GetPassiveValue(data)), 0);
		psTable = data.passiveHash["MANTIS20B"] as Hashtable;
		if(null != psTable){
			int universal = (int)psTable["universal"];
			int buffTime = (int)psTable["universalTime"];
			splashHealBuff(character, this, HeroMgr.heroHash.Values, aoeRadius, buffTime, universal);
		}
		
		psTable = data.passiveHash["MANTIS25"] as Hashtable;
		if(null != psTable){
			int atk = (int)psTable["universal"];
			int buffTime = (int)psTable["universalTime"];
			splashDamageBuff(character, this, HeroMgr.heroHash.Values, aoeRadius, buffTime, atk);
		}
		
	}
	
	private float GetPassiveValue(HeroData data){
		Hashtable psTable = data.passiveHash["MANTIS10A"] as Hashtable;
		float v = (null != psTable && psTable.ContainsKey("universal"))
					? ((int)psTable["universal"] * 0.01f + 1f)
					: 1f;
		return v;
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
	
	private void splashHealBuff(Character centerChar, Character healer, ICollection clt, int aoeRadius, int durTime, float val)
	{
		ArrayList cltClone = new ArrayList(clt);
		foreach(Character character in cltClone)
		{
			Vector2 vc2 = centerChar.transform.position - character.transform.position;
			if((character == centerChar) || StaticData.isInOval(aoeRadius,aoeRadius, vc2) )
			{
				character.addBuff("Skill_MANTIS20B", durTime, character.realMaxHp*(val/100f), BuffTypes.HP);
			}
		}
		cltClone.Clear();
	}
	
	private void splashDamageBuff(Character centerChar, Character healer, ICollection clt, int aoeRadius, int durTime, float val)
	{
		ArrayList cltClone = new ArrayList(clt);
		foreach(Character character in cltClone)
		{
			Vector2 vc2 = centerChar.transform.position - character.transform.position;
			if((character == centerChar) || StaticData.isInOval(aoeRadius,aoeRadius, vc2) )
			{
				character.addBuff("Skill_MANTIS25", 1, val, BuffTypes.ATK_PHY);
			}
		}
		cltClone.Clear();
	}
}
