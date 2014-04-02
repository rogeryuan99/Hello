using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPathManager {
	
	Dictionary<string, string> mDictionary = new Dictionary<string, string>();
	private static MusicPathManager _instance;
	public static MusicPathManager instance{
		get{
			if(_instance == null){
				_instance = new MusicPathManager();	
			}
			return _instance;
		}
	}
	private MusicPathManager(){
		TextAsset musicPath = Resources.Load("audioSources/MusicPath") as TextAsset;
		ByteReader reader = new ByteReader(musicPath);
		mDictionary = reader.ReadDictionary();	
	}
	public string Get(string key){
		string val;
		if (mDictionary.TryGetValue(key, out val)) return val;
		return key;
	}
}
