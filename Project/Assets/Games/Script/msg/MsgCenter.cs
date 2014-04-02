using UnityEngine;
using System.Collections;

public class MsgCenter : MsgDispatcher {
	public static string ENEMY_DEAD = "ENEMY_DEAD";
	public static string HERO_DEAD = "HERO_DEAD";
	public static string FALL_DOWN = "FALL_DOWN";
	public static string CREATE_HERO_COMPLETE = "CREATE_HERO_COMPLETE";
	public static string CONSUME_ITEM_CLEARCD = "CONSUME_ITEM_CLEARCD";
	public static string CONSUME_ITEM_RELIVE = "CONSUME_ITEM_RELIVE";
	public static string COPY_HERO_DEAD = "COPY_HERO_DEAD";
	public static string LEVEL_DEFEAT = "LEVEL_DEFEAT";
	// public static string LEVEL_VICTORY = "LEVEL_VICTORY";
	public static string CREATE_DIALOGUE = "CREATE_DIALOGUE";
	public static string CREATE_START_Battle = "CREATE_START_Battle";
	public static string HERO_RELIVE = "HERO_RELIVE" ;
	public static string ARMORY_UPDATEEQUIP = "ARMORY_UPDATEEQUIP";
	
	public static string FREEZE_START = "FREEZE_START";
	
	public static string HERO_HP_CHANGE = "HERO_HP_CHANGE";
	
//	public static string SKILL_CD_UPDATE = "SKILL_CD_UPDATE";
	//public static string HERO_CREATE_COMPLETE = "HERO_CREATE_COMPLETE";
	public static string PLANE_STATUS_UPDATE = "PLANE_STATUS_UPDATE";
	
	private static MsgCenter _instance = null;
	
	public static MsgCenter instance
	{
		get{
			if(_instance == null)
			{
				_instance = new MsgCenter();
			}
			return _instance;
		}
	}
}
