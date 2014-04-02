using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandTest : MonoBehaviour
{
	public static string secret = "kakakakakaka";
	static public string playerId = SystemInfo.deviceUniqueIdentifier;
	static public string authToken = "";
	private string result = "";

	void OnGUI ()
	{
		GUILayout.Label (BuildSetting.gameServerUrl);
		secret = GUILayout.TextField (secret);
		playerId = GUILayout.TextField (playerId);
		authToken = GUILayout.TextField (authToken);
		if (GUILayout.Button ("registerPlayerAndGetAuthToken")) {
			Auth_RegisterPlayerAndGetAuthTokenCommand cmd = new Auth_RegisterPlayerAndGetAuthTokenCommand (playerId, secret,
			delegate(Hashtable data){
				Debug.Log ("authToken = " + data ["result"]);	
				authToken = data ["result"] as string;
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error " + err_code);	
			}
			);
			cmd.excute ();
		}
		
		if (GUILayout.Button ("GetAutoTokenCommand")) {
			Auth_GetAutoTokenCommand cmd = new Auth_GetAutoTokenCommand (playerId, secret,
			delegate(Hashtable data){
				Debug.Log ("authToken = " + data ["result"]);	
				authToken = data ["result"] as string;
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error " + err_code);	
			}
			);
			cmd.excute ();
		}
		
		if (GUILayout.Button ("Player_GetCommand")) {
			Player_GetCommand cmd = new Player_GetCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
//		if (GUILayout.Button ("Player_BuyItemCommand")) {
//			Player_BuyItemCommand cmd = new Player_BuyItemCommand(playerId,authToken,"1111",1,
//			delegate(Hashtable data){
//				result = Utils.dumpHashTable(data);
//				Debug.Log("complete");	
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//			}
//			);
//			cmd.excute();
//		}
//		if (GUILayout.Button ("Player_HeroHireCommand")) {
//			Player_HeroHireCommand cmd = new Player_HeroHireCommand(playerId,authToken,"STARLORD",
//			delegate(Hashtable data){
//				result = Utils.dumpHashTable(data);
//				Debug.Log("complete");	
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//			}
//			);
//			cmd.excute();
//		}
//		if (GUILayout.Button ("Player_HeroUnhireCommand")) {
//			Player_HeroUnhireCommand cmd = new Player_HeroUnhireCommand(playerId,authToken,"STARLORD",
//			delegate(Hashtable data){
//				result = Utils.dumpHashTable(data);
//				Debug.Log("complete");	
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//			}
//			);
//			cmd.excute();
//		}
//		if (GUILayout.Button ("Player_HeroSkillEditCommand")) {
//			Player_HeroSkillEditCommand cmd = new Player_HeroSkillEditCommand(playerId,authToken,"STARLORD",null,
//			delegate(Hashtable data){
//				result = Utils.dumpHashTable(data);
//				Debug.Log("complete");	
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//			}
//			);
//			cmd.excute();
//		}
		if (GUILayout.Button ("Player_PackageUpdateCommand")) {
			Player_PackageUpdateCommand cmd = new Player_PackageUpdateCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		if (GUILayout.Button ("Player_MapUpdateCommand")) {
			Player_MapUpdateCommand cmd = new Player_MapUpdateCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		if (GUILayout.Button ("Player_UpdateAllCommand")) {
			Player_UpdateAllCommand cmd = new Player_UpdateAllCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		if (GUILayout.Button ("Player_LoginCommand")) {
			Player_LoginCommand cmd = new Player_LoginCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		
		if (GUILayout.Button ("guid")) {
			Debug.Log ("GUID: " + SystemInfo.deviceUniqueIdentifier);
		}
//		if (GUILayout.Button ("load file")) {
//			FileCommand cmd = new FileCommand("crossdomain.xml",
//			delegate(Hashtable data){
//				result = Utils.dumpHashTable(data);
//				Debug.Log("complete");	
//			},
//			delegate(string err_code,string err_msg,Hashtable data){
//				Debug.Log("error");	
//			}
//			);
//			cmd.excute();
//		}
		
		if (GUILayout.Button ("test.getGameContent")) {
			Test_GetGameContentCommand cmd = new Test_GetGameContentCommand (playerId, authToken,
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		if (GUILayout.Button ("test.allMeta")) {
			//path may be "[\"content\"][\"objects\"][\"Gift\"]" , or null,  
			Test_AllMetaCommand cmd = new Test_AllMetaCommand (playerId, authToken, "[\"content\"][\"objects\"]",
			delegate(Hashtable data){
				result = Utils.dumpHashTable (data);
				Debug.Log ("complete");	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error");	
			}
			);
			cmd.excute ();
		}
		if (GUILayout.Button ("on key get")) {
			RegisterOrGetToken();
		}
			
		sp = GUILayout.BeginScrollView (sp);
		result = GUILayout.TextArea (result);
		GUILayout.EndScrollView ();
	}

	private Vector2 sp = Vector2.zero;

	
	private void RegisterOrGetToken(){
		playerId = SystemInfo.deviceUniqueIdentifier;
		Auth_RegisterPlayerAndGetAuthTokenCommand cmd = new Auth_RegisterPlayerAndGetAuthTokenCommand (playerId, secret,
		delegate(Hashtable data){
			Debug.Log ("authToken = " + data ["result"]);	
			authToken = data ["result"] as string;
			step3 ();
		},
		delegate(string err_code,string err_msg,Hashtable data){
			Debug.Log ("error " + err_code);	
			if(err_code =="805"){
				//already
				step2 ();
			}
		}
		);
		cmd.excute ();
	}
	
	private void step2(){
		Auth_GetAutoTokenCommand cmd = new Auth_GetAutoTokenCommand (playerId, secret,
			delegate(Hashtable data){
				Debug.Log ("authToken = " + data ["result"]);	
				authToken = data ["result"] as string;
			step3();
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error " + err_code);	
			}
			);
			cmd.excute ();
	}
	
	private void step3(){
		Player_GetCommand cmd = new Player_GetCommand (playerId, authToken,
		delegate(Hashtable data){
			result = Utils.dumpHashTable (data);
			Debug.Log ("complete");	
		},
		delegate(string err_code,string err_msg,Hashtable data){
			Debug.Log ("error");	
		}
		);
		cmd.excute ();
	}
	
	

}