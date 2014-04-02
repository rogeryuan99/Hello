using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// CL-1 2012-12-25 for xml file upload to server. add by why
public class Task : MonoBehaviour
{
	public static T Create<T>() where T:Task{
		GameObject go = new GameObject(typeof(T).ToString());
		T component = go.AddComponent<T>();
		return component;
	}
	
	public enum Mode
	{
		SEQUENCY,
		PARALLEL 
	}

	public delegate void TaskCompleteDelegate (Task task);

	public delegate void TaskErrorDelegate (Task task);

	private string _taskName;

	public string taskName {
		set {
			_taskName = value;
			refreshGoName ();
		}
		get{ return _taskName;}
	}

	private Mode _mode = Mode.SEQUENCY;

	public Mode mode {
		set {
			_mode = value;
			refreshGoName ();
		}
		get{ return _mode;}
	}

	private void refreshGoName ()
	{
		gameObject.name = _taskName + "<" + _mode + ">";	
		
	}
	
	public Queue<Task> taskQueue = new Queue<Task> ();
	public Task parent = null;
	// The number of tasks have been completed
	private int finishCount = 0;
	// Total task
	private int currentFinishCount = 0;
	public TaskCompleteDelegate taskCompleteDelegate;
	public TaskErrorDelegate taskErrorDelegate;
	
	public void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public void start ()
	{
//		Debug.Log (name + " start"+"   count:"+taskQueue.Count);
		currentFinishCount = 0;
		finishCount = taskQueue.Count;
		preRun ();
		if (taskQueue.Count != 0) {
//			Debug.Log ("runNext");
			runNext ();
		} else {
//			Debug.Log ("run");
			run ();	
		}
	}
	
	private void runNext ()
	{
		if (mode == Task.Mode.PARALLEL) {
			while (taskQueue.Count != 0) {
				Task task = taskQueue.Dequeue ();
//				Debug.Log (this.name + "  PARALLEL start " + task.name);
				task.start ();
			}
		} else {
			if (taskQueue.Count > 0) {
				Task task = taskQueue.Dequeue ();
//				Debug.Log (this.name + "  Sequen start " + task.name);
				task.start ();
			}
		}
	}
	
	public void addTask (Task task)
	{
		string prefix = this.gameObject.transform.childCount+"_";
		task.gameObject.transform.parent = this.gameObject.transform;
		task.gameObject.name = prefix + task.gameObject.name;
		task.parent = this;
		taskQueue.Enqueue (task);
	}
	
	protected virtual void complete ()
	{
//		Debug.Log (name + " complete");
		this.afterRun ();
		
		// judge wheater top node
		// if parent == null so zhe this node is top node
		if (parent != null) {
			parent.childComplete (this);
		} else {
			Destroy (this.gameObject);
		}
	}
	protected void childComplete (Task task)
	{
		//Destroy (task.gameObject);
		task.gameObject.name += " Completed";
		
		currentFinishCount++;
		if (this.mode == Task.Mode.SEQUENCY) {
			if (currentFinishCount == finishCount) {
				if (this.taskCompleteDelegate != null) {
					this.taskCompleteDelegate (this);	
				}
				this.complete ();
			} else {
				runNext ();
			}
		} else {
			if (currentFinishCount == finishCount) {
				if (this.taskCompleteDelegate != null) {
					this.taskCompleteDelegate (this);	
				}
				this.complete ();
			}
		}
	}
	
	protected void error ()
	{
		parent.childError (this);
	}
	
	protected void childError (Task task)
	{
		//Debug.LogError("===TTT=== child Error:"+task);	
		if (this.taskErrorDelegate != null) {
			this.taskErrorDelegate (this);	
		}
		Destroy (task.gameObject);
//		currentFinishCount++;
//		if (currentFinishCount == finishCount) {
//			//Debug.Log("===TTT=== all child complete:"+this.taskName);
//			
//			this.complete ();
//			if (parent != null) {
//				parent.runNext ();
//			}
//		} else {
//			if (this.mode == Task.Mode.SEQUENCY) {
//				runNext ();
//			}
//		}
	}
	
	public override string ToString ()
	{
		return this.GetType () + " " + taskName;	
	}
	
	public virtual void run ()
	{
		if (this.taskCompleteDelegate != null) {
			this.taskCompleteDelegate (this);	
		}
		this.complete ();
//		Debug.Log("===TTT=== should be overrided");
	}

	public virtual void preRun ()
	{
//		Debug.Log("===TTT=== "+taskName+":preRun");
	}

	public virtual void afterRun ()
	{
//		Debug.Log("===TTT=== "+taskName+":afterRun");
	}
}
