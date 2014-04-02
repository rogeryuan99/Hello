using UnityEngine;
using System.Collections;

public class Autorotation : MonoBehaviour {
	public enum AIX{
		X,Y,Z	
	}
	public float speed;
	public AIX aix;
	private float angle = 0f;
	
	public void Start(){
		
	}
	public void Update(){
		angle+=speed;
		switch(aix){
		case AIX.X:
			this.transform.localRotation = Quaternion.Euler(new Vector3(angle,0f,0f));	
			break;
		case AIX.Y:
			this.transform.localRotation = Quaternion.Euler(new Vector3(0f,angle,0f));	
			break;
		case AIX.Z:
			this.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,angle));	
			break;
		}
	}
}
