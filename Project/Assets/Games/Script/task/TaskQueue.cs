using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskQueue : Singleton<TaskQueue> {

	public Queue<Task> queue = new Queue<Task> ();
	private Task runningTask = null;
	private bool isPaused = true;
	public void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public void addTaskQueue(Task t){
		queue.Enqueue (t);	
	}
	
	public void resume(){
		isPaused = false;
	}
	public void pause(){
		isPaused = true;
	}
	public void clear(){
		pause();
		queue.Clear();
	}
	
	void Update(){
		if(isPaused) return;
		if(runningTask!= null) return;
		if(queue.Count == 0) return;
		
		runningTask = queue.Dequeue();
		runningTask.taskCompleteDelegate = delegate (Task task){
			Debug.Log("Queue task complete:"+task);
			runningTask = null;
		};
		runningTask.taskErrorDelegate = delegate(Task task){
			Debug.LogError("Queue task error:"+task);
			runningTask = null;
		};
		runningTask.run();
	}
	
	
}
