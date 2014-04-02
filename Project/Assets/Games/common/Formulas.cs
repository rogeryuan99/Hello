using UnityEngine;
using System.Collections;
using LuaInterface;

public class Formulas
{
	private static Lua L;
	public static void initLoadLuaFile (string s)
	{
		L = new Lua ();
		L.DoString (s);	
	}
	
	public static int getPracticeLimitHp (int lv)
	{
		object[] v;
		v = L.GetFunction ("getPracticeLimitHp").Call (lv);
		dumpResult ("getPracticeLimitHp", v);
		return (int)(double)v[0];
	
	}

	public static int rewardGold (int currentChapterID, int currentLevelID)
	{
		object[] v = L.GetFunction("rewardGold").Call(currentChapterID, currentLevelID, Random.Range(0,20));
		dumpResult("rewardGold",v);
		
		return (int)(double)v[0];
	}
	
	public static int getCostStaminaByLevel(int currentChapterID, int currentLevelID)
	{
		object[] v = L.GetFunction("getCostStaminaByLevel").Call(currentChapterID, currentLevelID);
		dumpResult("getCostStaminaByLevel",v);
		
		return (int)(double)v[0];
	}
	
	public static int rewardCommandPoints (int currentChapterID, int currentLevelID)
	{
		object[] v = L.GetFunction("rewardCommandPoints").Call(currentChapterID, currentLevelID, Random.Range(0,20));
		dumpResult("rewardCommandPoints",v);
		
		return (int)(double)v[0];
	}
	
	public static int getUpgradeGearCostMoney (int currentLevel, int nextLevel, int consumeSilver, int basePer)
	{
		object[] v = L.GetFunction("getUpgradeGearCostMoney").Call(currentLevel, nextLevel, consumeSilver, basePer);
		dumpResult("getUpgradeGearCostMoney",v);
		
		return (int)(double)v[0];
	}
	
