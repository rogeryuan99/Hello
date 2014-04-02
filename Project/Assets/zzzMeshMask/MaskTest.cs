using UnityEngine;
using System.Collections;

public class MaskTest : MonoBehaviour {
	
	public float x,y;
	MaskScreen mask;
	
	// Use this for initialization
	void Start () {
			mask = gameObject.GetComponent<MaskScreen>();
			mask.changeMaskPositon(50,50);
			mask.maskSizeW = Screen.width * (mask.maskSizeW /1024);
			mask.maskSizeH = Screen.height * (mask.maskSizeH /768);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
