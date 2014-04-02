using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_Items : Task
{
	public override void run ()
	{
//			FileCommand cmd = new FileCommand("CONF_Items.xml",
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
		TextAsset ta = Resources.Load("configData/CONF_Items") as TextAsset;
		process(ta.text);
		this.complete();
	}
	
	private void process(string s){
		EquipFactory.loadStaticFromXml(s);
	}
}
