using UnityEngine;
using System.Collections;

public class AutoDestruct : MonoBehaviour {
	
	public float delay;
	
	public void Start(){
		Invoke("Destruct", delay);
	}
	
	private void Destruct(){
		Destroy(gameObject);
	}
}
