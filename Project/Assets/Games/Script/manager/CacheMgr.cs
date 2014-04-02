using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class CacheMgr : MonoBehaviour
{
	private int version;
	private static string serverURL = "";//"http://download.majesco.net/SFH/v1003/";//http://192.168f.2.115f/gameData/
	private static string assetURL = "";//ServerProxy.url + "sw/gameData/";
	public TextAsset mustBeDownLoad;
	public TextAsset bgDownLoad;
	private int amount;
	private int needAmount;
	private int downLoadCount = 0;
	public static Hashtable mustBeData;
	private static Hashtable loadSceneDc;
	private static Hashtable prbDc;
	static WWW w;
	static AssetBundle assetBdl;
	static CacheMgr instance;
	static int downloadBeginTime;
	static int downloadLasted;
	static bool  showingAlert = true;
//static bool  isAllDownload=false;

	public bool  shouldStartDownload = false;
	private static bool  isLoaded = false;
	private bool  downloadStarted = false;
	
	// Jugg
	public UIScrollBar progressBar;
	public string loadInfo = "";
	public static string INIT = "INIT";
	public static string LEVEL1 = "LEVEL1-4";
	public static string LEVEL5 = "LEVEL4+";
	public static string INTRO = "INTRO";
	public static string ENDING = "ENDING";
	private ArrayList highPriorityList;
	private bool  isHighPriDownload = false;
	private string currentState;

	public void Awake ()
	{
		highPriorityList = new ArrayList ();
	}

	public void Start ()
	{
//	Caching.CleanCache();
		instance = this;
		DontDestroyOnLoad (this.gameObject);
//	if (!mustBeData) {
//		mustBeData = MiniJsonExtensions.hashtableFromJson(mustBeDownLoad.text);
//		
//	}
//	if (mustBeData) {
//		serverURL  = mustBeData["serverURL"];
//	}
		//	mustBeData = Json.Deserialize(mustBeDownLoad.text);
		loadSceneDc = new Hashtable ();
		prbDc = new Hashtable ();
	}

	public void OnApplicationPause (bool pause)
	{
	}

	public void Update ()
	{
		if (!shouldStartDownload)
			return;
		if (downLoadCount >= amount) {
//		print(downLoadCount+"======"+amount);
//		print("download complete!");
			isLoaded = true;
			if (downLoadCount == amount) {
//			print("download complete!!!!");
//			isAllDownload = true;
//			this.gameObject.SetActiveRecursively(false);
			}
		} else {
//		print(downLoadCount+"======"+amount);
//		print(progressBar +"____"+downloadStarted);
			if (progressBar && downloadStarted) {
				if (w != null) {
					// Jugg
//					progressBar.Value = w.progress;
				} else {
//				Alert.show(InitGameData.txt["The internet connection seem stobelost"] as string, true);
				}
				// Jugg
//				progressBar.Text = loadInfo;
			}
//		print("resource Num:"+downLoadCount +" progress:"+ w.progress*100);
		}
	}

	public void initDownLoad (bool hasNetwork)
	{
		//	if(!hasNetwork)
//	{
//		mustBeData = MiniJsonExtensions.hashtableFromJson(mustBeDownLoad.text);
//	}
//	if(!mustBeData){
//		mustBeData = MiniJsonExtensions.hashtableFromJson(mustBeDownLoad.text);
//	}
//	serverURL  = mustBeData["serverURL"];
//	currentState = INIT;
//	isLoaded     = false;
//	downLoadContent(mustBeData[INIT], hasNetwork);
//	shouldStartDownload = true;
	}

	public bool isAllCached (string state)
	{
		Hashtable dlcHash = mustBeData [state] as Hashtable;
		foreach (string key in dlcHash.Keys) {
			Hashtable content = dlcHash [key] as Hashtable;
			string url = serverURL + content ["url"];
			if (StaticData.isTouch4 && !dlcHash.Contains ("sceneName") && url.Substring (url.Length - 7) != "imageSH") {
				url = serverURL + "itouch/" + content ["url"];
			}
			int version = int.Parse (content ["version"].ToString ());
			if (!Caching.IsVersionCached (url, version)) {
				return false;
			}
		}
		return true;
	}

	public string getCurrentState ()
	{
		return currentState;
	}

	public IEnumerator highPriDownloadContent (bool hasNetwork)
	{
		//highPriorityList.Add({"url":url , "version":version});
		downloadStarted = true;
		isLoaded = false;
		Debug.LogWarning ("start highpridownload ct" + highPriorityList.Count);
		downLoadCount = 0;
		amount = highPriorityList.Count;
		for (int i=0; i<highPriorityList.Count; i++) {
			Hashtable ctHash = highPriorityList [i] as Hashtable;
			string url = ctHash ["url"].ToString ();
			int version = int.Parse (ctHash ["version"].ToString ());
			if (!Caching.IsVersionCached (url, version)) {
				if (!hasNetwork) {
					//Alert.show("Network is not reachable and anything is not cached!");
					//Alert.show(InitGameData.txt["Theinternetconnectionseemstobelost"]);
					yield break;
				}
			
				w = WWW.LoadFromCacheOrDownload (url, version);
				loadInfo = /*"high priority:"+ */(downLoadCount + 1) + "/" + amount;
				yield return w;
				downLoadCount++;
				if (w.error != null) {
					print ("Error==url:" + url);
					print (w.error);
//				Alert.show(w.error);
					w.Dispose ();
					w = null;
				} else {
					w.assetBundle.Unload (false);
					w.Dispose ();
					w = null;
					print ("high pri ->version:" + version + " url:" + url);
				}
			} else {
				downLoadCount++;
				print ("high pri ->cached v:" + version + " url:" + url);
			}
		}
		highPriorityList = new ArrayList ();
		if (!hasNetwork) {
			shouldStartDownload = true;
		}
		downloadStarted = false;
		isHighPriDownload = false;
	}

	public static bool getIsLoaded ()
	{
		return isLoaded;
	}

	public static GameObject getHeroPrb (string heroType)
	{
		
		return Resources.Load ("heroes/" + heroType) as GameObject;
	
//	if( prbDc.Contains(heroType) )
//	{
//		AssetBundle assetBld = prbDc[heroType];
//		Debug.Log("====prbDc.Contains(heroType)");
//		return assetBld.mainAsset;
//	}else{
//		Debug.Log("========================");
//		Hashtable content = getObjInDL(heroType);
////		Hashtable content = mustBeData[heroType];
//		string url = serverURL+content["url"];
//		if(GData.isTouch4)
//		{
//			url = serverURL+"itouch/"+content["url"];
//		}
//		int version = content["version"];
//		print("!!!version:"+version+" url:"+url+" was cached");
//		WWW w = WWW.LoadFromCacheOrDownload(url, version); 
//		GameObject prb = w.assetBundle.mainAsset; 
//		prbDc[heroType] = w.assetBundle;
//		return prb;
//	}
	
	}

	public static GameObject getEnemyPrb (string enemyType)
	{
	
		if (prbDc.Contains (enemyType))
		{
			return prbDc [enemyType] as GameObject;
		} else {
			string path = "";
			if(StaticData.isPVP)
			{
				path = "heroes/" + enemyType;				
			}
			else
			{
				path = "enemies/enemy" + enemyType;
			}
//		int version = content["version"];
//		print("!!!version:"+version+" url:"+url+" was cached");
//		
//		Debug.LogError("enemyType " + enemyType);
//		Debug.LogError("url " + url);
//		Debug.LogError("content[\"url\"]"+ content["url"]);
//		
//		WWW w = WWW.LoadFromCacheOrDownload(url, version); 
//		GameObject prb = w.assetBundle.mainAsset; 
			prbDc [enemyType] = Resources.Load (path) as GameObject;
			//	w.assetBundle.Unload(false);
		
			return prbDc [enemyType] as GameObject;
		}
	}

	public static void loadScene (string sceneName)
	{ 
		/*
	if(!loadSceneDc.Contains(sceneName))
	{
		loadSceneDc[sceneName] = true;
		Hashtable content = getObjInDL(sceneName);
//		Hashtable content = mustBeData[sceneName];
		string url = serverURL+content["url"];
		int version = content["version"];
		print(url+"======="+version+"is cache"+Caching.IsVersionCached(url, version));
		WWW w = WWW.LoadFromCacheOrDownload(url, version);
	}else if(loadSceneDc[sceneName] == "progressing"){
		print(sceneName +"is progressing");
	}
	*/
	}

	public static void setBg (string bgName, GameObject gameObj)
	{
		Texture2D bg = Resources.Load ("bg/" + bgName) as Texture2D;
	
		gameObj.renderer.material.mainTexture = bg;
		//UITexture ut = gameObj.GetComponent<UITexture>();
		//ut.mainTexture = bg;
		
		print( string.Format("{0:F2}",Time.realtimeSinceStartup)+"xxxxxxx    bgname="+bgName+" gameObj="+gameObj.name);
		
		
//		SimpleSprite s = gameObj.GetComponent<SimpleSprite> ();
//		s.width = Screen.width * 2;
//		s.height = Screen.height * 2;
		//resize and move battle bg
		if (StaticData.isiPhone5) {
			SimpleSprite ss = gameObj.GetComponent<SimpleSprite> ();
			ss.SetSize (1137, 1137);
			gameObj.transform.position.SetY(gameObj.transform.position.y - 31.875f);
//		gameObj.transform.position.y -= 31.875f;
			BoxCollider bc = gameObj.GetComponent<BoxCollider> ();
			bc.center.SetY(bc.center.y - 31.875f);
//		bc.center.y -= 31.875f;
		} else {
			gameObj.transform.position.SetY(0);
//		gameObj.transform.position.y = 0;
		}
//	w.assetBundle.Unload(false);
//	w.Dispose();
//	w = null;
	}

	public bool objectIsCached (string objName)
	{
		//scenename , bgName, heroType
		Hashtable content = getObjInDL (objName);
		string url = serverURL + content ["url"];
		int version = int.Parse (content ["version"].ToString ());
		if (Caching.IsVersionCached (url, version)) {
			return true;
		} else {
			highPriorityList.Add (new Hashtable (){{"url",url },{ "version",version}});
			return false;
		}
	}

	public bool bgIsCached (string bgName)
	{
		Hashtable content = getObjInDL (bgName);
		string url = serverURL + "bg/" + bgName + ".imageSH";//+content["url"];
		int version = int.Parse (content ["version"].ToString ());
		if (Caching.IsVersionCached (url, version)) {
			Debug.Log ("was not cached:" + url);
			return true;
		} else {
			Debug.Log ("was not cached:" + url);
			highPriorityList.Add (new Hashtable (){{"url",url },{ "version",version}});
			return false;
		}
	}

	public bool enemyIsCached (ArrayList enemyList)
	{
		bool isAllDownLoad = true;
		for (int i=0; i<enemyList.Count; i++) {
//		Debug.Log(enemyList[i]);
			string url = "";
			Hashtable content = null;
			if (enemyList [i] is ArrayList) {
				ArrayList list2 = enemyList [i] as ArrayList;
				for (int j=0; j<list2.Count; j++) {
					string enemyType1 = list2 [j].ToString ();
					if (getObjInDL (enemyType1) == null) {
						Debug.LogError ("null enemy type: " + enemyType1);
					}
					content = getObjInDL (enemyType1);
					url = serverURL + content ["url"];
					if (StaticData.isTouch4) {
						url = serverURL + "itouch/" + content ["url"];
					}
					int version = int.Parse (content ["version"].ToString ());
					if (!Caching.IsVersionCached (url, version)) {
						Debug.Log ("was not cached:" + url);
						highPriorityList.Add (new Hashtable (){{"url",url },{ "version",version}});
						isAllDownLoad = false;
					}
				}
			} else {
				string enemyType = enemyList [i].ToString ();
				Hashtable content1 = getObjInDL (enemyType);
				string url1 = serverURL + content1 ["url"];
				if (StaticData.isTouch4) {
					url = serverURL + "itouch/" + content1 ["url"];
//				url = serverURL+"itouch/"+content["url"];
				}
				int version1 = int.Parse (content1 ["version"].ToString ());
				if (!Caching.IsVersionCached (url1, version1)) {
					Debug.Log ("was not cached:" + url1);
					highPriorityList.Add (new Hashtable (){{"url",url1 },{ "version",version1}});
					isAllDownLoad = false;
				}
				Debug.Log ("was not cached:" + enemyType);
			}
		
		}
		return isAllDownLoad;
	}

	public void downloadHighPriorityRes ()
	{
		isHighPriDownload = true;
//	WWW tempW = w;
//	w = null;
//	tempW.Dispose();
//	tempW = null;
//	
//	highPriDownloadContent(GData.hasNetwork());
	
		if (!downloadStarted) {
			Debug.LogWarning ("have not download");
			StartCoroutine (highPriDownloadContent (StaticData.hasNetwork ()));
		}
	}

	public static Hashtable getObjInDL (string keyName)
	{
//	for (int i = 0; i < mustBeData.Keys.Count; i++)
		foreach (string key in mustBeData.Keys) {
//		Debug.Log("outer key: "+key);
//		string key = mustBeData.Keys[i];
//		if (mustBeData[key] instanceof Hashtable) {
			if ((key == "serverURL") || (key == "Video")) {
				continue;
			}
			Hashtable stateHash = mustBeData [key] as Hashtable;
			foreach (string key1 in stateHash.Keys) {
//				Debug.Log("inner key: "+key1);
				if (keyName == key1) {
//					Debug.Log("target = inner key: "+keyName);
					return stateHash [key1] as  Hashtable;
				}
			}
//		}
		}
		return null;
	}

	public static void clearPrbDc ()
	{
		if (prbDc == null)
			return;
		foreach (string key in prbDc.Keys) {
			AssetBundle assetBld = prbDc [key] as AssetBundle;
			assetBld.Unload (true);
		}
		prbDc.Clear ();
	}

	public static string mixKey = "_sfh_";

	public static bool isSaveToLocal (string fileName, string version, bool isMix)
	{ 
		if (isMix) {
			Debug.Log (Application.persistentDataPath + "/" + version + mixKey + fileName);
			return System.IO.File.Exists (Application.persistentDataPath + "/" + version + mixKey + fileName);
		} else {
			Debug.Log (Application.persistentDataPath + "/" + version + fileName);
			return System.IO.File.Exists (Application.persistentDataPath + "/" + version + fileName);
		}
	}

	public static void saveToLocal (string fileName, string content, string version, string originalName, bool isMix)
	{
		string filePath1;
		if (isMix) {
			filePath1 = Application.persistentDataPath + "/" + version + mixKey + fileName;
		} else {
			filePath1 = Application.persistentDataPath + "/" + version + fileName;
		}
	
		string[] fileList = System.IO.Directory.GetFiles (Application.persistentDataPath);
		for (int i=0; i<fileList.Length; i++) {
			string fileN = fileList [i];
			if (fileN.Contains (originalName)) {
				File.Delete (fileN);
			}
		}
		try {
			System.IO.File.WriteAllText (filePath1, content);
		} catch (Exception e) {
			System.IO.File.Delete (filePath1);
		}
	}

	public static void saveToLocal (string fileName, byte[] content, string version, string originalName, bool isMix)
	{
		string filePath1;
		if (isMix) {
			filePath1 = Application.persistentDataPath + "/" + version + mixKey + fileName;
		} else {
			filePath1 = Application.persistentDataPath + "/" + version + fileName;
		}
	
		string[] fileList = System.IO.Directory.GetFiles (Application.persistentDataPath);
		for (int i=0; i<fileList.Length; i++) {
			string fileN = fileList [i];
			if (fileN.Contains (originalName)) {
				File.Delete (fileN);
			}
		}
		try {
			System.IO.File.WriteAllBytes (filePath1, content);
		} catch (Exception e) {
			Debug.LogError ("Error writing file to disk: " + e.Message);
			System.IO.File.Delete (filePath1);
		}
	}
//public static function setBg( string bgName ,   GameObject gameObj  )
//{  
//	print("Cache"+gameObj);
//	WWW www = new WWW ("http://192.168f.2.105f/gameData/bg/"+bgName+".png");
////		WWW w = WWW.LoadFromCacheOrDownload("http://192.168f.2.105f/gameData/bg/"+bgName+".png",1);
//	Debug.Log("!!!!dsadsadsadsadsadsa!!!!!");
////		yield return www; 
//	Debug.Log("~~~~~~~~!");
//	gameObj.renderer.material.mainTexture = www.texture;
////		return w.texture;
//}

}
