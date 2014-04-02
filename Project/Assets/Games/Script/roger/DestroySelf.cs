using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {
	// to work with message, like itween.
	void destroySelf(){
		Destroy(this.gameObject);
	}
}
