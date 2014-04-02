using UnityEngine;
using System.Collections;

public class FadeOutEffect : MonoBehaviour {


public BlurEffect blur;
public void Start (){
	
}
public string sceneName;

// Jugg 
public UILabel sptTxt;

public GameObject pinwheel;

public void toScene ( string scene  ){
//	BlurEffect blur = Camera.mainCamera.gameObject.AddComponent(BlurEffect);
//	blur.blurShader = Shader.Find("Hidden/BlurEffectConeTap");
//	blur = Camera.mainCamera.gameObject.GetComponent(BlurEffect);
//	if(blur)blur.enabled = true;
//	
//	iTween.ValueTo(this.gameObject,{"from":0,"to":20, "onupdate":"updateCurrentValue", "onupdatetarget":gameObject,
//										"time":0.2f, "easetype":"linear",
//										"oncomplete":"onComplete", "oncompletetarget":gameObject});
	
	
	GotoProxy.gotoScene(sceneName);
	
//	iTween.FadeTo(gameObject, {"alpha":1, "time":0.2f, "oncomplete":"onComplete", "oncompletetarget":gameObject});

//	GotoProxy.gotoScene(sceneName);
}

public void updateCurrentValue ( int val  ){
	if(blur)blur.iterations = val;
}

public void onComplete (){
//	sptTxt.Text = "Loading...";
	GotoProxy.gotoScene(sceneName);
}

public void Update (){

}
}