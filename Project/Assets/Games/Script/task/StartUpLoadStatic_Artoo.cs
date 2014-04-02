using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_Artoo : Task
{
	public override void run ()
	{
		if(BuildSetting.UseServerConfig){
			Debug.LogError("Use Server Configs");	
		}else{
			Debug.LogError("Use local Configs");	
		}
		
		if(BuildSetting.UseServerConfig){
			Test_AllMetaCommand cmd = new Test_AllMetaCommand ("noid", "no authToken", "[\"content\"][\"objects\"]",
			delegate(Hashtable data){
				process( data );
				this.complete();
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("load artoo content error:"+err_code);	
				CommonDlg dlg = DlgManager.instance.ShowCommonDlg("Load content error: "+err_code);
				dlg.setOneBtnDlg();
				dlg.onOk = () => {
					Application.Quit();
				};
			}
			);
			cmd.excute ();
		}else{
			TextAsset ta = Resources.Load("configData/ArtooBase") as TextAsset;
			Hashtable h = MiniJSON.jsonDecode(ta.text) as Hashtable ;
			h=h["content"]as Hashtable;
			h=h["objects"]as Hashtable;
			process(h);
			this.complete();
		}
	}
	private ICollection FromHashtableOrArrylistToList(object c){
		if(c is ArrayList){
			return c as ArrayList;
		}else if(c is Hashtable){
			return (c as Hashtable).Values;	
		}else{
			return null;	
		}
	}
	private void process(Hashtable h){
		ICollection others = FromHashtableOrArrylistToList(h["others"]);
		StaticData.initOthersWithJson(others);
		UserInfo.instance.initOthersWithJson(others);
		UserInfo.instance.loadDynamicScalars();
		
		ICollection xplevel =  FromHashtableOrArrylistToList(h["xplevel"]);
		InitGameData.instance.initXplevel(xplevel);
		
		ICollection a =  FromHashtableOrArrylistToList(h["skill_active"]);
		ICollection p =  FromHashtableOrArrylistToList(h["skill_passive"]);
		SkillLib.instance.initByArtoo(a,p);
		
		
		ICollection gear=  FromHashtableOrArrylistToList(h["gear"]);
		ICollection iso =  FromHashtableOrArrylistToList(h["iso8"]);
		EquipFactory.loadFromJson(gear);
		EquipFactory.loadFromJson(iso);
		
		
		ICollection heros =  FromHashtableOrArrylistToList(h["heros"] );
		InitGameData.instance.initHerosFromJson( heros );
		ICollection enemies =  FromHashtableOrArrylistToList(h["enemies"] );
		EnemyDataLib.instance.initWithJson(enemies);

		InitGameData.instance.initHerosFromDynamicData();
		EquipManager.Instance.loadDynamicData( UserInfo.instance.getPackage() );

		
		ICollection map_chapter =  FromHashtableOrArrylistToList(h["map_chapter"] );
		ICollection map_level =  FromHashtableOrArrylistToList(h["map_level"] );
		MapMgr.Instance.parseChapterFromArtooJson(map_chapter);
		MapMgr.Instance.parseLevelFromArtooJson(map_level);
		
		MapMgr.Instance.loadDynamicData(UserInfo.instance.getMapInfo());
		
		TsDynamicData.instance.loadDynamicData(UserInfo.instance.getFtueInfo());
		
		
	}
}
