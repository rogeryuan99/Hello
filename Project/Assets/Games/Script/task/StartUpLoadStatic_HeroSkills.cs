using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_HeroSkills : Task
{
	public override void run ()
	{
//			FileCommand cmd = new FileCommand("CONF_heroSkills.xml",
//			delegate(Hashtable data){
//				SkillLib.instance.setList (data["text"] as string);
//				this.complete();
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//				this.error();
//			}
//			);
//			cmd.excute();
		
		TextAsset ta = Resources.Load("configData/CONF_heroSkills") as TextAsset;
		SkillLib.instance.setList(ta.text);
		this.complete();
	}
}
