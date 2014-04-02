using UnityEngine;
using System.Collections;

public class UVAnim : MonoBehaviour {
  	public float speed;
	private float xx = 0;
	void Update () {
		UITexture uit = this.GetComponent<UITexture>();
		Rect r =  uit.uvRect;
		r.x += speed*Time.deltaTime;
		uit.uvRect = r;
	}
	
}
