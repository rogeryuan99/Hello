using UnityEngine;
using System.Collections;

public class PlanetColor : MonoBehaviour {


public Color planetColor;

void Start (){
	gameObject.renderer.material.SetColor("_Emission",planetColor);
}

void Update (){

}
}