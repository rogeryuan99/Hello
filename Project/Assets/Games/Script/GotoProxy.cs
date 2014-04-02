using UnityEngine;
using System.Collections;

public class GotoProxy : MonoBehaviour
{
	public static string BATTLESCENE = "NormalLevel";
	public static string UIMAIN = "UIMain";
	public static string TUTORIALSCENE = "TestTutorial";
	
	public static string EQUIP = "equip";
	public static string SKILLTREE = "skill";
	public static string START = "Start";
//gwp
	public static string MAP = "BattleShip";
	public static string ARMORY = "CombinedArmory";
	public static string COMBINED_ARMORY = "CombinedArmory";
	public static string MERCHANT = "Merchant_New";//"Merchant"
	public static string MERCHANT_IPHONE = "Merchant_New_iphone";//"MERCHANT_IPHONE"
	public static string FRIEND_LIST = "FriendList";
//static string FRIEND_LIST_IPHONE = "FriendList_iphone";
	public static string MAIN_MENU = "mainScreen";
//static string MAIN_MENU_iPHONE = "mainScreen_iphone";
	public static string LOGIN = "login";
	public static string MOVIE = "Movie";
	public static string LOADING_ASSETS = "initGameResource";
	public static string EDIT_TEAM = "editTeam";
//static string EDIT_TEAM_IPHONE = "editTeam_iphone";
	public static string YOUR_TEAM = "yourTeam"; //Remove Your Team Scene
//static string YOUR_TEAM_IPHONE ="yourTeam_iphone";

//static string LEVEL = "levelSelect";

	public static GameObject black;

	public static void setSceneName (string name)
	{
	}

	public static string getSceneName ()
	{
		return Application.loadedLevelName;
	}
	public static void gotoScene (string sceneName)
	{
		Debug.Log("LoadLevel :"+sceneName);
		Application.LoadLevel (sceneName);
	}
	public static void fadeInScene (string sceneName)
	{
		Debug.Log("LoadLevel :"+sceneName);
		Application.LoadLevel (sceneName);
	}

}
