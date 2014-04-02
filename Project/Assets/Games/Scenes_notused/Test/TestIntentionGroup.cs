using UnityEngine;
using System.Collections;


using cType = MyCircleRenderer.TYPE;

public class TestIntentionGroup : MonoBehaviour {
	
	
	public GameObject Hero1;
	public GameObject Hero2;
	public GameObject Hero3;
	public GameObject Enemy1;
	public GameObject Enemy2;
	public GameObject Skill;
	
	
	private IntentionGroup intentionGroup = new IntentionGroup();
	
	public void OnGUI(){
		if (GUI.Button(new Rect(0,0,100,50), "Add Finger")){
			intentionGroup.AddTarget(cType.FINGER);
		}
		if (GUI.Button(new Rect(0,50,100,50), "Add Hero1")){
			intentionGroup.AddTarget(cType.PLAYER, Hero1);
		}
		if (GUI.Button(new Rect(0,100,100,50), "Add Hero2")){
			intentionGroup.AddTarget(cType.PLAYER, Hero2);
		}
		if (GUI.Button(new Rect(0,150,100,50), "Add Hero3")){
			intentionGroup.AddTarget(cType.PLAYER, Hero3);
		}
		if (GUI.Button(new Rect(0,200,100,50), "Add Enemy1")){
			intentionGroup.AddTarget(cType.ENEMY, Enemy1);
		}
		if (GUI.Button(new Rect(0,250,100,50), "Add Enemy2")){
			intentionGroup.AddTarget(cType.ENEMY, Enemy2);
		}
		if (GUI.Button(new Rect(0,300,100,50), "Add Skill")){
			intentionGroup.AddTarget(cType.NONE,  Skill);
		}
		if (GUI.Button(new Rect(0,350,100,50), "EndDrawing")){
			intentionGroup.EndDrawing();
		}
		if (GUI.Button(new Rect(0,400,100,50), "Clear")){
			intentionGroup.Clear();
		}
	}
	
	public void Update(){
		intentionGroup.Update();
		UpdateHero();
	}
	
	private float time = 0f;
	public void UpdateHero(){
		time += .05f;
		Enemy1.transform.position = new Vector3((25 + Mathf.Sin(time)*20),
												Enemy1.transform.position.y,
												Enemy1.transform.position.z);
		Enemy2.transform.position = new Vector3((25 + Mathf.Sin(time)*10),
												Enemy2.transform.position.y,
												Enemy2.transform.position.z);
		
		Hero1.transform.position = new Vector3(Hero1.transform.position.x,  
												(25 + Mathf.Sin(time)*30),
												Hero1.transform.position.z);
		Hero2.transform.position = new Vector3(Hero2.transform.position.x,  
												(25 + Mathf.Sin(time)*20),
												Hero2.transform.position.z);
		Hero3.transform.position = new Vector3(Hero3.transform.position.x,  
												(25 + Mathf.Sin(time)*10),
												Hero3.transform.position.z);
	}
}
