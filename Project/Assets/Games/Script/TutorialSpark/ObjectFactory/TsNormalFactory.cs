using UnityEngine;
using System.Collections;

public class TsNormalObject: TsIFactory {
	
	public GameObject Create(string fullName){
		GameObject obj = null;
		Object prefab = Resources.Load(fullName);
		if (null != prefab){
			obj = (GameObject)MonoBehaviour.Instantiate(prefab);
		}
		else{
			Debug.LogError("!!!! Prefab is not Exist: " + fullName);
		}
		
		return obj;
	}
}
