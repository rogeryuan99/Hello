using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	public float interval;
	private float cumulateTime;
	// Update is called once per frame
	void Update () {
		cumulateTime += Time.deltaTime;
		if(cumulateTime>interval){
			shakeCamera(new Vector3(0,-20,0),1f);
			cumulateTime = 0;
		}
	}
	public void shakeCamera(Vector3 pos, float time)
	{
		Camera c = Camera.mainCamera;
		GameObject go = c.gameObject;
		iTween.PunchPosition(go,iTween.Hash("amount",pos,"time",time));
	}
	
}
