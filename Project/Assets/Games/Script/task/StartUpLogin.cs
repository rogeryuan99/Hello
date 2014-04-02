using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLogin : Task
{
	public override void run ()
	{
		step1();
	}
	
	private void step1(){
		CommandTest.playerId = SystemInfo.deviceUniqueIdentifier;
		Auth_RegisterPlayerAndGetAuthTokenCommand cmd = new Auth_RegisterPlayerAndGetAuthTokenCommand (CommandTest.playerId, CommandTest.secret,
		delegate(Hashtable data){
			Debug.Log ("authToken = " + data ["result"]);	
			FPS.InfoString = "Register Success";
			CommandTest.authToken = data ["result"] as string;
			this.complete();
		},
		delegate(string err_code,string err_msg,Hashtable data){
			Debug.Log ("error " + err_code);	
			FPS.InfoString = "register Failed "+err_code;
			if(err_code =="805"){
				//already
				step2 ();
			}else{
				this.error();	
			}
		}
		);
		cmd.excute ();
	}
	
	private void step2(){
		Auth_GetAutoTokenCommand cmd = new Auth_GetAutoTokenCommand (CommandTest.playerId, CommandTest.secret,
			delegate(Hashtable data){
				Debug.Log ("authToken = " + data ["result"]);	
				CommandTest.authToken = data ["result"] as string;
				FPS.InfoString = "Login Success";
				complete();
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log ("error " + err_code);	
				FPS.InfoString = "Login Failed "+err_code;
				this.error();
			}
			);
			cmd.excute ();
	}
		
}
