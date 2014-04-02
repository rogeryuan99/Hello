using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	public GameObject elevator;
	
	private float textureWidth = 854f;
	private float elevatorHeight = 420f;

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void Run(Vector3 targetPos,float time){
		if(elevator != null) iTween.MoveTo(elevator.gameObject, iTween.Hash("position", targetPos,"time", time,"easetype","linear","islocal",true));
	}
	
	public void GotoLv(int level){
		int lv = (level-1)%4;
		if(elevator != null) Run(new Vector3(50f+lv*textureWidth/3.0f,elevatorHeight-80f*lv,0),1.5f);
	}
	
	public void JumptoLv(int level){
		int lv = (level-1)%4;
		if(elevator != null) elevator.transform.localPosition = new Vector3(50f+lv*textureWidth/3.0f,elevatorHeight-80f*lv,0f);
	}
}
