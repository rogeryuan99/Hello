using UnityEngine;
using System.Collections;
using System.IO;
public class TaskTestScript : MonoBehaviour {
	
	
	private static TaskTestScript _instance = null;
    public static TaskTestScript instance {
        get {
            if (!_instance) {
                GameObject go = new GameObject ("TackTestScript GameObject");
                _instance = go.AddComponent<TaskTestScript> ();
				_instance.init();
            }
            return _instance; 
        }
    }
	
	private void init ()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start(){
	}
	
	// Use this for initialization
	public void testTask() {
		GameObject a = new GameObject("Task");
		Task A = a.AddComponent<Task>();
		A.taskName = "A";
		A.taskCompleteDelegate = loadMeComplete;
		A.taskErrorDelegate = loadMeError;
		
		GameObject b1 = new GameObject("TaskScript");
		TaskScript B1 = b1.AddComponent<TaskScript>();
		B1.taskName = "B1";
		B1.mode = Task.Mode.PARALLEL;
		A.addTask(B1);
		
		GameObject c1 = new GameObject("TaskScript");
		TaskScript C1 = c1.AddComponent<TaskScript>();
		C1.taskName="C1";
		B1.addTask(C1);
		GameObject c2 = new GameObject("TaskScript");
		TaskScript C2 = c2.AddComponent<TaskScript>();
		C2.taskName="C2";
		B1.addTask(C2);
		GameObject c3 = new GameObject("TaskScript");
		TaskScript C3 = c3.AddComponent<TaskScript>();
		C3.taskName="C3";
		B1.addTask(C3);
		
		GameObject b2 = new GameObject("TaskScript");
		TaskScript B2 = b2.AddComponent<TaskScript>();
		B2.taskName = "B2";
		B2.mode = Task.Mode.SEQUENCY;
		A.addTask(B2);
		
		GameObject d1 = new GameObject("TaskScript");
		TaskScript D1 = d1.AddComponent<TaskScript>();
		D1.taskName="D1";
		B2.addTask(D1);
		GameObject d2 = new GameObject("TaskScript");
		TaskScript D2 = d2.AddComponent<TaskScript>();
		D2.taskName="D2";
		B2.addTask(D2);
		GameObject d3 = new GameObject("TaskScript");
		TaskScript D3 = d3.AddComponent<TaskScript>();
		D3.taskName="D3";
		B2.addTask(D3);
		
		A.start();
		
		//Debug.Log(Application.dataPath +"/aaa/");
		
		Debug.Break();
		
	}
	
	private int nnn = 0;
	void OnGUI(){
		GUILayout.BeginVertical();
		if(GUILayout.Button("new event")){
			GameObject b1 = new GameObject("TaskScript");
			TaskScript B1 = b1.AddComponent<TaskScript>();
			B1.taskName = "B"+ nnn++;
			TaskQueue.Instance.addTaskQueue( B1);
		}
		if(GUILayout.Button("pause")){
			TaskQueue.Instance.pause();
		}
		if(GUILayout.Button("resume")){
			TaskQueue.Instance.resume();
		}
		GUILayout.EndVertical();
		
	}
	private void loadMeComplete(Task task)
	{
		Debug.Log("loadMeComplete");
	}
	private void loadMeError(Task task)
	{
	}
}
