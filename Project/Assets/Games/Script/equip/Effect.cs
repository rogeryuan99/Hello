using UnityEngine;
using System.Collections;

public class Effect 
{
	public static string ATK_PHY = "ATK_PHY";
	public static string ATK_IMP = "ATK_IMP";
	public static string ATK_PSY = "ATK_PSY";
	public static string ATK_EXP = "ATK_EXP";
	public static string ATK_ENG = "ATK_ENG";
	public static string ATK_MAG = "ATK_MAG";
	
	public static string DEF_PHY = "DEF_PHY";
	public static string DEF_IMP = "DEF_IMP";
	public static string DEF_PSY = "DEF_PSY";
	public static string DEF_EXP = "DEF_EXP";
	public static string DEF_ENG = "DEF_ENG";
	public static string DEF_MAG = "DEF_MAG";

	public static string ASPD = "ASPD";
	public static string MSPD = "MSPD";	
	public static string HP = "HP";//gwp create
	public static string DE_DEF = "DE_DEF";// reduce enemies defense
	
	public static string REGEN = "REGEN";
	public static string HEALOUT = "HEALOUT";
	public static string HEALIN = "HEALIN";
	
	//if effect just work in any planets 
	
	// delete by why 2014.2.7
//	public static string PLANT_F_ATK = "PLANT_F_ATK";//fire plant increase atk
//	public static string PLANT_F_DEF = "PLANT_F_DEF";//fire plant increase def
//	public static string PLANT_A_ATK = "PLANT_A_ATK";//asteroid plant increase atk
//	public static string PLANT_A_DEF = "PLANT_A_DEF";//asteroid plant increase def
//	public static string PLANT_T_ATK = "PLANT_T_ATK";//tech plant increase atk
//	public static string PLANT_T_DEF = "PLANT_T_DEF";//tech plant increase def
//	public static string PLANT_I_ATK = "PLANT_I_ATK";//ice plant increase atk
//	public static string PLANT_I_DEF = "PLANT_I_DEF";//ice plant increase def
//	public static string CRIT = "CRIT";
//	public static string EVADE = "EVADE";
//	public static string STRIKE = "STRIKE"; // gwp create
//	public static string HEAL_P = "HEAL_P";//heal party 
//	public static string HEAL_S = "HEAL_S";//heal self
//	public static string LUCK   = "LUCK";  // increase chance to find box key

//	public static string CHESTBOX = "CHESTBOX";//****LTJ add chestbox****
//	
//	public static string MERCHANDISE = "MERCHANDISE";//add by gwp for merchants 
//	
//	public static string FUEL = "FUEL";//Increased Fuel
	
//	public static string RCD  = "RCD";
//	public static string EXP  = "EXP";
//	public static string GOLD = "GOLD";
//	public static string ATK = "ATK";
//	public static string DEF = "DEF";
	
	public string eName;//ASPD,MSPD,reduceCD,
	public float num = 0;
	public bool isPer = false;
	
	public Effect()
	{
	}
	
	public Effect( string eName, float num, bool isPer)
	{
		this.eName = eName;
		this.num   = num;
		this.isPer = isPer;
	}
	
	public Effect clone()
	{
		return new Effect(this.eName, this.num, this.isPer);
	}
	
	public string description ()
	{
		string numStr;
		
		
		numStr = num.ToString();
		
		
		return numStr;//des.Replace("@", numStr);
	}
}
