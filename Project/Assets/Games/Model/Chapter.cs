using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chapter{
	//static
	public int id;
	public int passStars;
	public string name;
	public List<Level> levels = new List<Level>();
	//dynamic
	public int winStars{
		get{
			int n = 0;
			foreach(Level lv in levels){
				n+= lv.winStars;
			}
			return n;
		}
	}
	public override string ToString ()
	{
		string s = "";
		foreach(Level lv in levels){
			s += lv+"\n";
		}
		return string.Format ("[Chapter: id:{0} name:{1} winStars:{2}\n levels:\n{3} ]",id,name, winStars,s);
	}
	
	public Level getLevelByID(int id){
		foreach(Level lv in levels){
			if(lv.id == id){
				return lv;
			}
		}
		return null;
	}
	public bool isUnlocked(){
		Chapter pre = MapMgr.Instance.getChapterByID(this.id -1);
		if(pre == null){
			return true;
		}else{
			return pre.winStars >= passStars && pre.getLevelByID(12).winStars>0 ;	
		}
	}
	
	public ArrayList dumpDynamicData(){
		ArrayList result = new ArrayList();
		foreach(Level lv in levels){
			result.Add(lv.dumpDynamicData());
		}
		return result;
	}
	public void loadDynamicData(object o){
		ArrayList a = o as ArrayList;
		if(a == null){
			Debug.LogError(" null chapter info");	
			return;
		}
		for(int n = 0;n<a.Count;n++){
			levels[n].loadDynamicData(a[n]);	
		}
	}	
}
