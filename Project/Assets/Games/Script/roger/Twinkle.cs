using UnityEngine;
using System.Collections;

public class Twinkle : MonoBehaviour {
	public float speed;
	public float phase = 0;
	private UISprite sp = null;
	
	// Update is called once per frame
	void Update () {
		if(sp == null){
			sp = this.GetComponent<UISprite>();
		}
		phase += Time.deltaTime*speed;
		sp.alpha = Mathf.Sin(phase)*.5f + 0.5f;
	}
}
