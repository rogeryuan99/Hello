using UnityEngine;
using System;
using System.Collections;
using System.Xml;

using TYPE = TsObjectFactory.TYPE;

public class TsCreationDef :TsDefinition {
	
	private TYPE type;
	private string prefix;
	private string obj;
	private string parent;
	private float scale;
	
	public TYPE Type{
		get{ return type; }
	}
	public string Prefix{
		get{ return prefix; }
	}
	public string Obj{
		get{ return obj; }
	}
	public string Parent{
		get{ return parent; }
	}
	public float Scale{
		get{ return scale; }
	}
	
	// Functions 
	
	public override void FillWithXmlNode(XmlNode node){
		type = (TYPE)Enum.Parse(typeof(TYPE), node.Attributes["type"].Value);
		prefix = node.Attributes["prefix"].Value;
		obj = node.Attributes["obj"].Value;
		parent = node.Attributes["parent"].Value;
		scale = float.Parse(node.Attributes["scale"].Value);
	}
	
	public override string ToString(){
		return string.Format ("[TsCreationDef: Type={0} Prefix={1} Obj={2} Parent={3} Scale={4}]", Type, Prefix, Obj, Parent, Scale);
	}
}
