using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
	private static IDictionary achievementDict;
	public static string PREFSKEY_ACHIEVEMENT = "Achievements";

/*Public functions*/

//load achievement list from local save data; create empty list if no data found.
	public static void initLocalAchievements ()
	{
//		string achievementString = SaveGameManager.instance ().GetString (PREFSKEY_ACHIEVEMENT);
//		if ((achievementString == null) || (achievementString.Length == 0)) {
////		Debug.Log("createEmptyAchievementList");
//			createEmptyAchievementList ();
//		} else {
////		Debug.Log("createLocalAchievementList");
//			createLocalAchievementList (achievementString);
//		}
	}

//increase achievement progress by one.(NOT percentage!)
	public static void incrAchievement (string ID)
	{
//		incrAchievementAmount (ID, 1);
	}

//increase achievement progress by certain amount.(NOT percentage!)
	public static void incrAchievementAmount (string ID, int amount)
	{
//		IDictionary properties = achievementDict [ID] as IDictionary;
//		int currentProgress = int.Parse(properties ["current"].ToString());
//	
//		updateAchievement (ID, currentProgress + amount);
	}

//set achievement progress to zero.
	public static void resetAchievement (string ID)
	{
		setLocalAchievementProgress (ID, 0);
	}

//set achievement progress to certain value 
//ONLY if current progress is lower than target value.
//.(NOT percentage!)
	public static void updateAchievement (string ID, int currentProgress)
	{
//		setLocalAchievementProgress (ID, currentProgress);
//		//reportUpdatedAchievements();
//		saveUpdatedAchievements ();
	
	}

//commit all progressed achievement data to server.
	public static void reportUpdatedAchievements ()
	{
//		foreach (string key in achievementDict.Keys) {	//Debug.Log(key);
//			IDictionary properties = achievementDict [key] as IDictionary;
//			int current = int.Parse(properties ["current"].ToString());
//			int target = int.Parse(properties ["target"].ToString());
//			double progress = (current * 100) / target;
//			//Debug.Log("progress"+ progress);
//			if (bool.Parse(properties ["differFromGC"].ToString())) {
////			Debug.Log("report to gc");
//#if !UNITY_EDITOR
//				if (current == target) {
//					TrackDMgr.instance.achievementGot (properties ["Id"]);
//				}
//#endif
//				reportGCAchievementProgress (properties ["Id"].ToString(), progress);
//			}
//			if (bool.Parse(properties ["differFromOF"].ToString())) {
////			Debug.Log("report to of");
//				reportOFAchievementProgress (properties ["GREE_id"].ToString(), progress);
//				properties ["differFromOF"] = false;
//			}
//		}
	}

