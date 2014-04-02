using UnityEngine;
using System.Collections;

public class MyCircleRenderer {
	
	public enum TYPE{
		PLAYER=0, ENEMY, FINGER, GROUP, NONE
	}
	
	private GameObject trackingTarget;
	private GameObject circleObj;
	private Transform  parent;
	private TYPE type = TYPE.NONE;
	
	public TYPE Type{
		get{
			return type;
		}
		set{
			InitCircleObj(value);
			circleObj.SetActive(TYPE.NONE != value);
			
			type = value;
		}
	}
	public GameObject TrackingTarget{
		get{
			return trackingTarget;
		}
		set{
			trackingTarget = value;
		}
	}
	public Vector3 Position{
		get{
			return circleObj.transform.position;
		}
	}
	
	public MyCircleRenderer(TYPE type_):this(type_, null, null){}
	public MyCircleRenderer(TYPE type_, Transform parent_):this(type_, null, null){}
	public MyCircleRenderer(TYPE type_, GameObject target_):this(type_, target_, null){}
	public MyCircleRenderer(TYPE type_, GameObject target_, Transform parent_){
		parent = parent_;
		Type = type_;
		TrackingTarget = target_;
	}
	
	// Functions
	public void Update(){
		if (TYPE.NONE == type && null == trackingTarget) return;
		
		Vector3 pos = circleObj.transform.position;
			
		if (TYPE.FINGER == type){
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, StaticData.circleLayer);
			pos = BattleBg.CorrectingEndPointToFingerBounds(Camera.mainCamera.ScreenToWorldPoint(mousePos));
		}
		else {
			pos.Set(trackingTarget.transform.position.x, trackingTarget.transform.position.y, StaticData.circleLayer);
		}
		circleObj.transform.position = pos;
	}
	public void Clear(){
		type = TYPE.NONE;
		trackingTarget = null;
		MonoBehaviour.DestroyObject(circleObj);
	}
	public void FadeOutAndClear(){
		type = TYPE.NONE;
		trackingTarget = null;
		iTween.ScaleTo(circleObj,new Hashtable(){{"x",0.0f},{"y",0.0f},{"time",0.2f},{"easetype","linear"},{"oncomplete","destroySelf"},{"oncompletetarget",circleObj}});//,{
	}

	// Private
	private void InitCircleObj(TYPE value){
		GameObject newObj = IntentionGroupResources.Instance.GetCircle(value);
		
		if (null != circleObj){
			newObj.transform.position = circleObj.transform.position;
			newObj.transform.localScale = circleObj.transform.localScale;
			MonoBehaviour.DestroyObject(circleObj);
		}
		circleObj = newObj;
		circleObj.transform.parent = parent;
	}
}
