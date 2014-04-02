using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FtDelegateEvent : MonoBehaviour {

	public class Listener{
		public void Listen(){}
	}
	
	public List<Listener> listener = new List<Listener>();
	public delegate void TestDelegate();
	public event TestDelegate TestEvent;
	
	
	public void AddListener(){
		listener.Add(new Listener());
		TestEvent += listener[listener.Count-1].Listen;
	}
	
	public void RemoveListener(){
		TestEvent -= listener[0].Listen;
		listener.RemoveAt(0);
	}
	
	public void CheckEventIsNull(){
		Debug.LogError(string.Format("Event is null? {0}", (null == TestEvent)));
	}
	
	public void OnGUI(){
		GUILayout.BeginArea(new Rect(200f,200f,200f,400f));
		GUILayout.BeginVertical();
		
		if (GUILayout.Button("Add Listener")){
			AddListener();
		}
		if (GUILayout.Button("Remove Listener")){
			RemoveListener();
		}
		if (GUILayout.Button("Check is Event null?")){
			CheckEventIsNull();
		}
		
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
