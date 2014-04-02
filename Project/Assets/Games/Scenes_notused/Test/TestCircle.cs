using UnityEngine;
using System.Collections;

public class TestCircle : MonoBehaviour {
	
	private MyCircleRenderer c1;
	public GameObject hero;
	
	
	public void OnGUI(){
		if (GUI.Button(new Rect(200, 100, 100, 50), "trackMouse")){
			c1 = new MyCircleRenderer(MyCircleRenderer.TYPE.FINGER);
		}
		if (GUI.Button(new Rect(200, 150, 100, 50), "tackHero")){
			c1 = new MyCircleRenderer(MyCircleRenderer.TYPE.PLAYER, hero);
		}
		if (GUI.Button(new Rect(200, 200, 100, 50), "changeTarget")){
			c1.TrackingTarget = hero;
			c1.Type = (MyCircleRenderer.TYPE.PLAYER == c1.Type)? MyCircleRenderer.TYPE.FINGER: MyCircleRenderer.TYPE.PLAYER;
		}
	}
	
	public void Update(){
		if (null != c1){
			c1.Update();
		}
		UpdateHero();
	}
	
	private float time = 0f;
	public void UpdateHero(){
		time += .05f;
		hero.transform.position = new Vector3((100 + Mathf.Sin(time)*10),
												hero.transform.position.y,
												hero.transform.position.z);
	}
}
