using UnityEngine;
using System.Collections;

public class RollingDoor : MonoBehaviour {
	public UISprite button;
	public UISprite door;
	
	void Start () {
	
	}
	
	void Update () {

	}
	
	void Run(float targetPosY,float duration){
		MusicManager.playEffectMusic("SFX_Door_LevelComplete");
		if(button.spriteName == "Threshold_Red") button.spriteName = "Threshold_Green";
		else if(button.spriteName == "Indicator_Red") button.spriteName = "Indicator_Green";
		if(door != null) iTween.MoveTo(door.gameObject, iTween.Hash("y", targetPosY,"time", duration,"easetype","linear","islocal",true));		
	}
	
	public void OnSuccess(bool isWin){
		StartCoroutine(delayDoorEft(isWin));
//		if(isWin) Run(700.0f,.5f);
//		else OnFail();
	}
	
	void OnFail(){
		Debug.Log("fail~~");		
	}
	
	public IEnumerator delayDoorEft(bool isWin){
		yield return new WaitForSeconds(1f);
		
		if(isWin) Run(700.0f,.5f);
		else OnFail();
	}
}
