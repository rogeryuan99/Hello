using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
	protected Hashtable attributeHash;
	
	protected Hashtable characterHash;
	
	public GameObject lightRange;
	
	protected List<string> characterIDList = new List<string>();
	
	public enum GeneratorType
	{
		None,
		ShieldGenerator,
		LifeGenerator
	}
	
	protected GeneratorType type;
	
	public HPBar hpBar;
	
	protected int hpMax;
	protected int hp;
	
	public bool isEnabled = false;
	
	protected int radius;
	
	protected int per;
	
	public GameObject imageObj;
	
	public void init(Hashtable attributeHash, GeneratorType type, Hashtable characterHash, int time)
	{
		this.type = type;
		this.attributeHash = attributeHash;
		this.radius = (int)(this.attributeHash["AOERadius"]);
		this.hpMax = time;
		this.hp = this.hpMax;
		this.characterHash = characterHash;
		if(this.type == GeneratorType.LifeGenerator)
		{
			this.per = (int)((Effect)this.attributeHash["hp"]).num;
			startAddCharaterHp();
		}
		else if(this.type == GeneratorType.ShieldGenerator)
		{
			this.per = (int)((Effect)this.attributeHash["def_PHY"]).num;
			
		}
		if (null != lightRange){
			lightRange.transform.localScale *= this.radius/100;
		}
				
		hpBar.initBar(this.hpMax);
	
		startAutoConsumeTime();
		
		// startRotationImage();
		
		this.isEnabled = true;
	}
	
	protected void startAutoConsumeTime()
	{
		InvokeRepeating("autoConsumeTime",1,1);
	}
	
	protected void autoConsumeTime()
	{
		this.hp--;
		this.hpBar.ChangeHpTo(this.hp);
		if(this.hp <= 0)
		{
			restCharacterAttribute();
			this.characterIDList.Clear();
			
			if(IsInvoking("autoConsumeTime"))
			{
				CancelInvoke("autoConsumeTime");
			}
			
			if(IsInvoking("addCharaterHp"))
			{
				CancelInvoke("addCharaterHp");
			}
			
			if(IsInvoking("rotationImage"))
			{
				CancelInvoke("rotationImage");
			}
			
			Destroy(gameObject);
		}
	}
	
	void startRotationImage()
	{
		InvokeRepeating("rotationImage",0.1f,0.1f);
	}
	
	void rotationImage()
	{
		
		if(imageObj.gameObject.transform.localRotation.z >= 360.0f)
		{
			imageObj.gameObject.transform.localRotation = new Quaternion(0,0,0,0);
			
		}
		float rotationZ = imageObj.gameObject.transform.localRotation.z;
		rotationZ++;
		imageObj.gameObject.transform.localRotation = new Quaternion(
			imageObj.gameObject.transform.localRotation.x,
			imageObj.gameObject.transform.localRotation.y, 
			rotationZ,
			imageObj.gameObject.transform.localRotation.w );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isEnabled)
		{
			return;
		}
		changeCharacterAttribute();
		
	}
	
	protected void restCharacterAttribute()
	{
		if(this.type == GeneratorType.ShieldGenerator)
		{
			foreach(string charaterID in this.characterIDList)
			{
				Character character = this.characterHash[charaterID] as Character;
				character.resetDef();
				
				if(character is StarLord)
				{
					StarLord starlord = (StarLord)character;
					starlord.toNormalMode();
				}
			}
		}
	}
	
	protected void startAddCharaterHp()
	{
		InvokeRepeating("addCharaterHp",1,1);
	}
	
	protected void addCharaterHp()
	{
		foreach(string charaterID in this.characterIDList)
		{
			Character character = this.characterHash[charaterID] as Character;
			character.addHp((int)(character.realHp * (this.per / 100.0f + 1.0f)));
		}
	}
	
	protected void changeCharacterAttribute()
	{
		foreach(Character character in characterHash.Values)
		{
			Vector2 vc2 = character.gameObject.transform.position - transform.position;
			if(StaticData.isInOval(radius, radius*(BattleBg.actionBounds.size.y/BattleBg.actionBounds.size.x), vc2))
			{
				if(character.isDead)
				{
					if(this.type == GeneratorType.LifeGenerator && this.characterIDList.Contains(character.id))
					{
						character.resetDef();
					}
					continue;
				}
				if(character is StarLord)
				{
					HeroData heroD = character.data as HeroData;
					Hashtable passive1 =  heroD.getPSkillByID("STARLORD25");
					if(passive1 != null)
					{
						StarLord starlord = (StarLord)character;
						starlord.toGodMode();
					}
				}
				
				if(this.type == GeneratorType.ShieldGenerator && !this.characterIDList.Contains(character.id))
				{
					character.realDef.PHY +=  character.realDef.PHY * (this.per / 100.0f + 1.0f);
					this.characterIDList.Add(character.id);
				}
				else if(this.type == GeneratorType.LifeGenerator && !this.characterIDList.Contains(character.id))
				{
					this.characterIDList.Add(character.id);
				}
				
			}
			else
			{
				if(this.characterIDList.Contains(character.id))
				{
					if(character is StarLord)
					{
						StarLord starlord = (StarLord)character;
						starlord.toNormalMode();
					}
					this.characterIDList.Remove(character.id);
					if(this.type == GeneratorType.ShieldGenerator)
					{
						character.resetDef();
						
					}
					else if(this.type == GeneratorType.LifeGenerator)
					{
						
					}
				}
			}
		}
	}
}
