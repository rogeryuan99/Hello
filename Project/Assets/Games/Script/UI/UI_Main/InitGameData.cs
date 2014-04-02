using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class InitGameData : MonoBehaviour
{
	public static InitGameData instance;
	
	// Jugg
	public GameObject cacheMgrPrb;
	
	// Jugg
	
	private static bool  initDataComplete = false;

	public void  Awake ()
	{
		instance = this;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	public void  Start ()
	{
		StartCoroutine(delayedStart());	
	}
		
	private IEnumerator delayedStart(){
		yield return new WaitForSeconds(0.1f);
		if (!initDataComplete) {
			StartUpManager.Instance.Begin();
		}
	}

	public void initComplete(){
		initDataComplete = true;
		GotoProxy.gotoScene(GotoProxy.UIMAIN);
		Destroy(this);
	}
	
	public void  initHerosFromStaticDef ()
	{
		if ( UserInfo.heroDataList.Count < 1) {
			XmlNodeList heroXmlList = StaticData.getHeroXML ();
			for (int i=0; i<heroXmlList.Count; i++) {
				HeroData heroD = HeroData.createWithStaticDefXml(heroXmlList[i]);
				UserInfo.heroDataList.Add (heroD);
			} 			
		}
	}
	public void initHerosFromDynamicData(){
		foreach( HeroData heroD in UserInfo.heroDataList){
			heroD.loadDynamicData();	
		}
	}
	public void initXplevel(ICollection l){
		List<int> expList = new List<int>(l.Count);
		for(int n = 0; n<l.Count;n++){
			expList.Add(0);	
		}
		foreach( Hashtable h in l){
			int lv = int.Parse(h["uid"] as string);
			int xp = int.Parse(h["xp"] as string);
			expList[lv-1] = xp;
		}
		HeroData.expList = expList;
	}
	public void initHerosFromJson(ICollection al){
		foreach(Hashtable h in al){
			HeroData heroD = HeroData.createWithStaticDefJson(h);
			UserInfo.heroDataList.Add (heroD);
		}
		UserInfo.heroDataList.Sort(sortByDisplayIndex);
		
		Debug.Log(Utils.dumpList(UserInfo.heroDataList));
	}
	
	private int sortByDisplayIndex(HeroData heroD1, HeroData heroD2) 
	{ 
		if(heroD1.displayIndex >= heroD2.displayIndex)
		{
			return 1;
		}
		else if(heroD1.displayIndex < heroD2.displayIndex)
		{
			return -1;
		}
		return 0;
	}

} 
 
