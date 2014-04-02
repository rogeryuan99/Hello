using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command
{
	//protected static string fileServerUrl = "http://192.168.1.125/"; roger mac
	protected static string fileServerUrl = "http://54.251.224.41/gotg/1218/"; // amazon
	
	protected WebRequest req;
	public delegate void CompleteDelegate (Hashtable data);
	public delegate void ErrorDelegate (string err_code,string err_msg,Hashtable data);
	public CompleteDelegate onComplete;
	public ErrorDelegate onError;
	public string batch;

	public Command ()
	{
	}

	public virtual void excute ()
	{
		if (batch == null) {
			Debug.LogError ("actionName is null");	
		}
		WebRequest req = WebRequest.GenerateRequest (batch);
		req.url = BuildSetting.gameServerUrl;
		if (req == null) {
			Debug.LogError ("null req");
			return;
		}
		req.finishedDelegate = reqFinished;
		HTTPClientManager.Instance.commitRequest (req);
	}
	
	public virtual void reqFinished (WebRequest req)  //private ->public
	{
		if (req.www.error != null) {
			Debug.LogError (" req error:" + req.dumpRequest());
		} else {
			Hashtable t = MiniJsonExtensions.hashtableFromJson (req.www.text);
			if (t == null) {
				Debug.LogError ("http call return empty");	
				this.onError("Null HashTable","Null HashTable",null);
				return;
			}
			// {"protocolVersion":1,"serverTime":"2013-11-29 15:24:42,727","serverTimestamp":1385709882,"data":[{"requestId":null,"messages":{},"result":"aWeha_JMFgzaF5zWMR3tnObOtLZNPR4rO70DNdfWPvc.eyJ1c2VySWQiOiIyMCIsImV4cGlyZXMiOiIxMzg1NzA5ODgyIn0","status":0}]}
			ArrayList datas = t["data"] as ArrayList;
			if(datas == null || datas.Count != 1){
				Debug.LogError ("http call return no data");	
				this.onError("Null Data","Null Data",null);
				return;
			}
			Hashtable data = datas[0] as Hashtable;
			string status = "" + data ["status"];
			Hashtable result = data["result"] as Hashtable;
			if(result == null && data["result"] is string){
				result = data;	
			}
			if(result!=null)Debug.Log (Utils.dumpHashTable (result));
			if (status != "0" ) {
				Debug.LogError ("status=" + status );
				onError (status, status, result);
			} else{
				onComplete(result);	
			}
		}
	}
	

}
