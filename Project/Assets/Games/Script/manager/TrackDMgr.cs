using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackDMgr : Singleton<TrackDMgr> {


public static string plat;
public static string session="";
private static string version = "1.2f"; 

//public static TrackDMgr instance;
public static long levelStartTime;
public static int currentRaidID=-1;
public static ArrayList levelUsedHeroes = new ArrayList();
	
	
//public void Start (){
//	instance = this;
//	DontDestroyOnLoad(this.gameObject);
//}

public static string getGameVersion (){
	return version;
}

public static string getXMLVersion (){
	return "2.6";
}

public void sessionStart (){
	//{"act":"sesStart","skList":"skAName_skBName_skCName","g_c":"3_5","uPlat":"ios","user":"21","sess":"32bitsMd5String","gID":"sfh","gVer":"1.0f",
	//"cl":"W8_M10_H3_C4_T5_D6_E10_P1","tTime":10,"cTime":10,"useGr":1,"fNum":5}
}

public void levelComplete (){

}

public void levelComplete ( 
		int lvPer ,   
		int curLv ,   
		int alPer ,   
		bool isFirst ,    
		int dropItem ,   
		int dropCoins ,   
		int reliveCount ,  
		int rechargeCount ,   
		int tryCount ,   
		ArrayList skUse ,   
		int state  
		){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "levelUp");
//	hash.Add("lc", lvPer);
//	hash.Add("level", curLv);
//	hash.Add("alivePct", alPer);
//	hash.Add("isFC", isFirst);
//	hash.Add("iDrop", dropItem);
//	hash.Add("cDrop", dropCoins);
//	hash.Add("s", UserInfo.instance.getCoins());
//	hash.Add("res", reliveCount);
//	hash.Add("rec", rechargeCount);
//	hash.Add("tryC", tryCount);
//	hash.Add("skUse", skUse);
//	//new info
//	hash.Add("outcome",state);//completed=2, quit=1 defeat=0
//	hash.Add("stime", levelStartTime);
//	hash.Add("tag", TrackDMgr.currentRaidID);//raid id
//	hash.Add("cu", TrackDMgr.levelUsedHeroes);
//	
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
//	
//	currentRaidID = -1;
//	levelStartTime = -1;
//	//for 20121107 bug fix   start---->
//	GData.firstCompleteLevel = -1;
//	levelUsedHeroes.Clear();
//	//<--------------------  end
}

