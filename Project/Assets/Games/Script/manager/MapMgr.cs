using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapMgr{
	private static MapMgr _Instance;
	
	public static MapMgr Instance{
		get{
			if(_Instance == null){
				_Instance = new MapMgr();
			}
			return _Instance;
		}
	}
	
	public int currentChapterIndex = 1;
	public int currentLevelIndex = 1;
	
	public bool isArena{
		get{
			return (currentLevelIndex == 100);	
		}
	}
	
	public List<Chapter> chapters;
	
	public void parseChapterFromArtooJson(ICollection al){
		chapters = new List<Chapter>();
		foreach(Hashtable chash in al){
			Chapter c = new Chapter();
			chapters.Add(c);
			c.id = int.Parse(chash["uid"] as string);
			c.name = chash["name"] as string;
			c.passStars = int.Parse(chash["passStars"] as string); 
		}
		
		chapters.Sort(delegate(Chapter x, Chapter y){
			return (x.id < y.id) ? -1 : 1;
		});
		
		foreach(Chapter c in chapters){
			Debug.Log("chapter "+c.id+"  "+c.name);	
		}		
	}
	public void parseLevelFromArtooJson(ICollection al){
		foreach(Hashtable lvhash in al){
			Level lv = new Level();
			string uid = lvhash["uid"] as string;
			string[] cl =uid.Split('_');
			int cid = int.Parse(cl[0]);
			int lid = int.Parse(cl[1]);
			lv.id = lid;
			Chapter c = this.getChapterByID(cid);
			lv.chapter = c;
			lv.preLvIds = Utils.parseCommaSeperatedInt( lvhash["preLevel"] == null? null:lvhash["preLevel"].ToString());
//			if(lv.preLvIds!=null) Debug.Log("preLvIds of "+uid+":"+Utils.dumpList(lv.preLvIds));
			lv.wave = MiniJSON.jsonDecode(lvhash["wave"] as string) as ArrayList;
			lv.bgMusic = lvhash["bgMusic"] as string;
			c.levels.Add(lv);
			
			string hazardStr = lvhash["hazard"] as string;
			if(hazardStr != null)
			{
				ArrayList hazardList = MiniJSON.jsonDecode(lvhash["hazard"] as string) as ArrayList;
				if(hazardList != null)
				{
	//				Debug.LogError( lvhash["hazard"] as string);
	//				Debug.LogError( "  "+MiniJSON.jsonDecode(lvhash["hazard"] as string));
	//				Debug.LogError(Utils.dumpList(hazardList));
					
					foreach(Hashtable hazardTable in hazardList)
					{
	//					Debug.LogError(Utils.dumpHashTable(hazardTable));
						ReadHazardDefs(hazardTable, lv.hazardDefs);
					}
					
				}
			}
			lv.postLvIds = new List<int>();
		}
		
		
		foreach(Chapter c in chapters){
			c.levels.Sort(delegate(Level x, Level y){
				return (x.id < y.id) ? -1 : 1;
			});
			
			foreach(Level lv in c.levels){
				if(lv.preLvIds!=null){
					foreach(int prelvid in lv.preLvIds){
						Level prelv = c.getLevelByID(prelvid);
						if(prelv.postLvIds==null){
							prelv.postLvIds = new List<int>();	
						}
						prelv.postLvIds.Add(lv.id);
					}
				}
//				Debug.Log("Chapter-level "+c.id + " - "+lv.id);	
			}	
		}	
	}
	public void parseStaticFromJson(string jsontext){
		ArrayList allData = MiniJsonExtensions.arrayListFromJson(jsontext);
		chapters = new List<Chapter>();
		foreach(Hashtable chash in allData){
			// Debug.LogError(string.Format("Name: {0}", chash["name"] as string));
			Chapter c = new Chapter();
			chapters.Add(c);
			c.id = (int)(double)chash["chapter"];
			c.name = chash["name"] as string;
			c.passStars = (int)(double)chash["passStars"];
			ArrayList lvList = chash["levels"] as ArrayList;
			foreach(Hashtable lvhash in lvList){
				Level lv = new Level();
				lv.id = (int)(double)lvhash["level"];
				lv.preLvIds = Utils.parseCommaSeperatedInt( lvhash["preLevel"] == null? null:lvhash["preLevel"].ToString());
				lv.wave = lvhash["wave"] as ArrayList;
				lv.chapter = c;
				lv.bgMusic = lvhash["bgMusic"] as string;
				c.levels.Add(lv);	
				
				ReadHazardDefs(lvhash, lv.hazardDefs);
				
				if(lv.preLvIds!=null){
					foreach(int prelvid in lv.preLvIds){
						Level prelv = c.getLevelByID(prelvid);
						if(prelv.postLvIds==null){
							prelv.postLvIds = new List<int>();	
						}
						prelv.postLvIds.Add(lv.id);
					}
				}
				
			}
			
			
			
		}
//		foreach(Chapter c in chapters){
//			Debug.Log(c);	
//		}
	}
	public void dumpLevelJsonForArtoo(){
		string s = "";
		foreach(Chapter c in chapters){
			foreach(Level l in c.levels){
				s+="{ ";
				s+= "\"uid\":\""+c.id+"_"+l.id+"\",";	
				s+= "\"bgMusic\":\""+l.bgMusic+"\",";	
				if(l.preLvIds!=null)s+= "\"preLevel\":\""+l.preLvIds[0]+"\",";	
				string w = MiniJSON.jsonEncode(l.wave);
				w = w.Replace("\"","'");
				s+= "\"wave\":\""+w+"\" ";	
				s+="},\n";
			}
		}
		Debug.Log(s);
	}
	
	public void ReadHazardDefs(Hashtable hazardDefTable, List<HazardDef> list)
	{
		string type = hazardDefTable["type"] as string; 
			
		HazardDef hd = null;
		if(type.ToUpper() == HazardDef.HazardType.Cannon.ToString().ToUpper())
		{
			hd = new CannonDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.FireVent.ToString().ToUpper())
		{
			hd = new FireVentDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.TrapDoor.ToString().ToUpper())
		{
			hd = new TrapDoorDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.SentryGun.ToString().ToUpper())
		{
			hd = new SentryGunDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.LandMine.ToString().ToUpper())
		{
			hd = new LandMineDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.EnergyPole.ToString().ToUpper())
		{
			hd = new EnergyPoleManagerDef();
		}
		else if(type.ToUpper() == HazardDef.HazardType.PowerUp.ToString().ToUpper())
		{
			hd = new PowerUpManagerDef();
		}
		
		if(hd != null)
		{
			hd.parserAttributes(hazardDefTable, (HazardDef.HazardType)Enum.Parse(typeof(HazardDef.HazardType), type));
			list.Add(hd);
		}
			
		
	}
	
	public Chapter getChapterByID(int id){
		foreach(Chapter c in chapters){
			if(c.id == id){
				return c;	
			}
		}
		return null;
	}
	
	public void nextLevel(){
		if(getCurrentChapter().getLevelByID(this.currentLevelIndex +1) == null){
			//next chapter
			selectChapterAndLevel(this.currentChapterIndex+1,1);
		}else{
			//next level
			selectChapterAndLevel(this.currentChapterIndex,this.currentLevelIndex+1);
		}
	}
	
	public void selectChapterAndLevel (int chapterID,int levelID){
		Debug.Log("selectChapterAndLevel:"+chapterID+":"+levelID);
	
		this.currentChapterIndex = chapterID;
		this.currentLevelIndex = levelID;
		//GData.currentLevel=levelID;
	}
	public Chapter getCurrentChapter(){
		return getChapterByID(this.currentChapterIndex);
	}
	public Level getCurrentLevel(){
		return getCurrentChapter().getLevelByID(this.currentLevelIndex);	
	}
	
	public Level getNextLevel()
	{
		if(getCurrentChapter().getLevelByID(this.currentLevelIndex +1) != null){
			return getCurrentChapter().getLevelByID(this.currentLevelIndex+1);
		}
		if(getChapterByID(this.currentChapterIndex+1)!=null && getChapterByID(this.currentChapterIndex+1).getLevelByID(1) != null)
		{
			return getChapterByID(this.currentChapterIndex+1).getLevelByID(1);
		}
		return null;
	}
	
	public void loadDynamicData(object o){
		ArrayList a = o as ArrayList;
		if(a == null){
			Debug.LogError("no dynamic data for MapMgr");	
			return;
		}
		for(int n = 0;n<a.Count;n++){
			chapters[n].loadDynamicData(a[n]);	
		}
	}
	
	public ArrayList dumpDynamicData(){
		ArrayList result = new ArrayList();
		foreach(Chapter ch in chapters){
			result.Add(ch.dumpDynamicData());
		}
		return result;
	}
	
	public void setBonusStarForCurrentLv(int stars){
		getCurrentLevel().winStars = Mathf.Max(getCurrentLevel().winStars , stars);
	}
}
