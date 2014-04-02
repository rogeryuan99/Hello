using UnityEngine;
using System.Collections;

public class loading : MonoBehaviour {

void Start (){
}
void Update (){
	transform.Rotate(Vector3.back, Time.deltaTime * 120);
}
}