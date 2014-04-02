using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SkillLib
{
	public static SkillLib instance = new SkillLib ();
	public Hashtable allHeroSkillHash;
		
	private SkillLib ()
	{
		allHeroSkillHash = new Hashtable ();
	}
	
	public void initByArtoo(ICollection activeSkills, ICollection passiveSkills){
		foreach(Hashtable h in activeSkills){
			SkillDef sd = new SkillDef();
			sd.id = h["uid"] as string;
			sd.name = h["name"] as string;
			sd.unlockLv = int.Parse(h["level"] as string);
			sd.silver = int.Parse(h["buySilver"] as string);
			sd.gold = int.Parse(h["buyGold"] as string);
			sd.commandPoints = int.Parse(h["buyCp"] as string);
			sd.comboPartners =  h["comboPartner"] as string;
			sd.comboPartnersSkillID = h["partnerSkillID"] as string;
			sd.description = h["description"] as string;
			sd.learnTime = int.Parse(h["learnTime"] as string); 
			sd.buffDurationTime = 0;
			sd.skillDurationTime = 0;
			
			//-------active
			sd.isPassive = false;
			sd.coolDown = int.Parse(h["coolDown"] as string);
			sd.funcName = h["uid"] as string;
			sd.target = (null != h["target"])? h ["target"]  as string: string.Empty;
			
			sd.activeEffectTable = getEffectTable(h,"sk_");
			if(h["sk_time"] != null){
				sd.skillDurationTime = int.Parse(h["sk_time"] as string);
			}
			
			sd.buffEffectTable = getEffectTable(h,"buff_");
			if(h["buff_time"]!=null){
				sd.buffDurationTime =  int.Parse(h["buff_time"] as string);
			}

			allHeroSkillHash [sd.id] = sd;
			
		}
		foreach(Hashtable h in passiveSkills){
			SkillDef sd = new SkillDef();
			sd.id = h["uid"] as string;
			sd.name = h["name"] as string;
			sd.unlockLv = int.Parse(h["level"] as string);
			sd.silver = int.Parse(h["buySilver"] as string);
			sd.gold = int.Parse(h["buyGold"] as string);
			sd.commandPoints = int.Parse(h["buyCp"] as string);
			sd.comboPartners =  h["comboPartner"] as string;
			sd.comboPartnersSkillID = h["partnerSkillID"] as string;
			sd.description = h["description"] as string;
			sd.learnTime = int.Parse(h["learnTime"] as string); 
			sd.buffDurationTime = 0;
			sd.skillDurationTime = 0;
			sd.isPassive = true;
			sd.passiveEffectTable = getEffectTable(h,"effect_");
			allHeroSkillHash [sd.id] = sd;
			
		}
	}
	
	public void setList ( string skillList  ){
		if(allHeroSkillHash.Count == 0 ){
			xmlToList(skillList);
		}
	}
	
	public void resetList (string skillList)
	{
		xmlToList (skillList);
	}
	
	void xmlToList (string skillList)
	{
		XmlDocument document = new XmlDocument ();
		document.LoadXml (skillList);
		XmlNodeList skList = document.DocumentElement.GetElementsByTagName ("heroSkills");
		
		for (int i = 0; i<skList.Count; i++)
		{
			XmlNode node = skList [i];
			XmlNodeList childs = node.ChildNodes;
			string type = node.Attributes ["type"].Value;
			for (int j = 0; j<childs.Count; j++) 
			{
				XmlNode childNode = childs [j];
				
				string id =  childNode.Attributes["id"].Value;
				string name = childNode.Attributes ["name"].Value;
				int level = int.Parse (childNode.Attributes ["level"].Value);
				bool isPassive = "true".Equals (childNode.Attributes ["isPassive"].Value);
				
				int silver = int.Parse (childNode.Attributes ["buySilver"].Value);
				int gold = int.Parse (childNode.Attributes ["buyGold"].Value);
				int commandPoints = int.Parse (childNode.Attributes ["buyCp"].Value);
				
				long learnTime = int.Parse (childNode.Attributes ["learnTime"].Value);
				int coolDown = int.Parse (childNode.Attributes ["coolDown"].Value);
				
				string comboPartners = (null != childNode.Attributes["comboPartner"])? childNode.Attributes ["comboPartner"].Value: string.Empty;
				string comboPartnersSkillID = (null != childNode.Attributes["partnerSkillID"])? childNode.Attributes ["partnerSkillID"].Value: string.Empty;
				
				Hashtable number = new Hashtable ();
				string description = string.Empty;

				XmlNodeList nodes = childNode.ChildNodes;
				
				int buffDurationTime = 0;
				int skillDurationTime = 0;
				
//				List<Effect> passiveEffectList = new List<Effect>();
//	
//				List<Effect> buffEffectList = new List<Effect>();
				
				Hashtable passiveEffectTable = new Hashtable ();;
	
				Hashtable buffEffectTable = new Hashtable ();;
				
				for (int k = 0; k<nodes.Count; k++) 
				{
					XmlNode nd = nodes [k];
					if ("effect".Equals (nd.Name))
					{
						passiveEffectTable = getEffectTable(nd);
					} 
					if ("num".Equals (nd.Name))
					{
						skillDurationTime = int.Parse(nd.Attributes["time"].Value);
						
						number = getEffectTable(nd);
						
					}
					if ("buff".Equals (nd.Name))
					{
						buffDurationTime = int.Parse(nd.Attributes["time"].Value);
						buffEffectTable = getEffectTable(nd);
						
					}// end if
					
				}// end for k
				
				SkillDef sd = new SkillDef();
				sd.id = id;
				sd.name = name;
				sd.unlockLv = level;
				sd.isPassive = isPassive;
				sd.silver = silver;
				sd.gold = gold;
				sd.commandPoints = commandPoints;
				sd.comboPartners = comboPartners;
				sd.comboPartnersSkillID = comboPartnersSkillID;
				sd.description = description;
				sd.learnTime = learnTime;
				sd.buffDurationTime = buffDurationTime;
				sd.skillDurationTime = skillDurationTime;
				
				if (isPassive)
				{
					sd.passiveEffectTable = passiveEffectTable;
				}
				else
				{
					sd.funcName = childNode.Attributes ["id"].Value;
					sd.coolDown = coolDown;
					sd.activeEffectTable = number;
					sd.target = (null != childNode.Attributes["target"])? childNode.Attributes ["target"].Value: string.Empty;
					
					sd.buffEffectTable = buffEffectTable;
					
					
				}// end if
				allHeroSkillHash [id] = sd;
							
			}// end for j
			
		}// end for i
		
	}// end function xmlToList

	
	
//		<skill id="STARLORD1" name="Fire Blast" level="1" isPassive="false" buySilver="100" buyGold="1" buyCp="1" learnTime="35" coolDown="10" target="ENEMY"
//			num_time="5" num_atk_PHY="10%" num_atk_IMP="0" num_atk_PSY="0" num_atk_EXP="0" num_atk_ENG="0" num_atk_MAG="0" num_hp="" num_AOERadius="0"
//			buff_time="5" buff_atk_PHY="0" buff_atk_IMP="0" buff_atk_PSY="0" buff_atk_EXP="0" buff_atk_ENG="0" buff_atk_MAG="0" buff_def_PHY="0" buff_def_IMP="0" buff_def_PSY="0" buff_def_EXP="0" buff_def_ENG="0" buff_def_MAG="0" buff_aspd="0" buff_mspd="0" buff_hp="0"
//      	/>
	
	
	
	public void dumpSkills(){
		ArrayList al = new ArrayList();
		
		foreach(SkillDef sd in allHeroSkillHash.Values){
		
			Hashtable ht = new Hashtable();
			ht.Add("uid",sd.id.ToString());
			ht.Add("name",sd.name.ToString());
			ht.Add("level",sd.unlockLv.ToString());
			ht.Add("isPassive",sd.isPassive.ToString());
			ht.Add("buySilver",sd.silver.ToString());
			ht.Add("buyGold",sd.gold.ToString());
			ht.Add("buyCp",sd.commandPoints.ToString());
			ht.Add("learnTime",sd.learnTime.ToString());
			ht.Add("coolDown",sd.coolDown.ToString());
			if(sd.target!="") ht.Add("target",""+sd.target);
			if(sd.passiveEffectTable!=null){
				foreach(Effect ef in sd.passiveEffectTable.Values){
					if(ef.num>0){
						ht.Add("effect_"+ef.eName ,ef.num + (ef.isPer?"%":""));
					}
				}
			}
			if(sd.buffEffectTable!=null){
				foreach(Effect ef in sd.buffEffectTable.Values){
					if(ef.num>0){
						ht.Add("buff_"+ef.eName ,ef.num + (ef.isPer?"%":""));
					}
				}
				ht.Add("buff_time",""+sd.buffDurationTime);
			}
			if(sd.activeEffectTable!=null){
				foreach(object o in sd.activeEffectTable.Values){
					if(o is Effect){
						Effect ef = (Effect) o;
						if(ef.num>0){
							ht.Add("sk_"+ef.eName ,ef.num + (ef.isPer?"%":""));
						}
					}
				}	
				ht.Add("sk_time",""+sd.buffDurationTime);
				ht.Add("sk_AOERadius",""+sd.activeEffectTable["AOERadius"]);
				ht.Add("sk_chance",""+sd.activeEffectTable["chance"]);
			}
			
			al.Add(ht);					
		}
	
		Debug.Log(MiniJSON.jsonEncode(al));
	}
	
	public static Hashtable getEffectTable(XmlNode equipNode)
	{
		Hashtable effectNumber = new Hashtable();
	
		
		if(equipNode.Attributes["atk_PHY"] != null)
		{
			effectNumber["atk_PHY"] = getEffect(equipNode.Attributes["atk_PHY"].Value, "atk_PHY");
		}
		if(equipNode.Attributes["atk_IMP"] != null)
		{
			effectNumber["atk_IMP"] = getEffect(equipNode.Attributes["atk_IMP"].Value, "atk_IMP");
		}
		if(equipNode.Attributes["atk_PSY"] != null)
		{
			effectNumber["atk_PSY"] = getEffect(equipNode.Attributes["atk_PSY"].Value, "atk_PSY");
		}
		if(equipNode.Attributes["atk_EXP"] != null)
		{
			effectNumber["atk_EXP"] = getEffect(equipNode.Attributes["atk_EXP"].Value, "atk_EXP");
		}
		if(equipNode.Attributes["atk_ENG"] != null)
		{
			effectNumber["atk_ENG"] = getEffect(equipNode.Attributes["atk_ENG"].Value, "atk_ENG");
		}
		if(equipNode.Attributes["atk_MAG"] != null)
		{
			effectNumber["atk_MAG"] = getEffect(equipNode.Attributes["atk_MAG"].Value, "atk_MAG");
		}
		if(equipNode.Attributes["def_PHY"] != null)
		{
			effectNumber["def_PHY"] = getEffect(equipNode.Attributes["def_PHY"].Value, "def_PHY");
		}
		if(equipNode.Attributes["def_IMP"] != null)
		{
			effectNumber["def_IMP"] = getEffect(equipNode.Attributes["def_IMP"].Value, "def_IMP");
		}
		if(equipNode.Attributes["def_PSY"] != null)
		{
			effectNumber["def_PSY"] = getEffect(equipNode.Attributes["def_PSY"].Value, "def_PSY");
		}
		if(equipNode.Attributes["def_EXP"] != null)
		{
			effectNumber["def_EXP"] = getEffect(equipNode.Attributes["def_EXP"].Value, "def_EXP");
		}
		if(equipNode.Attributes["def_ENG"] != null)
		{
			effectNumber["def_ENG"] = getEffect(equipNode.Attributes["def_ENG"].Value, "def_ENG");
		}
		if(equipNode.Attributes["def_MAG"] != null)
		{
			effectNumber["def_MAG"] = getEffect(equipNode.Attributes["def_MAG"].Value, "def_MAG");
		}
		if(equipNode.Attributes["mspd"] != null)
		{
			effectNumber["mspd"] = getEffect(equipNode.Attributes["mspd"].Value, "mspd");
		}
		if(equipNode.Attributes["aspd"] != null)
		{
			effectNumber["aspd"] = getEffect(equipNode.Attributes["aspd"].Value, "aspd");
		}
		if(equipNode.Attributes["hp"] != null)
		{
			effectNumber["hp"] = getEffect(equipNode.Attributes["hp"].Value, "hp");
		}
		if(equipNode.Attributes["AOERadius"] != null)
		{
			effectNumber["AOERadius"] = int.Parse(equipNode.Attributes["AOERadius"].Value);
		}
		if(equipNode.Attributes["sk_damage"] != null)
		{
			effectNumber["sk_damage"] = int.Parse(equipNode.Attributes["sk_damage"].Value);
		}
		if(equipNode.Attributes["chance"] != null)
		{
			effectNumber["chance"] = int.Parse(equipNode.Attributes["chance"].Value);
		}
		if(equipNode.Attributes["atk_damage"] != null)
		{
			effectNumber["atk_damage"] = int.Parse(equipNode.Attributes["atk_damage"].Value);
		}
		if(equipNode.Attributes["universal"] != null)
		{
			effectNumber["universal"] = int.Parse(equipNode.Attributes["universal"].Value);
		}
		if(equipNode.Attributes["universalTime"] != null)
		{
			effectNumber["universalTime"] = int.Parse(equipNode.Attributes["universalTime"].Value);
		}
		if(equipNode.Attributes["buff_time"] != null)
		{
			effectNumber["buff_time"] = int.Parse(equipNode.Attributes["buff_time"].Value);
		}
		if(equipNode.Attributes["debuff_time"] != null)
		{
			effectNumber["debuff_time"] = int.Parse(equipNode.Attributes["debuff_time"].Value);
		}
		if(equipNode.Attributes["debuff_mspd"] != null)
		{
			effectNumber["debuff_mspd"] = int.Parse(equipNode.Attributes["debuff_mspd"].Value);
		}
		if(equipNode.Attributes["debuff_hp"] != null)
		{
			effectNumber["debuff_hp"] = int.Parse(equipNode.Attributes["debuff_hp"].Value);
		}
		
		return effectNumber;
	}
	public static Hashtable getEffectTable(Hashtable h, string prefix)
	{
		Hashtable effectNumber = new Hashtable();
		parseEffect(h,prefix+"atk_PHY",effectNumber,"atk_PHY");
		parseEffect(h,prefix+"atk_IMP",effectNumber,"atk_IMP");
		parseEffect(h,prefix+"atk_PSY",effectNumber,"atk_PSY");
		parseEffect(h,prefix+"atk_EXP",effectNumber,"atk_EXP");
		parseEffect(h,prefix+"atk_ENG",effectNumber,"atk_ENG");
		parseEffect(h,prefix+"atk_MAG",effectNumber,"atk_MAG");
		parseEffect(h,prefix+"def_PHY",effectNumber,"def_PHY");
		parseEffect(h,prefix+"def_IMP",effectNumber,"def_IMP");
		parseEffect(h,prefix+"def_PSY",effectNumber,"def_PSY");
		parseEffect(h,prefix+"def_EXP",effectNumber,"def_EXP");
		parseEffect(h,prefix+"def_ENG",effectNumber,"def_ENG");
		parseEffect(h,prefix+"def_MAG",effectNumber,"def_MAG");
		parseEffect(h,prefix+"mspd"   ,effectNumber,"mspd"   );
		parseEffect(h,prefix+"aspd"   ,effectNumber,"aspd"   );
		parseEffect(h,prefix+"hp"     ,effectNumber,"hp"     );
		if(h[prefix+"AOERadius"] != null)
		{
			effectNumber["AOERadius"] = int.Parse(h[prefix+"AOERadius"] as string);
		}
		if(h[prefix+"sk_damage"] != null)
		{
			string str = h[prefix+"sk_damage"] as string;
			str = str.Contains("%")? str.Remove(str.IndexOf("%")): str;
			effectNumber["sk_damage"] = int.Parse(str);
		}
		parseEffectNumber(h,prefix+"sk_damage", effectNumber,"sk_damage");
		parseEffectNumber(h,prefix+"chance", effectNumber,"chance");
		parseEffectNumber(h,prefix+"atk_damage", effectNumber,"atk_damage");
		
		//add by xiaoyong
		if(h[prefix+"universal"] != null)
		{
			string universalValue = h[prefix+"universal"] as string;
			effectNumber["universal"] = int.Parse(universalValue);
		}
		if(h[prefix+"universalTime"] != null)
		{
			string universalTime = h[prefix+"universalTime"] as string;
			effectNumber["universalTime"] = int.Parse(universalTime);
		}
		parseEffectNumber(h,prefix+"buff_time", effectNumber,"buff_time");
		parseEffectNumber(h,prefix+"debuff_time", effectNumber,"debuff_time");
		parseEffectNumber(h,prefix+"debuff_mspd", effectNumber,"debuff_mspd");
		parseEffectNumber(h,prefix+"debuff_hp", effectNumber,"debuff_hp");
		
		return effectNumber;
	}
	
		
	protected static void parseEffectNumber(Hashtable sourceHash, string sourceKey, Hashtable destHash, string destKey){
		if(sourceHash[sourceKey] != null)
		{
			string str = sourceHash[sourceKey] as string;
			str = str.Contains("%")? str.Remove(str.IndexOf("%")): str;
			destHash[destKey] = int.Parse(str);
		}
	}
	
	protected static void parseEffect(Hashtable sourceHash, string sourceKey,Hashtable destHash, string destKey){
		if(sourceHash[sourceKey] != null)
		{
			destHash[destKey] = getEffect( sourceHash[sourceKey] as string, destKey);
		}
	}
	protected static Effect getEffect(string v, string name)
	{
		
		Effect eft = new Effect();
		eft.eName = name;
		if(v.Contains("%"))
		{
			eft.isPer = true;
			string num = v.Remove(v.IndexOf('%'));
			eft.num = int.Parse(num);
		}
		else
		{
			eft.isPer = false;
			
			if(v == "")
			{
				v = "0";
			}
			eft.num = int.Parse(v);
		}
		return eft;
	}
	
	public SkillDef getSkillDefBySkillID(string skID)
	{
		return (allHeroSkillHash[skID] as SkillDef);
	}
	
	public string getSkillNameByID(string skID)
	{
		return (allHeroSkillHash[skID] as SkillDef).name;
	}
}