/*Private functions*/

	private static void createEmptyAchievementList ()
	{
		achievementDict = new Hashtable (){
			{"FIRE_COMPLETE",new Hashtable (){{"Id","FIRE_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"TECH_COMPLETE",new Hashtable (){{"Id","TECH_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ASTEROID_COMPLETE",new Hashtable (){{"Id","ASTE_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ICE_COMPLETE",new Hashtable (){{"Id","ICE_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"TUTORIAL_COMPLETE",new Hashtable (){{"Id","TUTORIAL_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ALL_HEROES_HIRED",new Hashtable (){{"Id","ALL_HEROES_HIRED"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"20SKILL_1BATTLE",new Hashtable (){{"Id","20SKILL_1BATTLE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"10MANA_USED",new Hashtable (){{"Id","10MANA_USED"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"LEVEL_COMPLETE",new Hashtable (){{"Id","LEVEL_COMPLETE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"BEAT_1_MINIBOSS",new Hashtable (){{"Id","BEAT_1_MINIBOSS"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"BEAT_1_FINALBOSS",new Hashtable (){{"Id","BEAT_1_FINALBOSS"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"50_PURCHASED",new Hashtable (){{"Id","50_PURCHASED"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"100_PURCHASED",new Hashtable (){{"Id","100_PURCHASED"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"10_WINS_IN_A_ROW",new Hashtable (){{"Id","10_WINS_IN_A_ROW"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"HERO_LV10",new Hashtable (){{"Id","HERO_LV10"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"HERO_LV20",new Hashtable (){{"Id","HERO_LV20"},{ "GREE_id","1690792"},{ "target",20},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"HERO_LV30",new Hashtable (){{"Id","HERO_LV30"},{ "GREE_id","1690792"},{ "target",30},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ALL_HERO_LV30",new Hashtable (){{"Id","ALL_HERO_LV30"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"500_GOLD_SPENT",new Hashtable (){{"Id","500_GOLD_SPENT"},{ "GREE_id","1690792"},{ "target",1000},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"1000_GOLD_SPENT",new Hashtable (){{"Id","1000_GOLD_SPENT"},{ "GREE_id","1690792"},{ "target",50000},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"GIFT_SENT",new Hashtable (){{"Id","GIFT_SENT"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"10_GIFT_SENT",new Hashtable (){{"Id","10_GIFT_SENT"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ENTER_RAID",new Hashtable (){{"Id","ENTER_RAID"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"FINISH_1_RAIDS",new Hashtable (){{"Id","FINISH_1_RAIDS"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"FINISH_5_RAIDS",new Hashtable (){{"Id","FINISH_5_RAIDS"},{ "GREE_id","1690792"},{ "target",5},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"FINISH_10_RAIDS",new Hashtable (){{"Id","FINISH_10_RAIDS"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"BEAT_ALL_WITH_1",new Hashtable (){{"Id","BEAT_ALL_WITH_1"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"BEAT_BOSS_NO_LOSS",new Hashtable (){{"Id","BEAT_BOSS_NO_LOSS"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_HEALER_100_TIMES",new Hashtable (){{"Id","USE_HEALER_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_MARINE_100_TIMES",new Hashtable (){{"Id","USE_MARINE_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_TRAINER_100_TIMES",new Hashtable (){{"Id","USE_TRAINER_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_PRIEST_100_TIMES",new Hashtable (){{"Id","USE_PRIEST_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_WIZARD_100_TIMES",new Hashtable (){{"Id","USE_WIZARD_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_COWBOY_100_TIMES",new Hashtable (){{"Id","USE_COWBOY_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_TANK_100_TIMES",new Hashtable (){{"Id","USE_TANK_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"USE_DRUID_100_TIMES",new Hashtable (){{"Id","USE_DRUID_100_TIMES"},{ "GREE_id","1690792"},{ "target",100},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"GANG_UP_10",new Hashtable (){{"Id","GANG_UP_10"},{ "GREE_id","1690792"},{ "target",10},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"DIRAC_SEA_50",new Hashtable (){{"Id","DIRAC_SEA_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"NANITECANNON_50",new Hashtable (){{"Id","NANITECANNON_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"SPACECADET_50",new Hashtable (){{"Id","SPACECADET_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"BERSERK_50",new Hashtable (){{"Id","BERSERK_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"TIGERCLAW_50",new Hashtable (){{"Id","TIGERCLAW_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"PROTECT_50",new Hashtable (){{"Id","PROTECT_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"HAPPYTHOUGHT_50",new Hashtable (){{"Id","HAPPYTHOUGHT_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"MINDSHIELD_50",new Hashtable (){{"Id","MINDSHIELD_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"SHOCKTHERAPY_50",new Hashtable (){{"Id","SHOCKTHERAPY_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"OVERCLOCK_50",new Hashtable (){{"Id","OVERCLOCK_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"QUICKDRAW_50",new Hashtable (){{"Id","QUICKDRAW_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"FRONTIERS_50",new Hashtable (){{"Id","FRONTIERS_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ENERGIZEDALLOY_50",new Hashtable (){{"Id","ENERGIZEDALLOY_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"HMETALATTRACTION_50",new Hashtable (){{"Id","HMETALATTRACTION_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"MONSTERID_50",new Hashtable (){{"Id","MONSTERID_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"SERESTORE_50",new Hashtable (){{"Id","SERESTORE_50"},{ "GREE_id","1690792"},{ "target",50},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ALL_SKILLS_FOR_ONE",new Hashtable (){{"Id","ALL_SKILLS_FOR_ONE"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"ALL_SKILLS_FOR_ALL",new Hashtable (){{"Id","ALL_SKILLS_FOR_ALL"},{ "GREE_id","1690792"},{ "target",1},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"OPEN_20_BOXES",new Hashtable (){{"Id","OPEN_20_BOXES"},{ "GREE_id","1690792"},{ "target",20},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}},
			{"FIND_10_BOXES",new Hashtable (){{"Id","FIND_10_BOXES"},{ "GREE_id","1690792"},{ "target",20},{ "current",0},{ "differFromGC",false},{ "differFromOF",false}}}
		};
	}

	private static void createLocalAchievementList (string jsonDataStr)
	{
//	Debug.Log(jsonDataStr);
//		achievementDict = Json.Deserialize (jsonDataStr) as IDictionary;
	}

	private static void setLocalAchievementProgress (string ID, int currentProgress)
	{
//		IDictionary properties = achievementDict [ID] as IDictionary;
//		if (properties != null) {
//			int current = int.Parse(properties ["current"].ToString());
//			int target = int.Parse(properties ["target"].ToString());
//			if (current < currentProgress) {	
//				properties ["current"] = (currentProgress > target) ? target : currentProgress;
//				properties ["differFromGC"] = true;
//				properties ["differFromOF"] = true;
//			}
//		}
	}

	private static void saveUpdatedAchievements ()
	{
//		SaveGameManager.instance ().SetString (PREFSKEY_ACHIEVEMENT, Json.Serialize (achievementDict));
//		reportUpdatedAchievements ();
	}

/*
public static void reportAchievementProgress ( string ID ,   double progress  ){
	reportGCAchievementProgress(ID, progress);
	reportOFAchievementProgress(ID, progress);
	
}
*/

	private static void reportGCAchievementProgress (string ID, double progress)
	{
////	Debug.Log(ID);
//	#if UNITY_IPHONE && !UNITY_EDITOR
//	Social.ReportProgress(ID, progress, function (success) {if (success) {	IDictionary properties = achievementDict[ID];
//			properties["differFromGC"] = false;
//			//Debug.Log("GC ACHIEVEMENT UPDATE SUCCESSFUL");
//		}
//		else {	//Debug.Log("GC ACHIEVEMENT UPDATE FAILED");
//		}
//	});
//	#endif
	}

	private static void reportOFAchievementProgress (string ID, double progress)
	{
//	OpenFeint.UpdateAchievement(ID, progress, true);
		//reportGREEAchievement(ID, progress, null);
	}

	private static void reportGREEAchievement (string ID, double progress)
	{//	Gree.Achievement.LoadAchievements (function ( IAchievement[] achievements  ) {
//		foreach(Gree.Achievement achi in achievements) {
//			if (achi.id == ID) {
//				achi.percentCompleted = progress;
//				achi.ReportProgress (callback);
//				return;
//			}
//		}
//		// achievement not found: invalid achievementId
//		if (callback != null) {
//			callback (false);
//		}
//	});
	}

	void Start ()
	{

	}

	void Update ()
	{

	}
}