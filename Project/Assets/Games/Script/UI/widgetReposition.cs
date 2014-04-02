using UnityEngine;
using System.Collections;

public class widgetReposition : MonoBehaviour {


public GameObject[] backButtons;
//SimpleSprite[] backGrounds;
public GameObject moneyWidget;
public GameObject merchantFG_original;
public GameObject merchantFG_iphone5;
public GameObject learnSkillPanel;

[HideInInspector]
float BACK_BUTTON_X_POSITION = 400.0f;
[HideInInspector]
float BACK_BUTTON_X_POSITION_MAP = 81.0f;
[HideInInspector]
float MONEY_WIDGET_X_POSITION = -411.0f;
[HideInInspector]
float MONEY_WIDGET_X_POSITION_MAP = -152.0f;

void Start (){
	//Debug.Log("start");
	BACK_BUTTON_X_POSITION = 415.0f;
	MONEY_WIDGET_X_POSITION = -95.0f;
	BACK_BUTTON_X_POSITION_MAP = -42.03f;
	MONEY_WIDGET_X_POSITION_MAP = -19.0f;
	//test
	//GData.isPhone = true;
	//test
	if (StaticData.isPhone) {
			GameObject button = null;
		//move back buttons
		for (int i = 0; i < backButtons.Length; i++) {
			button = (GameObject)backButtons[i];
			moveBackButton(button);
		}
		//resize back grounds
		/*
		for (i = 0; i < backGrounds.length; i++) {
			SimpleSprite bg = backGrounds[i];
			if (bg.gameObject.active) {
				resizeBG(bg);
			}else {
				bg.gameObject.active = true;
				resizeBG(bg);
				bg.gameObject.active = false;
			}
		}
		*/
		//move money widget
		moveMoneyWidget();
		//enlarge and move merchant foreground if is merchant scene
		//enlargeMerchantForeground();
		//enlarge learn skill panel in skill tree scene
		//enlargeLearnSkillPanel();
	}else{
			GameObject button = null;
		if (GotoProxy.getSceneName() != GotoProxy.MAP) {
			  float widthInInches = Screen.width/(Screen.height/320.0f);
			  	for (int j = 0; j < backButtons.Length; j++) {
					button = (GameObject)backButtons[j];
					button.transform.localPosition = new Vector3(-widthInInches + 50, button.transform.localPosition.y, button.transform.localPosition.z);
//					button.transform.localPosition.x = -widthInInches + 50;
				}		
		}
	}

}

void moveBackButton ( GameObject button  ){
	if (GotoProxy.getSceneName() == GotoProxy.MAP) {
		button.transform.localPosition = new Vector3(BACK_BUTTON_X_POSITION_MAP, button.transform.localPosition.y, button.transform.localPosition.z);
//		button.transform.localPosition.x = BACK_BUTTON_X_POSITION_MAP;
	}else {
		if (GotoProxy.getSceneName() == GotoProxy.COMBINED_ARMORY) {
				button.transform.localPosition = new Vector3(-BACK_BUTTON_X_POSITION, button.transform.localPosition.y, button.transform.localPosition.z);
//			button.transform.localPosition.x = -BACK_BUTTON_X_POSITION;
		}else {
				button.transform.localPosition = new Vector3(-BACK_BUTTON_X_POSITION, button.transform.localPosition.y, button.transform.localPosition.z);
//			button.transform.position.x = -BACK_BUTTON_X_POSITION;
		}
	}
}

void resizeBG ( SimpleSprite bg  ){
	if (GotoProxy.getSceneName() == GotoProxy.MAP) {
			bg.gameObject.transform.localScale= new Vector3(1.1875f, 1.1875f, bg.gameObject.transform.localScale.z);
//		bg.gameObject.transform.localScale.x = 1.1875f;
//		bg.gameObject.transform.localScale.y = 1.1875f;
	}else {
		bg.SetSize(1137,1137);
	}
	/*
	if (GotoProxy.getSceneName() == GotoProxy.MAIN_MENU) {
		bg.gameObject.transform.position.y -= 60;
	}
	*/
}

void moveMoneyWidget (){
	if (moneyWidget) {
		if (GotoProxy.getSceneName() == GotoProxy.MAP) {
				moneyWidget.transform.localPosition = new Vector3(MONEY_WIDGET_X_POSITION_MAP, moneyWidget.transform.localPosition.y, moneyWidget.transform.localPosition.z);
//			moneyWidget.transform.localPosition.x = MONEY_WIDGET_X_POSITION_MAP;
		}else {
				moneyWidget.transform.localPosition = new Vector3(MONEY_WIDGET_X_POSITION, moneyWidget.transform.localPosition.y, moneyWidget.transform.localPosition.z);
//			moneyWidget.transform.position.x = MONEY_WIDGET_X_POSITION;
		}
	}
}

void enlargeMerchantForeground (){
	if (GotoProxy.getSceneName() == GotoProxy.MERCHANT) {
		merchantFG_original.active = false;
		merchantFG_iphone5.active = true;
	}
}

void enlargeLearnSkillPanel (){
//	if (GotoProxy.getSceneName() == GotoProxy.COMBINED_ARMORY && CombinedArmoryManager.currentScene == CombinedArmoryManager.SKILL) {
//		learnSkillPanel.transform.localScale.x *= 1.1875f;
//		learnSkillPanel.transform.position.x = 1300.0f;
//		foreach(Transform child in learnSkillPanel.transform) {
//			child.localScale.x /= 1.1875f;
//			if (child.name == "plane_hire") {
//				child.localPosition.x = 604.0f;
//			}
//			if (child.name == "plane_recruit") {
//				child.localPosition.x = 495.0f;
//			}
//			if (child.name == "CancelBtn") {
//				child.localPosition.x = 542.0f;
//				child.localPosition.y = 401.0f;
//			}
//		}
//	}
}

void Update (){

}
}