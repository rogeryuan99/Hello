using UnityEngine;
using System.Collections;

public class difficultyButtonMethods : MonoBehaviour {

//
//[HideInInspector]
//static difficultyManager self;

BG bg;

UIButton difLvl1Button;
UIButton difLvl2Button;
UIButton difLvl3Button;
UIButton difToggleButton;
	
	// Jugg 
UILabel toggleButtonText;

GameObject difficultyObjects;
void setDifficultyNormal (){
	StaticData.difLevel = 1;
	StaticData.difLevel = (int)(StaticData.difLevel > difficultyManager.maxDifficulty?difficultyManager.maxDifficulty:StaticData.difLevel);
	applyDifficultySettings();
}
    
void setDifficultyNightmare (){
	StaticData.difLevel = 2;
	StaticData.difLevel = (int)(StaticData.difLevel > difficultyManager.maxDifficulty?difficultyManager.maxDifficulty:StaticData.difLevel);
	applyDifficultySettings();
}

void setDifficultyHell (){
	StaticData.difLevel = 3;
	StaticData.difLevel = (int)(StaticData.difLevel > difficultyManager.maxDifficulty?difficultyManager.maxDifficulty:StaticData.difLevel);
	applyDifficultySettings();
}

void applyDifficultySettings (){
	setCurrentDifficultyText();
	hideDifficultySelections();
	reInitLevelDetails();
	difficultyManager.saveDifficultySettings();
}

void configurePlanetsAndLevels (){
}

void showDifficultySelections (){
	//int destinationX = 0;
	int destinationY = 0;
	if (difficultyManager.maxDifficulty >= 1) {
		difLvl1Button.gameObject.SetActiveRecursively(true);
		difLvl1Button.gameObject.transform.localPosition.SetY(109);
	}
	if (difficultyManager.maxDifficulty >= 2) {
		difLvl2Button.gameObject.SetActiveRecursively(true);
		difLvl2Button.gameObject.transform.localPosition.SetY(219);
	}
	if (difficultyManager.maxDifficulty >= 3) {
		difLvl3Button.gameObject.SetActiveRecursively(true);
		difLvl3Button.gameObject.transform.localPosition.SetY(328);
	} 
		// Jugg
//	difToggleButton.methodToInvoke = "hideDifficultySelections";
}

void hideDifficultySelections (){
	difLvl1Button.gameObject.SetActiveRecursively(false);
	difLvl2Button.gameObject.SetActiveRecursively(false);
	difLvl3Button.gameObject.SetActiveRecursively(false);
	difLvl1Button.gameObject.transform.localPosition.SetY(0);
	difLvl2Button.gameObject.transform.localPosition.SetY(0);
	difLvl3Button.gameObject.transform.localPosition.SetY(0);
		// Jugg
//	difToggleButton.methodToInvoke = "showDifficultySelections";
}

void setCurrentDifficultyText (){
	//string difText = "";
//	Debug.Log("current dif level: "+GData.difLevel);
	switch (StaticData.difLevel) {
		case 1:
			toggleButtonText.text = "NORMAL";
			break;
		case 2:
			toggleButtonText.text = "NOVA";
			break;
		case 3:
			toggleButtonText.text = "SUPERNOVA";
			break;
		default:
			toggleButtonText.text = "NORMAL";
			break;
	}
	float size = (400/toggleButtonText.text.Length);
		// Jugg
//	toggleButtonText.SetCharacterSize(size>60.0f?55.0f:size);
	if (bg) {
		bg.setColorOfSun();
	}
}

void reInitLevelDetails (){
	Message msg= new Message(MsgCenter.PLANE_STATUS_UPDATE,this);
	MsgCenter.instance.dispatch(msg);
}

void showDifficultyObjects ( bool shouldShow  ){
	 //if (difficultyManager.maxDifficulty > 1) {
	difficultyObjects.SetActiveRecursively(shouldShow && (difficultyManager.maxDifficulty > 1));
	//}
	hideDifficultySelections();
	//difToggleButton.methodToInvoke = "hideDifficultyObjects";
}

void hideDifficultyObjects (){
	showDifficultyObjects(false); 
}
void Start (){
//	self = this;
	
	if (difficultyManager.maxDifficulty > 1) {
		showDifficultyObjects(true);
		setCurrentDifficultyText();
		hideDifficultySelections();
	}else {
		showDifficultyObjects(false);
	}
}
//
//void init (){
//	difLvl1Button = GameObject.Find("Ewoks").GetComponent<UIButton>();
//	difLvl2Button = GameObject.Find("Mandalore").GetComponent<UIButton>();
//	difLvl3Button = GameObject.Find("Wookies").GetComponent<UIButton>();
//	difToggleButton = GameObject.Find("Current").GetComponent<UIButton>();
//}
//
//public static difficultyManager instance (){
//	if (!self) {
//		Debug.Log("no live instance of difficulty manager found!");
//		self = new difficultyManager();
//	}
//	return self;
//}

void Update (){

}
}