public void levelFail ( int level ,   int wave ,   int reliveCount ,   int rechargeCount ,   bool isQuit  ){
 //	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "event");
//	hash.Add("event", "LevelFail");
//	hash.Add("cat", level+"_"+wave);
//	hash.Add("sub", reliveCount+"_"+rechargeCount);
//	hash.Add("sub2", isQuit?"1":"0");
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void skillTraining ( string heroType1 ,   string skID ,   bool isCredits  ){
//	string heroType = heroType1;//w = wizard m=marine h=healer c=cowboy t=trainer d=druid e=exxo p=pirest
//	switch(heroType)
//	{
//		case HeroData.MARINE:
//			heroType = "m";
//			break;
//		case HeroData.HEALER:
//			heroType = "h";
//			break;
//		case HeroData.TANK:
//			heroType = "e";
//			break;
//		case HeroData.WIZARD:
//			heroType = "w";
//			break;
//		case HeroData.DRUID:
//			heroType = "d";
//			break;
//		case HeroData.PRIEST:
//			heroType = "p";
//			break;
//		case HeroData.COWBOY:
//			heroType = "c";
//			break;
//		case HeroData.TRAINER:
//			heroType = "t";
//			break;
//		default:
//			heroType = "w";
//			break;
//	}
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "event");
//	hash.Add("event", "SkillTraining");
//	hash.Add("cat", heroType);
//	hash.Add("sub", skID);//skill id
//	hash.Add("sub2", isCredits?"1":"0");//for credits or time learn
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void achievementGot ( string achiName  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "cQuest");
//	hash.Add("qID", achiName);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void itemPurchase ( string itemID ,   bool isEvt ,    int currencyType ,   int fullPrice ,   int price  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "pItem");
//	hash.Add("itemSID", itemID);
//	hash.Add("hotDeal", isEvt?"sale":"");
//	Hashtable fullPriceType;
//	Hashtable priceType;
//	//currencyType  1  hard     2  soft     3  DOLLAR
//	if(currencyType == 1)
//	{
//		fullPriceType = new Hashtable(){{"cType","HARD"},{"q",fullPrice}};
//		priceType     = new Hashtable(){{"cType","HARD"},{"q",price}};
//	}else if(currencyType == 2){
//		fullPriceType = new Hashtable(){{"cType","DOLLAR"},{"q",fullPrice}};
//		priceType     = new Hashtable(){{"cType","SOFT"},{"q",price}};
//	}else{
//		fullPriceType = new Hashtable(){{"cType","DOLLAR"},{"q",fullPrice}};
//		priceType     = new Hashtable(){{"cType","DOLLAR"},{"q",price}};
//	}
//	hash.Add("fullPrice", fullPriceType);
//	hash.Add("price", priceType);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void declinePurchase ( string itemID  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "bItem");
//	hash.Add("itemSID", itemID);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void fueStep ( int step ,   string title ,   bool isLastStep  ){
//	 Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "fue");
//	hash.Add("Id", title);
//	hash.Add("o", step);
//	hash.Add("c", isLastStep);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void userCapability (){
	//{"act":"usrCaps","gree":FALSE,"uPlat":"iPhone","user":"21","sess":"1336152557075","gID":"sfh","gVer":"1.0f","time":1335819606139,"l":13,"e":5} 
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "usrCaps");
//	hash.Add("gree", false);
//	hash.Add("flashCaps", 0);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void socialRaidInvite ( string social_id ,   string frdList ,   int difficulty ,   string path  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "inviteStaff"); 
//	hash.Add("tag", social_id);
//	hash.Add("friendsInvitedList", frdList);//frdList = 12,56,67
//	hash.Add("itemType", "RAID");
//	string hours;
//	if(difficulty == 1)
//	{
//		hours = "3hr";
//	}else if(difficulty == 2){
//		hours = "12hr";
//	}else if(difficulty == 3){
//		hours = "23hr";
//	}else{
//		hours = "3hr";
//	}
//	string pathStr;
//	switch(path)
//	{
//		case "A":
//			pathStr = "ARENA";
//			break;
//		case "B":
//			pathStr = "ARMORY";
//			break;
//		case "C":
//			pathStr = "MAYHEM";
//			break;
//		case "D":
//			pathStr = "SECURITY";
//			break;
//		default:
//			pathStr = "ARENA";
//			break;
//	}
//	hash.Add("cat", hours);//the raid category (24hr, 12hr, 3hr)
//	hash.Add("sub", pathStr);//The raid subcategory 4 path   Arena , armory,  mayhem,  security
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void socialRaidAccepted ( string social_id ,   string fromUser  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "staffInviteAccepted");
//	hash.Add("fromUser", fromUser);
//	hash.Add("tag", social_id);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void upgrades ( string itemID ,   string itemLv  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "event");
//	hash.Add("event", "Upgrade");
//	hash.Add("cat", itemID);
//	hash.Add("sub", itemLv);
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void guestConvert (){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "userInfoUpdate");
//	hash.Add("fullName", "");
//	hash.Add("age", getHighestLvChrt());
//	hash.Add("verified", "guest");
//	hash.Add("first", "");
//	hash.Add("last", "");
//	hash.Add("email", "");
//	hash.Add("gender", "");
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void guestConvert ( string fbName  ){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "userInfoUpdate");
//	hash.Add("fullName", fbName);
//	hash.Add("age", getHighestLvChrt());
//	hash.Add("verified", "facebook");
//	hash.Add("first", PlayerPrefs.GetString("firstName"));
//	hash.Add("last", PlayerPrefs.GetString("lastName"));
//	hash.Add("email", PlayerPrefs.GetString("fbEmail"));//PlayerPrefs.GetString("fbEmail")
//	hash.Add("gender", PlayerPrefs.GetString("gender"));
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void install (){
//	Hashtable hash = getBaseMsgHash();
//	hash.Add("act", "install");
//	string jsonStr = MiniJSON.jsonEncode(hash);
//	postToTrackServer(jsonStr);
}

public void social (){
	//{"act":"social","tel":10,"invt":10,"acpt":10,"uPlat":"ios","user":"21","sess":"32bitsMd5String","gID":"sfh","gVer":"1.0f",
	//"cl":"W8_M10_H3_C4_T5_D6_E10_P1","tTime":10,"cTime":10,"useGr":1,"fNum":5}
}

