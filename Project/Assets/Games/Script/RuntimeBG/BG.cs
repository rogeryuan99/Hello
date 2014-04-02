using UnityEngine;
using System.Collections;

public class BG : MonoBehaviour {


public GameObject sun;

public GameObject topHalf;
public GameObject bottomHalf;

public void Start (){
	BGMeshColor topHalfMC = topHalf.GetComponent<BGMeshColor>();
	BGMeshColor bottomHalfMC = bottomHalf.GetComponent<BGMeshColor>();
	if (topHalfMC) {
		topHalfMC.setColor();
	}
	if (bottomHalfMC) {
		bottomHalfMC.setColor();
	}
	setColorOfSun();
}

public void setColorOfSun (){
	if (sun) {
		switch(StaticData.difLevel) {
			case 1:	
				sun.renderer.material.SetColor("_Emission", new Color32(127, 127, 127, 255));
				break;
			case 2:
				sun.renderer.material.SetColor("_Emission", new Color32(200, 100, 0, 255));
				break;
			case 3:
				sun.renderer.material.SetColor("_Emission", new Color32(255, 0, 0, 255));
				break;
			default:
				sun.renderer.material.SetColor("_Emission", new Color32(127, 127, 127, 255));
				break;
		}
	}
}

public void Update (){

}
}