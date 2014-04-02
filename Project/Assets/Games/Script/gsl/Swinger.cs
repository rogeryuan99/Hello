using UnityEngine;
using System.Collections;

public class Swinger : MonoBehaviour {
	public float speed = 2f;
	public float angle1 = 0;
	public float angle2 = 90;
	public float phase = 100f;
	public Style style =  Style.Sine;
	private float cumulateTime = 0;
	public enum Style{
		Sine
	}
	
	void Update () {
		cumulateTime += Time.deltaTime * speed;
		if(style == Style.Sine){
			float angle = Mathf.Lerp(angle1,angle2, .5f +.5f*Mathf.Sin(phase+cumulateTime));
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,angle));	
		}
	}
	
}