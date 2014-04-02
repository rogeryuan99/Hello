using UnityEngine;
using System.Collections;

public class StartUpManager : Singleton<StartUpManager> {
	public TextAsset artoobase;
	public void Begin() {
		Task startup = new GameObject("Startup").AddComponent<Task>();
		startup.mode = Task.Mode.SEQUENCY;
		
		if(BuildSetting.autoReg_n_login)	startup.addTask(new GameObject("StartUpLogin").AddComponent<StartUpLogin>());
		
		startup.addTask(new GameObject("StartUpInitDynamicData").AddComponent<StartUpInitDynamicData>());
		
		startup.addTask(new GameObject("StartUpLoadStatic_Formulas").AddComponent<StartUpLoadStatic_Formulas>());
		startup.addTask(new GameObject("StartUpLoadStatic_Artoo").AddComponent<StartUpLoadStatic_Artoo>());
		//startup.addTask(new GameObject("StartUpLoadStatic_Items").AddComponent<StartUpLoadStatic_Items>());
		//startup.addTask(new GameObject("StartUpLoadStatic_GameData").AddComponent<StartUpLoadStatic_GameData>());
		//startup.addTask(new GameObject("StartUpLoadStatic_HeroSkills").AddComponent<StartUpLoadStatic_HeroSkills>());
		//startup.addTask(new GameObject("StartUpLoadStatic_LevelDef").AddComponent<StartUpLoadStatic_LevelDef>());
		startup.addTask(new GameObject("StartUpLoadStatic_Others").AddComponent<StartUpLoadStatic_Others>());
		startup.addTask(new GameObject("StartUpLoadStatic_CacheMgrLoad").AddComponent<StartUpLoadStatic_CacheMgrLoad>());
		

		startup.taskCompleteDelegate = startUpComplete;
		startup.taskErrorDelegate = startUpError;
		startup.start();
	}
	// Update is called once per frame
	private void startUpComplete(Task task)
	{
		Debug.Log("startUpComplete");
		InitGameData.instance.initComplete();
	}
	private void startUpError(Task task)
	{
		Debug.Log("startUpError");
	}
}
