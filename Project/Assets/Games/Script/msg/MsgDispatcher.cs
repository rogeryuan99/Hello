using UnityEngine;
using System.Collections;

public class MsgDispatcher{
	private Hashtable listers = new Hashtable();
	
	public delegate void Function(Message msg);
	
	public void addListener ( string eventName ,   Function func  ){
		if( listers[eventName] == null)
		{
			listers[eventName] = new ArrayList();
		}
		ArrayList funcAry = listers[eventName] as ArrayList;
		funcAry.Add(func);
		listers[eventName] = funcAry;
	}
	
	public void removeListener ( string eventName ,   Function func  ){
		if(listers[eventName]!=null)
		{
			ArrayList funcAry = listers[eventName] as ArrayList;
			funcAry.Remove(func);
			if(funcAry.Count == 0)
			{
				listers.Remove(eventName);
			}else{
				listers[eventName] = funcAry;
			}
		}
	}
	
	public void removeAll (){
		foreach( string key in listers.Keys)
		{
			listers.Remove(key);
		}
	}
	
	public void dispatch ( Message msg  ){
//		Debug.Log("---all listeners");
//		foreach(string s in listers.Keys){
//			Debug.Log(s);	
//		}
//		Debug.Log("===all listeners");
		if(listers[msg.name]!=null)
		{
//			Debug.Log("===funcs");
			ArrayList funcAry = listers[msg.name] as ArrayList;
			for( int i=0; i<funcAry.Count; i++)
			{
//				Debug.Log(""+funcAry[i]);
				Function func = funcAry[i] as Function;
//				Debug.Log("func = "+ func);
				func(msg);
//				Debug.Log(""+funcAry[i]);
			}
//			Debug.Log("===funcs");
		}
	}
}
public class Message
{
	public object sender;
	public object data;
	public string name;
	
	public Message ( string msgName ,   object sender ,   object data  ){
		this.data = data;
		name = msgName;
		this.sender = sender;
	}
	
	public Message ( string msgName ,   object sender  ){
		name = msgName;
		this.sender = sender;
	}
}
