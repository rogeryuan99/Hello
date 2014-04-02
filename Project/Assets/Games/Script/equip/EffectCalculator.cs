using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectCalculator
{
	protected delegate void FunHandler (Effect eft, DataModifier dmAdd, DataModifier dmMult);
	
	protected static Hashtable equipEftHandler;
	
	public static void calculate (HeroData heroData)
	{
		if (equipEftHandler == null) 
		{
			initHashtable ();
		}
		
		heroData.resetEft ();
		GlobalModifier.reset ();
		
		List<Effect> equipEftAry = heroData.getEquipEft ();

		foreach(Effect eft in equipEftAry)
		{
			FunHandler func = equipEftHandler [eft.eName] as FunHandler;
			
			if(func == null)
			{
				continue;
			}
			func (eft, 
				heroData.itemAdd,
				heroData.itemMult);
		}
		
	    
		List<Effect> skEftAry = heroData.getSkEft ();
		
		foreach(Effect eft in skEftAry)
		{
			
//			Debug.Log(eftD.eName);
//			Debug.Log ("eftD.eName=" + eft.eName);
//			Debug.Log (Utils.dumpHashTable (equipEftHandler));
			
			FunHandler func = equipEftHandler [eft.eName.ToUpper()] as FunHandler;
			if(func == null)
			{
				continue;
			}
			func (eft, 
				heroData.skillAdd,
				heroData.skillMult);
		}
	    
	}
	
	protected static void atkPhyHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.PHY += eftD.num;
		}
		else
		{
			dmAdd.attack.PHY += eftD.num;
		}
	}
	
	protected static void atkImphpHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.IMP += eftD.num;
		}
		else
		{
			dmAdd.attack.IMP += eftD.num;
		}
	}
	protected static void atkPsyHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.PSY += eftD.num;
		}
		else
		{
			dmAdd.attack.PSY += eftD.num;
		}
	}
	protected static void atkExpHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.EXP += eftD.num;
		}
		else
		{
			dmAdd.attack.EXP += eftD.num;
		}
	}
	protected static void atkEngHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.ENG += eftD.num;
		}
		else
		{
			dmAdd.attack.ENG += eftD.num;
		}
	}
	protected static void atkMagHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.attack.MAG += eftD.num;
		}
		else
		{
			dmAdd.attack.MAG += eftD.num;
		}
	}
	
	
	protected static void defPhyHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		
		if(eftD.isPer)
		{
			dmMult.defense.PHY += eftD.num;
		}
		else
		{
			dmAdd.defense.PHY += eftD.num;
		}
	}
	
	protected static void defImphpHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.defense.IMP += eftD.num;
		}
		else
		{
			dmAdd.defense.IMP += eftD.num;
		}
	}
	protected static void defPsyHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		
		if(eftD.isPer)
		{
			dmMult.defense.PSY += eftD.num;
		}
		else
		{
			dmAdd.defense.PSY += eftD.num;
		}
	}
	protected static void defExpHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
	
		if(eftD.isPer)
		{
			dmMult.defense.EXP += eftD.num;
		}
		else
		{
			dmAdd.defense.EXP += eftD.num;
		}
	}
	protected static void defEngHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		
		if(eftD.isPer)
		{
			dmMult.defense.ENG += eftD.num;
		}
		else
		{
			dmAdd.defense.ENG += eftD.num;
		}
	}
	protected static void defMagHandler(Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.defense.MAG += eftD.num;
		}
		else
		{
			dmAdd.defense.MAG += eftD.num;
		}
	}
	
//	protected static void atkHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.atk += eftD;
//	}
	
//	protected static void defHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.defense += eftD;
//	}
//	
	protected static void aspdHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.atkSpd += eftD.num;
		}
		else
		{
			dmAdd.atkSpd += eftD.num;
		}
	}
//	
//	protected static void plantFAtkHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.atk += eftD;
//	}
//
	protected static void mspdHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.moveSpd += eftD.num;
		}
		else
		{
			dmAdd.moveSpd += eftD.num;
		}
	}
//	
//	//xingyh
//	protected static void plantFDefHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.defense += eftD;
//	}
//	
//	protected static void plantAAtkHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.atk += eftD;
//	}
//	
//	protected static void plantADefHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.defense += eftD;
//	}
//	
//	protected static void plantTAtkHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.atk += eftD;
//	}
//	
//	protected static void plantTDefHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.defense += eftD;
//	}
//	
//	protected static void plantIAtkHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//			dm.atk += eftD;
//	}
//	
//	protected static void plantIDefHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{	
//		dm.defense += eftD;
//	}
//	
//	protected static void critHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.crtlStk += eftD;
//	}
//	
//	protected static void evadeHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.evade += eftD;
//	}
//	
//	protected static void rcdHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.rcd += eftD;
//	}
//	
//	protected static void expHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.exp += eftD;
//	}
	
	protected static void goldHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		GlobalModifier.gold += (int)eftD.num;
	}

//	protected static void healPartHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.atkHealPart += eftD;
//	}
//	
//	protected static void healSelfHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		dm.atkHealSelf += eftD;
//	}
		
	protected static void deDefHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		GlobalModifier.de_def += (int)eftD.num;
	}
	
