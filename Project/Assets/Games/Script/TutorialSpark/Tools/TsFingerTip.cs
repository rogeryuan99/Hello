using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TsFingerTip : MonoBehaviour {
	
	public UISprite fingerSprite;
	public UISprite circleSprite;
	private int motionIndex = 0;
	private List<string[]> motions = new List<string[]>();
	private List<string[]> clickingObjects = new List<string[]>();
	private const float COUNT_DOWN_TIME = 6f;
	
	private int MotionIndex{
		get{ return motionIndex; }
		set{ 
			motionIndex = value % motions.Count;
		}
	}
	
	// Functions 
	// -Publics
	public void AddMotion(string[] parms){
		motions.Add(parms);
	}
	public void PlayMotion(){
		AddListenerForClickingObjects();
		SendMessage(motions[MotionIndex][0], motions[MotionIndex]);
		MotionIndex++;
	}
	
	public void Stop(){
		RemoveListenerForClickObjects();
		iTween.Stop(gameObject);
		iTween.Stop(this.gameObject);
		MotionIndex = 0;
		motions.Clear();
		clickingObjects.Clear();
		//this.gameObject.SetActive(false); deleted by roger, if inactive, can not be found, so can not be released
	}
	
	/// <summary>
	/// Adds the clicking object to pause.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format:{0}ObjectName{1}CameraName
	/// </param>
	public void AddClickingObjectToPause(string[] parms){
		clickingObjects.Add(parms);
	}
	
	// -Privates
	private void MoveToSomething(string[] parms){
		GameObject go = TsObjectFactory.GetGameObject(parms[1]);
		Vector3 p = go.transform.position;
		if(NGUITools.FindInParents<UIRoot>(go)==null){
			p = MainCameraToUiCamera(p);
		}
		p.z = p.z -1;
		transform.position = p;
		iTween.MoveTo(gameObject, new Hashtable(){
			{"time", 0.01f},
			{"onComplete", "PlayMotion"}
		});
	}
	
	/// <summary>
	/// Moves from something to something.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}FunctionName{1}ObjName{2}ObjName{3}time{4}delay{5}Weight: which pos need to translate from mainCamera to uiCamera.{6}easeType
	/// Weight: 0->none, 1->parm[0], 2->parm[1], 3->1+2->all
	/// </param>
	private void MoveFromSomethingToSomething(string[] parms){
		float time   = float.Parse(parms[3]);
		float delay  = float.Parse(parms[4]);
		Vector3 []pos = GetPos(parms);
		//StartCoroutine(delayForSwipeMusic());
		this.transform.position.Set(pos[0].x, pos[0].y, transform.position.z);
		this.gameObject.SetActive(true);
		iTween.MoveTo(this.gameObject, new Hashtable(){
			{"x", pos[1].x},
			{"y", pos[1].y},
			{"time",  time},
			{"delay", delay},
			{"easeType", parms[6]},
			{"onCompleteTarget", gameObject},
			{"onComplete", "PlayMotion"}
		});
	}
//	private IEnumerator delayForSwipeMusic(){
//		yield return new WaitForSeconds(0.8f);
//		MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
//	}
	private void ClickPrompt(string[] parms){
		float t = 0.5f;
		UITweener[] tweeners = this.gameObject.GetComponentsInChildren<UITweener>(true);
		foreach(UITweener tw in tweeners){
			tw.Reset();
			tw.Play(true);
		}
		StartCoroutine(delayForMusic());
		iTween.MoveTo(gameObject, new Hashtable(){
			{"time", t},
			{"onComplete", "PlayMotion"}
		});	
	}
	private IEnumerator delayForMusic(){
		yield return new WaitForSeconds(.5f);
//		MusicManager.playEffectMusic("SFX_Tap_Here_3b");
		yield return new WaitForSeconds(1f);
//		MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
	}
	/// <summary>
	/// Wait the specified parms.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}FunctionName{1}time
	/// </param>
	private void Wait(string[] parms){
		Wait(float.Parse(parms[1]));
	}
	private void Wait(float time){
		this.gameObject.SetActive(true);
		iTween.MoveTo(gameObject, new Hashtable(){
			{"time", time},
			{"onComplete", "PlayMotion"}
		});
	}
	
	 
	private Vector3[] GetPos(string[] parms){
		int    weight = int.Parse(parms[5]);
		Vector3 []pos = new Vector3[2];
		
		for (int i=1; i>=0; i--){
			pos[i] = TsObjectFactory.GetGameObject(parms[i+1]).transform.position;
			int curWeight = (int)Mathf.Pow(2, i);
			if (weight >= curWeight){
				weight -= curWeight;
				pos[i] = MainCameraToUiCamera(pos[i]);
			}
		}
		
		return pos;
	}
	
	private void AddListenerForClickingObjects(){
		for (int i=0; i<clickingObjects.Count; i++){
			GameObject obj = TsObjectFactory.GetGameObject(clickingObjects[i][0]);
			if (null == obj) continue;
			if (null != obj.GetComponent<TsUserClickSomething>()) continue;
			
			TsUserClickSomething behavior = obj.AddComponent<TsUserClickSomething>();
			behavior.UsingCamera = TsObjectFactory.GetGameObject(clickingObjects[i][1]).GetComponent<Camera>();
			behavior.OnClickUp += ()=>{
				Pausing(COUNT_DOWN_TIME);
			};
		}
	}
	
	private void RemoveListenerForClickObjects(){
		for (int i=0; i<clickingObjects.Count; i++){
			GameObject obj = TsObjectFactory.GetGameObject(clickingObjects[i][0]);
			if (null == obj) continue;
			TsUserClickSomething behavior = obj.GetComponent<TsUserClickSomething>();
			if (null != behavior) Destroy(behavior);
		}
	}
	
	private void Pausing(float time){
		iTween.Stop(gameObject);
		iTween.Stop(this.gameObject);
		MotionIndex = 0;
		this.transform.position = new Vector3(0,0,10000000);
		Wait(time);
	}
	
	// Caution: Move this Function to other where, please
	private Vector3 MainCameraToUiCamera(Vector3 orign){
		Camera uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
		
		Vector3 screen = Camera.mainCamera.WorldToScreenPoint(orign);
		Vector3 target = uiCamera.ScreenToWorldPoint(screen);
		
		return target;
	}
}
