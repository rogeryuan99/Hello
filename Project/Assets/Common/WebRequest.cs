using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class WebRequest
{
	public string url;
	public Dictionary<string,string> param;
	public int seq = -1;
	static int g_seq = 0;
	public WWW www;
	public delegate void WebRequestFinishedDelegate(WebRequest req);
	public WebRequestFinishedDelegate finishedDelegate;
	public string code=null;
	public int retryCount = 0;
	public static WebRequest GenerateRequest(string batch)
	{
		Dictionary<string,string> d = new Dictionary<string, string>();
		d.Add("batch",batch);
		WebRequest req = new WebRequest();
		req.seq = g_seq++;
		req.param = d;
		return req;
	}
	public static WebRequest GenerateRequest()
	{
		WebRequest req = new WebRequest();
		req.seq = g_seq++;
		return req;
	}
	public string dumpRequest(){
		string s = www.url+"?";
		int n = 0;
		if(param != null){
			foreach(string k in param.Keys)
			{
				if(n++>0) s+="&";
				s+= k+"="+param[k];
			}
		}
		return s;
	}
	
}

