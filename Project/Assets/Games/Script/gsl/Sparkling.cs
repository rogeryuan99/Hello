using UnityEngine;
using System.Collections;

public class Sparkling : MonoBehaviour {
	public UITexture bgTexture;
	public GameObject tower;
//	public UISprite towerSprite;
	public float speed = 3.0f;
	
	private float cumulateTime = 0;
	private float textureWidth = 854f;
	private float elevatorHeight = 240f;
	
	void Start () {
	
	}
	
	void Update () {
		cumulateTime += Time.deltaTime * speed;
		if(bgTexture != null) bgTexture.color = new Color(1f,1f,1f,0.85f +0.15f*Mathf.Sin(cumulateTime));
		//if(towerSprite != null) towerSprite.color = new Color(1f,1f,1f,0.85f +0.15f*Mathf.Sin(cumulateTime));
	}
	
	void Run(Vector3 targetPos,float time){
		if(tower != null) iTween.MoveTo(tower.gameObject, iTween.Hash("position", targetPos,"time", time,"easetype","linear","islocal",true));
	}
	
	public void GotoLv(int level){
		int lv = (level-1)%4;
		if(tower != null){
			if(lv == 0) Run(new Vector3(150f+lv*textureWidth/3.0f,elevatorHeight,0),1.5f);
			else if(lv == 1) Run(new Vector3(100f+lv*textureWidth/3.0f,elevatorHeight+40f,0),1.5f);
			else if(lv == 2) Run(new Vector3(-50f+lv*textureWidth/3.0f,elevatorHeight-40f,0),1.5f);
			else Run(new Vector3(-100f+lv*textureWidth/3.0f,elevatorHeight,0),1.5f);
		}
	}
	
	public void JumptoLv(int level){
		int lv = (level-1)%4;
		if(tower != null){
			if(lv == 0) tower.transform.localPosition = new Vector3(150f+lv*textureWidth/3.0f,elevatorHeight,0f);
			else if(lv == 1) tower.transform.localPosition = new Vector3(100f+lv*textureWidth/3.0f,elevatorHeight+40f,0f);
			else if(lv == 2) tower.transform.localPosition = new Vector3(-50f+lv*textureWidth/3.0f,elevatorHeight-40f,0f);
			else tower.transform.localPosition = new Vector3(-100f+lv*textureWidth/3.0f,elevatorHeight,0f);
		}
	}
}
