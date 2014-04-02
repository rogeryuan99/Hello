using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TsDynamicData {
	//public static string path = Application.persistentDataPath+"/tsdata"+BuildSetting.LocalSaveVersion+".xml";
	private static TsDynamicData _instance;
	public static TsDynamicData instance {
		get { 
			if (_instance == null) {
				_instance = new TsDynamicData ();
			}
			return _instance;
		}
	}
	
	public TsDynamicData ()
	{
	}
	public ArrayList dumpDynamicData(){
		ArrayList result = new ArrayList();
		foreach(string s in ids){
			result.Add(s);
		}
		return result;
	}
	public void loadDynamicData(object o){
		Debug.Log(o);
		ArrayList a = o as ArrayList;
		if(a == null){
			Debug.LogError(" null ftue info");	
			return;
		}
		ids.Clear();
		for(int n = 0;n<a.Count;n++){
			ids.Add(a[n] as string);
		}
	}	
	public static void reset ()
	{
		_instance = new TsDynamicData ();
		UserInfo.instance.saveAll();
	}

	/// <summary>
	private List<string> ids = new List<string>(); 
	private List<string> finishedIdsCache = new List<string>();
	private List<string> branches = new List<string>();
	
	public bool CheckHasDoneBefore(string[] ids_){
		bool result = false;
		
		for (int i=0; i<ids_.Length; i++){
			if (ids.Contains(ids_[i]) || finishedIdsCache.Contains(ids_[i])){
				result = true;
				i = ids_.Length;
			}
		}
		
		return result;
	}
	
	public bool CheckPreconditions(string[] ids_){
		bool result = true;
		
		for (int i=0; i<ids_.Length; i++){
			if (!ids.Contains(ids_[i]) && !finishedIdsCache.Contains(ids_[i])){
				result = false;
				i = ids_.Length;
			}
		}
		
		return result;
	}
	
	public bool CheckBranches(){
		bool result = (branches.Count > 0)? true: false;
		
		for (int i=0; i<branches.Count; i++){
			if (!ids.Contains(branches[i]) && !finishedIdsCache.Contains(branches[i])){
				result = false;
				i = branches.Count;
			}
		}
		
		return result;
	}
	
	public void FinishId(string[] ids_){
		for (int i=0; i<ids_.Length; i++){
			if (!string.IsNullOrEmpty(ids_[i])){
				ids.Add(ids_[i]);
			}
		}
	}
	
	public void AddBranches(string[] ids_){
		if (null == ids_) return; 
		
		for (int i=0; i<ids_.Length; i++){
			if (!string.IsNullOrEmpty(ids_[i])){
				branches.Add(ids_[i]);
			}
		}
	}
	
	public void CacheFinishedId(string[] ids_){
		for (int i=0; i<ids_.Length; i++){
			if (!string.IsNullOrEmpty(ids_[i])){
				finishedIdsCache.Add(ids_[i]);
			}
		}
	}
	
	public void ClearCache(){
		ids.AddRange(finishedIdsCache);
		finishedIdsCache.Clear();
	}
	
	public void ClearBranches(){
		branches.Clear();
	}
}
