using UnityEngine;
using System.Collections;
using System.IO;
public class StartUpInitDynamicData : Task
{
	public override void run ()
	{
		SaveGameManager.instance().init();
		if(BuildSetting.LOCAL_READ){ 
			Debug.LogError("LOCAL_READ dynamic data");
			SaveGameManager.instance().loadLocalSavedData();
			this.complete();
		}else{
			Debug.LogError("server_READ dynamic data");
			Player_GetCommand cmd = new Player_GetCommand(CommandTest.playerId,CommandTest.authToken,
			delegate(Hashtable data){
				Debug.Log("=-=-=-=-=-=-=- "+Utils.dumpHashTable(data));
				SaveGameManager.instance().initFromServerData(data);
				Debug.Log("complete");	
				this.complete();
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log("error");	
			}
			);
			cmd.excute();
		}
	}
}
