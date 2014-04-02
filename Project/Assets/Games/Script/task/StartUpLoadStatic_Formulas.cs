using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_Formulas : Task
{
	public override void run ()
	{
		if(false){//InitGameData.UseServerConfig
			FileCommand cmd = new FileCommand("CONF_LuaFormulas.txt",
			delegate(Hashtable data){
				process(data["text"] as string);
				this.complete();
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log("error");	
				this.error();
			}
			);
			cmd.excute();
		}else{
			TextAsset ta = Resources.Load("configData/CONF_LuaFormulas") as TextAsset;
			process(ta.text);
			this.complete();
		}
	}
	private void process(string input){
		Formulas.initLoadLuaFile(input);
	}

}
