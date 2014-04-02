using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
	public enum Boss{
		None,
		MiniBoss,
		finalBoss
	}
	public int id;
	public string name;
	public int winStars;
	public Chapter chapter;
	public string bgMusic;
	public Boss boss = Boss.None;
	public bool pass {
		get {
			return winStars > 0;
		}
	}
	
	public List<HazardDef> hazardDefs = new List<HazardDef>();
	public List<int> preLvIds;
	public List<int> postLvIds;
	public ArrayList wave;
	public override string ToString ()
	{
		string pre = "";
		if (preLvIds != null) {
			foreach (int n in preLvIds) {
				if(pre.Length>0) pre +=",";
				pre += n;
			}
		}
		string post = "";
		if (postLvIds != null) {
			foreach (int n in postLvIds) {
				if(post.Length>0) post +=",";
				post += n;
			}
		}
		return string.Format ("Level-{0}, pre:{1}, post:{2}", id, pre, post);
	}
	
	
	public bool isUnlocked(){
		if(preLvIds == null) return true;
		foreach(int n in preLvIds){
			Level preLv = chapter.getLevelByID(n);
			if(!preLv.pass){
				return false;	
			}
		}
		return true;
	}
	
	public object dumpDynamicData(){
		return winStars;
	}	
	
	public void loadDynamicData(object o){
		 int.TryParse(o+"",out winStars);
	}
}
