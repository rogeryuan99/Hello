using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_LevelDef : Task
{
	public override void run ()
	{
//			FileCommand cmd = new FileCommand("CONF_LevelDef.json",
//			delegate(Hashtable data){
//				MapMgr.Instance.parseStaticFromJson(data["text"] as string);
//				MapMgr.Instance.loadDynamicData(UserInfo.instance.getMapInfo());
//				//MapManager.instance.init();
//				this.complete();
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//				this.error();
//			}
//			);
//			cmd.excute();		
		TextAsset ta = Resources.Load("configData/CONF_LevelDef") as TextAsset;
		MapMgr.Instance.parseStaticFromJson(ta.text);
		//NewLevelMgr.Instance.loadDynamicData(MiniJSON.jsonDecode( "[[0], [4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]]"));
		MapMgr.Instance.loadDynamicData(UserInfo.instance.getMapInfo());

		//MapManager.instance.init();
		this.complete();
	}
}
