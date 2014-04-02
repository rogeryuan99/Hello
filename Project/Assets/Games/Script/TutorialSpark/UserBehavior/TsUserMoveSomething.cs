using UnityEngine;
using System.Collections;

public class TsUserMoveSomething : TsUserBehavior {

	private GameObject targetObj = null;
	private float radius = 0f;
	
	public GameObject TargetObj{
		get{ return targetObj; }
		set{ targetObj = value; }
	}
	public float Radius{
		get{ return radius; }
		set{ radius = value; }
	}
	
	// Functions 
	// -Publics
	public void Update(){
		if (false == CheckSecurity()) return; 
		
		float distance = Vector3.Distance(transform.position, TargetObj.transform.position);
		if (distance < Radius && null != OnFinished){
			OnFinished();
			Destroy(this);
		}
	}
	
	private bool CheckSecurity(){
		bool security = true;
		
		if (null == TargetObj) { 
			Debug.LogError ("TargetObj is null"); 
			security = false;
		}
		else if (0 == Radius){
			if (null != TargetObj.collider)
				Radius = Mathf.Min(TargetObj.collider.bounds.size.x, TargetObj.collider.bounds.size.y);
			else {
				Debug.Log ("Raduis and collider is null");
				security = false;
			}
		}
		
		return security;
	}
}
