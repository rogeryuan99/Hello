using UnityEngine;
using System.Collections;

public class TaskScript : Task {
	public TaskScript()
	{
	}
	public override void run()
	{
		StartCoroutine(hideActivityView());
	}
	
	public IEnumerator hideActivityView()
	{
		yield return new WaitForSeconds(1.0f );
		complete();
	}
}
