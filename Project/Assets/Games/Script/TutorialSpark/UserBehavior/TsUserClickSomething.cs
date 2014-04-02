using UnityEngine;
using System.Collections;

public class TsUserClickSomething : TsUserBehavior {

	private bool onClick = false;
	public FinishedCallbackDelegate OnClickDown;
	public FinishedCallbackDelegate OnClickUp;
	public FinishedCallbackDelegate OnClickOutside;
	
	// Functions
	// -Publics
	public void Update(){
		if (Input.GetMouseButtonDown(0)){
			onClick = CheckRayCastOnMe();
			if (onClick && null != OnClickDown){
				OnClickDown();
			}
			if (!onClick && null != OnClickOutside){
				OnClickOutside();
			}
		}
		if (Input.GetMouseButtonUp(0)){
			if (onClick && CheckRayCastOnMe() && null != OnFinished){
//				MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
				OnFinished();
				Destroy (this);
			}
			if ((onClick || CheckRayCastOnMe()) && null != OnClickUp){
				OnClickUp();
			}
			
			onClick = false;
		}
	}
}
