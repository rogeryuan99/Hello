using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaTest : MonoBehaviour {
	public TextAsset luaText;	
	bool virgin;
	Lua L;
	
	void Start()
	{
		L = new Lua();
		L.DoString(luaText.text);	
		object[] v;
		
		v = L.GetFunction("getCostStaminaByLevel").Call(4,4);
		dumpResult("getCostStaminaByLevel",v);
		
		
//			
		v = L.GetFunction("rewardGold").Call(4,4,21);
		dumpResult("rewardGold",v);
//		
		v = L.GetFunction("getGearLevelUpCostSilver").Call(1,2,100,100);
		dumpResult("getGearLevelUpCostSilver",v);
//		v = L.GetFunction("getPracticeLimitAtk").Call(4);
//		dumpResult("getPracticeLimitAtk",v);
//		v = L.GetFunction("getPracticeLimitDef").Call(4);
//		dumpResult("getPracticeLimitDef",v);
//		v = L.GetFunction("getPracticeLimitSp").Call(5);
//		dumpResult("getPracticeLimitSp",v);
//		v = L.GetFunction("getSummonCostCash").Call("b");
//		dumpResult("getSummonCostCash",v);
//		
//		
//		
//		v = L.GetFunction("getTrumpFuseProvideXp").Call(4,2);
//		dumpResult("getTrumpFuseProvideXp",v);
//		
//		v = L.GetFunction("getEquipForgeLvLimit").Call(3);
//		dumpResult("getEquipForgeLvLimit",v);
//		
//		v = L.GetFunction("getEquipForgeCostCoins").Call(2,11);
//		dumpResult("getEquipForgeCostCoins",v);
//		
//		v = L.GetFunction("getEquipForgeCostEquipClips").Call(2);
//		dumpResult("getEquipForgeCostEquipClips",v);
//		
//		v = L.GetFunction("getPracticeLimitHp").Call(2);
//		dumpResult("getPracticeLimitHp",v);
//		v = L.GetFunction("getPracticeLimitAtk").Call(2);
//		dumpResult("getPracticeLimitAtk",v);
//		v = L.GetFunction("getPracticeLimitDef").Call(2);
//		dumpResult("getPracticeLimitDef",v);
//		v = L.GetFunction("getPracticeLimitSp").Call(2);
//		dumpResult("getPracticeLimitSp",v);
		
	}
	private void dumpResult(string funName,object[] v){
		string s="";
		foreach(var o in v){
			s+=o+" ";
		}	
		Debug.Log(funName+":"+s);	
	}
}
