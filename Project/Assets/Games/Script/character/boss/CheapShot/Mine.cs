using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {


public GameObject skillB;
private float time = 0.2f;
private int timeCount = 0;

void Start (){
		this.transform.rotation = Quaternion.Euler(new  Vector3(0,0,-2));
//	this.transform.rotation.eulerAngles = new  Vector3(0,0,-2);
	InvokeRepeating("explosionTimer",0,1);
}

void explosionTimer (){
	if(timeCount >= 5)
	{
		CancelInvoke("explosionTimer");
		explosion();
	}else if( timeCount == 4){
		shake(0.01f);
	}else if(timeCount == 2){
		shake(0.05f);
	}else if(timeCount == 0){
		shake(0.1f);
	}
	timeCount++;
}

void explosion (){
	MusicManager.playEffectMusic("skill_particlegun_explosion");
	skillB.gameObject.transform.localScale = new Vector3(9,9,1);
	GameObject tempSkillB= Instantiate(skillB,new Vector3(transform.position.x,transform.position.y,transform.position.z-50),transform.rotation) as GameObject;
	Message msg = new Message(CheapShot.MSG_MINEBOMB, this);
	MsgCenter.instance.dispatch(msg);
	Destroy(this.gameObject);
}

void shake ( float time  ){
	iTween.Stop(gameObject);
	this.transform.rotation = Quaternion.Euler( new Vector3(0,0,-2));
	iTween.RotateTo(gameObject,new Hashtable(){{"z",2},{"time",time},{"looptype","pingPong"},{ "EaseType","linear"}});
}

//function CorrectionRotation()
//{
//	transform.localRotation = Quaternion(0,0,0,0);
//}

void killThis (){
	Destroy(this.gameObject);
}
}