using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class TsTriggerEventDef : TsDefinition {
	
	private const string BLOCK_NAME = "-";
	
	enum TYPE{
		NORMAL = 0
	}
	
	private string id;
	private string name;
	private TYPE type;
	private List<string> rejects;
	private List<string> branches;
	private string call;
	private string[] parms;
	private string[] preconditions;
	private bool block = false;
	
	public string Id{
		get{ return id; }
	}
	
	public string Name{
		get{ 
			return (true == block? BLOCK_NAME: name); 
		}
	}
	
	public string[] Rejects{
		get{ return (0 == rejects.Count? null: rejects.ToArray()); }
	}
	
	public string[] Branches{
		get{ return (0 == branches.Count? null: branches.ToArray()); }
	}
	
	public string Call{
		get{ return call; }
	}
	
	public string[] Parms{
		get{ return parms; }
	}
	
	public string[] Ids{
		get{
			string[] ids = new string[rejects.Count + 1];
			ids[0] = id;
			rejects.CopyTo(ids, 1);
			
			return ids;
		}
	}
	
	public string[] Preconditions{
		get{
			return preconditions;
		}
	}
	
	public bool Block{
		get{ return block; }
		set{ block = value; }
	}
	
	public override void FillWithXmlNode(XmlNode node){
		id           = node.Attributes["id"].Value;
		name         = node.Attributes["name"].Value;
		type         = (TYPE)Enum.Parse(typeof(TYPE), node.Attributes["type"].Value);
		rejects      = TsParmsTranslator.TranslateToList (node.Attributes["rejects"].Value);
		branches     = TsParmsTranslator.TranslateToList (node.Attributes["branches"].Value);
		call         = node.Attributes["call"].Value;
		parms        = TsParmsTranslator.Translate (node.Attributes["parms"].Value);
		preconditions= TsParmsTranslator.Translate (node.Attributes["preconditions"].Value);
	}
	
	public override string ToString ()
	{
		return string.Format ("[TsTriggerEventDef: id={0}, name={1}, type={2}, rejects={3}, call={4}, parms={5}, preconditions={6}]", 
			id, name, type, rejects.ToString(), call, parms.ToString(), preconditions.ToString());
	}
}
