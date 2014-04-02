using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class HTTPClientManager :Singleton<HTTPClientManager>
{
	Queue<WebRequest> _queue = new Queue<WebRequest>();
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		performRequest();
	}

	private void performRequest()
	{
		if(_queue.Count != 0)
		{ 
			WebRequest req = _queue.Dequeue();
			WWWForm form = new WWWForm();
			if(req.param != null){
				foreach(string k in req.param.Keys)
				{
					form.AddField(k, req.param[k]);
				}
			}
			req.www = new WWW(req.url, form);

			string s = " >>> "+req.dumpRequest();
			if(req.retryCount>0) s = "RETRY "+req.retryCount +s;
			Debug.Log(s);
			
			StartCoroutine(waitForRequest(req));
		}
	}
	
	public void commitRequest(WebRequest req)
	{
		_queue.Enqueue(req);
	}


	IEnumerator waitForRequest(WebRequest req)
	{
		yield return req.www;
		
		if(req.www.error != null)
		{
			dumpError(req);
			if(req.retryCount<3){
				req.retryCount++;
				req.www = null;
				commitRequest(req);
			}else{
				onNetError();
			}
		}else{
			try{
				HTTPClientManager.dumpOK(req);
			}catch(Exception e){
				Debug.LogError(e);
				onNetError();
			}
			if(req.finishedDelegate != null)
			{
				req.finishedDelegate(req);
			}
		}
	}
	
	// Added By FuNing _9.12.2012
	public static void dumpOK(WebRequest req){
//		string s="<<< [" + req.seq +"] "+ req.dumpRequest();
//		Debug.Log(s);
		string s= "<<< [" + req.seq +"] "+ req.www.text;
		Debug.Log(s);
	}
	public static void dumpError(WebRequest req){
		string s="<<<ERROR :"+req.www.error+" [" + req.seq +"] "+ req.dumpRequest();
		
		Debug.Log(s);
	}
	// End 
	public enum ErrorType{
		 NO_ERR = 0
	}
	
	int netErrorCounter = 0;
	private void onNetError(){
		Debug.LogError("net error");
		CommonDlg dlg = DlgManager.instance.ShowCommonDlg("net work error. Please make sure you can access\nhttp://n7vgq1ggaxmapp01.general.disney.private:8080/app/monitor/health");
		dlg.setOneBtnDlg();
		dlg.onOk = () => {
			Application.Quit();
		};
	}
}





 
