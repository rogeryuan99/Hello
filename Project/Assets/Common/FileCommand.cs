using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FileCommand:Command
{
	protected string fileName;
	
	public FileCommand( string fileName , CompleteDelegate completeDelegate, ErrorDelegate errorDelegate){
		this.fileName = fileName;
		this.onComplete = completeDelegate;
		this.onError = errorDelegate;
	}

	public override void excute ()
	{
		WebRequest req = WebRequest.GenerateRequest ("decache");
		req.url = fileServerUrl+fileName;
		if (req == null) {
			Debug.LogError ("null req");
			return;
		}
		req.finishedDelegate = reqFinished;
		HTTPClientManager.Instance.commitRequest (req);
	}
	
	public override void reqFinished (WebRequest req)  //private ->public
	{
		if (req.www.error != null) {
			Debug.LogError (" req error:" + req.dumpRequest());
		} else {
			Hashtable h = new Hashtable();
			h.Add("text",req.www.text);
			onComplete(h);	
		}
	}
	

}
