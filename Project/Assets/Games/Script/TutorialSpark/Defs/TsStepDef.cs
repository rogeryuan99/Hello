using UnityEngine;
using System;
using System.Collections;
using System.Xml;

public class TsStepDef :TsDefinition {

	private TsCreationDef[] creations;
	private TsActionDef[] actions;
	
	public TsCreationDef[] Creations{
		get{ return creations; }
	}
	public TsActionDef[] Actions{
		get{ return actions; }
	}
	
	public override void FillWithXmlNode(XmlNode node){
		creations = GetListFromXmlNode<TsCreationDef>(node.ChildNodes[0]);
		actions = GetListFromXmlNode<TsActionDef>(node.ChildNodes[1]);
	}
	
	public override string ToString(){
		string result = "[TsStepDef:]\n";
		for (int i=0; i<creations.Length; i++){
			result += creations[i].ToString() + "\n";
		}
		result += "------\n";
		for (int i=0; i<actions.Length; i++){
			result += actions[i].ToString() + "\n";
		}
		
		return result;
	}
}
