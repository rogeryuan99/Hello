using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDef
{
	public string id;
	
	public bool isPassive;
	
	public string name;
	
	public string target;
	
	public string description;
	
	public int unlockLv;
	
	public long learnTime;
	
	public string funcName;
	
	public int coolDown;
	
	public Hashtable activeEffectTable; //activeEffectTable
	
	public Hashtable passiveEffectTable;  //passiveEffectTable;
	
	public Hashtable buffEffectTable;//buffEffectTable;
	
//	public List<Effect> passiveEffectList = new List<Effect>();
//	
//	public List<Effect> buffEffectList = new List<Effect>();
	
	public int buffDurationTime = 0;
	public int skillDurationTime = 0;
	
	public int silver = 0;
	public int gold = 0;
	public int commandPoints = 0;
	
	public string comboPartners = string.Empty;
	public string comboPartnersSkillID = string.Empty;

	public override string ToString ()
	{
		return Utils.dumpObjectSimple (this);
	}
} 
