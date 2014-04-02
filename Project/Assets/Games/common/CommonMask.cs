using UnityEngine;
using System.Collections;

public class CommonMask : MonoBehaviour {

	// Use this for initialization
	public UISprite maskSp;
	public UISprite loadingSp;
	private static CommonMask _instance;
	private static int counter = 0;
	public int skip = 5;
	public int speed = -24;
	void Start () {
	
	}

	// Update is called once per frame
	private int delay = 0;
	void Update () {
		delay = (delay+1)%skip;
		if(delay == 0)
	    	loadingSp.transform.Rotate(0,0,speed);
//	    	loadingSp.transform.Rotate(0,0,-Time.deltaTime * 200);
	}
	
	public static void show(){
		if(counter == 0){
			GameObject prefab = (GameObject)Resources.Load("CommonMask") ;
			GameObject go = Instantiate(prefab) as GameObject;
			_instance = go.GetComponent<CommonMask>();
		}
		counter++;
	}
	
	public static void hide(){
		counter--;
		if(counter < 0){
			Debug.LogError("hide error");
		}
		if(counter == 0)
		    Destroy(_instance.gameObject);
	}
	

	
}
