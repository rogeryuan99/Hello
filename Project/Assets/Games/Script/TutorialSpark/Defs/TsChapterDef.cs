using UnityEngine;
using System.Collections;
using System.Xml;

public class TsChapterDef :TsDefinition {

	private TsStepDef[] steps;
	
	public TsStepDef[] Steps{
		get{ return steps; }
	}
	
	public void FillWithXmlNode(XmlNode node){
		steps = GetListFromXmlNode<TsStepDef>(node);
	}
	
	public override string ToString(){
		string result = "[TsChapterDef:]\n";
		for (int i=0; i<steps.Length; i++){
			result += steps[i].ToString();
		}
		
		return result;
	}
}
