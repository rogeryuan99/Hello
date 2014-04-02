using UnityEngine;
using System.Collections;

public class LightEffect : MonoBehaviour {
	public UISprite light;
	
	public float speed = 2f;
	public float lightSpeed = 2f;
	public float angle1 = 0;
	public float angle2 = 90;
	public float phase = 100f;
	public float lightMomentum = 0.8f;
	private float cumulateTime = 0;
	
	void Start () {
		
	}
	
	void Update () {
		cumulateTime += Time.deltaTime;
		float angle = Mathf.Lerp(angle1,angle2, .5f +.5f*Mathf.Sin(phase+cumulateTime*speed));
		this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,angle));	
		light.color = new Color(1f,1f,1f,lightMomentum + (1.0f-lightMomentum)*Mathf.Sin(cumulateTime*lightSpeed));
	}
}