//	protected static void fuelHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		GlobalModifier.fuelChance += eftD.num;
//	}
//	
//	protected static void luckHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
//	{
//		GlobalModifier.luck += eftD.num;
//	}
		
	protected static void hpHandler (Effect eftD, DataModifier dmAdd, DataModifier dmMult)
	{
		if(eftD.isPer)
		{
			dmMult.maxHp += (int)eftD.num;
		}
		else
		{
			dmAdd.maxHp += (int)eftD.num;
		}
	}
	//end
	
	
	
	protected static void initHashtable ()
	{
		equipEftHandler = new Hashtable (); 
		equipEftHandler [Effect.HP] = (FunHandler)hpHandler;
		equipEftHandler [Effect.ASPD] = (FunHandler)aspdHandler;
		equipEftHandler [Effect.MSPD] = (FunHandler)mspdHandler;
		
		equipEftHandler [Effect.ATK_PHY] = (FunHandler)atkPhyHandler;
		equipEftHandler [Effect.ATK_IMP] = (FunHandler)atkImphpHandler;
		equipEftHandler [Effect.ATK_PSY] = (FunHandler)atkPsyHandler;
		equipEftHandler [Effect.ATK_EXP] = (FunHandler)atkExpHandler;
		equipEftHandler [Effect.ATK_ENG] = (FunHandler)atkEngHandler;
		equipEftHandler [Effect.ATK_MAG] = (FunHandler)atkMagHandler;
		
		equipEftHandler [Effect.DEF_PHY] = (FunHandler)defPhyHandler;
		equipEftHandler [Effect.DEF_IMP] = (FunHandler)defImphpHandler;
		equipEftHandler [Effect.DEF_PSY] = (FunHandler)defPsyHandler;
		equipEftHandler [Effect.DEF_EXP] = (FunHandler)defExpHandler;
		equipEftHandler [Effect.DEF_ENG] = (FunHandler)defEngHandler;
		equipEftHandler [Effect.DEF_MAG] = (FunHandler)defMagHandler;
		
		equipEftHandler [Effect.DE_DEF] = (FunHandler)deDefHandler;	
		
		// delete by why 2014.2.7
//		equipEftHandler [Effect.PLANT_F_ATK] = (FunHandler)plantFAtkHandler;
//		
//		//gwp
//		equipEftHandler [Effect.STRIKE] = (FunHandler)strikeHandler;
//		
//		//xingyh
//		equipEftHandler [Effect.PLANT_F_DEF] = (FunHandler)plantFDefHandler;
//		equipEftHandler [Effect.PLANT_A_ATK] = (FunHandler)plantAAtkHandler;
//		equipEftHandler [Effect.PLANT_A_DEF] = (FunHandler)plantADefHandler;
//		equipEftHandler [Effect.PLANT_T_ATK] = (FunHandler)plantTAtkHandler;
//		equipEftHandler [Effect.PLANT_T_DEF] = (FunHandler)plantTDefHandler;
//		equipEftHandler [Effect.PLANT_I_ATK] = (FunHandler)plantIAtkHandler;
//		equipEftHandler [Effect.PLANT_I_DEF] = (FunHandler)plantIDefHandler;
//		equipEftHandler [Effect.CRIT] = (FunHandler)critHandler;
//		equipEftHandler [Effect.EVADE] = (FunHandler)evadeHandler;
//		equipEftHandler [Effect.HEAL_P] = (FunHandler)healPartHandler;
//		equipEftHandler [Effect.HEAL_S] = (FunHandler)healSelfHandler;
//		equipEftHandler [Effect.FUEL] = (FunHandler)fuelHandler;
//		equipEftHandler [Effect.LUCK] = (FunHandler)luckHandler;		
//		equipEftHandler [Effect.ATK] = (FunHandler)atkHandler;
//		equipEftHandler [Effect.DEF] = (FunHandler)defHandler;
//		equipEftHandler [Effect.RCD] = (FunHandler)rcdHandler;
//		equipEftHandler [Effect.EXP] = (FunHandler)expHandler;
//		equipEftHandler [Effect.GOLD] = (FunHandler)goldHandler;
	}
	
	/*
    static{
	}
	
	protected static string ATK = "ATK";
	protected static string DEF = "DEF";
	protected static string ASPD = "ASPD";
	protected static string MSPD = "MSPD";
	protected static string RCD  = "RCD";
	protected static string EXP  = "EXP";
	protected static string GOLD = "GOLD";
	protected static string CRIT = "CRIT";
	protected static string EVADE = "EVADE";
	protected static string HEAL_P = "HEAL_P";//heal party 
	protected static string HEAL_S = "HEAL_S";//heal self
	protected static string LUCK   = "LUCK";  // increase chance to find box key
	protected static string DE_DEF = "DE_DEF";// reduce enemies defense
	protected static string PLANT_F_ATK = "PLANT_F_ATK";//fire plant increase atk
	protected static string PLANT_F_DEF = "PLANT_F_DEF";//fire plant increase def
	protected static string PLANT_A_ATK = "PLANT_A_ATK";//asteroid plant increase atk
	protected static string PLANT_A_DEF = "PLANT_A_DEF";//asteroid plant increase def
	protected static string PLANT_T_ATK = "PLANT_T_ATK";//tech plant increase atk
	protected static string PLANT_T_DEF = "PLANT_T_DEF";//tech plant increase def
	protected static string PLANT_I_ATK = "PLANT_I_ATK";//ice plant increase atk
	protected static string PLANT_I_DEF = "PLANT_I_DEF";//ice plant increase def
	*/
}
