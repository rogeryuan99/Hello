using UnityEngine;
using System.Collections;
using System.Xml;

public class TsActionDef :TsDefinition {
	/// <action obj="xxxDialog" call="FillData" parms="{0}Title{1}Description{2}headIcon"/>
	private string obj;
	private string call;
	private string parms;
	
	public string Obj{
		get{ return obj; }
	}
	public string Call{
		get{ return call; }
	}
	public string Parms{
		get{ return parms; }
	}
	
	public override void FillWithXmlNode(XmlNode node){
		obj = node.Attributes["obj"].Value;
		call = node.Attributes["call"].Value;
		parms = node.Attributes["parms"].Value;
	}
	
	public override string ToString ()
	{
		return string.Format ("[TsActionDef: Obj={0}, Call={1}, Parms={2}]", Obj, Call, Parms);
	}
}
