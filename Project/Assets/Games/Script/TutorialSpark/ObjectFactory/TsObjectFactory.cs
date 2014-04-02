using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TsObjectFactory: MonoBehaviour {

	public enum TYPE{
		NORMAL, HERO, ENEMY
	}
	
	// Functions
	// -Publics
	
	public static GameObject CreateGameObject(TsCreationDef def){
		string fullName = GetFullName(def);
		GameObject obj = GetFactory(def.Type).Create(fullName);
		
		if (null != obj){
			obj.name = def.Obj;
			obj.transform.parent = GetParentTransform(def.Parent);
			if(def.Type != TYPE.ENEMY && def.Type!= TYPE.HERO){
				obj.transform.localScale = new Vector3(def.Scale, def.Scale, 1f);
			}
		}
		else{
			Debug.LogError(string.Format("{0} can not Create.\nCall in CreateGameObject", fullName));
		}
		
		return obj;
	}
	private static Hashtable hidenObject = new Hashtable();
		
	public static GameObject GetGameObject(string objName){
//		Debug.LogError(string.Format("objName: {0}", objName));
		return GameObject.Find(objName);
	}
	
	public void ReleaseGameObject(string[] parms){
		for (int i=0; i<parms.Length; i++){
			GameObject obj = GetGameObject(parms[i]);
			obj.name = "NullObject";
			Destroy(obj);
		}
	}
	public void HideGameObject(string[] parms){
		for (int i=0; i<parms.Length; i++){
			GameObject obj = GetGameObject(parms[i]);
			if (null != obj) {
				obj.SetActive(false);
				if(hidenObject.Contains(parms[i])){
					hidenObject[parms[i]] = obj;
				}else{
					hidenObject.Add(parms[i],obj);
				}
			}
		}
		
	}
	public void ShowGameObject(string[] parms){
		for (int i=0; i<parms.Length; i++){
			GameObject obj = hidenObject[parms[i]] as GameObject;
			if (null != obj){
				obj.SetActive(true);
			}
			else{
				Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function ShowGameObject.", parms[i]));
			}
		}
	}
	
	
	// -Privates
	
	private static Transform GetParentTransform(string name){
		GameObject uiRoot = GameObject.Find(name);
		Transform parent = null;
		
		if (null != uiRoot){
			parent = uiRoot.transform;
		}
		
		return parent;
	}
	
	private static string GetFullName(TsCreationDef def){
		bool noPrefix = string.IsNullOrEmpty(def.Prefix);
		bool hasSlash = (!noPrefix && '/' == def.Prefix[def.Prefix.Length-1]);
		
		return (def.Prefix + (hasSlash || noPrefix? "": "/") + def.Obj);
	}
	
	private static TsIFactory GetFactory(TYPE type){
		TsIFactory iFactory = null;
		
		if      (TYPE.NORMAL == type) iFactory = new TsNormalObject();
		else if (TYPE.HERO   == type) iFactory = new TsHeroFactory();
		else if (TYPE.ENEMY  == type) iFactory = new TsEnemyFactory();
		
		return iFactory;
	}
}
