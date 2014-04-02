using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MeshTester : MonoBehaviour
{
	public GameObject template;
	private List<GameObject> list = new List<GameObject>();
	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (Screen.width - 100, 0, 100, 800));
		GUILayout.BeginVertical ();
		GUILayout.Label("Total: "+(list.Count+1));
		if (GUILayout.Button ("+5",GUILayout.Width(100),GUILayout.Height(88))) {
			createOne();
			createOne();
			createOne();
			createOne();
			createOne();
		}
		if (GUILayout.Button ("+1",GUILayout.Width(100),GUILayout.Height(88))) {
			createOne();
		}
		if (GUILayout.Button ("-5",GUILayout.Width(100),GUILayout.Height(88))) {
			deleteOne();
			deleteOne();
			deleteOne();
			deleteOne();
			deleteOne();
		}
		if (GUILayout.Button ("-1",GUILayout.Width(100),GUILayout.Height(88))) {
			deleteOne();
		}
		GUILayout.EndVertical ();
		
		GUILayout.EndArea ();
	}
	private void createOne(){
		GameObject go = Instantiate(template) as GameObject;
		go.transform.position += new Vector3(Random.Range(-300,300),Random.Range(-300,300),0);
		list.Add(go);
	}
	private void deleteOne(){
		GameObject go = list[0];
		list.Remove(go);
		Destroy(go);
	}
}