public Hashtable getBaseMsgHash (){ 
//	long time = getTimeStamp();
//	string tempPlat = plat;
//	#if UNITY_IPHONE && !UNITY_EDITOR
//	tempPlat = plat +" "+iPhoneSettings.systemVersion;
//	#elif UNITY_ANDROID && !UNITY_EDITOR
//	tempPlat = plat + " " + SystemInfo.deviceModel;
//	Debug.Log("user is using "+SystemInfo.deviceModel);
//	#endif
//	Hashtable hash = new Hashtable(){{"uPlat",tempPlat},{"user",UserInfo.instance.getID()},{"sess",session},{"gID","sfh"},{"gVer",version},{"time",time},{"l",getHighestLvChrt()},{"e",getFrdNum()}};
//	return hash;
	return null;
}

public long getTimeStamp (){
	System.DateTime dateTime = new System.DateTime(1970,1,1);
	System.TimeSpan timespan = System.DateTime.UtcNow - dateTime;
	long time = (long)timespan.TotalSeconds*1000;
	return time;
}

public int getHighestLvChrt (){
	if(UserInfo.heroDataList.Count>0)
	{
		ArrayList lvAry = new ArrayList();
		for( int i=0; i<UserInfo.heroDataList.Count; i++)
		{
			HeroData heroD = (HeroData)UserInfo.heroDataList[i];
			lvAry.Add(heroD.lv);
		}
		lvAry.Sort();
		return (int)lvAry[lvAry.Count-1];
	}else{
		return 0;
	}
}
public int getFrdNum (){
	 if(PlayerPrefs.HasKey("UserFriendCount"))
	 {
	 	 return PlayerPrefs.GetInt("UserFriendCount");
	 }else{
	 	 return 0;
	 }
}

public string formatHeroLv (){
	List<HeroData> heroDList = UserInfo.heroDataList;
	ArrayList resultAry = new ArrayList();
	for( int i=0; i<heroDList.Count; i++)
	{
		HeroData heroD = (HeroData)heroDList[i];
		string type = string.Empty;
		if(heroD.state == HeroData.State.RECRUITED_NOT_SELECTED)
		{
			switch(heroD.type)
			{
				case HeroData.MARINE:
					type = "M"+heroD.lv;
					break;
				case HeroData.HEALER:
					type = "H"+heroD.lv;
					break;
				case HeroData.TANK:
					type = "T"+heroD.lv;
					break;
				case HeroData.WIZARD:
					type = "W"+heroD.lv;
					break;
				case HeroData.DRUID:
					type = "D"+heroD.lv;
					break;
				case HeroData.PRIEST:
					type = "P"+heroD.lv;
					break;
				case HeroData.COWBOY:
					type = "C"+heroD.lv;
					break;
				case HeroData.TRAINER:
					type = "T"+heroD.lv;
					break;
				default:
					break;
			}
			resultAry.Add(type);
		}
	}
	return resultAry.Join("_");
}

//function postToTrackServer( string jsonStr  )
//{  
//	//"http://rx.scifiheroes.com/report/track";
//	//+WWW.EscapeURL(jsonStr); 
//	//http://<reporting receiver>/track?data=<message>
//	string url = "http://rx.scifiheroes.com/track?data="+WWW.EscapeURL(jsonStr);
//	//print(url);
//	WWW w = new WWW(url);
//	yield return w;
//	if(w.error)
//	{
//		//print(w.error);
//	}else{
//		 //print(w.text);
//	}
//} 
public IEnumerator postToTrackServer ( string jsonStr  ){
	System.Text.UTF8Encoding encoding= new System.Text.UTF8Encoding();
	Hashtable postHeader= new Hashtable();
	
	//print("user id:"+UserInfo.instance.getID());
	//print(jsonStr);
	string url = "http://rx.scifiheroes.com/report/track";
	WWWForm form= new WWWForm();
	WWW w;
	postHeader.Add("Content-Type", "text/json");
	postHeader.Add("Content-Length", jsonStr.Length);
	w = new WWW(url, encoding.GetBytes(jsonStr), postHeader);
	yield return w;
	if(false == string.IsNullOrEmpty(w.error))
	{
//		//print(w.error);
	}else{
//		//print(w.text+"_"+jsonStr);
	}
}

public void Update (){

}
}