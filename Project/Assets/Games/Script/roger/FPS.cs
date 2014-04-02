using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
	float updateInterval = 0.5f;
	string fpsString;
	public static string InfoString;
	float accum = 0.0f; // FPS accumulated over the interval
	float frames = 0; // Frames drawn over the interval
	float timeleft;
	float fps = 0;
	// Use this for initialization

	void Start ()
	{
		DontDestroyOnLoad(this.gameObject);
		Application.targetFrameRate = 30;
		timeleft = updateInterval;  
	}

	void Update ()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		fps = accum / frames;

		if (timeleft <= 0.0) {
			fpsString = System.String.Format ("{0:f2}", fps);
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
        {
			Time.timeScale *=0.5f;
		}
		if (Input.GetKey(KeyCode.RightArrow))
        {
			Time.timeScale *=2f;
		}
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (0, 30, 300, 100), "FPS: " + fpsString);
		GUI.Label (new Rect (0, 60, 500, 100), "FPS: " + InfoString);
		GUI.Label (new Rect (0, 80, 300, 100), string.Format("Current Level {0}:{1}",MapMgr.Instance.currentChapterIndex,MapMgr.Instance.currentLevelIndex));
//		if(GUI.Button(new Rect(0, 100, 100, 100),"inactive")){
//			panel.gameObject.SetActive(false);
//		}
//		if(GUI.Button(new Rect(0, 300, 100, 100),"active")){
//			panel.gameObject.SetActive(true);
//		}
	}
	
}