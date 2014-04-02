using UnityEngine;
using System.Collections;

public class TsUserBehavior : MonoBehaviour {

	public delegate void FinishedCallbackDelegate();
	public FinishedCallbackDelegate OnFinished;
	
	protected Camera usingCamera = Camera.main;
	
	public Camera UsingCamera{
		get{
			return usingCamera;
		}
		set{
			usingCamera = value;
		}
	}
	
	
	// Functions
	// -Protected
	protected bool CheckRayCastOnMe(){
		bool result = false;
		
		Ray ray = UsingCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit []hitObjs = Physics.RaycastAll(ray);
		for (int i=0; i<hitObjs.Length; i++){
			if (hitObjs[i].collider.gameObject.Equals(gameObject)){
				result = true;
				i = hitObjs.Length;
			}
		}
		
		return result;
	}
}