	public static int getUpgradeGearProperty (int currentLevel, int nextLevel, int basePropertyValue, int basePer)
	{
		object[] v = L.GetFunction("getUpgradeGearProperty").Call(currentLevel, nextLevel, basePropertyValue, basePer);
		dumpResult("getUpgradeGearProperty",v);
		
		return (int)(double)v[0];
	}
	
	
//	public static EquipClips getForgeCostEquipClips (Common.Rare rare)
//	{
//		object[] v = L.GetFunction("getEquipForgeCostEquipClips").Call((int)rare);
//		dumpResult("getEquipForgeCostEquipClips",v);
//		
//		return new EquipClips((int)(double)v[0],(int)(double)v[1],(int)(double)v[2],(int)(double)v[3]);
//	}
//
//	public static int getForgeCostCoins (Common.Rare rare, int lv)
//	{
//		object[] v = L.GetFunction("getEquipForgeCostCoins").Call((int)rare,lv);
//		dumpResult("getEquipForgeCostCoins",v);
//		return (int)(double)v[0];
//	}
//	
//	
//	public static EquipClips getSellEarn (List<Equip> equipsSell)
//	{
//		EquipClips ec = new EquipClips();
//		foreach(Equip e in equipsSell){
//			object[] v = L.GetFunction("getEquipSellEarn").Call((int)e.rare,e.forgeLv);
//			dumpResult("getEquipSellEarn",v);
//			ec.add (new EquipClips((int)(double)v[0],(int)(double)v[1],(int)(double)v[2],(int)(double)v[3]));
//		}
//		return ec;	
//	}
//	
//	public static int getSellCoin (List<Equip> equipsSell)
//	{
//		int coin = 0;
//		foreach(Equip e in equipsSell){
//			object[] v = L.GetFunction("getEquipSellCoin").Call((int)e.rare,e.forgeLv);
//			dumpResult("getEquipSellCoin",v);
//			coin += (int)(double)v[0];
//		}
//		return coin;
//	}
//	
//	public static LevelData getTrumpXpLvData (int xp, int mergeNeedClips)
//	{
//		LevelData ld = new LevelData ();
//		
//		for (int n = 1; n<100; n++) {
//			ld.currentLv = n;
//			ld.thisLvMin = (n - 1) * (n - 1) * mergeNeedClips;
//			ld.thisLvMax = n * n * mergeNeedClips;
//			if (xp < ld.thisLvMax) {
//				return ld;	
//			}
//		}
//		return ld;
//	}
//	
//	public static int getPracticeLimitHp ()
//	{
//		object[] v = L.GetFunction("getPracticeLimitHp").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getPracticeLimitHp",v);
//		return (int)(double)v[0];
//	}
//
//	public static int getPracticeLimitAtk ()
//	{
//		object[] v = L.GetFunction("getPracticeLimitAtk").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getPracticeLimitAtk",v);
//		return (int)(double)v[0];
//	}
//
//	public static int getPracticeLimitDef ()
//	{
//		object[] v = L.GetFunction("getPracticeLimitDef").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getPracticeLimitDef",v);
//		return (int)(double)v[0];
//	}
//
//	public static int getPracticeLimitSp ()
//	{
//		object[] v = L.GetFunction("getPracticeLimitSp").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getPracticeLimitSp",v);
//		return (int)(double)v[0];
//	}
//
//	public static int getTrumpFuseProvideXp (Trump trump)
//	{
//		object[] v = L.GetFunction("getTrumpFuseProvideXp").Call((int)trump.rare,trump.def.mergeNeedClips);
//		dumpResult("getTrumpFuseProvideXp",v);
//		return (int)(double)v[0];
//	}
//
//	public static int getEquipForgeLvLimit ()
//	{
//		object[] v = L.GetFunction("getEquipForgeLvLimit").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getEquipForgeLvLimit",v);
//		return  (int)(double)v[0];	
//	}
//	
//	//type = a:1/10, b:1/100, c:1/1000
//	public static int getSummonCostCash (string type)
//	{
//		object[] v = L.GetFunction("getSummonCostCash").Call(type);
//		dumpResult("getSummonCostCash",v);
//		return (int)(double)v[0];		
//	}
//
//	private static void dumpResult(string funName,object[] v){
//		string s="";
//		foreach(var o in v){
//			s+=o+"(" + o.GetType()+") ";
//		}	
////		Debug.Log(funName+":"+s);	
//	}
//	
//	public static int getLeagueSkillHpAdd(int skillLv){
//		object[] v = L.GetFunction("getLeagueSkillHpAdd").Call(skillLv);
//		dumpResult("getLeagueSkillHpAdd",v);
//		return (int)(double)v[0];		
//	}
//	public static int getLeagueSkillSpAdd(int skillLv){
//		object[] v = L.GetFunction("getLeagueSkillSpAdd").Call(skillLv);
//		dumpResult("getLeagueSkillSpAdd",v);
//		return (int)(double)v[0];		
//	}
//	public static int getLeagueSkillAtkAdd(int skillLv){
//		object[] v = L.GetFunction("getLeagueSkillAtkAdd").Call(skillLv);
//		dumpResult("getLeagueSkillAtkAdd",v);
//		return (int)(double)v[0];		
//	}
//	public static int getLeagueSkillDefAdd(int skillLv){
//		object[] v = L.GetFunction("getLeagueSkillDefAdd").Call(skillLv);
//		dumpResult("getLeagueSkillDefAdd",v);
//		return (int)(double)v[0];		
//	}
//	public static int getLeagueSkillUpCost(int skillLv){
//		object[] v = L.GetFunction("getLeagueSkillUpCost").Call(skillLv);
//		dumpResult("getLeagueSkillUpCost",v);
//		return (int)(double)v[0];		
//	}
//	public static int getLeagueSkillLvLimit(){
//		object[] v = L.GetFunction("getLeagueSkillLvLimit").Call(GameData.Instance.myLeague.leagueXpLv);
//		dumpResult("getLeagueSkillLvLimit",v);
//		return (int)(double)v[0];		
//	}
//	public static int getRankingBonusPoint(int rank){
//		object[] v = L.GetFunction("getRankingBonusPoint").Call(GameData.Instance.myLeague.ranking);
//		dumpResult("getRankingBonusPoint",v);
//		return (int)(double)v[0];		
//	}
//	public static int getEnergyCDSeconds(){
//		return 360 - GameData.Instance.myLeague.leagueXpLv*2;
//	}
//	public static int getRageCDSeconds(){
//		return 600 - GameData.Instance.myLeague.leagueXpLv*2;
//	}
//	
//	public static int getSummonACDSecconds(){
//		return 10800;
//	}	
//	public static int getSummonBCDSecconds(){
//		return 86400;
//	}	
//	public static int getSummonCCDSecconds(){
//		return 345600;
//	}	
	
	
	
	
	
	
	
	
	
	private static void dumpResult (string funName, object[] v)
	{
		string s = "";
		foreach (var o in v) {
			s += o + " ";
		}	
//		Debug.Log (funName + ":" + s);	
	}	
	
	public static int HowMuchToSkipSkillTraining(int seconds){
		return (seconds > 0)? Mathf.CeilToInt((float)seconds / 300f)+(Mathf.CeilToInt((float)seconds/300f)-1)*5 : 0;
	}

}
