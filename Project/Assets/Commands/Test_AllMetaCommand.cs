using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test_AllMetaCommand : Command {
	//type = a:1/10, b:1/100, c:1/1000
	public Test_AllMetaCommand( string playerId,string authToken,string nodePath, CompleteDelegate completeDelegate, ErrorDelegate errorDelegate){
		Hashtable batchHash = new Hashtable ();
		batchHash.Add ("authKey", authToken);
		
		ArrayList commands = new ArrayList();
		Hashtable command = new Hashtable ();
		command.Add ("action", "test.allMeta");
		command.Add ("time", TimeUtils.UnixTime);
		command.Add ("args", new Hashtable () { { "playerId", playerId },{"nodePath",nodePath}});
		command.Add ("requestId", 123);
		commands.Add(command);
		batchHash.Add("commands",commands);
		
		batch = MiniJSON.jsonEncode(batchHash);
		
		////////
		this.onComplete = delegate(Hashtable t){
			completeDelegate(t);
		};
		/////////
		this.onError = delegate(string err_code,string err_msg, Hashtable data){
			errorDelegate(err_code,err_msg,data);
		};
	
	}
}


//parameters:
// u = player uid
// t = type
// p = useCash true/false
//result[HashTable]:
//  isdup:False <System.Boolean>
//  char[HashTable]:
//    hp:259 <System.Double>
//    rare:2 <System.Double>
//    equips[ArrayList]:
//    seq:20 <System.Double>
//    clan:0 <System.Double>
//    atk:56 <System.Double>
//    uid:pc:3:1:20 <System.String>
//    xp:0 <System.Double>
//    sp:101 <System.Double>
//    trumps[ArrayList]:
//    def:73 <System.Double>
//    mind:0 <System.Double>
//    typeid:040 <System.String>
//err_code:0 <System.Double>
