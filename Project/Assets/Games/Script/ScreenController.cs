using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour{

	private static ScreenController _instance;
	public static ScreenController Instance{
		get{
			if (null == _instance){
				GameObject obj = new GameObject("ScreenController");
				_instance = obj.AddComponent<ScreenController>();
			}
			return _instance;
		}
	}
	
	private GameObject blackObj;
	
	
	// Functions 
	// -Publics
	public void EnableBlackScreen(){
		if (null == blackObj){
			blackObj = GetBlackScreenObj();
			// blackObj.transform.parent = GameObject.Find("UIRoot/GamePanel").transform;
		}
		blackObj.SetActive(true);
	}
	
	public void DisableBlackScreen(){
		blackObj.SetActive(false);
	}
	
	
	// -Privates 
	private GameObject GetBlackScreenObj(){
		Object blackPrefab = Resources.Load("eft/BlackScreen");
		if (!blackPrefab) Debug.LogError("None Prefab: eft/BlackScreen");
		return (Instantiate(blackPrefab) as GameObject);
	}
}
