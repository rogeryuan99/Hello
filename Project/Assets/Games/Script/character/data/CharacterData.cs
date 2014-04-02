using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CharacterData
{
	public int maxHp;
	public float moveSpeed;
	public float attackSpeed;
	public float attackRange = 150.0f;
	
	public Vector6 attack;
	public Vector6 defense;
	
//	public float atk_PHY=0;
//	public float atk_IMP=0;
//	public float atk_PSY=0;
//	public float atk_EXP=0;
//	public float atk_ENG=0;
//	public float atk_MAG=0;
//	
//	public float def_PHY=0;
//	public float def_IMP=0;
//	public float def_PSY=0;
//	public float def_EXP=0;
//	public float def_ENG=0;
//	public float def_MAG=0;
	
//	public float attack;
//	public float defense;
	
	public string type;
	
	public int rewardSilver= 30;
	public int rewardExp = 30;
	
	public bool  isDead=false;
	
// delete by why 2014.2.7	
//	public int criticalStk;//Critical Strike chance
//	public int evade;
//	public int strike;
	
	public DataModifier itemMult;
	public DataModifier itemAdd;
	public DataModifier skillMult;
	public DataModifier skillAdd;
	
	public CharacterData ()
	{
		resetEft();
	}
	
	public void resetEft ()
	{
		/*eAttack = 0;
		eMaxHp  = 0;
		eDefense= 0;
		eAtkSpd = 0;
		eMoveSpd= 0;
		eCrtlStk= 0;*/
		
		itemAdd = new DataModifier();
		itemMult  = new DataModifier();
		
		skillAdd = new DataModifier();
		skillMult = new DataModifier(); 
	}
	
	//{type:"type1", hp:100, mspd:1.2f, aspd:1, atk:1, def:1, dropCoins:10, dropExp:10, cstk:8, evd:10, stk:25}
	public CharacterData ( Hashtable jsonHash  )
	{
		type  = jsonHash["type"].ToString();
		maxHp = int.Parse(jsonHash["hp"].ToString());
		moveSpeed  = float.Parse(jsonHash["mspd"].ToString()) * Utils.characterScale;
		attackSpeed = float.Parse(jsonHash["aspd"].ToString());
		attack  = jsonHash["atk"] as Vector6;//Vector6.createWithHashtable(jsonHash, "atk");
		defense = jsonHash["def"] as Vector6;//Vector6.createWithHashtable(jsonHash, "def");
		rewardSilver  = int.Parse(jsonHash["rewardSilver"].ToString());
		rewardExp = int.Parse(jsonHash["rewardExp"].ToString());
		
		// delete by why 2014.2.7	
//		criticalStk  = int.Parse(jsonHash["cstk"].ToString());
//		evade = int.Parse(jsonHash["evd"].ToString());
//		strike  = int.Parse(jsonHash["stk"].ToString());
		
		resetEft();
	}
}
