using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_GameData : Task
{
	public override void run ()
	{
//		if(InitGameData.UseServerConfig){
//			FileCommand cmd = new FileCommand("CONF_gameData.xml",
//			delegate(Hashtable data){
//				process(data["text"] as string);
//				this.complete();
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//				this.error();
//			}
//			);
//			cmd.excute();
//		}else{
//			TextAsset ta = Resources.Load("configData/CONF_gameData") as TextAsset;
//			process(ta.text);
//			this.complete();
//		}
		process();
		this.complete();
	}
	private void process(){
		//StaticData.setXmlData (input);
		//UserInfo.instance.initDefaultScalars();
		//UserInfo.instance.loadDynamicScalars();
		//InitGameData.instance.initHerosFromStaticDef();	
		InitGameData.instance.initHerosFromDynamicData();
		EquipManager.Instance.loadDynamicData( UserInfo.instance.getPackage() );
	}

}
