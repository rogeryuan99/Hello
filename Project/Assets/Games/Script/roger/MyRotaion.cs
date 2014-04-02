using UnityEngine;
using System.Collections;

public class MyRotaion : MonoBehaviour {
	public Vector3 speed = new Vector3(0,0,10);
	void Update(){
		this.transform.Rotate(speed);	
	}
}
