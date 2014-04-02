using UnityEngine;
using System.Collections;

public class SceneLoading : MonoBehaviour {

GameObject pet;
void Start (){
	if(pet == null)
	{
		return;
	}
	iTween.MoveTo(pet, new Hashtable(){{"x",-281},{ "speed",100},{ "looptype","pingPong"},{ "easetype","linear"},{ "onComplete","petMoveComplete"},{ "onCompleteTarget",this.gameObject}});
}

void petMoveComplete (){
	Vector3 v = pet.transform.localScale;
	v.x = -v.x;
	pet.transform.localScale = v;
}

void Update (){

}
}